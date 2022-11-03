using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzarFramework.Models;

namespace SzarFramework.SAP
{
    public sealed class Database
    {

        public static void Update()
        {
            B1AppDomain.Application.SetStatusBarMessage("Iniciando verificação de estrutura de dados...", BoMessageTime.bmt_Short, false);

            DatabaseTables.Update();

            B1AppDomain.Application.SetStatusBarMessage("Verificação concluida", BoMessageTime.bmt_Short, false);
        }


    }
}
