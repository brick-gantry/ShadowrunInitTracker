using ShadowrunInitTracker.Model;
using ShadowrunInitTracker.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShadowrunInitTracker.View
{
    /// <summary>
    /// Interaction logic for EventTab.xaml
    /// </summary>
    public partial class EventTab : UserControl
    {
        public EventTab()
        {
            InitializeComponent();
        }

        EventViewModel vm { get { return DataContext as EventViewModel; } }
        
        public static RoutedUICommand AddEventCmd = new RoutedUICommand(
            "Add Event", "AddEventCmd", typeof(EventTab));
        private void AddEvent(object sender, ExecutedRoutedEventArgs e)
        {
            vm.AddEvent();
        }

        public static RoutedUICommand DeleteEventCmd = new RoutedUICommand(
            "Delete Event", "DeleteEventCmd", typeof(EventTab));
        private void DeleteEvent(object sender, ExecutedRoutedEventArgs e)
        {
            vm.DeleteEvent(e.Parameter as Event);
        }
    }
}
