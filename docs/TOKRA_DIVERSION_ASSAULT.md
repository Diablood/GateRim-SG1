# Tok'ra diversion assault

Status: final `0.3.34-dev` implementation validated on local revision `r3` and published under tag `v0.3.34-dev`.

## Purpose

This recurrent operation lets a Tok'ra cell use the colony as the end of a false Goa'uld trail. Acceptance authorizes a short false transmission through the secure channel. No physical mission object is delivered or placed.

A Goa'uld-aligned Jaffa force then attacks the colony. The assault is the objective:

- victory occurs when the registered attackers are dead, downed or retreating empty-handed;
- failure occurs if a registered attacker reaches the map edge with a player pawn or stolen item;
- losing the player map also fails the operation;
- the assault does not end through the ordinary raid timeout.

The operation keeps the stable internal archetype `DecoyTransmissionDefense` and MissionDef `SG1_TokraOrganic_DecoyTransmissionDefense` to avoid renumbering an already persisted enum value during development. These identifiers are technical only; all player-facing text describes a diversion assault.

## Dedicated breach group

`SG1_GoauldJaffaLuredAssault` uses the zero-weight strategy `SG1_GoauldJaffaLuredBreachingAssault` and explicitly requests the mission-only pawn-group kind `SG1_TokraDiversionAssault`. Neither definition enters ordinary storyteller raid selection.

RimWorld's immediate-breaching strategy requires its selected pawn group to contain at least one `PawnKindDef` marked `isGoodBreacher`. Revision r2 incorrectly used the normal Goa'uld `Combat` group, which contained no compatible pawn and caused the strategy minimum to rise to `99999` before raid adjustment.

Revision r3 adds `SG1_GoauldJaffaBreacher`:

- `isGoodBreacher = true`;
- `canBeSapper = true`;
- maximum four per assault group;
- inherited Ma'Tok-only weapon tag;
- available only through the mission group, not ordinary Goa'uld combat or settlement groups.

The Ma'Tok projectile already applies additional structural impact to buildings. The vanilla breach Lord therefore receives both a recognized breacher pawn and a lore-consistent wall-damaging weapon without adding a generic grenade or siege-hammer item.

Configuration validation now requires the custom group and a Ma'Tok-equipped good breacher. A future broken Def configuration disables the operation cleanly instead of attempting an impossible raid.

## Difficulty

The MissionDef captures a RimWorld threat snapshot when the offer is created. The hostile response uses:

- factor: `0.75`;
- minimum: `180` points;
- maximum: `3000` points.

The exact captured and scaled values persist through save/reload and are consumed when the assault starts. The dedicated group prevents the vanilla required-breacher minimum from replacing this budget with the error fallback near `99999` points.

## Persistence and resolution

The generic mission runtime stores the hidden assault due tick, whether the assault has started, the exact ThingIDs of the mission raiders, temporary cargo-carrier state and the extracted cargo kind.

The framework report also exposes the number of registered breachers for developer validation. No new persistent game-component field is required because breacher identity is derived from the registered pawns' `PawnKindDef` after loading.

Success applies `+3` Tok'ra trust. Failure applies `-2` trust. The shared outcome guard prevents duplicate letters, trust changes or counters.

## File-removal status

The five obsolete r1 transmitter files remain removed. Revision r3 removes no additional code, XML, documentation or texture file.
