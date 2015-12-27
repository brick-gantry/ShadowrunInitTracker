using ShadowrunInitTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.ViewModel
{
    public class CombatInitiativeViewModel
    {
        public InitiativeTurn CurrentInit { get { return Combat.CurrentTurn; } }
        public CombatInstance Combat = new CombatInstance();
    }
}
