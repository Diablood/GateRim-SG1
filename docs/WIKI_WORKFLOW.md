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

Published player-facing pages live directly at the root of the separate repository:

```text
GateRim-SG1.wiki/
├── Home.md
├── _Sidebar.md
└── ...
```

Do not create a `wiki/` subdirectory inside `GateRim-SG1.wiki/`.

## Status labels

Every player-facing page should contain one of these statuses:

| Status | Meaning |
|---|---|
| `Implémenté` | Available and tested |
| `Prototype` | Available or documented but still evolving |
| `Prévu` | Planned, not yet available in game |

Add the introduction version when relevant.

## Recommended synchronization command

From the main repository:

```bash
./tools/sync-wiki.sh
```

The script copies `docs/wiki/.` directly into the root of the sibling `GateRim-SG1.wiki` repository.

## Manual equivalent

Run from the parent directory containing both repositories:

```bash
cp -R GateRim-SG1/docs/wiki/. GateRim-SG1.wiki/
```

## Versioning policy

- Use annotated version tags only in `GateRim-SG1`.
- Use simple documentation commits in `GateRim-SG1.wiki`.
