using RestaurantManagement.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace RestaurantManagement.ViewModel
{
    public class EmployeesViewModel : ViewModelBase
    {
        private Restaurant _restaurant;
        private Employee _selectedEmployee;

        public ObservableCollection<Employee> Employees { get; set; }

        public Employee SelectedEmployee
        {
            get => _selectedEmployee;
            set
            {
                _selectedEmployee = value;
                OnPropertyChanged();
            }
        }

        public EmployeesViewModel(Restaurant restaurant)
        {
            _restaurant = restaurant;
            Employees = new ObservableCollection<Employee>();
            LoadEmployees();
        }

        private void LoadEmployees()
        {
            using var db = new DAL.ApplicationDbContext();
            var emps = db.Employee.Where(e => e.RestaurantId == _restaurant.Id).ToList();
            foreach (var e in emps)
                Employees.Add(e);
        }
    }
}
