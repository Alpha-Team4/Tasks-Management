using TasksManagement.Models.Contracts;
using TasksManagement.Models;
using System.Collections.Generic;
using System.Linq;

using TasksManagement.Core.Contracts;
using TasksManagement.Core;

namespace TasksManagement.Tests.Helpers
{
    public class TestHelpers
    {
        public static List<string> GetListWithSize(int size)
        {
            return new string[size].ToList();
        }

        public static string GetStringWithSize(int size)
        {
            return new string('x', size);
        }

        public static IRepository GetTestRepository()
        {
            return new Repository();
        }

        public static IVehicle GetTestVehicle()
        {
            return new Bus(
                    id: 1,
                    passengerCapacity: BusData.ValidCapacity,
                    pricePerKilometer: BusData.ValidPricePerKilometerValue,
                    hasFreeTv: true);
        }

        public static IJourney GetTestJourney()
        {
            return new Journey(
                    id: 1,
                    from: new string('x', JourneyData.ValidStartLocationLength),
                    to: new string('x', JourneyData.ValidDestinationLength),
                    distance: JourneyData.ValidDistanceValue,
                    vehicle: GetTestVehicle());
        }
    }
}
