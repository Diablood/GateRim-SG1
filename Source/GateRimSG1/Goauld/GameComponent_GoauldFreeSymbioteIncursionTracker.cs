using System.Collections.Generic;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Keeps the last visible incursion-letter variant across save and reload
    /// so recurrent Goa'uld symbiote incursions do not immediately repeat the
    /// same RP text.
    /// </summary>
    public sealed class GameComponent_GoauldFreeSymbioteIncursionTracker
        : GameComponent
    {
        private int lastLetterVariant = -1;

        public GameComponent_GoauldFreeSymbioteIncursionTracker(Game game)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_Values.Look(
                ref lastLetterVariant,
                "goauldFreeSymbioteIncursionLastLetterVariant",
                -1);
        }

        public int SelectLetterVariant(int variantCount)
        {
            if (variantCount <= 1)
            {
                lastLetterVariant = 0;
                return 0;
            }

            List<int> candidates = new List<int>(variantCount);

            for (int index = 0; index < variantCount; index++)
            {
                if (index != lastLetterVariant)
                {
                    candidates.Add(index);
                }
            }

            int selected = candidates[Rand.Range(0, candidates.Count)];
            lastLetterVariant = selected;
            return selected;
        }

        public static GameComponent_GoauldFreeSymbioteIncursionTracker Current
        {
            get
            {
                return Verse.Current.Game
                    ?.GetComponent<GameComponent_GoauldFreeSymbioteIncursionTracker>();
            }
        }
    }
}
