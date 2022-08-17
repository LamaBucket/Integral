﻿using Integral.Domain.Models.Enums;
using Integral.WPF.Commands;
using Integral.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.ViewModels
{
    public class SessionViewModel : BaseViewModel
    {

        public static IEnumerable<Role> UserRoles => Enum.GetValues<Role>();


        public SessionViewModel(IAuthenticator authenticator)
        {
            _loginCommand = new LoginCommand(this, authenticator);
        }


        private ICommand _loginCommand;

        public ICommand LoginCommand
        {
            get => _loginCommand;
            set
            {
                _loginCommand = value;
                OnPropertyChanged(nameof(LoginCommand));
            }
        }


        private string _serverAddress = String.Empty;

        public string ServerAddress
        {
            get => _serverAddress;
            set
            {
                _serverAddress = value;
                OnPropertyChanged(nameof(ServerAddress));
            }
        }


        private string _login = String.Empty;

        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }


        private string _password = String.Empty;

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }


        private Role _userRole = default;

        public Role UserRole
        {
            get => _userRole;
            set
            {
                _userRole = value;
                OnPropertyChanged(nameof(UserRole));
            }
        }

    }
}