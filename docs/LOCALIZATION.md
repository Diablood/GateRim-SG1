# Localization workflow

## Rules

- Keep English labels and descriptions directly in functional `Defs`.
- Add French translations in parallel once a functional content batch is stable.
- Use `Languages/French/DefInjected/<DefType>/...` for translated `Def` fields.
- Use `Languages/English/Keyed/...` and `Languages/French/Keyed/...` for future UI messages, C# strings and non-Def text.
- Perform a global language pass before Steam Workshop publication.

## Current translated content

| Def type | Def name | French translation |
|---|---|---|
| `XenotypeDef` | `SG1_Jaffa` | Added |
| `GeneDef` | `SG1_JaffaPhysiology` | Added |
| `GeneDef` | `SG1_JaffaLongevity` | Added |

## Current directory layout

```text
Languages/
└── French/
    └── DefInjected/
        ├── GeneDef/
        │   └── SG1_JaffaGenes.xml
        └── XenotypeDef/
            └── SG1_Jaffa.xml
```

## Manual test checklist

1. Enable `Core`, `Biotech`, then `GateRim SG-1`.
2. Switch RimWorld to English and confirm that the Jaffa xenotype still displays correctly.
3. Switch RimWorld to French and restart if requested.
4. Confirm that the xenotype label, description and short description are translated.
5. Confirm that `physiologie jaffa` and `longévité jaffa` are translated.
6. Check `Player.log` for translation-key or XML errors.
