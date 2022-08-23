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
    public class LoadSetLeaderDialogCommand : ICommand
    {
        private IGroupWebDataService _groupWebDataService;
        
        private GroupsViewModel ViewModel;

        public LoadSetLeaderDialogCommand(IGroupWebDataService groupWebDataService, GroupsViewModel viewModel)
        {
            _groupWebDataService = groupWebDataService;
            ViewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            if (ViewModel.SetLeaderDialogOpen)
            {
                if (ViewModel.CreateGroupDialogOpen)
                {
                    ViewModel.SetLeaderUsers = await _groupWebDataService.GetUsersThatCanOwnGroup(ViewModel.CreateGroupGroupType);
                    ViewModel.SetLeaderSelectedUser = null;
                }
                else
                {
                    if(ViewModel.SelectedGroup is not null)
                    {
                        ViewModel.SetLeaderUsers = await _groupWebDataService.GetUsersThatCanOwnGroup(ViewModel.SelectedGroup.GroupType);
                        ViewModel.SetLeaderSelectedUser = ViewModel.SelectedGroup.Leader;
                    }
                }
            }
        }
    }
}
