namespace DataManagers.Interfaces
{
    public interface IDataService
    {
        INews News { get; }
        IFlights Flights { get; }
        IBrands Brands { get; }
    }
}
