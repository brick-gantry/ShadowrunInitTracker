using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ShadowrunInitTracker.Model
{
    public class EventCollection : ObservableCollection<Event>
    {    
        public IEnumerable<Event> GetEventsForPass(int phase, int pass)
        {
            return this.Where((e) => e.Turn == phase && e.Pass == pass);
        }
    }
}
