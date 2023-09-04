using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using SzarFramework.Attributes;

namespace SzarFramework.SAP.DB
{
    public class RepositoryBaseNoObject<TEntity> : IDisposable, IRepositoryBaseNoObject<TEntity>
        where TEntity : class, new()
    {
        public string QuerySelectEntity(TEntity obj)
        {

            Dictionary<string, string> list = new Dictionary<string, string>();
            FieldsAttribute flAttribute = null;
            TablesAttribute tbAttribute = null;


            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                foreach (object field in info.GetCustomAttributes(true))
                {
                    if (field is FieldsAttribute)
                    {
                        flAttribute = field as FieldsAttribute;
                        list.Add("U_" + flAttribute.Name, flAttribute.Description);

                    }

                }
            }

            string fields = "";

            int control = 1;
            foreach (KeyValuePair<string, string> objects in list)
            {
                if (control == 1)
                {
                    fields = objects.Key + " as '" + objects.Value + "'";
                }
                else
                {
                    fields = fields + "," + objects.Key + " as '" + objects.Value + "'";

                }

                control++;
            }

            string tableName = "";
            foreach (object customAttribute in obj.GetType().GetCustomAttributes(true))
            {
                if (customAttribute is TablesAttribute)
                {
                    tbAttribute = customAttribute as TablesAttribute;
                    tableName = tbAttribute.Name;
                }
            }



            return string.Format("select Code, Name, {0} from [@{1}]", fields, tableName);
        }

        public void Add(TEntity obj)
        {
            Dictionary<string, object> list = new Dictionary<string, object>();
            FieldsAttribute flAttribute = null;
            TablesAttribute tbAttribute = null;

            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                foreach (object field in info.GetCustomAttributes(true))
                {
                    if (field is FieldsAttribute)
                    {
                        flAttribute = field as FieldsAttribute;
                        if (info.GetValue(obj) != null)
                        {
                            if (info.GetValue(obj).GetType() == typeof(DateTime))
                            {
                                list.Add("U_" + flAttribute.Name, ((DateTime)info.GetValue(obj)).ToString("yyyyMMdd"));
                            }
                            else
                            {
                                list.Add("U_" + flAttribute.Name, info.GetValue(obj));
                            }

                        }



                    }

                }
            }


            string fields = "";
            string values = "";

            int control = 1;
            foreach (KeyValuePair<string, object> objects in list)
            {
                if (control == 1)
                {
                    fields = objects.Key;
                    values = "'" + objects.Value + "'";
                }
                else
                {
                    fields = fields + "," + objects.Key;
                    values = values + "," + "'" + objects.Value + "'";
                }
                control++;
            }

            string tableName = "";
            foreach (object customAttribute in obj.GetType().GetCustomAttributes(true))
            {
                if (customAttribute is TablesAttribute)
                {
                    tbAttribute = customAttribute as TablesAttribute;
                    tableName = tbAttribute.Name;
                }
            }

            string key = Guid.NewGuid().ToString().Substring(0, 30);
            string query = string.Format(@"insert into [@{0}] (Code, Name, {1}) values ('{3}','{3}',{2})", tableName,
                fields, values, key);
            QB.RSQuery(query);


        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public TEntity GetByEntity(TEntity obj, string key)
        {


            Dictionary<string, object> list = new Dictionary<string, object>();
            FieldsAttribute flAttribute = null;
            TablesAttribute tbAttribute = null;


            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                foreach (object field in info.GetCustomAttributes(true))
                {
                    if (field is FieldsAttribute)
                    {
                        flAttribute = field as FieldsAttribute;
                        list.Add("U_" + flAttribute.Name, info.GetValue(obj));

                    }

                }
            }

            string fields = "";
            string where = "";

            int control = 1;
            if (!string.IsNullOrEmpty(key))
            {
                where = "Code = '" + key + "'";
            }
            
