using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Taga.Core.Dynamix;
using TagKid.Core.Models.Database;
using Taga.Core.DynamicProxy;

namespace TagKid.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                //var mapper = new PocoMapper();


                var mapper = CreateMapper<User>();

                var users = new List<User>();
                using (var conn = new SqlConnection("Server=localhost;Database=TagKid;uid=sa;pwd=123456;"))
                {
                    conn.Open();

                    var cmd = conn.CreateCommand();
                    cmd.CommandText = "select * from [User]";
                    var reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        users.Add(mapper.Map(reader));
                    }
                }

                Console.WriteLine("OK!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }

        private static IPocoMapper<T> CreateMapper<T>() where T : class, new()
        {
            var assemblyBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(
                new AssemblyName("Taga.Core.Dynamix.Proxies"),
                AssemblyBuilderAccess.Run);

            var assemblyName = assemblyBuilder.GetName().Name;

            var moduleBuilder = assemblyBuilder.DefineDynamicModule(assemblyName);

            var entityType = typeof (T);

            var typeName = entityType.Name + "Mapper";

            var interfaceType = typeof (IPocoMapper<>).MakeGenericType(entityType);

            var typeBuilder = moduleBuilder.DefineType(
                typeName,
                TypeAttributes.Public | TypeAttributes.Class,
                typeof (object),
                new[] {interfaceType});

            var baseCtor = typeof(object).GetConstructor(Type.EmptyTypes);

            var ctorBuilder = typeBuilder.DefineConstructor(
                MethodAttributes.Public,
                CallingConventions.HasThis,
                Type.EmptyTypes);

            var ctorIL = ctorBuilder.GetILGenerator();
            ctorIL.Emit(OpCodes.Ldarg_0);
            ctorIL.Emit(OpCodes.Call, baseCtor);
            ctorIL.Emit(OpCodes.Ret);

            var methodInfo = interfaceType.GetMethod("Map");

            // public override? ReturnType Method(arguments...)
            var methodBuilder = typeBuilder.DefineMethod(methodInfo.Name,
                MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.Virtual,
                methodInfo.ReturnType,
                methodInfo.GetParameterTypes());
            
            var methodIL = methodBuilder.GetILGenerator();
               
            var entityCtor = typeof(T).GetConstructor(Type.EmptyTypes);

            var user = methodIL.DeclareLocal(entityType); // User user;
            methodIL.Emit(OpCodes.Newobj, entityCtor); // new User();   
            methodIL.Emit(OpCodes.Stloc, user); // user = new User();

            var i = 0;
            foreach (var property in entityType.GetProperties())
            {
                var drMethod = GetDataRecordMethod(property.PropertyType);
                var setter = property.GetSetMethod(true);

                methodIL.Emit(OpCodes.Ldloc, user);
                methodIL.Emit(OpCodes.Ldarg_1);
                methodIL.Emit(OpCodes.Ldc_I4, i++);
                methodIL.Emit(OpCodes.Callvirt, drMethod);
                methodIL.Emit(OpCodes.Callvirt, setter);
            }

            methodIL.Emit(OpCodes.Ldloc, user);
            methodIL.Emit(OpCodes.Ret);

            var mapperType = typeBuilder.CreateType();

            return (IPocoMapper<T>)Activator.CreateInstance(mapperType);
        }

        private static MethodInfo GetDataRecordMethod(Type type)
        {
            if (type == typeof(long))
            {
                return typeof(IDataRecord).GetMethod("GetInt64");
            }
            if (type == typeof(string))
            {
                return typeof(IDataRecord).GetMethod("GetString");
            }
            if (type == typeof(DateTime))
            {
                return typeof(IDataRecord).GetMethod("GetDateTime");
            }
            if (type.IsEnum)
            {
                return typeof(IDataRecord).GetMethod("GetInt32");
            }
            throw new Exception(type.ToString());
        }
    }


    public class PocoMapper : IPocoMapper<User>
    {
        public User Map(IDataReader reader)
        {
            var user = new User();

            user.Id = reader.GetInt64(0);
            user.Fullname = reader.GetString(1);
            user.Email = reader.GetString(2);
            user.Username = reader.GetString(3);
            user.Password = reader.GetString(4);
            user.JoinDate = reader.GetDateTime(5);
            user.FacebookId = reader.GetString(6);
            user.Type = (UserType)reader.GetInt32(7);
            user.Status = (UserStatus)reader.GetInt32(8);

            return user;
        }
    }
}