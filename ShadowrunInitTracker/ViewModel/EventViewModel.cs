using ShadowrunInitTracker.Model;
using System.ComponentModel;
using System.Windows.Input;

namespace ShadowrunInitTracker.ViewModel
{
    public class EventViewModel : INotifyPropertyChanged
    {
        public CombatInstance Combat { get { return DataLibrary.Combat; } }
        public EventCollection Events { get { return Combat.Events; } }
        Event selectedEvent;
        public Event SelectedEvent
        {
            get { return selectedEvent; }
            set
            {
                selectedEvent = value;
                NotifyPropertyChanged("SelectedEvent");
            }
        }

        public void AddEvent(CombatInstance.Time eventTime = null)
        {
            Event toAdd = (eventTime != null)
                ? new Event { Description="<New Event>", Turn = eventTime.Turn, Pass = eventTime.Pass, Phase = eventTime.Phase }
                : new Event { Description = "<New Event>" };
            Combat.AddEvent(toAdd);
        }

        public void DeleteEvent(Event toDelete)
        {
            Combat.RemoveEvent(toDelete);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
