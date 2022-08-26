using Integral.WPF.Models.Enums;
using Integral.WPF.Services.Interfaces;
using Integral.WPF.ViewModels;
using System;
using System.Collections.Generic;
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

        private Dictionary<ViewModelType, BaseViewModel> _viewModels;

        public RootViewModelFactory(IAuthenticator authenticator, IUserWebDataService userWebDataService, IStudentWebDataService studentWebDataService, IGroupWebDataService groupWebDataService, IMeetingWebDataService meetingWebDataService)
        {
            _authenticator = authenticator;
            _userWebDataService = userWebDataService;

            _viewModels = new();
            _studentWebDataService = studentWebDataService;
            _groupWebDataService = groupWebDataService;
            _meetingWebDataService = meetingWebDataService;
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
                    DataManipulationViewModel dataManipulationViewModel = new();
                    return dataManipulationViewModel;
                default:
                    return null;
            }
        }
    }
}
