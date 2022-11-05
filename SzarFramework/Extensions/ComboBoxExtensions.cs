using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzarFramework.Extensions
{
    public static class ComboBoxExtensions
    {
        public static bool ClearData(this SAPbouiCOM.ComboBox oCombo)
        {
            try
            {
                if (oCombo.ValidValues.Count > 0)
                {

                    while (oCombo.ValidValues.Count > 0)
                    {

                        oCombo.ValidValues.Remove(0, SAPbouiCOM.BoSearchKey.psk_Index);

                    }
                }

            }
            catch (Exception ex) { throw ex; }

            return true;
        }
    }
}
