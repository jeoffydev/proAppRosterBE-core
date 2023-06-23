using RosterSoftwareApp.Api.Entities;

namespace RosterSoftwareApp.Api.Repositories;

public interface INotificationRepository
{
    Task CreateNotificationAsync(Notification n);
    Task DeleteNotificationAsync(int id);
    Task<IEnumerable<Notification>> GetAllNotificationsAsync();
    Task<Notification?> GetNotificationAsync(int id);
    Task UpdateNotificationAsync(Notification updatedNotification);

    // Members IRepository
    Task<IEnumerable<Notification>> GetAllMembersNotificationsAsync();
}
