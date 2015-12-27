using ShadowrunInitTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.ViewModel
{
    public class FullInitiativeViewModel
    {
        public InitiativeTurn CurrentInit { get; set; }
        public InitiativeTurn OriginalInit { get; set; }
        CombatInstance clock = new CombatInstance();
    }
}
