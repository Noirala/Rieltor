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
using System.Windows.Shapes;

namespace RieltorskayaFirma
{
    /// <summary>
    /// Логика взаимодействия для ClientsList.xaml
    /// </summary>
    public partial class ClientsList : Window, INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler PropertyChanged;

        private IEnumerable<Clients> _ClientsListView;
        public IEnumerable<Clients> ClientsListView
        {
            get
            {
                return _ClientsListView;
            }
            set
            {
                _ClientsListView = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ClientsListView"));
                }
            }
        }

        public ClientsList()
        {
            InitializeComponent();
            DataContext = this;
            ClientsListView = Core.DB.Clients.ToArray();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var EditClientsWindow = new ClientsItem(ClientsDataGrid.SelectedItem as Clients);
            if (EditClientsWindow.ShowDialog() == true)
            {
                ClientsListView = Core.DB.Clients.ToArray();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var Client = ClientsDataGrid.SelectedItem as Clients;
            try
            {
                Core.DB.Clients.Remove(Client);
                Core.DB.SaveChanges();
                ClientsListView = Core.DB.Clients.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении объекта : {ex.Message}");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var NewClientWindow = new ClientsItem(new Clients());
            if (NewClientWindow.ShowDialog() == true)
            {
                ClientsListView = Core.DB.Clients.ToArray();
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }


    }
}
