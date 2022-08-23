using Integral.Domain.Models;
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
    public class LoadModifyStudentsDialogCommand : ICommand
    {
        public LoadModifyStudentsDialogCommand(IStudentWebDataService studentsWebDataService, GroupsViewModel viewModel)
        {
            StudentsWebDataService = studentsWebDataService;
            ViewModel = viewModel;
        }

        public IStudentWebDataService StudentsWebDataService { get; set; }

        public GroupsViewModel ViewModel { get; set; }



        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            if(ViewModel.SelectedGroup is not null)
            {
                ViewModel.ModifyStudentsAssignedStudents = new(ViewModel.SelectedGroup.Students ?? new List<Student>());
                ViewModel.ModifyStudentsNonAssignedStudents = new((await StudentsWebDataService.GetAll() ?? new List<Student>()).Except(ViewModel.ModifyStudentsAssignedStudents));
            }
        }
    }
}
