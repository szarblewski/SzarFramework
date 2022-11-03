using SAPbobsCOM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzarFramework.Querys;


namespace SzarFramework.Models
{
    internal class OUTB
    {
        public string TableName { get; set; }
        public string Descr { get; set; }
        public int TblNum { get; set; }
        public string ObjectType { get; set; }
        public string UsedInObj { get; set; }
        public string LogTable { get; set; }
        public string Archivable { get; set; }
        public string ArchivDate { get; set; }


        internal List<OUTB> Tables()
        {
            try
            {
                List<OUTB> tbs = new List<OUTB>();

                Recordset oRs = B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                tbs = oRs.DoQuery(sql.outbList, this);
                oRs.ClearMemory();
                return tbs;

            }
            catch (Exception ex)
            {
                B1Exception.writeLog(ex.Message);
                return null;
            }
        }
        


    }
}
