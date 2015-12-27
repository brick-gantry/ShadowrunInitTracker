using ShadowrunInitTracker.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.ViewModel
{
    public class EventEditViewModel
    {
        public Event Model = new Event();

        public EventEditViewModel(CombatInstance.Time now)
        {
            Phase = now.Turn;
            Pass = now.Pass;
            Initiative = now.Phase;
        }

        public string Description { get; set; }
        public int? Phase { get; set; }
        public int? Pass { get; set; }
        public int? Initiative { get; set; }

        public void CopyToModel()
        {
            Model.Description = Description;
            Model.Turn = Phase.HasValue ? Phase.Value : 0;
            Model.Pass = Pass.HasValue ? Pass.Value : 0;
            Model.Phase = Initiative.HasValue ? Initiative.Value : 0;
        }

    }
}
