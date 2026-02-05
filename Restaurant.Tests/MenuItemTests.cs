using RestaurantManagement.Models;

namespace RestaurantManagement.Tests
{
    public class MenuItemTests
    {
        [Fact]
        public void MenuItemCreation_ShouldSetAllProperties()
        {
            // Arrange & Act
            var menuItem = new MenuItem
            {
                Name = "Pizza Margherita",
                Description = "Classic pizza with tomato sauce and mozzarella",
                Price = 25.99f
            };

            // Assert
            Assert.Equal("Pizza Margherita", menuItem.Name);
            Assert.Equal("Classic pizza with tomato sauce and mozzarella", menuItem.Description);
            Assert.Equal(25.99f, menuItem.Price);
        }

        [Fact]
        public void MenuItem_PriceShouldBePositive()
        {
            // Arrange & Act
            var menuItem = new MenuItem
            {
                Name = "Spaghetti Carbonara",
                Description = "Pasta with carbonara sauce",
                Price = 32.50f
            };

            // Assert
            Assert.True(menuItem.Price > 0);
        }

        [Fact]
        public void MenuItem_NameShouldNotBeEmpty()
        {
            // Arrange & Act
            var menuItem = new MenuItem
            {
                Name = "Burger",
                Description = "Juicy beef with vegetables",
                Price = 28.00f
            };

            // Assert
            Assert.NotNull(menuItem.Name);
            Assert.NotEmpty(menuItem.Name);
        }

        [Fact]
        public void MenuItem_DescriptionShouldNotBeEmpty()
        {
            // Arrange & Act
            var menuItem = new MenuItem
            {
                Name = "Greek Salad",
                Description = "Fresh vegetables with feta cheese",
                Price = 18.50f
            };

            // Assert
            Assert.NotNull(menuItem.Description);
            Assert.NotEmpty(menuItem.Description);
        }

        [Fact]
        public void MenuItem_PropertiesCanBeModified()
        {
            // Arrange
            var menuItem = new MenuItem
            {
                Name = "Tomato Soup",
                Description = "Traditional soup",
                Price = 12.00f
            };

            // Act
            menuItem.Name = "Creamy Tomato Soup";
            menuItem.Description = "Smooth tomato cream soup";
            menuItem.Price = 15.00f;

            // Assert
            Assert.Equal("Creamy Tomato Soup", menuItem.Name);
            Assert.Equal("Smooth tomato cream soup", menuItem.Description);
            Assert.Equal(15.00f, menuItem.Price);
        }

        [Theory]
        [InlineData("Pierogi", "Traditional Polish dumplings", 18.00f)]
        [InlineData("Pork Cutlet", "Breaded pork cutlet with potatoes", 35.00f)]
        [InlineData("Beetroot Soup", "Traditional red beet soup", 10.00f)]
        public void MenuItem_ShouldAcceptVariousValues(string name, string description, float price)
        {
            // Arrange & Act
            var menuItem = new MenuItem
            {
                Name = name,
                Description = description,
                Price = price
            };

            // Assert
            Assert.Equal(name, menuItem.Name);
            Assert.Equal(description, menuItem.Description);
            Assert.Equal(price, menuItem.Price);
        }
    }
}
