using System;

namespace ShadowrunInitTracker.Model
{
    [Serializable]
    public class CombatInstance
    {
        public ActorCollection Actors { get; set; } = new ActorCollection();
        public InitiativePass DelayingActors { get; set; } = new InitiativePass();
        public InitiativePass AllActors { get; set; } = new InitiativePass();
        public EventCollection Events { get; set; } = new EventCollection();

        public InitiativeTurn CurrentTurn { get; } = new InitiativeTurn();
        public int CurrentTurnNumber { get { return CurrentTurn.TurnNumber; } }
        public InitiativePass CurrentPass { get { return CurrentTurn.CurrentPass; } }
        public InitiativeEntry CurrentEntry { get { return CurrentPass.CurrentEntry; } }
        
        public enum NextResult { NextActorFound, NoMoreActors }
        public NextResult Next()
        {
            var result = CurrentTurn.Next();
            if(result == InitiativeTurn.NextResult.LoopBack)
            {
                CurrentTurn.Clear();
                foreach (var a in Actors)
                    a.UpdateForNextTurn();
                return NextResult.NoMoreActors;
            }
            return NextResult.NextActorFound;
        }

        public class Time
        {
            public int Turn { get; set; }
            public int Pass { get; set; }
            public int Phase { get; set; }
        }

        public Time Now
        {
            get
            {
                return new Time
                {
                    Turn = CurrentTurn.TurnNumber,
                    Pass = CurrentTurn.CurrentPassNumber,
                    Phase = CurrentTurn.CurrentPass.CurrentEntry.Phase
                };
            }
        }

        public void Reset()
        {
            CurrentTurn.Clear();
            Actors.Clear();
            DelayingActors.Clear();
            Events.Clear();
        }

        public void BuildInit()
        {
            for (int pass = 1; pass <= 4; pass++)
            {
                foreach (var a in Actors.GetActorsForPass(pass))
                    CurrentTurn.Passes[pass].Add(new ActorInitiativeEntry(a));
                foreach (var e in Events.GetEventsForPass(CurrentTurnNumber, pass))
                    CurrentTurn.Passes[pass].Add(new EventInitiativeEntry(e));
            }
        }

        public void AddActor(Actor toAdd)
        {
            Actors.Add(toAdd);
            AllActors.Add(new ActorInitiativeEntry(toAdd));
            toAdd.PropertyChanged += Actor_PropertyChanged;
        }

        private void Actor_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            var changed = sender as Actor;
            switch(e.PropertyName)
            {
                case "Delaying":
                    if(changed.Delaying)
                    {
                        DelayingActors.Add(new ActorInitiativeEntry(changed));
                        foreach(var pass in CurrentTurn.Passes.Values)
                        {
                            pass.RemoveWhereSource(changed);
                        }
                    }
                    else
                    {
                        DelayingActors.RemoveWhereSource(changed);
                        CurrentTurn.CurrentPass.AddImmediate(new ActorInitiativeEntry(changed));
                        for (int pass = CurrentTurn.CurrentPassNumber+1; pass <= changed.TurnInitiativePasses; pass++)
                        {
                            CurrentTurn.Passes[pass].Add(new ActorInitiativeEntry(changed));
                        }
                    }
                    break;
            }
        }

        public void RemoveActor(Actor toRemove)
        {
            Actors.Remove(toRemove);
            AllActors.RemoveWhereSource(toRemove);
            DelayingActors.RemoveWhereSource(toRemove);
            foreach (var pass in CurrentTurn.Passes.Values)
            {
                pass.RemoveWhereSource(toRemove);
            }
        }

        public void AddEvent(Event toAdd)
        {
            Events.Add(toAdd);
            if(toAdd.Turn == CurrentTurnNumber)
            {
                CurrentTurn.Passes[toAdd.Pass].Add(new EventInitiativeEntry(toAdd));
            }
        }

        public void RemoveEvent(Event toRemove)
        {
            Events.Remove(toRemove);
            foreach (var pass in CurrentTurn.Passes.Values)
            {
                pass.RemoveWhereSource(toRemove);
            }
        }
    }
}
