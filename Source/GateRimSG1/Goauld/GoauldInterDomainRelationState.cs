using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public enum GoauldInterDomainRelation
    {
        Neutral,
        Rivalry,
        OpenConflict,
        Truce,
        Alliance
    }

    public sealed class GoauldInterDomainRelationState : IExposable
    {
        public Faction firstDomain;
        public Faction secondDomain;
        public GoauldInterDomainRelation relation =
            GoauldInterDomainRelation.Neutral;
        public GoauldInterDomainRelation previousRelation =
            GoauldInterDomainRelation.Neutral;
        public int establishedTick;
        public int lastTransitionTick = -1;
        public int nextTransitionTick;
        public int transitionCount;

        public void ExposeData()
        {
            Scribe_References.Look(ref firstDomain, "firstDomain");
            Scribe_References.Look(ref secondDomain, "secondDomain");
            int relationValue = (int)relation;
            int previousRelationValue = (int)previousRelation;
            Scribe_Values.Look(
                ref relationValue,
                "relation",
                (int)GoauldInterDomainRelation.Neutral);
            Scribe_Values.Look(
                ref previousRelationValue,
                "previousRelation",
                (int)GoauldInterDomainRelation.Neutral);
            relation = (GoauldInterDomainRelation)relationValue;
            previousRelation =
                (GoauldInterDomainRelation)previousRelationValue;
            Scribe_Values.Look(ref establishedTick, "establishedTick", 0);
            Scribe_Values.Look(
                ref lastTransitionTick,
                "lastTransitionTick",
                -1);
            Scribe_Values.Look(
                ref nextTransitionTick,
                "nextTransitionTick",
                0);
            Scribe_Values.Look(ref transitionCount, "transitionCount", 0);
        }
    }
}
