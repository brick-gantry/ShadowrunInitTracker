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
        
        public void Next()
        {
            var result = CurrentTurn.Next();
            if(result == InitiativeTurn.NextResult.LoopBack)
            {
                Turn++;
                CurrentTurn.Clear();
                BuildInit();
            }
        }

        public class Time
        {
            public int Turn { get; set; }
            public int Pass { get; set; }
            public int Phase { get; set; }
        }

        public Time Now {  get { return new Time { Turn = Turn, Pass = CurrentTurn.CurrentPassNumber, Phase = CurrentTurn.CurrentPass.CurrentActor.CurrentInitiativePhase }; } }

        public void Reset()
        {
            Turn = 1;
            CurrentTurn.Clear();
        }

        public void BuildInit()
        {
            foreach (var a in Actors)
                a.UpdateForNextPhase();

            //todo roll initiatives

            for (int pass = 1; pass <= 4; pass++)
            {
                foreach (var a in Actors.GetActorsForPass(pass))
                    CurrentTurn.Passes[pass].Add(a);
                foreach (var e in Events.GetEventsForPass(Turn, pass))
                    CurrentTurn.Passes[pass].Add(e);
            }
        }
    }
}
