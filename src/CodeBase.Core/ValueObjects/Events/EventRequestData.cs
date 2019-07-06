using System;

namespace CodeBase.Core.ValueObjects.Events
{
    public class EventRequestData : BaseRequestData
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
