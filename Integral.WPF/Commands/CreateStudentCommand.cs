using Integral.Domain.Models;
using Integral.WPF.Exceptions;
using Integral.WPF.Models;
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
    public class CreateStudentCommand : ICommand
    {
        public CreateStudentCommand(IStudentWebDataService studentWebDataService, StudentsViewModel viewModel)
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
            ViewModel.CreateStudentFirstName = ViewModel.CreateStudentFirstName.Trim();
            ViewModel.CreateStudentSecondName = ViewModel.CreateStudentSecondName.Trim();
            ViewModel.CreateStudentThirdName = ViewModel.CreateStudentThirdName.Trim();


            if (String.IsNullOrEmpty(ViewModel.CreateStudentFirstName) || string.IsNullOrEmpty(ViewModel.CreateStudentSecondName) || string.IsNullOrEmpty(ViewModel.CreateStudentThirdName))
                throw new ClientException(ClientErrorCodes.InvalidForm.ToString());

            Student? student = await StudentWebDataService.CreateStudent(ViewModel.CreateStudentFirstName, ViewModel.CreateStudentSecondName, ViewModel.CreateStudentThirdName);

            if (student is not null)
            {
                if(ViewModel.Students is null)
                {
                    ViewModel.Students = new() { student };
                }
                else
                {
                    ViewModel.Students.Add(student);
                }
                ViewModel.SelectedStudent = student;
            }
                
        }
    }
}
