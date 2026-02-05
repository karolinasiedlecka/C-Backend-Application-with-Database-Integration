using RestaurantManagement.Models;
using RestaurantManagement.Models.Enums;

namespace RestaurantManagement.Tests
{
    public class EmployeeTests
    {
        [Fact]
        public void EmployeeCreation_ShouldSetAllProperties()
        {
            // Arrange
            var address = new Address("Poland", "00-001", "Warsaw", "Warszawska 10");
            var hiredDate = DateTime.Now;

            // Act
            var employee = new Employee
            {
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "123456789",
                Email = "john.smith@restaurant.com",
                DateOfBirth = new DateTime(1990, 5, 15),
                Address = address,
                EmployeeType = EmployeeType.Waiter,
                Salary = 3500,
                HiredOn = hiredDate
            };

            // Assert
            Assert.Equal("John", employee.FirstName);
            Assert.Equal("Smith", employee.LastName);
            Assert.Equal(EmployeeType.Waiter, employee.EmployeeType);
            Assert.Equal(3500, employee.Salary);
            Assert.Equal(hiredDate, employee.HiredOn);
            Assert.Null(employee.FiredOn);
        }

        [Fact]
        public void Employee_ShouldInheritFromPerson()
        {
            // Arrange
            var address = new Address("Poland", "00-001", "Warsaw", "Warszawska 10");
            var employee = new Employee
            {
                FirstName = "Anna",
                LastName = "Nowak",
                PhoneNumber = "987654321",
                Email = "anna@restaurant.com",
                DateOfBirth = new DateTime(1992, 8, 20),
                Address = address,
                EmployeeType = EmployeeType.Cook,
                Salary = 4500,
                HiredOn = DateTime.Now
            };

            // Assert
            Assert.IsAssignableFrom<Person>(employee);
            Assert.Equal("Anna Nowak", employee.FullName);
        }

        [Fact]
        public void Employee_ToString_ShouldReturnEmployeeInfo()
        {
            // Arrange
            var address = new Address("Poland", "00-001", "Warsaw", "Warszawska 10");
            var employee = new Employee
            {
                FirstName = "Peter",
                LastName = "Wisniewski",
                PhoneNumber = "111222333",
                Email = "peter@restaurant.com",
                DateOfBirth = new DateTime(1988, 3, 10),
                Address = address,
                EmployeeType = EmployeeType.Chef,
                Salary = 8000,
                HiredOn = DateTime.Now
            };

            // Act
            var result = employee.ToString();

            // Assert
            Assert.Contains("Peter", result);
            Assert.Contains("Wisniewski", result);
        }

        [Fact]
        public void Employee_FiredOn_CanBeSet()
        {
            // Arrange
            var address = new Address("Poland", "00-001", "Warsaw", "Warszawska 10");
            var employee = new Employee
            {
                FirstName = "Maria",
                LastName = "Kowalczyk",
                PhoneNumber = "555666777",
                Email = "maria@restaurant.com",
                DateOfBirth = new DateTime(1995, 11, 5),
                Address = address,
                EmployeeType = EmployeeType.Bartender,
                Salary = 3800,
                HiredOn = DateTime.Now
            };

            // Act
            employee.FiredOn = DateTime.Now;

            // Assert
            Assert.NotNull(employee.FiredOn);
        }

        [Fact]
        public void Employee_Salary_CanBeModified()
        {
            // Arrange
            var address = new Address("Poland", "00-001", "Warsaw", "Warszawska 10");
            var employee = new Employee
            {
                FirstName = "Thomas",
                LastName = "Nowak",
                PhoneNumber = "999888777",
                Email = "thomas@restaurant.com",
                DateOfBirth = new DateTime(1987, 6, 15),
                Address = address,
                EmployeeType = EmployeeType.Waiter,
                Salary = 3000,
                HiredOn = DateTime.Now
            };

            // Act
            employee.Salary = 3500;

            // Assert
            Assert.Equal(3500, employee.Salary);
        }

        [Fact]
        public void Employee_FullName_ShouldReturnFirstAndLastName()
        {
            // Arrange
            var address = new Address("Poland", "00-001", "Warsaw", "Warszawska 10");
            var employee = new Employee
            {
                FirstName = "Martin",
                LastName = "Jankowski",
                PhoneNumber = "333444555",
                Email = "martin@restaurant.com",
                DateOfBirth = new DateTime(1992, 10, 8),
                Address = address,
                EmployeeType = EmployeeType.Manager,
                Salary = 7000,
                HiredOn = DateTime.Now
            };

            // Assert
            Assert.Equal("Martin Jankowski", employee.FullName);
        }
    }
}
