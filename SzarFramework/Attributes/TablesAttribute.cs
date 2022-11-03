using SAPbobsCOM;
using System;

namespace SzarFramework.Attributes
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = true)]
    public sealed class TablesAttribute : Attribute
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public BoUTBTableType TypeTable { get; set; }
        public bool SystemTable { get; set; }



        public TablesAttribute(string name, string description, BoUTBTableType typeTable, bool systemTable)
        {
            this.TypeTable = typeTable;
            this.Name = name;
            this.Description = description;
            this.SystemTable = systemTable;            
        }

        public TablesAttribute(string name, string description, bool systemTable)
        {
            this.Name = name;
            this.Description = description;
            this.SystemTable = systemTable;
        }
    }
}
