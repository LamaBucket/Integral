using Integral.Domain.Models;
using Integral.Domain.Services;
using Integral.WPF.Models.Enums;
using Integral.WPF.Services.Interfaces;
using Integral.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services.ViewModelFactories
{
    public class RootViewModelFactory : IRootViewModelFactory
    {
        private IAuthenticator _authenticator;
        private IUserWebDataService _userWebDataService;
        private IStudentWebDataService _studentWebDataService;
        private IGroupWebDataService _groupWebDataService;
        private IMeetingWebDataService _meetingWebDataService;
       
        public ILoadExtractWebService<User> UserLoadExtractService { get; set; }

        public ILoadExtractWebService<Student> StudentLoadExtractService { get; set; }

        public ILoadExtractWebService<Group> GroupLoadExtractService { get; set; }

        public ILoadExtractWebService<Meeting> MeetingLoadExtractService { get; set; }


        public IDataParserService<IEnumerable<User>, DataTable> UsersDataTableParser { get; set; }

        public IDataParserService<IEnumerable<Student>, DataTable> StudentsDataTableParser { get; set; }

        public IDataParserService<IEnumerable<Group>, DataTable> GroupsDataTableParser { get; set; }

        public IDataParserService<IEnumerable<Meeting>, DataTable> MeetingsDataTableParser { get; set; }


        public IDataParserService<string, DataTable> TextDataTableParser { get; set; }


        private Dictionary<ViewModelType, BaseViewModel> _viewModels;

        public RootViewModelFactory(IAuthenticator authenticator,
                                    IUserWebDataService userWebDataService,
                                    IStudentWebDataService studentWebDataService,
                                    IGroupWebDataService groupWebDataService,
                                    IMeetingWebDataService meetingWebDataService,
                                    IDataParserService<string, DataTable> textParser,
                                    ILoadExtractWebService<User> userLoadExtractService,
                                    ILoadExtractWebService<Student> studentLoadExtractService,
                                    ILoadExtractWebService<Group> groupLoadExtractService,
                                    ILoadExtractWebService<Meeting> meetingLoadExtractService,
                                    IDataParserService<IEnumerable<User>, DataTable> usersDataTableParser,
                                    IDataParserService<IEnumerable<Student>, DataTable> studentsDataTableParser,
                                    IDataParserService<IEnumerable<Group>, DataTable> groupsDataTableParser,
                                    IDataParserService<IEnumerable<Meeting>, DataTable> meetingsDataTableParser)
        {
            _authenticator = authenticator;
            _userWebDataService = userWebDataService;
            _studentWebDataService = studentWebDataService;
            _groupWebDataService = groupWebDataService;
            _meetingWebDataService = meetingWebDataService;
            TextDataTableParser = textParser;

            UserLoadExtractService = userLoadExtractService;
            StudentLoadExtractService = studentLoadExtractService;
            GroupLoadExtractService = groupLoadExtractService;
            MeetingLoadExtractService = meetingLoadExtractService;

            UsersDataTableParser = usersDataTableParser;
            StudentsDataTableParser = studentsDataTableParser;
            GroupsDataTableParser = groupsDataTableParser;
            MeetingsDataTableParser = meetingsDataTableParser;

            _viewModels = new Dictionary<ViewModelType, BaseViewModel>();
        }

        public void ClearCache()
        {
            _viewModels = new();
        }

        public BaseViewModel CreateViewModel(ViewModelType type)
        {
            if (!_viewModels.ContainsKey(type))
            {
                BaseViewModel? vm = GetViewModel(type);

                if (vm is null)
                    return new BaseViewModel();

                _viewModels.Add(type, vm);
            }
            
            return _viewModels[type];
        }

        public BaseViewModel RecreateViewModel(ViewModelType type)
        {
            if (_viewModels.ContainsKey(type))
            {
                _viewModels.Remove(type);
            }

            BaseViewModel? vm = GetViewModel(type);

            if (vm is null)
                return new BaseViewModel();

            _viewModels.Add(type, vm);

            return _viewModels[type];
        }

        private BaseViewModel? GetViewModel(ViewModelType type)
        {
            switch (type)
            {
                case ViewModelType.Session:
                    SessionViewModel sessionViewModel = new(_authenticator);
                    return sessionViewModel;
                case ViewModelType.Users:
                    UsersViewModel usersViewModel = new(_userWebDataService);
                    return usersViewModel;
                case ViewModelType.Students:
                    StudentsViewModel studentsViewModel = new(_studentWebDataService);
                    return studentsViewModel;
                case ViewModelType.Groups:
                    GroupsViewModel groupsViewModel = new(_groupWebDataService, _studentWebDataService);
                    return groupsViewModel;
                case ViewModelType.Meetings:
                    MeetingsViewModel meetingsViewModel = new(_groupWebDataService, _meetingWebDataService);
                    return meetingsViewModel;
                case ViewModelType.DataManipulation:
                    DataManagementViewModel dataManipulationViewModel = new(TextDataTableParser,
                                                                            UserLoadExtractService,
                                                                            StudentLoadExtractService,
                                                                            GroupLoadExtractService,
                                                                            MeetingLoadExtractService,
                                                                            UsersDataTableParser,
                                                                            StudentsDataTableParser,
                                                                            GroupsDataTableParser,
                                                                            MeetingsDataTableParser);
                    return dataManipulationViewModel;
                default:
                    return null;
            }
        }
    }
}
