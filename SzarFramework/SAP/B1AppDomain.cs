using SAPbobsCOM;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using SzarFramework.Models;

namespace SzarFramework
{
    public sealed class B1AppDomain
    {
        //DICIONARIOS QUE ARMAZENAM AS INSTANCIAS DAS CLASSES
        internal static Dictionary<string, FormBase> DictionaryFormEvent = new Dictionary<string, FormBase>();
        internal static Dictionary<string, MenuBase> DictionaryMenuEvent = new Dictionary<string, MenuBase>();
        internal static Dictionary<object, TableModel> DictionaryTablesFields = new Dictionary<object, TableModel>();
        internal static Dictionary<object, UdoModel> DictionaryUdos = new Dictionary<object, UdoModel>();
        internal static Dictionary<object, UdoChildsModel> DictionaryUdosChilds = new Dictionary<object, UdoChildsModel>();

        static private B1AppDomain objAppDomainClass = null;
        static private SAPbouiCOM.Application objApplication = null;
        static private SAPbobsCOM.Company objCompany = null;
        static private string strConnectionString = "";

        #region PUBLICO
        static public bool Connected { get; set; }
        
        static public string ConnectionString
        {
            get
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new B1AppDomain();
                return strConnectionString;
            }
            set
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new B1AppDomain();
                strConnectionString = value;
                AppDomain.CurrentDomain.SetData("SAPConnectionString", strConnectionString);

            }
        }

        static public SAPbouiCOM.Application Application
        {
            get
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new B1AppDomain();
                return objApplication;
            }
            set
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new B1AppDomain();
                objApplication = value;
                AppDomain.CurrentDomain.SetData("SAPApplication", objApplication);
            }
        }

        static public SAPbobsCOM.Company Company
        {
            get
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new B1AppDomain();
                return objCompany;
            }
            set
            {
                if (objAppDomainClass == null)
                    objAppDomainClass = new B1AppDomain();
                objCompany = value;
                AppDomain.CurrentDomain.SetData("SAPCompany", objCompany);

            }
        }

        



        #endregion

        #region INTERNO

        internal static void RegisterFormByType(string formUid, FormBase formBase)
        {

            if (!string.IsNullOrEmpty(formUid))
            {
                DictionaryFormEvent.Add(formUid, formBase);
            }

        }

        internal static void RegisterMenuByType(string menuUid, MenuBase menuBase)
        {
            if (!string.IsNullOrEmpty(menuUid))
            {
                DictionaryMenuEvent.Add(menuUid, menuBase);
            }
        }

        internal static void RegisterUdo(object obj, UdoModel udo)
        {
            if (obj != null)
            {
                DictionaryUdos.Add(obj, udo);
            }
        }

        internal static void RegisterUdoChild(object obj, UdoChildsModel udo)
        {
            if (obj != null)
            {
                DictionaryUdosChilds.Add(obj, udo);
            }
        }

        internal static void RegisterTable(object obj, TableModel tables)
        {
            if (obj != null)
            {
                DictionaryTablesFields.Add(obj, tables);
            }
        }

        #endregion
    }
}
