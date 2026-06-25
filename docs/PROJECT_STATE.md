# Current project state

Current milestone: `0.3.43-dev - Add Free Jaffa trade network` — final local revision `r5` validated and published.

## Repository state

- Starting tag: `v0.3.42-dev`.
- Published branch: `feature/free-jaffa-trade-network`.
- Final tag: `v0.3.43-dev`.
- Published versions: `0.3.43-dev` and `0.3.43.0`.
- Final local revision: `r5`.
- Main GitHub repository and separate wiki synchronized.

## Published scope

The visible Free Jaffa faction uses RimWorld's existing settlement, visitor,
caravan and comms-console trade flows. Caravan arrivals use the dedicated
`SG1_Caravan_FreeJaffaClanSupplies` trader kind instead of a generic vanilla
bulk-goods inventory.

The clan-supply convoy is deliberately bounded around:

- durable field provisions and medicine;
- steel, plasteel, components, chemfuel and cloth in moderate quantities;
- industrial human ranged and melee weapons;
- military armor;
- limited Ma'Tok, Zat'nik'tel and modular Jaffa equipment;
- a purchasing budget of roughly `850` to `1300` silver.

The weapon and armor generators allow the convoy to buy human military
equipment from the player. The finite silver reserve remains the primary limit
on how much raid loot or manufactured equipment one visit can absorb.

The five public Jaffa armor ThingDefs are tradeable in both directions so the
custom convoy can generate them for sale. The internal retracted helmet state
remains non-tradeable and cannot enter merchant stock.

## Dedicated trader pawn

`SG1_FreeJaffaTrader` provides a culturally coherent caravan contact:

- Jaffa xenotype and automatic Prim'ta;
- Free Jaffa childhood, adulthood and cultural name generation;
- Ma'Tok staff;
- light Jaffa armor, gauntlets and reinforced boots;
- no automatic Goa'uld forehead mark;
- no helmet, keeping the caravan contact distinct from its guards.

## Deliberate limits

Visitor and settlement trade still use their standard vanilla profiles. This
milestone specializes only the caravan inventory.

The convoy is not intended to cover every economic need of a future world with
only GateRim SG-1 factions. Future commercial factions, including the Nox,
should cover missing categories after a shared economy audit instead of turning
the Free Jaffa into a universal merchant.

Military aid, quest sites, recruitment, goodwill changes and natural Free Jaffa
raids remain outside this milestone. No persistent faction, pawn, item or
save-data identifier was removed or renamed.

## Final validation

- `check-project-consistency.cmd` passed and the existing `0.3.43.0` build remained valid;
- startup completed without Free Jaffa trader cross-reference, stock-generator or tradeability errors;
- settlement trade and forced clan-supply caravan generation were validated;
- the trader pawn, guards, pack animals and trade window behaved correctly;
- repeated stocks preserved the intended military-supply identity and bounded Jaffa equipment;
- human weapons and armor could be sold until the convoy's finite silver reserve was exhausted;
- the retracted helmet remained excluded from commerce;
- save/reload and `Player.log` checks passed without a new GateRim SG-1 regression.

## Next step

Choose the next milestone after rereading `docs/ROADMAP.md`,
`docs/IDEAS_TO_REVISIT.md` and `docs/MILESTONE_PUBLICATION.md`. Start it
explicitly from `v0.3.43-dev` on a new dedicated branch.
