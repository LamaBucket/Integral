using Integral.Domain.Models;
using Integral.Domain.Services;
using Integral.WPF.Commands;
using Integral.WPF.Models.Enums;
using Integral.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.ViewModels
{
    public class DataManagementViewModel : BaseViewModel
    {
        public ILoadExtractWebService<User> UserLoadExtractService { get; set; }

        public ILoadExtractWebService<Student> StudentLoadExtractService { get; set; }

        public ILoadExtractWebService<Group> GroupLoadExtractService { get; set; }

        public ILoadExtractWebService<Meeting> MeetingLoadExtractService { get; set; }


        public IDataParserService<IEnumerable<User>, DataTable> UsersDataTableParser { get; set; }

        public IDataParserService<IEnumerable<Student>, DataTable> StudentsDataTableParser { get; set; }

        public IDataParserService<IEnumerable<Group>, DataTable> GroupsDataTableParser { get; set; }

        public IDataParserService<IEnumerable<Meeting>, DataTable> MeetingsDataTableParser { get; set; }


        public IDataParserService<string, DataTable> TextDataTableParser { get; set; }


        public DataManagementViewModel(IApplicationStateService applicationStateService, 
                                       IDataParserService<string, DataTable> textParser,
                                       ILoadExtractWebService<User> userLoadExtractService,
                                       ILoadExtractWebService<Student> studentLoadExtractService,
                                       ILoadExtractWebService<Group> groupLoadExtractService,
                                       ILoadExtractWebService<Meeting> meetingLoadExtractService,
                                       IDataParserService<IEnumerable<User>, DataTable> usersDataTableParser,
                                       IDataParserService<IEnumerable<Student>, DataTable> studentsDataTableParser,
                                       IDataParserService<IEnumerable<Group>, DataTable> groupsDataTableParser,
                                       IDataParserService<IEnumerable<Meeting>, DataTable> meetingsDataTableParser) : base(applicationStateService)
        {
            TextDataTableParser = textParser;

            UserLoadExtractService = userLoadExtractService;
            StudentLoadExtractService = studentLoadExtractService;
            GroupLoadExtractService = groupLoadExtractService;
            MeetingLoadExtractService = meetingLoadExtractService;

            UsersDataTableParser = usersDataTableParser;
            StudentsDataTableParser = studentsDataTableParser;
            GroupsDataTableParser = groupsDataTableParser;
            MeetingsDataTableParser = meetingsDataTableParser;


            SelectFilePathCommand = new SelectFilePathCommand(this);
            DataManagementProcessFileCommand = new DataManagementProcessFileCommand(this);
            DataManagementProcessServerCommand = new DataManagementProcessServerCommand(this);

            LoadExtractTargetTypes = ApplicationStateService.CurrentPermissions.AvailableTargetTypesForLoad;
        }

        public static IEnumerable<DataManagementActionType> DataManagementActionTypes => Enum.GetValues<DataManagementActionType>();


        public ICommand SelectFilePathCommand { get; set; }

        public ICommand DataManagementProcessFileCommand { get; set; }

        public ICommand DataManagementProcessServerCommand { get; set; }



        private string _selectedFilePath = String.Empty;

        public string SelectedFilePath
        {
            get => _selectedFilePath;
            set
            {
                _selectedFilePath = value;
                OnPropertyChanged(nameof(SelectedFilePath));
            }
        }



        private bool _selectFileDialogOpen;

        public bool SelectFileDialogOpen
        {
            get => _selectFileDialogOpen;
            set
            {
                _selectFileDialogOpen = value;
                OnPropertyChanged(nameof(SelectFileDialogOpen));
            }
        }



        private DataTable? _displayedData;

        public DataTable? DisplayedData
        {
            get => _displayedData;
            set
            {
                _displayedData = value;
                OnPropertyChanged(nameof(DisplayedData));
            }
        }



        private LoadExtractTargetType? _selectedTargetType;

        public LoadExtractTargetType? SelectedTargetType
        {
            get => _selectedTargetType;
            set
            {
                _selectedTargetType = value;
                OnPropertyChanged(nameof(SelectedTargetType));
            }
        }


        private DataManagementActionType _selectedActionType = DataManagementActionType.Load;

        public DataManagementActionType SelectedActionType
        {
            get => _selectedActionType;
            set
            {
                _selectedActionType = value;
                OnPropertyChanged(nameof(SelectedActionType));

                LoadExtractTargetTypes = value == DataManagementActionType.Load ? ApplicationStateService.CurrentPermissions.AvailableTargetTypesForLoad : ApplicationStateService.CurrentPermissions.AvailableTargetTypesForExtract;
            }
        }


        private IEnumerable<LoadExtractTargetType>? _loadExtractTargetTypes;

        public IEnumerable<LoadExtractTargetType>? LoadExtractTargetTypes
        {
            get => _loadExtractTargetTypes;
            set
            {
                _loadExtractTargetTypes = value;
                OnPropertyChanged(nameof(LoadExtractTargetTypes));
            }
        }


    }
}
