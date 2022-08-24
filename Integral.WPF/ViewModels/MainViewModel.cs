using Integral.WPF.Commands;
using Integral.WPF.Services.Interfaces;
using Integral.WPF.Services.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private INavigator _navigator;

        public INavigator Navigator
        {
            get => _navigator;
            set
            {
                _navigator = value;
                OnPropertyChanged(nameof(Navigator));
            }
        }


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


        private bool _navigationMenuVisible;

        public bool NavigationMenuVisible
        {
            get => _navigationMenuVisible;
            set
            {
                _navigationMenuVisible = value;
                OnPropertyChanged(nameof(NavigationMenuVisible));
            }
        }



        public ICommand LogoutCommand { get; set; }

        public MainViewModel(INavigator navigator, IAuthenticator authenticator)
        {
            _navigator = navigator;
            _authenticator = authenticator;

            LogoutCommand = new LogoutCommand(authenticator, navigator, this);
        }

    }
}
