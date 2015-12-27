using ShadowrunInitTracker.Model;
using ShadowrunInitTracker.ViewModel;
using System.Windows.Controls;
using System.Windows.Input;

namespace ShadowrunInitTracker.View
{
    /// <summary>
    /// Interaction logic for CharacterTab.xaml
    /// </summary>
    public partial class CharacterTab : UserControl
    {
        public CharacterTab()
        {
            InitializeComponent();
        }

        CharacterViewModel vm { get { return DataContext as CharacterViewModel; } }

        private void AddCharacter(object sender, ExecutedRoutedEventArgs e)
        {
            vm.AddCharacter();
        }

        private void DeleteCharacter(object sender, ExecutedRoutedEventArgs e)
        {
            vm.DeleteCharacter(e.Parameter as Character);
        }

        private void ImportCharacters(object sender, ExecutedRoutedEventArgs e)
        {
            vm.ImportCharacterSet(DataLibrary.Characters);
        }

        private void ExportCharacters(object sender, ExecutedRoutedEventArgs e)
        {
            vm.ExportCharacterSet(DataLibrary.Characters);
        }
    }
}
