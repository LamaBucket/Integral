using Integral.WPF.Models.Enums;
using Integral.WPF.Services.Navigators;
using Integral.WPF.Services.ViewModelFactories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.Commands
{
    public class RefreshCurrentViewModelCommand : ICommand
    {
        public RefreshCurrentViewModelCommand(INavigator navigator, IRootViewModelFactory viewModelFactory)
        {
            Navigator = navigator;
            ViewModelFactory = viewModelFactory;
        }

        public INavigator Navigator { get; set; }

        public IRootViewModelFactory ViewModelFactory { get; set; }


        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            Navigator.CurrentViewModel = ViewModelFactory.RecreateViewModel(Navigator.CurrentViewModelType);  
        }
    }
}
