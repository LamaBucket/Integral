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
    public class DeleteStudentCommand : DeleteItemCommand<Student>
    {
        public DeleteStudentCommand(ICommonWebDataService<Student> webDataService, StudentsViewModel viewModel) : base(webDataService)
        {
            ViewModel = viewModel;
        }

        public StudentsViewModel ViewModel { get; set; }

        protected override void UpdateLayout()
        {
            if(ViewModel.SelectedStudent is not null)
            {
                ViewModel.Students?.Remove(ViewModel.SelectedStudent);
                ViewModel.SelectedStudent = null;
            }
        }
    }
}
