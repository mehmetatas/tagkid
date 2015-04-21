using System.Reflection;

namespace TagKid.Framework.Repository.Mapping
{
    public class ColumnMapping
    {
        public PropertyInfo PropertyInfo { get; set; }
        public string ColumnName { get; set; }
        public bool IsId { get; set; }
        public bool IsAutoIncrement { get; set; }
    }
}