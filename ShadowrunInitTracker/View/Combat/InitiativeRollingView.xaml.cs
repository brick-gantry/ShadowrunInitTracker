using ShadowrunInitTracker.Model;
using ShadowrunInitTracker.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShadowrunInitTracker.View
{
    /// <summary>
    /// Interaction logic for InitiativeRollingView.xaml
    /// </summary>
    public partial class InitiativeRollingView : UserControl
    {
        CombatViewModel vm { get { return DataContext as CombatViewModel; } }

        public InitiativeRollingView()
        {
            InitializeComponent();
        }

        public static RoutedUICommand RollInitiativeCmd = new RoutedUICommand(
            "Roll Actor Initiative", "RollInitiativeCmd", typeof(InitiativeRollingView));
        private void RollInitiative(object sender, ExecutedRoutedEventArgs e)
        {
            Actor a = e.Parameter as Actor;
            a.RollInit(false);
        }

        public static RoutedUICommand RollEdgeInitiativeCmd = new RoutedUICommand(
            "Roll Actor Initiative w/ Edge", "RollEdgeInitiativeCmd", typeof(InitiativeRollingView));
        private void RollEdgeInitiative(object sender, ExecutedRoutedEventArgs e)
        {
            Actor a = e.Parameter as Actor;
            a.RollInit(true);
        }

        public static RoutedUICommand AcceptInitiativesCmd = new RoutedUICommand(
            "Accept Initiatives", "AcceptInitiativesCmd", typeof(InitiativeRollingView));
        private void AcceptInitiatives(object sender, ExecutedRoutedEventArgs e)
        {
            vm.AcceptInitiativeRolls();
        }
    }
}
