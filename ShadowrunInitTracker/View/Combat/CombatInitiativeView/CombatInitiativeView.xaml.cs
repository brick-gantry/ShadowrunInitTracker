using ShadowrunInitTracker.Model;
using ShadowrunInitTracker.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShadowrunInitTracker.View
{
    /// <summary>
    /// Interaction logic for FullView.xaml
    /// </summary>
    public partial class CombatInitiativeView : UserControl
    {
        CombatViewModel vm { get { return DataContext as CombatViewModel; } }

        public CombatInitiativeView()
        {
            InitializeComponent();
        }

        public static RoutedUICommand NextCmd = new RoutedUICommand(
            "Next!", "NextCmd", typeof(CombatInitiativeView));
        private void Next(object sender, ExecutedRoutedEventArgs e)
        {
            vm.Next();
        }

        public static RoutedUICommand SaveCombatCmd = new RoutedUICommand(
            "Save Combat", "SaveCombatCmd", typeof(CombatInitiativeView));
        private void SaveCombat(object sender, ExecutedRoutedEventArgs e)
        {
            vm.SaveCombat();
        }

        public static RoutedUICommand EndCombatCmd = new RoutedUICommand(
            "End Combat", "EndCombatCmd", typeof(CombatInitiativeView));
        private void EndCombat(object sender, ExecutedRoutedEventArgs e)
        {
            vm.EndCombat();
        }

        public static readonly RoutedUICommand DelayActorCmd = new RoutedUICommand(
            "Delay", "DelayActorCmd", typeof(CombatInitiativeView));
        private void DelayActor(object sender, ExecutedRoutedEventArgs e)
        {
            var actor = vm.Combat.CurrentEntry.Source as Actor;
            if(actor != null)
                actor.Delaying = true;
        }
    }
}
