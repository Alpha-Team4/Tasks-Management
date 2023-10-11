using TasksManagement.Exceptions;
using TasksManagement.Models;
using TasksManagement.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TasksManagement.Tests.Models
{
    [TestClass]
    public class AirplaneTests
    {
        [TestMethod]
        [DataRow(AirplaneData.CapacityTooSmall)]
        [DataRow(AirplaneData.CapacityTooBig)]
        public void Constructor_Should_ThrowException_When_PassengerCapacityOutOfBounds(int testValue)
        {
            Assert.ThrowsException<InvalidUserInputException>(() =>
                new Airplane(
                    id: 1,
                    passengerCapacity: testValue,
                    pricePerKilometer: AirplaneData.ValidPricePerKilometerValue,
                    isLowCost: true));
        }

        [TestMethod]
        [DataRow(AirplaneData.PricePerKilometerValueTooSmall)]
        [DataRow(AirplaneData.PricePerKilometerValueTooBig)]
        public void Constructor_Should_ThrowException_When_PricePerKmOutOfBounds(double testValue)
        {
            Assert.ThrowsException<InvalidUserInputException>(() =>
                new Airplane(
                    id: 1,
                    passengerCapacity: AirplaneData.ValidCapacity,
                    pricePerKilometer: testValue,
                    isLowCost: true));
        }
    }
}
