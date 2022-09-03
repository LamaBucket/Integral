using Integral.Domain.Models;
using Integral.WPF.Exceptions;
using Integral.WPF.Models;
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
    public class CreateUserCommand : ICommand
    {
        public CreateUserCommand(IUserWebDataService userWebDataService, UsersViewModel viewModel)
        {
            UserWebDataService = userWebDataService;
            ViewModel = viewModel;
        }

        public IUserWebDataService UserWebDataService { get; set; }

        public UsersViewModel ViewModel { get; set; }



        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            ViewModel.CreateUserUsername = ViewModel.CreateUserUsername.Trim();
            ViewModel.CreateUserPassword = ViewModel.CreateUserPassword.Trim();

            if (String.IsNullOrEmpty(ViewModel.CreateUserUsername) || String.IsNullOrEmpty(ViewModel.CreateUserPassword))
                throw new ClientException(ClientErrorCodes.InvalidForm.ToString());

            User? user = await UserWebDataService.CreateUser(ViewModel.CreateUserUsername, ViewModel.CreateUserPassword);

            if(user is not null)
            {
                if(ViewModel.Users is null)
                {
                    ViewModel.Users = new() { user };
                }
                else
                {
                    ViewModel.Users.Add(user);
                }
                
                ViewModel.SelectedUser = user;
            }
        }
    }
}
