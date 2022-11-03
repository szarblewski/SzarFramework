using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SzarFramework
{
    public static class RecordsetExtensions
    {

        public static List<TEntity> DoQuery<TEntity>(this Recordset oRs, string query, TEntity table)
        {
            try
            {
                List<TEntity> list = new List<TEntity>();
                Recordset rs = B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                rs.DoQuery(query);
                while (!rs.EoF)
                {
                    TEntity t = table;
                    foreach (PropertyInfo info in t.GetType().GetProperties())
                    {
                        try{
                            info.SetValue(t, rs.Fields.Item(info.Name).Value);
                        }catch{}
                        
                    }
                    list.Add(t);

                    rs.MoveNext();
                }
                rs.ClearMemory();
                return list;
            }
            catch (Exception ex)
            {
                B1Exception.writeLog(ex.Message);
                return null;
            }
        }


    }
}
