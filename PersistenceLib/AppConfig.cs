using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLib
{
    class AppConfig
    {
        private Configuration config;

        public AppConfig()
        {
            this.config = InitConfig();
        }

        public Configuration InitConfig()
        {
            Configuration config = null;
            string exeConfigPath = this.GetType().Assembly.Location;
            try
            {
                config = ConfigurationManager.OpenExeConfiguration(exeConfigPath);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return config;
        }

        public string GetAppSetting(string key)
        {
            KeyValueConfigurationElement element = this.config.AppSettings.Settings[key];
            if (element != null)
            {
                string value = element.Value;
                if (!string.IsNullOrEmpty(value))
                    return value;
            }
            return string.Empty;
        }
    }
}
