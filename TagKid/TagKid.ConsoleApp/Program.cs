using System;
using System.Data;
using Taga.Core.Dynamix;
using TagKid.Core.Models.Database;

namespace TagKid.ConsoleApp
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                //var mapper = PocoMapper.For<User>();
                //var user = mapper.Map<User>(new MockDataReader());
                var d = new SecondDerivative();
                d.GetChild();

                Console.WriteLine("OK!");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.ReadLine();
        }
    }

    public class User
    {
        public virtual long Id { get; set; }
        public virtual string Email { get; set; }
        
        public virtual int Int32NotNull { get; set; }
        public virtual int? Int32Null1 { get; set; }
        public virtual int? Int32Null2 { get; set; }
        
        public virtual DateTime DateTimeNotNull { get; set; }
        public virtual DateTime? DateTimeNull1 { get; set; }
        public virtual DateTime? DateTimeNull2 { get; set; }

        public virtual UserType EnumNotNull { get; set; }
        public virtual UserType? EnumNull1 { get; set; }
        public virtual UserType? EnumNull2 { get; set; }
    }

    public class MockDataReader : IDataReader
    {

        private User user = new User
        {
            Id = 1,
            Email = "Mail",
            Int32NotNull = 1,
            Int32Null1 = 2,
            DateTimeNotNull = DateTime.Now,
            DateTimeNull1 = DateTime.Now.AddDays(1),
            EnumNotNull = UserType.Moderator,
            EnumNull1 = UserType.Admin
        };

        public object GetValue(int i)
        {
            if (i == 0)
                return user.Id;
            if (i == 1)
                return user.Email;
            if (i == 2)
                return user.Int32NotNull;
            if (i == 3)
                return user.Int32Null1.Value;
            if (i == 4)
                return DBNull.Value;
            if (i == 5)
                return user.DateTimeNotNull;
            if (i == 6)
                return user.DateTimeNull1.Value;
            if (i == 7)
                return DBNull.Value;
            if (i == 8)
                return user.EnumNotNull;
            if (i == 9)
                return user.EnumNull1.Value;
            if (i == 10)
                return DBNull.Value;
            return DBNull.Value;
        }

        public bool Read()
        {
            throw new NotImplementedException();
        }

        public void Close()
        {
            throw new NotImplementedException();
        }

        public int Depth
        {
            get { throw new NotImplementedException(); }
        }

        public DataTable GetSchemaTable()
        {
            throw new NotImplementedException();
        }

        public bool IsClosed
        {
            get { throw new NotImplementedException(); }
        }

        public bool NextResult()
        {
            throw new NotImplementedException();
        }

        public int RecordsAffected
        {
            get { throw new NotImplementedException(); }
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public int FieldCount
        {
            get { throw new NotImplementedException(); }
        }

        public bool GetBoolean(int i)
        {
            throw new NotImplementedException();
        }

        public byte GetByte(int i)
        {
            throw new NotImplementedException();
        }

        public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public char GetChar(int i)
        {
            throw new NotImplementedException();
        }

        public long GetChars(int i, long fieldoffset, char[] buffer, int bufferoffset, int length)
        {
            throw new NotImplementedException();
        }

        public IDataReader GetData(int i)
        {
            throw new NotImplementedException();
        }

        public string GetDataTypeName(int i)
        {
            throw new NotImplementedException();
        }

        public DateTime GetDateTime(int i)
        {
            throw new NotImplementedException();
        }

        public decimal GetDecimal(int i)
        {
            throw new NotImplementedException();
        }

        public double GetDouble(int i)
        {
            throw new NotImplementedException();
        }

        public Type GetFieldType(int i)
        {
            throw new NotImplementedException();
        }

        public float GetFloat(int i)
        {
            throw new NotImplementedException();
        }

        public Guid GetGuid(int i)
        {
            throw new NotImplementedException();
        }

        public short GetInt16(int i)
        {
            throw new NotImplementedException();
        }

        public int GetInt32(int i)
        {
            throw new NotImplementedException();
        }

        public long GetInt64(int i)
        {
            throw new NotImplementedException();
        }

        public string GetName(int i)
        {
            throw new NotImplementedException();
        }

        public int GetOrdinal(string name)
        {
            throw new NotImplementedException();
        }

        public string GetString(int i)
        {
            throw new NotImplementedException();
        }

        public int GetValues(object[] values)
        {
            throw new NotImplementedException();
        }

        public bool IsDBNull(int i)
        {
            throw new NotImplementedException();
        }

        public object this[string name]
        {
            get { throw new NotImplementedException(); }
        }

        public object this[int i]
        {
            get { throw new NotImplementedException(); }
        }
    }


    public interface ISecretFactory<T>
    {
        T Create(string param);
    }

    public abstract class Base<T> where T : Base<T>, ISecretFactory<T>
    {
        public T GetChild()
        {
            var factory = this as ISecretFactory<T>;
            return factory.Create("base param");
        }
    }

    public class Derivative : Base<Derivative>, ISecretFactory<Derivative>
    {
        public Derivative()
        {

        }

        private Derivative(string param)
        {

        }

        Derivative ISecretFactory<Derivative>.Create(string param)
        {
            return new Derivative(param);
        }
    }

    public class SecondDerivative : Derivative
    {
        public void F()
        {
            
        }
    }
}