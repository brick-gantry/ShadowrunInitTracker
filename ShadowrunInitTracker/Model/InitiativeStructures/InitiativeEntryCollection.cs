using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ShadowrunInitTracker.Model
{
    [Serializable]
    public class InitiativeEntryCollection : ICollection<InitiativeEntry>, INotifyPropertyChanged, INotifyCollectionChanged
    {
        protected List<InitiativeEntry> entries = new List<InitiativeEntry>();

        int currentEntryIndex = -1;
        public int CurrentEntryIndex
        {
            get { return currentEntryIndex; }
            set
            {
                currentEntryIndex = value;
                NotifyPropertyChanged("CurrentEntry");
                NotifyPropertyChanged("CurrentEntryIndex");
            }
        }
        public InitiativeEntry CurrentEntry
        {
            get { return currentEntryIndex != -1 ? entries[currentEntryIndex] : null; }
            set
            {
                if (value == null)
                    currentEntryIndex = -1;
                else
                    currentEntryIndex = entries.IndexOf(value);
                NotifyPropertyChanged("CurrentEntry");
                NotifyPropertyChanged("CurrentEntryIndex");
            }
        }
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

        protected void Entry_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NotifyCollectionChanged();
        }

        public void Add(InitiativeEntry item)
        {
            item.PropertyChanged += Entry_PropertyChanged;
            entries.Add(item);
            NotifyCollectionChanged();
        }

        public void Clear()
        {
            entries.Clear();
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
            item.PropertyChanged -= Entry_PropertyChanged;
            var result = entries.Remove(item);
            if (result)
            {
                NotifyCollectionChanged();
            }
            return result;
        }

        public IEnumerator<InitiativeEntry> GetEnumerator()
        {
            return entries.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return entries.GetEnumerator();
        }

        public void RemoveWhereSource(object source)
        {
            entries.RemoveAll(s => s.Source == source);
            NotifyCollectionChanged();
        }
    }
}
