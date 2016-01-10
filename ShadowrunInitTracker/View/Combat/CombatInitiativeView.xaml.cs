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
    }
}
