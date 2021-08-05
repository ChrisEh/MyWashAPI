using MyWashApi.Data.Models;
using MyWashApi.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MyWashApi.Service.Services
{
    public class PickupService : IPickupService
    {
        private readonly IPickupRepository _pickupRepository;

        public PickupService(IPickupRepository pickupRepository)
        {
            _pickupRepository = pickupRepository;
        }

        public async Task<Pickup> GetPickupById(Guid pickupId)
        {
            return await _pickupRepository.GetPickupById(pickupId);
        }

        public List<Pickup> GetPendingPickups()
        {
            return _pickupRepository.GetPendingPickups();
        }

        public List<Pickup> GetCompletedPickups()
        {
            return _pickupRepository.GetCompletedPickups();
        }

        public int GetUncompletedPickupsCount()
        {
            return _pickupRepository.GetUncompletedPickupsCount();
        }

        public List<Pickup> GetPickupsOfUser(Guid userId)
        {
            return _pickupRepository.GetPickupsOfUser(userId);
        }

        public async Task<Pickup> CreatePickup(Pickup newPickup, Guid userId)
        {
            return await _pickupRepository.CreatePickup(newPickup, userId);
        }

        public async Task CompletePickup(Guid pickupId)
        {
            await _pickupRepository.CompletePickup(pickupId);
        }
    }
}
