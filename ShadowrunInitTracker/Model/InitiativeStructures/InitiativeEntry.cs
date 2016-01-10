using System;
using System.ComponentModel;

namespace ShadowrunInitTracker.Model
{
    [Serializable]
    public abstract class InitiativeEntry : INotifyPropertyChanged
    {
        public abstract INotifyPropertyChanged Source { get; }

        /*int actionOrder = int.MaxValue;
        public int ActionOrder
        {
            get { return actionOrder; }
            set
            {
                actionOrder = value;
                NotifyPropertyChanged("ActionOrder");
                NotifyPropertyChanged("Phase");
            }
        }*/

        /*public enum QueueCategory { ActionTaken, TakingAction, ResumingAction, Waiting };
        QueueCategory mode;
        public QueueCategory Mode
        {
            get { return mode; }
            set
            {

            }
        }*/


        bool actionTaken = false;
        public bool ActionTaken
        {
            get { return actionTaken; }
            set
            {
                actionTaken = value;
                NotifyPropertyChanged("ActionTaken");
            }
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

        public override string ToString()
        {
            return Description;
        }
    }
}
