using FlightManagementTask.DAL.Entities;


namespace FlightManagementTask.Application.IService
{
    public interface IAlertService
    {
        Task<List<PriceAlert>> GetUserAlertsAsync(string userId);
        Task<PriceAlert?> GetAlertById(Guid id);
        Task AddAsync(PriceAlert alert);
        Task UpdateAsync(PriceAlert alert);
        Task DeleteAsync(Guid id);
    }
}
