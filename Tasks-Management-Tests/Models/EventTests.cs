using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TasksManagement_Tests.Helpers.TestData;
using static TasksManagement_Tests.Helpers.TestHelpers;
using TasksManagement.Exceptions;

namespace TasksManagement_Tests.Models
{
    [TestClass]
    public class EventTests
    {
        [TestMethod]
        public void Constructor_Should_AssignsCorrectDescription()
        {
            var description = "test description";
            var evt = InitializeTestEvent(description);
            Assert.AreEqual(evt.Description, description);
        }

    }
}
