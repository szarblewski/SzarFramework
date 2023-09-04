using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using SzarFramework.Attributes;

namespace SzarFramework.SAP.DB.Repositories
{
    public class RepositoryBaseUdo<TEntity> : IDisposable, IRepositoryBaseUdo<TEntity> where TEntity : class,
        new()
    {
        public class Child
        {
            public Child()
            {
                Filhos = new List<object>();
            }
            public object Objeto { get; set; }
            public List<object> Filhos { get; set; }
        }

        public List<Child> Childs;
        CompanyService oCompanyService;
        GeneralService oGeneralService;
        GeneralData oGeneralData;
        GeneralDataParams oGeneralDataParams;

        GeneralDataCollection oChildren;
        GeneralData oChild;

        public RepositoryBaseUdo()
        {
            Childs = new List<Child>();
        }

        public int Add(TEntity entity)
        {
            try
            {
                FieldsAttribute flAttribute = null;
                var tb = B1AppDomain.DictionaryTablesFields.Where(x => x.Key.GetType() == typeof(TEntity)).Select(x => x.Value).FirstOrDefault();
                object ob = B1AppDomain.DictionaryTablesFields.Where(x => x.Key.GetType() == typeof(TEntity)).Select(x => x.Key).FirstOrDefault();
                oCompanyService = B1AppDomain.Company.GetCompanyService();
                oGeneralService = oCompanyService.GetGeneralService(tb.Udos.Code);
                oGeneralData = oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralData);

                //if (tb.Udos.ObjectType == BoUDOObjType.boud_Document)
                //{
                //    oGeneralData.SetProperty("DocEntry", ((ob)entity).DocEntry);
                //}
                //else if (tb.Udos.ObjectType == BoUDOObjType.boud_MasterData)
                //{
                //    oGeneralData.SetProperty("Code", entity.Code);
                //}


                foreach (PropertyInfo info in entity.GetType().GetProperties())
                {
                    var t = info.Name;
                    if (info.Name == "Code")
                    {
                        try
                        {
                            if (tb.Udos.ObjectType == BoUDOObjType.boud_MasterData)
                            {
                                if (!string.IsNullOrEmpty(info.GetValue(entity).ToString()))
                                {
                                    oGeneralData.SetProperty(info.Name, info.GetValue(entity).ToString());
                                }
                            }


                        }
                        catch
                        {
                        }
                    }


                    foreach (object field in info.GetCustomAttributes(true))
                    {
                        if (field is FieldsAttribute)
                        {
                            flAttribute = field as FieldsAttribute;
                            string nome = "U_" + flAttribute.Name;
                            object vlr = info.GetValue(entity);
                            try
                            {
                                if (info.GetType() == typeof(DateTime))
                                {
                                    oGeneralData.SetProperty(nome, ((DateTime)vlr).ToString("yyyyMMdd"));
                                }
                                else
                                {
                                    oGeneralData.SetProperty(nome, vlr);
                                }

                            }
                            catch { }

                        }
                    }
                }

                foreach (Child item in Childs)
                {
                    var tbc = B1AppDomain.DictionaryTablesFields.Where(x => x.Key.GetType() == item.Objeto.GetType()).Select(x => x.Value).FirstOrDefault();
                    oChildren = oGeneralData.Child(tbc.Name);
                    foreach (var filho in item.Filhos)
                    {
                        oChild = oChildren.Add();
                        foreach (PropertyInfo info in filho.GetType().GetProperties())
                        {
                            var t = info.Name;


                            foreach (object field in info.GetCustomAttributes(true))
                            {
                                if (field is FieldsAttribute)
                                {

                                    flAttribute = field as FieldsAttribute;
                                    string nome = "U_" + flAttribute.Name;
                                    object vlr = info.GetValue(filho);
                                    try
                                    {
                                        if (info.GetType() == typeof(DateTime))
                                        {
                                            oChild.SetProperty(nome, ((DateTime)vlr).ToString("yyyyMMdd"));
                                        }
                                        else
                                        {
                                            oChild.SetProperty(nome, vlr);
                                        }

                                    }
                                    catch { }

                                }
                            }
                        }
                    }
                }



                oGeneralDataParams = oGeneralService.Add(oGeneralData);
                if (tb.Udos.ObjectType == BoUDOObjType.boud_Document)
                {
                    return oGeneralDataParams.GetProperty("DocEntry");
                }
                else if (tb.Udos.ObjectType == BoUDOObjType.boud_MasterData)
                {
                    return oGeneralDataParams.GetProperty("Code");
                }
                oGeneralData.ClearMemory();
                oGeneralDataParams.ClearMemory();
                oGeneralService.ClearMemory();
                return -1;

            }
            catch (Exception ex)
            {

                B1Exception.writeLog(ex.Message);
                return -1;
            }




        }

