using System;
using System.Linq;
using System.Windows;
using RestaurantManagement.Models;
using RestaurantManagement.Models.Enums;
using RestaurantManagement.ViewModel;
using RestaurantManagement.DAL;

namespace RestaurantManagement.Views
{
    public partial class EmployeesView : Window
    {
        private Restaurant _restaurant;

        public EmployeesView(Restaurant selectedRestaurant)
        {
            InitializeComponent();
            _restaurant = selectedRestaurant;
            this.DataContext = new EmployeesViewModel(selectedRestaurant);

            PositionComboBox.ItemsSource = Enum.GetValues(typeof(EmployeeType));
            PositionComboBox.SelectedIndex = 0; 
        }

        public void AddEmployee_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (EmployeesViewModel)this.DataContext;

            if (!string.IsNullOrWhiteSpace(FirstNameTxt.Text) &&
                !string.IsNullOrWhiteSpace(LastNameTxt.Text) &&
                int.TryParse(SalaryTxt.Text, out int salary))
            {
                using (var db = new ApplicationDbContext())
                {
                    try
                    {
                        var newEmp = new Employee
                        {
                            FirstName = FirstNameTxt.Text,
                            LastName = LastNameTxt.Text,
                            Email = "brak@test.pl",
                            PhoneNumber = "000000000",
                            DateOfBirth = DateTime.Now.AddYears(-20),
                            Address = new Address("City", "Street", "00-000", "Poland"),
                            HiredOn = DateTime.Now,
                            Salary = salary,

                            EmployeeType = (EmployeeType)PositionComboBox.SelectedItem,

                            RestaurantId = _restaurant.Id
                        };

                        db.Employee.Add(newEmp);
                        db.SaveChanges();

                        viewModel.Employees.Add(newEmp);

                        EmployeesDataGrid.ItemsSource = null;
                        EmployeesDataGrid.ItemsSource = viewModel.Employees;

                        FirstNameTxt.Clear();
                        LastNameTxt.Clear();
                        SalaryTxt.Clear();

                        MessageBox.Show("Employee added successfully!");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Error: " + ex.Message);
                    }
                }
            }
            else
            {
                MessageBox.Show("Please fill in all fields correctly (Salary must be a number).");
            }
        }

        public void DeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            var viewModel = (EmployeesViewModel)this.DataContext;
            var selected = EmployeesDataGrid.SelectedItem as Employee;

            if (selected != null)
            {
                var result = MessageBox.Show($"Are you sure you want to delete {selected.FirstName}?", "Confirm", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new ApplicationDbContext())
                    {
                        try
                        {
                            var empInDb = db.Employee.FirstOrDefault(emp => emp.Id == selected.Id);
                            if (empInDb != null)
                            {
                                db.Employee.Remove(empInDb);
                                db.SaveChanges();
                            }

                            viewModel.Employees.Remove(selected);

                            EmployeesDataGrid.ItemsSource = null;
                            EmployeesDataGrid.ItemsSource = viewModel.Employees;
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Delete error: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Select an employee to delete.");
            }
        }
    }
}