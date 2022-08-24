using Integral.Domain.Models;
using Integral.WPF.Services.Interfaces;
using Integral.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Commands
{
    public class DeleteUserCommand : DeleteItemCommand<User>
    {
        public DeleteUserCommand(ICommonWebDataService<User> webDataService, UsersViewModel viewModel) : base(webDataService)
        {
            ViewModel = viewModel;
        }

        public UsersViewModel ViewModel { get; set; }

        protected override void UpdateLayout()
        {
            if(ViewModel.SelectedUser is not null)
            {
                ViewModel.Users?.Remove(ViewModel.SelectedUser);
                ViewModel.SelectedUser = null;
            }
        }
    }
}
