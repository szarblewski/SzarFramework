using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using SzarFramework.Connections;
using SzarFramework;
using SAPbobsCOM;

namespace TesteFramework
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //exemplo de conexao com ui
            ConnectUI con = new ConnectUI();
            con.Connect("TesteFramework".GetTypesFromAssembly());

            SzarFramework.SAP.Database.Update();

            tabelaConsulta tb = new tabelaConsulta();

            //exemplo de conexao com DI
            //ConnectDI con = new ConnectDI();
            //con.Server = "192.168.1.220\\SRVSAP";
            //con.LicenseServer = "SRVSAP:30000";
            //con.CompanyDB = "SBOHybel";
            //con.UserName = "manager";
            //con.Password = "1234";
            //con.DbUserName = "sa";
            //con.DbPassword = "123456";
            //con.DbServerType = BoDataServerTypes.dst_MSSQL2014;
            //con.Language = BoSuppLangs.ln_Portuguese_Br;
            //bool teste = con.Connect();




            Application.Run();
        }
    }
}
