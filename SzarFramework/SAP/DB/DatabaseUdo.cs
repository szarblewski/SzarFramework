using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SzarFramework.Models;
using SzarFramework.Querys;

namespace SzarFramework.SAP
{
    internal class DatabaseUdo
    {
        private static bool controlUdo { get; set; }
        internal static void Update()
        {
			try
			{
                controlUdo = false;
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
                                                B1AppDomain.DictionaryUdosForms.Where(x => x.Key == table.Udos.Form).Select(x => x.Value).SingleOrDefault() :
                                                "";
                    if (string.IsNullOrEmpty(newScreen))
                        newScreen = "";
                    
                    int comp = string.Compare(currentScreen, newScreen.Replace("\r\n", "\r"));
                    if (!string.IsNullOrEmpty(currentScreen) && comp != 0)
                    {
                        UpdateUdo(table);
                    }
                    else
                    {
                        AddUdo(table);
                    }

                }

                pbUdos.Stop();
                pbUdos.ClearMemory();

                B1AppDomain.Application.SetStatusBarMessage("Verificação de UDO concluida!", BoMessageTime.bmt_Short, false);

                if (controlUdo)
                {
                    B1AppDomain.Application.MessageBox("Dados foram alterados o sistema será reiniciado");
                    B1AppDomain.Company.Disconnect();
                    B1AppDomain.Application.Menus.Item("3329").Activate();
                    B1AppDomain.Application.Forms.ActiveForm.Items.Item("3").Click();
                }

            }
			catch (Exception ex)
			{
				B1Exception.writeLog(ex.Message);
			}


        }

        private static void AddUdo(TableModel table)
        {
            int intRetCode = -1;
            SAPbobsCOM.UserObjectsMD oUserObjectMD = null;

            oUserObjectMD = ((SAPbobsCOM.UserObjectsMD)(B1AppDomain.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD)));
            oUserObjectMD.CanCancel = table.Udos.Cancel;
            oUserObjectMD.CanClose = table.Udos.Close;
            oUserObjectMD.CanCreateDefaultForm = table.Udos.CreateDefaultForm;
            oUserObjectMD.EnableEnhancedForm = table.Udos.EnableEnhancedform;
            if (table.Udos.CreateDefaultForm == SAPbobsCOM.BoYesNoEnum.tYES)
            {
                if (table.Udos.ObjectType != BoUDOObjType.boud_Document)
                {
                    oUserObjectMD.FormColumns.FormColumnAlias = "Code";
                    oUserObjectMD.FormColumns.FormColumnDescription = "Code";
                }
                else
                {
                    oUserObjectMD.FormColumns.FormColumnAlias = "DocEntry";
                    oUserObjectMD.FormColumns.FormColumnDescription = "DocEntry";
                }

                int voltaCampos = 1;
                foreach (FieldModel field in table.Fields)
                {
                    oUserObjectMD.FormColumns.Add();
                    oUserObjectMD.FormColumns.SetCurrentLine(voltaCampos);
                    oUserObjectMD.FormColumns.FormColumnAlias = "U_" + field.Name;
                    oUserObjectMD.FormColumns.FormColumnDescription = field.Description;
                    oUserObjectMD.FormColumns.Editable = BoYesNoEnum.tYES;
                    oUserObjectMD.FormColumns.SonNumber = 0;
                    voltaCampos++;
                }

            }

            oUserObjectMD.CanDelete = table.Udos.Delete;
            oUserObjectMD.CanFind = table.Udos.Find;
            if (table.Udos.Find == SAPbobsCOM.BoYesNoEnum.tYES)
            {
                if (table.Udos.ObjectType != BoUDOObjType.boud_Document)
                {
                    oUserObjectMD.FindColumns.ColumnAlias = "Code";
                    oUserObjectMD.FindColumns.ColumnDescription = "Code";
                    oUserObjectMD.FindColumns.Add();
                    oUserObjectMD.FindColumns.ColumnAlias = "Name";
                    oUserObjectMD.FindColumns.ColumnDescription = "Name";
                }
                else
                {
                    oUserObjectMD.FindColumns.ColumnAlias = "DocEntry";
                    oUserObjectMD.FindColumns.ColumnDescription = "DocEntry";
                    oUserObjectMD.FindColumns.ColumnAlias = "DocNum";
                    oUserObjectMD.FindColumns.ColumnDescription = "DocNum";
                }

                int voltaFind = 1;
                foreach (FieldModel field in table.Fields)
                {
                    oUserObjectMD.FindColumns.Add();
                    oUserObjectMD.FindColumns.SetCurrentLine(voltaFind);
                    oUserObjectMD.FindColumns.ColumnAlias = "U_" + field.Name;
                    oUserObjectMD.FindColumns.ColumnDescription = field.Description;
                    voltaFind++;
                }
            }


            oUserObjectMD.CanYearTransfer = table.Udos.YearTransfer;
            oUserObjectMD.Code = table.Udos.Code;
            oUserObjectMD.ManageSeries = table.Udos.ManageSeries;
            oUserObjectMD.Name = table.Udos.Name;
            oUserObjectMD.ObjectType = table.Udos.ObjectType;
            oUserObjectMD.TableName = table.Udos.TableName;
            if (table.Udos.Log)
            {
                oUserObjectMD.CanLog = BoYesNoEnum.tYES;
                oUserObjectMD.LogTableName = "L" + table.Udos.TableName;
            }


