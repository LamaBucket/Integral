using Integral.WPF.Models.Enums;
using Integral.WPF.Services.ViewModelFactories;
using Integral.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.Services.Navigators
{
    public interface INavigator
    {
        BaseViewModel? CurrentViewModel { get; set; }

        ViewModelType CurrentViewModelType { get; set; }

        IRootViewModelFactory ViewModelFactory { get; }


        ICommand ChangeCurrentViewModelCommand { get; }

        ICommand RefreshCurrentViewModelCommand { get; }
    }
}
