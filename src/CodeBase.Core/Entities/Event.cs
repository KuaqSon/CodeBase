using System;

namespace CodeBase.Core.Entities
{
    public class Event : BaseEntity
    {
        public string Name { get; set; }
        public DateTime Date { get; set; }
    }
}
