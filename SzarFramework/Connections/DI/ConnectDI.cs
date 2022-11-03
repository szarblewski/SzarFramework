using SAPbobsCOM;
using System;

namespace SzarFramework.Connections
{
    public class ConnectDI
    {
        private Company objCompany = null;
        public string Server { get; set; }
        public string CompanyDB { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string LicenseServer { get; set; }
        public string DbUserName { get; set; }
        public string DbPassword { get; set; }
        public BoSuppLangs Language { get; set; }
        public BoDataServerTypes DbServerType { get; set; }
        
        public bool Connect()
        {
            try
            {
                if (objCompany == null)
                    objCompany = new SAPbobsCOM.Company();
                
                objCompany.Server = Server;
                objCompany.LicenseServer = LicenseServer;
                objCompany.CompanyDB = CompanyDB;
                objCompany.UserName = UserName;
                objCompany.Password = Password;
                objCompany.DbUserName = DbUserName;
                objCompany.DbPassword = DbPassword;
                objCompany.DbServerType = DbServerType;
                objCompany.language = Language;
                int codCon = objCompany.Connect();
                if (codCon != 0)
                {
                    B1Exception.writeLog("Erro conexão código: " + 
                        codCon.ToString() + 
                        " ::: Descrição: " + 
                        objCompany.GetLastErrorDescription());
                    

                    return false;
                }
                else
                {
                    B1Exception.writeLog("Conexão estabelecida com sucesso!");
                    B1AppDomain.Company = objCompany;
                    B1AppDomain.Connected = true;
                    return true;
                }
            }
            catch (Exception ex)
            {
                B1Exception.writeLog(ex.Message);
                return false;
            }


            
            
        }
    }
    
}
