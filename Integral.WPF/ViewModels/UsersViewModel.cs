using Integral.Domain.Models;
using Integral.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.ViewModels
{
    public class UsersViewModel : BaseViewModel
    {
        private readonly IUserWebDataService _userWebDataService;

        private readonly object _usersLock = new();
        private readonly object _userLock = new();

        public UsersViewModel(IUserWebDataService userWebDataService)
        {
            _userWebDataService = userWebDataService;

            TryRefreshUsers();
        }


        private async void TryRefreshUsers()
        {
            IEnumerable<User>? _users = await _userWebDataService.GetAll();

            if (_users is null)
                return;

            lock (_usersLock)
            {
                Users = _users.ToList();
            }
        }

        private async void TryRefreshUser(int id)
        {
            User? _user = await _userWebDataService.Get(id);

            if (_user is null)
                return;

            lock (_userLock)
            {
                _selectedUser = _user;
                OnPropertyChanged(nameof(SelectedUser));
            }
        }


        private List<User>? _users;

        public List<User>? Users
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
                    TryRefreshUser(value.Id);

                
                OnPropertyChanged(nameof(SelectedUser));
            }
        }

    }
}
