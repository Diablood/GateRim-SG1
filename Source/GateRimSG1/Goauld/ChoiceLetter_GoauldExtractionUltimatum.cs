using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    public sealed class ChoiceLetter_GoauldExtractionUltimatum
        : ChoiceLetter
    {
        private Faction domainFaction;

        public Faction DomainFaction => domainFaction;

        public override bool CanDismissWithRightClick => false;

        public override IEnumerable<DiaOption> Choices
        {
            get
            {
                if (!GameComponent_GoauldDomainReprisalTracker
                    .IsCurrentUltimatum(domainFaction))
                {
                    yield return Option_Close;
                    yield break;
                }

                DiaOption surrender = new DiaOption(
                    "GR_GoauldDomainUltimatum_SurrenderAction".Translate());

                if (!GameComponent_GoauldDomainReprisalTracker
                    .CanSurrenderDemandedSymbiote(
                        domainFaction,
                        out string disabledReason))
                {
                    surrender.Disable(disabledReason);
                }
                else
                {
                    surrender.action = delegate
                    {
                        GameComponent_GoauldDomainReprisalTracker
                            .SurrenderFromLetter(domainFaction);
                    };
                    surrender.resolveTree = true;
                }

                yield return surrender;

                DiaOption defy = new DiaOption(
                    "GR_GoauldDomainUltimatum_DefyAction".Translate());
                defy.action = delegate
                {
                    GameComponent_GoauldDomainReprisalTracker
                        .DefyFromLetter(domainFaction);
                };
                defy.resolveTree = true;
                yield return defy;

                DiaOption postpone = Option_Postpone;
                postpone.SetText(
                    "GR_GoauldDomainUltimatum_PostponeAction".Translate());
                yield return postpone;
            }
        }

        public void Initialize(Faction faction)
        {
            domainFaction = faction;
        }

        public override void ExposeData()
        {
            base.ExposeData();
            Scribe_References.Look(
                ref domainFaction,
                "goauldUltimatumDomainFaction");
        }
    }
}
