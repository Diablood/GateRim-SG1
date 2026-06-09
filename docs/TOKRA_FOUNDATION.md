# Tok'ra foundation prototype

## Scope of 0.1.39-dev

This milestone adds the first playable Tok'ra foundation without generating a
complete world faction yet.

## Safe staged rollout

The first iteration deliberately separates:

```text
Tok'ra identity and voluntary-host mechanics
    ↓ validate
world settlements, pawn groups, traders, quests and diplomacy events
    ↓ add later
queen-origin biology and Egeria-inspired content
```

## FactionDef foundation

```text
SG1_Tokra
```

The faction Def is:

```text
hidden
non-generated at game start
non-random
non-permanent-enemy
```

It is a future integration anchor, not a fully spawned world faction.

## Free Tok'ra symbiote

```text
SG1_TokraSymbiote
```

Spawn it through developer tools for this prototype.

The free Tok'ra symbiote:

```text
does not hunt autonomously
does not expose forced implantation
does not expose Goa'uld ritual implantation
does not expose the autonomous-hunt toggle
does expose voluntary implantation
```

## Voluntary implantation

Command:

```text
Voluntary Tok'ra implantation
```

French label:

```text
Implantation Tok'ra volontaire
```

Requirements:

```text
player-controlled compatible humanoid
within 12 cells
reachable
13 biological years or older
no existing recent or active adult symbiote
```

On selection, the same persistent adult-symbiote identity moves into the recent
implantation Hediff and later converts into the shared active-host state.

## Persistent origin

```text
GoauldSymbioteOrigin.Tokra
```

The existing persistent data model now creates new free symbiotes with an
explicit origin.

After extraction, the same Tok'ra identity returns as:

```text
SG1_TokraSymbiote
```

rather than the hunting Goa'uld variant.

## Shared host Hediffs

The existing recent and active adult-host Hediffs are reused. Their descriptions
are now neutral enough to cover:

```text
forced Goa'uld possession
voluntary Tok'ra symbiosis
```

## Scope boundaries

This prototype does not yet add:

```text
generated Tok'ra world faction
Tok'ra settlements
Tok'ra visitors or traders
Tok'ra pawn groups
Tok'ra quests
named Tok'ra characters
Egeria
queen-origin larva production
Tok'ra-specific host visuals
```

## Test checklist

1. Build with `build.cmd`.
2. Start a new test map or reload a development save.
3. Spawn `Tok'ra symbiote` through developer tools.
4. Select it and confirm only `Voluntary Tok'ra implantation` is available.
5. Confirm forced implantation, ritual implantation and autonomous hunt are absent.
6. Confirm the inspection panel displays origin `Tok'ra` and hunt `disabled`.
7. Place a player-controlled compatible adult within `12` cells.
8. Start voluntary implantation and target the colonist.
9. Confirm recent implantation receives the same persistent ID.
10. Save and reload.
11. Wait one day and confirm active Tok'ra symbiosis.
12. Schedule the existing extraction surgery during recent implantation.
13. Confirm the extracted pawn is again `Tok'ra symbiote`.
14. Confirm its origin remains `Tok'ra` and autonomous hunting remains disabled.
15. Spawn a normal `Goa'uld symbiote`.
16. Confirm its previous forced, ritual and autonomous-hunt workflows remain available.


## 0.1.39-dev-r1 FactionDef loading fix

The first local test exposed four `FactionDef` issues.

Removed unsupported XML fields:

```text
hairTags
startingGoodwill
naturalColonyGoodwill
```

Added the required RimWorld 1.6 field:

```text
raidLootValueFromPointsCurve
```

The Tok'ra faction remains hidden and non-generated. Goodwill and diplomacy
will be designed during the later faction-expansion milestone rather than being
represented by invalid placeholder fields.


## Voluntary-host pawn prototype

`0.1.40-dev` adds:

```text
SG1_TokraVoluntaryHost
```

Spawn this player-controlled human pawn through developer tools. Within `60`
ticks, it receives one active adult symbiote with persistent origin:

```text
Tokra
```

The initialization registry is persistent and grants the embedded symbiote only
once per pawn. Removing the symbiote later does not create an artificial
replacement.


## Pawn-group foundation

`0.1.41-dev-r1` adds two valid nested profiles inside the hidden Tok'ra
`FactionDef`:

```text
Combat
Peaceful
```

Both currently use:

```text
SG1_TokraVoluntaryHost
```

The faction remains hidden and non-generated. These profiles prepare future
small teams and peaceful visitors without enabling settlements, traders or
diplomacy.
