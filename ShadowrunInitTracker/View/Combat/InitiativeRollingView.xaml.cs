using ShadowrunInitTracker.Model;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShadowrunInitTracker.View
{
    /// <summary>
    /// Interaction logic for InitiativeRollingView.xaml
    /// </summary>
    public partial class InitiativeRollingView : UserControl
    {
        public InitiativeRollingView()
        {
            InitializeComponent();
        }

        public static ICommand RollInitiativeCommand = new RoutedCommand();
        private void RollInitiative(object sender, ExecutedRoutedEventArgs e)
        {
            Actor a = e.Parameter as Actor;
            a.RollInit(false);
        }

        public static ICommand RollEdgeInitiativeCommand = new RoutedCommand();
        private void RollEdgeInitiative(object sender, ExecutedRoutedEventArgs e)
        {
            Actor a = e.Parameter as Actor;
            a.RollInit(true);
        }
    }
}
