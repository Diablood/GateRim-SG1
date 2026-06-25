# Free Jaffa world-name generators

Version: `0.3.45-dev`

## Scope

This milestone removes the remaining vanilla outlander name makers from the
visible Free Jaffa faction:

```text
NamerFactionOutlander
NamerSettlementOutlander
```

They are replaced by:

```text
SG1_NamerFactionFreeJaffa
SG1_NamerSettlementFreeJaffa
```

The world-creation entry keeps the generic cultural label `Free Jaffa` /
`Jaffa libres`, but each newly generated faction instance now receives its own
collective name. Newly generated settlements use a separate dedicated grammar.

## Faction names

The original implementation retained a shared `fixedName`, so every manually
added Free Jaffa faction appeared as `Free Jaffa` / `Jaffa libres`.

Revision `r3` removes that fixed instance name and uses a combinatorial grammar
made of `12` collective forms and `18` resistance themes, for `216` possible
faction names.

Representative English results include:

```text
Alliance of the Free Clans
Council of the Jaffa Resistance
Brotherhood of the Liberated Hosts
Pact against the Masters
```

Representative French results include:

```text
Alliance des clans libres
Conseil de la résistance Jaffa
Fraternité des hôtes libérés
Pacte contre les Maîtres
```

The faction Def label remains generic so the Create World interface still
identifies the selectable culture clearly.

## Settlement names

The initial `r1` pool contained `24` complete names. Functional validation
showed that repeated results could still be disambiguated by RimWorld with
visible suffixes such as `2` or `3`.

Revision `r2` replaced that fixed list with a combinatorial settlement grammar:

- `5` masculine settlement types;
- `5` feminine settlement types;
- `12` cultural themes;
- `4` ordinal ranks for each grammatical gender;
- unnumbered templates weighted twice as often as ordinal templates.

This produces `600` distinct complete settlement names. Most results remain
unnumbered, while a smaller share naturally distinguishes related settlements
through forms such as `First Refuge...`, `Second City...`, `Premier refuge...`
or `Deuxième cité...`.

Revision `r3` separates the place word used at the beginning of a name from the
lower-case form used after an ordinal. French themes also keep ordinary nouns in
lower case, while proper cultural titles may retain a capital.

Representative French results include:

```text
Refuge des affranchis
Citadelle des clans libres
Premier abri des Maîtres déchus
Deuxième cité de la chaîne brisée
Quatrième place forte de la veille de la Porte
```

The enlarged grammars do not create a persistent uniqueness registry. A
collision remains theoretically possible, but it should be exceptional during
normal world generation instead of systematic.

## Save compatibility

Existing faction and settlement names are serialized in saves and are not
renamed. The new RulePackDefs apply only when RimWorld generates a new Free
Jaffa faction or settlement.

No PawnKind, world object, culture profile or persistent C# data is changed.

## World-map icon boundary

Free Jaffa settlements still use the vanilla house silhouette with the faction's
green color spectrum. The resulting light and dark green variants are subtle
and are not intended as the final faction identity.

A custom Free Jaffa settlement icon is deferred to the global visual pass. That
pass must also assign clearly distinct silhouettes to the other visible
GateRim SG-1 factions and future races, so identification does not depend only
on small color differences.

## Validation boundaries

The milestone does not add:

- custom settlement layouts;
- custom world-map textures or icons;
- new factions, races, missions or incidents;
- clan-specific diplomacy or persistent clan identities;
- a procedural Jaffa language generator.

## Final validation

Final local revision `r3` validated:

- varied generated names for multiple Free Jaffa factions;
- varied settlement names with natural French casing;
- no outlander name-maker leakage or unresolved grammar tokens;
- bilingual indexed content loading without new errors;
- no migration or persistent save-data change;
- unchanged settlement trade, convoy, peaceful-visitor and military-aid behavior.

The milestone was published as `v0.3.45-dev`.
