using System.Windows;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModel;
using RestaurantManagement.DAL;


namespace RestaurantManagement.Views
{
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            this.DataContext = new MainViewModel();
        }

        private void AddRestaurant_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(NewRestaurantNameTextBox.Text)) return;

            using (var db = new ApplicationDbContext())
            {
              
                var newRes = new Restaurant
                {
                    Name = NewRestaurantNameTextBox.Text,
                    Address = new Address("City", "Street", "00-000", "Country"), 
                    PhoneNumber = "000",
                    Email = "a@a.pl",
                    OpeningHours = new System.TimeOnly(8, 0),
                    ClosingHours = new System.TimeOnly(22, 0),
                    Employees = new System.Collections.Generic.List<Employee>(),
                    Menu = new System.Collections.Generic.List<MenuItem>()
                };

                db.Restaurant.Add(newRes);
                db.SaveChanges();

                ((MainViewModel)this.DataContext).Restaurants.Add(newRes);
                NewRestaurantNameTextBox.Clear();
            }
        }

        private void Continue_Click(object sender, RoutedEventArgs e)
        {
            var selected = ((MainViewModel)this.DataContext).SelectedRestaurant;
            if (selected != null)
            {
                var mainWin = new RestaurantManagement.WpfApp.MainWindow(selected);
                mainWin.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Wybierz restaurację z listy!");
            }
        }
    }
}