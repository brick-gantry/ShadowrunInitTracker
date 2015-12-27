using ShadowrunInitTracker.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShadowrunInitTracker.View
{
    /// <summary>
    /// Interaction logic for FullView.xaml
    /// </summary>
    public partial class CombatInitiativeView : UserControl
    {
        CombatInitiativeViewModel vm { get { return DataContext as CombatInitiativeViewModel; } }

        public CombatInitiativeView()
        {
            InitializeComponent();
        }

        private void StartCombat(object sender, ExecutedRoutedEventArgs e)
        {
            Commands.StartCombat();
        }

        private void SaveCombat(object sender, ExecutedRoutedEventArgs e)
        {
            Commands.SaveCombat(vm.Combat);
        }

        private void LoadCombat(object sender, ExecutedRoutedEventArgs e)
        {
            Commands.LoadCombat(out vm.Combat);
        }
    }
}
