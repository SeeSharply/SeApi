using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SeApi.Common
{
    public class SeLog
    {
        public static log4net.Ext.IExtLog Log { get; set; }

         static SeLog()
        {
            log4net.Ext.IExtLog log = log4net.Ext.ExtLogManager.GetLogger("filelog");
            Log = log;
        }
    }
}
