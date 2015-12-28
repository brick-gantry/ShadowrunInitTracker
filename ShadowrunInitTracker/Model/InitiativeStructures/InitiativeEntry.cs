using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public abstract class InitiativeEntry : INotifyPropertyChanged
    {
        public abstract object Source { get; }

        bool actionTaken = false;
        public bool ActionTaken
        {
            get { return ActionTaken; }
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
    }
}
