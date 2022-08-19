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
    public class AddUserRoleCommand : ICommand
    {
        public AddUserRoleCommand(IUserWebDataService userWebDataService, UsersViewModel viewModel)
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
            if(ViewModel.SelectedUser is not null)
            {
                await UserWebDataService.AddUserRole(ViewModel.SelectedUser.Id, ViewModel.SelectedRoleToAdd);
            }
        }
    }
}
