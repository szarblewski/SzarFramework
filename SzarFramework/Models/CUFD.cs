using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzarFramework.Querys;

namespace SzarFramework.Models
{
    internal class CUFD
    {
        public string TableID { get; set; }
        public int FieldID { get; set; }
        public string AliasID { get; set; }
        public string Descr { get; set; }
        public string TypeID { get; set; }
        public string EditType { get; set; }
        public int SizeID { get; set; }
        public int EditSize { get; set; }
        public string Dflt { get; set; }
        public string NotNull { get; set; }
        public string IndexID { get; set; }
        public string RTable { get; set; }
        public int RField { get; set; }
        public string Action { get; set; }
        public string Sys { get; set; }
        public DateTime DfltDate { get; set; }
        public string RelUDO { get; set; }


        internal List<CUFD> Fields(string tableName = "")
        {
            try
            {
                List<CUFD> flds = new List<CUFD>();
                Recordset oRs = B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                if (string.IsNullOrEmpty(tableName))
                {
                    flds = oRs.DoQuery(sql.cufdList, this);
                }
                else
                {
                    flds = oRs.DoQuery(sql.cufdList + " where tableid = '" + tableName + "'", this);
                }
                    

                oRs.ClearMemory();
                return flds;

            }
            catch (Exception ex)
            {
                B1Exception.writeLog(ex.Message);
                return null;
            }
        }

    }
}
