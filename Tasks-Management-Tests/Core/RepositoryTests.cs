using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Models;
using TasksManagement.Tests.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TasksManagement.Tests.Core
{
    [TestClass]
    public class RepositoryTests
    {
        private IRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = TestHelpers.GetTestRepository();
        }

        [TestMethod]
        public void CreateAirplane_Should_AddAirplaneToList()
        {
            // Arrange
            repository.CreateAirplane(
                    AirplaneData.ValidCapacity,
                    AirplaneData.ValidPricePerKilometerValue,
                    true);

            // Act, Assert
            Assert.AreEqual(1, repository.Vehicles.Count);
        }

        [TestMethod]
        public void CreateBus_Should_AddBusToList()
        {
            // Arrange
            repository.CreateBus(
                    BusData.ValidCapacity,
                    BusData.ValidPricePerKilometerValue,
                    true);

            // Act, Assert
            Assert.AreEqual(1, repository.Vehicles.Count);
        }

        [TestMethod]
        public void CreateTrain_Should_AddTrainToList()
        {
            // Arrange
            repository.CreateTrain(
                    TrainData.ValidTrainCapacity,
                    TrainData.ValidPricePerKilometerValue,
                    TrainData.ValidCartsValue);

            // Act, Assert
            Assert.AreEqual(1, repository.Vehicles.Count);
        }

        [TestMethod]
        public void CreateJourney_Should_AddJourneyToList()
        {
            // Arrange
            repository.CreateJourney("start", "destination", 100, TestHelpers.GetTestVehicle());

            // Act, Assert
            Assert.AreEqual(1, repository.Journeys.Count);
        }

        [TestMethod]
        public void CreateTicket_Should_AddTicketToList()
        {
            // Arrange
            repository.CreateTicket(TestHelpers.GetTestJourney(), 1.60);

            // Act, Assert
            Assert.AreEqual(1, repository.Tickets.Count);
        }

        [TestMethod]
        public void FindVehicleById_Should_ThrowException_When_VehicleDoesNotExist()
        {
            Assert.ThrowsException<EntityNotFoundException>(() => repository.FindVehicleById(1));
        }

        [TestMethod]
        public void FindVehicleById_Should_ReturnVehicle_When_VehicleExists()
        {
            // Arrange
            repository.CreateAirplane(
                    AirplaneData.ValidCapacity,
                    AirplaneData.ValidPricePerKilometerValue,
                    true);

            // Act
            var vehicle = repository.FindVehicleById(1);

            // Assert
            Assert.AreEqual(1, vehicle.Id);
        }

        [TestMethod]
        public void FindJourneyById_Should_ThrowException_When_JourneyDoesNotExist()
        {
            Assert.ThrowsException<EntityNotFoundException>(() => repository.FindJourneyById(1));
        }

        [TestMethod]
        public void FindJourneyById_Should_ReturnJourney_When_JourneyExists()
        {
            // Arrange
            repository.CreateJourney("start", "destination", 100, TestHelpers.GetTestVehicle());

            // Act
            var journey = repository.FindJourneyById(1);

            // Assert
            Assert.AreEqual(1, journey.Id);
        }

        [TestMethod]
        public void FindTicketById_Should_ThrowException_When_TicketDoesNotExist()
        {
            Assert.ThrowsException<EntityNotFoundException>(() => repository.FindTicketById(1));
        }

        [TestMethod]
        public void FindTicketById_Should_ReturnTicket_When_TicketExists()
        {
            // Arrange
            repository.CreateTicket(TestHelpers.GetTestJourney(), 1.60);

            // Act
            var ticket = repository.FindTicketById(1);

            // Assert
            Assert.AreEqual(1, ticket.Id);
        }
    }
}
