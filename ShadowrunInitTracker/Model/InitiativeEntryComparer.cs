using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    class InitiativeEntryComparer : IComparer<InitiativeEntry>
    {
        public int Compare(InitiativeEntry a, InitiativeEntry b)
        {
            if (a.ActionTaken || b.ActionTaken)
                return 0;

            if(a is ActorInitiativeEntry)
            {
                if (b is ActorInitiativeEntry)
                    return Compare(a as ActorInitiativeEntry, b as ActorInitiativeEntry);
                else
                    return Compare(a as ActorInitiativeEntry, b as EventInitiativeEntry);
            }
            else
            {
                if (b is ActorInitiativeEntry)
                    return -Compare(b as ActorInitiativeEntry, a as EventInitiativeEntry);
                else
                    return Compare(a as EventInitiativeEntry, b as EventInitiativeEntry);
            }
        }

        int Compare(ActorInitiativeEntry a, ActorInitiativeEntry b)
        {
            //critical glitches go last
            if (a.Actor.InitiativeGlitch == GlitchType.CriticalGlitch &&
               b.Actor.InitiativeGlitch != GlitchType.CriticalGlitch)
                return 1;
            if (a.Actor.InitiativeGlitch != GlitchType.CriticalGlitch &&
                b.Actor.InitiativeGlitch == GlitchType.CriticalGlitch)
                return -1;

            //core sorting logic
            if (a.Actor.InitiativeScore != b.Actor.InitiativeScore)
                return b.Actor.AdjustedInitiativeScore - a.Actor.AdjustedInitiativeScore;

            //glitches go after non glitches
            if (a.Actor.InitiativeGlitch == GlitchType.Glitch &&
               b.Actor.InitiativeGlitch == GlitchType.NoGlitch)
                return 1;
            if (a.Actor.InitiativeGlitch == GlitchType.NoGlitch &&
                b.Actor.InitiativeGlitch == GlitchType.Glitch)
                return -1;

            //tie breaking
            if (a.Actor.Edge != b.Actor.Edge)
                return b.Actor.Edge - a.Actor.Edge;
            if (a.Actor.TurnInitiativeAttribute != b.Actor.TurnInitiativeAttribute)
                return b.Actor.TurnInitiativeAttribute - a.Actor.TurnInitiativeAttribute;
            if (a.Actor.Reaction != b.Actor.Reaction)
                return b.Actor.Reaction - a.Actor.Reaction;

            return 0;
        }

        int Compare(ActorInitiativeEntry a, EventInitiativeEntry b)
        {
            return b.Event.Phase - a.Actor.InitiativeScore;
        }

        int Compare(EventInitiativeEntry a, EventInitiativeEntry b)
        {
            return b.Phase - a.Phase;
        }
    }
}
