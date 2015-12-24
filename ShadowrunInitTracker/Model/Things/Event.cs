using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public class Event
    {
        public int Phase { get; set; }
        public int Pass { get; set; }
        public int Initiative { get; set; }
        public string Description { get; set; }
    }
}
