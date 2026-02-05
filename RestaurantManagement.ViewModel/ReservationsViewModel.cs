using RestaurantManagement.DAL;
using RestaurantManagement.Models;
using RestaurantManagement.ViewModel;
using System.Collections.ObjectModel;
using System.Linq;

namespace RestaurantManagement.ViewModel
{
    public class ReservationsViewModel : ViewModelBase
    {
        private Restaurant _restaurant;
        private Reservation _selectedReservation;

        public ObservableCollection<Reservation> Reservations { get; set; }

        public Reservation SelectedReservation
        {
            get => _selectedReservation;
            set
            {
                _selectedReservation = value;
                OnPropertyChanged();
            }
        }

        public ReservationsViewModel(Restaurant restaurant)
        {
            _restaurant = restaurant;
            Reservations = new ObservableCollection<Reservation>();
            LoadReservations();
        }

        private void LoadReservations()
        {
            using var db = new ApplicationDbContext();

            var resList = db.Reservation
                .Where(r => r.RestaurantId == _restaurant.Id)
                .OrderBy(r => r.Date)
                .ThenBy(r => r.Time)
                .ToList();

            Reservations.Clear();
            foreach (var r in resList)
            {
                Reservations.Add(r);
            }
        }

        public void AddReservation(string customerName, int people, string phone, DateTime date, TimeOnly time)
        {
            using var db = new ApplicationDbContext();

            var reservation = new Reservation
            {
                CustomerName = customerName,
                NumberOfPeople = people,
                PhoneNumber = phone,
                Date = date,
                Time = time,
                RestaurantId = _restaurant.Id
            };

            db.Reservation.Add(reservation);
            db.SaveChanges();

            Reservations.Add(reservation); // odświeżenie ObservableCollection
        }

        public void RemoveReservation(Reservation reservation)
        {
            if (reservation == null) return;

            using var db = new ApplicationDbContext();

            var res = db.Reservation.FirstOrDefault(r => r.Id == reservation.Id);
            if (res != null)
            {
                db.Reservation.Remove(res);
                db.SaveChanges();
                Reservations.Remove(reservation);
            }
        }
    }
}
