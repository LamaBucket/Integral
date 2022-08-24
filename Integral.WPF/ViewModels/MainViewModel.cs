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

        public ICommand LogoutCommand { get; set; }

        public MainViewModel(INavigator navigator, IAuthenticator authenticator)
        {
            _navigator = navigator;

            LogoutCommand = new LogoutCommand(authenticator, navigator);
        }

    }
}
