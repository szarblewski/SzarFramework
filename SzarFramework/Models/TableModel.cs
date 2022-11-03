using System.Collections.Generic;


namespace SzarFramework.Models
{
    internal class TableModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public SAPbobsCOM.BoUTBTableType TableTypeSAP { get; set; }
        public TableType TableType { get; set; }
        public UdoModel Udos { get; set; }
        public List<TableModel> TableChilds { get; set; }
        public List<FieldModel> Fields { set; get; }
        public string XmlForm { get; set; }

    }
}
