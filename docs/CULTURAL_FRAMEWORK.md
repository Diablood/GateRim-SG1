# Cultural framework

Version: `0.3.21-dev`

## Purpose

The `0.3.x` series builds one internal cultural framework that can be reused by names, backstories, starting scenarios and later culture-dependent systems. It is not a separate mod dependency.

The framework separates:

- a generic C# resolver;
- culture and identity profiles configured in XML;
- consumers that request only the data they need.

## Profile resolution

`CulturalPawnProfileDef` profiles declare:

- a priority;
- one or more matchers;
- an optional default cultural name group;
- optional name rules based on selected childhood or adulthood;
- optional starting-pawn backstory rules;
- configurable exclusive replacement rules and additive weighted-pool rules for childhoods and adulthoods;
- optional weighted generated-host origins for identities created already joined with a symbiote;
- optional scenario-part requirements.

Each matcher can identify a pawn through one or more of:

- race `ThingDef`;
- xenotype;
- `PawnKindDef`;
- faction;
- the `PlayerStarter` generation context.

A profile matches when one matcher succeeds. Inside a matcher, every configured criterion must succeed. The highest-priority matching profile wins. Equal-priority ambiguities produce a technical warning and resolve deterministically by `defName`.

## Initial consumers

### Cultural names

`GameComponent_CulturalPawnNameManager` no longer contains its own hard-coded culture switch. It asks `CulturalProfileResolver` for the resolved cultural name group.

This preserves the existing behavior for Goa'uld-aligned Jaffa, Free Jaffa, Goa'uld, Tok'ra and Tau'ri / SGC pawns while moving the mapping into XML.

### Starting-pawn randomization

A hidden `ScenPart_CulturalStarterProfiles` is added to Def-based scenarios. It acts only during `PawnGenerationContext.PlayerStarter` generation.

The part:

1. resolves the starter profile;
2. selects configured compatible childhood and adulthood Defs;
3. replaces only the newly generated pawn's initial backstories;
4. preserves the randomized skill baseline, passions and gene aptitudes;
5. applies only the exact difference between the old and new backstory skill bonuses;
6. assigns a cultural name from the final adulthood where a mixed profile defines name rules.

The callback occurs while the pawn is still being generated, before the player can make manual edits. It never validates or rejects the final pawn later.

## Initial profiles

| Profile | Main criterion | Starter behavior | Name behavior |
|---|---|---|---|
| Starter Jaffa | `SG1_Jaffa` xenotype + player starter | Jaffa childhood; Goa'uld-aligned or Free Jaffa adulthood | Follows the selected adulthood |
| Starter Goa'uld host | `SG1_GoauldHost` xenotype + player starter | Off-world-human childhood; Goa'uld-host or Tok'ra adulthood | Follows the selected adulthood |
| Goa'uld-aligned Jaffa | Current Goa'uld Jaffa PawnKinds | None | Goa'uld Jaffa generator |
| Free Jaffa | Current Free Jaffa PawnKinds or faction | None | Free Jaffa generator |
| Goa'uld | Current Goa'uld host, System Lord, queen and symbiote PawnKinds | None | Goa'uld generator |
| Tok'ra | Current Tok'ra host or symbiote PawnKinds, or Tok'ra faction | None | Tok'ra generator |
| Ordinary human starter | Human race + player starter | Keeps vanilla generation and adds the eight SGC careers to the same effective weighted adulthood pool as compatible vanilla careers | Keeps the generated vanilla name |
| Tau'ri / SGC | Player SGC expedition faction | Eight SGC adult careers only when the SG-team scenario part is present | Tau'ri generator |

The ordinary-human profile is deliberately lower priority than the Jaffa, Goa'uld-host and SG-team profiles. It preserves the vanilla childhood and uses the already generated vanilla adulthood as the vanilla side of a merged weighted pool. The eight Tau'ri / SGC careers each contribute their normal backstory weight, so their frequency automatically scales against the number and weight of compatible vanilla careers instead of relying on a hard-coded percentage.

The SG-team profile intentionally preserves the existing vanilla childhood until a dedicated Tau'ri childhood set is designed. This milestone does not increase the number of backstories.

### Starter biological completion (`0.3.87-dev`)

The same hidden scenario part completes the biological state implied by two
GateRim starter profiles during `Notify_PawnGenerated`, after the final
backstory and cultural name have been selected but before the pawn is shown to
the player.

- an eligible adult `SG1_Jaffa` receives one `SG1_JaffaPrimta`;
- every `SG1_GoauldHost` receives one persistent adult symbiote;
- a final Tok'ra name group creates Tok'ra origin plus a distinct generated
  human-host identity and dual personality;
