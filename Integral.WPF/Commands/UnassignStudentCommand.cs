using Integral.WPF.Services.Interfaces;
using Integral.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.Commands
{
    public class UnassignStudentCommand : ICommand
    {
        public UnassignStudentCommand(IGroupWebDataService groupWebDataService, GroupsViewModel viewModel)
        {
            GroupWebDataService = groupWebDataService;
            ViewModel = viewModel;
        }

        public IGroupWebDataService GroupWebDataService { get; set; }

        public GroupsViewModel ViewModel { get; set; }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if (ViewModel.ModifyStudentsAssignedStudents is not null && ViewModel.ModifyStudentsSelectedStudent is not null && ViewModel.ModifyStudentsNonAssignedStudents is not null)
            {
                ViewModel.ModifyStudentsNonAssignedStudents.Add(ViewModel.ModifyStudentsSelectedStudent);
                ViewModel.ModifyStudentsAssignedStudents.Remove(ViewModel.ModifyStudentsSelectedStudent);
            }

        }
    }
}
