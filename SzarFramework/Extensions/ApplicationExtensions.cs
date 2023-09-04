using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Xml;
using SzarFramework.Models;

namespace SzarFramework
{
    public static class ApplicationExtensions
    {
        /// <summary>
        /// Abre tela no SAP com base no XML '.srf' passado como parametro
        /// </summary>
        /// <param name="app"></param>
        /// <param name="screen">Tela XML</param>
        /// <param name="screenName">ID da tela XML</param>
        /// <returns></returns>
        public static Form OpenForm(this Application app, string screen, string screenName)
        {
            
            try
            {
                FormCreationParams creationPackage = (FormCreationParams)B1AppDomain.Application.CreateObject(BoCreatableObjectType.cot_FormCreationParams);
                XmlDocument document = new XmlDocument
                {
                    InnerXml = screen
                };
                creationPackage.XmlData = document.InnerXml;
                creationPackage.UniqueID = DateTime.Now.ToBinary().ToString();
                creationPackage.FormType = screenName;
                Form frm = B1AppDomain.Application.Forms.AddEx(creationPackage);
                //if (B1AppDomain.DictionaryFormEvent.Count(x => x.Key == screenName) > 0)
                //{
                //    frm = B1AppDomain.DictionaryFormEvent.Where(x => x.Key == screenName).Select(x => x.Value).FirstOrDefault().oForm;
                //    if(frm == null)
                //        frm = B1AppDomain.Application.Forms.AddEx(creationPackage);
                //    frm.Freeze(true);
                //    List<DataBindModel> dbind = B1AppDomain.DictionaryFormEvent.Where(x => x.Key == screenName).Select(x => x.Value).FirstOrDefault().DataBind;
                //    List<GridFormaterModel> gridFormaters = B1AppDomain.DictionaryFormEvent.Where(x => x.Key == screenName).Select(x => x.Value).FirstOrDefault().GridFormater;

                //    LoadData(frm, dbind);
                //    FormaterGrid(frm, gridFormaters);
                //}
                //else
                //{
                //    frm = B1AppDomain.Application.Forms.AddEx(creationPackage);

                //}                                 


                //frm.Freeze(false);
                return frm;

            }
            catch (Exception ex)
            {
                B1Exception.throwException("Erro ao carregar formularios XML :: ", ex);
                return null;
            }




        }

        internal static void FormaterGrid(Form frm, List<GridFormaterModel> gridFormater)
        {
            try
            {
                foreach (GridFormaterModel item in gridFormater.OrderBy(x => x.ItemName))
                {
                    Grid oGrid = frm.Items.Item(item.ItemName).Specific;
                    oGrid.Columns.Item(item.ColumnName).TitleObject.Caption = item.Title;
                    oGrid.Columns.Item(item.ColumnName).Editable = item.Editable;
                    oGrid.Columns.Item(item.ColumnName).Visible = item.Visible;
                    oGrid.Columns.Item(item.ColumnName).Type = item.ColumnType;
                    oGrid.Columns.Item(item.ColumnName).TitleObject.Sortable = item.Sortable;
                    switch (item.ColumnType)
                    {
                        case BoGridColumnType.gct_EditText:
                            EditTextColumn oEditTextColumn = (EditTextColumn)oGrid.Columns.Item(item.ColumnName);
                            oEditTextColumn.LinkedObjectType = item.LinkedObjectType;
                            break;
                        case BoGridColumnType.gct_CheckBox:
                            break;
                        case BoGridColumnType.gct_ComboBox:
                            break;
                        case BoGridColumnType.gct_Picture:
                            break;
                        default:
                            break;
                    }
                    
                    oGrid.AutoResizeColumns();
                }
                
            }
            catch (Exception ex)
            {
                B1Exception.throwException("FormaterGrid: ", ex);
            }
        }

        private static void LoadData(Form frm, List<DataBindModel> dbind)
        {
            try
            {
                foreach (DataBindModel item in dbind)
                {
                    string teste = item.UIType.ToString();
                    switch (item.UIType.ToString())
                    {
                        case "SAPbouiCOM.EditText":
                            if (!string.IsNullOrEmpty(item.UDSName))
                                frm.DataSources.UserDataSources.Add(item.UDSName, item.DataType, item.Size);

                            if (!string.IsNullOrEmpty(item.UDSName) && !string.IsNullOrEmpty(item.ItemName))
                                ((EditText)frm.Items.Item(item.ItemName).Specific).DataBind.SetBound(true, "", item.UDSName);

                            if (item.StartValue != null)
                                frm.DataSources.UserDataSources.Item(item.UDSName).Value = item.StartValue.ToString();
                            
                            break;

                        case "SAPbouiCOM.Grid":
                            if (!string.IsNullOrEmpty(item.UDTName))
                                if (frm.DataSources.DataTables.Item(item.UDTName) == null)
                                    frm.DataSources.DataTables.Add(item.UDTName);
                            

                            if (!string.IsNullOrEmpty(item.UDTName) && !string.IsNullOrEmpty(item.ItemName))
                                ((Grid)frm.Items.Item(item.ItemName).Specific).DataTable = frm.DataSources.DataTables.Item(item.UDTName);

                            if (!string.IsNullOrEmpty(item.ItemName) && !string.IsNullOrEmpty(item.Query))
                                ((Grid)frm.Items.Item(item.ItemName).Specific).DataTable.ExecuteQuery(item.Query);
                            

                            break;

                        default:
                            break;
                    }

                    

                }

                
            }
            catch (Exception ex)
            {
                B1Exception.throwException("LoadData :", ex);
            }
        }
    }
}
