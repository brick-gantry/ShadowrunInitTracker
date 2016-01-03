using ShadowrunInitTracker.Model;
using ShadowrunInitTracker.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShadowrunInitTracker.View
{
    /// <summary>
    /// Interaction logic for ActorSelectionView.xaml
    /// </summary>
    public partial class ActorSelectionView : UserControl
    {
        CombatViewModel vm { get { return DataContext as CombatViewModel; } }

        public ActorSelectionView()
        {
            InitializeComponent();
        }

        public static RoutedUICommand LoadCombatCmd = new RoutedUICommand(
            "Load Combat", "LoadCombatCmd", typeof(ActorSelectionView));
        private void LoadCombat(object sender, ExecutedRoutedEventArgs e)
        {
            vm.LoadCombat();
        }

        public static RoutedUICommand StartCombatCmd = new RoutedUICommand(
            "Start Combat", "StartCombatCmd", typeof(ActorSelectionView));
        private void StartCombat(object sender, ExecutedRoutedEventArgs e)
        {
            vm.StartCombat();
        }
    }
}
