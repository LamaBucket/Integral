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
    public class AddAddressToConfigCommand : ICommand
    {
        public AddAddressToConfigCommand(SessionViewModel viewModel, IAppConfigManagementService appConfigManager)
        {
            ViewModel = viewModel;
            AppConfigManager = appConfigManager;
        }

        public SessionViewModel ViewModel { get; set; }

        public IAppConfigManagementService AppConfigManager { get; set; }


        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter)
        {
            ViewModel.Config.SavedServerAddresses.Add(ViewModel.AddAddressName, ViewModel.AddAddressAddress);

            ViewModel.Config = await AppConfigManager.UpdateConfig(ViewModel.Config);
        }
    }
}
