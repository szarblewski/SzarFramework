using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzarFramework.Querys;

namespace SzarFramework
{
    internal class QuerySelect
    {
        public static string Select(string nameQuery)
        {
			try
			{
				string ret = "";
				switch (B1AppDomain.Company.DbServerType)
				{
					case BoDataServerTypes.dst_HANADB:
                        ret = hana.ResourceManager.GetString(nameQuery);
                        break;
					
					default:
                        ret = sql.ResourceManager.GetString(nameQuery);
                        break;
                        
				}
                return ret;
            }
			catch (Exception ex)
			{
				B1Exception.throwException("Error Select:", ex);
                return "";
			}
        }
    }
}
