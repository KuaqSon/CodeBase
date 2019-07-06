using System;

namespace CodeBase.Core.ValueObjects.Events
{
    public class EventResponseData : BaseResponseData
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
