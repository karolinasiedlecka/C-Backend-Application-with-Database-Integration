using RestaurantManagement.Models;
using System.Collections.ObjectModel;
using RestaurantManagement.DAL;

namespace RestaurantManagement.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private Restaurant _selectedRestaurant;

        public ObservableCollection<Restaurant> Restaurants { get; set; }

        public Restaurant SelectedRestaurant
        {
            get => _selectedRestaurant;
            set
            {
                _selectedRestaurant = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            Restaurants = new ObservableCollection<Restaurant>();
            LoadRestaurants();
        }

        private void LoadRestaurants()
        {
            using var db = new ApplicationDbContext();
            foreach (var r in db.Restaurant)
            {
                Restaurants.Add(r);
            }
        }
    }
}
