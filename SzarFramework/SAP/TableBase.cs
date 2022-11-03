using System.Collections.Generic;
using System.Reflection;
using SzarFramework.Attributes;
using SzarFramework.Models;

namespace SzarFramework
{
    public abstract class TableBase
    {

        public string Code { get; set; }
        public string Name { get; set; }
        public int DocEntry { get; set; }
        public int DocNum { get; set; }

        private readonly List<string> TableId;

        protected TableBase()
        {

            TableId = new List<string>();
            TablesAttribute attribute = null;
            FieldsAttribute flAttribute = null;
            UdoAttribute udoAttribute = null;
            UdoChildAttribute udoChildAttribute = null;
            ValidValuesAttribute validValues = null;

            foreach (object obj2 in base.GetType().GetCustomAttributes(false))
            {

                #region Atributo Tabelas

                if (obj2 is TablesAttribute)
                {
                    attribute = obj2 as TablesAttribute;

                    if (!string.IsNullOrEmpty(attribute.Name))
                    {
                        TableModel tb = new TableModel();
                        tb.Name = attribute.Name;
                        tb.Description = attribute.Description;
                        tb.TableTypeSAP = attribute.TypeTable;
                        tb.Fields = new List<FieldModel>();
                        tb.TableType = attribute.SystemTable ? TableType.System : TableType.User;

                        foreach (PropertyInfo info in this.GetType().GetProperties())
                        {
                            List<ValidValuesModel> vlrs = new List<ValidValuesModel>();
                            FieldModel cp = new FieldModel();
                            foreach (object field in info.GetCustomAttributes(true))
                            {

                                if (field is FieldsAttribute)
                                {
                                    flAttribute = field as FieldsAttribute;

                                    RelationalReader.verifyTypes(cp, info, flAttribute, tb.Name);

                                }

                                if (field is ValidValuesAttribute)
                                {
                                    validValues = field as ValidValuesAttribute;
                                    vlrs.Add(new ValidValuesModel() { Description = validValues.Description, Value = validValues.Value });
                                }


                            }

                            if (!string.IsNullOrEmpty(cp.Name))
                            {
                                if (vlrs.Count > 0)
                                {
                                    cp.ValidValues = vlrs;
                                }
                                tb.Fields.Add(cp);
                            }



                        }


                        B1AppDomain.RegisterTable(this, tb);
                    }

                }

                #endregion

                #region Atributo Udo

                if (obj2 is UdoAttribute)
                {

                    udoAttribute = obj2 as UdoAttribute;

                    if (!string.IsNullOrEmpty(udoAttribute.Code))
                    {
                        UdoModel ud = new UdoModel();
                        ud.TableName = udoAttribute.TableName;
                        ud.Name = udoAttribute.Name;
                        ud.Code = udoAttribute.Code;
                        ud.Cancel = udoAttribute.Cancel;
                        ud.Close = udoAttribute.Close;
                        ud.CreateDefaultForm = udoAttribute.CreateDefaultForm;
                        ud.Delete = udoAttribute.Delete;
                        ud.Find = udoAttribute.Find;
                        ud.YearTransfer = udoAttribute.YearTransfer;
                        ud.ManageSeries = udoAttribute.ManageSeries;
                        ud.ObjectType = udoAttribute.ObjectType;
                        ud.Form = udoAttribute.Form;
                        ud.EnableEnhancedform = udoAttribute.EnableEnhancedform;
                        ud.RebuildEnhancedForm = udoAttribute.RebuildEnhancedForm;
                        ud.Log = udoAttribute.Log;

                        B1AppDomain.RegisterUdo(this, ud);
                    }

                }

                #endregion

                #region Atributo UdoFilhos

                if (obj2 is UdoChildAttribute)
                {
                    udoChildAttribute = obj2 as UdoChildAttribute;

                    if (!string.IsNullOrEmpty(udoChildAttribute.TableName))
                    {
                        UdoChildsModel udf = new UdoChildsModel();
                        udf.TableName = udoChildAttribute.TableName;
                        udf.TableFather = udoChildAttribute.TabelaFather;

                        B1AppDomain.RegisterUdoChild(this, udf);
                    }

                }

                #endregion


            }
            if (attribute == null)
            {
                B1Exception.writeLog("Falha ao indexar Tabela. Por favor checar os atributos informados");
            }



        }

    }
}
