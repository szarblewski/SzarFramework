using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SzarFramework.Models;
using SzarFramework.Querys;

namespace SzarFramework.SAP.DB
{
    internal class DatabaseUdo
    {
        internal static void Update()
        {
			try
			{
                ProgressBar pbUdos = B1AppDomain.Application.StatusBar.CreateProgressBar("Aguarde... Registrando Udos", B1AppDomain.DictionaryUdos.Count, false);
                foreach (KeyValuePair<object, UdoModel> udo in B1AppDomain.DictionaryUdos)
                {
                    pbUdos.Value++;
                    pbUdos.Text = "Registrando Udo " + udo.Value.Code;
                    TableModel table = B1AppDomain.DictionaryTablesFields.Where(x => x.Value.Name == udo.Value.TableName).FirstOrDefault().Value;

                    table.Udos = udo.Value;
                    foreach (KeyValuePair<object, UdoChildsModel> child in B1AppDomain.DictionaryUdosChilds.Where(p => p.Value.TableFather == udo.Value.TableName))
                    {
                        if (table.Udos.Children == null)
                        {
                            table.Udos.Children = new List<UdoChildsModel>();
                        }
                        table.Udos.Children.Add(child.Value);
                    }

                    Recordset oRsReg = B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                    oRsReg.DoQuery(String.Format(sql.udoList, table.Udos.Code));
                    oRsReg.MoveFirst();
                    string currentScreen = oRsReg.Fields.Item(1).Value.ToString();
                    oRsReg.ClearMemory();
                    string newScreen = !string.IsNullOrEmpty(table.Udos.Form) ?
                    SAP.UI.Forms.LayoutsUDO.Layouts.Where(x => x.Key == tabela.Udos.Formulario).Select(x => x.Value).SingleOrDefault() :
                    "";
                    int comp = string.Compare(currentScreen, newScreen.Replace("\r\n", "\r"));
                    if (!string.IsNullOrEmpty(currentScreen) && comp != 0)
                    {
                        UpdateUdo(tabela);
                    }
                    else
                    {
                        AddUdo(tabela);
                    }

                }

                pbUdos.Stop();
                pbUdos.ClearMemory();


            }
			catch (Exception ex)
			{
				B1Exception.writeLog(ex.Message);
			}


        }
    }
}
