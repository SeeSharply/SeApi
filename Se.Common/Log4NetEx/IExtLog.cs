using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net;

namespace log4net.Ext
{
    /*使用1方法
     * log4net.Ext.IExtLog log = log4net.Ext.ExtLogManager.GetLogger("filelog");
     */
    public interface IExtLog : ILog
    {
        void Info(string clientIP, string clientUser, string requestUri, string action, object message);
        void Info(string clientIP, string clientUser, string requestUri, string action, object message, Exception t);


        void Warn(string clientIP, string clientUser, string requestUri, string action, object message);
        void Warn(string clientIP, string clientUser, string requestUri, string action, object message, Exception t);


        void Error(string clientIP, string clientUser, string requestUri, string action, object message);
        void Error(string clientIP, string clientUser, string requestUri, string action, object message, Exception t);

        void Fatal(string clientIP, string clientUser, string requestUri, string action, object message);
        void Fatal(string clientIP, string clientUser, string requestUri, string action, object message, Exception t);
    }
}