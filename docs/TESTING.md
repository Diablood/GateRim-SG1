# Testing workflow

## Minimal isolated test

Use this active mod list first:

```text
Core
Biotech
GateRim SG-1
```

This isolates GateRim definitions from unrelated third-party gene categories and patches.

## Jaffa foundation checklist

1. Start RimWorld with the minimal isolated mod list.
2. Open the xenotype editor.
3. Confirm that the editor opens without exceptions.
4. Load the premade `Jaffa` xenotype.
5. Confirm that `Jaffa physiology` displays a texture.
6. Confirm that `Jaffa longevity` displays a `150%` lifespan factor.
7. Switch to French and verify the translated labels and descriptions.
8. Close the game and inspect `Player.log`.

## Interpreting the first external test log

The first external log contained two categories of issues:

### GateRim issues corrected in 0.1.5-dev
- Leading and trailing whitespace in the Jaffa xenotype description.
- French translation values formatted across multiple lines.
- Missing `UI/Icons/Genes/Gene_Robust` texture.

### Third-party compatibility issue to isolate separately
- `KeyNotFoundException` for a gene category named `Ability`.

The `Ability` category is not declared by the current GateRim definitions. Re-run the minimal isolated test before investigating loaded third-party mods.


## Free Goa'uld symbiote prototype

Use developer mode to spawn:

```text
SG1_GoauldSymbiote
```

Checklist:

1. Confirm the pawn appears with its temporary sprite.
2. Confirm movement and a weak bite attack.
3. Confirm that no natural biome spawn occurs.
4. Switch to French and verify the translated label and description.
5. Check `Player.log` for `SG1_GoauldSymbiote` errors.


## 0.1.9-dev XML regression check

After applying the free-symbiote XML correction:

1. Launch with `Core`, `Biotech`, and `GateRim SG-1`.
2. Confirm that `Player.log` no longer reports:
   ```text
   XML error: <wildness>1</wildness> doesn't correspond to any field in type RaceProperties.
   ```
3. Spawn `SG1_GoauldSymbiote` through developer mode.
4. Confirm that movement, the weak bite, and the temporary sprite still work.
