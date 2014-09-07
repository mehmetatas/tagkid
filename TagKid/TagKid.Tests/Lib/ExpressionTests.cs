using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq.Expressions;

namespace TagKid.Tests.Lib
{
    [TestClass]
    public class ExpressionTests
    {
        [TestMethod]
        public void TestParam()
        {
            Search<Student>(s => s.Id == 1L);

            GetStudent(1L);
        }

        private void GetStudent(long id)
        {
            Search<Student>(s => s.Id == id);
        }

        private void Search<T>(Expression<Func<T, bool>> filter)
        {
            var visitor = new MyExpressionVisitor();
            visitor.Visit(filter);
        }
    }

    public class MyExpressionVisitor : ExpressionVisitor
    {
        protected override Expression VisitConstant(ConstantExpression node)
        {
            var value = node.Value;

            if (value.GetType().IsNested)
            {
                dynamic d = node.Value;
                value = d.id;
            }

            Assert.AreEqual(1L, value);
            return base.VisitConstant(node);
        }
    }

    public class Student
    {
        public long Id { get; set; }
    }
}
