using Microsoft.EntityFrameworkCore;
using RosterSoftwareApp.Api.Data;
using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Repositories;

public class EntityFrameworkNotificationRepository : INotificationRepository
{
    private readonly RosterStoreContext dbContext; //ctrl .
    public EntityFrameworkNotificationRepository(RosterStoreContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<IEnumerable<Notification>> GetAllNotificationsAsync()
    {
        return await dbContext.Notifications
        .OrderByDescending((n) => n.Id)
        .AsNoTracking().ToListAsync();
    }

    public async Task<Notification?> GetNotificationAsync(int id)
    {
        return await dbContext.Notifications.FindAsync(id);
    }

    public async Task CreateNotificationAsync(Notification n)
    {
        dbContext.Notifications.Add(n);
        await dbContext.SaveChangesAsync();
    }

    public async Task UpdateNotificationAsync(Notification updatedNotification)
    {
        dbContext.Notifications.Update(updatedNotification);
        await dbContext.SaveChangesAsync();
    }

    public async Task DeleteNotificationAsync(int id)
    {
        await dbContext.Notifications.Where(e => e.Id == id).ExecuteDeleteAsync();
    }

    // Members Repository
    public async Task<IEnumerable<Notification>> GetAllMembersNotificationsAsync()
    {
        return await dbContext.Notifications
        .Where((n) => n.Active == 1)
        .OrderByDescending((n) => n.Id)
        .AsNoTracking().ToListAsync();
    }


}