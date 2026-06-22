namespace GateRimSG1.Goauld
{
    public enum TokraOrganicOperationArchetype
    {
        None = 0,
        GoauldObservation = 1,
        DeadDropRecovery = 2,
        WoundedAgentCare = 3,
        MedicalSupplyHandoff = 4,
        DistressCall = 5
    }

    public enum TokraOrganicOperationState
    {
        None = 0,
        Offered = 1,
        Accepted = 2,
        Ready = 3
    }

    public enum TokraDistressCallVariant
    {
        None = 0,
        GenuineRescue = 1,
        CompromisedSignal = 2,
        LateArrival = 3
    }

    public enum TokraDistressCallSceneType
    {
        None = 0,
        AmbushedCaravan = 1,
        TemporaryCamp = 2,
        CompromisedPosition = 3,
        OverrunCamp = 4
    }

    public enum TokraIntelligenceAnalysisMethod
    {
        None = 0,
        Cautious = 1,
        Accelerated = 2
    }
}
