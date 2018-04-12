using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Configuration;
using System.Diagnostics;
using MaerskApplication.Models;
using MaerskApplication.Models.ViewModel;
using System.Globalization;

namespace MaerskApplication.Controllers
{
    public class HomeController : Controller
    {
        DataAccess da = new DataAccess();

        public ActionResult Index()
        {
            var users = da.GetUser();
            
            return View();
        }

        public ActionResult ViewCustomer()
        {
            var customers = da.GetCustomer();

            return View(customers);
        }

        [HttpGet]
        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer(CustomerViewModel model)
        {
            Customer customer = new Customer();
            customer.Name = model.Name;
            customer.CompanyName = model.CompanyName;

            da.AddCustomer(customer);

            return RedirectToAction("ViewCustomer");
        }

        public ActionResult ViewAgent()
        {
            var agents = da.GetAgents();

            return View(agents);
        }

        [HttpGet]
        public ActionResult AddAgent()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddAgent(AgentViewModel model)
        {
            Users agent = new Users();
            agent.Name = model.Name;
            agent.Username = model.Username;
            agent.Password = model.Password;
            agent.Role = 0;

            da.AddAgent(agent);

            return RedirectToAction("ViewAgent");
        }

        public ActionResult ViewShipping()
        {
            var shippings = da.GetShippingDetail();

            return View(shippings);
        }

        [HttpGet]
        public ActionResult AddShipping()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddShipping(ShippingViewModel model)
        {
            ShippingDetail shipping = new ShippingDetail();
            shipping.Source = model.Source;
            shipping.Destination = model.Destination;
            shipping.DateFrom = DateTime.ParseExact(model.DateFrom, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            shipping.DateTo = DateTime.ParseExact(model.DateTo, "MM/dd/yyyy", CultureInfo.InvariantCulture);
            shipping.AvailableSlots = model.AvailableSlots;

            da.AddShipping(shipping);

            return RedirectToAction("ViewShipping");
        }

        public ActionResult ViewBooking()
        {
            var bookings = da.GetBooking();

            return View(bookings);
        }

        [HttpGet]
        public ActionResult AddBooking()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddBooking(BookingViewModel model)
        {
            Booking booking = new Booking();
            booking.CustomerId = model.CustomerId;
            booking.ShippingId = model.ShippingId;
            booking.Slots = model.Slots;

            da.AddBooking(booking);
            
            return RedirectToAction("BookSuccess");
        }

        public ActionResult BookSuccess()
        {
            return View();
        }
    }
}