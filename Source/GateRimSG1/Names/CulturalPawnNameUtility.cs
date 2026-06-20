using System.Collections.Generic;
using System.Text;
using GateRimSG1.Goauld;
using Verse;

namespace GateRimSG1.Names
{
    public enum CulturalPawnNameGroup
    {
        None,
        GoauldJaffa,
        FreeJaffa,
        Goauld,
        Tokra,
        Tauri,
        OffworldHuman
    }

    /// <summary>
    /// Produces original culture-specific names without depending on vanilla
    /// faction name banks. The combinatorial pools are deliberately broad so
    /// repeated incidents remain varied during long games.
    /// </summary>
    public static class CulturalPawnNameUtility
    {
        private static readonly string[] GoauldJaffaPrefixes =
        {
            "Ara", "Bra", "Cala", "Dra", "Ena", "Gor", "Ha", "Jora",
            "Ka", "Kra", "Lora", "Ma", "Nara", "Ota", "Pra", "Qua",
            "Rha", "Sha", "Ta", "Tora", "Va", "Vora", "Zan", "Zha"
        };

        private static readonly string[] GoauldJaffaSuffixes =
        {
            "c", "dar", "gar", "kal", "kar", "kel", "kor", "lak",
            "mak", "nar", "noc", "rak", "rel", "ron", "tal", "tar",
            "tek", "tok", "var", "vek", "zor", "mir", "shan", "kesh"
        };

        private static readonly string[] FreeJaffaPrefixes =
        {
            "Ari", "Bela", "Cora", "Dai", "Eri", "Fara", "Gana", "Hala",
            "Ira", "Jani", "Kera", "Lani", "Mera", "Nali", "Ora", "Pera",
            "Rani", "Sela", "Tari", "Ura", "Vela", "Yara", "Zari", "Kori"
        };

        private static readonly string[] FreeJaffaSuffixes =
        {
            "an", "ar", "as", "en", "eth", "in", "ir", "is", "on", "or",
            "ra", "ren", "rin", "sha", "tal", "tan", "var", "ven", "ya", "zar",
            "kan", "mel", "nor", "vek"
        };

        private static readonly string[] GoauldPrefixes =
        {
            "Amon", "Anub", "Aresh", "Bast", "Chera", "Dagon", "Eset", "Hakar",
            "Ishar", "Khepra", "Maat", "Nefra", "Nekar", "Orun", "Pta", "Qet",
            "Rahem", "Seker", "Sethra", "Tef", "Uaset", "Voran", "Yaret", "Zekar"
        };

        private static readonly string[] GoauldSuffixes =
        {
            "ak", "amon", "aris", "atek", "em", "eris", "esh", "et", "hotep", "is",
            "kar", "khet", "mon", "nak", "on", "oris", "ra", "rek", "set", "tek",
            "this", "un", "var", "zar"
        };

        private static readonly string[] TokraPrefixes =
        {
            "Aela", "Ari", "Bel", "Cera", "Dela", "Eli", "Fara", "Galen",
            "Hera", "Ilan", "Jora", "Kelan", "Lira", "Mara", "Nela", "Oren",
            "Pela", "Rian", "Sela", "Taren", "Ulan", "Vera", "Yarin", "Zora"
        };

        private static readonly string[] TokraSuffixes =
        {
            "an", "ar", "as", "el", "en", "eth", "ia", "iel", "in", "ir",
            "is", "on", "or", "ra", "ren", "reth", "ril", "tal", "van", "ven",
            "ya", "yel", "zen", "zar"
        };

        private static readonly string[] OffworldHumanPrefixes =
        {
            "Ala", "Bera", "Cali", "Dara", "Eli", "Fara", "Galen", "Hara",
            "Ilan", "Jera", "Kara", "Loran", "Mera", "Nerin", "Ora", "Palen",
            "Rana", "Saren", "Tala", "Ulan", "Vara", "Yaren", "Zela", "Korin"
        };

        private static readonly string[] OffworldHumanSuffixes =
        {
            "an", "ar", "as", "el", "en", "eth", "ia", "in", "ir", "is",
            "on", "or", "ra", "ren", "ria", "ril", "tan", "var", "ven", "ya",
            "iel", "nor", "sel", "tek"
        };

        private static readonly string[] TauriMaleFirstNames =
        {
            "Aaron", "Adam", "Adrian", "Alex", "Andre", "Ben", "Caleb", "Daniel",
            "David", "Elias", "Ethan", "Felix", "Gabriel", "Hugo", "Ian", "Isaac",
            "Jonas", "Julian", "Leon", "Liam", "Lucas", "Marc", "Mateo", "Nathan",
            "Noah", "Oliver", "Owen", "Paul", "Rafael", "Samuel", "Thomas", "Victor"
        };

