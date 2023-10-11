using TasksManagement.Commands;
using TasksManagement.Core;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Tests.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;

namespace TasksManagement.Tests.Commands
{
    [TestClass]
    public class CreateJourneyCommandTests
    {
        private IRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = TestHelpers.GetTestRepository();
        }

        [TestMethod]
        [DataRow(CreateJourneyCommand.ExpectedNumberOfArguments - 1)]
        [DataRow(CreateJourneyCommand.ExpectedNumberOfArguments + 1)]
        public void Execute_Should_ThrowException_When_ArgumentsCountDifferentThanExpected(int testValue)
        {
            // Arrange
            var commandParameters = TestHelpers.GetListWithSize(testValue);
            var command = new CreateJourneyCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_DistanceNotNumber()
        {
            // Arrange
            var commandParameters = new string[] { "start", "destination", "distance", "1" }.ToList();
            var command = new CreateJourneyCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_VehicleIdNotNumber()
        {
            // Arrange
            var commandParameters = new string[] { "start", "destination", "10", "index" }.ToList();
            var command = new CreateJourneyCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_CreateNewJourney_When_ValidParameters()
        {
            // Arrange
            repository.CreateBus(10, 2, true);

            var commandParameters = new string[] { "start", "destination", "10", "1" }.ToList();
            var command = new CreateJourneyCommand(commandParameters, repository);

            // Act
            command.Execute();

            // Assert
            Assert.AreEqual(1, repository.Journeys.Count);
        }
    }
}
