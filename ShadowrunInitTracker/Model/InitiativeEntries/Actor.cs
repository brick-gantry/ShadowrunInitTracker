namespace ShadowrunInitTracker.Model
{
    public class Actor : Character, InitiativeEntry
    {
        string notes;
        public string Notes
        {
            get { return notes; }
            set
            {
                notes = value;
                NotifyPropertyChanged("Notes");

            }
        }

        int initiativeScore;
        public int InitiativeScore
        {
            get { return initiativeScore; }
            set
            {
                initiativeScore = value;
                NotifyPropertyChanged("RolledInitiative");
                NotifyPropertyChanged("CurrentInitiativePhase");
            }
        }

        int woundModifier;
        public int WoundModifier
        {
            get { return woundModifier; }
            set
            {
                woundModifier = value;
                NotifyPropertyChanged("WoundModifier");
                NotifyPropertyChanged("CurrentInitiativePhase");
            }
        }

        public int CurrentInitiativePhase
        {
            get
            {
                return InitiativeScore + WoundModifier;
            }
        }

        #region mode
        public enum CombatMode { Physical, Matrix, Astral }
        CombatMode currentMode;
        public CombatMode CurrentMode
        {
            get { return currentMode; }
            set
            {
                currentMode = value;
                NotifyPropertyChanged("CurrentMode");
                NotifyPropertyChanged("CurrentInitiativeAttribute");
                NotifyPropertyChanged("CurrentInitiativePasses");
            }
        }
        CombatMode turnMode;
        public CombatMode TurnMode
        {
            get { return turnMode; }
            set
            {
                turnMode = value;
                NotifyPropertyChanged("TurnMode");
                NotifyPropertyChanged("TurnInitiativeAttribute");
                NotifyPropertyChanged("TurnInitiativePasses");
            }
        }
        #endregion


        public int CurrentInitiativeAttribute
        {
            get
            {
                switch (CurrentMode)
                {
                    case CombatMode.Astral:
                        return AstralInitiativeAttribute;
                    case CombatMode.Matrix:
                        return MatrixInitiativeAttribute;
                    case CombatMode.Physical:
                    default:
                        return PhysicalInitiativeAttribute;
                }
            }
        }

        public int TurnInitiativeAttribute
        {
            get
            {
                switch (TurnMode)
                {
                    case CombatMode.Astral:
                        return AstralInitiativeAttribute;
                    case CombatMode.Matrix:
                        return MatrixInitiativeAttribute;
                    case CombatMode.Physical:
                    default:
                        return PhysicalInitiativeAttribute;
                }
            }
        }

        public int CurrentInitiativePasses
        {
            get
            {
                switch (CurrentMode)
                {
                    case CombatMode.Astral:
                        return AstralPasses;
                    case CombatMode.Matrix:
                        return MatrixPasses;
                    case CombatMode.Physical:
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
                    case CombatMode.Astral:
                        return AstralPasses;
                    case CombatMode.Matrix:
                        return MatrixPasses;
                    case CombatMode.Physical:
                    default:
                        return PhysicalPasses;
                }
            }
        }

        public void UpdateForNextPhase()
        {
            TurnMode = CurrentMode;
        }

        public void RollInit(DiceRoller.Args args)
        {
            args.NumDice = CurrentInitiativeAttribute + WoundModifier;
            if (args.UsingEdge)
                args.NumDice += Edge;
            var result = DiceRoller.Roll(args);
            InitiativeScore = result.Hits;

            //todo use glitches
        }

        new void NotifyPropertyChanged(string propertyName)
        {
            base.NotifyPropertyChanged(propertyName);
        }
    }
}
