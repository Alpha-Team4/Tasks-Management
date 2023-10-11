using TasksManagement.Commands;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Tests.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;

namespace TasksManagement.Tests.Commands
{
    [TestClass]
    public class CreateTrainCommandTests
    {
        private IRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = TestHelpers.GetTestRepository();
        }

        [TestMethod]
        [DataRow(CreateTrainCommand.ExpectedNumberOfArguments - 1)]
        [DataRow(CreateTrainCommand.ExpectedNumberOfArguments + 1)]
        public void Execute_Should_ThrowException_When_ArgumentsCountDifferentThanExpected(int testValue)
        {
            // Arrange
            var commandParameters = TestHelpers.GetListWithSize(testValue);
            var command = new CreateTrainCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_PassengerCapacityNotNumber()
        {
            // Arrange
            var commandParameters = new string[] { "capacity", "2", "3" }.ToList();
            var command = new CreateTrainCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_PriceNotNumber()
        {
            // Arrange
            var commandParameters = new string[] { "30", "price", "3" }.ToList();
            var command = new CreateTrainCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_CartsCountNotNumber()
        {
            // Arrange
            var commandParameters = new string[] { "30", "2", "carts" }.ToList();
            var command = new CreateTrainCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_CreateNewAirplane_When_ValidParameters()
        {
            // Arrange
            var commandParameters = new string[] { "30", "2", "3" }.ToList();
            var command = new CreateTrainCommand(commandParameters, repository);

            // Act
            command.Execute();

            // Assert
            Assert.AreEqual(1, repository.Vehicles.Count);
        }
    }
}
