using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ShadowrunInitTracker.Model
{
    [Serializable]
    public class InitiativePass : ICollection<InitiativeEntry>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        private List<InitiativeEntry> entries = new List<InitiativeEntry>();
        private int numEntriesProcessed = 0;

        InitiativeEntry currentEntry;
        public InitiativeEntry CurrentEntry
        {
            get { return currentEntry; }
            set
            {
                currentEntry = value;
                NotifyPropertyChanged("CurrentEntry");
                NotifyPropertyChanged("CurrentEntryIndex");
            }
        }
        public int CurrentEntryIndex { get { return entries.IndexOf(CurrentEntry); } }
        public InitiativeEntry this[int key]
        {
            get { return entries[key]; }
            set { entries[key] = value; }
        }

        public int Count
        {
            get
            {
                return entries.Count;
            }
        }

        public bool IsReadOnly
        {
            get
            {
                return false;
            }
        }

        private int Compare(InitiativeEntry a, InitiativeEntry b)
        {
            if(a.ActionOrder != b.ActionOrder)
                return a.ActionOrder - b.ActionOrder;

            Actor actorA = a.Source as Actor;
            Actor actorB = b.Source as Actor;
            Event eventA = a.Source as Event;
            Event eventB = b.Source as Event;

            if (a.Source is Actor)
            {
                if (b.Source is Actor)
                {
                    //critical glitches go last
                    if (actorA.InitiativeGlitch == GlitchType.CriticalGlitch &&
                        actorB.InitiativeGlitch != GlitchType.CriticalGlitch)
                        return 1;
                    if (actorA.InitiativeGlitch != GlitchType.CriticalGlitch &&
                        actorB.InitiativeGlitch == GlitchType.CriticalGlitch)
                        return -1;
                    
                    //core sorting logic
                    if (actorA.InitiativeScore != actorB.InitiativeScore)
                        return actorB.InitiativeScore - actorA.InitiativeScore;

                    //glitches go after non glitches
                    if (actorA.InitiativeGlitch == GlitchType.Glitch &&
                        actorB.InitiativeGlitch == GlitchType.NoGlitch)
                        return 1;
                    if (actorA.InitiativeGlitch == GlitchType.NoGlitch &&
                        actorB.InitiativeGlitch == GlitchType.Glitch)
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

        public enum NextResult { NextSelected, NoneSelected }
        public NextResult Next()
        {
            if (CurrentEntry != null)
            {
                CurrentEntry.ActionOrder = numEntriesProcessed;
                numEntriesProcessed++;
            }
            SelectNextEntry();
            return (CurrentEntry == null) ? NextResult.NoneSelected : NextResult.NextSelected;
        }

        private void SelectNextEntry()
        {
            CurrentEntry = null;
            for (int i = 0; i < Count; i++)
            {
                if (!this[i].ActionTaken)
                {
                    CurrentEntry = this[i];
                    break;
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged;
        public void NotifyCollectionChanged()
        {
            if (CollectionChanged != null)
                CollectionChanged(this, new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        public void Add(InitiativeEntry item)
        {
            entries.Add(item);
            item.PropertyChanged += Entry_PropertyChanged;
            entries.Sort(Compare);
            NotifyCollectionChanged();
        }

        public void AddImmediate(ActorInitiativeEntry item)
        {
            item.ActionOrder = numEntriesProcessed;
            CurrentEntry = item;
            entries.Add(item);
            item.PropertyChanged += Entry_PropertyChanged;
            entries.Sort(Compare);
            NotifyCollectionChanged();
        }

        private void Entry_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var entry = sender as InitiativeEntry;
            switch(e.PropertyName)
            {
                case "Phase":
                    entries.Sort(Compare);
                    NotifyCollectionChanged();
                    break;
            }
        }

        public void Clear()
        {
            entries.Clear();
            numEntriesProcessed = 0;
            NotifyCollectionChanged();
        }

        public bool Contains(InitiativeEntry item)
        {
            return entries.Contains(item);
        }

        public void CopyTo(InitiativeEntry[] array, int arrayIndex)
        {
            entries.CopyTo(array, arrayIndex);
        }

        public bool Remove(InitiativeEntry item)
        {
            var result = entries.Remove(item);
            if (result)
            {
                NotifyCollectionChanged();
                if (CurrentEntry == item)
                    SelectNextEntry();
            }
            return result;
        }

        public void RemoveWhereSource(object source)
        {
            entries.RemoveAll(s => s.Source == source);
            NotifyCollectionChanged();
            if (CurrentEntry != null && CurrentEntry.Source == source)
                SelectNextEntry();
        }

        public IEnumerator<InitiativeEntry> GetEnumerator()
        {
            return entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return entries.GetEnumerator();
        }
    }
}
