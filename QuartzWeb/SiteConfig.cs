using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace QuartzWeb
{
    /// <summary>
    /// 读取配置信息帮助类
    /// </summary>
    public static class SiteConfig
    {
        private static IConfigurationSection _appSection = null;
        /// <summary>
        /// 获取配置节点Value
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetQuarzConfigValue(string key)
        {
            
            string str = string.Empty;
            if (_appSection.GetSection(key) != null)
            {
                str = _appSection.GetSection(key).Value;
            }
            return str;
        }
        public static void SetAppSetting(IConfigurationSection section)
        {
            _appSection = section;
        }
    }
}
