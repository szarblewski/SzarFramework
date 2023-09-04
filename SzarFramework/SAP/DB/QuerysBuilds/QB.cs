using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzarFramework.SAP
{
    internal class QB
    {
        internal static void RSQuery(string query)
        {
            try
            {
                
                Recordset oRs = B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);

                oRs.DoQuery(query);

                oRs.ClearMemory();
            }
            catch (Exception ex)
            {
                B1Exception.throwException("Erro ao executar query: " + query, ex);
            }
        }
    }
}
