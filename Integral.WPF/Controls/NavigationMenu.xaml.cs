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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Integral.WPF.Controls
{
    /// <summary>
    /// Interaction logic for NavigationMenu.xaml
    /// </summary>
    public partial class NavigationMenu : UserControl
    {


        public bool MenuVisible
        {
            get { return (bool)GetValue(MenuVisibleProperty); }
            set { SetValue(MenuVisibleProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MenuVisible.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty MenuVisibleProperty =
            DependencyProperty.Register("MenuVisible", typeof(bool), typeof(NavigationMenu), new PropertyMetadata(true));

        public NavigationMenu()
        {
            InitializeComponent();
        }
    }
}
