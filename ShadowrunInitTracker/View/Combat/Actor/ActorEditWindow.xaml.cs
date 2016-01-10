using ShadowrunInitTracker.Model;
using ShadowrunInitTracker.ViewModel;
using System.Windows;

namespace ShadowrunInitTracker.View
{
    /// <summary>
    /// Interaction logic for ActorEditWindow.xaml
    /// </summary>
    public partial class ActorEditWindow : Window
    {
        public ActorEditWindow(Actor vm)
        {
            DataContext = vm;
            InitializeComponent();
        }
    }
}
