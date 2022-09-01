using Integral.Domain.Models;
using Integral.Domain.Services;
using Integral.WPF.Models.Enums;
using Integral.WPF.Services;
using Integral.WPF.Services.Interfaces;
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

            IIntegralHttpClientFactory clientFactory = new IntegralHttpClientFactory(new());

            clientFactory.Client.Timeout = TimeSpan.FromSeconds(5);

            Authenticator authenticator = new(clientFactory);

            IRootViewModelFactory vmFactory = new RootViewModelFactory(authenticator,
                                                                       new UserWebDataService(clientFactory),
                                                                       new StudentWebDataService(clientFactory),
                                                                       new GroupWebDataService(clientFactory),
                                                                       new MeetingWebDataService(clientFactory),
                                                                       new CSVDataTableParser(),
                                                                       new LoadExtractWebService<User>(clientFactory, new("DataManagement/Users", UriKind.Relative)),
                                                                       new LoadExtractWebService<Student>(clientFactory, new("DataManagement/Students", UriKind.Relative)),
                                                                       new LoadExtractWebService<Group>(clientFactory, new("DataManagement/Groups", UriKind.Relative)),
                                                                       new LoadExtractWebService<Meeting>(clientFactory, new("DataManagement/Meetings", UriKind.Relative)),
                                                                       new UsersDataTableParser(),
                                                                       new StudentsDataTableParser(),
                                                                       new GroupsDataTableParser(),
                                                                       new MeetingsDataTableParser());

            INavigator navigator = new Navigator(vmFactory);

            MainViewModel vm = new(navigator, authenticator);

            vm.Navigator.ChangeCurrentViewModelCommand.Execute(ViewModelType.Session);

            DataContext = vm;
        }
    }
}
