using ShadowrunInitTracker.Model;
using System.ComponentModel;
using System.Windows.Input;

namespace ShadowrunInitTracker.ViewModel
{
    public class EventViewModel : INotifyPropertyChanged
    {
        public EventCollection Events { get { return DataLibrary.Combat.Events; } }
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

        public static ICommand AddEventCommand = new RoutedCommand();
        public static ICommand DeleteEventCommand = new RoutedCommand();

        public void AddEvent(CombatInstance.Time eventTime = null)
        {
            Event toAdd = (eventTime != null)
                ? new Event { Description="<New Event>", Turn = eventTime.Turn, Pass = eventTime.Pass, Phase = eventTime.Phase }
                : new Event { Description = "<New Event>" };
            Events.Add(toAdd);
        }

        public void DeleteEvent(Event toDelete)
        {
            Events.Remove(toDelete);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
