# Free Jaffa trade network

Version: `0.3.43-dev`

## Purpose

This milestone opens trade with the visible Free Jaffa faction without adding a
separate currency, custom trade window or scripted reward economy. Settlement,
visitor, caravan and comms-console interactions remain ordinary RimWorld flows.

## Trade profiles

`SG1_FreeJaffa` exposes:

```text
caravan:   SG1_Caravan_FreeJaffaClanSupplies
visitor:   Visitor_Outlander_Standard
settlement: Base_Outlander_Standard
```

Only the caravan inventory is specialized in the final validated revision `r5`.
Visitor and settlement commerce remain vanilla baselines until later
faction-wide economy work justifies changing them.

## Clan-supply convoy

`SG1_Caravan_FreeJaffaClanSupplies` represents an armed resistance exchanging
supplies, salvaged equipment and repaired Jaffa technology between independent
communities.

Its bounded stock contains:

- survival meals, pemmican and field medicine;
- steel, plasteel, industrial components, occasional advanced components,
  chemfuel and cloth;
- a small selection of industrial human ranged and melee weapons;
- a small selection of military armor;
- limited Ma'Tok staffs and modular Jaffa equipment;
- an uncommon single Zat'nik'tel;
- approximately `850` to `1300` silver for purchases from the player.

The broad weapon and armor generators are intentional. Free Jaffa rebels can
purchase effective human military equipment rather than rejecting it because it
is not Goa'uld-made. The finite silver reserve limits how much raid loot or
manufactured equipment can be sold during one visit.

The convoy does not generate furniture, art, recreational-drug catalogues,
animals for sale, exotic implants or a broad luxury inventory.

The five public Jaffa armor pieces use bidirectional tradeability so they can be
bought from or sold to the convoy. `SG1_JaffaRetractedHelmet` remains an internal
alternate state with no tradeability and is never generated as stock.

## Trader pawn group

The faction declares a dedicated `Trader` pawn-group profile:

```text
trader:   SG1_FreeJaffaTrader
guards:   SG1_FreeJaffaWarrior / SG1_FreeJaffaGuard
carriers: muffalo / dromedary / alpaca
```

`SG1_FreeJaffaTrader` is explicitly declared as a trader PawnKind and uses:

- the Jaffa xenotype;
- automatic Prim'ta provisioning;
- Free Jaffa childhood, adulthood and names;
- a Ma'Tok staff;
- light Jaffa armor, gauntlets and reinforced boots;
- no automatic Goa'uld forehead mark.

The missing helmet is intentional so the caravan contact remains visually
distinct from the guards.

## Long-term economy boundary

This convoy is not a universal fallback for a future world without vanilla
factions. Free Jaffa primarily cover resistance supplies, strategic resources
and military equipment. Future trade-oriented factions, notably the Nox, should
be designed after auditing which economic categories are still missing. This
keeps faction inventories complementary instead of duplicating one complete
vanilla economy in every culture.

## Boundaries retained

The following faction features remain disabled:

```text
canRequestMilitaryAid: false
canGenerateQuestSites: false
raidsForbidden: true
canSiege: false
canStageAttacks: false
```

No goodwill rule, recruitment offer, quest, military aid or natural raid is
introduced here.

## Save compatibility

No persistent identifier or C# save-data format changes. Existing saves with a
visible Free Jaffa faction receive the new trader definition after loading the
updated mod.

## Validation

Final local revision `r5` validated repeated stock generation, purchasing
limits, military-equipment acceptance, caravan identity, settlement trade,
save/reload and a clean `Player.log`. Durable regression coverage is maintained
in `docs/TESTING.md`.
