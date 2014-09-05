using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using TagKid.Lib.PetaPoco;

namespace TagKid.Tests.Core.Mock
{
    class MockTable<T>
    {
        private int _nextId = 1;
        private readonly PropertyInfo _primaryKeyProp;
        private readonly List<T> _rows;

        public MockTable()
        {
            _rows = new List<T>();

            var tableInfo = TableInfo.FromPoco(typeof (T));
            if (!String.IsNullOrWhiteSpace(tableInfo.PrimaryKey) && tableInfo.AutoIncrement)
            {
                _primaryKeyProp = typeof (T).GetProperties().First(p => p.GetCustomAttribute<PrimaryKeyAttribute>() != null);
            }
        }

        public IEnumerable<T> Rows
        {
            get { return _rows.AsReadOnly(); }
        }

        public void Insert(T row)
        {
            _rows.Add(row);
            if (_primaryKeyProp != null)
                _primaryKeyProp.SetValue(row, _nextId++);
        }
    }
}
