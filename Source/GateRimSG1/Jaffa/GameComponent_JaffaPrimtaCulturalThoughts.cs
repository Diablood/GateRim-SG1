using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Jaffa
{
    /// <summary>
    /// Persists which pawns have already received their first Prim'ta cultural
    /// memory. Removing and reimplanting a larva must not grant the same rite-of-
    /// passage mood bonus repeatedly.
    ///
    /// Later milestones can refine this generic prototype with faction,
    /// background and optional Ideology context.
    /// </summary>
    public class GameComponent_JaffaPrimtaCulturalThoughts : GameComponent
    {
        private List<string> pawnsGrantedReceivedPrimtaThought = new List<string>();

        public GameComponent_JaffaPrimtaCulturalThoughts(Game game)
        {
        }

        public override void ExposeData()
        {
            base.ExposeData();

            Scribe_Collections.Look(
                ref pawnsGrantedReceivedPrimtaThought,
                "pawnsGrantedReceivedPrimtaThought",
                LookMode.Value);

            if (pawnsGrantedReceivedPrimtaThought == null)
            {
                pawnsGrantedReceivedPrimtaThought = new List<string>();
            }
        }

        public bool TryGrantReceivedPrimtaThought(Pawn pawn)
        {
            if (!JaffaPrimtaUtility.IsCompatibleJaffa(pawn)
                || !JaffaPrimtaUtility.MeetsPrimtaImplantationAge(pawn)
                || pawn?.needs?.mood?.thoughts?.memories == null)
            {
                return false;
            }

            string pawnId = pawn.ThingID;

            if (string.IsNullOrEmpty(pawnId)
                || pawnsGrantedReceivedPrimtaThought.Contains(pawnId))
            {
                return false;
            }

            pawn.needs.mood.thoughts.memories.TryGainMemory(
                GR_DefOf.SG1_ReceivedPrimta);

            pawnsGrantedReceivedPrimtaThought.Add(pawnId);

            GR_Log.Message(
                $"Granted first Prim'ta cultural memory to "
                + $"{JaffaPrimtaUtility.PawnDebugLabel(pawn)}.");

            return true;
        }
    }
}
