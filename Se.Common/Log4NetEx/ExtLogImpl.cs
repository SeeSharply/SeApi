using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using log4net.Core;

namespace log4net.Ext
{
    public class ExtLogImpl : LogImpl, IExtLog
    {
        /// <summary>
        /// The fully qualified name of this declaring type not the type of any subclass.
        /// </summary>
        private readonly static Type ThisDeclaringType = typeof(ExtLogImpl);
        public ExtLogImpl(ILogger logger)
            : base(logger)
        {
        }
        #region IExtLog 成员

        public void Info(string clientIP, string clientUser, string requestUri, string action, object message)
        {
            Info(clientIP, clientUser, requestUri, action, message, null);
        }

        public void Info(string clientIP, string clientUser, string requestUri, string action, object message, Exception t)
        {
            if (this.IsInfoEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Info, message, t);
                loggingEvent.Properties["ClientIP"] = clientIP;
                loggingEvent.Properties["ClientUser"] = clientUser;
                loggingEvent.Properties["RequestUrl"] = requestUri;
                loggingEvent.Properties["Action"] = action;
                Logger.Log(loggingEvent);
            }
        }


        public void Warn(string clientIP, string clientUser, string requestUri, string action, object message)
        {
            Warn(clientIP, clientUser, requestUri, action, message, null);
        }

        public void Warn(string clientIP, string clientUser, string requestUri, string action, object message, Exception t)
        {
            if (this.IsWarnEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Warn, message, t);
                loggingEvent.Properties["ClientIP"] = clientIP;
                loggingEvent.Properties["ClientUser"] = clientUser;
                loggingEvent.Properties["RequestUrl"] = requestUri;
                loggingEvent.Properties["Action"] = action;
                Logger.Log(loggingEvent);
            }
        }

        public void Error(string clientIP, string clientUser, string requestUri, string action, object message)
        {
            Error(clientIP, clientUser, requestUri, action, message, null);
        }

        public void Error(string clientIP, string clientUser, string requestUri, string action, object message, Exception t)
        {
            if (this.IsErrorEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Error, message, t);
                loggingEvent.Properties["ClientIP"] = clientIP;
                loggingEvent.Properties["ClientUser"] = clientUser;
                loggingEvent.Properties["RequestUrl"] = requestUri;
                loggingEvent.Properties["Action"] = action;
                Logger.Log(loggingEvent);
            }
        }

        public void Fatal(string clientIP, string clientUser, string requestUri, string action, object message)
        {
            Fatal(clientIP, clientUser, requestUri, action, message, null);
        }

        public void Fatal(string clientIP, string clientUser, string requestUri, string action, object message, Exception t)
        {
            if (this.IsFatalEnabled)
            {
                LoggingEvent loggingEvent = new LoggingEvent(ThisDeclaringType, Logger.Repository, Logger.Name, Level.Fatal, message, t);
                loggingEvent.Properties["ClientIP"] = clientIP;
                loggingEvent.Properties["ClientUser"] = clientUser;
                loggingEvent.Properties["RequestUrl"] = requestUri;
                loggingEvent.Properties["Action"] = action;
                Logger.Log(loggingEvent);
            }
        }
        #endregion
    }
}