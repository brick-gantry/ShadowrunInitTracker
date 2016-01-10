using ShadowrunInitTracker.Model;
using ShadowrunInitTracker.ViewModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShadowrunInitTracker.View
{
    /// <summary>
    /// Interaction logic for CombatToggleTab.xaml
    /// </summary>
    public partial class CombatToggleTab : UserControl
    {
        public CombatViewModel vm { get { return DataContext as CombatViewModel; } }

        public CombatToggleTab()
        {
            InitializeComponent();
        }

        public static RoutedUICommand LoadCombatCmd = new RoutedUICommand(
            "Load Combat", "LoadCombatCmd", typeof(CombatToggleTab));
        private void LoadCombat(object sender, ExecutedRoutedEventArgs e)
        {
            vm.LoadCombat();
        }

        public static RoutedUICommand StartCombatCmd = new RoutedUICommand(
            "Start Combat", "StartCombatCmd", typeof(CombatToggleTab));
        private void StartCombat(object sender, ExecutedRoutedEventArgs e)
        {
            vm.StartCombat();
        }

        public static RoutedUICommand RollInitiativeCmd = new RoutedUICommand(
            "Roll Actor Initiative", "RollInitiativeCmd", typeof(CombatToggleTab));
        private void RollInitiative(object sender, ExecutedRoutedEventArgs e)
        {
            vm.RollInit(e.Parameter as Actor, false);
        }

        public static RoutedUICommand RollEdgeInitiativeCmd = new RoutedUICommand(
            "Roll Actor Initiative w/ Edge", "RollEdgeInitiativeCmd", typeof(CombatToggleTab));
        private void RollEdgeInitiative(object sender, ExecutedRoutedEventArgs e)
        {
            vm.RollInit(e.Parameter as Actor, true);
        }

        public static RoutedUICommand AcceptInitiativesCmd = new RoutedUICommand(
            "Accept Initiatives", "AcceptInitiativesCmd", typeof(CombatToggleTab));
        private void AcceptInitiatives(object sender, ExecutedRoutedEventArgs e)
        {
            vm.AcceptInitiativeRolls();
        }

        public static RoutedUICommand NextCmd = new RoutedUICommand(
            "Next!", "NextCmd", typeof(CombatToggleTab));
        private void Next(object sender, ExecutedRoutedEventArgs e)
        {
            vm.Next();
        }

        public static RoutedUICommand SaveCombatCmd = new RoutedUICommand(
            "Save Combat", "SaveCombatCmd", typeof(CombatToggleTab));
        private void SaveCombat(object sender, ExecutedRoutedEventArgs e)
        {
            vm.SaveCombat();
        }

        public static RoutedUICommand EndCombatCmd = new RoutedUICommand(
            "End Combat", "EndCombatCmd", typeof(CombatToggleTab));
        private void EndCombat(object sender, ExecutedRoutedEventArgs e)
        {
            vm.EndCombat();
        }

        public static readonly RoutedUICommand DelayActorCmd = new RoutedUICommand(
            "Delay", "DelayActorCmd", typeof(CombatToggleTab));
        private void DelayActor(object sender, ExecutedRoutedEventArgs e)
        {
            var actor = vm.Combat.CurrentEntry.Source as Actor;
            if (actor != null)
                actor.Delaying = true;
        }

        public static readonly RoutedUICommand ResumeActorCmd = new RoutedUICommand(
            "Resume", "ResumeActorCmd", typeof(CombatToggleTab));
        private void ResumeActor(object sender, ExecutedRoutedEventArgs e)
        {
            var currEntry = vm.Combat.DelayingActors.CurrentEntry;
            if (currEntry == null)
                return;
            var actor = currEntry.Source as Actor;
            if (actor != null)
                actor.Delaying = false;
        }

        public static readonly RoutedUICommand RemoveEntryCmd = new RoutedUICommand(
            "Remove", "RemoveEntryCmd", typeof(CombatToggleTab));
        private void RemoveEntry(object sender, ExecutedRoutedEventArgs e)
        {
            vm.Combat.RemoveEntry(e.Parameter as InitiativeEntry);
        }

        public static readonly RoutedUICommand AddActorCmd = new RoutedUICommand(
            "Add Actor", "AddActorCmd", typeof(CombatToggleTab));
        private void AddActor(object sender, ExecutedRoutedEventArgs e)
        {
            //todo implement
        }

        public static readonly RoutedUICommand RemoveActorCmd = new RoutedUICommand(
            "Remove Actor", "RemoveActorCmd", typeof(CombatToggleTab));
        private void RemoveActor(object sender, ExecutedRoutedEventArgs e)
        {
            var actor = (e.Parameter as InitiativeEntry).Source as Actor;
            vm.Combat.RemoveActor(actor);
        }

        public static readonly RoutedUICommand EditWoundCmd = new RoutedUICommand(
            "Edit Actor Wound", "EditWoundCmd", typeof(CombatToggleTab));
        private void EditWound(object sender, ExecutedRoutedEventArgs e)
        {
            var actorEdit = new ActorEditWindow((e.Parameter as ActorInitiativeEntry).Actor);//pass the view model when I get the binding to work
            actorEdit.ShowDialog();
        }
    }
}
