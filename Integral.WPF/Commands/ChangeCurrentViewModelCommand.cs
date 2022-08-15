using Integral.WPF.Controls;
using Integral.WPF.Models.Enums;
using Integral.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.Commands
{
    public class ChangeCurrentViewModelCommand : ICommand
    {
        public ChangeCurrentViewModelCommand(NavigationBar navigBar)
        {
            _navigBar = navigBar;
        }


        private NavigationBar _navigBar;

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if(parameter is ViewModelType type)
            {
                switch (type)
                {
                    case ViewModelType.Session:
                        _navigBar.CurrentViewModel = new SessionViewModel();
                        break;
                    case ViewModelType.Users:
                        _navigBar.CurrentViewModel = new UsersViewModel();
                        break;
                    case ViewModelType.Students:
                        _navigBar.CurrentViewModel = new StudentsViewModel();
                        break;
                    case ViewModelType.Groups:
                        _navigBar.CurrentViewModel = new GroupsViewModel();
                        break;
                    case ViewModelType.Meetings:
                        _navigBar.CurrentViewModel = new MeetingsViewModel();
                        break;
                }
            }
        }
    }
}
