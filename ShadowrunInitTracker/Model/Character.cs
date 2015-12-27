using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    [Serializable]
    public class Character
    {
        public enum CharacterType { PC, NPC }

        #region unchanging stats
        public string Name { get; set; }
        public CharacterType Type { get; set; }
        public int Edge { get; set; }
        public int Reaction { get; set; }
        public int PhysicalInitiative { get; set; }
        public int PhysicalPasses { get; set; }
        public int MatrixInitiative { get; set; }
        public int MatrixPasses { get; set; }
        public int AstralInitiative { get; set; }
        public int AstralPasses { get; set; }
        #endregion
    }
}
