using RestaurantManagement.Models;
using System.Collections.ObjectModel;

namespace RestaurantManagement.ViewModel
{
    public class RestaurantSelectionViewModel : ViewModelBase
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

        public RestaurantSelectionViewModel()
        {
            Restaurants = new ObservableCollection<Restaurant>();
            LoadRestaurants();
        }

        private void LoadRestaurants()
        {
            using var db = new DAL.ApplicationDbContext();
            foreach (var r in db.Restaurant)
            {
                Restaurants.Add(r);
            }
        }
    }
}
