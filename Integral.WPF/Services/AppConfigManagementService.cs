using Integral.WPF.Exceptions;
using Integral.WPF.Models;
using Integral.WPF.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services
{
    public class AppConfigManagementService : IAppConfigManagementService
    {
        public static string AppConfigPath => Path.Combine(Directory.GetCurrentDirectory(), "IntegralAppConfig.json");

        public AppConfig GetConfig()
        {
            if (!File.Exists(AppConfigPath))
                RecreateConfigSync();

            string content = File.ReadAllText(AppConfigPath);

            var config = JsonConvert.DeserializeObject<AppConfig>(content);

            if(config is null)
                throw new ClientException(ClientErrorCodes.UnableToGetConfig.ToString());

            return config;
        }

        public async Task<bool> RecreateConfig()
        {
            await File.WriteAllTextAsync(AppConfigPath, JsonConvert.SerializeObject(AppConfig.DefaultConfig));

            return true;
        }

        public async Task<AppConfig> UpdateConfig(AppConfig config)
        {
            await File.WriteAllTextAsync(AppConfigPath, JsonConvert.SerializeObject(config));

            return config;
        }

        protected void RecreateConfigSync()
        {
            File.WriteAllText(AppConfigPath, JsonConvert.SerializeObject(AppConfig.DefaultConfig));
        }
    }
}
