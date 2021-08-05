using System;

namespace MyWashApi.Data.Models
{
    public enum PickupStatus
    {
        Requested,
        Washing,
        OutForDelivery,
        PaymentPending,
        Finished
    }

    public class Pickup
    {
        public Guid Id { get; set; }
        public User User { get; set; }
        public double PickupTotal { get; set; }
        public DateTime PickupPlaced { get; set; }
        public bool IsPickupCompleted { get; set; }
        public PickupStatus PickupStatus { get; set; }
    }
}