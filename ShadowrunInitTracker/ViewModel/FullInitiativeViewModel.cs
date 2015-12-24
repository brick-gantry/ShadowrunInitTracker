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
        public InitPhase CurrentInit { get; set; }
        public InitPhase OriginalInit { get; set; }
        Clock clock = new Clock();
    }
}