        public int Delete(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public TEntity GetByKey(string key, TEntity entity)
        {
            try
            {
                FieldsAttribute flAttribute = null;
                var tb = B1AppDomain.DictionaryTablesFields.Where(x => x.Key.GetType() == typeof(TEntity)).Select(x => x.Value).FirstOrDefault();
                object ob = B1AppDomain.DictionaryTablesFields.Where(x => x.Key.GetType() == typeof(TEntity)).Select(x => x.Key).FirstOrDefault();
                oCompanyService = B1AppDomain.Company.GetCompanyService();
                oGeneralService = oCompanyService.GetGeneralService(tb.Udos.Code);
                oGeneralDataParams = ((GeneralDataParams)(oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams)));
                if (tb.TableTypeSAP == BoUTBTableType.bott_MasterData)
                {
                    oGeneralDataParams.SetProperty("Code", key);
                }
                else
                {
                    oGeneralDataParams.SetProperty("DocEntry", key);
                }
                oGeneralData = oGeneralService.GetByParams(oGeneralDataParams);

                foreach (PropertyInfo info in entity.GetType().GetProperties())
                {
                    var t = info.Name;
                    if (info.Name == "Code")
                    {
                        try
                        {
                            if (tb.Udos.ObjectType == BoUDOObjType.boud_MasterData)
                            {
                                if (!string.IsNullOrEmpty(oGeneralData.GetProperty(info.Name).ToString()))
                                {
                                    //oGeneralData.SetProperty(info.Name, info.GetValue(entity).ToString());
                                    info.SetValue(entity, oGeneralData.GetProperty(info.Name));
                                }
                            }

                        }
                        catch
                        {
                        }
                    }
                    else if (info.Name == "DocEntry")
                    {
                        try
                        {
                            if (tb.Udos.ObjectType == BoUDOObjType.boud_Document)
                            {
                                if (!string.IsNullOrEmpty(oGeneralData.GetProperty(info.Name).ToString()))
                                {
                                    //oGeneralData.SetProperty(info.Name, info.GetValue(entity).ToString());
                                    info.SetValue(entity, oGeneralData.GetProperty(info.Name));
                                }
                            }
                        }
                        catch
                        {
                        }
                    }


                    foreach (object field in info.GetCustomAttributes(true))
                    {
                        if (field is FieldsAttribute)
                        {
                            flAttribute = field as FieldsAttribute;
                            string nome = "U_" + flAttribute.Name;
                            object vlr = info.GetValue(entity);
                            try
                            {
                                if (!string.IsNullOrEmpty(oGeneralData.GetProperty(nome).ToString()))
                                {
                                    //oGeneralData.SetProperty(nome, vlr);
                                    info.SetValue(entity, oGeneralData.GetProperty(nome));
                                }

                            }
                            catch { }

                        }
                    }
                }



                return entity;


            }
            catch (Exception ex)
            {

                B1Exception.writeLog(ex.Message);
                return null;
            }
        }

