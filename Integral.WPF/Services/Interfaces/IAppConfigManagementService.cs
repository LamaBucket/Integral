using Integral.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services.Interfaces
{
    public interface IAppConfigManagementService
    {
        AppConfig GetConfig();

        Task<bool> RecreateConfig();

        Task<AppConfig> UpdateConfig(AppConfig config);
    }
}
