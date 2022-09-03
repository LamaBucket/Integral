using Integral.WPF.Commands;
using Integral.WPF.Exceptions;
using Integral.WPF.Services.Interfaces;
using Integral.WPF.Services.Navigators;
using System;
using System.Collections.Generic;
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
            if(e.Exception is WebRequestException _ex)
            {
                string message = _ex.Message;

                if(Int32.TryParse(_ex.Message, out int code))
                {
                    //Parse Code To message Logic
                }

                ExceptionContent = message;
                ExceptionDialogHeader = _ex.StatusCode.ToString();

                ExceptionDialogShown = true;
            }
            else
            {
                ExceptionContent = e.Exception.GetBaseException().Message;
                ExceptionDialogHeader = "Exception";

                ExceptionDialogShown = true;
            }

            e.Handled = true;
        }
    }
}
