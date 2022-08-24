using Integral.WPF.Models.Enums;
using Integral.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Integral.WPF.Services.ViewModelFactories
{
    public interface IRootViewModelFactory
    {
        BaseViewModel CreateViewModel(ViewModelType type);

        BaseViewModel RecreateViewModel(ViewModelType type);

        void ClearCache();
    }
}
