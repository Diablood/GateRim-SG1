# Current testing — final storyteller SG-1 portrait

Jalon : `0.3.88-dev`
Révision visuelle validée : `r1`
Révision de finalisation validée : `r2`
Révision de clôture documentaire : `r3`
Version de DLL validée : `0.3.88.0`

## Build and static checks — passed

The assembly built and loaded successfully during `r1`. Revision `r2` changed
only documentation and the visual-checker whitelist; revision `r3` changes only
publication-state documentation. Neither revision modifies the DLL or either
validated PNG.

The final checks passed after `r2`:

```powershell
./tools/check-duration-formatting.cmd
./tools/check-visual-assets.cmd
./tools/check-project-consistency.cmd -ExpectedVersion 0.3.88-dev
git diff --check
```

Validated result:

- both storyteller families are parsed as `final` / `done`;
- the exact final-family whitelist contains `11` local families;
- README, wiki-home, content-status, project-state, current-test and changelog
  versions all resolve to `0.3.88-dev`;
- the documented DLL version resolves to `0.3.88.0`;
- no Markdown tab, local-link, backstory-count or visual-asset check fails.

## Storyteller portrait validation — passed in r1

1. Enabled GateRim SG-1 and Biotech, then opened the new-game storyteller
   selection.
2. Selected **Commandement SG-1**.
3. Confirmed the large portrait uses the new SG-1 officer image.
4. Compared its apparent size and framing with Cassandra, Phoebe and Randy.
5. Confirmed the head, shoulders, vest and insignia are not clipped unexpectedly.
6. Confirmed transparent margins do not render as an opaque white rectangle.
7. Started the game and inspected the surfaces that use the tiny portrait.
8. Confirmed the `122×130` face crop is centered, sharp and recognizable.
9. Confirmed both images remain correct after normal storyteller UI reuse.
10. Confirmed storyteller selection and behavior remain unchanged.

## Regression result

- Main menu reports `0.3.88-dev`.
- Storyteller description and selection behavior are unchanged.
- Cassandra baseline and GateRim strategic orchestration remain unchanged.
- No new relevant XML, texture or C# error was reported.
- `docs/wiki/images/SG1_Command.png` is byte-identical to
  `Textures/Storytellers/SG1_Command.png`.

## Final decision

The two maintainer-provided portraits are accepted as final. Revision `r2`
completed the register, whitelist, wiki and metadata finalization, and all final
checks passed. Revision `r3` records the publication closure only.

The milestone is published through the final feature-branch commit,
fast-forward integration into `develop`, annotated tag `v0.3.88-dev` and the
synchronized separate wiki. No further functional or visual test remains for
this milestone.
