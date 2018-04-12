using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaerskApplication.Models.ViewModel
{
    public class BookingViewModel
    {
        public int CustomerId { get; set; }
        public int ShippingId { get; set; }
        public int Slots { get; set; }
    }
}