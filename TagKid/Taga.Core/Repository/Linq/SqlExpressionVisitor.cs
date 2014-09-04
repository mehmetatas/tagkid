using System;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Taga.Core.Repository.Linq
{
    public class SqlExpressionVisitor : ExpressionVisitor
    {
        private Sql _sql;
        private readonly StringBuilder _whereBuilder;

        public SqlExpressionVisitor() {
            _whereBuilder = new StringBuilder();
        }

        public Sql ToSql(Expression expression) {
            _sql = new Sql();
            _whereBuilder.Clear();

            base.Visit(expression);

            _sql.Where = _whereBuilder.ToString();
            return _sql;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node) {
            if(node.Method.DeclaringType == typeof(Queryable) && node.Method.Name == "Where") {
                base.Visit(node.Arguments[0]);
                var lambda = (LambdaExpression)StripQuotes(node.Arguments[1]);
                base.Visit(lambda.Body);
                return node;
            }

            var parsed = false;
            switch(node.Method.Name) {
                case "StartsWith":
                    parsed = ParseStringStartsWithExpression(node);
                    break;
                case "EndsWith":
                    parsed = ParseStringEndsWithExpression(node);
                    break;
                case "Contains":
                    parsed = ParseStringContainsExpression(node);
                    break;
                case "In":
                    parsed = ParseStringInExpression(node);
                    break;
            }

            if(!parsed)
                throw new NotSupportedException(String.Format("The method '{0}' is not supported", node.Method.Name));

            //var nextExpression = node.Arguments[0];
            //var expression = base.Visit(nextExpression);
            //return expression;
            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node) {
            switch(node.NodeType) {
                case ExpressionType.Not:
                    _whereBuilder.Append(" NOT");
                    base.Visit(node.Operand);
                    break;
                case ExpressionType.Convert:
                    base.Visit(node.Operand);
                    break;
                default:
                    throw new NotSupportedException(String.Format("The unary operator '{0}' is not supported", node.NodeType));
            }
            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node) {
            var isQuery = node.Value is IQueryable;

            if(isQuery)
                return node;

            if(node.Value == null) {
                _whereBuilder.Append(" NULL");
                return node;
            }

            switch(Type.GetTypeCode(node.Value.GetType())) {
                case TypeCode.Object:
                    throw new NotSupportedException(String.Format("The constant for '{0}' is not supported", node.Value));

                default:
                    _whereBuilder.AppendFormat(" @P_{0}", _sql.Parameters.Count);
                    _sql.Parameters.Add(node.Value);
                    break;
            }

            return node;
        }

        protected override Expression VisitMember(MemberExpression node) {
            if(node.Expression != null && node.Expression.NodeType == ExpressionType.Parameter) {
                _whereBuilder.Append(node.Member.Name);
                return node;
            }

            throw new NotSupportedException(String.Format("The member '{0}' is not supported", node.Member.Name));
        }

        protected override Expression VisitBinary(BinaryExpression node) {
            _whereBuilder.Append(" (");

            base.Visit(node.Left);

            switch(node.NodeType) {
                case ExpressionType.And:
                case ExpressionType.AndAlso:
                    _whereBuilder.Append(" AND");
                    break;
                case ExpressionType.Or:
                case ExpressionType.OrElse:
                    _whereBuilder.Append(" OR");
                    break;
                case ExpressionType.Equal:
                    _whereBuilder.Append(IsNullConstant(node.Right) ? " IS" : " =");
                    break;
                case ExpressionType.NotEqual:
                    _whereBuilder.Append(IsNullConstant(node.Right) ? " IS NOT" : " <>");
                    break;
                case ExpressionType.LessThan:
                    _whereBuilder.Append(" <");
                    break;
                case ExpressionType.GreaterThan:
                    _whereBuilder.Append(" >");
                    break;
                case ExpressionType.LessThanOrEqual:
                    _whereBuilder.Append(" <=");
                    break;
                case ExpressionType.GreaterThanOrEqual:
                    _whereBuilder.Append(" >=");
                    break;
                default:
                    throw new NotSupportedException(String.Format("The binary operator '{0}' is not supported", node.NodeType));
            }

            base.Visit(node.Right);

            _whereBuilder.Append(")");

            return node;
        }

        protected bool IsNullConstant(Expression exp) {
            return exp.NodeType == ExpressionType.Constant && ((ConstantExpression)exp).Value == null;
        }

        private static Expression StripQuotes(Expression node) {
            while(node.NodeType == ExpressionType.Quote) {
                node = ((UnaryExpression)node).Operand;
            }
            return node;
        }

        private bool ParseStringStartsWithExpression(MethodCallExpression expression) {
            if(expression.Method.DeclaringType != typeof(string))
                return false;

            if(expression.Object == null)
                return false;

            var not = " NOT";
            var isNot = _whereBuilder.EndsWith(not);
            if(isNot) {
                _whereBuilder.RemoveLast(not.Length);
            } else {
                not = String.Empty;
            }

            var propName = ((MemberExpression)expression.Object).Member.Name;
            var valueExpression = (ConstantExpression)expression.Arguments[0];

            _whereBuilder.AppendFormat(" {0}{1} LIKE '%' + @P_{2}", propName, not, _sql.Parameters.Count);
            _sql.Parameters.Add(valueExpression.Value);
            return true;
        }

        private bool ParseStringEndsWithExpression(MethodCallExpression expression) {
            if(expression.Method.DeclaringType != typeof(string))
                return false;

            if(expression.Object == null)
                return false;

            var not = " NOT";
            var isNot = _whereBuilder.EndsWith(not);
            if(isNot) {
                _whereBuilder.RemoveLast(not.Length);
            } else {
                not = String.Empty;
            }

            var propName = ((MemberExpression)expression.Object).Member.Name;
            var valueExpression = (ConstantExpression)expression.Arguments[0];

            _whereBuilder.AppendFormat(" {0}{1} LIKE @P_{2} + '%'", propName, not, _sql.Parameters.Count);
            _sql.Parameters.Add(valueExpression.Value);
            return true;
        }

        private bool ParseStringContainsExpression(MethodCallExpression expression) {
            if(expression.Method.DeclaringType != typeof(string))
                return false;

            if(expression.Object == null)
                return false;

            var not = " NOT";
            var isNot = _whereBuilder.EndsWith(not);
            if(isNot) {
                _whereBuilder.RemoveLast(not.Length);
            } else {
                not = String.Empty;
            }

            var propName = ((MemberExpression)expression.Object).Member.Name;
            var valueExpression = (ConstantExpression)expression.Arguments[0];

            _whereBuilder.AppendFormat(" {0}{1} LIKE '%' + @P_{2} + '%'", propName, not, _sql.Parameters.Count);
            _sql.Parameters.Add(valueExpression.Value);
            return true;
        }

        private bool ParseStringInExpression(MethodCallExpression expression) {
            if(expression.Method.DeclaringType != typeof(Extensions))
                return false;

            var not = " NOT";
            var isNot = _whereBuilder.EndsWith(not);
            if(isNot) {
                _whereBuilder.RemoveLast(not.Length);
            } else {
                not = String.Empty;
            }

            var propName = ((MemberExpression)expression.Arguments[0]).Member.Name;
            var newArrExpression = (NewArrayExpression)expression.Arguments[1];
            var values = newArrExpression.Expressions.Select(exp => ((ConstantExpression) exp).Value).ToArray();

            _whereBuilder.AppendFormat(" {0}{1} IN ({2})", propName, not, String.Join(",", Enumerable.Range(0, values.Length).Select(i => String.Format("@P_{0}", i + _sql.Parameters.Count))));
            _sql.Parameters.AddRange(values);
            return true;
        }
    }
}