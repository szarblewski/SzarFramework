using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzarFramework.Models
{
    public class GridFormaterModel
    {
        public string ItemName { get; set; }
        public string ColumnName { get; set; }
        public string Title { get; set; }
        public BoGridColumnType ColumnType { get; set; }
        public string LinkedObjectType { get; set; }
        public bool Editable { get; set; }
        public bool Visible { get; set; }
        public bool Sortable { get; set; }
        
    }
}
