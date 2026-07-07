# Current milestone test procedure

Jalon : `0.3.85-dev - Audit provisional visual assets`

Revision to test: `r3`

Version de DLL attendue : `0.3.85.0`

Status: **final revision `r3` validated and published**.

## Purpose

Validate the repository-wide visual inventory and its read-only checker. No PNG,
Def behavior, rendering code or gameplay rule changes in this milestone.

## Revision history

- `r1`: the first Windows PowerShell 5.1 run failed before meaningful inventory
  comparison. `Path.ChangeExtension($relative, $null)` left a trailing period,
  so the checker reported `608` false local families and `82` false external
  paths even though PNG signatures and registered totals were valid.
- `r2`: remove that overload-sensitive extension step and normalize each original
  `.png` relative path directly.
- `r3`: apply the maintainer-approved final-art boundary. Keep only the seven
  validated world-event site families final, keep `About/ModIcon.png` final
  outside the local family count, downgrade storyteller/faction visuals to
  temporary, add a final-family checker whitelist and create the progressive wiki
  reference page.

## Validation result

The maintainer reports the complete required procedure as successful:

- build `0.3.85.0`;
- duration-formatting check;
- visual-asset check with the expected `608 / 75 / 7 / 7` baseline and zero
  missing or unregistered local family;
- complete project-consistency check and clean diff;
- focused review and acceptance of the strict final/temporary boundary, P0/P1
  findings and progressive wiki-reference rule;
- RimWorld launch to the main menu with version `0.3.85-dev`;
- no new relevant XML, texture, translation or C# error in `Player.log`.

No PNG, Def, rendering, gameplay or save-data change was introduced.

## Automated checks

From the repository root:

```powershell
./build.cmd
./tools/check-duration-formatting.cmd
./tools/check-visual-assets.cmd
./tools/check-project-consistency.cmd
git diff --check
```

Expected build version:

```text
Assembly: 0.3.85.0
```

Expected visual checker summary:

```text
Final local texture families: 7
Local PNG files: 608
Local texture families: 75
Direct external texture paths: 7
Missing local references: 0
Unregistered local families: 0
Visual asset check passed.
```

Do not continue if any command fails.

## Required focused review

Open:

```text
docs/VISUAL_ASSET_REGISTER.md
```

Confirm that the register contains:

- `608` local PNG files;
- `75` canonical local texture families;
- `7` accepted final local families;
- `33` temporary-original families;
- `15` temporary recolor families;
- `6` temporary reuse families;
- `14` personal-icon placeholder families;
- priorities `16` P0, `27` P1, `25` P2 and `7` done;
- `7` direct vanilla texture dependencies;
- `About/ModIcon.png` recorded as final public mod art outside the local family
  count;
- exactly seven final local families, all under
  `World/WorldObjects/Expanding/Sites/`;
- storyteller portraits and four world-faction icons recorded as temporary;
- the progressive wiki reference in `docs/wiki/Visual-Assets.md`.

Review the P0/P1 findings and confirm that the priority order is acceptable:

1. command, gene, xenotype and basin surfaces using the personal demon icon;
2. kara kesh and healing bracelet using the Zat texture;
3. Jaffa xenotype using the vanilla Hussar icon;
4. Prim'ta larva and free symbiote sharing one image;
5. Tok'ra mission objects sharing packet or communicator art;
6. prominent Jaffa, officer, Tok'ra and SGC apparel remaining temporary.

## Minimal RimWorld smoke test

Because the milestone changes assembly metadata but no gameplay code or image:

1. launch RimWorld;
2. confirm GateRim SG-1 reports version `0.3.85-dev`;
3. reach the main menu;
4. exit normally;
5. inspect `Player.log`.

Expected result:

- no missing texture warning;
- no XML parse error;
- no translation error;
- no C# initialization error;
- no visual-check script side effect on the repository.

No new game, world generation or map scenario is required.

## Optional negative checker tests

These tests are useful but are not required for focused acceptance.

### Unregistered texture family

Temporarily copy one PNG to a new canonical path under `Textures/`, run:

```powershell
./tools/check-visual-assets.cmd
```

Expected: non-zero exit with `Unregistered local texture family`.

Delete the temporary file and rerun successfully.

### Missing registered family

Temporarily rename one registered PNG family, run the checker and expect:

```text
Registered family without matching PNG files
```

Restore the original path and rerun successfully.

### Stale external dependency

Temporarily alter one direct vanilla path in the register or a Def. The checker
must reject either an unregistered external path or a stale registered path.
Restore the file and rerun successfully.

## Acceptance report

Report:

- build result;
- duration checker result;
- visual checker result;
- project consistency result;
- `git diff --check` result;
- whether the register's P0/P1 priorities are accepted;
- main-menu smoke-test result;
- relevant `Player.log` errors, if any.

The result is recorded in `docs/PROJECT_STATE.md` and retained here as the
final focused acceptance report.

## Publication state

Final revision `r3` is published through the feature-branch commit,
fast-forward integration into `develop`, annotated tag `v0.3.85-dev` and
synchronization of the separate wiki.
