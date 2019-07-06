using CodeBase.Core.ValueObjects;
using CodeBase.Core.ValueObjects.Events;
using CodeBase.Web.Clients.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CodeBase.Web.Controllers
{
    public class HomeController : Controller
    {
        private RequestContext<T> PrepareContext<T>(T payload)
            where T : IRequestData
        {
            return new RequestContext<T>(payload);
        }

        private readonly IEventClient _eventClient;
        public HomeController(IEventClient eventClient)
        {
            _eventClient = eventClient;
        }

        public ActionResult Index()
        {
            return View();
        }

        public async Task<ActionResult> About()
        {
            var response = await _eventClient.AddEventAsync(PrepareContext<EventRequestData>(new EventRequestData
            {
                Name = "New Event",
                Date = DateTime.Now
            }));

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}