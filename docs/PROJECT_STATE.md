# Project state

Current milestone: `0.3.85-dev - Audit provisional visual assets`

Status: validated and published.

- Starting point: published `develop` aligned with annotated tag
  `v0.3.84-dev` at commit
  `393c36e3824e4df3ec0958580e759e4c8ac864c4`.
- Final feature branch: `feature/provisional-visual-asset-audit`.
- Final local revision: `r3`.
- Published assembly version: `0.3.85.0`.
- Final annotated tag: `v0.3.85-dev`.
- Integration target: `develop`, by fast-forward from the validated feature
  branch.

## Decided scope

- Inventory every image shipped by the repository and every texture path directly
  referenced by XML or C#.
- Group directional and body-type files under one canonical texture family while
  retaining the exact physical-file count.
- Classify each local family as final, temporary original, temporary recolor,
  temporary reuse or personal-icon placeholder. Only explicitly approved visuals
  may be final.
- Record every direct vanilla texture dependency as accepted or provisional.
- Assign replacement priority `P0`, `P1`, `P2`, `P3` or `done`.
- Preserve every current Def name and texture path. This milestone audits art but
  replaces no PNG and changes no rendering behavior.
- Preserve `About/ModIcon.png` as final public mod art while explicitly forbidding
  its reuse as gameplay, gene, xenotype, building or command art.
- Treat only the seven validated world-event site icons as final local families;
  all storyteller, faction, equipment, building, gene, xenotype, projectile, pawn
  and command visuals remain temporary until explicit maintainer approval.
- Publish each newly approved visual to the wiki reference page in the same
  revision, instead of relying on one later global wiki pass.
- Add a read-only automated check that fails when a local texture family,
  physical-file count or external texture dependency drifts from the register.
- Integrate the visual check into the existing project-consistency command.
- Keep the future definitive-art replacement as later implementation work split
  into manageable priority lots.

## Implemented local revisions r1 to r3

- `docs/VISUAL_ASSET_REGISTER.md` is the authoritative register for:
  - `608` local PNG files;
  - `75` canonical local texture families;
  - `7` direct vanilla texture paths;
  - `2` runtime vanilla icon constants;
  - the preserved repository-level `About/ModIcon.png`.
- The audit identifies:
  - `7` accepted final local families;
  - `33` temporary-original families;
  - `15` temporary recolor families;
  - `6` temporary reuse families;
  - `14` personal-icon placeholder families;
  - `16` P0, `27` P1, `25` P2 and `7` completed local families.
- Highest-priority debt includes:
  - command, gene, xenotype and basin surfaces reusing the personal demon icon;
  - the kara kesh and healing bracelet reusing the Zat texture;
  - the Jaffa xenotype using the vanilla Hussar icon;
  - the Prim'ta larva and free symbiote sharing one image;
  - several Tok'ra mission objects sharing one packet or communicator image.
- `tools/check-visual-assets.ps1` and `.cmd` now:
  - verify PNG signatures;
  - normalize directional/body-type variants into canonical families;
  - compare all physical files and counts against the register;
  - parse direct XML and C# texture references;
  - reject missing, unreferenced, unregistered or case-drifted families;
  - reject unregistered or stale direct vanilla texture paths.
- `tools/check-project-consistency.ps1` invokes the visual checker as a mandatory
  project consistency stage.
- Version and public documentation are aligned on `0.3.85-dev` / `0.3.85.0`.
- The first Windows PowerShell 5.1 run of `r1` exposed a checker-only
  normalization defect: `Path.ChangeExtension($relative, $null)` left a trailing
  period, producing `608` false one-file families instead of the registered `75`
  canonical families and sending real local paths into the external-path list.
- Corrective revision `r2` removes that overload-sensitive preprocessing and
  sends the original `.png` path to `Normalize-TextureFamily`, whose existing
  extension and variant stripping now applies consistently.
- Corrective revision `r3` reclassifies the two storyteller portraits and four
  faction icons from final to temporary-original/P1, records the seven accepted
  event-site families as the only final local set, and adds a checker whitelist.
  No PNG, Def, gameplay behavior or texture path changes.
- `docs/wiki/Visual-Assets.md` becomes the progressive public reference for
  approved visuals. It begins with `About/ModIcon.png` and the seven validated
  event-site icons, and must be updated whenever another visual is accepted.

## Explicit non-effects

This milestone does not:

- change any PNG content;
- rename a Def or texture path;
- modify rendering code;
- alter stats, research, recipes, equipment availability or pawn generation;
- change factions, missions, storyteller behavior or save data;
- add or remove any gameplay object.

## Validation result

The maintainer reports the complete final `r3` procedure as successful.

Validated behavior includes:

- successful local build of assembly `0.3.85.0`;
- successful duration-formatting, visual-asset and complete project-consistency
  checks;
- visual-check summary of `608` PNG files, `75` canonical local families, `7`
  final local families, `7` direct external texture paths, no missing local
  reference and no unregistered local family;
- maintainer acceptance of the strict final-art boundary, P0/P1 findings and
  replacement order;
- `About/ModIcon.png` retained as final public art outside the local-family
  count, with exactly seven final event-site families under `Textures/`;
- storyteller, faction, equipment, building, gene, xenotype, projectile, pawn
  and command visuals retained as temporary until explicit approval;
- progressive wiki-reference rule accepted for every future validation or new
  visual addition;
- successful RimWorld launch to the main menu with version `0.3.85-dev`;
- no new relevant XML, texture, translation or C# error in `Player.log`;
- no PNG, Def, rendering, gameplay or save-data change.

## Publication state

The final milestone state is published through one feature-branch commit,
fast-forward integration into `develop`, push of `develop`, annotated tag
`v0.3.85-dev` and synchronization of the separate wiki because this milestone
adds and changes `docs/wiki/` sources.

No `rN` suffix belongs in the final commit or tag. `main` remains untouched.

## Next step

No later version or branch is reserved. Before starting another milestone:

1. verify local `develop`, `origin/develop` and peeled tag `v0.3.85-dev` point to
   the same integrated commit;
2. read `docs/ROADMAP.md` and select one distinct decided milestone;
3. create its dedicated `feature/*` or `fix/*` branch from the up-to-date
   `develop` branch;
4. update this handoff before implementation.
