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
    public class ModifyStudentsCommand : ICommand
    {
        public ModifyStudentsCommand(IGroupWebDataService groupWebDataService, GroupsViewModel viewModel)
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
            if(ViewModel.SelectedGroup is not null && ViewModel.SelectedGroup.Students is not null && ViewModel.ModifyStudentsAssignedStudents is not null)
            {
                int groupId = ViewModel.SelectedGroup.Id;

                IEnumerable<Student> removedStudents = ViewModel.SelectedGroup.Students.Except(ViewModel.ModifyStudentsAssignedStudents);
                IEnumerable<Student> addedStudents = ViewModel.ModifyStudentsAssignedStudents.Except(ViewModel.SelectedGroup.Students);

                foreach(Student student in removedStudents)
                {
                    GroupWebDataService.UnassignStudent(groupId, student.Id);
                }

                foreach (Student student in addedStudents)
                {
                    GroupWebDataService.AssignStudent(groupId, student.Id);
                }
            }
        }
    }
}