- every other final name group creates Goa'uld origin and no Tok'ra personality
  switch.

Tok'ra and Goa'uld are therefore two cultural/allegiance branches of the same
Goa'uld-family biology rather than one branch being allowed to remain an empty
xenotype shell.

This deliberately remains a generation callback rather than a reconciler. A
non-serialized ThingID guard is reset whenever `Notify_NewPawnGenerating` begins
a new starter-generation cycle, including a reroll that reuses the same pawn
identifier, and is armed only after the affected biological state has actually
been resolved. A later manual removal from the displayed pawn is not undone at
game start or on save load. The ordinary non-player PawnKind initializers remain
separate and unchanged.

### Persistent host / symbiote identities

`0.3.10-dev` adds a third consumer without adding culture-specific branches to the resolver.

A profile may expose optional `identityChildhoods` and `identityAdulthoods`. `CulturalIdentityUtility` selects a stable entry from those lists using the persistent identity key, so save migration and repeated loading do not depend on a new random roll.

The first configured use is the Tok'ra symbiote identity:

- the six existing Tok'ra adult careers are declared on `SG1_Culture_Tokra`;
- the persistent symbiote ID selects one career deterministically;
- the host's current name and backstories are captured separately at implantation;
- the same data object already used by implantation and extraction carries both records;
- only a directly player-controlled Tok'ra exposes the new RP summary.

The framework does not replace the pawn's active backstories or apply skill changes in this phase. Future cultures may configure identity pools without C# changes while the existing slots are sufficient.

### Unified cultural identity diagnostics

`0.3.15-dev` adds a development-only consumer that reads the existing framework without creating another source of truth. For one selected pawn it reports:

- current race, xenotype, `PawnKindDef`, faction and active backstories;
- matching profiles, the selected profile and the name group in both normal and player-starter contexts;
- Jaffa physiology, Prim'ta and forehead mark;
- contextual social identities already used by thoughts and faction logic;
- persistent adult-symbiote data when present.

The report is deliberately evaluated at the time it is opened. It does not cache, assign, normalize or repair culture data. Its purpose is to reveal disagreements between existing consumers so a real defect can be reproduced before any shared framework rule is changed.

The complete `0.3.15-dev` matrix was validated for ordinary humans, Free Jaffa, Goa'uld-aligned Jaffa, Goa'uld hosts and pre-joined Tok'ra, including active-personality switching, save/load, both access paths and hidden-state behavior. Repeated inspection produced no pawn mutation or new log error.

### Measured backstory expansion

`0.3.16-dev` demonstrates that an existing cultural catalogue can be expanded without modifying the generic resolver. Twelve new `BackstoryDef` entries are added as passive data, while one XML patch appends them to the explicit starter lists and mixed-profile name rules already exposed by `CulturalPawnProfileDef`.

The patch preserves the existing semantics:

- ordinary human starters merge eight SGC careers with compatible vanilla adulthoods;
- the stranded SG-team scenario restricts adulthood to those eight SGC careers;
- starter Jaffa use ten shared childhoods and the expanded aligned adulthood pools;
- starter Goa'uld hosts use the existing off-world childhoods and the expanded Goa'uld or Tok'ra adulthood pools;
- normal world generation continues to rely on each BackstoryDef's spawn category.

No new matcher, resolver branch or skill-offset rule is introduced. This is the preferred extension pattern while the existing Def schema expresses the content accurately.

The complete `0.3.16-dev` matrix validated the expanded starter pools, mixed cultural name rules, additive ordinary-human behavior, normal world generation, save/load and Tok'ra switching without a post-test C# or Def correction.

### Cultural starter restrictions and loadouts (`0.3.21-dev`)

The starter-rule consumer reads candidate restrictions and equipment data from the same `CulturalStarterRule`:

- minimum biological age;
- requirement to remain capable of violence;
- optional replacement of generated clothing;
- legacy ordered apparel lists;
- weighted apparel slots with optional shared variant groups;
- per-slot selection chance;
- weighted apparel options;
- optional material for stuffable apparel;
- requested quality category.

Restrictions are evaluated while the player randomizes starters. Apparel is applied only after a `PlayerStarter` pawn is generated. Profiles without these fields preserve their existing candidate selection and clothing.

Weighted slots are the preferred schema for variable loadouts. A mandatory slot uses a chance of `1`; an optional slot uses a lower chance and therefore needs no placeholder Def for the `nothing` result. Multiple options can share one slot with relative weights. This is suitable for visual variants, headgear alternatives and future culture-specific equipment.

