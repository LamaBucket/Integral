using Integral.WPF.Models.Enums;
using Integral.WPF.Services;
using Integral.WPF.Services.Navigators;
using Integral.WPF.Services.ViewModelFactories;
using Integral.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Integral.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            HttpClient client = new();

            client.Timeout = TimeSpan.FromSeconds(5);

            IRootViewModelFactory vmFactory = new RootViewModelFactory(new Authenticator(client), new UserWebDataService(client));

            INavigator navigator = new Navigator(vmFactory);

            MainViewModel vm = new(navigator);

            vm.Navigator.ChangeCurrentViewModelCommand.Execute(ViewModelType.Session);

            DataContext = vm;
        }
    }
}
