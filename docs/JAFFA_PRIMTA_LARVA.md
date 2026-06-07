# Physical Prim'ta larva resource

## Scope of 0.1.27-dev

This milestone introduces a physical immature Goa'uld symbiote resource for
Jaffa Prim'ta implantation.

## ThingDef

```text
SG1_PrimtaLarva
```

Player-facing label:

```text
Prim'ta larva
```

French label:

```text
larve de Prim'ta
```

## Current properties

| Property | Value |
|---|---:|
| Type | Haulable item resource |
| Stack limit | `10` |
| Storage category | Manufactured |
| Mass | `0.1` |
| Market value | `35` |
| Natural acquisition | Not implemented |
| Current test acquisition | Developer spawn |

The graphic is a temporary placeholder derived from the existing free-symbiote
prototype.

## Surgery requirement

The Jaffa implantation bill now requires:

```text
1 medicine
1 Prim'ta larva
```

The vanilla bill system handles ingredient hauling and consumption. The custom
worker performs an additional defensive check before attaching `SG1_JaffaPrimta`.

## Current flow

```text
spawn Prim'ta larva in developer mode
    ↓ haulable physical item
schedule implant Jaffa Prim'ta
    ↓ doctor hauls larva and medicine
successful surgery
    ↓
SG1_JaffaPrimta acquired state
```

## Future milestones

- natural larva acquisition;
- larva cultivation or Goa'uld supply chains;
- storage and preservation constraints;
- maturation into adult symbiotes;
- age ceremony for Jaffa implantation;
- dependency and tretonin;
- faction-specific access rules.

## Test checklist

1. Build with `build.cmd`.
2. Spawn one `Prim'ta larva` through developer tools.
3. Confirm the item can be hauled and stored.
4. Spawn a compatible Jaffa.
5. Schedule `implant Jaffa Prim'ta`.
6. Confirm the bill requests one medicine and one larva.
7. Allow the surgery to complete.
8. Confirm the larva is consumed.
9. Confirm `Prim'ta symbiote` appears on the Jaffa.
10. Save and reload.
11. Confirm persistence.
12. Try scheduling without any larva available.
13. Confirm the bill cannot be completed until a larva exists.
