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
    }
}
