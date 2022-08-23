using Integral.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.Commands
{
    public class ClearMeetingSelectionCommand : ICommand
    {
        public MeetingsViewModel ViewModel { get; set; }

        public ClearMeetingSelectionCommand(MeetingsViewModel viewModel)
        {
            ViewModel = viewModel;
        }


        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            ViewModel.FilterGrade = null;
            ViewModel.FilterGroupType = null;

            ViewModel.DisplayedGroups = ViewModel.Groups is null ? null : new(ViewModel.Groups);
        }
    }
}
