using Microsoft.EntityFrameworkCore;
using MyWashApi.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWashApi.Data.Repositories
{
    public class PickupRepository : Repository<Pickup>, IPickupRepository
    {
        public PickupRepository(MyWashContext ctx) : base(ctx)
        {
        }

        public async Task<Pickup> GetPickupById(Guid pickupId)
        {
            return await _ctx.Pickups.FirstOrDefaultAsync(o => o.Id == pickupId);
        }
        public List<Pickup> GetPendingPickups()
        {
            return _ctx.Pickups.Where(o => !o.IsPickupCompleted).ToList();
        }

        public List<Pickup> GetCompletedPickups()
        {
            return _ctx.Pickups.Where(o => o.IsPickupCompleted).ToList();
        }

        public int GetUncompletedPickupsCount()
        {
            return _ctx.Pickups.Where(o => !o.IsPickupCompleted).Count();
        }

        public List<Pickup> GetPickupsOfUser(Guid userId)
        {
            return _ctx.Pickups.Where(o => o.User.Id == userId)
                .OrderByDescending(o => o.PickupPlaced).ToList();
        }

        public async Task<Pickup> CreatePickup(Pickup newPickup)
        {
            _ctx.Pickups.Add(newPickup);
            await _ctx.SaveChangesAsync();
            return newPickup;
        }

        public async Task CompletePickup(Guid pickupId)
        {
            var pickup = await _ctx.Pickups.FirstOrDefaultAsync(o => o.Id == pickupId);
            pickup.IsPickupCompleted = true;
            await _ctx.SaveChangesAsync();
        }
    }
}