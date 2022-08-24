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
    public class UsersViewModel : BaseViewModel
    {
        public UsersViewModel(IUserWebDataService userWebDataService)
        {
            _userWebDataService = userWebDataService;

            TryRefreshUsers();

            DeleteUserCommand = new DeleteUserCommand(_userWebDataService, this);
            AddUserRoleCommand = new AddUserRoleCommand(_userWebDataService, this);
            RemoveUserRoleCommand = new RemoveUserRoleCommand(_userWebDataService, this);
            CreateUserCommand = new CreateUserCommand(_userWebDataService, this);
        }


        private readonly IUserWebDataService _userWebDataService;

        private readonly object _usersLock = new();
        private readonly object _userLock = new();

        
        public ICommand DeleteUserCommand { get; set; }

        public ICommand AddUserRoleCommand { get; set; }

        public ICommand RemoveUserRoleCommand { get; set; }

        public ICommand CreateUserCommand { get; set; }


        public IEnumerable<Role>? UserRoles => SelectedUser?.UserRoles?.Select(x => x.Role);

        public IEnumerable<Role>? NonUserRoles => UserRoles != null ? Enum.GetValues<Role>().Except(UserRoles) : Enum.GetValues<Role>();


        private async void TryRefreshUsers()
        {
            IEnumerable<User>? _users = await _userWebDataService.GetAll();

            if (_users is null)
                return;

            lock (_usersLock)
            {
                Users = new(_users);
            }
        }

        private async void TryLoadUser(int id)
        {
            User? _user = await _userWebDataService.Get(id);

            if (_user is null)
                return;

            lock (_userLock)
            {
                _selectedUser = _user;

                OnPropertyChanged(nameof(SelectedUser));
                OnPropertyChanged(nameof(UserRoles));
                OnPropertyChanged(nameof(NonUserRoles));
            }
        }


        private ObservableCollection<User>? _users;

        public ObservableCollection<User>? Users
        {
            get => _users;
            set
            {
                _users = value;
                OnPropertyChanged(nameof(Users));
            }
        }


        private User? _selectedUser;

        public User? SelectedUser
        {
            get => _selectedUser;
            set
            {
                _selectedUser = value;

                if (value != null)
                    TryLoadUser(value.Id);

                
                OnPropertyChanged(nameof(SelectedUser));
                OnPropertyChanged(nameof(UserRoles));
                OnPropertyChanged(nameof(NonUserRoles));
            }
        }


        private Role? _selectedRoleToAdd;

        public Role? SelectedRoleToAdd
        {
            get => _selectedRoleToAdd;
            set
            {
                _selectedRoleToAdd = value;
                OnPropertyChanged(nameof(SelectedRoleToAdd));
            }
        }


        private Role? _selectedRoleToRemove;

        public Role? SelectedRoleToRemove
        {
            get => _selectedRoleToRemove;
            set
            {
                _selectedRoleToRemove = value;
                OnPropertyChanged(nameof(SelectedRoleToRemove));
            }
        }



        private string _createUserUsername = String.Empty;

        public string CreateUserUsername
        {
            get => _createUserUsername;
            set
            {
                _createUserUsername = value;
                OnPropertyChanged(nameof(CreateUserUsername));
            }
        }


        private string _createUserPassword = String.Empty;

        public string CreateUserPassword
        {
            get => _createUserPassword;
            set
            {
                _createUserPassword = value;
                OnPropertyChanged(nameof(CreateUserPassword));
            }
        }


        private bool _createDialogOpen;

        public bool CreateDialogOpen
        {
            get => _createDialogOpen;
            set
            {
                _createDialogOpen = value;
                OnPropertyChanged(nameof(CreateDialogOpen));
            }
        }


    }
}