The stranded SG-team scenario is the first consumer. Its Tau'ri / SGC rule configures:

- age 20 or older;
- violence-capable starters;
- mandatory cloth T-shirt;
- mandatory olive, black or desert SG field pants;
- optional olive, black or desert SG field jacket;
- mandatory tactical boots, gloves and vest;
- optional SG field helmet.

The SG field uniform is split into independent apparel pieces. Pants cover the legs on `OnSkin`; the T-shirt covers the torso on `OnSkin`; the jacket uses `Middle`; and the tactical vest uses `Shell`. This permits all four layers to coexist and allows a pawn to remain in a T-shirt when the optional jacket is not selected.

The headgear slot is intentionally extensible. A future SG-team cap can be appended beside the helmet in XML, while the slot's selection chance continues to allow no headgear.

The scenario keeps `SG1_StrandedSGTeamStartingGear` only as a hidden marker implemented by the generic `ScenPart_CulturalMarker`; the old scenario-specific C# dresser is removed. Temporary vanilla firearms remain loose scenario things, but their set is rebalanced to one assault rifle, one machine pistol, one autopistol and one pump shotgun.

Configuration validation rejects negative ages, replacement without apparel, invalid chances, empty slots, non-positive option weights, null or non-apparel Defs and invalid material declarations. This schema concerns player starters only; equipment preferences for raids, visitors or normal world pawns remain a separate future consumer.

## Boundaries

The starter-profile consumer does not modify:

- raids;
- visitors;
- settlement inhabitants;
- incident or quest pawns;
- developer `Spawn pawn` generation;
- existing saves or already generated pawns;
- manual edits made by a compatible pawn editor after generation.

The generated-host consumer is a separate explicit exception: it acts only on a Tok'ra PawnKind created already fused, because that pawn has no historical implantation from which a host identity could otherwise be captured. It does not broaden starter filtering or globally replace world-pawn backstories.

The XML patch adds the hidden part to scenarios defined through `ScenarioDef`. Custom scenario files that bypass Def loading remain a compatibility case for later testing.


### Reversible active identity and skill offsets

`BackstorySkillOffsetUtility` is the first reusable framework service for an identity that changes after pawn generation. It:

- derives skill-level offsets from any configured childhood and adulthood pair;
- stores one shared raw-XP progression per skill;
- synchronizes XP gained or lost under the current identity before a switch;
- applies only the target backstory offsets;
- leaves passions, gene aptitudes and the pawn's randomized baseline untouched.

The first consumer is the player-controlled Tok'ra personality switch. The utility is not Tok'ra-specific and can support a future culture or identity mechanic that needs reversible backstory-derived skill differences without duplicating progression logic.

The `0.3.11-dev` implementation is functionally validated for repeated switching, shared XP, save/load under both identities, extraction, reimplantation and player / AI boundaries.

## Adding a future culture

For a future Asgard, Nox, Unas or other culture:

1. create the required xenotype, PawnKinds or faction identifiers;
2. create its cultural name generator and backstories;
3. add a `CulturalPawnProfileDef` with appropriate matchers and priority;
4. add starter rules only if that culture needs restricted or additive starter randomization;
5. add name rules only when multiple cultural identities share one biological profile;
6. validate that no equal-priority ambiguity is introduced.

No C# change should be required while the existing matcher and rule types express the new culture accurately.

## Future extensions

The same resolved profile may later expose data for scenarios, equipment preferences, incidents, quests, dialogue, debug tools and other systems. Persistent identity pools are now an implemented example of this reuse. Those fields should be added only when at least one real consumer requires them.

## Active identity interface integration (`0.3.12-dev`)

The shared identity framework now also centralizes whether a Tok'ra is directly controlled by the player. The rule covers spawned colonists and permanent colonist owners travelling in a player caravan, while excluding guests, prisoners, slaves and AI-managed pawns. Map and caravan interfaces delegate to the same persistent identity and shared-skill services rather than maintaining separate switch implementations.

## Generated-host origin profiles (`0.3.13-dev`)

The `0.3.12-dev` audit exposed a generation case that now uses the framework rather than a Tok'ra-specific name reroll. A Tok'ra PawnKind created already fused has no historical implantation event and therefore no pre-existing host identity to capture.

`GeneratedHostOriginDef` declares one possible historical host culture:

- selection weight;
- cultural name group;
- compatible childhood and adulthood pools;
- optional race and xenotype restrictions.

`CulturalPawnProfileDef.generatedHostOrigins` exposes one or more of those Defs to a profile. `CulturalGeneratedHostIdentityUtility` resolves the winning profile, filters compatible origins and selects a stable origin, name, childhood and adulthood from the persistent symbiote ID.

