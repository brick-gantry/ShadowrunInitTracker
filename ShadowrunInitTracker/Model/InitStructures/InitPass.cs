using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public class InitPass : List<object>
    {
        public object CurrentActor { get; set; }
        public int CurrentActorIndex
        {
            get { return this.IndexOf(CurrentActor); }
            set
            {
                int nextIndex = value;
                CurrentActor = (nextIndex == Count) ? null : this[nextIndex];
            }
        }

        public InitPass()
        {
            /*this.ch += (s, e) =>
            {
                Sort();
            };*/
        }

        new public void Sort()
        {
            Sort((a, b) =>
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

        public enum NextResult {  NextSelected, NoneSelected }
        public NextResult Next()
        {
            CurrentActorIndex++;
            return (CurrentActor == null) ? NextResult.NoneSelected : NextResult.NextSelected;
        }
    }
}
