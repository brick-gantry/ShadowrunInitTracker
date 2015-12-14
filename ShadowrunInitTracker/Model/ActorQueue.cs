using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public class ActorQueue
    {
        public List<object> Entries; //actors and events
        public int Count { get { return Entries.Count; } }

        public int CurrentActing = 0;

        public void Next()
        {
            CurrentActing++;
        }

        public void Add(Actor a)
        {
            Entries.Add(a);
            Sort();
        }

        public void Add(Event e)
        {
            Entries.Add(e);
            Sort();
        }

        public void Sort()
        {
            Entries.Sort((a, b)
            =>
            {
                Actor actorA = a as Actor;
                Actor actorB = b as Actor;
                Event eventA = a as Event;
                Event eventB = b as Event;

                if (a is Actor)
                {
                    if (b is Actor)
                    {
                        if (actorA.RolledInit != actorB.RolledInit)
                            return actorA.RolledInit - actorB.RolledInit;
                        if (actorA.Edge != actorB.Edge)
                            return actorA.Edge - actorB.Edge;
                        if (actorA.CurrentInitiativeSet.Value != actorB.CurrentInitiativeSet.Value)
                            return actorA.CurrentInitiativeSet.Value - actorB.CurrentInitiativeSet.Value;
                        if (actorA.Reaction != actorB.Reaction)
                            return actorA.Reaction - actorB.Reaction;
                    }
                    else if (b is Event)
                    {
                        return actorA.RolledInit - eventB.Initiative;
                    }
                }
                if (a is Event)
                {
                    if (b is Actor)
                    {
                        return eventA.Initiative - actorB.RolledInit;
                    }
                    else if (b is Event)
                    {
                        return eventA.Initiative - eventB.Initiative;
                    }
                }
                return 0;
            });
        }
    }
}