The initial `SG1_GeneratedHost_OffworldHuman` origin uses:

- the Human race;
- the off-world-human name generator;
- six existing off-world-human childhoods;
- six new civilian off-world-human adult careers.

The name selection retries deterministically if it would equal the stored symbiote name. The result therefore remains stable across save/load while keeping the two identities of the same Tok'ra distinct.

`TokraHostIdentitySource` explicitly records whether data came from:

- an unknown older save awaiting classification;
- a real implantation into an existing host;
- generated pre-joined initialization.

Migration never infers the source by comparing display names. A real implantation continues to capture the existing pawn unchanged. If a symbiote originally generated pre-joined is later extracted and implanted into a new pawn, the source changes to the real-host state and the generated historical-host origin is cleared.

The cultural name manager cooperates with this consumer: it may assign a proper Tok'ra name to the symbiote identity, but it leaves the generated historical host visible while the host personality is active.

`0.3.18-dev` adds and validates the first additional origin, `SG1_GeneratedHost_TauriSGCVolunteer`, entirely through XML. It uses the existing Human race and Tau'ri name generator, two dedicated modern-Earth childhoods and the eight current SGC adult careers. Relative weights of `1` for the off-world origin and `0.2` for the Tau'ri origin keep off-world humans dominant without introducing a hard percentage.

The new Tau'ri childhoods remain restricted to their own spawn category and are not injected into ordinary starter pools. Existing saved generated identities retain their stored Def references and are not rerolled when the list of available origins grows. The complete `r1` matrix validated both weighted origins, personality switching, shared progression, save/load, legacy identity stability and the real-reimplantation boundary without a C# correction.

Future Jaffa, Unas or other compatible origins can be added mainly in XML once their biological compatibility, backstories and name generators are genuinely available. No additional C# branch is required while the current origin schema remains sufficient.



## Generation-time Free Jaffa leader names (`0.3.47-dev`)

Functional testing of local revisions `r1` and `r2` showed that a post-generation cultural scan is too late for the world-creation interface: RimWorld has already generated and displayed `Faction.leader` before the game component begins its normal naming cycle.

The milestone therefore leaves the generic resolver unchanged and uses the native generation path:

- `SG1_FreeJaffaGuard` remains the fixed leader kind of `SG1_FreeJaffa`;
- the PawnKind references `SG1_NamerPawnFreeJaffa` through `nameMaker` and `nameMakerFemale`;
- `Faction.TryGenerateNewLeader` receives the cultural name before the leader is exposed to the world-creation interface;
- the later cultural manager recognizes a native PawnKind name maker and registers its result without renaming the pawn a second time.

The first `r3` startup exposed that `chanceToUseNameMaker` is not a valid RimWorld 1.6 `PawnKindDef` field, so the invalid XML entry was removed.

The `r4` world-generation test then exposed the native pawn-name format requirement. `PawnKindDef.nameMaker` results are parsed as `NameTriple`. Single-token results leave the first and last fields empty, and `NameTriple.ConfusinglySimilarTo` consequently treats every later single-token candidate as equivalent after the first leader is registered.

Revision `r5` keeps the same generation point but emits a complete formal structure:

- `576` explicit personal names are built from the existing Free Jaffa cultural syllables;
- each root rule repeats that personal name as its explicit nickname;
- a second language-neutral clan component is selected from `24` bynames;
- full labels show the personal and clan names, while short labels remain the personal name;
- the uniqueness validator now receives non-empty first and last fields.

The provisional faction-only resolver entry point and dedicated persisted leader registry from `r1` and `r2` remain removed. Existing save data needs no migration because the final design adds no new save field. Existing leaders keep their serialized names, while replacement leaders generated later through the same fixed PawnKind use the new name maker.

The fixed leader PawnKind is also used for ordinary Free Jaffa guards. Giving it the same formal cultural grammar changes only when and in what valid native structure their intended Free Jaffa identity is assigned.

## Generation-time Goa'uld System Lord identities (`0.3.48-dev`)

`SG1_GoauldSystemLordHost` now receives its visible Goa'uld identity from a native PawnKind name maker before the world-creation interface displays the leader. The generated `NameTriple` supplies non-empty first and last fields and preserves the personal Goa'uld name as the short nickname.

When the game begins, the existing host initializer reconciles that visible name with `GoauldSymbioteData.symbioteName`, generates a distinct off-world human host name and prepares the current host ThingID before Hediff attachment. This prevents `AttachToHost` from overwriting the hidden host identity. Supported release and extraction paths restore the stored host name when the pawn is displaying the symbiote identity.
