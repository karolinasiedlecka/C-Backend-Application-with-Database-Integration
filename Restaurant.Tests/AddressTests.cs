using RestaurantManagement.Models;
using RestaurantManagement.Models.Interfaces;

namespace RestaurantManagement.Tests
{
    public class AddressTests
    {
        [Fact]
        public void AddressCreation_ShouldSetAllProperties()
        {
            // Arrange & Act
            var address = new Address("Poland", "00-001", "Warsaw", "Warszawska 10");

            // Assert
            Assert.Equal("Poland", address.Country);
            Assert.Equal("00-001", address.ZipCode);
            Assert.Equal("Warsaw", address.City);
            Assert.Equal("Warszawska 10", address.Street);
        }

        [Fact]
        public void AddressToString_ShouldReturnFormattedString()
        {
            // Arrange
            var address = new Address("Poland", "00-001", "Warsaw", "Warszawska 10");

            // Act
            var result = address.ToString();

            // Assert
            Assert.Contains("Poland", result);
            Assert.Contains("00-001", result);
            Assert.Contains("Warsaw", result);
            Assert.Contains("Warszawska 10", result);
        }

        [Fact]
        public void Address_ShouldImplementIAddress()
        {
            // Arrange
            var address = new Address("Poland", "00-001", "Warsaw", "Warszawska 10");

            // Assert
            Assert.IsAssignableFrom<IAddress>(address);
        }

        [Fact]
        public void Address_PropertiesCanBeModified()
        {
            // Arrange
            var address = new Address("Poland", "00-001", "Warsaw", "Warszawska 10");

            // Act
            address.Country = "Germany";
            address.ZipCode = "10115";
            address.City = "Berlin";
            address.Street = "Unter den Linden";

            // Assert
            Assert.Equal("Germany", address.Country);
            Assert.Equal("10115", address.ZipCode);
            Assert.Equal("Berlin", address.City);
            Assert.Equal("Unter den Linden", address.Street);
        }
    }
}
