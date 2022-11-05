using Microsoft.Win32;
using SAPbobsCOM;
using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using SzarFramework.Models;
using SzarFramework.Querys;

namespace SzarFramework.SAP
{
    internal class DatabaseFields
    {
        internal static void Update()
        {
            try
            {
                OUTBModel userTables = new OUTBModel();
                CUFDModel userFields = new CUFDModel();

                List<OUTBModel> userTablesList = userTables.Tables();
                int qtyFields = B1AppDomain.DictionaryTablesFields.Sum(x => x.Value.Fields.Count());

                ProgressBar pbFields = B1AppDomain.Application.StatusBar.CreateProgressBar("Aguarde... Atualizando Campos de usuários", qtyFields, false);

                foreach (KeyValuePair<object, TableModel> table in B1AppDomain.DictionaryTablesFields)
                {
                    List<CUFDModel> userFieldList = table.Value.TableType == TableType.System ?
                        userFields.Fields() :
                        userFields.Fields("@" + table.Value.Name);

                    foreach (FieldModel field in table.Value.Fields)
                    {
                        pbFields.Value++;
                        pbFields.Text = field.TableName + " - " + field.Name;

                        if (userFieldList.Count(p => p.AliasID.ToUpper() == field.Name.ToUpper() &&
                                               p.TableID.ToUpper() == (table.Value.TableType == TableType.System ?
                                                                            field.TableName.ToUpper() : "@" + field.TableName.ToUpper())) <= 0)
                        {
                            AddField(field, table.Value.TableType);
                        }
                        else
                        {
                            if (VerifyField(field, userFieldList.Where(p => p.AliasID.ToUpper() == field.Name.ToUpper() && 
                                                                       p.TableID.ToUpper() == (table.Value.TableType == TableType.System ? 
                                                                                                     field.TableName.ToUpper() : "@" + field.TableName.ToUpper())).SingleOrDefault()))
                            {
                                UpdateField(field, table.Value.TableType);
                            }
                        }
                    }
                }

                pbFields.Stop();
                pbFields.ClearMemory();

            }
            catch (Exception ex)
            {
                B1Exception.writeLog(ex.Message);
            }
        }

        private static bool VerifyField(FieldModel field, CUFDModel cUFD)
        {
            bool mand = cUFD.NotNull == "Y";
            if (mand != (field.Mandatory == BoYesNoEnum.tYES) && field.Type != BoFieldTypes.db_Date && field.Type != BoFieldTypes.db_Memo)
            {
                return true;
            }
            if (field.Description != cUFD.Descr)
            {
                return true;
            }
            if (field.Name != cUFD.AliasID)
            {
                return true;
            }
            if (field.SubType != RelationalReader.GetSubType(cUFD.EditType))
            {
                return true;
            }
            if (field.TableReference != null)
            {
                if (field.TableReference != cUFD.RTable)
                {
                    return true;
                }
            }

            if (field.Size != cUFD.EditSize && field.Type == BoFieldTypes.db_Alpha && field.Type == BoFieldTypes.db_Numeric)
            {
                return true;
            }
            if (field.Size != cUFD.EditSize && field.Size > cUFD.EditSize && (field.Type == BoFieldTypes.db_Alpha || field.Type == BoFieldTypes.db_Numeric))
            {
                return true;
            }

            if (field.DefaultValue != null)
            {
                if (field.DefaultValue != cUFD.Dflt)
                {
                    return true;
                }
            }

            if (field.Type != RelationalReader.GetFieldType(cUFD.TypeID))
            {
                return true;
            }
            if (field.UdoReference != null)
            {
                if (field.UdoReference != cUFD.RelUDO)
                {
                    return true;
                }
            }

            if (field.ValidValues != null)
            {
                UFD1Model ufd1 = new UFD1Model();
                foreach (UFD1Model item in ufd1.FieldItems(field.TableName, field.Name))
                {
                    if (field.ValidValues.Where(p => p.Value == item.FldValue && p.Description == item.Descr).Count() <= 0)
                    {
                        return true;
                    }
                }
                
            }

            return false;
        }

        private static void UpdateField(FieldModel field, TableType tableType)
        {
            int intRetCode = -1;

            SAPbobsCOM.UserFieldsMD objUserFieldsMD = null;

            //instancia objeto para alterar campo
            objUserFieldsMD = (UserFieldsMD)B1AppDomain.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);


            if (objUserFieldsMD.GetByKey("@" + field.TableName, idField(field.Name, field.TableName)))
            {

                //seta propriedades
                objUserFieldsMD.EditSize = field.Size;
                objUserFieldsMD.Mandatory = field.Mandatory;
                objUserFieldsMD.Description = field.Description;
                objUserFieldsMD.DefaultValue = field.DefaultValue;
                objUserFieldsMD.AddValidValues(field.ValidValues);


                //Atualiza Campos campo
                intRetCode = objUserFieldsMD.Update();
                //verifica e retorna erro
                if (intRetCode != 0 && intRetCode != -2035)
                {
                    B1Exception.throwException(intRetCode);
                }
            }

            //mata objeto para reutilizar senao trava
            objUserFieldsMD.ClearMemory();
            
        }

        private static int idField(string name, string tableName)
        {
            Recordset _rset = B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            string query = string.Format(sql.getFieldID, name, "@" + tableName);
            _rset.DoQuery(query);

            string ret = _rset.Fields.Item(0).Value.ToString();
            _rset.ClearMemory();

            return Convert.ToInt32(ret);
        }

        private static void AddField(FieldModel field, TableType tableType)
        {
            int intRetCode = -1;
            SAPbobsCOM.UserFieldsMD objUserFieldsMD = null;

            //instancia objeto para criar campo
            objUserFieldsMD = (UserFieldsMD)B1AppDomain.Company.GetBusinessObject(SAPbobsCOM.BoObjectTypes.oUserFields);

            //seta propriedades
            objUserFieldsMD.Name = field.Name;
            objUserFieldsMD.TableName = tableType == TableType.User ? "@" + field.TableName : field.TableName;
            objUserFieldsMD.Type = field.Type;
            objUserFieldsMD.SubType = field.SubType;
            objUserFieldsMD.EditSize = field.Size;
            objUserFieldsMD.Mandatory = field.Mandatory;
            objUserFieldsMD.Description = field.Description;
            objUserFieldsMD.AddValidValues(field.ValidValues);
            if (!string.IsNullOrEmpty(field.UdoReference))
            {
                objUserFieldsMD.LinkedUDO = field.UdoReference;
            }

            if (!string.IsNullOrEmpty(field.TableReference))
            {
                objUserFieldsMD.LinkedTable = field.TableReference;
            }


            objUserFieldsMD.DefaultValue = field.DefaultValue;
            //adiciona campo
            intRetCode = objUserFieldsMD.Add();
            //verifica e retorna erro
            if (intRetCode != 0 && intRetCode != -2035)
            {
                B1Exception.throwException(intRetCode);
            }

            //mata objeto para reutilizar senao trava
            objUserFieldsMD.ClearMemory();
            
        }
    }
}
