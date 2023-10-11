using TasksManagement.Exceptions;
using TasksManagement.Models;
using TasksManagement.Tests.Helpers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TasksManagement.Tests.Models
{
    [TestClass]
    public class TicketTests
    {
        [TestMethod]
        [DataRow(JourneyData.InvalidAdministrativeCostsValue)]
        public void Constructor_Should_ThrowException_When_CostsAreNegative(double testValue)
        {
            Assert.ThrowsException<InvalidUserInputException>(() =>
                new Ticket(
                    id: 1,
                    journey: TestHelpers.GetTestJourney(),
                    administrativeCosts: testValue));
        }
    }
}
