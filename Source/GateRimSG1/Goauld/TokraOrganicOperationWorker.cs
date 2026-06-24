using System.Collections.Generic;
using RimWorld;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Operation-specific behavior plugged into the shared manager.
    /// </summary>
    internal abstract class TokraOrganicOperationWorker
    {
        public abstract TokraOrganicOperationArchetype Archetype { get; }

        public abstract void Tick(
            GameComponent_TokraOrganicOperationManager manager,
            int currentTick);

        public abstract bool TryAccept(
            GameComponent_TokraOrganicOperationManager manager,
            Map map,
            Pawn operatorPawn);

        public virtual bool TryHandleCommunicatorCompletion(
            GameComponent_TokraOrganicOperationManager manager,
            Pawn operatorPawn)
        {
            return false;
        }
    }

    internal sealed class TokraOrganicOperationWorker_GoauldObservation
        : TokraOrganicOperationWorker
    {
        public override TokraOrganicOperationArchetype Archetype
            => TokraOrganicOperationArchetype.GoauldObservation;

        public override void Tick(
            GameComponent_TokraOrganicOperationManager manager,
            int currentTick)
        {
            manager.TickAcceptedObservation(currentTick);
        }

        public override bool TryAccept(
            GameComponent_TokraOrganicOperationManager manager,
            Map map,
            Pawn operatorPawn)
        {
            return manager.TryAcceptObservationOperation(map, operatorPawn);
        }

        public override bool TryHandleCommunicatorCompletion(
            GameComponent_TokraOrganicOperationManager manager,
            Pawn operatorPawn)
        {
            manager.UpdateObservationReadyState(
                Find.TickManager?.TicksGame ?? 0,
                notifyPlayer: false);

            return manager.ActiveState == TokraOrganicOperationState.Ready
                && manager.TryStartObservationTransmission(operatorPawn);
        }
    }

    internal sealed class TokraOrganicOperationWorker_DeadDropRecovery
        : TokraOrganicOperationWorker
    {
        public override TokraOrganicOperationArchetype Archetype
            => TokraOrganicOperationArchetype.DeadDropRecovery;

        public override void Tick(
            GameComponent_TokraOrganicOperationManager manager,
            int currentTick)
        {
            manager.TickAcceptedPhysicalObjective(currentTick);
        }

        public override bool TryAccept(
            GameComponent_TokraOrganicOperationManager manager,
            Map map,
            Pawn operatorPawn)
        {
            return manager.TryAcceptPhysicalObjectiveOperation(
                map,
                operatorPawn);
        }

        public override bool TryHandleCommunicatorCompletion(
            GameComponent_TokraOrganicOperationManager manager,
            Pawn operatorPawn)
        {
            return manager.TryOpenIntelligenceAnalysis(operatorPawn);
        }
    }

    internal sealed class TokraOrganicOperationWorker_WoundedAgentCare
        : TokraOrganicOperationWorker
    {
        public override TokraOrganicOperationArchetype Archetype
            => TokraOrganicOperationArchetype.WoundedAgentCare;

        public override void Tick(
            GameComponent_TokraOrganicOperationManager manager,
            int currentTick)
        {
            manager.TickAcceptedWoundedAgent(currentTick);
        }

        public override bool TryAccept(
            GameComponent_TokraOrganicOperationManager manager,
            Map map,
            Pawn operatorPawn)
        {
            return manager.TryAcceptWoundedAgentCare(map, operatorPawn);
        }
    }

    internal sealed class TokraOrganicOperationWorker_MedicalSupplyHandoff
        : TokraOrganicOperationWorker
    {
        public override TokraOrganicOperationArchetype Archetype
            => TokraOrganicOperationArchetype.MedicalSupplyHandoff;

        public override void Tick(
            GameComponent_TokraOrganicOperationManager manager,
            int currentTick)
        {
            manager.TickAcceptedMedicalSupply(currentTick);
        }

        public override bool TryAccept(
            GameComponent_TokraOrganicOperationManager manager,
            Map map,
            Pawn operatorPawn)
        {
            return manager.TryAcceptMedicalSupplyHandoff(map, operatorPawn);
        }
    }

    internal sealed class TokraOrganicOperationWorker_DistressCall
        : TokraOrganicOperationWorker
    {
        public override TokraOrganicOperationArchetype Archetype
            => TokraOrganicOperationArchetype.DistressCall;

        public override void Tick(
            GameComponent_TokraOrganicOperationManager manager,
            int currentTick)
        {
            manager.TickAcceptedDistressCall(currentTick);
        }

        public override bool TryAccept(
            GameComponent_TokraOrganicOperationManager manager,
            Map map,
            Pawn operatorPawn)
        {
            return manager.TryAcceptDistressCall(map, operatorPawn);
        }
    }


    internal sealed class TokraOrganicOperationWorker_TemporaryBaseDelivery
        : TokraOrganicOperationWorker
    {
        public override TokraOrganicOperationArchetype Archetype
            => TokraOrganicOperationArchetype.TemporaryBaseDelivery;

        public override void Tick(
            GameComponent_TokraOrganicOperationManager manager,
            int currentTick)
        {
            manager.TickAcceptedTemporaryBaseDelivery(currentTick);
        }

        public override bool TryAccept(
            GameComponent_TokraOrganicOperationManager manager,
            Map map,
            Pawn operatorPawn)
        {
            return manager.TryAcceptTemporaryBaseDelivery(
                map,
                operatorPawn);
        }
    }


    internal sealed class TokraOrganicOperationWorker_DiversionAssault
        : TokraOrganicOperationWorker
    {
        public override TokraOrganicOperationArchetype Archetype
            => TokraOrganicOperationArchetype.DecoyTransmissionDefense;

        public override void Tick(
            GameComponent_TokraOrganicOperationManager manager,
            int currentTick)
        {
            manager.TickAcceptedDiversionAssault(currentTick);
        }

        public override bool TryAccept(
            GameComponent_TokraOrganicOperationManager manager,
            Map map,
            Pawn operatorPawn)
        {
            return manager.TryAcceptDiversionAssault(
                map,
                operatorPawn);
        }
    }

    internal sealed class TokraOrganicOperationWorker_JaffaOfficerCapture
        : TokraOrganicOperationWorker
    {
        public override TokraOrganicOperationArchetype Archetype
            => TokraOrganicOperationArchetype.JaffaOfficerCapture;

        public override void Tick(
            GameComponent_TokraOrganicOperationManager manager,
            int currentTick)
        {
            manager.TickAcceptedJaffaOfficerCapture(currentTick);
        }

        public override bool TryAccept(
            GameComponent_TokraOrganicOperationManager manager,
            Map map,
            Pawn operatorPawn)
        {
            return manager.TryAcceptJaffaOfficerCapture(map, operatorPawn);
        }

        public override bool TryHandleCommunicatorCompletion(
            GameComponent_TokraOrganicOperationManager manager,
            Pawn operatorPawn)
        {
            return manager.TryRequestJaffaOfficerExtraction(operatorPawn);
        }
    }


    internal static class TokraOrganicOperationWorkerRegistry
    {
        private static readonly IReadOnlyDictionary<
            TokraOrganicOperationArchetype,
            TokraOrganicOperationWorker> Workers
                = new Dictionary<
                    TokraOrganicOperationArchetype,
                    TokraOrganicOperationWorker>
                {
                    {
                        TokraOrganicOperationArchetype.GoauldObservation,
                        new TokraOrganicOperationWorker_GoauldObservation()
                    },
                    {
                        TokraOrganicOperationArchetype.DeadDropRecovery,
                        new TokraOrganicOperationWorker_DeadDropRecovery()
                    },
                    {
                        TokraOrganicOperationArchetype.WoundedAgentCare,
                        new TokraOrganicOperationWorker_WoundedAgentCare()
                    },
                    {
                        TokraOrganicOperationArchetype.MedicalSupplyHandoff,
                        new TokraOrganicOperationWorker_MedicalSupplyHandoff()
                    },
                    {
                        TokraOrganicOperationArchetype.DistressCall,
                        new TokraOrganicOperationWorker_DistressCall()
                    },
                    {
                        TokraOrganicOperationArchetype.TemporaryBaseDelivery,
                        new TokraOrganicOperationWorker_TemporaryBaseDelivery()
                    },
                    {
                        TokraOrganicOperationArchetype.DecoyTransmissionDefense,
                        new TokraOrganicOperationWorker_DiversionAssault()
                    },
                    {
                        TokraOrganicOperationArchetype.JaffaOfficerCapture,
                        new TokraOrganicOperationWorker_JaffaOfficerCapture()
                    }
                };

        public static TokraOrganicOperationWorker Get(
            TokraOrganicOperationArchetype archetype)
        {
            TokraOrganicOperationWorker worker;

            return Workers.TryGetValue(archetype, out worker)
                ? worker
                : null;
        }
    }
}
