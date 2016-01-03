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

        public static RoutedUICommand NextCmd = new RoutedUICommand(
            "Next!", "NextCmd", typeof(CombatToggleTab));
    }
}
