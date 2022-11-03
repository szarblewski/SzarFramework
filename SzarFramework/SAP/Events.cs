using System;
using SAPbouiCOM;

namespace SzarFramework
{
    internal class Events
    {

        static private SAPbouiCOM.Application _objApplication = null;

        public Events()
        {
            try
            {
                _objApplication = B1AppDomain.Application;

                _objApplication.AppEvent += new _IApplicationEvents_AppEventEventHandler(Global.CatchAppEvents);
                _objApplication.ItemEvent += new _IApplicationEvents_ItemEventEventHandler(Global.CatchItemEvent);
                _objApplication.MenuEvent += new _IApplicationEvents_MenuEventEventHandler(Global.CatchMenuEvent);
                _objApplication.RightClickEvent += new _IApplicationEvents_RightClickEventEventHandler(Global.CatchRightClickEvent);
                _objApplication.FormDataEvent += new _IApplicationEvents_FormDataEventEventHandler(Global.CatchFormDataEvent);
                _objApplication.UDOEvent += new _IApplicationEvents_UDOEventEventHandler(Global.CatchUdoEvent);
                _objApplication.ProgressBarEvent += new _IApplicationEvents_ProgressBarEventEventHandler(Global.CatchProgressBarEvent);
                _objApplication.PrintEvent += new _IApplicationEvents_PrintEventEventHandler(Global.CatchPrintEvent);
                _objApplication.ReportDataEvent += new _IApplicationEvents_ReportDataEventEventHandler(Global.CatchReportDataEvent);

                _objApplication.MetadataAutoRefresh = true;
                B1AppDomain.Application = _objApplication;
            }
            catch (Exception ex)
            {
                B1Exception.throwException("Eventos: ", ex);
            }
        }

    }
}
