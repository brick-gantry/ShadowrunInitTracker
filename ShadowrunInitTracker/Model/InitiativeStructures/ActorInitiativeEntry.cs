using System;
using System.ComponentModel;

namespace ShadowrunInitTracker.Model
{
    [Serializable]
    public class ActorInitiativeEntry : InitiativeEntry
    {
        Actor Actor;
        public override INotifyPropertyChanged Source { get { return Actor; } }

        public ActorInitiativeEntry(Actor actor)
        {
            Actor = actor;
            Actor.PropertyChanged += Actor_PropertyChanged;
        }

        ~ActorInitiativeEntry()
        {
            Actor.PropertyChanged -= Actor_PropertyChanged;
        }

        private void Actor_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Name":
                case "CurrentMode":
                    NotifyPropertyChanged("Description");
                    break;
                case "CurrentInitiativePhase":
                    NotifyPropertyChanged("Phase");
                    NotifyPropertyChanged("PhaseDescripton");
                    break;
            }
        }

        public override string Description
        {
            get
            {
                return string.Format("{0} ({1})", Actor.Name, Actor.CurrentMode);
            }
        }

        public override string PhaseDescription
        {
            get
            {
                if(Actor.InitiativeGlitch == GlitchType.CriticalGlitch)
                    return string.Format("{0} ({1})", "CG", Actor.WoundModifier);
                return string.Format("{0} ({1})", Actor.AdjustedInitiativeScore, Actor.WoundModifier);
            }
        }

        public override int Phase
        {
            get
            {
                return Actor.AdjustedInitiativeScore;
            }
        }
    }
}
