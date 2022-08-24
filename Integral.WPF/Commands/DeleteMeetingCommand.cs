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
    public class DeleteMeetingCommand : DeleteItemCommand<Meeting>
    {
        public DeleteMeetingCommand(ICommonWebDataService<Meeting> webDataService, MeetingsViewModel viewModel) : base(webDataService)
        {
            ViewModel = viewModel;
        }

        public MeetingsViewModel ViewModel { get; set; }

        public override bool CanExecute(object? parameter)
        {
            return true;
        }

        protected override void UpdateLayout()
        {
            if(ViewModel.SelectedMeeting is not null)
            {
                ViewModel.Meetings?.Remove(ViewModel.SelectedMeeting);
                ViewModel.SelectedMeeting = null;
            }
        }
    }
}
