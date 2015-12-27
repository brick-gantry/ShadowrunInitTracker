using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public class ActorCollection : List<Actor>
    {
        public IEnumerable<Actor> GetActorsForPass(int pass)
        {
            return this.Where((a) => a.TurnInitiativePasses >= pass);
        }
    }
}
