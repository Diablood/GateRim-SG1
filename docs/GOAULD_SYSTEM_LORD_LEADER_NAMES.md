# Goa'uld System Lord leader names

Version: `0.3.48-dev`

## Purpose

New Goa'uld System Lord leaders must no longer expose vanilla human names in the world-creation interface. Their visible identity represents the controlling Goa'uld symbiote, while the human host identity remains separate and recoverable.

## Generation-time visible identity

The fixed leader PawnKind remains:

```text
SG1_GoauldSystemLordHost
```

It now references:

```text
SG1_NamerPawnGoauldSystemLord
```

through the supported `nameMaker` and `nameMakerFemale` fields. This runs inside RimWorld's native leader-generation path, before the player selects a starting tile.

The RulePack provides:

- `575` Goa'uld personal names derived from the established cultural syllables;
- `24` language-neutral throne-house bynames;
- `13,800` complete formal combinations.

Each result repeats the personal name as the explicit nickname:

```text
Amonaris 'Amonaris' Kheper
```

The full diplomatic label is `Amonaris Kheper`, while short pawn labels remain `Amonaris`. The second component is a formal throne-house byname, not a human family surname.

## Host and symbiote reconciliation

At game start, `GameComponent_GoauldHostCasteInitializer` creates the persistent adult symbiote data as before. For `SG1_GoauldSystemLordHost` only, it now performs an additional reconciliation before the host Hediff is added:

1. the native PawnKind-generated visible name becomes the persistent symbiote name;
2. a deterministic, distinct off-world human name is generated from the existing `OffworldHuman` cultural pool;
3. that name is stored as the host identity;
4. the current host ThingID is prepared before Hediff attachment so `AttachToHost` does not overwrite the hidden host name with the visible symbiote name.

No new persistent field is added. The existing structured host-name fields in `GoauldSymbioteData` store the generated human identity.

## Extraction and release

`ReleaseHostControl` now restores the stored host name not only after an active hostile takeover, but also when a Goa'uld-origin pawn is currently displaying its persistent symbiote name. This covers generated System Lords removed through supported extraction or recovery paths without affecting ordinary implanted hosts whose visible name is already their host identity.

The extracted symbiote keeps the former visible Goa'uld name because the same persistent `GoauldSymbioteData` instance is transferred.

## Save compatibility

- Existing serialized leaders keep their current names.
- New worlds and newly generated replacement System Lords use the new native name maker.
- Existing `GoauldSymbioteData` fields remain compatible.
- No migration or new save key is introduced.

## Deliberate limits

This milestone does not:

- add canon System Lords;
- bind the leader name to the domain name;
- expose a new player-facing dual-identity panel for AI leaders;
- change the leader's backstories, equipment, rank, stats or behavior;
- modify ordinary Goa'uld hosts;
- add faction icons.

## Validation and durable regression coverage

The final local revision `r1` was validated on a newly generated world: several Goa'uld System Lord leaders displayed varied two-part cultural names before the player selected a starting tile, without falling back to vanilla human names.

The following cases remain durable regression coverage and must be replayed after future changes to this system:

1. Start the colony and confirm the visible name remains unchanged after cultural and symbiote initialization.
2. Inspect `Player.log` or the developer diagnostics for distinct, non-empty `symbioteName` and `hostName` values.
3. Save and reload, then confirm both identities remain stable.
4. Generate a replacement System Lord and confirm the same native name maker is used.
5. On a disposable save, perform a supported extraction and confirm the host restores the stored human name while the extracted symbiote retains the Goa'uld name.
6. Revalidate Free Jaffa leaders, ordinary Goa'uld hosts, permanent hostility, raids and the absence of Goa'uld trade, aid and quest sites.

These deeper identity and extraction cases are not presented as separately executed tests in the focused `0.3.48-dev` validation.
