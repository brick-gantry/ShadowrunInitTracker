using System;
using System.ComponentModel;

namespace ShadowrunInitTracker.Model
{
    public enum CombatActorMode { Physical, Matrix, Astral }

    [Serializable]
    public class Actor : INotifyPropertyChanged
    {
        public Character Character;
        public Actor(Character character)
        {
            Character = character;
            Character.PropertyChanged += Character_PropertyChanged;
        }

        public Actor()
        {
            Character = new Character();
            Character.PropertyChanged += Character_PropertyChanged;
        }

        ~Actor()
        {
            Character.PropertyChanged -= Character_PropertyChanged;
        }

        private void Character_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyPropertyChanged(e.PropertyName);
        }

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
                NotifyPropertyChanged("InitiativeScore");
                NotifyPropertyChanged("AdjustedInitiativeScore");
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
                NotifyPropertyChanged("AdjustedInitiativeScore");
            }
        }

        public int AdjustedInitiativeScore
        {
            get
            {
                return InitiativeScore + WoundModifier;
            }
        }

        #region mode
        CombatActorMode currentMode;
        public CombatActorMode CurrentMode
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
        CombatActorMode turnMode;
        public CombatActorMode TurnMode
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
                    case CombatActorMode.Astral:
                        return AstralInitiativeAttribute;
                    case CombatActorMode.Matrix:
                        return MatrixInitiativeAttribute;
                    case CombatActorMode.Physical:
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
                    case CombatActorMode.Astral:
                        return AstralInitiativeAttribute;
                    case CombatActorMode.Matrix:
                        return MatrixInitiativeAttribute;
                    case CombatActorMode.Physical:
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
                    case CombatActorMode.Astral:
                        return AstralPasses;
                    case CombatActorMode.Matrix:
                        return MatrixPasses;
                    case CombatActorMode.Physical:
                    default:
                        return PhysicalPasses;
                }
            }
        }

        public int TurnInitiativePasses
        {
            get
            {
                int passes;
                switch (TurnMode)
                {
                    case CombatActorMode.Astral:
                        passes = AstralPasses;
                        break;
                    case CombatActorMode.Matrix:
                        passes = MatrixPasses;
                        break;
                    case CombatActorMode.Physical:
                    default:
                        passes = PhysicalPasses;
                        break;
                }
                if (InitiativeGlitch == GlitchType.CriticalGlitch && passes > 1)
                    passes--;
                return passes;
            }
        }

        public void UpdateForNextTurn()
        {
            TurnMode = CurrentMode;
        }

        GlitchType initiativeGlitch = GlitchType.NoGlitch;
        public GlitchType InitiativeGlitch
        {
            get { return initiativeGlitch; }
            set
            {
                initiativeGlitch = value;
                NotifyPropertyChanged("InitiativeGlitch");
            }
        }

        bool delaying = false;
        public bool Delaying
        {
            get { return delaying; }
            set
            {
                delaying = value;
                NotifyPropertyChanged("Delaying");
            }
        }

        public void RollInit(bool useEdge)
        {
            DiceRoller.Args args = new DiceRoller.Args { UsingEdge = useEdge };
            args.NumDice = TurnInitiativeAttribute + WoundModifier;
            if (args.UsingEdge)
                args.NumDice += Edge;
            var result = DiceRoller.Roll(args);
            InitiativeScore = result.Hits + TurnInitiativeAttribute;
            InitiativeGlitch = result.Glitch;
        }

        #region character stuff
        public string Name { get { return Character.Name; } set { Name = value; } }
        
        public CharacterType Type { get { return Character.Type; } set { Character.Type = value; } }
        
        public int Edge { get { return Character.Edge; } set { Character.Edge = value;  } }
        
        public int Reaction { get { return Character.Reaction; } set { Character.Reaction = value; } }
        
        public int PhysicalInitiativeAttribute { get { return Character.PhysicalInitiativeAttribute; } set { Character.PhysicalInitiativeAttribute = value; } }
        
        public int PhysicalPasses { get { return Character.PhysicalPasses; } set { Character.PhysicalPasses = value; } }
        
        public int MatrixInitiativeAttribute { get { return Character.MatrixInitiativeAttribute; } set { Character.MatrixInitiativeAttribute = value; } }
        
        public int MatrixPasses { get { return Character.MatrixPasses; } set { Character.MatrixPasses = value; } }
        
        public int AstralInitiativeAttribute { get { return Character.AstralInitiativeAttribute; } set { Character.AstralInitiativeAttribute = value; } }
        
        public int AstralPasses { get { return Character.AstralPasses; } set { Character.AstralPasses = value; } }
        #endregion

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
