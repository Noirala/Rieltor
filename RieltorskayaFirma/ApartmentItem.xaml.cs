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
    /// Логика взаимодействия для ApartmentItem.xaml
    /// </summary>
    public partial class ApartmentItem : Window
    {
        public Apartments CurrentApartment { get; set; }
        public IEnumerable<Cities> CitiesList { get; set; }
        public IEnumerable<Streets> StreetsList { get; set; }

        public ApartmentItem(Apartments Apart)
        {
            InitializeComponent();
            DataContext = this;
            CurrentApartment = Apart;
            CitiesList = Core.DB.Cities.ToArray();
            StreetsList = Core.DB.Streets.ToArray();
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (CurrentApartment.Cities == null)
                    throw new Exception("Не выбран город");
                if (CurrentApartment.Streets == null)
                    throw new Exception("Не выбрана улица");
                if (CurrentApartment.Id == 0)
                    Core.DB.Apartments.Add(CurrentApartment);
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
