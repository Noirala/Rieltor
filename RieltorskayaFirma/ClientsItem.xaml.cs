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
using System.Windows.Shapes;

namespace RieltorskayaFirma
{
    /// <summary>
    /// Логика взаимодействия для ClientsItem.xaml
    /// </summary>
    public partial class ClientsItem : Window
    {

        public Clients CurrentClient { get; set; }

        public ClientsItem(Clients Client)
        {
            InitializeComponent();
            DataContext = this;
            CurrentClient = Client;
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentClient.Id == 0)
                    Core.DB.Clients.Add(CurrentClient);
                Core.DB.SaveChanges();
                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка: {ex.Message}");
            }
        }
    }
}
