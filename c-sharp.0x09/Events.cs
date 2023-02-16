using System;
using System.Collections.Generic;
using System.Linq;

namespace actra
{
    class Event
    {
        public DateTime Timestamp;
        public string Description;
        public string Type;

        public Event(string type, string description, DateTime? timestamp = null)
        {
            this.Timestamp = timestamp ?? DateTime.Now;
            this.Type = type;
            this.Description = description;
        }
    }

    class EventList
    {
        private List<Event> List;
        public EventList()
        {
            List = new List<Event>();
        }

        public IEnumerable<Event> getNext()
        {
            int index = 0;
            while (index < this.List.Count)
                yield return this.List[index++];
        }

        public void add(Event e)
        {
            List.Add(e);
        }

        delegate bool Filter(Event e);

        private List<Event> filterEvents(Filter f)
        {
            var result = from n in this.List where f(n) select n;
            return result.ToList();
        }

        public List<string> showEvents(string searchStr)
        {
            Filter filter = x => true;
            if (searchStr.Trim().Equals(""))
            {
                filter = e => e.Description.Contains(searchStr);
            }
            var list = from l in filterEvents(filter)
                       select l.Timestamp + l.Description;
            return list.ToList();
        }
    }
}
