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
    public class ChangeStudentNamesCommand : ICommand
    {
        public ChangeStudentNamesCommand(IStudentWebDataService studentWebDataService, StudentsViewModel viewModel)
        {
            StudentWebDataService = studentWebDataService;
            ViewModel = viewModel;
        }

        public IStudentWebDataService StudentWebDataService { get; set; }

        public StudentsViewModel ViewModel { get; set; }


        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            if(ViewModel.SelectedStudent is not null)
            {
                Student? student = await StudentWebDataService.UpdateStudent(ViewModel.SelectedStudent.Id, ViewModel.SelectedStudent.FirstName, ViewModel.SelectedStudent.SecondName, ViewModel.SelectedStudent.ThirdName);

                if(student is not null)
                {
                    ViewModel.Students?.Remove(student);
                    ViewModel.Students?.Add(student);
                    ViewModel.SelectedStudent = student;
                }
            }
        }
    }
}
