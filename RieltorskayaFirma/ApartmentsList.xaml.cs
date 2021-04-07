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
    /// Логика взаимодействия для ApartmentsList.xaml
    /// </summary>
    public partial class ApartmentsList : Window, INotifyPropertyChanged
    {

        private IEnumerable<Apartments> _ApartamentsListView;

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<Apartments> ApartmentsListView
        {
            get
            {
                return _ApartamentsListView;
            }
            set
            {
                _ApartamentsListView = value;
                if (PropertyChanged != null)
                {
                    PropertyChanged(this, new PropertyChangedEventArgs("ApartmentsListView"));
                }
            }
        }

        public ApartmentsList()
        {
            InitializeComponent();
            DataContext = this;
            ApartmentsListView = Core.DB.Apartments.ToArray();
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            var EditApartmentWindow = new ApartmentItem(ApartmentsDataGrid.SelectedItem as Apartments);
            if (EditApartmentWindow.ShowDialog()==true)
            {
                ApartmentsListView = Core.DB.Apartments.ToArray();
            }
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            var Apartment = ApartmentsDataGrid.SelectedItem as Apartments;
            if (Apartment.Offers.Count > 0)
            {
                MessageBox.Show("Нельзя удалять объект недвижимости на который есть предложение");
            }

            try
            {
                Core.DB.Apartments.Remove(Apartment);
                Core.DB.SaveChanges();
                ApartmentsListView = Core.DB.Apartments.ToArray();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении объекта недвижимости: {ex.Message}");
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            var NewApartmentWindow = new ApartmentItem(new Apartments());
            if (NewApartmentWindow.ShowDialog()==true)
            {
                ApartmentsListView = Core.DB.Apartments.ToArray();
            }

        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
