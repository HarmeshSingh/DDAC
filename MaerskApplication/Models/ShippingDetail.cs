using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaerskApplication.Models
{
    public class ShippingDetail
    {
        public int Id { get; set; }
        public int AvailableSlots { get; set; }

        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }

        public string Source { get; set; }
        public string Destination { get; set; }
    }
}