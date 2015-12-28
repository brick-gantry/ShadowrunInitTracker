using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public class CombatInstance
    {
        public ActorCollection Actors = new ActorCollection();
        public EventCollection Events = new EventCollection();

        public int Turn { get; set; } = 1;
        public InitiativeTurn CurrentTurn { get; set; } = new InitiativeTurn();
        public InitiativePass CurrentPass { get { return CurrentTurn.CurrentPass; } }
        
        public enum NextResult { NextActorFound, NoMoreActors }
        public NextResult Next()
        {
            var result = CurrentTurn.Next();
            if(result == InitiativeTurn.NextResult.LoopBack)
            {
                Turn++;
                CurrentTurn.Clear();
                foreach (var a in Actors)
                    a.UpdateForNextTurn();
                return NextResult.NoMoreActors;
            }
            return NextResult.NextActorFound;
        }

        public class Time
        {
            public int Turn { get; set; }
            public int Pass { get; set; }
            public int Phase { get; set; }
        }

        public Time Now
        {
            get
            {
                return new Time
                {
                    Turn = Turn,
                    Pass = CurrentTurn.CurrentPassNumber,
                    Phase = CurrentTurn.CurrentPass.CurrentActor.Phase
                };
            }
        }

        public void Reset()
        {
            Turn = 1;
            CurrentTurn.Clear();
            BuildInit();
        }

        public void BuildInit()
        {
            for (int pass = 1; pass <= 4; pass++)
            {
                foreach (var a in Actors.GetActorsForPass(pass))
                    CurrentTurn.Passes[pass].Add(new ActorInitiativeEntry(a));
                foreach (var e in Events.GetEventsForPass(Turn, pass))
                    CurrentTurn.Passes[pass].Add(new EventInitiativeEntry(e));
            }
        }
    }
}
