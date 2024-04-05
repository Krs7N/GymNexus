namespace GymNexus.Core.Contracts;

public interface IAdminService
{
    Task<int> GetAllOrdersCountAsync();

    Task<int> GetPendingOrdersCountAsync();

    Task<int> GetConfirmedOrdersCountAsync();

    Task<int> GetCompletedOrdersCountAsync();
}