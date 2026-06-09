# GateRim SG-1 — French translation audit follow-up

## Known issue

RimWorld still reports five French translation errors during loading.

Do not guess these corrections from the generic warning alone. Generate the native RimWorld translation report first, then fix the exact reported entries.

## Next-step checklist

- [ ] Start RimWorld with GateRim SG-1 enabled.
- [ ] Generate the French translation report from the RimWorld developer tools.
- [ ] Save or copy the report.
- [ ] Review the five exact errors.
- [ ] Patch the corresponding `Languages/French/DefInjected/...` or `Languages/French/Keyed/...` files.
- [ ] Reload RimWorld.
- [ ] Confirm the French translation warning has disappeared.
- [ ] Run a regression pass on the affected UI strings.

## Scope rule

Keep English source text in `Defs`, French `DefInjected` translations in parallel, and English/French `Keyed` files for C# and non-Def UI strings.
