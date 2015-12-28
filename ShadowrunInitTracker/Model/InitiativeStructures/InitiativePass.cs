using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public class InitiativePass : List<InitiativeEntry>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        InitiativeEntry currentActor;
        public InitiativeEntry CurrentActor
        {
            get { return currentActor; }
            set
            {
                currentActor = value;
                NotifyPropertyChanged("CurrentActor");
                NotifyPropertyChanged("CurrentActorIndex");
            }
        }
        public int CurrentActorIndex { get { return this.IndexOf(CurrentActor); } }

        private int Compare(InitiativeEntry a, InitiativeEntry b)
        {
            Actor actorA = a.Source as Actor;
            Actor actorB = b.Source as Actor;
            Event eventA = a.Source as Event;
            Event eventB = b.Source as Event;

            if (a.Source is Actor)
            {
                if (b.Source is Actor)
                {
                    //critical glitches go last
                    if (actorA.InitiativeGlitch == DiceRoller.SuccessType.CriticalGlitch &&
                        actorB.InitiativeGlitch != DiceRoller.SuccessType.CriticalGlitch)
                        return 1;
                    if (actorA.InitiativeGlitch != DiceRoller.SuccessType.CriticalGlitch &&
                        actorB.InitiativeGlitch == DiceRoller.SuccessType.CriticalGlitch)
                        return -1;


                    //core sorting logic
                    if (actorA.InitiativeScore != actorB.InitiativeScore)
                        return actorB.InitiativeScore - actorA.InitiativeScore;

                    //glitches go after non glitches
                    if (actorA.InitiativeGlitch == DiceRoller.SuccessType.Glitch &&
                        actorB.InitiativeGlitch == DiceRoller.SuccessType.NoGlitch)
                        return 1;
                    if (actorA.InitiativeGlitch == DiceRoller.SuccessType.NoGlitch &&
                        actorB.InitiativeGlitch == DiceRoller.SuccessType.Glitch)
                        return -1;

                    //tie breaking
                    if (actorA.Edge != actorB.Edge)
                        return actorB.Edge - actorA.Edge;
                    if (actorA.TurnInitiativeAttribute != actorB.TurnInitiativeAttribute)
                        return actorB.TurnInitiativeAttribute - actorA.TurnInitiativeAttribute;
                    if (actorA.Reaction != actorB.Reaction)
                        return actorB.Reaction - actorA.Reaction;
                }
                else if (b.Source is Event)
                {
                    return actorB.InitiativeScore - eventA.Phase;
                }
            }
            if (a.Source is Event)
            {
                if (b.Source is Actor)
                {
                    return eventB.Phase - actorA.InitiativeScore;
                }
                else if (b.Source is Event)
                {
                    return eventB.Phase - eventA.Phase;
                }
            }
            return 0;
        }

        new public void Sort()
        {
            Sort(Compare);

            NotifyCollectionChanged(NotifyCollectionChangedAction.Move);
        }

        public enum NextResult { NextSelected, NoneSelected }
        public NextResult Next()
        {
            CurrentActor.ActionTaken = true;

            CurrentActor = null;
            for (int i = 0; i < Count; i++)
            {
                if(!this[i].ActionTaken)
                {
                    CurrentActor = this[i];
                    break;
                }
            }

            return (CurrentActor == null) ? NextResult.NoneSelected : NextResult.NextSelected;
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public void NotifyCollectionChanged(NotifyCollectionChangedAction action)
        {
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(action));
        }
    }
}
