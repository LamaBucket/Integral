using Integral.WPF.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Integral.WPF.Commands
{
    public class DeleteItemCommand<T> : ICommand
    {
        private ICommonWebDataService<T> _webDataService;

        public DeleteItemCommand(ICommonWebDataService<T> webDataService)
        {
            _webDataService = webDataService;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter)
        {
            return true;
        }

        public async void Execute(object? parameter = null)
        {
            if(parameter is int id)
            {
                await _webDataService.Delete(id);
            }
        }
    }
}
