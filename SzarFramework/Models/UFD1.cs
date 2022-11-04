using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzarFramework.Querys;

namespace SzarFramework.Models
{
    internal class UFD1
    {
        public string TableID { get; set; }
        public int FieldID { get; set; }
        public int IndexID { get; set; }
        public string FldValue { get; set; }
        public string Descr { get; set; }
        public DateTime FldDate { get; set; }


        internal List<UFD1> FieldItems(string tableName, string name)
        {
            try
            {
                List<UFD1> fieldItems = new List<UFD1>();

                Recordset oRs = B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                fieldItems = oRs.DoQuery(String.Format(sql.ufd1List, tableName, name), this);
                oRs.ClearMemory();
                return fieldItems;

            }
            catch (Exception ex)
            {
                B1Exception.writeLog(ex.Message);
                return null;
            }
        }
        
    }
}
