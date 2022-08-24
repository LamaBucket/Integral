using Integral.WPF.Models.Enums;
using Integral.WPF.Services.Interfaces;
using Integral.WPF.Services.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.Commands
{
    public class LogoutCommand : ICommand
    {
        public LogoutCommand(IAuthenticator authenticator, INavigator navigator)
        {
            Authenticator = authenticator;
            Navigator = navigator;
        }

        public IAuthenticator Authenticator { get; set; }

        public INavigator Navigator { get; set; }


        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            bool ok = await Authenticator.Logout();

            if (ok)
            {
                Navigator.ViewModelFactory.ClearCache();

                if (Navigator.ChangeCurrentViewModelCommand.CanExecute(ViewModelType.Session))
                {
                    Navigator.ChangeCurrentViewModelCommand.Execute(ViewModelType.Session);
                }
            }
        }
    }
}
