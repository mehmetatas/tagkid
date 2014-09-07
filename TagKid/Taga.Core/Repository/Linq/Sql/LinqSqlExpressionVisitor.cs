using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;

namespace Taga.Core.Repository.Linq.Sql
{
    internal class LinqSqlExpressionVisitor : ExpressionVisitor
    {
        private LinqSqlQuery _sql;
        private readonly StringBuilder _whereBuilder;
        private readonly ILinqSqlSchemaSolver _sqlSchemaSolver;

        public LinqSqlExpressionVisitor(ILinqSqlSchemaSolver sqlSchemaSolver)
        {
            _whereBuilder = new StringBuilder();
            _sqlSchemaSolver = sqlSchemaSolver;
        }

        public LinqSqlQuery ToSql(Expression expression)
        {
            _sql = new LinqSqlQuery();
            _whereBuilder.Clear();

            base.Visit(expression);

            _sql.Where = _whereBuilder.ToString();
            return _sql;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var parsed = false;
            switch (node.Method.Name)
            {
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
                    parsed = ParseInExpression(node);
                    break;
            }

            if (!parsed)
                throw new NotSupportedException(String.Format("The method '{0}' is not supported", node.Method.Name));

            return node;
        }

        protected override Expression VisitUnary(UnaryExpression node)
        {
            switch (node.NodeType)
            {
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

        protected override Expression VisitConstant(ConstantExpression node)
        {
            var isQuery = node.Value is IQueryable;

            if (isQuery)
                return node;

            if (node.Value == null)
            {
                _whereBuilder.Append(" NULL");
                return node;
            }

            var type = node.Value.GetType();

            switch (Type.GetTypeCode(type))
            {
                case TypeCode.Object:
                    if (type.IsNested && type.Name.Contains("DisplayClass"))
                    {
                        _whereBuilder.AppendFormat(" @{0}", _sql.Parameters.Count);
                        _sql.Parameters.Add(type.GetFields()[0].GetValue(node.Value));
                    }
                    else
                    {
                        throw new NotSupportedException(String.Format("The constant for '{0}' is not supported", node.Value));
                    }
                    break;
                default:
                    _whereBuilder.AppendFormat(" @{0}", _sql.Parameters.Count);
                    _sql.Parameters.Add(node.Value);
                    break;
            }

            return node;
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression != null)
            {
                if (node.Expression.NodeType == ExpressionType.Parameter)
                {
                    _whereBuilder.Append(GetColumnName(node.Member));
                    return node;
                }

                if (node.Expression.NodeType == ExpressionType.Constant)
                {
                    VisitConstant((ConstantExpression)node.Expression);
                    return node;
                }
            }

            throw new NotSupportedException(String.Format("The member '{0}' is not supported", node.Member.Name));
        }

        protected override Expression VisitBinary(BinaryExpression node)
        {
            _whereBuilder.Append(" (");

            base.Visit(node.Left);

            switch (node.NodeType)
            {
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

        protected bool IsNullConstant(Expression exp)
        {
            return exp.NodeType == ExpressionType.Constant && ((ConstantExpression)exp).Value == null;
        }

        private bool ParseStringStartsWithExpression(MethodCallExpression expression)
        {
            if (expression.Method.DeclaringType != typeof(string))
                return false;

            if (expression.Object == null)
                return false;

            var not = " NOT";
            var isNot = _whereBuilder.EndsWith(not);
            if (isNot)
            {
                _whereBuilder.RemoveLast(not.Length);
            }
            else
            {
                not = String.Empty;
            }

            var columnName = GetColumnName(((MemberExpression)expression.Object).Member);
            var valueExpression = (ConstantExpression)expression.Arguments[0];

            _whereBuilder.AppendFormat(" {0}{1} LIKE '%' + @P_{2}", columnName, not, _sql.Parameters.Count);
            _sql.Parameters.Add(valueExpression.Value);
            return true;
        }

        private bool ParseStringEndsWithExpression(MethodCallExpression expression)
        {
            if (expression.Method.DeclaringType != typeof(string))
                return false;

            if (expression.Object == null)
                return false;

            var not = " NOT";
            var isNot = _whereBuilder.EndsWith(not);
            if (isNot)
            {
                _whereBuilder.RemoveLast(not.Length);
            }
            else
            {
                not = String.Empty;
            }

            var columnName = GetColumnName(((MemberExpression)expression.Object).Member);
            var valueExpression = (ConstantExpression)expression.Arguments[0];

            _whereBuilder.AppendFormat(" {0}{1} LIKE @P_{2} + '%'", columnName, not, _sql.Parameters.Count);
            _sql.Parameters.Add(valueExpression.Value);
            return true;
        }

        private bool ParseStringContainsExpression(MethodCallExpression expression)
        {
            if (expression.Method.DeclaringType != typeof(string))
                return false;

            if (expression.Object == null)
                return false;

            var not = " NOT";
            var isNot = _whereBuilder.EndsWith(not);
            if (isNot)
            {
                _whereBuilder.RemoveLast(not.Length);
            }
            else
            {
                not = String.Empty;
            }

            var columnName = GetColumnName(((MemberExpression)expression.Object).Member);
            var valueExpression = (ConstantExpression)expression.Arguments[0];

            _whereBuilder.AppendFormat(" {0}{1} LIKE '%' + @P_{2} + '%'", columnName, not, _sql.Parameters.Count);
            _sql.Parameters.Add(valueExpression.Value);
            return true;
        }

        private bool ParseInExpression(MethodCallExpression expression)
        {
            if (expression.Method.DeclaringType != typeof(LinqRepositoryExtensions))
                return false;

            var not = " NOT";
            var isNot = _whereBuilder.EndsWith(not);
            if (isNot)
            {
                _whereBuilder.RemoveLast(not.Length);
            }
            else
            {
                not = String.Empty;
            }

            var columnName = GetColumnName(((MemberExpression)expression.Arguments[0]).Member);
            var newArrExpression = (NewArrayExpression)expression.Arguments[1];
            var values = newArrExpression.Expressions.Select(exp => ((ConstantExpression)exp).Value).ToArray();

            _whereBuilder.AppendFormat(" {0}{1} IN ({2})", columnName, not,
                String.Join(",", Enumerable.Range(0, values.Length).Select(i => String.Format("@{0}", i + _sql.Parameters.Count))));
            _sql.Parameters.AddRange(values);
            return true;
        }

        private string GetColumnName(MemberInfo memberInfo)
        {
            return _sqlSchemaSolver.GetColumnName((PropertyInfo)memberInfo);
        }
    }
}