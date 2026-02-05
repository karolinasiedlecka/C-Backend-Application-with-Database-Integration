using RestaurantManagement.Models;
using System.Collections.ObjectModel;

namespace RestaurantManagement.ViewModel
{
    public class MenuViewModel : ViewModelBase
    {
        private Restaurant _restaurant;
        private MenuItem _selectedItem;

        public ObservableCollection<MenuItem> MenuItems { get; set; }

        public MenuItem SelectedItem
        {
            get => _selectedItem;
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }

        public MenuViewModel(Restaurant restaurant)
        {
            _restaurant = restaurant;
            MenuItems = new ObservableCollection<MenuItem>();
            LoadMenu();
        }

        private void LoadMenu()
        {
            using var db = new DAL.ApplicationDbContext();
            var restaurant = db.Restaurant
                .Where(r => r.Id == _restaurant.Id)
                .FirstOrDefault();

            if (restaurant != null)
            {
                foreach (var m in restaurant.Menu)
                    MenuItems.Add(m);
            }
        }
    }
}
