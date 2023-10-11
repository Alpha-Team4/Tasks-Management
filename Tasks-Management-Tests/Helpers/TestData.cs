using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TasksManagement.Tests.Helpers
{
    public static class AirplaneData
    {
        private const int PassengerCapacityMinValue = 1;
        private const int PassengerCapacityMaxValue = 800;
        private const double PricePerKilometerMinValue = 0.10;
        private const double PricePerKilometerMaxValue = 2.50;

        public const int ValidCapacity = PassengerCapacityMinValue;
        public const int CapacityTooBig = PassengerCapacityMaxValue + 1;
        public const int CapacityTooSmall = PassengerCapacityMinValue - 1;

        public const double ValidPricePerKilometerValue = PricePerKilometerMinValue;
        public const double PricePerKilometerValueTooBig = PricePerKilometerMaxValue + 1;
        public const double PricePerKilometerValueTooSmall = PricePerKilometerMinValue - 1;
    }

    public static class BusData
    {
        private const int PassengerCapacityMinValue = 10;
        private const int PassengerCapacityMaxValue = 50;

        private const double PricePerKilometerMinValue = 0.10;
        private const double PricePerKilometerMaxValue = 2.50;

        public const int ValidCapacity = PassengerCapacityMinValue;
        public const int CapacityTooBig = PassengerCapacityMaxValue+1;
        public const int CapacityTooSmall = PassengerCapacityMinValue - 1;

        public const double ValidPricePerKilometerValue = PricePerKilometerMinValue;
        public const double PricePerKilometerValueTooBig = PricePerKilometerMaxValue + 1;
        public const double PricePerKilometerValueTooSmall = PricePerKilometerMinValue - 1;
    }

    public static class JourneyData
    {
        private const int StartLocationMinLength = 5;
        private const int StartLocationMaxLength = 25;
        private const int DestinationMinLength = 5;
        private const int DestinationMaxLength = 25;
        private const int DistanceMinValue = 5;
        private const int DistanceMaxValue = 5000;

        public const int ValidStartLocationLength = StartLocationMinLength;
        public const int StartLocationLengthTooLong = StartLocationMaxLength+1;
        public const int StartLocationLengthTooShort = StartLocationMinLength-1;

        public const int ValidDestinationLength = DestinationMinLength;
        public const int DestinationLengthTooLong = DestinationMaxLength+1;
        public const int DestinationLengthTooShort = DestinationMinLength - 1;

        public const int ValidDistanceValue = DistanceMinValue;
        public const int DistanceValueTooLong = DistanceMaxValue+1;
        public const int DistanceValueTooShort = DistanceMinValue - 1;

        public const double InvalidAdministrativeCostsValue = double.MinValue;

    }

    public static class TrainData
    {
        private const int PassengerCapacityMaxValue = 150;

        private const double PricePerKilometerMinValue = 0.10;
        private const double PricePerKilometerMaxValue = 2.50;

        private const int CartsMinValue = 1;
        private const int PassengerCapacityMinValue = 30;
        private const int CartsMaxValue = 15;

        public const int ValidTrainCapacity = PassengerCapacityMinValue;
        public const int CapacityTooBig = PassengerCapacityMaxValue + 1;
        public const int CapacityTooSmall = PassengerCapacityMinValue - 1;

        public const double ValidPricePerKilometerValue = PricePerKilometerMinValue;
        public const double PricePerKilometerValueTooBig = PricePerKilometerMaxValue + 1;
        public const double PricePerKilometerValueTooSmall = PricePerKilometerMinValue - 1;

        public const int ValidCartsValue = CartsMinValue;
        public const int CartsValueTooBig = CartsMaxValue + 1;
        public const int CartsValueTooSmall = CartsMinValue - 1;
    }

    public static class VehicleData
    {
        private const int PassengerCapacityMinValue = 1;
        private const int PassengerCapacityMaxValue = 800;

        private const double PricePerKilometerMinValue = 0.10;
        private const double PricePerKilometerMaxValue = 2.50;

        public const int ValidVehicleCapacity = PassengerCapacityMinValue;
        public const int PassengerCapacityTooBig = PassengerCapacityMaxValue + 1;
        public const int PassengerCapacityTooSmall = PassengerCapacityMinValue - 1;

        public const double ValidPricePerKilometerValue = PricePerKilometerMinValue;
        public const double PricePerKilometerValueTooBig = PricePerKilometerMaxValue + 1;
        public const double PricePerKilometerValueTooSmall = PricePerKilometerMinValue - 1;
    }
}
