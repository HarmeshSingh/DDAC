using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaerskApplication.Models
{
    public class Booking
    {
        public int CustomerId { get; set; }

        public Customer Customer { get; set; }

        public int ShippingId { get; set; }

        public ShippingDetail ShippingDetail { get; set; }

        public int Slots { get; set; }
    }
}