# Project state

Current milestone: `0.3.86-dev - Add final Goa'uld host and Jaffa xenotype icons`

Status: validated and published.

- Starting point: published `develop` aligned with annotated tag
  `v0.3.85-dev` at commit
  `7111ca0032b09c92bf3f4b0a1917a4bcfd03459a`.
- Final feature branch: `feature/final-xenotype-icons`.
- Final local revision: `r2`.
- Published assembly version: `0.3.86.0`.
- Final annotated tag: `v0.3.86-dev`.
- Integration target: `develop`, by fast-forward from the validated feature
  branch.

## Decided scope

- Replace only the two xenotype identity icons selected as the simplest first
  definitive-art lot.
- Keep the existing stable Goa'uld-host texture path:
  `UI/Xenotypes/SG1_GoauldHost`.
- Add the dedicated Jaffa texture path:
  `UI/Xenotypes/SG1_Jaffa`.
- Use the maintainer-supplied Jaffa icon exactly as approved: a simplified
  human/Baseliner form carrying the mark of Apophis.
- Use the approved simplified Goa'uld symbiote silhouette as the symbolic icon
  for the acquired host state. A host without its symbiote returns to ordinary
  human identity, so the defining organism is clearer than a forehead mark or
  difficult-to-render glowing eyes.
- Follow the established monochrome convention: white primary shapes with black
  or empty negative-space details.
- Preserve every xenotype Def name, label, gene list, combat factor,
  inheritable flag and save behavior.
- Update the visual register, automated final-family whitelist and progressive
  wiki reference in the same revision.
- Display both approved icons on their dedicated wiki pages and keep the wiki
  copies byte-identical to the gameplay textures.
- Do not extend this lot to genes, commands, factions, buildings, apparel or
  other temporary art.

## Implemented in revisions r1 and r2

- Replace `Textures/UI/Xenotypes/SG1_GoauldHost.png` with the approved
  `64×64` transparent white Goa'uld-symbiote icon.
- Add `Textures/UI/Xenotypes/SG1_Jaffa.png` from the exact maintainer-provided
  `64×64` PNG.
- Point `SG1_Jaffa.iconPath` away from the unrelated vanilla Hussar icon and to
  `UI/Xenotypes/SG1_Jaffa`.
- Keep `SG1_GoauldHost.iconPath` stable while replacing only its PNG contents.
- Remove both resolved P0 debts from the register:
  - personal demon icon reused for the Goa'uld-host xenotype;
  - vanilla Hussar icon reused for the Jaffa xenotype.
- Update the final visual baseline to:
  - `609` local PNG files;
  - `76` canonical local texture families;
  - `9` final local families;
  - `33` temporary-original families;
  - `15` temporary-recolor families;
  - `6` temporary-reuse families;
  - `13` personal-icon placeholder families;
  - `15` P0, `27` P1, `25` P2 and `9` completed local families;
  - `6` direct external texture paths.
- Extend the automated final-family whitelist with both xenotype icon paths.
- Add both approved icons to `docs/wiki/Visual-Assets.md` immediately rather
  than deferring them to a later global wiki pass.
- In `r2`, copy the exact validated PNG files to `docs/wiki/images/`, display
  them on `Jaffa.md`, `Active-Goauld-Host.md` and `Visual-Assets.md`, and make
  the visual checker reject wiki copies that differ from the gameplay icons.
- Align public and assembly metadata on `0.3.86-dev` / `0.3.86.0`.

## Explicit non-effects

This milestone does not:

- modify xenotype genes, inheritance, combat power or descriptions;
- change pawn generation, implantation, extraction or symbiote lifecycle code;
- alter any Def name or existing save identifier;
- add a gene, race, faction, item, mission, incident or storyteller rule;
- replace the remaining temporary gene, command, faction, building, apparel or
  creature visuals;
- modify `About/ModIcon.png`.

## Validation result

The maintainer reports final revision `r2` as successful.

Validated results include:

- successful build of assembly `0.3.86.0`;
- successful duration-formatting, visual-asset and complete
  project-consistency checks;
- correct Jaffa and Goa'uld-host icons at the real xenotype UI scale;
- removal of the vanilla Hussar and personal demon placeholders from these two
  xenotype surfaces;
- main-menu version `0.3.86-dev` and no new relevant `Player.log` error;
- exact wiki copies displayed on `Jaffa`, `Hôte Goa'uld actif` and
  `Références visuelles validées`;
- wiki image copies kept byte-identical to the gameplay PNGs;
- no xenotype, pawn-generation, implantation, extraction, lifecycle or save-data
  behavior changed by this milestone.

## Deferred issue discovered during validation

A custom starting group containing a Jaffa and a Tok'ra exposed an unrelated
pre-existing generation gap:

- a starting Jaffa selected only through its xenotype may lack a Prim'ta and
  quickly suffer withdrawal;
- a starting Tok'ra selected only through its xenotype may lack the real Tok'ra
  symbiote and dual-identity state, leaving only the enhanced human xenotype and
  one personality.

This is not caused or corrected by the icon milestone. It is recorded as a
separate future gameplay milestone covering starting pawns, custom scenarios,
reconciliation, duplicate prevention and the required biological identities.
No later version or branch is reserved yet.

## Publication state

The final milestone state is published through one feature-branch commit,
fast-forward integration into `develop`, push of `develop`, annotated tag
`v0.3.86-dev` and synchronization of the separate wiki.

No `rN` suffix belongs in the final commit or tag. `main` remains untouched.

## Next step

No later version or branch is reserved. Before starting another milestone:

1. verify local `develop`, `origin/develop` and peeled tag `v0.3.86-dev` point to
   the same integrated commit;
2. read `docs/ROADMAP.md` and select one distinct decided milestone;
3. create its dedicated `feature/*` or `fix/*` branch from the up-to-date
   `develop` branch;
4. update this handoff before implementation.
