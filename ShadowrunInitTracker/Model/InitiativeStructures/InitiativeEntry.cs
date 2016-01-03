using System;
using System.ComponentModel;

namespace ShadowrunInitTracker.Model
{
    [Serializable]
    public abstract class InitiativeEntry : INotifyPropertyChanged
    {
        public abstract INotifyPropertyChanged Source { get; }

        int actionOrder = int.MaxValue;
        public int ActionOrder
        {
            get { return actionOrder; }
            set
            {
                actionOrder = value;
                NotifyPropertyChanged("ActionOrder");
                NotifyPropertyChanged("ActionTaken");
                NotifyPropertyChanged("Phase");
            }
        }

        bool actionTaken = false;
        public bool ActionTaken
        {
            get { return actionOrder != int.MaxValue; }
        }

        public abstract string Description { get; }
        public abstract string PhaseDescription { get; }
        public abstract int Phase { get; }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
