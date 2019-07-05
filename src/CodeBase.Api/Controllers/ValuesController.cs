using CodeBase.Core.Services.Event;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CodeBase.Api.Controllers
{
    public class ValuesController : BaseApiController
    {
        private readonly IEventService _eventService;

        public ValuesController(IEventService eventService)
        {
            _eventService = eventService;
        }

        // GET api/values
        public IEnumerable<string> Get()
        {
            _eventService.AddEventAsync(new Core.ValueObjects.Events.EventRequestData { });

            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
