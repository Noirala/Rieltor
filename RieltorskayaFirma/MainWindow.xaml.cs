using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace RieltorskayaFirma
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private bool _PasswordVisibility = true;

        private bool PasswordVisibility
        {
            get
            {
                return _PasswordVisibility;
            }
            set
            {
                _PasswordVisibility = value;
                PasswordGrid.Visibility = PasswordVisibility ? Visibility.Visible : Visibility.Collapsed;
                ContentGrid.Visibility = PasswordVisibility ? Visibility.Collapsed : Visibility.Visible;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("Root"));
                }
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            PasswordVisibility = true;
            DataContext = this;
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            PasswordVisibility = !PasswordVisibility;
        }

        private void Apartmnets_Click(object sender, RoutedEventArgs e)
        {
            var ApartmentsList = new ApartmentsList();
            ApartmentsList.ShowDialog();
        }

        private void Clients_Click(object sender, RoutedEventArgs e)
        {
            var ClientsList = new ClientsList();
            ClientsList.ShowDialog();
        }
    }
}
