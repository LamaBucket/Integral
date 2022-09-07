using Integral.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.Commands
{
    public class SelectAddressCommand : ICommand
    {
        public SessionViewModel ViewModel { get; set; }

        public SelectAddressCommand(SessionViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public void Execute(object? parameter)
        {
            if(ViewModel.SelectedAddress is not null)
            {
                ViewModel.ServerAddress = ViewModel.SelectedAddress.Item2;
            }
        }
    }
}
