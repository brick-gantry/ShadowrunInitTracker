using ShadowrunInitTracker.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.ViewModel
{
    public class CharacterCollectionViewModel
    {
        public CharacterCollection Characters { get; set; }
        
        public CharacterCollectionViewModel(CharacterCollection characters)
        {
            Characters = characters;
        }
    }
}
