using Integral.WPF.ViewModels;
using Integral.WPF.Models.Enums;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System;

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
            if (ViewModel.SelectedActionType == DataManagementActionType.Load)
            {
                var openFileDialog = new OpenFileDialog();

                openFileDialog.Filter = "CSV Files (*.csv)|*.csv";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ViewModel.SelectedFilePath = openFileDialog.FileName;
                }
            }
            else if(ViewModel.SelectedActionType == DataManagementActionType.Extract)
            {
                var openFileDialog = new FolderBrowserDialog();

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    ViewModel.SelectedFilePath = openFileDialog.SelectedPath + $"IntegralDataExtract-{ViewModel.SelectedTargetType}-{DateTime.Now:yyyy:MM:dd-hh:mm}.csv";
                }
            }
        }
    }
}
