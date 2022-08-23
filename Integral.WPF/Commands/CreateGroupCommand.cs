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
    public class CreateGroupCommand : ICommand
    {
        public CreateGroupCommand(IGroupWebDataService groupWebDataService, GroupsViewModel viewModel)
        {
            GroupWebDataService = groupWebDataService;
            ViewModel = viewModel;
        }

        public IGroupWebDataService GroupWebDataService { get; set; }

        public GroupsViewModel ViewModel { get; set; }


        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            if(ViewModel.SetLeaderSelectedUser is not null)
            {
                await GroupWebDataService.CreateGroup(ViewModel.CreateGroupName, ViewModel.CreateGroupGrade, ViewModel.SetLeaderSelectedUser.Id, ViewModel.CreateGroupGroupType);
            }
        }
    }
}
