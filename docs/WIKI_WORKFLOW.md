# Player wiki workflow

## Local workspace layout

Keep the repositories side by side:

```text
GateRim-SG1/
GateRim-SG1.wiki/
```

Do not clone the wiki repository inside the main repository.

## Source of truth

Versioned wiki drafts live in:

```text
GateRim-SG1/docs/wiki/
```

Player-facing pages published on GitHub live in the separate repository:

```text
GateRim-SG1.wiki/
```

## Status labels

Every player-facing page should contain one of these statuses:

| Status | Meaning |
|---|---|
| `Implémenté` | Available and tested |
| `Prototype` | Available or documented but still evolving |
| `Prévu` | Planned, not yet available in game |

Add the introduction version when relevant.

## Publish all stabilized drafts

Run from the parent directory containing both repositories:

```bash
cp -R GateRim-SG1/docs/wiki/. GateRim-SG1.wiki/
```

Then commit inside `GateRim-SG1.wiki`.

## Publish a single page

```bash
cp GateRim-SG1/docs/wiki/Jaffa.md GateRim-SG1.wiki/Jaffa.md
```

## Versioning policy

- Use annotated version tags only in `GateRim-SG1`.
- Use simple documentation commits in `GateRim-SG1.wiki`.
