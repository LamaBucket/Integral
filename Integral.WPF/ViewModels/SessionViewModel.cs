using Integral.Domain.Models.Enums;
using Integral.WPF.Commands;
using Integral.WPF.Models;
using Integral.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.ViewModels
{
    public class SessionViewModel : BaseViewModel
    {

        public static IEnumerable<Role> UserRoles => Enum.GetValues<Role>();


        private IAuthenticator _authenticator;

        public IAuthenticator Authenticator
        {
            get => _authenticator;
            set
            {
                _authenticator = value;
                OnPropertyChanged(nameof(Authenticator));
            }
        }


        public SessionViewModel(IAuthenticator authenticator, IAppConfigManagementService configManagementService)
        {
            _authenticator = authenticator;
            _loginCommand = new LoginCommand(this, authenticator);

            _config = configManagementService.GetConfig();

            AddAddressCommand = new AddAddressToConfigCommand(this, configManagementService);
            SelectAddressCommand = new SelectAddressCommand(this);
        }

        public ICommand AddAddressCommand { get; set; }

        public ICommand SelectAddressCommand { get; set; }


        private ICommand _loginCommand;

        public ICommand LoginCommand
        {
            get => _loginCommand;
            set
            {
                _loginCommand = value;
                OnPropertyChanged(nameof(LoginCommand));
            }
        }


        private string _serverAddress = String.Empty;

        public string ServerAddress
        {
            get => _serverAddress;
            set
            {
                _serverAddress = value;
                OnPropertyChanged(nameof(ServerAddress));
            }
        }


        private string _login = String.Empty;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }


        private string _password = String.Empty;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }


        private Role _userRole = default;

        public Role UserRole
        {
            get => _userRole;
            set
            {
                _userRole = value;
                OnPropertyChanged(nameof(UserRole));
            }
        }



        private AppConfig _config;

        public AppConfig Config
        {
            get => _config;
            set
            {
                _config = value;
                OnPropertyChanged(nameof(Config));
                OnPropertyChanged(nameof(AddressNames));
            }
        }


        private bool _modifyAddressesDialogOpen;

        public bool ModifyAddressesDialogOpen
        {
            get => _modifyAddressesDialogOpen;
            set
            {
                _modifyAddressesDialogOpen = value;
                OnPropertyChanged(nameof(ModifyAddressesDialogOpen));
            }
        }


        private bool _addAddressDialogOpen;

        public bool AddAddressDialogOpen
        {
            get => _addAddressDialogOpen;
            set
            {
                _addAddressDialogOpen = value;
                OnPropertyChanged(nameof(AddAddressDialogOpen));
            }
        }


        private string _addAddressAddress = String.Empty;

        public string AddAddressAddress
        {
            get => _addAddressAddress;
            set
            {
                _addAddressAddress = value;
                OnPropertyChanged(nameof(AddAddressAddress));
            }
        }


        private string _addAddressName = String.Empty;

        public string AddAddressName
        {
            get => _addAddressName;
            set
            {
                _addAddressName = value;
                OnPropertyChanged(nameof(AddAddressName));
            }
        }

        public IEnumerable<Tuple<string, string>> AddressNames => Config.SavedServerAddresses.Select(x => new Tuple<string, string>(x.Key, x.Value));


        private Tuple<string, string>? _selectedAddress;

        public Tuple<string, string>? SelectedAddress
        {
            get => _selectedAddress;
            set
            {
                _selectedAddress = value;
                OnPropertyChanged(nameof(SelectedAddress));
            }
        }

    }
}