            foreach (KeyValuePair<string, object> objects in list)
            {
                
                if (control == 1)
                {
                    fields = objects.Key;
                    //var value = objects.Value == null ? "" : objects.Value.ToString();
                    //where = objects.Key + " = '" + value + "'";

                }
                else
                {
                    fields = fields + "," + objects.Key;
                    if (objects.Value != null)
                    {
                        if (objects.Value.ToString().Length < 254)
                        {
                            if (objects.Value.GetType() == typeof(DateTime))
                            {
                                DateTime dateValue = Convert.ToDateTime(objects.Value);
                                DateTime dateEmpty = Convert.ToDateTime("01/01/0001 00:00:00");
                                if (dateValue > dateEmpty)
                                {
                                    where = where + " and " + objects.Key + " = '" + Convert.ToDateTime(objects.Value).ToString("yyyyMMdd") + "'";
                                }

                            }
                            else if (objects.Value.GetType() == typeof(int))
                            {
                                if (Convert.ToInt32(objects.Value) != 0)
                                {
                                    where = where + " and " + objects.Key + " = '" + objects.Value.ToString() + "'";
                                }
                            }
                            else
                            {
                                where = where + " and " + objects.Key + " = '" + objects.Value.ToString() + "'";
                            }

                        }

                    }


                }

                control++;
            }

            string tableName = "";
            foreach (object customAttribute in obj.GetType().GetCustomAttributes(true))
            {
                if (customAttribute is TablesAttribute)
                {
                    tbAttribute = customAttribute as TablesAttribute;
                    tableName = tbAttribute.Name;
                }
            }


            string query = string.Format("select Code, Name, {0} from [@{1}] where {2}", fields, tableName, where);


