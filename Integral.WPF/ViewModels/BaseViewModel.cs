using Integral.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.ViewModels
{
    public class BaseViewModel : INotifyPropertyChanged
    {
        public IApplicationStateService ApplicationStateService { get; set; }

        public BaseViewModel(IApplicationStateService applicationStateService)
        {
            ApplicationStateService = applicationStateService;
        }


        public event PropertyChangedEventHandler? PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string? propName = null) => PropertyChanged?.Invoke(this, new(propName));
    }
}
