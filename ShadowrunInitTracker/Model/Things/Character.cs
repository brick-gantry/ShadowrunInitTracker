using System;
using System.ComponentModel;

namespace ShadowrunInitTracker.Model
{
    public enum CharacterType { PC, NPC }

    [Serializable]
    public class Character : INotifyPropertyChanged
    {
        string name;
        public string Name { get { return name; } set { name = value; NotifyPropertyChanged("Name"); } }

        CharacterType type;
        public CharacterType Type { get { return type; } set { type = value; NotifyPropertyChanged("Type"); } }

        int edge;
        public int Edge { get { return edge; } set { edge = value; NotifyPropertyChanged("Edge"); } }

        int reaction;
        public int Reaction { get { return reaction; } set { reaction = value; NotifyPropertyChanged("Reaction"); } }

        int physicalInitiative;
        public int PhysicalInitiativeAttribute { get { return physicalInitiative; } set { physicalInitiative = value; NotifyPropertyChanged("PhysicalInitiative"); } }

        int physicalPasses;
        public int PhysicalPasses { get { return physicalPasses; } set { physicalPasses = value; NotifyPropertyChanged("PhysicalPasses"); } }

        int matrixInitiative;
        public int MatrixInitiativeAttribute { get { return matrixInitiative; } set { matrixInitiative = value; NotifyPropertyChanged("MatrixInitiative"); } }

        int matrixPasses;
        public int MatrixPasses { get { return matrixPasses; } set { matrixPasses = value; NotifyPropertyChanged("MatrixPasses"); } }

        int astralInitiative;
        public int AstralInitiativeAttribute { get { return astralInitiative; } set { astralInitiative = value; NotifyPropertyChanged("AstralInitiative"); } }

        int astralPasses;
        public int AstralPasses { get { return astralPasses; } set { astralPasses = value; NotifyPropertyChanged("AstralPasses"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
