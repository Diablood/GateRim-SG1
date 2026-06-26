# Goa'uld world-name generators

Version: `0.3.46-dev`

## Scope

This milestone removes the shared fixed instance name and the remaining vanilla
pirate name makers from the visible Goa'uld System Lord faction:

```text
Goa'uld System Lord domains
NamerFactionPirate
NamerSettlementPirate
```

They are replaced by:

```text
SG1_NamerFactionGoauldDomain
SG1_NamerSettlementGoauldDomain
```

The world-creation entry keeps the generic cultural label `Goa'uld System Lord
domains` / `Domaines des Grands Maîtres Goa'uld`, while each newly generated
faction instance receives its own domain name.

## Domain names

The faction grammar combines `12` forms of power with `24` cult, throne and
imperial themes, for `288` possible names.

Representative English results include:

```text
Dominion of the Golden Throne
Empire of the Serpent Crown
Court of the Eternal Eye
Hegemony of the Black Sun
```

Representative French results include:

```text
Dominion du trône d'or
Empire de la couronne du serpent
Cour de l'œil éternel
Hégémonie du soleil noir
```

The generated name deliberately describes the domain rather than a named
individual. This avoids inventing canon System Lords and avoids creating a false
identity link when RimWorld separately generates the leader pawn and symbiote.

## Settlement names

The settlement grammar combines:

- `6` masculine settlement types;
- `6` feminine settlement types;
- `24` Goa'uld imperial and religious themes;
- `5` ordinal ranks for each grammatical gender;
- unnumbered templates weighted twice as often as ordinal templates.

This produces `1,728` distinct complete settlement names.

Representative French results include:

```text
Temple du trône d'or
Citadelle de la couronne du serpent
Premier sanctuaire de la Porte sacrée
Deuxième pyramide du soleil noir
Cinquième nécropole du Souverain immortel
```

Initial place words and post-ordinal lower-case forms are translated separately
so French casing remains natural.

## Save compatibility

Existing faction and settlement names are serialized in saves and are not
renamed. The new RulePackDefs apply only when RimWorld generates a new Goa'uld
domain faction or settlement.

No world object, PawnKind, domain identity Def, leader, symbiote or persistent
C# data is changed.

## Identity boundary

This milestone does not introduce named canon System Lords and does not force a
faction name to match its generated leader. A future named-domain system would
need to coordinate faction identity, leader identity, Jaffa markings, equipment
and save persistence as one dedicated feature rather than infer that link from a
world-name string.

## Visual boundary

Goa'uld settlements continue to use the temporary vanilla settlement silhouette
and gold-toned faction colors. A distinct Goa'uld world icon remains part of the
global visual pass shared with all visible GateRim SG-1 factions.

## Final validation

Final local revision `r1` validated:

- varied generated names for multiple Goa'uld domains;
- varied settlement names with natural French casing and ordinal agreement;
- no pirate name-maker leakage or unresolved grammar tokens;
- bilingual indexed content loading without new errors;
- no migration or persistent save-data change;
- unchanged leaders, domain identity, settlements, permanent hostility, raids and free-symbiote incursion.

The milestone was published as `v0.3.46-dev`.

## Planned follow-up boundaries

Leader names remain independent from the world-name grammar. In `0.3.48-dev`, `feature/goauld-system-lord-leader-names` assigns the generated leader a formal Goa'uld symbiote identity while preserving a separate host name. It deliberately does not force the personal leader identity to match the independently generated domain name.

The equivalent Free Jaffa correction was published in `0.3.47-dev`.

The temporary vanilla house silhouettes remain planned for `feature/faction-world-icon-overhaul`, shared with all visible GateRim SG-1 factions.
