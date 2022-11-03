using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzarFramework;
using SzarFramework.Attributes;

namespace TesteFramework
{
    [Form("134", "parceiro de negocios")]
    internal class parceiro : FormBase
    {
                
        public override void ItemPressed_Before(string formUID, ref ItemEvent pVal, ref bool bubbleEvent)
        {
            try
            {
                bubbleEvent = true;
                B1AppDomain.Application.MessageBox("agora vai");
                return;
            }
            catch (Exception ex)
            {
                B1Exception.writeLog(ex.Message);
            }
        }

    }
}
