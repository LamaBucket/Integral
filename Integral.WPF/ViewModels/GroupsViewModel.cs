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
    public class GroupsViewModel : BaseViewModel
    {
        private IGroupWebDataService _groupWebDataService;

        private object _groupsLock = new();
        private object _groupLock = new();

        public static int[] Grades => new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11 };

        public static GroupType[] GroupTypes => Enum.GetValues<GroupType>();


        public GroupsViewModel(IGroupWebDataService groupWebDataService, IStudentWebDataService studentsWebDataService)
        {
            _groupWebDataService = groupWebDataService;

            TryRefreshGroups();

            DeleteGroupCommand = new DeleteItemCommand<Group>(_groupWebDataService);
            LoadSetLeaderDialogCommand = new LoadSetLeaderDialogCommand(_groupWebDataService, this);
            SetLeaderCommand = new SetLeaderCommand(_groupWebDataService, this);
            CreateGroupCommand = new CreateGroupCommand(_groupWebDataService, this);

            AssignStudentCommand = new AssignStudentCommand(_groupWebDataService, this);
            UnassignStudentCommand = new UnassignStudentCommand(_groupWebDataService, this);

            ModifyStudentsCommand = new ModifyStudentsCommand(_groupWebDataService, this);
            LoadModifyStudentsDialogCommand = new LoadModifyStudentsDialogCommand(studentsWebDataService, this);
        }

        public ICommand DeleteGroupCommand { get; init; }


        public ICommand LoadSetLeaderDialogCommand { get; set; }

        public ICommand SetLeaderCommand { get; set; }


        public ICommand CreateGroupCommand { get; set; }


        public ICommand AssignStudentCommand { get; set; }

        public ICommand UnassignStudentCommand { get; set; }

        public ICommand ModifyStudentsCommand { get; set; }

        public ICommand LoadModifyStudentsDialogCommand { get; set; }



        private ObservableCollection<Group>? _groups;

        public ObservableCollection<Group>? Groups
        {
            get => _groups;
            set
            {
                _groups = value;
                OnPropertyChanged(nameof(Groups));
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
                    TryLoadGroup(value.Id);

                OnPropertyChanged(nameof(SelectedGroup));
            }
        }


        private bool _createGroupDialogOpen;

        public bool CreateGroupDialogOpen
        {
            get => _createGroupDialogOpen;
            set
            {
                _createGroupDialogOpen = value;
                OnPropertyChanged(nameof(CreateGroupDialogOpen));
            }
        }


        private string _createGroupName = String.Empty;

        public string CreateGroupName
        {
            get => _createGroupName;
            set
            {
                _createGroupName = value;
                OnPropertyChanged(nameof(CreateGroupName));
            }
        }


        private int _createGroupGrade = 1;

        public int CreateGroupGrade
        {
            get => _createGroupGrade;
            set
            {
                _createGroupGrade = value;
                OnPropertyChanged(nameof(CreateGroupGrade));
            }
        }


        private GroupType _createGroupGroupType;

        public GroupType CreateGroupGroupType
        {
            get => _createGroupGroupType;
            set
            {
                _createGroupGroupType = value;
                OnPropertyChanged(nameof(CreateGroupGroupType));
            }
        }



        private bool _setLeaderDialogOpen = default(bool);

        public bool SetLeaderDialogOpen
        {
            get => _setLeaderDialogOpen;
            set
            {
                _setLeaderDialogOpen = value;
                OnPropertyChanged(nameof(SetLeaderDialogOpen));
            }
        }


        private IEnumerable<User>? _setLeaderUsers;

        public IEnumerable<User>? SetLeaderUsers
        {
            get => _setLeaderUsers;
            set
            {
                _setLeaderUsers = value;
                OnPropertyChanged(nameof(SetLeaderUsers));
            }
        }


        private User? _setLeaderSelectedUser;

        public User? SetLeaderSelectedUser
        {
            get => _setLeaderSelectedUser;
            set
            {
                _setLeaderSelectedUser = value;
                OnPropertyChanged(nameof(SetLeaderSelectedUser));
            }
        }


        private bool _modifyStudentsDialogOpen;

        public bool ModifyStudentsDialogOpen
        {
            get => _modifyStudentsDialogOpen;
            set
            {
                _modifyStudentsDialogOpen = value;
                OnPropertyChanged(nameof(ModifyStudentsDialogOpen));
            }
        }



        private ObservableCollection<Student>? _modifyStudentsAssignedStudents;

        public ObservableCollection<Student>? ModifyStudentsAssignedStudents
        {
            get => _modifyStudentsAssignedStudents;
            set
            {
                _modifyStudentsAssignedStudents = value;
                OnPropertyChanged(nameof(ModifyStudentsAssignedStudents));
            }
        }


        private ObservableCollection<Student>? _modifyStudentsNonAssignedStudents;

        public ObservableCollection<Student>? ModifyStudentsNonAssignedStudents
        {
            get => _modifyStudentsNonAssignedStudents;
            set
            {
                _modifyStudentsNonAssignedStudents = value;
                OnPropertyChanged(nameof(ModifyStudentsNonAssignedStudents));
            }
        }


        private Student? _modifyStudentsSelectedStudent;

        public Student? ModifyStudentsSelectedStudent
        {
            get => _modifyStudentsSelectedStudent;
            set
            {
                _modifyStudentsSelectedStudent = value;
                OnPropertyChanged(nameof(ModifyStudentsSelectedStudent));
            }
        }



        private async void TryRefreshGroups()
        {
            IEnumerable<Group>? groups = await _groupWebDataService.GetAll();

            if (groups is null)
                return;

            lock (_groupsLock)
            {
                Groups = new(groups);
            }
        }

        private async void TryLoadGroup(int id)
        {
            Group? group = await _groupWebDataService.Get(id);

            if (group is null)
                return;

            lock (_groupLock)
            {
                _selectedGroup = group;
                OnPropertyChanged(nameof(SelectedGroup));
            }
        }
    }
}
