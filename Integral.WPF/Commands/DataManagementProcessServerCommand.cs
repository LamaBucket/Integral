using Integral.Domain.Models;
using Integral.Domain.Services;
using Integral.WPF.Models.Enums;
using Integral.WPF.Services.Interfaces;
using Integral.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.Commands
{
    public class DataManagementProcessServerCommand : ICommand
    {
        public DataManagementViewModel ViewModel { get; set; }

        public DataManagementProcessServerCommand(DataManagementViewModel viewModel)
        {
            ViewModel = viewModel;
        }


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
                    await LoadData(ViewModel.SelectedTargetType);
                    break;
                case DataManagementActionType.Extract:
                    await ExtractData(ViewModel.SelectedTargetType);
                    break;
            }
        }



        protected async Task ExtractData(LoadExtractTargetType? targetType)
        {
            if (targetType is null)
                return;

            switch (ViewModel.SelectedTargetType)
            {
                case LoadExtractTargetType.Users:
                    await ExtractData<User>(ViewModel.UserLoadExtractService, ViewModel.UsersDataTableParser);
                    break;
                case LoadExtractTargetType.Groups:
                    await ExtractData<Group>(ViewModel.GroupLoadExtractService, ViewModel.GroupsDataTableParser);
                    break;
                case LoadExtractTargetType.Meetings:
                    await ExtractData<Meeting>(ViewModel.MeetingLoadExtractService, ViewModel.MeetingsDataTableParser);
                    break;
                case LoadExtractTargetType.Students:
                    await ExtractData<Student>(ViewModel.StudentLoadExtractService, ViewModel.StudentsDataTableParser);
                    break;
            }
        }

        protected async Task LoadData(LoadExtractTargetType? targetType)
        {
            if (targetType is null)
                return;

            switch (ViewModel.SelectedTargetType)
            {
                case LoadExtractTargetType.Users:
                    await LoadData<User>(ViewModel.UserLoadExtractService, ViewModel.UsersDataTableParser);
                    break;
                case LoadExtractTargetType.Groups:
                    await LoadData<Group>(ViewModel.GroupLoadExtractService, ViewModel.GroupsDataTableParser);
                    break;
                case LoadExtractTargetType.Meetings:
                    await LoadData<Meeting>(ViewModel.MeetingLoadExtractService, ViewModel.MeetingsDataTableParser);
                    break;
                case LoadExtractTargetType.Students:
                    await LoadData<Student>(ViewModel.StudentLoadExtractService, ViewModel.StudentsDataTableParser);
                    break;
            }
        }


        protected async Task ExtractData<T>(ILoadExtractWebService<T> dataService, IDataParserService<IEnumerable<T>, DataTable> dataParser) where T : DomainObject
        {
            IEnumerable<T>? data = await dataService.Extract();

            if (data is null)
                return;

            DataTable? dt = dataParser.Parse(data);

            ViewModel.DisplayedData = dt;
        }

        protected async Task LoadData<T>(ILoadExtractWebService<T> dataService, IDataParserService<IEnumerable<T>, DataTable> dataParser) where T : DomainObject
        {
            if(ViewModel.DisplayedData is not null)
            {
                IEnumerable<T>? data = dataParser.ParseBack(ViewModel.DisplayedData);

                if (data is null)
                    return;

                IEnumerable<T>? omittedRows = await dataService.Load(data);

                if(omittedRows is null)
                {
                    ViewModel.DisplayedData = null;
                    return;
                }
                
                ViewModel.DisplayedData = dataParser.Parse(omittedRows);
            }
        }
    }
}
