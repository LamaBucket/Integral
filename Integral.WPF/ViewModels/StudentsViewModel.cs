using Integral.Domain.Models;
using Integral.WPF.Commands;
using Integral.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.ViewModels
{
    public class StudentsViewModel : BaseViewModel
    {
        private object _studentsLock = new();
        private object _studentLock = new();

        private IStudentWebDataService _studentWebDataService;

        public StudentsViewModel(IStudentWebDataService studentWebDataService)
        {
            _studentWebDataService = studentWebDataService;

            TryRefreshStudents();

            DeleteStudentCommand = new DeleteItemCommand<Student>(_studentWebDataService);
            ChangeStudentNamesCommand = new ChangeStudentNamesCommand(_studentWebDataService, this);
            CreateStudentCommand = new CreateStudentCommand(_studentWebDataService, this);
        }

        public ICommand DeleteStudentCommand { get; init; }
        public ICommand ChangeStudentNamesCommand { get; init; }

        public ICommand CreateStudentCommand { get; set; }


        private IEnumerable<Student>? _students;

        public IEnumerable<Student>? Students
        {
            get => _students;
            set
            {
                _students = value;
                OnPropertyChanged(nameof(Students));
            }
        }


        private Student? _selectedStudent;

        public Student? SelectedStudent
        {
            get => _selectedStudent;
            set
            {
                _selectedStudent = value;

                if (value is not null)
                    TryLoadStudent(value.Id);

                OnPropertyChanged(nameof(SelectedStudent));
            }
        }

        private async void TryRefreshStudents()
        {
            IEnumerable<Student>? students = await _studentWebDataService.GetAll();

            if (students is null)
                return;

            lock (_studentsLock)
            {
                Students = students;
            }
        }

        private async void TryLoadStudent(int id)
        {
            Student? student = await _studentWebDataService.Get(id);

            if (student is null)
                return;

            lock (_studentLock)
            {
                _selectedStudent = student;
                OnPropertyChanged(nameof(SelectedStudent));
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


        private string _createStudentFirstName = String.Empty;

        public string CreateStudentFirstName
        {
            get => _createStudentFirstName;
            set
            {
                _createStudentFirstName = value;
                OnPropertyChanged(nameof(CreateStudentFirstName));
            }
        }


        private string _createStudentSecondName = String.Empty;

        public string CreateStudentSecondName
        {
            get => _createStudentSecondName;
            set
            {
                _createStudentSecondName = value;
                OnPropertyChanged(nameof(CreateStudentSecondName));
            }
        }


        private string _createStudentThirdName = String.Empty;

        public string CreateStudentThirdName
        {
            get => _createStudentThirdName;
            set
            {
                _createStudentThirdName = value;
                OnPropertyChanged(nameof(CreateStudentThirdName));
            }
        }

    }
}
