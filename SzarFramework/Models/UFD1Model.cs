﻿using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzarFramework.Querys;

namespace SzarFramework.Models
{
    internal class UFD1Model
    {
        public string TableID { get; set; }
        public int FieldID { get; set; }
        public int IndexID { get; set; }
        public string FldValue { get; set; }
        public string Descr { get; set; }
        public DateTime FldDate { get; set; }


        internal List<UFD1Model> FieldItems(string tableName, string name)
        {
            try
            {
                List<UFD1Model> fieldItems = new List<UFD1Model>();

                Recordset oRs = B1AppDomain.Company.GetBusinessObject(BoObjectTypes.BoRecordset);
                fieldItems = oRs.DoQuery(String.Format(QuerySelect.Select("ufd1List"), tableName, name), this);
                oRs.ClearMemory();
                return fieldItems;

            }
            catch (Exception ex)
            {
                B1Exception.writeLog(ex.Message);
                return null;
            }
        }
        
    }
}
