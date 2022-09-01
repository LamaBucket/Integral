using Integral.Domain.Services;
using Integral.WPF.Models.Enums;
using Integral.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.Commands
{
    public class DataManagementProcessFileCommand : ICommand
    {
        public DataManagementProcessFileCommand(DataManagementViewModel viewModel)
        {
            ViewModel = viewModel;
        }

        public DataManagementViewModel ViewModel { get; set; }


        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            switch (ViewModel.SelectedActionType)
            {
                case DataManagementActionType.Load:
                    ProcessDataImport();
                    break;
                case DataManagementActionType.Extract:
                    await ProcessDataExport();
                    break;
            }
        }

        protected async Task ProcessDataExport()
        {
            if(ViewModel.DisplayedData is not null)
            {
                string? text = ViewModel.TextDataTableParser.ParseBack(ViewModel.DisplayedData);

                await File.WriteAllTextAsync(ViewModel.SelectedFilePath, text);
            }
        }

        protected void ProcessDataImport()
        {
            if (File.Exists(ViewModel.SelectedFilePath))
            {
                string content = File.ReadAllText(ViewModel.SelectedFilePath);

                ViewModel.DisplayedData = ViewModel.TextDataTableParser.Parse(content);
            }
        }
    }
}
