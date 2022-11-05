using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace SzarFramework
{
    public static class ObjectExtensions
    {
        public static void ClearMemory(this object obj)
        {
            Marshal.FinalReleaseComObject(obj);
            obj = null;
            GC.Collect();
        }
    }
}
