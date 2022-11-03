﻿using SAPbobsCOM;
using System.Collections.Generic;


namespace SzarFramework.Models
{
    internal class FieldModel
    {       

        public string TableName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public BoFieldTypes Type { get; set; }
        public BoFldSubTypes SubType { get; set; }
        public BoYesNoEnum Mandatory { get; set; }
        public string DefaultValue { get; set; }
        public int Size { get; set; }
        public string UdoReference { get; set; }
        public string TableReference { get; set; }               
        public List<ValidValuesModel> ValidValues { get; set; }
    }
}
