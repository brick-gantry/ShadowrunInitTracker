using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public class Actor : Character, InitiativeEntry
    {
        public int RolledInit { get; set; }
        public int WoundModifier { get; set; }

        #region mode
        public enum InitMode { Physical, Matrix, Astral }
        public InitMode CurrentMode { get; set; }
        public InitMode TurnMode { get; set; }
        #endregion

        public string Notes { get; set; }

        public int ModeInitiativeScore
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

        public int TurnInitiativeScore
        {
            get
            {
                switch (TurnMode)
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

        public int ModeInitiativePasses
        {
            get
            {
                switch (CurrentMode)
                {
                    case InitMode.Astral:
                        return AstralPasses;
                    case InitMode.Matrix:
                        return MatrixPasses;
                    case InitMode.Physical:
                    default:
                        return PhysicalPasses;
                }
            }
        }

        public int TurnInitiativePasses
        {
            get
            {
                switch (TurnMode)
                {
                    case InitMode.Astral:
                        return AstralPasses;
                    case InitMode.Matrix:
                        return MatrixPasses;
                    case InitMode.Physical:
                    default:
                        return PhysicalPasses;
                }
            }
        }

        public int CurrentInitiativePhase
        {
            get
            {
                return RolledInit - WoundModifier;
            }
        }

        public void UpdateForNextPhase()
        {
            TurnMode = CurrentMode;
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
            args.NumDice = ModeInitiativeScore;
            if (args.UsingEdge)
                args.NumDice += Edge;
            var result = DiceRoller.Roll(args);
            RolledInit = result.Hits;

            //todo use glitches
        }
    }
}
