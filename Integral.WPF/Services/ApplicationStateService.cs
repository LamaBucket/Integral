using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using Integral.WPF.Models;
using Integral.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services
{
    public class ApplicationStateService : IApplicationStateService, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;


        public bool IsLoggedIn => CurrentRole.HasValue;



        private User? _currentUser;

        public User? CurrentUser
        {
            get => _currentUser;
            set
            {
                _currentUser = value;
                PropertyChanged?.Invoke(this, new(nameof(CurrentUser)));
            }
        }




        private Role? _currentRole;

        public Role? CurrentRole
        {
            get => _currentRole;
            set
            {
                _currentRole = value;
                PropertyChanged?.Invoke(this, new(nameof(CurrentRole)));
                PropertyChanged?.Invoke(this, new(nameof(CurrentPermissions)));
                PropertyChanged?.Invoke(this, new(nameof(IsLoggedIn)));
            }
        }




        public ApplicationRolePermissions CurrentPermissions => CurrentRole.HasValue && ApplicationRolePermissions.AllPermissions.ContainsKey(CurrentRole.Value) ? ApplicationRolePermissions.AllPermissions[CurrentRole.Value] : ApplicationRolePermissions.DefaultPermissions;
    }
}
