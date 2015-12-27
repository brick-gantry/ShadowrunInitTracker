using ShadowrunInitTracker.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.ViewModel
{
    public class CharacterEditViewModel
    {
        public Character Model = new Character();

        public CharacterEditViewModel() { }
        public CharacterEditViewModel(Character model)
        {
            Model = model;
            CopyFromModel();
        }

        public string Name { get; set; }
        public int? Edge { get; set; }
        public int? Reaction { get; set; }
        public int? PhysicalInitiative { get; set; }
        public int? PhysicalPasses { get; set; }
        public int? MatrixInitiative { get; set; }
        public int? MatrixPasses { get; set; }
        public int? AstralInitiative { get; set; }
        public int? AstralPasses { get; set; }

        private void CopyFromModel()
        {
            Name = Model.Name;
            Edge = Model.Edge;
            Reaction = Model.Reaction;
            PhysicalInitiative = Model.PhysicalInitiative;
            PhysicalPasses = Model.PhysicalPasses;
            MatrixInitiative = Model.MatrixInitiative;
            MatrixPasses = Model.MatrixPasses;
            AstralInitiative = Model.AstralInitiative;
            AstralPasses = Model.AstralPasses;
        }

        public void CopyToModel()
        {
            Model.Name = Name;
            Model.Edge = Edge.HasValue ? Edge.Value : 0;
            Model.Reaction = Reaction.HasValue ? Reaction.Value : 0;
            Model.PhysicalInitiative = PhysicalInitiative.HasValue ? PhysicalInitiative.Value : 0;
            Model.PhysicalPasses = PhysicalPasses.HasValue ? PhysicalPasses.Value : 0;
            Model.MatrixInitiative = MatrixInitiative.HasValue ? MatrixInitiative.Value : 0;
            Model.MatrixPasses = MatrixPasses.HasValue ? MatrixPasses.Value : 0;
            Model.AstralInitiative = AstralInitiative.HasValue ? AstralInitiative.Value : 0;
            Model.AstralPasses = AstralPasses.HasValue ? AstralPasses.Value : 0;
        }

    }
}
