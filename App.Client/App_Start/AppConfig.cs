using System;
using System.Configuration;

namespace App.Client
{
    public class AppConfig
    {
        public static readonly string AppName = ConfigurationManager.AppSettings["brand:AppName"];
        public static readonly string AppVersion = ConfigurationManager.AppSettings["brand:AppVersion"];
        public static readonly string AppEnvironment = ConfigurationManager.AppSettings["brand:AppEnvironment"];
        public static readonly string CompanyName = ConfigurationManager.AppSettings["brand:CompanyName"];
        public static readonly string BootstrapTheme = ConfigurationManager.AppSettings["brand:BootstrapTheme"];
        public static readonly bool ThemeInverse = Convert.ToBoolean(ConfigurationManager.AppSettings["brand:ThemeInverse"]);
        public static readonly string DefaultUser = ConfigurationManager.AppSettings["seed:DefaultUser"];
        public static readonly string DefaultUserPassword = ConfigurationManager.AppSettings["seed:DefaultUserPassword"];
    }
}
