using Integral.Domain.Models;
using Integral.Domain.Models.Enums;
using Integral.WPF.Commands;
using Integral.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.ViewModels
{
    public class MeetingsViewModel : BaseViewModel
    {
        public MeetingsViewModel(IGroupWebDataService groupWebDataService, IMeetingWebDataService meetingWebDataService)
        {
            GroupWebDataService = groupWebDataService;
            MeetingWebDataService = meetingWebDataService;

            TryLoadGroups();

            CreateMeetingCommand = new CreateMeetingCommand(meetingWebDataService, this);
            DeleteMeetingCommand = new DeleteMeetingCommand(meetingWebDataService, this);
            SaveChangesCommand = new ChangeMeetingNoteCommand(meetingWebDataService, this);
            ClearMeetingSelectionCommand = new ClearMeetingSelectionCommand(this);
        }

        private object _groupsLock = new();
        private object _meetingsLock = new();

        public static int?[] Grades => new int?[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

        public static GroupType?[] GroupTypes => new GroupType?[] { GroupType.Class, GroupType.ElectiveGroup };


        public IGroupWebDataService GroupWebDataService { get; set; }

        public IMeetingWebDataService MeetingWebDataService { get; set; }


        public ICommand CreateMeetingCommand { get; set; }

        public ICommand DeleteMeetingCommand { get; set; }

        public ICommand SaveChangesCommand { get; set; }

        public ICommand ClearMeetingSelectionCommand { get; set; }


        private int? _filterGrade;

        public int? FilterGrade
        {
            get => _filterGrade;
            set
            {
                _filterGrade = value;

                if (value is not null)
                    TryFilterGroups();

                OnPropertyChanged(nameof(FilterGrade));
            }
        }


        private GroupType? _filterGroupType;

        public GroupType? FilterGroupType
        {
            get => _filterGroupType;
            set
            {
                _filterGroupType = value;

                if (value is not null)
                    TryFilterGroups();

                OnPropertyChanged(nameof(FilterGroupType));
            }
        }


        private IEnumerable<Group>? _groups;

        public IEnumerable<Group>? Groups
        {
            get => _groups;
            set
            {
                _groups = value;

                if (value is null)
                    DisplayedGroups = null;
                else
                    DisplayedGroups = new(value);

                OnPropertyChanged(nameof(Groups));
            }
        }


        private ObservableCollection<Group>? _displayedGroups;

        public ObservableCollection<Group>? DisplayedGroups
        {
            get => _displayedGroups;
            set
            {
                _displayedGroups = value;
                OnPropertyChanged(nameof(DisplayedGroups));
            }
        }


        private Group? _selectedGroup;

        public Group? SelectedGroup
        {
            get => _selectedGroup;
            set
            {
                _selectedGroup = value;

                if (value is not null)
                    TryLoadMeetings(value.Id);

                SelectedMeeting = null;

                OnPropertyChanged(nameof(SelectedGroup));
            }
        }


        private IEnumerable<Meeting>? _meetings;

        public IEnumerable<Meeting>? Meetings
        {
            get => _meetings;
            set
            {
                _meetings = value;
                OnPropertyChanged(nameof(Meetings));
            }
        }


        private Meeting? _selectedMeeting;

        public Meeting? SelectedMeeting
        {
            get => _selectedMeeting;
            set
            {
                _selectedMeeting = value;
                OnPropertyChanged(nameof(SelectedMeeting));
            }
        }


        private string _createMeetingTheme = String.Empty;

        public string CreateMeetingTheme
        {
            get => _createMeetingTheme;
            set
            {
                _createMeetingTheme = value;
                OnPropertyChanged(nameof(CreateMeetingTheme));
            }
        }



        private string? _createMeetingNote = null;

        public string? CreateMeetingNote
        {
            get => _createMeetingNote;
            set
            {
                _createMeetingNote = value;
                OnPropertyChanged(nameof(CreateMeetingNote));
            }
        }



        private DateTime _createMeetingDate = DateTime.Now;

        public DateTime CreateMeetingDate
        {
            get => _createMeetingDate;
            set
            {
                _createMeetingDate = value;
                OnPropertyChanged(nameof(CreateMeetingDate));
            }
        }



        private async void TryLoadGroups()
        {
            IEnumerable<Group>? groups = await GroupWebDataService.GetAll();

            if (groups is null)
                return;

            lock (_groupsLock)
            {
                Groups = groups;
            }
        }


        private bool _createMeetingDialogOpen;

        public bool CreateMeetingDialogOpen
        {
            get => _createMeetingDialogOpen;
            set
            {
                _createMeetingDialogOpen = value;
                OnPropertyChanged(nameof(CreateMeetingDialogOpen));
            }
        }


        private async void TryLoadMeetings(int groupId)
        {
            IEnumerable<Meeting>? meetings = await MeetingWebDataService.GetAll(groupId);

            if (meetings is null)
                return;

            lock(_meetingsLock)
            {
                _meetings = meetings;
                OnPropertyChanged(nameof(Meetings));
            }
        }

        private async void TryFilterGroups()
        {
            if (DisplayedGroups is not null && Groups is not null)
            {
                IEnumerable<Group> filtered = new List<Group>(Groups);

                await Task.Run(() =>
                {

                    if (FilterGrade is not null)
                        filtered = filtered.Where(x => x.Grade == FilterGrade);

                    if (FilterGroupType is not null)
                        filtered = filtered.Where(x => x.GroupType == FilterGroupType);

                
                });

                DisplayedGroups = new(filtered);
            }
        }
    }
}
