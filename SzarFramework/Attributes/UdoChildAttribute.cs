using System;

namespace SzarFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class UdoChildAttribute : Attribute
    {
        public string TableName { get; set; }
        public string TableFather { get; set; }
        
        public UdoChildAttribute(string tableName, string tableFather)
        {
            this.TableName = tableName;
            this.TableFather = tableFather;
        }

    }
}
