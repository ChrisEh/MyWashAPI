using MyWashApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWashApi.Service.Services
{
    public interface IPickupService
    {
        Task<Pickup> GetPickupById(Guid pickupId);
        List<Pickup> GetPendingPickups();
        List<Pickup> GetCompletedPickups();
        int GetUncompletedPickupsCount();
        List<Pickup> GetPickupsOfUser(Guid userId);
        Task<Pickup> CreatePickup(Pickup newPickup, Guid userId);
        Task CompletePickup(Guid pickupId);
    }
}