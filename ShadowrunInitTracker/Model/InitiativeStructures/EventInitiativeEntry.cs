using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    [Serializable]
    public class EventInitiativeEntry : InitiativeEntry
    {
        Event @event;
        public override INotifyPropertyChanged Source { get { return @event; } }
        public Event Event { get { return @event; } }

        public EventInitiativeEntry(Event @event)
        {
            this.@event = @event;
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
