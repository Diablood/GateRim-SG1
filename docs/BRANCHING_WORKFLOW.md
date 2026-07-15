# Branching workflow

Status: active project policy from `0.3.67-dev`, simplified command format from
the next milestone after `0.3.92-dev`.

## Purpose

GateRim SG-1 uses a lightweight integration workflow. The goal is to keep one
obvious development base without introducing the full overhead of GitFlow.
Published tags remain the immutable milestone archive.

The detailed validation and publication safeguards are defined in
[`MILESTONE_PUBLICATION.md`](MILESTONE_PUBLICATION.md).

## Command-format rule

Commands supplied for a real milestone must be directly copyable in PowerShell.

For a concrete milestone, always resolve and write literally:

- the version;
- the latest published tag;
- the feature or fix branch;
- the commit and tag messages;
- the local repository paths;
- the ZIP filename when one must be extracted.

Do not use PowerShell variables, `if` blocks, `throw`, `Push-Location` or
`Pop-Location` in the normal start or publication sequence. These constructs may
be used only for exceptional diagnostics explicitly requested by the maintainer.

Generic policy examples may retain markers such as `<version>` or
`<milestone-name>`. Before commands are given to the maintainer, every marker
must be replaced with the actual value.

## Branch roles

| Branch | Role |
|---|---|
| `main` | Stable public line, reserved for `1.0.0` and later stable releases or hotfixes |
| `develop` | Canonical integrated state of the latest validated development milestone |
| `feature/*` | Temporary branch for one gameplay, content, documentation or tooling milestone |
| `fix/*` | Temporary branch for one correction against the current development line |
| `hotfix/*` | Stable-line correction created from `main` after `1.0.0` |

No ordinary work is committed directly to `main` or `develop`.

## Initial migration

The workflow began without rewriting history:

1. create `develop` exactly at published tag `v0.3.66-dev`;
2. push `develop` to `origin`;
3. make `develop` the default branch during pre-`1.0.0` development;
4. preserve every existing tag and historical feature branch;
5. create `feature/develop-branch-workflow` from `develop` for `0.3.67-dev`.

The old published branches do not need to be merged again because their
validated commits are already ancestors of `v0.3.66-dev`.

## Starting a milestone

Run the following commands from the main repository with the actual latest tag
and future branch name already filled in:

```powershell
Set-Location "D:\SteamLibrary\steamapps\common\RimWorld\Mods\GateRim-SG1"
git switch develop
git pull --ff-only origin develop
git fetch origin --tags
git rev-list -n 1 v<latest-version>
git rev-parse develop
```

The last two commands must print the same commit identifier. Compare the two
displayed identifiers directly. If they differ, stop and reconcile `develop`
before creating the new branch.

Then:

```powershell
git status --short
git switch -c feature/<milestone-name>
git branch --show-current
```

A corrective milestone uses `fix/<correction-name>` instead.

Do not start an ordinary milestone from `main`, an old feature branch or a
working tree containing unrelated changes.

## Validation and final commit

Development, builds and local tests occur on the temporary branch. Do not merge
an unvalidated milestone into `develop`.

The final feature-branch commit uses the existing format:

```text
<version> - <short description>
```

Local `rN` revision suffixes never appear in the final commit or tag.

For an actual milestone, provide a literal block of this form:

```powershell
git status --short
git add -A
git diff --cached --check
git commit -m "<version> - <short description>"
```

## Integration into `develop`

After explicit publication authorization:

```powershell
git switch develop
git pull --ff-only origin develop
git merge --ff-only feature/<milestone-name>
git push origin develop
```

The fast-forward requirement keeps the history linear and ensures that the
validated feature commit is exactly the integrated commit.

## Final annotated tag

Create the tag only after fast-forward integration into `develop`:

```powershell
git tag -a v<version> -m "<version> - <short description>"
git push origin v<version>
```

The tag and `develop` must point to the same commit.

## Separate wiki synchronization

Only synchronize the wiki when files under `docs/wiki` changed.

Use direct paths and separate command blocks:

```powershell
Set-Location "D:\SteamLibrary\steamapps\common\RimWorld\Mods\GateRim-SG1.wiki"
git pull --ff-only
```

Then:

```powershell
Set-Location "D:\SteamLibrary\steamapps\common\RimWorld\Mods\GateRim-SG1"
.\tools\sync-wiki.cmd
```

Finally:

```powershell
Set-Location "D:\SteamLibrary\steamapps\common\RimWorld\Mods\GateRim-SG1.wiki"
git status --short
git add .
git diff --cached --check
git commit -m "<version> - document <milestone description>"
git push origin HEAD
```

Do not create an empty wiki commit.

## When `develop` moved during a milestone

If `git merge --ff-only` refuses because `develop` advanced, stop publication.
Replay the milestone on the updated integration branch:

```powershell
git switch develop
git pull --ff-only origin develop
git switch feature/<milestone-name>
git rebase develop
```

Resolve conflicts, rerun the relevant build and checks, then retry the normal
fast-forward publication sequence. Do not create an incidental merge commit.

## Optional feature-branch cleanup

After integration, tag publication and verification:

```powershell
git branch -d feature/<milestone-name>
```

If the temporary branch had also been pushed:

```powershell
git push origin --delete feature/<milestone-name>
```

Do not delete a branch before its integrated commit and tag have been verified.

## Stable `1.0.0` transition

When the development line is accepted as the first stable release:

```powershell
git switch main
git merge --ff-only develop
git push origin main
git tag -a v1.0.0 -m "1.0.0 - initial stable release"
git push origin v1.0.0
```

At that point `main` becomes the stable public line. New evolution continues
from `develop`.

## Stable hotfixes after `1.0.0`

A stable correction starts from `main` on `hotfix/*`. After validation, integrate
it into `main`, tag the stable patch release, then integrate the same correction
into `develop`. Never leave a stable fix absent from future development.

## Permanent invariants

- `develop` represents the latest validated and published development state.
- A new milestone starts from `develop`, not from a historical feature branch.
- Development tags are created on commits integrated into `develop`.
- `main` remains untouched before the stable line is intentionally opened.
- Feature branches are temporary; tags and integration branches preserve history.
- No published tag is rewritten or moved.
- Fast-forward integration is required; unexpected divergence blocks publication
  until reconciled and revalidated.
- Normal maintainer command sequences remain literal, short and sequential.
