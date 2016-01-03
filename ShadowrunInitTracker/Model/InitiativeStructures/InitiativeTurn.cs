using System;
using System.Collections.Generic;
using System.Linq;

namespace ShadowrunInitTracker.Model
{
    [Serializable]
    public class InitiativeTurn
    {
        public int TurnNumber { get; set; } = 1;

        public Dictionary<int, InitiativePass> Passes { get; } = 
            new Dictionary<int, InitiativePass> {
                { 1, new InitiativePass() },
                { 2, new InitiativePass() },
                { 3, new InitiativePass() },
                { 4, new InitiativePass() } };
        public int CurrentPassNumber { get; set; } = 1;
        public InitiativePass CurrentPass { get { return Passes[CurrentPassNumber]; } }
        
        public enum NextResult { NextSelected, LoopBack }
        public NextResult Next()
        {
            var result = CurrentPass.Next();
            switch (result)
            {
                case InitiativePass.NextResult.NextSelected:
                    return NextResult.NextSelected;
                case InitiativePass.NextResult.NoneSelected:
                default:
                    CurrentPassNumber++;

                    if (CurrentPassNumber > 4)
                    {
                        CurrentPassNumber = 1;
                        TurnNumber++;
                        return NextResult.LoopBack;
                    }
                    return Next();
            }
        }

        public void Clear()
        {
            Passes.Values.ToList().ForEach(p => p.Clear());
            CurrentPassNumber = 1;
            TurnNumber = 1;
        }
    }
}
