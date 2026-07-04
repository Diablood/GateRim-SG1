using System.Collections.Generic;
using Verse;

namespace GateRimSG1.Goauld
{
    /// <summary>
    /// Data-driven strategic preference profile for one Goa'uld domain.
    ///
    /// The values modify only the relative weights of the three already
    /// validated natural Jaffa raid doctrines. Eligibility thresholds,
    /// incident frequency and storyteller threat points remain authoritative.
    /// </summary>
    public sealed class GoauldDomainDoctrineProfileDef : Def
    {
        public float directWeight = 2f;
        public float abductionWeight = 1f;
        public float destructionWeight = 1f;

        public GoauldJaffaRaidDoctrineWeights BaseWeights
        {
            get
            {
                return new GoauldJaffaRaidDoctrineWeights
                {
                    direct = directWeight,
                    abduction = abductionWeight,
                    destruction = destructionWeight
                };
            }
        }

        public override IEnumerable<string> ConfigErrors()
        {
            foreach (string error in base.ConfigErrors())
            {
                yield return error;
            }

            if (directWeight <= 0f)
            {
                yield return $"{defName}: directWeight must remain above zero.";
            }

            if (abductionWeight < 0f)
            {
                yield return $"{defName}: abductionWeight cannot be negative.";
            }

            if (destructionWeight < 0f)
            {
                yield return $"{defName}: destructionWeight cannot be negative.";
            }
        }
    }

    public static class GoauldDomainDoctrineProfileUtility
    {
        public const string ConquestDefName =
            "SG1_GoauldDomainDoctrine_Conquest";
        public const string EnslavementDefName =
            "SG1_GoauldDomainDoctrine_Enslavement";
        public const string ScorchedEarthDefName =
            "SG1_GoauldDomainDoctrine_ScorchedEarth";

        private static readonly string[] AssignmentOrder =
        {
            ConquestDefName,
            EnslavementDefName,
            ScorchedEarthDefName
        };

        public static List<GoauldDomainDoctrineProfileDef>
            GetAssignmentProfiles()
        {
            List<GoauldDomainDoctrineProfileDef> profiles =
                new List<GoauldDomainDoctrineProfileDef>();

            for (int index = 0; index < AssignmentOrder.Length; index++)
            {
                GoauldDomainDoctrineProfileDef profile =
                    DefDatabase<GoauldDomainDoctrineProfileDef>
                        .GetNamedSilentFail(AssignmentOrder[index]);

                if (profile != null)
                {
                    profiles.Add(profile);
                }
            }

            return profiles;
        }

        public static GoauldDomainDoctrineProfileDef GetByDefName(
            string defName)
        {
            return DefDatabase<GoauldDomainDoctrineProfileDef>
                .GetNamedSilentFail(defName);
        }

        public static bool IsAssignmentProfile(
            GoauldDomainDoctrineProfileDef profile)
        {
            if (profile == null)
            {
                return false;
            }

            for (int index = 0; index < AssignmentOrder.Length; index++)
            {
                if (profile.defName == AssignmentOrder[index])
                {
                    return true;
                }
            }

            return false;
        }

        public static string GetDisplayLabel(
            GoauldDomainDoctrineProfileDef profile)
        {
            switch (profile?.defName)
            {
                case ConquestDefName:
                    return "GR_GoauldDomainDoctrine_Conquest_Label"
                        .Translate()
                        .ToString();

                case EnslavementDefName:
                    return "GR_GoauldDomainDoctrine_Enslavement_Label"
                        .Translate()
                        .ToString();

                case ScorchedEarthDefName:
                    return "GR_GoauldDomainDoctrine_ScorchedEarth_Label"
                        .Translate()
                        .ToString();

                default:
                    return profile?.LabelCap.ToString()
                        ?? "GR_GoauldDomainDoctrine_Unknown_Label"
                            .Translate()
                            .ToString();
            }
        }

        public static string GetDisplayDescription(
            GoauldDomainDoctrineProfileDef profile)
        {
            switch (profile?.defName)
            {
                case ConquestDefName:
                    return "GR_GoauldDomainDoctrine_Conquest_Description"
                        .Translate()
                        .ToString();

                case EnslavementDefName:
                    return "GR_GoauldDomainDoctrine_Enslavement_Description"
                        .Translate()
                        .ToString();

                case ScorchedEarthDefName:
                    return "GR_GoauldDomainDoctrine_ScorchedEarth_Description"
                        .Translate()
                        .ToString();

                default:
                    return profile?.description
                        ?? "GR_GoauldDomainDoctrine_Unknown_Description"
                            .Translate()
                            .ToString();
            }
        }
    }
}
