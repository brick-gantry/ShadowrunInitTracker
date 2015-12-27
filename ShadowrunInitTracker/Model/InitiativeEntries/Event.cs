using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public class Event : InitiativeEntry
    {
        public string Description { get; set; }
        public int Turn { get; set; }
        public int Pass { get; set; }
        public int Phase { get; set; }

        public int CurrentInitiativePhase
        {
            get
            {
                return Phase;
            }
        }
    }
}
