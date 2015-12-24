using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public static class ActorCollection
    {
        public static List<Actor> Actors = new List<Actor>();

        public static IEnumerable<Actor> GetActorsForPass(int pass)
        {
            return Actors.Where((a) => a.CurrentInitiativeSet.Passes >= pass);
        }
    }
}
