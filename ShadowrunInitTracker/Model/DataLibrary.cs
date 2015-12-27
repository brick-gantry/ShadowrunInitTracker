using System.Collections.ObjectModel;

namespace ShadowrunInitTracker.Model
{
    public static class DataLibrary
    {
        public static ObservableCollection<Character> Characters = new ObservableCollection<Character>();

        public static CombatInstance Combat = new CombatInstance();
    }
}
