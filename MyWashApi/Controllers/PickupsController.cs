using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MyWashApi.Data.Models;
using MyWashApi.Service.Services;

namespace MyWashApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PickupsController : ControllerBase
    {
        private readonly IPickupService _pickupService;

        public PickupsController( IPickupService pickupService)
        {
            _pickupService = pickupService;
        }

        // For Admin
        // GET: api/Pickups/PendingPickups
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult PendingPickups()
        {
            return Ok(_pickupService.GetPendingPickups());
        }

        // GET: api/Pickups/CompletedPickups
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult CompletedPickups()
        {
            return Ok(_pickupService.GetCompletedPickups());
        }

        // GET: api/Pickups/PickupDetails/5
        [HttpGet("[action]/{pickupId}")]
        public IActionResult PickupDetails(string pickupId)
        {
            return Ok(_pickupService.GetCompletedPickups());
        }

        // GET: api/Pickups/UncompletedPickupsCount
        [Authorize(Roles = "Admin")]
        [HttpGet("[action]")]
        public IActionResult UncompletedPickupsCount()
        {
            return Ok(new { PendingPickups = _pickupService.GetUncompletedPickupsCount() });
        }

        // GET: api/Pickups/PickupsByUser/5
        [HttpGet("[action]/{userId}")]
        public IActionResult PickupsByUser(string userId)
        {
            return Ok(_pickupService.GetPickupsOfUser(new Guid(userId)));
        }

        // POST: api/Pickups
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Pickup pickup)
        {
            var newPickup = await _pickupService.CreatePickup(pickup);

            return Ok(new { PickupId = newPickup.Id });
        }

        // PUT: api/Pickups/CompletePickup/5
        [Authorize(Roles = "Admin")]
        [HttpPut("[action]/{pickupId}")]
        public async Task<IActionResult> CompletePickupAsync(string pickupId)
        {
            var pickup = await _pickupService.GetPickupById(new Guid(pickupId));

            if (pickup == null)
            {
                return NotFound($"No pickup found for id '{pickupId}'.");
            }
            else
            {
                await _pickupService.CompletePickup(new Guid(pickupId));

                return Ok("Pickup completed");
            }
        }
    }
}