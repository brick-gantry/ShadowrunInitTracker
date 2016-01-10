using System;
using System.ComponentModel;

namespace ShadowrunInitTracker.Model
{
    [Serializable]
    public class Event : INotifyPropertyChanged
    {
        string description;
        public string Description { get { return description; } set { description = value; NotifyPropertyChanged("Description"); } }

        int turn;
        public int Turn { get { return turn; } set { turn = value; NotifyPropertyChanged("Turn"); } }

        int pass;
        public int Pass { get { return pass; } set { pass = value; NotifyPropertyChanged("Pass"); } }

        int phase;
        public int Phase { get { return phase; } set { phase = value; NotifyPropertyChanged("Phase"); } }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public override string ToString()
        {
            return Description;
        }
    }
}
