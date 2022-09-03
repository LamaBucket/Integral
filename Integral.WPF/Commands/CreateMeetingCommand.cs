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
    public class CreateMeetingCommand : ICommand
    {
        public CreateMeetingCommand(IMeetingWebDataService meetingWebDataService, MeetingsViewModel viewModel)
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
            if(ViewModel.SelectedGroup is not null)
            {
                ViewModel.CreateMeetingTheme = ViewModel.CreateMeetingTheme.Trim();
                ViewModel.CreateMeetingNote = ViewModel.CreateMeetingNote?.Trim();

                if (String.IsNullOrEmpty(ViewModel.CreateMeetingTheme))
                    throw new ClientException(ClientErrorCodes.InvalidForm.ToString());

                Meeting? meeting = await MeetingWebDataService.CreateMeeting(ViewModel.SelectedGroup.Id, ViewModel.CreateMeetingTheme, ViewModel.CreateMeetingDate, ViewModel.CreateMeetingNote);

                if(meeting is not null)
                {
                    if(ViewModel.Meetings is null)
                    {
                        ViewModel.Meetings = new() { meeting };
                    }
                    else
                    {
                        ViewModel.Meetings.Add(meeting);
                    }
                    
                    ViewModel.SelectedMeeting = meeting;
                }
            } 
        }
    }
}
