using System;
using System.Windows;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModel;
using RestaurantManagement.DAL;

namespace RestaurantManagement.Views
{
    public partial class ReservationsView : Window
    {
        private Restaurant _restaurant;

        public ReservationsView(Restaurant selectedRestaurant)
        {
            InitializeComponent();
            _restaurant = selectedRestaurant;
            this.DataContext = new ReservationsViewModel(selectedRestaurant);
        }

        private void AddReservation_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (ReservationsViewModel)this.DataContext;

            if (!string.IsNullOrWhiteSpace(CustomerNameTxt.Text) &&
                int.TryParse(GuestsTxt.Text, out int guests) &&
                ResDatePicker.SelectedDate.HasValue)
            {
                using (var db = new ApplicationDbContext())
                {
                    try
                    {
                        var newRes = new Reservation
                        {
                            CustomerName = CustomerNameTxt.Text,
                            PhoneNumber = PhoneTxt.Text,
                            NumberOfPeople = guests,
                            Date = ResDatePicker.SelectedDate.Value, 
                            Time = TimeOnly.Parse(TimeTxt.Text),    
                            RestaurantId = _restaurant.Id
                        };

                        db.Reservation.Add(newRes);
                        db.SaveChanges();

                        viewModel.Reservations.Add(newRes);

                        CustomerNameTxt.Clear();
                        PhoneTxt.Clear();
                        GuestsTxt.Clear();
                        TimeTxt.Clear();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Błąd formatu godziny! Użyj np. 14:00. " + ex.Message);
                    }
                }
            }
        }

        private void DeleteReservation_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (ReservationsViewModel)this.DataContext;
            var selected = ReservationsDataGrid.SelectedItem as Reservation;

            if (selected != null)
            {
                using (var db = new ApplicationDbContext())
                {
                    db.Reservation.Remove(selected);
                    db.SaveChanges();
                    viewModel.Reservations.Remove(selected);
                }
            }
        }
    }
}