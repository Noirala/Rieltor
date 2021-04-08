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
    public partial class ApartmentsList : Window, INotifyPropertyChanged
    {

        private IEnumerable<Apartments> _ApartamentsListView;

        public event PropertyChangedEventHandler PropertyChanged;

        public IEnumerable<Apartments> ApartmentsListView
        {
            get
            {
                var res = _ApartamentsListView;
                if (_StreetFilterValue > 0)
                    res = res.Where(ai => ai.AddressStreetId == _StreetFilterValue);

                if (SearchFilter != "")
                    res = res.Where(ai => 
                       (ai.Streets.Name.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) >= 0)
                       || (ai.Cities.Name.IndexOf(SearchFilter, StringComparison.OrdinalIgnoreCase) >= 0)
                   );

                if (SortAsc) res = res.OrderBy(ai => ai.Rooms);
                else res = res.OrderByDescending(ai => ai.Rooms);

                return res;
            }
            set
            {
                _ApartamentsListView = value;
                Invalidate();
            }
        }

        public List<Streets> StreetsList { get; set; }

        public int SelectedRows
        {
            get
            {
                return ApartmentsListView.Count();
            }
        }
        public int TotalRows
        {
            get
            {
                return _ApartamentsListView.Count();
            }
        }

        private void Invalidate()
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs("ApartmentsListView"));
                PropertyChanged(this, new PropertyChangedEventArgs("SelectedRows"));
                PropertyChanged(this, new PropertyChangedEventArgs("TotalRows"));
            }
        }

        public ApartmentsList()
        {
            InitializeComponent();
            DataContext = this;
            ApartmentsListView = Core.DB.Apartments.ToArray();
            StreetsList = Core.DB.Streets.ToList();
            StreetsList.Insert(0, new Streets { Name = "Все улицы" });
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

        private int _StreetFilterValue = 0;
        public int StreetFilterValue
        {
            get
            {
                return _StreetFilterValue;
            }
            set
            {
                _StreetFilterValue = value;
                Invalidate();
            }
        }

        private void StreetFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            StreetFilterValue = (StreetFilter.SelectedItem as Streets).Id;
        }

        private string _SearchFilter = "";
        public string SearchFilter
        {
            get
            {
                return _SearchFilter;
            }
            set
            {
                _SearchFilter = value;
                Invalidate();
            }
        }

        private void SearchFilterTextBox_KeyUp(object sender, KeyEventArgs e)
        {
            SearchFilter = SearchFilterTextBox.Text;
        }

        private bool _SortAsc = true;
        public bool SortAsc
        {
            get
            {
                return _SortAsc;
            }
            set
            {
                _SortAsc = value;
                Invalidate();
            }
        }

        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            SortAsc = (sender as RadioButton).Tag.ToString() == "1";
        }
    }

    public partial class Apartments
    {
        public bool TotalAreaBigger50
        {
            get
            {
                return TotalArea > 50;
            }
        }
    }
}
