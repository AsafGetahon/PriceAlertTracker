using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementTask.Model.Dto
{
    public class PriceAlertCreateDto
    {
        public string UserId { get; set; } = string.Empty;
        public string Origin { get; set; } = string.Empty;
        public string Destination { get; set; } = string.Empty;
        public DateTime DepartureDate { get; set; }
        public decimal TargetPrice { get; set; }
        public string Currency { get; set; } = "USD";
    }
}
