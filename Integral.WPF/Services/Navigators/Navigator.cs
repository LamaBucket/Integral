using Integral.WPF.Commands;
using Integral.WPF.Models.Enums;
using Integral.WPF.Services.ViewModelFactories;
using Integral.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.Services.Navigators
{
    public class Navigator : INavigator, INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;


        private BaseViewModel? _currentViewModel;

        public BaseViewModel? CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;

                PropertyChanged?.Invoke(this, new(nameof(CurrentViewModel)));
            }
        }



        private ViewModelType _currentViewModelType;

        public ViewModelType CurrentViewModelType
        {
            get => _currentViewModelType;
            set
            {
                _currentViewModelType = value;
                PropertyChanged?.Invoke(this, new(nameof(CurrentViewModelType)));
            }
        }



        public ICommand ChangeCurrentViewModelCommand { get; init; }

        public ICommand RefreshCurrentViewModelCommand { get; init; }

        public IRootViewModelFactory ViewModelFactory { get; init; }

        public Navigator(IRootViewModelFactory rootViewModelFactory)
        {
            ViewModelFactory = rootViewModelFactory;

            ChangeCurrentViewModelCommand = new ChangeCurrentViewModelCommand(this, ViewModelFactory);
            RefreshCurrentViewModelCommand = new RefreshCurrentViewModelCommand(this, ViewModelFactory);
        }

    }
}
