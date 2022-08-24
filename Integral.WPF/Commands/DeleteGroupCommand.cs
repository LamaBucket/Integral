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
    public class DeleteGroupCommand : DeleteItemCommand<Group>
    {
        public DeleteGroupCommand(ICommonWebDataService<Group> webDataService, GroupsViewModel viewModel) : base(webDataService)
        {
            ViewModel = viewModel;
        }

        public GroupsViewModel ViewModel { get; set; }

        protected override void UpdateLayout()
        {
            if(ViewModel.SelectedGroup is not null)
            {
                ViewModel.Groups?.Remove(ViewModel.SelectedGroup);
                ViewModel.SelectedGroup = null;
            }
        }
    }
}
