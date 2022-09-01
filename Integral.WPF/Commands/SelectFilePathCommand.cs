using Integral.WPF.ViewModels;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.Commands
{
    public class SelectFilePathCommand : ICommand
    {
        public DataManagementViewModel ViewModel { get; set; }

        public SelectFilePathCommand(DataManagementViewModel viewModel)
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
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() ?? false)
            {
                ViewModel.SelectedFilePath = openFileDialog.FileName;
            }
        }
    }
}
