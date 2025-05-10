using FlightManagementTask.Application.IService;
using FlightManagementTask.DAL.Entities;
using Microsoft.EntityFrameworkCore;


namespace FlightManagementTask.Application.Service
{
    public class AlertService : IAlertService
    {
        private readonly ApplicationDbContext _context;

        public AlertService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<PriceAlert>> GetUserAlertsAsync(string userId) =>
            await _context.PriceAlerts.Where(a => a.UserId == userId).ToListAsync();

        public async Task<PriceAlert?> GetAlertById(Guid id) =>
            await _context.PriceAlerts.FindAsync(id);

        public async Task AddAsync(PriceAlert alert)
        {
            _context.PriceAlerts.Add(alert);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(PriceAlert alert)
        {
            _context.PriceAlerts.Update(alert);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var alert = await _context.PriceAlerts.FindAsync(id);
            if (alert != null)
            {
                _context.PriceAlerts.Remove(alert);
                await _context.SaveChangesAsync();
            }
        }
    }

}
