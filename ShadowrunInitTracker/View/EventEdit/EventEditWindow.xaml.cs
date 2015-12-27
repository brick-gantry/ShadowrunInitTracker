using ShadowrunInitTracker.Model;
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
using System.Windows.Shapes;

namespace ShadowrunInitTracker.View
{
    /// <summary>
    /// Interaction logic for ActorEditWindow.xaml
    /// </summary>
    public partial class EventEditWindow : Window
    {
        public EventEditWindow(EventEditViewModel vm)
        {
            InitializeComponent();
            DataContext = vm;
        }

        public bool DoSave = false;
        private void bSave_Click(object sender, RoutedEventArgs e)
        {
            DoSave = true;
            Close();
        }
    }
}
