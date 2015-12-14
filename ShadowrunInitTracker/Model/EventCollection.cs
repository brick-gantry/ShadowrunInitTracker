using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public class EventCollection
    {
        public static List<Event> Events = new List<Event>();
    
        public static IEnumerable<Event> GetEventsForPass(int phase, int pass)
        {
            return Events.Where((e) => e.Phase == phase && e.Pass == pass);
        }
    }
}
