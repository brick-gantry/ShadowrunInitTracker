using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShadowrunInitTracker.Model
{
    public static class DiceRoller
    {
        public enum SuccessType { NoGlitch, Glitch, CriticalGlitch }

        public class Args
        {
            public int NumDice { get; set; } = 0;
            public bool UsingEdge { get; set; } = false;
            public bool Hit4 { get; set; } = false;
            public bool Glitch2 { get; set; } = false;
        }

        public class Result
        {
            public int Hits { get; set; }
            public SuccessType Success { get; set; }
        }

        static Random r = new Random();

        public static Result Roll(Args args)
        {
            Result output = new Result();

            int countGlitch = 0;
            int countHits = 0;

            for (int i = 0; i < args.NumDice; i++)
            {
                switch (r.Next(5) + 1)
                {
                    case 1:
                        countGlitch++;
                        break;
                    case 2:
                        if (args.Glitch2)
                            countGlitch++;
                        break;
                    case 3:
                        break;
                    case 4:
                        if (args.Hit4)
                            countHits++;
                        break;
                    case 5:
                        countHits++;
                        break;
                    case 6:
                        countHits++;
                        args.NumDice++;
                        break;
                }
            }
            
            bool glitch = (countGlitch >= Math.Ceiling((double)args.NumDice / 2));

            return new Result
            {
                Hits = countHits,
                Success = !glitch ? SuccessType.NoGlitch : countHits > 0 ? SuccessType.Glitch : SuccessType.CriticalGlitch
            };
        }
    }
}
