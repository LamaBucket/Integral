using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using Integral.WPF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services.Interfaces
{
    public interface IApplicationStateService
    {
        bool IsLoggedIn { get; }

        User? CurrentUser { get; set; }

        Role? CurrentRole { get; set; }

        ApplicationRolePermissions CurrentPermissions { get; }
    }
}
