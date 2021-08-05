using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyWashApi.Dtos
{
    public class PickupWriteDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public DateTime PickupPlaced { get; set; }
        public bool IsPickupCompleted { get; set; }
    }
}
