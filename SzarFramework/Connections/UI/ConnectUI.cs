using SAPbouiCOM;
using System;
using System.Linq;
using System.Reflection;


namespace SzarFramework.Connections
{
    public class ConnectUI
    {
                
        public void Connect(Type[] types)
        {
            SAPbouiCOM.SboGuiApi objGUIApi = null;
            SAPbouiCOM.Application objApplication = null;
            SAPbobsCOM.Company objCompany = null;
            B1AppDomain.ConnectionString = "0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056";
            //"0030002C0030002C00530041005000420044005F00440061007400650076002C0050004C006F006D0056004900490056";
            
            try
            {
                objGUIApi = new SAPbouiCOM.SboGuiApi();
                objGUIApi.Connect(B1AppDomain.ConnectionString);
                objApplication = objGUIApi.GetApplication(-1);
                objCompany = (SAPbobsCOM.Company)objApplication.Company.GetDICompany();
                                
                if (objCompany.Connected)
                {
                    B1AppDomain.Application = objApplication;
                    B1AppDomain.Company = objCompany;
                    B1AppDomain.Connected = true;
                    B1AppDomain.Application.SetStatusBarMessage("Conexão estabelecida com sucesso!", BoMessageTime.bmt_Short, false);
                    new Events();
                    B1AppDomain.Application.SetStatusBarMessage("Carregando Aplicação...", BoMessageTime.bmt_Short, false);
                    CreateInstanceClass(types);
                    B1AppDomain.Application.SetStatusBarMessage("Carregamento Concluido!", BoMessageTime.bmt_Short, false);

                }
                else
                {
                    B1AppDomain.Connected = false;
                }

            }
            catch (Exception er)
            {
                B1AppDomain.Connected = false;
                B1Exception.throwException("Erro ao conectar no SAP B1: ", er);

            }


        }

        private static void CreateInstanceClass(Type[] nameSpace)
        {
            Assembly asm = Assembly.GetExecutingAssembly();

            foreach (Type type in nameSpace)
            {
                if (type.Name != "Program" && type.CustomAttributes.Count() > 0)
                {

                    try
                    {
                        Activator.CreateInstance(type);
                    }
                    catch { }
                    
                }

            }

        }

    }
}
