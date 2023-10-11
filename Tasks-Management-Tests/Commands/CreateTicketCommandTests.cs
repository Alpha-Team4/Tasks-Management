using TasksManagement.Commands;
using TasksManagement.Core.Contracts;
using TasksManagement.Exceptions;
using TasksManagement.Tests.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using System.Linq;

namespace TasksManagement.Tests.Commands
{
    [TestClass]
    public class CreateTicketCommandTests
    {
        private IRepository repository;

        [TestInitialize]
        public void Setup()
        {
            repository = TestHelpers.GetTestRepository();
        }

        [TestMethod]
        [DataRow(CreateTicketCommand.ExpectedNumberOfArguments - 1)]
        [DataRow(CreateTicketCommand.ExpectedNumberOfArguments + 1)]
        public void Execute_Should_ThrowException_When_ArgumentsCountDifferentThanExpected(int testValue)
        {
            // Arrange
            var commandParameters = TestHelpers.GetListWithSize(testValue);
            var command = new CreateTicketCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_JourneyIdNotNumber()
        {
            // Arrange
            var commandParameters = new string[] { "journey", "1.60" }.ToList();
            var command = new CreateTicketCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_ThrowException_When_CostsNotNumber()
        {
            // Arrange
            var commandParameters = new string[] { "2", "costs" }.ToList();
            var command = new CreateTicketCommand(commandParameters, repository);

            // Act, Assert
            Assert.ThrowsException<InvalidUserInputException>(() =>
                command.Execute());
        }

        [TestMethod]
        public void Execute_Should_CreateNewTicket_When_ValidParameters()
        {
            // Arrange
            var bus = repository.CreateBus(10, 2, true);
            repository.CreateJourney("start", "destination", 10, bus);

            var commandParameters = new string[] { "1", "1.60" }.ToList();
            var command = new CreateTicketCommand(commandParameters, repository);

            // Act
            command.Execute();

            // Assert
            Assert.AreEqual(1, repository.Tickets.Count);
        }
    }
}
