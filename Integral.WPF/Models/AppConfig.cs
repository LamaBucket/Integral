using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Models
{
    public class AppConfig
    {
        public static AppConfig DefaultConfig => new("en-Us", new());


        [JsonConstructor]
        private AppConfig(string uiLanguageCultureCode, Dictionary<string, string> savedServerAddresses)
        {
            UILanguageCultureCode = uiLanguageCultureCode;
            SavedServerAddresses = savedServerAddresses;
        }

        public string UILanguageCultureCode { get; set; }

        public Dictionary<string, string> SavedServerAddresses { get; set; }
    }
}
