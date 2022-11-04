using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SzarFramework.Models;

namespace SzarFramework.SAP
{
    internal class DatabaseTables
    {
        internal static void Update()
        {
			try
			{
				OUTB userTables = new OUTB();
				List<OUTB> userTablesList = userTables.Tables();
				ProgressBar pbTables = B1AppDomain.Application.StatusBar.CreateProgressBar("Aguarde... Atualizando Tabelas", B1AppDomain.DictionaryTablesFields.Count, false);
				foreach (KeyValuePair<object, TableModel> table in B1AppDomain.DictionaryTablesFields.Where(x => x.Value.TableType == TableType.User))
				{
					pbTables.Value++;
					pbTables.Text = table.Value.Name;
                    if (userTablesList.Where(x => x.TableName == table.Value.Name).Count() <= 0)
					{
						AddTable(table.Value.Name, table.Value.Description, table.Value.TableTypeSAP);
                    }
                    else
                    {
                        OUTB tb = userTablesList.Where(x => x.TableName == table.Value.Name).SingleOrDefault();

                        if(tb.Descr != table.Value.Description)
                        {
                            UpdateTable(table.Value.Name, table.Value.Description);
                        }

                    }
				}
				pbTables.Stop();
				pbTables.ClearMemory();
				

			}
			catch (Exception ex)
			{
				B1Exception.writeLog(ex.Message);
			}
        }

		private static void AddTable(string name, string description, BoUTBTableType tableTypeSAP)
		{
            int intRetCode = -1;
            SAPbobsCOM.UserTablesMD objUserTablesMD = null;

            try
            {
                //instancia objeto para criar tabela
                objUserTablesMD = (UserTablesMD)B1AppDomain.Company.GetBusinessObject(BoObjectTypes.oUserTables);
                
                //seta propriedades
                objUserTablesMD.TableName = name;
                objUserTablesMD.TableDescription = description;
                objUserTablesMD.TableType = tableTypeSAP;

                //adiciona tabela
                intRetCode = objUserTablesMD.Add();
                //verifica e retorna erro
                if (intRetCode != 0 && intRetCode != -2035)
                {
                    B1Exception.throwException(intRetCode);
                }

                //mata objeto para reutilizar senao trava
                objUserTablesMD.ClearMemory();


            }
            catch (Exception ex)
            {
                B1Exception.throwException("Erro ao Criar Tabela :: " + name + " - ", ex);

            }
        }

        private static void UpdateTable(string name, string description)
        {
            int intRetCode = -1;
            SAPbobsCOM.UserTablesMD objUserTablesMD = null;

            try
            {
                //instancia objeto para atualizar tabela
                objUserTablesMD = (UserTablesMD)B1AppDomain.Company.GetBusinessObject(BoObjectTypes.oUserTables);
                if (objUserTablesMD.GetByKey(name))
                {

                    objUserTablesMD.TableDescription = description;

                    //atualiza tabela
                    intRetCode = objUserTablesMD.Update();
                    //verifica e retorna erro
                    if (intRetCode != 0 && intRetCode != -2035)
                    {
                        B1Exception.throwException("MetaData.UpdateTable: ", new Exception(B1AppDomain.Company.GetLastErrorDescription()));
                    }

                }
                else
                {
                    B1AppDomain.Application.SetStatusBarMessage("Tabela não localizada no sistema :: " + name, BoMessageTime.bmt_Short, true);
                }
                objUserTablesMD.ClearMemory();

            }
            catch (Exception ex)
            {

                B1Exception.throwException("Erro ao Atualizar Tabela :: " + name + " - ", ex);
            }
        }
	}
}
