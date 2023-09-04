using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzarFramework.Models
{
    public class DataBindModel
    {
        public string UDSName { get; set; }
        public BoDataType DataType { get; set; }
        public int Size { get; set; }
        public string UDTName { get; set; }
        public string ItemName { get; set; }
        public object StartValue { get; set; }
        public Type UIType { get; set; }
        public string Query { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        
    }
}
