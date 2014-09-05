using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Taga.Core.IoC;

namespace TagKid.Tests.Core
{
    [TestClass]
    public class ServiceProviderTests
    {
        [TestMethod]
        public void TestGenericType()
        {
            var prov = ServiceProvider.Provider;

            prov.Register(typeof(IGenericInterface<>), typeof(GenericClass<>));

            var s = prov.GetOrCreate<IGenericInterface<string>>();
            Assert.AreEqual(typeof(GenericClass<string>), s.GetType());

            var i = prov.GetOrCreate<IGenericInterface<int>>();
            Assert.AreEqual(typeof(GenericClass<int>), i.GetType());
        }

        //[TestMethod]
        //public void TestExplicitGenericType2()
        //{
        //    var prov = ServiceProvider.Provider;

        //    prov.Register(typeof(IGenericInterface<string>), typeof(GenericClass<string>));

        //    var i = prov.GetOrCreate<IGenericInterface<int>>();
        //    Assert.AreEqual(typeof(GenericClass<int>), i.GetType());
        //}

        [TestMethod]
        public void TestExplicitGenericType()
        {
            var prov = ServiceProvider.Provider;

            prov.Register(typeof(IGenericInterface<string>), typeof(GenericClass<string>));

            var s = prov.GetOrCreate<IGenericInterface<string>>();
            Assert.AreEqual(typeof(GenericClass<string>), s.GetType());
        }
    }

    interface IGenericInterface<T>
    {
         
    }

    class GenericClass<T> : IGenericInterface<T>
    {
        
    }
}
