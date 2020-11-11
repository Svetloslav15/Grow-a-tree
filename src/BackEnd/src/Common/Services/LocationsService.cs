namespace Common.Services
{
    using System;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Common.Interfaces;

    public class LocationsService : ILocationsService
    {
        public double CalculateDistanceBetweenTwoPoints(double firstLatitude, double firstLongtitute, double secondLatitude, double secondLongtitute)
        {
            // This whole code calculates the distance between two points matematically. It is copied and I am no sure if it works :).
            var d1 = firstLatitude * (Math.PI / 180.0);
            var num1 = firstLongtitute * (Math.PI / 180.0);
            var d2 = secondLatitude * (Math.PI / 180.0);
            var num2 = (secondLongtitute * (Math.PI / 180.0)) - num1;
            var d3 = Math.Pow(Math.Sin((d2 - d1) / 2.0), 2.0) + (Math.Cos(d1) * Math.Cos(d2) * Math.Pow(Math.Sin(num2 / 2.0), 2.0));

            return 6376500.0 * (2.0 * Math.Atan2(Math.Sqrt(d3), Math.Sqrt(1.0 - d3)));
        }
    }
}