            Recordset oRs = B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            oRs.DoQuery(query);




            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                if (info.Name == "Code")
                {
                    info.SetValue(obj, oRs.Fields.Item("Code").Value);
                }
                else if (info.Name == "Name")
                {
                    info.SetValue(obj, oRs.Fields.Item("Name").Value);
                }
                foreach (object field in info.GetCustomAttributes(true))
                {
                    if (field is FieldsAttribute)
                    {
                        flAttribute = field as FieldsAttribute;


                        info.SetValue(obj, oRs.Fields.Item("U_" + flAttribute.Name).Value);



                    }

                }

            }

            oRs.ClearMemory();

            return obj;



        }

        public List<TEntity> GetAll()
        {

            TEntity obj = new TEntity();

            Dictionary<string, object> list = new Dictionary<string, object>();
            FieldsAttribute flAttribute = null;
            TablesAttribute tbAttribute = null;


            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                foreach (object field in info.GetCustomAttributes(true))
                {
                    if (field is FieldsAttribute)
                    {
                        flAttribute = field as FieldsAttribute;
                        list.Add("U_" + flAttribute.Name, info.GetValue(obj));

                    }

                }
            }

            string fields = "";

            int control = 1;
            foreach (KeyValuePair<string, object> objects in list)
            {
                if (control == 1)
                {
                    fields = objects.Key;

                }
                else
                {
                    fields = fields + "," + objects.Key;


                }

                control++;
            }

            string tableName = "";
            foreach (object customAttribute in obj.GetType().GetCustomAttributes(true))
            {
                if (customAttribute is TablesAttribute)
                {
                    tbAttribute = customAttribute as TablesAttribute;
                    tableName = tbAttribute.Name;
                }
            }


            string query = string.Format("select Code, Name, {0} from [@{1}]", fields, tableName);

            Recordset oRs = B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            oRs.DoQuery(query);



            List<TEntity> result = new List<TEntity>();


            while (!oRs.EoF)
            {
                TEntity obj2 = new TEntity();
                foreach (PropertyInfo info in obj2.GetType().GetProperties())
                {
                    if (info.Name == "Code")
                    {
                        info.SetValue(obj2, oRs.Fields.Item("Code").Value);
                    }
                    else if (info.Name == "Name")
                    {
                        info.SetValue(obj2, oRs.Fields.Item("Name").Value);
                    }
                    foreach (object field in info.GetCustomAttributes(true))
                    {
                        if (field is FieldsAttribute)
                        {
                            flAttribute = field as FieldsAttribute;


                            info.SetValue(obj2, oRs.Fields.Item("U_" + flAttribute.Name).Value);



                        }

                    }

                }

                result.Add(obj2);

                oRs.MoveNext();
            }

            oRs.ClearMemory();

            return result;


        }

        /// <summary>
        /// busca todos usando filtros and
        /// </summary>
        /// <param name="filters">campo, valor </param>
        /// <returns></returns>
        public List<TEntity> GetAll(Dictionary<string, object> filters)
        {
            if (filters == null)
            {
                return null;
            }

            TEntity obj = new TEntity();

            Dictionary<string, object> list = new Dictionary<string, object>();
            FieldsAttribute flAttribute = null;
            TablesAttribute tbAttribute = null;


            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                foreach (object field in info.GetCustomAttributes(true))
                {
                    if (field is FieldsAttribute)
                    {
                        flAttribute = field as FieldsAttribute;
                        list.Add("U_" + flAttribute.Name, info.GetValue(obj));

                    }

                }
            }

            string fields = "";

            int control = 1;
            foreach (KeyValuePair<string, object> objects in list)
            {
                if (control == 1)
                {
                    fields = objects.Key;

                }
                else
                {
                    fields = fields + "," + objects.Key;


                }

                control++;
            }

            string tableName = "";
            foreach (object customAttribute in obj.GetType().GetCustomAttributes(true))
            {
                if (customAttribute is TablesAttribute)
                {
                    tbAttribute = customAttribute as TablesAttribute;
                    tableName = tbAttribute.Name;
                }
            }


            string where = "";
            control = 1;
            foreach (KeyValuePair<string, object> filter in filters)
            {
                if (control == 1)
                {
                    where = filter.Key + " = '" + filter.Value + "'";
                }
                else
                {
                    where = where + " and " + filter.Key + " = '" + filter.Value + "'";
                }
            }

            string query = string.Format("select Code, Name, {0} from [@{1}] where {2}", fields, tableName, where);

            Recordset oRs = B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
            oRs.DoQuery(query);

            List<TEntity> result = new List<TEntity>();


            while (!oRs.EoF)
            {
                TEntity obj2 = new TEntity();
                foreach (PropertyInfo info in obj2.GetType().GetProperties())
                {
                    foreach (object field in info.GetCustomAttributes(true))
                    {
                        if (field is FieldsAttribute)
                        {
                            flAttribute = field as FieldsAttribute;


                            info.SetValue(obj2, oRs.Fields.Item("U_" + flAttribute.Name).Value);



                        }

                    }

                }

                result.Add(obj2);

                oRs.MoveNext();
            }

            oRs.ClearMemory();

            return result;


        }


        public void Update(TEntity obj)
        {
            Dictionary<string, object> list = new Dictionary<string, object>();
            FieldsAttribute flAttribute = null;
            TablesAttribute tbAttribute = null;
            string key = "";

            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                if (info.Name == "Code")
                {
                    key = info.GetValue(obj).ToString();
                }
                foreach (object field in info.GetCustomAttributes(true))
                {
                    if (field is FieldsAttribute)
                    {
                        flAttribute = field as FieldsAttribute;
                        if (info.GetValue(obj) != null)
                        {
                            if (info.GetValue(obj).GetType() == typeof(DateTime))
                            {
                                list.Add("U_" + flAttribute.Name, ((DateTime)info.GetValue(obj)).ToString("yyyyMMdd"));
                            }
                            else
                            {
                                list.Add("U_" + flAttribute.Name, info.GetValue(obj));
                            }

                        }



                    }

                }
            }


            string fields = "";

            int control = 1;
            foreach (KeyValuePair<string, object> objects in list)
            {
                if (control == 1)
                {
                    fields = objects.Key + " = '" + objects.Value + "'";
                }
                else
                {
                    fields = fields + ", " + objects.Key + " = '" + objects.Value + "'";
                }
                control++;
            }

            string tableName = "";
            foreach (object customAttribute in obj.GetType().GetCustomAttributes(true))
            {
                if (customAttribute is TablesAttribute)
                {
                    tbAttribute = customAttribute as TablesAttribute;
                    tableName = tbAttribute.Name;
                }
            }


            string query = string.Format(@"update [@{0}] set {1} where Code = '{2}'", tableName, fields, key);
            QB.RSQuery(query);



        }

        public void Delete(TEntity obj)
        {
            string code = "";
            TablesAttribute tbAttribute = null;

            foreach (PropertyInfo info in obj.GetType().GetProperties())
            {
                if (info.Name == "Code")
                {
                    code = info.GetValue(obj).ToString();
                }
            }

            string tableName = "";
            foreach (object customAttribute in obj.GetType().GetCustomAttributes(true))
            {
                if (customAttribute is TablesAttribute)
                {
                    tbAttribute = customAttribute as TablesAttribute;
                    tableName = tbAttribute.Name;
                    break;
                }
            }

            QB.RSQuery(string.Format("delete from [@{0}] where Code = '{1}'", tableName, code));
        }
    }
}
