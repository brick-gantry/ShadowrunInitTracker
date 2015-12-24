using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public class InitPhase
    {
        public List<InitPass> Passes { get; set; } = new List<InitPass>(4);
        public int CurrentPassNumber { get; set; } = 1;
        public InitPass CurrentPass { get { return Passes[CurrentPassNumber - 1]; } }


        public enum NextResult { NextSelected, LoopBack }
        public NextResult Next()
        {
            CurrentPassNumber++;

            if(CurrentPassNumber > 4)
                CurrentPassNumber = 1;

            return (CurrentPassNumber == 1) ? NextResult.LoopBack : NextResult.NextSelected;
        }

        public void Clear()
        {
            Passes.ForEach(p => p.Clear());
        }
    }
}
