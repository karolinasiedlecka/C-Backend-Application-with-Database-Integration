using RestaurantManagement.Models;
using RestaurantNetwork.Model;
using System;
using Xunit;

namespace RestaurantManagement.Tests
{
    public class ReservationTests
    {
        [Fact]
        public void Reservation_ShouldHaveValidCustomerName()
        {
            // Arrange & Act
            var reservation = new Reservation
            {
                CustomerName = "John",
                PhoneNumber = "123456789",
                NumberOfPeople = 1,
                Date = DateTime.Now,
                Time = new TimeOnly(18, 0)
            };

            // Assert
            Assert.NotNull(reservation.CustomerName);
            Assert.NotEmpty(reservation.CustomerName);
            Assert.True(reservation.CustomerName.Length > 2);
        }

        [Fact]
        public void ReservationCreation_ShouldSetAllProperties()
        {
            // Arrange
            var date = new DateTime(2025, 12, 15);
            var time = new TimeOnly(18, 30);

            // Act
            var reservation = new Reservation
            {
                CustomerName = "John Smith",
                NumberOfPeople = 4,
                PhoneNumber = "123456789",
                Date = date,
                Time = time
            };

            // Assert
            Assert.Equal("John Smith", reservation.CustomerName);
            Assert.Equal(4, reservation.NumberOfPeople);
            Assert.Equal("123456789", reservation.PhoneNumber);
            Assert.Equal(date, reservation.Date);
            Assert.Equal(time, reservation.Time);
        }

        [Fact]
        public void Reservation_ToString_ShouldContainBasicInformation()
        {
            // Arrange
            var reservation = new Reservation
            {
                CustomerName = "Anna Nowak",
                NumberOfPeople = 2,
                PhoneNumber = "987654321",
                Date = new DateTime(2025, 12, 20),
                Time = new TimeOnly(19, 0)
            };

            // Act
            var result = reservation.ToString();

            // Assert
            Assert.Contains("Anna Nowak", result);
            Assert.Contains("2", result);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(5)]
        public void Reservation_ShouldAcceptVariousNumberOfPeople(int numberOfPeople)
        {
            // Arrange & Act
            var reservation = new Reservation
            {
                CustomerName = "Test Customer",
                NumberOfPeople = numberOfPeople,
                PhoneNumber = "111222333",
                Date = DateTime.Now.AddDays(1),
                Time = new TimeOnly(18, 0)
            };

            // Assert
            Assert.Equal(numberOfPeople, reservation.NumberOfPeople);
        }

        [Fact]
        public void Reservation_PhoneNumberShouldHaveCorrectLength()
        {
            // Arrange
            var reservation = new Reservation
            {
                CustomerName = "Peter Wisniewski",
                NumberOfPeople = 3,
                PhoneNumber = "555666777",
                Date = new DateTime(2025, 12, 25),
                Time = new TimeOnly(20, 0)
            };

            // Assert
            Assert.Equal(9, reservation.PhoneNumber.Length);
        }

        [Fact]
        public void Reservation_DateCanBeInTheFuture()
        {
            // Arrange
            var futureDate = DateTime.Now.AddDays(7);

            // Act
            var reservation = new Reservation
            {
                CustomerName = "Maria Kowalczyk",
                NumberOfPeople = 6,
                PhoneNumber = "444555666",
                Date = futureDate,
                Time = new TimeOnly(19, 30)
            };

            // Assert
            Assert.True(reservation.Date > DateTime.Now.Date);
        }

        [Fact]
        public void Reservation_PropertiesCanBeModified()
        {
            // Arrange
            var reservation = new Reservation
            {
                CustomerName = "Original Name",
                NumberOfPeople = 2,
                PhoneNumber = "111111111",
                Date = DateTime.Now,
                Time = new TimeOnly(18, 0)
            };

            // Act
            reservation.CustomerName = "Updated Name";
            reservation.NumberOfPeople = 4;
            reservation.PhoneNumber = "999999999";
            reservation.Date = DateTime.Now.AddDays(1);
            reservation.Time = new TimeOnly(20, 0);

            // Assert
            Assert.Equal("Updated Name", reservation.CustomerName);
            Assert.Equal(4, reservation.NumberOfPeople);
            Assert.Equal("999999999", reservation.PhoneNumber);
            Assert.Equal(new TimeOnly(20, 0), reservation.Time);
        }
    }
}