        public int Update(string key, TEntity entity)
        {
            try
            {
                FieldsAttribute flAttribute = null;
                var tb = B1AppDomain.DictionaryTablesFields.Where(x => x.Key.GetType() == typeof(TEntity)).Select(x => x.Value).FirstOrDefault();
                object ob = B1AppDomain.DictionaryTablesFields.Where(x => x.Key.GetType() == typeof(TEntity)).Select(x => x.Key).FirstOrDefault();
                oCompanyService = B1AppDomain.Company.GetCompanyService();
                oGeneralService = oCompanyService.GetGeneralService(tb.Udos.Code);
                oGeneralDataParams = ((GeneralDataParams)(oGeneralService.GetDataInterface(GeneralServiceDataInterfaces.gsGeneralDataParams)));
                if (tb.TableTypeSAP == BoUTBTableType.bott_MasterData)
                {
                    oGeneralDataParams.SetProperty("Code", key);
                }
                else
                {
                    oGeneralDataParams.SetProperty("DocEntry", key);
                }
                oGeneralData = oGeneralService.GetByParams(oGeneralDataParams);

                foreach (PropertyInfo info in entity.GetType().GetProperties())
                {
                    var t = info.Name;
                    if (info.Name == "Code")
                    {
                        try
                        {
                            if (tb.Udos.ObjectType == BoUDOObjType.boud_MasterData)
                            {
                                if (!string.IsNullOrEmpty(info.GetValue(entity).ToString()))
                                {
                                    oGeneralData.SetProperty(info.Name, info.GetValue(entity).ToString());
                                }
                            }


                        }
                        catch
                        {
                        }
                    }


                    foreach (object field in info.GetCustomAttributes(true))
                    {
                        if (field is FieldsAttribute)
                        {
                            flAttribute = field as FieldsAttribute;
                            string nome = "U_" + flAttribute.Name;
                            object vlr = info.GetValue(entity);
                            try
                            {
                                if (info.GetType() == typeof(DateTime))
                                {
                                    if (oGeneralData.GetProperty(nome) != vlr && vlr != null)
                                    {
                                        oGeneralData.SetProperty(nome, ((DateTime)vlr).ToString("yyyyMMdd"));
                                    }
                                }
                                else if (info.PropertyType == typeof(Int32))
                                {
                                    if (Convert.ToInt32(vlr) > 0)
                                    {
                                        oGeneralData.SetProperty(nome, vlr);
                                    }


                                }
                                else
                                {
                                    if (oGeneralData.GetProperty(nome) != vlr && vlr != null)
                                    {
                                        oGeneralData.SetProperty(nome, vlr);
                                    }
                                }


                            }
                            catch { }

                        }
                    }
                }

                foreach (Child item in Childs)
                {
                    var tbc = B1AppDomain.DictionaryTablesFields.Where(x => x.Key.GetType() == item.Objeto.GetType()).Select(x => x.Value).FirstOrDefault();
                    oChildren = oGeneralData.Child(tbc.Name);
                    foreach (var filho in item.Filhos)
                    {
                        oChild = oChildren.Add();
                        foreach (PropertyInfo info in filho.GetType().GetProperties())
                        {
                            var t = info.Name;


                            foreach (object field in info.GetCustomAttributes(true))
                            {
                                if (field is FieldsAttribute)
                                {

                                    flAttribute = field as FieldsAttribute;
                                    string nome = "U_" + flAttribute.Name;
                                    object vlr = info.GetValue(filho);
                                    try
                                    {

                                        if (info.PropertyType == typeof(DateTime))
                                        {
                                            DateTime dt = (DateTime)vlr;
                                            //vlr = dt.ToString("ddMMyyyy");
                                            oChild.SetProperty(nome, dt);
                                        }
                                        else
                                        {
                                            oChild.SetProperty(nome, vlr);
                                        }




                                        //if (oChild.GetProperty(nome) != vlr)
                                        //{
                                        //    if (info.PropertyType == typeof(DateTime) && vlr != null)
                                        //    {

                                        //    }


                                        //}


                                    }
                                    catch { }

                                }
                            }
                        }
                    }
                }



                oGeneralService.Update(oGeneralData);

                oGeneralData.ClearMemory();
                oGeneralDataParams.ClearMemory();
                oGeneralService.ClearMemory();
                return 0;


            }
            catch (Exception ex)
            {
                B1Exception.writeLog(ex.Message);
                return -1;
            }
        }
    }
}
