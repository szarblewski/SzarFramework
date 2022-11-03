using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzarFramework;

namespace TesteFramework
{
    public class tabelaConsulta
    {
        List<tabelaConsulta> lista { get; set; }
        public string TableName { get; set; }
        public string Descr { get; set; }
        public int TblNum { get; set; }
        public string ObjectType { get; set; }
        public string UsedInObj { get; set; }
        public string LogTable { get; set; }
        public string Archivable { get; set; }
        public string ArchivDate { get; set; }

        public tabelaConsulta()
        {
            Recordset ors = B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            lista = ors.DoQuery(@"select 
                                    TableName
                                    ,Descr
                                    ,TblNum
                                    ,ObjectType
                                    ,UsedInObj
                                    ,LogTable
                                    ,Archivable
                                    ,ArchivDate 
                                    from OUTB", this);




        }
    }
}
