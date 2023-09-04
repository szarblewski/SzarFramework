using SAPbouiCOM;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SzarFramework.Models;

namespace SzarFramework.Extensions
{
    public static class GridExtensions
    {

        public static void UpdateGridFormater(this Grid oGrid, Form oForm)
        {
            try
            {
                
                List<GridFormaterModel> gridFormater = B1AppDomain.DictionaryFormEvent.Where(x => x.Key == oForm.TypeEx).Select(x => x.Value).FirstOrDefault().GridFormater;
                ApplicationExtensions.FormaterGrid(oForm, gridFormater);
               
            }
            catch (Exception ex)
            {
               
                B1Exception.throwException("UpdateGridFormater:", ex);
            }
            
        }

        
        
    }
}
