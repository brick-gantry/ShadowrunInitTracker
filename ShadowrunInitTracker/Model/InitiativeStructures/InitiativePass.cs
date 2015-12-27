using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public class InitiativePass : List<InitiativeEntry>
    {
        public InitiativeEntry CurrentActor { get; set; }
        public int CurrentActorIndex
        {
            get { return this.IndexOf(CurrentActor); }
            set
            {
                int nextIndex = value;
                CurrentActor = (nextIndex == Count) ? null : this[nextIndex];
            }
        }

        public InitiativePass()
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
                        if (actorA.InitiativeScore != actorB.InitiativeScore)
                            return actorA.InitiativeScore - actorB.InitiativeScore;
                        if (actorA.Edge != actorB.Edge)
                            return actorA.Edge - actorB.Edge;
                        if (actorA.CurrentInitiativePhase != actorB.CurrentInitiativePhase)
                            return actorA.CurrentInitiativePhase - actorB.CurrentInitiativePhase;
                        if (actorA.Reaction != actorB.Reaction)
                            return actorA.Reaction - actorB.Reaction;
                    }
                    else if (b is Event)
                    {
                        return actorA.InitiativeScore - eventB.Phase;
                    }
                }
                if (a is Event)
                {
                    if (b is Actor)
                    {
                        return eventA.Phase - actorB.InitiativeScore;
                    }
                    else if (b is Event)
                    {
                        return eventA.Phase - eventB.Phase;
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
