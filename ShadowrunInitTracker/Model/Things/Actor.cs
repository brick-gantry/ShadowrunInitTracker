using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public enum InitMode { Physical, Matrix, Astral };

    public class Actor
    {
        public string Name { get; set; }
        public string Notes { get; set; }

        #region unchanging stats
        public class InitiativeSet
        {
            public int Value;
            public int Passes;
        }
        public InitiativeSet PhysicalInitiative, MatrixInitiative, AstralInitiative;
        public int Edge, Init, Reaction;
        #endregion

        #region changing stats
        public int RolledInit { get; set; }
        public int WoundModifier { get; set; }
        #endregion

        public InitMode CurrentMode { get; set; } = InitMode.Physical;

        public InitMode PassMode { get; set; } = InitMode.Physical;

        public InitiativeSet CurrentInitiativeSet
        {
            get
            {
                switch (CurrentMode)
                {
                    case InitMode.Astral:
                        return AstralInitiative;
                    case InitMode.Matrix:
                        return MatrixInitiative;
                    case InitMode.Physical:
                    default:
                        return PhysicalInitiative;
                }
            }
        }

        public InitiativeSet PassInitiativeSet
        {
            get; set;
        }

        public int CurrentInitiative
        {
            get
            {
                return RolledInit - WoundModifier;
            }
        }

        public int CurrentPasses
        {
            get
            {
                return PassInitiativeSet.Passes;
            }
        }

        public void UpdateForNextPhase()
        {
            PassMode = CurrentMode;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public void RollInit(DiceRoller.Args args)
        {
            args.NumDice = CurrentInitiativeSet.Value;
            if (args.UsingEdge)
                args.NumDice += Edge;
            var result = DiceRoller.Roll(args);
            RolledInit = result.Hits;

            //todo use glitches
        }
    }
}
