using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using RestaurantManagement.Models;
using RestaurantManagement.Views;

namespace RestaurantManagement.WpfApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Restaurant _selectedRestaurant;

        public MainWindow(Restaurant restaurant)
        {
            InitializeComponent();
            _selectedRestaurant = restaurant;

            this.Title = $"Management: {_selectedRestaurant.Name}";
        }

        private void OpenEmployees_Click(object sender, RoutedEventArgs e)
        {
            var view = new EmployeesView(_selectedRestaurant);
            view.Show();
        }
        private void ChangeRestaurant_Click(object sender, RoutedEventArgs e)
        {
            Window1 selectionWin = new Window1();
            selectionWin.Show();
            this.Close();
        }

        private void OpenMenu_Click(object sender, RoutedEventArgs e)
        {
            var view = new MenuView(_selectedRestaurant);
            view.Show();
        }

        private void OpenReservations_Click(object sender, RoutedEventArgs e)
        {
            var view = new ReservationsView(_selectedRestaurant);
            view.Show();
        }
    }
}