using NLog;
using NLog.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzWeb
{
    /// <summary>
    /// NLog
    /// </summary>
    public class NLogHelper
    {
        private static Logger logger;

        static NLogHelper()
        {
            logger = LogManager.GetCurrentClassLogger();
        }

        public static void Info(string msg)
        {
            logger.Info(msg);
        }
        public static void Debug(string msg)
        {
            logger.Debug(msg);
        }
        public static void Error(string msg)
        {
            logger.Error(msg);
        }
        public static void Warn(string msg)
        {
            logger.Warn(msg);
        }
        /// <summary>
        /// 严重错误
        /// </summary>
        /// <param name="msg"></param>
        public static void Fatal(string msg)
        {
            logger.Fatal(msg);
        }
        /// <summary>
        /// 跟踪
        /// </summary>
        /// <param name="msg"></param>
        public static void Trace(string msg)
        {
            logger.Trace(msg);
        }
    }
}
