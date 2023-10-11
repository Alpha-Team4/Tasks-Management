using TasksManagement.Exceptions;
using TasksManagement.Models;
using TasksManagement.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TasksManagement.Tests.Models
{
    [TestClass]
    public class JourneyTests
    {
        [TestMethod]
        [DataRow(JourneyData.DistanceValueTooShort)]
        [DataRow(JourneyData.DistanceValueTooLong)]
        public void Constructor_Should_Throw_When_StartLocationLengthOutOfBounds(int testValue)
        {
            Assert.ThrowsException<InvalidUserInputException>(() =>
                new Journey(
                    id: 1,
                    from: TestHelpers.GetStringWithSize(testValue),
                    to: TestHelpers.GetStringWithSize(JourneyData.ValidDestinationLength),
                    distance: JourneyData.ValidDistanceValue,
                    vehicle: TestHelpers.GetTestVehicle()));
        }

        [TestMethod]
        [DataRow(JourneyData.DestinationLengthTooShort)]
        [DataRow(JourneyData.DestinationLengthTooLong)]
        public void Constructor_Should_Throw_When_DestinationLengthOutOfBounds(int testValue)
        {
            Assert.ThrowsException<InvalidUserInputException>(() =>
                new Journey(
                    id: 1,
                    from: TestHelpers.GetStringWithSize(JourneyData.ValidStartLocationLength),
                    to: TestHelpers.GetStringWithSize(testValue),
                    distance: JourneyData.ValidDistanceValue,
                    vehicle: TestHelpers.GetTestVehicle()));
        }

        [TestMethod]
        [DataRow(JourneyData.DistanceValueTooShort)]
        [DataRow(JourneyData.DistanceValueTooLong)]
        public void Constructor_Should_Throw_When_DistanceOutOfBounds(int testValue)
        {
            Assert.ThrowsException<InvalidUserInputException>(() =>
                new Journey(
                    id: 1,
                    from: TestHelpers.GetStringWithSize(JourneyData.ValidStartLocationLength),
                    to: TestHelpers.GetStringWithSize(JourneyData.ValidDestinationLength),
                    distance: testValue,
                    vehicle: TestHelpers.GetTestVehicle()));
        }
    }
}
