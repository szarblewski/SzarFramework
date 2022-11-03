using SAPbobsCOM;
using System.Collections.Generic;


namespace SzarFramework.Models
{
    internal class UdoModel
    {
        public string TableName { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public SAPbobsCOM.BoYesNoEnum Cancel { get; set; }
        public SAPbobsCOM.BoYesNoEnum Close { get; set; }
        public SAPbobsCOM.BoYesNoEnum CreateDefaultForm { get; set; }
        public SAPbobsCOM.BoYesNoEnum Delete { get; set; }
        public SAPbobsCOM.BoYesNoEnum Find { get; set; }
        public SAPbobsCOM.BoYesNoEnum YearTransfer { get; set; }
        public SAPbobsCOM.BoYesNoEnum ManageSeries { get; set; }
        public SAPbobsCOM.BoUDOObjType ObjectType { get; set; }
        public string TableFather { get; set; }
        public string Form { get; set; }
        public List<UdoChildsModel> Children { get; set; }
        public List<UdoModel> ListUdos { get; set; }
        public SAPbobsCOM.BoYesNoEnum EnableEnhancedform { get; set; }
        public BoYesNoEnum RebuildEnhancedForm { get; set; }
        public bool Log { get; set; }
    }
}
