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
    public class ChangeMeetingNoteCommand : ICommand
    {
        public ChangeMeetingNoteCommand(IMeetingWebDataService meetingWebDataService, MeetingsViewModel viewModel)
        {
            MeetingWebDataService = meetingWebDataService;
            ViewModel = viewModel;
        }

        public IMeetingWebDataService MeetingWebDataService { get; set; }

        public MeetingsViewModel ViewModel { get; set; }


        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            if(ViewModel.SelectedMeeting is not null)
            {
                await MeetingWebDataService.ChangeMeetingNote(ViewModel.SelectedMeeting.Id, ViewModel.SelectedMeeting.Note);
            }
        }
    }
}