        private static readonly string[] TauriFemaleFirstNames =
        {
            "Ada", "Aisha", "Alice", "Amara", "Anna", "Camille", "Clara", "Diana",
            "Elena", "Emma", "Eva", "Farah", "Grace", "Hana", "Iris", "Julia",
            "Leah", "Lena", "Mara", "Maya", "Nadia", "Naomi", "Nora", "Olivia",
            "Rina", "Sara", "Sofia", "Tara", "Valerie", "Vera", "Yasmin", "Zoe"
        };

        private static readonly string[] TauriNeutralFirstNames =
        {
            "Alex", "Ari", "Casey", "Charlie", "Drew", "Emery", "Jamie", "Jordan",
            "Morgan", "Quinn", "Reese", "Riley", "Robin", "Rowan", "Sam", "Taylor"
        };

        private static readonly string[] TauriLastNames =
        {
            "Alvarez", "Andersen", "Bauer", "Bennett", "Carver", "Chen", "Cole", "Costa",
            "Dubois", "Fischer", "Garcia", "Grant", "Haddad", "Hansen", "Hughes", "Ivanov",
            "Jensen", "Keller", "Khan", "Kim", "Laurent", "Lewis", "Martinez", "Meyer",
            "Morgan", "Nakamura", "Novak", "Okafor", "Park", "Patel", "Reyes", "Rossi",
            "Sato", "Schmidt", "Silva", "Singh", "Sullivan", "Tanaka", "Walker", "Williams"
        };

        public static Name GenerateName(
            CulturalPawnNameGroup group,
            Gender gender)
        {
            switch (group)
            {
                case CulturalPawnNameGroup.GoauldJaffa:
                    return new NameSingle(
                        Pick(GoauldJaffaPrefixes)
                        + "'"
                        + Pick(GoauldJaffaSuffixes));

                case CulturalPawnNameGroup.FreeJaffa:
                    return new NameSingle(
                        Pick(FreeJaffaPrefixes)
                        + Pick(FreeJaffaSuffixes));

                case CulturalPawnNameGroup.Goauld:
                    return new NameSingle(
                        Pick(GoauldPrefixes)
                        + Pick(GoauldSuffixes));

                case CulturalPawnNameGroup.Tokra:
                    return new NameSingle(
                        Pick(TokraPrefixes)
                        + Pick(TokraSuffixes));

                case CulturalPawnNameGroup.Tauri:
                    string firstName = Pick(GetTauriFirstNames(gender));
                    string lastName = Pick(TauriLastNames);
                    return new NameTriple(firstName, firstName, lastName);

                case CulturalPawnNameGroup.OffworldHuman:
                    return new NameSingle(
                        Pick(OffworldHumanPrefixes)
                        + Pick(OffworldHumanSuffixes));

                default:
                    return null;
            }
        }

        public static Name GenerateStableName(
            CulturalPawnNameGroup group,
            Gender gender,
            string identityKey)
        {
            switch (group)
            {
                case CulturalPawnNameGroup.GoauldJaffa:
                    return new NameSingle(
                        PickStable(GoauldJaffaPrefixes, identityKey, "prefix")
                        + "'"
                        + PickStable(GoauldJaffaSuffixes, identityKey, "suffix"));

                case CulturalPawnNameGroup.FreeJaffa:
                    return new NameSingle(
                        PickStable(FreeJaffaPrefixes, identityKey, "prefix")
                        + PickStable(FreeJaffaSuffixes, identityKey, "suffix"));

                case CulturalPawnNameGroup.Goauld:
                    return new NameSingle(
                        PickStable(GoauldPrefixes, identityKey, "prefix")
                        + PickStable(GoauldSuffixes, identityKey, "suffix"));

                case CulturalPawnNameGroup.Tokra:
                    return new NameSingle(
                        PickStable(TokraPrefixes, identityKey, "prefix")
                        + PickStable(TokraSuffixes, identityKey, "suffix"));

                case CulturalPawnNameGroup.Tauri:
                    string firstName = PickStable(
                        GetTauriFirstNames(gender),
                        identityKey,
                        "first");
                    string lastName = PickStable(
                        TauriLastNames,
                        identityKey,
                        "last");
                    return new NameTriple(firstName, firstName, lastName);

                case CulturalPawnNameGroup.OffworldHuman:
                    return new NameSingle(
                        PickStable(OffworldHumanPrefixes, identityKey, "prefix")
                        + PickStable(OffworldHumanSuffixes, identityKey, "suffix"));

                default:
                    return null;
            }
        }

        public static string GenerateSymbioteNameText(
            GoauldSymbioteOrigin origin)
        {
            CulturalPawnNameGroup group = origin
                == GoauldSymbioteOrigin.Tokra
                    ? CulturalPawnNameGroup.Tokra
                    : CulturalPawnNameGroup.Goauld;

            return GenerateName(group, Gender.None)?.ToStringFull
                ?? string.Empty;
        }

