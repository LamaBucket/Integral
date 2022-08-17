using Integral.WPF.Commands;
using Integral.WPF.Services.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private INavigator _navigator;

        public INavigator Navigator
        {
            get => _navigator;
            set
            {
                _navigator = value;
                OnPropertyChanged(nameof(Navigator));
            }
        }

        public MainViewModel(INavigator navigator)
        {
            _navigator = navigator;
        }

    }
}
