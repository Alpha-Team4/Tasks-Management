using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TasksManagement_Tests.Helpers.TestData;
using TasksManagement.Commands.ShowCommands;
using TasksManagement.Core;
using TasksManagement.Exceptions;

namespace TasksManagement_Tests.Commands.ShowCommands
{
    [TestClass]
    public class ShowTeamsCommandTests
    {
        [TestMethod]
        public void Constructor_InitializesCommandInstance()
        {
            var repository = new Repository();

            var command = new ShowTeamsCommand(repository);

            Assert.IsInstanceOfType(command, typeof(ShowTeamsCommand));
        }

        [TestMethod]
        public void Execute_ShowsCorrect_TeamNames()
        {
            var repository = new Repository();

            repository.CreateTeam("team1");
            repository.CreateTeam("team2");


            var command = new ShowTeamsCommand(repository);
            var expectedOutput = "team1" + Environment.NewLine + "team2";

            Assert.AreEqual(expectedOutput, command.Execute());
        }

        [TestMethod]
        public void Execute_ThrowsWhen_NoTeamsInRepo()
        {
            var repository = new Repository();

            var command = new ShowTeamsCommand(repository);

            Assert.ThrowsException<EntityNotFoundException>(() => command.Execute());
        }
    }
}
