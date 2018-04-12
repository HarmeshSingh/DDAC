using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaerskApplication.Models.ViewModel
{
    public class ShippingViewModel
    {
        public string DateFrom { get; set; }
        public string DateTo { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
        public int AvailableSlots { get; set; }
    }
}