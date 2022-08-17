using Integral.WPF.Services.Interfaces;
using Integral.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.Commands
{
    public class LoginCommand : ICommand
    {
        public LoginCommand(SessionViewModel viewModel, IAuthenticator authenticator)
        {
            ViewModel = viewModel;
            Authenticator = authenticator;
        }

        public SessionViewModel ViewModel { get; set; }

        public IAuthenticator Authenticator { get; set; }



        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            throw new NotImplementedException();
        }

        public void Execute(object? parameter)
        {
            Authenticator.Login(new Uri(ViewModel.ServerAddress), ViewModel.Login, ViewModel.Password, ViewModel.UserRole);
        }
    }
}
