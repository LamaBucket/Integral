using Integral.WPF.Models;
using Integral.WPF.Services;
using Integral.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Integral.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {

        public App()
        {
            IAppConfigManagementService configManager = new AppConfigManagementService();

            AppConfig config = configManager.GetConfig();

            SetupLanguage(config);
        }

        protected void SetupLanguage(AppConfig config)
        {
            Thread.CurrentThread.CurrentUICulture = new(config.UILanguageCultureCode);
        }
    }
}