        public static string BuildSampleReport(int samplesPerGroup)
        {
            int sampleCount = samplesPerGroup < 1 ? 1 : samplesPerGroup;
            StringBuilder builder = new StringBuilder();

            builder.AppendLine(
                "GR_CulturalNames_SampleReportTitle".Translate().ToString());
            builder.AppendLine();
            builder.AppendLine(
                "GR_CulturalNames_SampleReportIntro".Translate().ToString());

            AppendSamples(
                builder,
                CulturalPawnNameGroup.GoauldJaffa,
                Gender.Male,
                sampleCount);
            AppendSamples(
                builder,
                CulturalPawnNameGroup.FreeJaffa,
                Gender.Female,
                sampleCount);
            AppendSamples(
                builder,
                CulturalPawnNameGroup.Goauld,
                Gender.None,
                sampleCount);
            AppendSamples(
                builder,
                CulturalPawnNameGroup.Tokra,
                Gender.None,
                sampleCount);
            AppendSamples(
                builder,
                CulturalPawnNameGroup.OffworldHuman,
                Gender.None,
                sampleCount);
            AppendTauriSamples(builder, sampleCount);

            return builder.ToString().TrimEndNewlines();
        }

        public static string GetGroupLabel(CulturalPawnNameGroup group)
        {
            switch (group)
            {
                case CulturalPawnNameGroup.GoauldJaffa:
                    return "GR_CulturalNames_Group_GoauldJaffa"
                        .Translate().ToString();

                case CulturalPawnNameGroup.FreeJaffa:
                    return "GR_CulturalNames_Group_FreeJaffa"
                        .Translate().ToString();

                case CulturalPawnNameGroup.Goauld:
                    return "GR_CulturalNames_Group_Goauld"
                        .Translate().ToString();

                case CulturalPawnNameGroup.Tokra:
                    return "GR_CulturalNames_Group_Tokra"
                        .Translate().ToString();

                case CulturalPawnNameGroup.Tauri:
                    return "GR_CulturalNames_Group_Tauri"
                        .Translate().ToString();

                case CulturalPawnNameGroup.OffworldHuman:
                    return "GR_CulturalNames_Group_OffworldHuman"
                        .Translate().ToString();

                default:
                    return "GR_CulturalNames_Group_None"
                        .Translate().ToString();
            }
        }

        private static void AppendSamples(
            StringBuilder builder,
            CulturalPawnNameGroup group,
            Gender gender,
            int count)
        {
            if (count <= 0)
            {
                return;
            }

            builder.AppendLine();
            builder.AppendLine(GetGroupLabel(group));

            HashSet<string> localNames = new HashSet<string>();
            int attempts = 0;

            while (localNames.Count < count && attempts < count * 20)
            {
                attempts++;
                Name generatedName = GenerateName(group, gender);
                string text = generatedName?.ToStringFull;

                if (!text.NullOrEmpty())
                {
                    localNames.Add(text);
                }
            }

            foreach (string name in localNames)
            {
                builder.Append("- ");
                builder.AppendLine(name);
            }
        }

        private static void AppendTauriSamples(
            StringBuilder builder,
            int count)
        {
            builder.AppendLine();
            builder.AppendLine(GetGroupLabel(CulturalPawnNameGroup.Tauri));

            HashSet<string> localNames = new HashSet<string>();
            int attempts = 0;

            while (localNames.Count < count && attempts < count * 20)
            {
                Gender gender = attempts % 2 == 0
                    ? Gender.Female
                    : Gender.Male;
                attempts++;

                Name generatedName = GenerateName(
                    CulturalPawnNameGroup.Tauri,
                    gender);
                string text = generatedName?.ToStringFull;

                if (!text.NullOrEmpty())
                {
                    localNames.Add(text);
                }
            }

            foreach (string name in localNames)
            {
                builder.Append("- ");
                builder.AppendLine(name);
            }
        }

        private static string[] GetTauriFirstNames(Gender gender)
        {
            switch (gender)
            {
                case Gender.Female:
                    return TauriFemaleFirstNames;

                case Gender.Male:
                    return TauriMaleFirstNames;

                default:
                    return TauriNeutralFirstNames;
            }
        }

        private static string Pick(string[] values)
        {
            return values[Rand.Range(0, values.Length)];
        }

        private static string PickStable(
            string[] values,
            string identityKey,
            string salt)
        {
            if (values == null || values.Length == 0)
            {
                return string.Empty;
            }

            unchecked
            {
                uint hash = 2166136261u;
                string source = (identityKey ?? string.Empty)
                    + ":"
                    + (salt ?? string.Empty);

                for (int index = 0; index < source.Length; index++)
                {
                    hash ^= source[index];
                    hash *= 16777619u;
                }

                return values[(int)(hash % (uint)values.Length)];
            }
        }
    }
}
