using Integral.Domain.Models;
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
    public class RemoveUserRoleCommand : ICommand
    {
        public RemoveUserRoleCommand(IUserWebDataService userWebDataService, UsersViewModel viewModel)
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
            if(ViewModel.SelectedUser is not null && ViewModel.SelectedRoleToRemove.HasValue)
            {
                User? user = await UserWebDataService.RemoveUserRole(ViewModel.SelectedUser.Id, ViewModel.SelectedRoleToRemove.Value);

                ViewModel.SelectedUser = user;
                ViewModel.SelectedRoleToAdd = null;

                ViewModel.OnPropertyChanged(nameof(ViewModel.NonUserRoles));
                ViewModel.OnPropertyChanged(nameof(ViewModel.UserRoles));
            }
        }
    }
}
