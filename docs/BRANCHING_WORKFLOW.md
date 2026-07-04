# Branching workflow

Status: active project policy from `0.3.67-dev`.

## Purpose

GateRim SG-1 uses a lightweight integration workflow. The goal is to keep one
obvious development base without introducing the full overhead of GitFlow.
Published tags remain the immutable milestone archive.

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

The workflow begins without rewriting history:

1. create `develop` exactly at published tag `v0.3.66-dev`;
2. push `develop` to `origin`;
3. make `develop` the repository default branch while the project remains on
   the pre-`1.0.0` development line;
4. preserve every existing tag and historical feature branch;
5. create `feature/develop-branch-workflow` from `develop` for `0.3.67-dev`.

The old published branches do not need to be merged again because their
validated commits are already ancestors of `v0.3.66-dev`.

## Starting a milestone

Update the integration branch first:

```powershell
git switch develop
git pull --ff-only origin develop
```

Confirm that it points to the latest published development tag:

```powershell
$tagCommit = git rev-list -n 1 v<latest-version>
$developCommit = git rev-parse develop

if ($tagCommit -ne $developCommit) {
    throw "develop does not match the latest published development tag."
}
```

Use `git rev-list -n 1` (or the peeled `v<version>^{}` form) for annotated
tags. Plain `git rev-parse v<version>` may return the tag-object identifier
instead of the commit identifier.

Then create the dedicated branch:

```powershell
git switch -c feature/<milestone-name>
```

A corrective milestone against the current development line uses `fix/*` in
the same way.

## Validation and commits

Development, builds and local tests occur on the temporary branch. Do not merge
an unvalidated milestone into `develop`.

The final feature-branch commit uses the existing format:

```text
<version> - <short description>
```

Local `rN` revision suffixes never appear in the final commit or tag.

## Integration and publication

After explicit publication authorization:

```powershell
git switch develop
git pull --ff-only origin develop
git merge --ff-only feature/<milestone-name>
git push origin develop
```

The fast-forward requirement keeps the history linear and ensures the
validated feature commit is exactly the integrated commit.

Create the annotated tag only after the fast-forward integration:

```powershell
git tag -a v<version> -m "<version> - <short description>"
git push origin v<version>
```

The tag and `develop` must point to the same commit.

Publishing the temporary branch is optional. It may be pushed for backup or
review, but it is not the durable project base. After successful integration,
tagging and verification, it may be deleted locally and remotely:

```powershell
git branch -d feature/<milestone-name>
git push origin --delete feature/<milestone-name>
```

Do not delete a branch before its integrated commit and tag have been verified.

## When `develop` moved during a milestone

If `git merge --ff-only` refuses because `develop` advanced, stop publication.
Update and replay the milestone rather than creating an incidental merge commit:

```powershell
git switch develop
git pull --ff-only origin develop
git switch feature/<milestone-name>
git rebase develop
```

Resolve conflicts, rerun the relevant build and checks, then retry the
fast-forward integration.

## Stable `1.0.0` transition

When the development line is accepted as the first stable release:

```powershell
git switch main
git merge --ff-only develop
git push origin main
git tag -a v1.0.0 -m "1.0.0 - initial stable release"
git push origin v1.0.0
```

At that point `main` becomes the stable default line. New evolution continues
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
- Fast-forward integration is preferred; unexpected divergence blocks
  publication until reconciled and revalidated.
