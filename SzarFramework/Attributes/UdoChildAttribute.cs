using System;

namespace SzarFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class UdoChildAttribute : Attribute
    {
        public string TableName { get; set; }
        public string TabelaFather { get; set; }
        
        public UdoChildAttribute(string tableName, string tabelaFather)
        {
            this.TableName = tableName;
            this.TabelaFather = tabelaFather;
        }

    }
}
