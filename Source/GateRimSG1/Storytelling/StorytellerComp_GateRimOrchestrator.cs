
using RimWorld;

namespace GateRimSG1.Storytelling
{
    /// <summary>
    /// Marker component for GateRim-specific storyteller orchestration.
    ///
    /// The foundation deliberately emits no incidents. Future milestones can
    /// extend this component while other storytellers remain untouched.
    /// </summary>
    public sealed class StorytellerComp_GateRimOrchestrator
        : StorytellerComp
    {
    }

    public sealed class StorytellerCompProperties_GateRimOrchestrator
        : StorytellerCompProperties
    {
        public StorytellerCompProperties_GateRimOrchestrator()
        {
            compClass = typeof(StorytellerComp_GateRimOrchestrator);
        }
    }
}
