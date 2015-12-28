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

        public static ICommand NextCommand = new RoutedCommand();
        private void Next(object sender, ExecutedRoutedEventArgs e)
        {
            vm.Next();
        }

        public static ICommand SaveCombatCommand = new RoutedCommand();
        private void SaveCombat(object sender, ExecutedRoutedEventArgs e)
        {
            vm.SaveCombat();
        }

        public static ICommand EndCombatCommand = new RoutedCommand();
        private void EndCombat(object sender, ExecutedRoutedEventArgs e)
        {
            vm.EndCombat();
        }
    }
}
