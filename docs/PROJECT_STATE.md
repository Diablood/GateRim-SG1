# Project state

Current milestone: `0.3.101-dev - Finalize Jaffa helmet gizmo and manual toggle`

Status: validated in local revision `r4`; the final repository state is complete
for fast-forward integration into `develop` and the unique annotated tag
`v0.3.101-dev`.

- Starting point: published `develop` aligned with `v0.3.100-dev`.
- Working branch: `feature/final-jaffa-helmet-mode-gizmo`.
- Final local revision: `r4`.
- Assembly version: `0.3.101.0`.
- Final annotated tag: `v0.3.101-dev`.

## Implemented scope

- Replace `Textures/UI/Commands/SG1_JaffaHelmetMode.png` in place with one final
  transparent `64×64` cobra-helmet command icon.
- Keep the stable C# texture path and avoid directional variants.
- Remove automatic draft-dependent deployment from normal gameplay.
- Replace the former three-mode cycle with one direct manual toggle:
  - a retracted helmet offers `Déployer casque`;
  - a deployed helmet offers `Rétracter casque`.
- Keep the inspection display limited to the actual deployed or retracted
  position.
- Preserve the ordinary and officer helmet pairs without cross-conversion.
- Preserve raw armor values and the existing coverage difference between the
  deployed and retracted Defs.
- Migrate legacy `Automatic` save values by preserving the physical helmet Def
  stored in the save and converting it to the matching manual state.
- Keep the historical updater type as an empty compatibility component so older
  saves can still resolve it without periodic automatic synchronization.
- Add a protected byte-identical wiki copy and register the icon as the fifth
  finalized command-icon family.

## Validated results

- The final cobra gizmo is readable in the real command interface and has genuine
  exterior transparency.
- The command label always shows the next available action.
- Each click immediately deploys or retracts the helmet.
- Drafting and undrafting no longer changes the helmet.
- The selected position persists through save and reload.
- The ordinary and officer helmet pairs remain isolated.
- The inspection panel reports only the physical position.
- Forced Release build succeeded with assembly `0.3.101.0`.
- Duration-formatting audit passed.
- Documentation-guard regression fixtures passed.
- Documentation-consistency audit passed after removal of duplicate milestone
  sections.
- Visual-asset audit passed with `610` PNG files, `77` local families and `45`
  accepted final local families.
- Aggregate project-consistency audit passed with `83` cultural backstories.
- `git diff --check` passed.
- No unrelated Def, crafting, research, loadout, balance or save identifier was
  changed.

## Publication state

The final commit is:

```text
0.3.101-dev - Finalize Jaffa helmet gizmo and manual toggle
```

It is integrated into `develop` by fast-forward, tagged once as
`v0.3.101-dev`, and followed by synchronization of the separate wiki because
`docs/wiki/` changed in this milestone.
