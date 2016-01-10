using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;

namespace ShadowrunInitTracker.Model
{
    [Serializable]
    public class InitiativePass : InitiativeEntryCollection
    {
        //private int numEntriesProcessed = 0;

        public void InitialSort()
        {
            entries.Sort(new InitiativeEntryComparer());
            NotifyCollectionChanged();
        }

        public enum NextResult { NextSelected, NoneSelected }
        public NextResult Next()
        {
            if (CurrentEntry != null)
            {
                //CurrentEntry.ActionOrder = numEntriesProcessed;
                CurrentEntry.ActionTaken = true;
                CurrentEntry.PropertyChanged -= Entry_PropertyChanged;
                //numEntriesProcessed++;
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

        public void ResumeActor(ActorInitiativeEntry item)
        {
            item.DelayedActionPhase = CurrentEntry.Phase;

            entries.Insert(CurrentEntryIndex + 1, item);
            item.PropertyChanged += Entry_PropertyChanged;
            NotifyCollectionChanged();
            Next();
        }

        private void ShiftPosition(InitiativeEntry toSort)
        {
            InitiativeEntryComparer comparer = new InitiativeEntryComparer();
            int currentIndex = entries.IndexOf(toSort);
            while (currentIndex > 0 && !entries[currentIndex - 1].ActionTaken &&
                comparer.Compare(entries[currentIndex - 1], entries[currentIndex]) > 0)
            {
                var temp = entries[currentIndex];
                entries[currentIndex] = entries[currentIndex - 1];
                entries[currentIndex - 1] = temp;
                currentIndex--;
            }
            while (currentIndex < entries.Count - 1 &&
                comparer.Compare(entries[currentIndex], entries[currentIndex + 1]) > 0)
            {
                var temp = entries[currentIndex];
                entries[currentIndex] = entries[currentIndex + 1];
                entries[currentIndex + 1] = temp;
                currentIndex++;
            }
        }

        new protected void Entry_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            var entry = sender as InitiativeEntry;
            switch(e.PropertyName)
            {
                case "Phase":
                    ShiftPosition(entry);
                    break;
            }
            base.Entry_PropertyChanged(sender, e);
        }

        new public bool Remove(InitiativeEntry item)
        {
            bool currentPass = CurrentEntry != null;

            var result = base.Remove(item);
            if (result)
            {
                if (currentPass)
                    SelectNextEntry();
            }
            return result;
        }

        new public void RemoveWhereSource(object source)
        {
            bool currentPass = CurrentEntry != null;
            base.RemoveWhereSource(source);
            if(currentPass)
                SelectNextEntry();
        }
    }
}
