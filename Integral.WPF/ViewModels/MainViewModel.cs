using Integral.WPF.Commands;
using Integral.WPF.Exceptions;
using Integral.WPF.Services.Interfaces;
using Integral.WPF.Services.Navigators;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
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


        private IAuthenticator _authenticator;

        public IAuthenticator Authenticator
        {
            get => _authenticator;
            set
            {
                _authenticator = value;
                OnPropertyChanged(nameof(Authenticator));
            }
        }


        private bool _navigationMenuVisible;

        public bool NavigationMenuVisible
        {
            get => _navigationMenuVisible;
            set
            {
                _navigationMenuVisible = value;
                OnPropertyChanged(nameof(NavigationMenuVisible));
            }
        }



        private bool _excetionDialogShown;

        public bool ExceptionDialogShown
        {
            get => _excetionDialogShown;
            set
            {
                _excetionDialogShown = value;
                OnPropertyChanged(nameof(ExceptionDialogShown));
            }
        }


        private string _excetionContent = String.Empty;

        public string ExceptionContent
        {
            get => _excetionContent;
            set
            {
                _excetionContent = value;
                OnPropertyChanged(nameof(ExceptionContent));
            }
        }


        private string _exceptionDialogHeader = String.Empty;

        public string ExceptionDialogHeader
        {
            get => _exceptionDialogHeader;
            set
            {
                _exceptionDialogHeader = value;
                OnPropertyChanged(nameof(ExceptionDialogHeader));
            }
        }


        public ICommand LogoutCommand { get; set; }

        public MainViewModel(INavigator navigator, IAuthenticator authenticator)
        {
            _navigator = navigator;
            _authenticator = authenticator;

            LogoutCommand = new LogoutCommand(authenticator, navigator, this);

            Application.Current.DispatcherUnhandledException += Current_DispatcherUnhandledException;
        }

        private void Current_DispatcherUnhandledException(object sender, System.Windows.Threading.DispatcherUnhandledExceptionEventArgs e)
        {
            Exception _ex = e.Exception;

            TryParseErrorCode(ref _ex);

            ExceptionContent = _ex.Message;
            ExceptionDialogHeader = "Exception";

            if (e.Exception is WebRequestException _webEx)
            {
                ExceptionDialogHeader = _webEx.StatusCode.ToString();
            }

            ExceptionDialogShown = true;

            e.Handled = true;
        }

        private void TryParseErrorCode(ref Exception e)
        {
            if(Int32.TryParse(e.Message, out int code))
            {
                
                string text = Properties.Resources.ErrorStrings;

                var dict = JsonConvert.DeserializeObject<Dictionary<int, string>>(text);

                if(dict is not null && dict.ContainsKey(code))
                {
                    e = new(dict[code]);
                }
                
            }

            e = e.GetBaseException();
        }
    }
}
