using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public class DataLibrary
    {
        private DataLibrary() { }

        public static DataLibrary Instance { get; } = new DataLibrary();

        public CharacterCollection Characters = new CharacterCollection();
    }
}
