﻿using Integral.WPF.Models.Enums;
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

        private Dictionary<ViewModelType, BaseViewModel> _viewModels;

        public RootViewModelFactory(IAuthenticator authenticator, IUserWebDataService userWebDataService, IStudentWebDataService studentWebDataService, IGroupWebDataService groupWebDataService)
        {
            _authenticator = authenticator;
            _userWebDataService = userWebDataService;

            _viewModels = new();
            _studentWebDataService = studentWebDataService;
            _groupWebDataService = groupWebDataService;
        }

        public BaseViewModel CreateViewModel(ViewModelType type)
        {
            if (!_viewModels.ContainsKey(type))
            {
                switch (type)
                {
                    case ViewModelType.Session:
                        SessionViewModel sessionViewModel = new(_authenticator);
                        _viewModels.Add(type, sessionViewModel);
                        break;
                    case ViewModelType.Users:
                        UsersViewModel usersViewModel = new(_userWebDataService);
                        _viewModels.Add(type, usersViewModel);
                        break;
                    case ViewModelType.Students:
                        StudentsViewModel studentsViewModel = new(_studentWebDataService);
                        _viewModels.Add(type, studentsViewModel);
                        break;
                    case ViewModelType.Groups:
                        GroupsViewModel groupsViewModel = new(_groupWebDataService);
                        _viewModels.Add(type, groupsViewModel);
                        break;
                    case ViewModelType.Meetings:
                        MeetingsViewModel meetingsViewModel = new();
                        _viewModels.Add(type, meetingsViewModel);
                        break;
                    default:
                        return new BaseViewModel();
                }
            }
            
            return _viewModels[type];
        }

        public BaseViewModel RecreateViewModel(ViewModelType type)
        {
            if (_viewModels.ContainsKey(type))
            {
                _viewModels.Remove(type);
            }

            switch (type)
            {
                case ViewModelType.Session:
                    SessionViewModel sessionViewModel = new(_authenticator);
                    _viewModels.Add(type, sessionViewModel);
                    break;
                case ViewModelType.Users:
                    UsersViewModel usersViewModel = new(_userWebDataService);
                    _viewModels.Add(type, usersViewModel);
                    break;
                case ViewModelType.Students:
                    StudentsViewModel studentsViewModel = new(_studentWebDataService);
                    _viewModels.Add(type, studentsViewModel);
                    break;
                case ViewModelType.Groups:
                    GroupsViewModel groupsViewModel = new(_groupWebDataService);
                    _viewModels.Add(type, groupsViewModel);
                    break;
                case ViewModelType.Meetings:
                    MeetingsViewModel meetingsViewModel = new();
                    _viewModels.Add(type, meetingsViewModel);
                    break;
                default:
                    return new BaseViewModel();
            }

            return _viewModels[type];
        }
    }
}
