using System.Collections.ObjectModel;

namespace ShadowrunInitTracker.Model
{
    public class DataLibrary
    {
        private DataLibrary() { }

        public static DataLibrary Instance { get; } = new DataLibrary();

        public ObservableCollection<Character> Characters = new ObservableCollection<Character>();
    }
}
