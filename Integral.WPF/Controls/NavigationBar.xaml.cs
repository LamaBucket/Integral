using Integral.WPF.Commands;
using Integral.WPF.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Integral.WPF.Controls
{
    /// <summary>
    /// Interaction logic for NavigationBar.xaml
    /// </summary>
    public partial class NavigationBar : UserControl
    {
        public BaseViewModel CurrentViewModel
        {
            get { return (BaseViewModel)GetValue(CurrentViewModelProperty); }
            set { SetValue(CurrentViewModelProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CurrentViewModel.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CurrentViewModelProperty =
            DependencyProperty.Register("CurrentViewModel", typeof(BaseViewModel), typeof(NavigationBar), new PropertyMetadata());



        public ICommand ChangeCurrentViewModelCommand
        {
            get { return (ICommand)GetValue(ChangeCurrentViewModelCommandProperty); }
            set { SetValue(ChangeCurrentViewModelCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ChangeCurrentViewModelCommand.  This enables animation, styling, binding, etc...
        private static readonly DependencyProperty ChangeCurrentViewModelCommandProperty =
            DependencyProperty.Register("ChangeCurrentViewModelCommand", typeof(ICommand), typeof(NavigationBar), new PropertyMetadata());


        public NavigationBar()
        {
            ChangeCurrentViewModelCommand = new ChangeCurrentViewModelCommand(this);

            InitializeComponent();
        }
    }
}
