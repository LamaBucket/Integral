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
    internal class SetLeaderCommand : ICommand
    {
        public SetLeaderCommand(IGroupWebDataService groupWebDataService, GroupsViewModel viewModel)
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
            if (ViewModel.SetLeaderDialogOpen)
            {
                if (!ViewModel.CreateGroupDialogOpen && ViewModel.SelectedGroup is not null && ViewModel.SetLeaderSelectedUser is not null)
                {
                    await GroupWebDataService.ChangeLeader(ViewModel.SelectedGroup.Id, ViewModel.SetLeaderSelectedUser.Id);
                }
            }
        }
    }
}
