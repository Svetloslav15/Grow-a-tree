namespace Common.Interfaces
{
    using System.Threading.Tasks;

    public interface ILocationsService
    {
        public double CalculateDistanceBetweenTwoPoints(double firstLatitude, double firstLongtitute, double secondLatitude, double secondLongtitute);
    }
}
