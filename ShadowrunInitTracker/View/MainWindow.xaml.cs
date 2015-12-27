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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShadowrunInitTracker.View
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public FullInitiativeViewModel ViewModel {  get { return DataContext as FullInitiativeViewModel; } }

        private void AddCharacter(object sender, ExecutedRoutedEventArgs e)
        {
            Commands.AddCharacter();
        }

        private void SaveCharacters(object sender, ExecutedRoutedEventArgs e)
        {
            Commands.ExportCharacterSet(DataLibrary.Instance.Characters);
        }

        private void ImportCharacters(object sender, ExecutedRoutedEventArgs e)
        {
            Commands.ImportCharacterSet(DataLibrary.Instance.Characters);
        }
    }
}
