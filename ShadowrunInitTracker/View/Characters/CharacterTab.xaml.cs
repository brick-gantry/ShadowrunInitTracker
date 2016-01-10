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


        public static RoutedUICommand AddCharacterCmd = new RoutedUICommand(
            "Add Character", "AddCharacterCmd", typeof(CharacterTab));
        private void AddCharacter(object sender, ExecutedRoutedEventArgs e)
        {
            vm.AddCharacter();
        }

        public static RoutedUICommand DeleteCharacterCmd = new RoutedUICommand(
            "Delete Character", "DeleteCharacterCmd", typeof(CharacterTab));
        private void DeleteCharacter(object sender, ExecutedRoutedEventArgs e)
        {
            vm.DeleteCharacter(e.Parameter as Character);
        }

        public static RoutedUICommand ImportCharactersCmd = new RoutedUICommand(
            "Import Characters", "ImportCharactersCmd", typeof(CharacterTab));
        private void ImportCharacters(object sender, ExecutedRoutedEventArgs e)
        {
            vm.ImportAllCharacters();
        }

        public static RoutedUICommand ExportCharactersCmd = new RoutedUICommand(
            "Export Characters", "ExportCharactersCmd", typeof(CharacterTab));
        private void ExportCharacters(object sender, ExecutedRoutedEventArgs e)
        {
            vm.ExportAllCharacters();
        }
    }
}