            int numerofilho = 1;

            if (table.Udos.Children != null)
            {
                foreach (UdoChildsModel listUdo in table.Udos.Children)
                {
                    if (numerofilho > 1)
                    {
                        oUserObjectMD.ChildTables.Add();
                    }

                    oUserObjectMD.ChildTables.TableName = listUdo.TableName;
                    oUserObjectMD.ChildTables.ObjectName = listUdo.TableName;

                    if (table.Udos.EnableEnhancedform == BoYesNoEnum.tNO)
                    {
                        foreach (TableModel tableChild in B1AppDomain.DictionaryTablesFields.Where(p => p.Value.Name == listUdo.TableName).Select(p => p.Value))
                        {
                            int voltaFilhos = 1;
                            foreach (FieldModel field in tableChild.Fields)
                            {
                                oUserObjectMD.FormColumns.Add();
                                oUserObjectMD.FormColumns.SetCurrentLine(voltaFilhos);
                                oUserObjectMD.FormColumns.FormColumnAlias = "U_" + field.Name;
                                oUserObjectMD.FormColumns.FormColumnDescription = field.Description;
                                oUserObjectMD.FormColumns.Editable = BoYesNoEnum.tYES;
                                oUserObjectMD.FormColumns.SonNumber = numerofilho;
                                voltaFilhos++;
                            }

                        }

                    }
                    else
                    {
                        foreach (TableModel tableChild in B1AppDomain.DictionaryTablesFields.Where(p => p.Value.Name == listUdo.TableName).Select(p => p.Value))
                        {
                            int voltaNovo = 0;
                            foreach (FieldModel field in tableChild.Fields)
                            {

                                if (voltaNovo > 0)
                                {
                                    oUserObjectMD.EnhancedFormColumns.Add();
                                }

                                oUserObjectMD.EnhancedFormColumns.SetCurrentLine(voltaNovo);
                                oUserObjectMD.EnhancedFormColumns.ColumnAlias = "U_" + field.Name;
                                oUserObjectMD.EnhancedFormColumns.ColumnDescription = field.Description;
                                oUserObjectMD.EnhancedFormColumns.Editable = BoYesNoEnum.tYES;
                                oUserObjectMD.EnhancedFormColumns.ColumnIsUsed = BoYesNoEnum.tYES;
                                oUserObjectMD.EnhancedFormColumns.ChildNumber = numerofilho;
                                voltaNovo++;

                            }
                        }
                    }

                    numerofilho++;

                }
            }
            
            oUserObjectMD.RebuildEnhancedForm = table.Udos.RebuildEnhancedForm;

            //oUserObjectMD.FormSRF = table.Udos.Form;

            oUserObjectMD.FormSRF =
                !string.IsNullOrEmpty(table.Udos.Form) ?
                B1AppDomain.DictionaryUdosForms.Where(x => x.Key == table.Udos.Form).Select(x => x.Value).SingleOrDefault() :
                "";

            intRetCode = oUserObjectMD.Add();

            //mata objeto para reutilizar senao trava
            oUserObjectMD.ClearMemory();

            //verifica e retorna erro
            if (intRetCode != 0 && intRetCode != -2035)
            {
                B1Exception.throwException(intRetCode);
            }
            else
            {
                controlUdo = true;
            }
            
        }

        private static void UpdateUdo(TableModel table)
        {
            int intRetCode = -1;
            SAPbobsCOM.UserObjectsMD oUserObjectMD = null;

            oUserObjectMD = ((SAPbobsCOM.UserObjectsMD)(B1AppDomain.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserObjectsMD)));
            oUserObjectMD.GetByKey(table.Udos.Code);
            oUserObjectMD.CanCancel = table.Udos.Cancel;
            oUserObjectMD.CanClose = table.Udos.Close;
            oUserObjectMD.CanCreateDefaultForm = table.Udos.CreateDefaultForm;
            oUserObjectMD.EnableEnhancedForm = table.Udos.EnableEnhancedform;
            oUserObjectMD.CanDelete = table.Udos.Delete;
            oUserObjectMD.CanFind = table.Udos.Find;
            oUserObjectMD.CanYearTransfer = table.Udos.YearTransfer;
            oUserObjectMD.Code = table.Udos.Code;
            oUserObjectMD.ManageSeries = table.Udos.ManageSeries;
            oUserObjectMD.Name = table.Udos.Name;
            oUserObjectMD.ObjectType = table.Udos.ObjectType;
            oUserObjectMD.TableName = table.Udos.TableName;
            oUserObjectMD.RebuildEnhancedForm = table.Udos.RebuildEnhancedForm;
                        
            oUserObjectMD.FormSRF =
                !string.IsNullOrEmpty(table.Udos.Form) ?
                B1AppDomain.DictionaryUdosForms.Where(x => x.Key == table.Udos.Form).Select(x => x.Value).SingleOrDefault() :
                "";

            intRetCode = oUserObjectMD.Update();

            //mata objeto para reutilizar senao trava
            oUserObjectMD.ClearMemory();
            

            //verifica e retorna erro
            if (intRetCode != 0 && intRetCode != -2035)
            {
                string teste = B1AppDomain.Company.GetLastErrorDescription();
                B1AppDomain.Application.SetStatusBarMessage(teste, BoMessageTime.bmt_Short, true);
                B1Exception.throwException(intRetCode);

            }
            else
            {
                controlUdo = true;
            }
            
        }
    }
}
