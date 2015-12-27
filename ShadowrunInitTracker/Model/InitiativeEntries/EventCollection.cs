using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public class EventCollection : List<Event>
    {    
        public IEnumerable<Event> GetEventsForPass(int phase, int pass)
        {
            return this.Where((e) => e.Turn == phase && e.Pass == pass);
        }
    }
}
