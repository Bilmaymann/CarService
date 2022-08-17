using CarService.Dtos.RequestFeatures;
using CarService.Models;

namespace CarService.Repositories.Interfaces
{
    public interface ICarRepository
    {
        Task<List<Car>> GetAllAsync(CarParameters carParams);
        Task<string> GetCompaniesAsync();
        Task<string> GetModelsAsync();
        Task<string> GetColorsAsync();
    }
}
