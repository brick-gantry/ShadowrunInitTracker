using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public class Clock
    {
        public int Phase = 1;
        public int Pass = 1;

        //todo observable data structure
        public ActorQueue ActorsInPass;


        public void Next()
        {
            if (ActorsInPass.Count > 0)
            {
                ActorsInPass.Next();
            }
            else
            {
                Pass++;
                if (Pass > 4)
                {
                    Pass = 1;
                    Phase++;
                    //todo roll new initiatives
                }

                ActorsInPass = GetActorQueue();

                if (ActorsInPass.Count == 0)
                {
                    //todo ask if anyone wants to use edge to act in pass
                }

                if (ActorsInPass.Count == 0)
                {
                    Pass = 1;
                    Phase++;
                }
            }
        }

        public ActorQueue GetActorQueue()
        {
            ActorQueue aq = new ActorQueue();
            foreach (var a in ActorCollection.GetActorsForPass(Pass))
                aq.Add(a);
            foreach (var e in EventCollection.GetEventsForPass(Phase, Pass))
                aq.Add(e);
            return aq;
        }
    }
}
