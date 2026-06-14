# Project state

0.2.25-dev - Define Tok'ra interaction roadmap

Status: prepared for local application and review.

This milestone is documentation/design focused. It freezes the current Tok'ra
interaction direction before adding larger rewards, a communicator, quests or
military support.

Current mod metadata after applying `0.2.25-dev`:

- `About/About.xml`: `modVersion = 0.2.25-dev`
- `Source/GateRimSG1/GateRimSG1.csproj`: `Version`, `AssemblyVersion` and `FileVersion` set to `0.2.25`

## Current implemented Tok'ra loop

```text
hidden persistent Tok'ra faction
    -> Tok'ra trust score and tiers
    -> medical/support incidents
    -> safehouse signals and stored leads
    -> temporary safehouse marker/site
    -> peaceful non-trading safehouse contact
    -> once-per-contact briefing
```

Implemented contact outcome:

```text
+1 Tok'ra trust
trust-scaled Medicine XP
closeable vanilla briefing dialog
cooperative/trusted tiers can provide one follow-up safehouse lead if capacity remains
```

Current limits remain:

```text
no trade
no recruitment
no automatic cure
no questline yet
no military aid yet
no permanent Tok'ra settlement
```

## Roadmap decision

Before adding a final reward, the Tok'ra direction is now split into planned
families:

```text
medical support
safehouse network
secure Tok'ra communicator
rare defensive military support
short questline
later race/culture-specific branches
```

Military support should be rare, defensive and trusted-tier only. Preferred
first directions are tactical warning or covert disruption, before any temporary
agent reinforcement.

Recommended next implementation path:

```text
0.2.26-dev - Add trusted Tok'ra communicator foundation
0.2.27-dev - Add communicator medical-support request
0.2.28-dev - Add communicator safehouse-lead request
0.2.29-dev - Add trusted Tok'ra tactical warning prototype
later      - Add short Tok'ra questline and race/culture branches
```

## Build note

No gameplay C# logic is changed in `0.2.25-dev`. A rebuild is optional unless
assembly metadata must be refreshed locally.

## Repository rules reminder

- Work on a dedicated branch.
- Do not switch to `main` as a working base.
- Create annotated tags prefixed with `v`.
- Keep ZIP archives ignored and out of commits.
- Preserve `About/ModIcon.png`.
