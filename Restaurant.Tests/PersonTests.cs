using RestaurantManagement.Models;

namespace RestaurantManagement.Tests
{
    public class PersonTests
    {
        [Fact]
        public void PersonCreation_ShouldSetAllRequiredProperties()
        {
            // Arrange
            var address = new Address("Poland", "00-001", "Warsaw", "Warszawska 10");
            var dateOfBirth = new DateTime(1990, 5, 15);

            // Act
            var person = new Person
            {
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "123456789",
                Email = "john.smith@example.com",
                DateOfBirth = dateOfBirth,
                Address = address
            };

            // Assert
            Assert.Equal("John", person.FirstName);
            Assert.Equal("Smith", person.LastName);
            Assert.Equal("123456789", person.PhoneNumber);
            Assert.Equal("john.smith@example.com", person.Email);
            Assert.Equal(dateOfBirth, person.DateOfBirth);
            Assert.Equal(address, person.Address);
        }

        [Fact]
        public void FullName_ShouldReturnCombinedFirstAndLastName()
        {
            // Arrange
            var address = new Address("Poland", "00-001", "Warsaw", "Warszawska 10");
            var person = new Person
            {
                FirstName = "John",
                LastName = "Smith",
                PhoneNumber = "123456789",
                Email = "john.smith@example.com",
                DateOfBirth = new DateTime(1990, 5, 15),
                Address = address
            };

            // Act
            var fullName = person.FullName;

            // Assert
            Assert.Equal("John Smith", fullName);
        }

        [Fact]
        public void Person_ShouldAcceptValidEmail()
        {
            // Arrange
            var address = new Address("Poland", "00-001", "Warsaw", "Warszawska 10");

            // Act
            var person = new Person
            {
                FirstName = "Anna",
                LastName = "Nowak",
                PhoneNumber = "987654321",
                Email = "anna.nowak@test.com",
                DateOfBirth = new DateTime(1985, 12, 1),
                Address = address
            };

            // Assert
            Assert.Contains("@", person.Email);
            Assert.Equal("anna.nowak@test.com", person.Email);
        }

        [Fact]
        public void Person_PhoneNumberCanBeSet()
        {
            // Arrange
            var address = new Address("Poland", "00-001", "Warsaw", "Warszawska 10");
            var person = new Person
            {
                FirstName = "Peter",
                LastName = "Wisniewski",
                PhoneNumber = "111222333",
                Email = "peter@example.com",
                DateOfBirth = new DateTime(1995, 3, 20),
                Address = address
            };

            // Assert
            Assert.Equal("111222333", person.PhoneNumber);
            Assert.Equal(9, person.PhoneNumber.Length);
        }
    }
}
