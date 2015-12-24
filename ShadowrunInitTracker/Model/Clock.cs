using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public class Clock
    {
        public int Phase { get; set; } = 1;

        public InitPhase CurrentPhase { get; set; } = new InitPhase();
        public InitPass CurrentPass { get { return CurrentPhase.CurrentPass; } }
        
        public void Next()
        {
            var result = CurrentPhase.Next();
            if(result == InitPhase.NextResult.LoopBack)
            {
                Phase++;
                CurrentPhase.Clear();
                BuildInit();
            }
        }

        public void BuildInit()
        {
            //todo roll initiatives
            for (int pass = 1; pass <= 4; pass++)
            {
                foreach (var a in ActorCollection.GetActorsForPass(pass))
                    CurrentPhase.Passes[pass].Add(a);
                foreach (var e in EventCollection.GetEventsForPass(Phase, pass))
                    CurrentPhase.Passes[pass].Add(e);
            }
        }
    }
}
