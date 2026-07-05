# Goa'uld host-caste baseline

Version: `0.2.3-dev-r3`

## Purpose

This milestone adds naturally generated persistent Goa'uld hosts to visible
Goa'uld-domain factions.

```text
SG1_GoauldHostCaste
SG1_GoauldSystemLordHost
```

Player-facing labels:

```text
Goa'uld
Goa'uld System Lord
```

The ordinary host is deliberately called simply `Goa'uld`.

## Architecture

Generated hosts remain biologically human baseliners. Possession is acquired:

```text
human host body
+
SG1_GoauldHostSymbiote
+
GoauldSymbioteData with Goauld origin
```

This matches the existing implantation-conversion architecture.

The legacy non-inheritable xenotype prototype:

```text
SG1_GoauldHost
```

is not forced onto generated castes. Doing so would model acquired possession
as a permanent xenotype and would leave Goa'uld genes behind after a future
extraction.

## Initialization

```text
GateRimSG1.Goauld.GameComponent_GoauldHostCasteInitializer
```

The component scans every 60 ticks:

```text
Goa'uld faction leaders
spawned map pawns
```

It targets only:

```text
SG1_GoauldHostCaste
SG1_GoauldSystemLordHost
```

For each newly observed host, it adds one active host Hediff and one persistent
adult symbiote identity. The pawn ThingID is recorded so removing the symbiote
later cannot create an artificial replacement.

After a supported active extraction, subsequent scans treat the absent
symbiote component as the expected terminal state. They neither restore it nor
attempt to update its former domain allegiance.

If that generated host was captured by the player, successful extraction also
removes its System Lord faction membership. The human remains a factionless
colony prisoner, not a free colonist, and follows vanilla recruitment or
release choices.

## Faction integration

The faction leader kind becomes:

```text
SG1_GoauldSystemLordHost
```

The Settlement group uses dedicated capped profiles:

```text
SG1_GoauldSettlementJaffaWarrior: weight 4, max 7
SG1_GoauldSettlementJaffaGuard:   weight 2, max 2
SG1_GoauldHostCaste:              weight 1, max 1
```

The caps apply to each generated settlement group:

```text
up to 7 warriors per group
up to 2 guards per group
up to 1 ordinary Goa'uld host per group
```

A full settlement map may resolve more than one group. The final city-wide
count can therefore include two ordinary Goa'uld hosts while keeping them a
minority among the Jaffa defenders.

Since `0.3.75-dev`, each generated Goa'uld Settlement group containing at least
five Jaffa may replace one `SG1_GoauldSettlementJaffaGuard` with one
`SG1_GoauldSettlementJaffaOfficer`. Both kinds use `130` combat power, so the
replacement preserves the group budget and count. The `maxPerGroup = 1` officer
profile prevents a second officer inside the same generated group.

The XML option lists remain unchanged. Eligible generated Combat and Settlement
groups are post-processed only after vanilla has spent the original threat budget,
so the new officer remains a Jaffa replacement rather than an extra pawn. Final
cumulative revision `r2` validates the five-Jaffa threshold, `130`-point
one-for-one replacement, one-officer cap and save/reload behavior.

## Create World caste summary

The faction-level summary remains:

```text
Jaffa: 100%
```

Vanilla summarizes xenotypes, not acquired Hediff-based possession states.
Since `0.3.52-dev`, GateRim appends a qualitative caste section to the Goa'uld
faction description: dominant Jaffa servants, minority Goa'uld hosts and the
System Lord host leader. The generated Goa'uld castes remain real persistent
hosts without being added to the germline xenotype percentages.

## Temporary attire

Until dedicated Goa'uld visuals exist, both naturally generated host kinds
receive:

```text
Apparel_Pants
Apparel_CollarShirt
Apparel_Duster
```

This prevents naked generated hosts while keeping the final visual pass
separate.

## Initial biological healing

Immediately after the persistent adult symbiote is attached, the generated-host
initializer removes a narrow set of chronic biological ailments such as bad
back, frailty, cataracts, hearing loss, dementia, asthma, artery blockage and
carcinoma.

It deliberately preserves scars, missing body parts and ordinary combat
injuries.

## Deferred work

- dedicated cultural backstories;
- dedicated Goa'uld apparel;
- active-host extraction;
- final Goa'uld apparel and texture pass;

## Manual test checklist

1. Rebuild the C# assembly with `-t:Rebuild`.
2. Generate a new world with at least one Goa'uld-domain faction.
3. Start the colony and inspect the Goa'uld faction leader.
4. Confirm the leader kind is `Grand Maître Goa'uld`.
5. Confirm the leader has `SG1_GoauldHostSymbiote`.
6. Enable advanced diagnostics and record the persistent symbiote ID.
7. Visit or attack a Goa'uld settlement on a disposable save.
8. Confirm a mixed Jaffa settlement with at least one ordinary pawn labelled `Goa'uld`; two hosts on a larger city map remain acceptable.
9. Confirm the ordinary Goa'uld host carries `SG1_GoauldHostSymbiote`.
10. Confirm ordinary hosts and System Lords wear temporary vanilla clothing.
11. Confirm that generated hosts do not retain bad back or similar chronic conditions.
12. Confirm Jaffa remain the majority.
11. Trigger a direct natural or controlled raid and confirm it contains Jaffa only.
12. Save and reload.
13. Confirm the same leader and ordinary-host symbiote IDs remain stable.

## System Lord visible identity in `0.3.48-dev`

Newly generated `SG1_GoauldSystemLordHost` pawns receive a formal Goa'uld name before the world-creation interface displays them. When the persistent adult symbiote is initialized, that formal name becomes the stored symbiote name and a separate off-world human host name is prepared in the existing structured host-name fields. Supported extraction can therefore restore the hidden host name without rerolling or losing the Goa'uld identity transferred to the extracted symbiote.
