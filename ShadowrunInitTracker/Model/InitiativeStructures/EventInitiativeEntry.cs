using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public class EventInitiativeEntry : InitiativeEntry
    {
        public Event Event;
        public override object Source { get { return Event; } }

        public EventInitiativeEntry(Event @event)
        {
            Event = @event;
            Event.PropertyChanged += Event_PropertyChanged;
        }

        ~EventInitiativeEntry()
        {
            Event.PropertyChanged -= Event_PropertyChanged;
        }

        private void Event_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Description":
                    NotifyPropertyChanged("Description");
                    break;
                case "Phase":
                    NotifyPropertyChanged("PhaseDescription");
                    NotifyPropertyChanged("Phase");
                    break;
            }
        }

        public override string Description
        {
            get
            {
                return Event.Description;
            }
        }

        public override string PhaseDescription
        {
            get
            {
                return Event.Phase.ToString();
            }
        }

        public override int Phase
        {
            get
            {
                return Event.Phase;
            }
        }
    }
}
