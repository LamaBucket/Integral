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
    /// Interaction logic for IntegralModal.xaml
    /// </summary>
    public partial class IntegralModal : UserControl
    {
        public object ModalContent
        {
            get { return (object)GetValue(ModalContentProperty); }
            set { SetValue(ModalContentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ModalContent.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModalContentProperty =
            DependencyProperty.Register("ModalContent", typeof(object), typeof(IntegralModal), new PropertyMetadata());


        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(IntegralModal), new PropertyMetadata(false));



        public string ModalHeaderString
        {
            get { return (string)GetValue(ModalHeaderStringProperty); }
            set { SetValue(ModalHeaderStringProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ModalHeaderString.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ModalHeaderStringProperty =
            DependencyProperty.Register("ModalHeaderString", typeof(string), typeof(IntegralModal), new PropertyMetadata());


        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Command.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.Register("Command", typeof(ICommand), typeof(IntegralModal), new PropertyMetadata());

        public object CommandParameter
        {
            get { return (object)GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        // Using a DependencyProperty as the backing store for CommandParameter.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.Register("CommandParameter", typeof(object), typeof(IntegralModal), new PropertyMetadata());



        public IntegralModal()
        {
            InitializeComponent();
        }

        private void btnCloseModal_Click(object sender, RoutedEventArgs e)
        {
            IsOpen = false;

            e.Handled = true;
        }

        private void btnOkModal_Click(object sender, RoutedEventArgs e)
        {
            if (Command.CanExecute(CommandParameter))
                Command.Execute(CommandParameter);

            IsOpen = false;
        }

        private void btnCancelModal_Click(object sender, RoutedEventArgs e)
        {
            IsOpen = false;

            e.Handled = true;
        }
    }
}
