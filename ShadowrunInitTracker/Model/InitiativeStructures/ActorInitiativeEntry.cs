using System;
using System.ComponentModel;

namespace ShadowrunInitTracker.Model
{
    [Serializable]
    public class ActorInitiativeEntry : InitiativeEntry
    {
        Actor actor;
        public override INotifyPropertyChanged Source { get { return actor; } }
        public Actor Actor { get { return actor; } }

        public ActorInitiativeEntry(Actor actor)
        {
            this.actor = actor;
            actor.PropertyChanged += Actor_PropertyChanged;
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
                case "AdjustedInitiativeScore":
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
                return string.Format("{0} ({1})", Phase, Actor.WoundModifier);
            }
        }


        public override int Phase
        {
            get
            {
                return TakingDelayedAction ? DelayedActionPhase.Value : Actor.AdjustedInitiativeScore;
            }
        }

        int? delayedActionPhase = null;
        public int? DelayedActionPhase
        {
            get { return delayedActionPhase; }
            set
            {
                delayedActionPhase = value;
                NotifyPropertyChanged("DelayedActionPhase");
                NotifyPropertyChanged("Phase");
                NotifyPropertyChanged("PhaseDescription");
            }
        }
        public bool TakingDelayedAction
        {
            get { return DelayedActionPhase.HasValue; }
        }
    }
}
