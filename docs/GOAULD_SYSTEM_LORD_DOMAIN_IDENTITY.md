# Goa'uld System Lord domain identity foundation

Version: `0.1.74-dev r1`

## Scope

The data-driven Goa'uld domain identity profile remains in place, but its rank
slots now reference intrinsic forehead-mark Defs rather than technical genes.
Since `0.2.1-dev`, the same profile is attached to the visible Goa'uld
world-faction baseline and its rare direct-assault raid path.

## Domain Def

The first profile is:

```text
SG1_GoauldSystemLordDomainPrototype
```

It maps rank slots to intrinsic marks:

| Rank slot | Temporary intrinsic mark |
|---|---|
| Ordinary | `SG1_JaffaForeheadMark_GenericIntrinsic` |
| Elite | `SG1_JaffaForeheadMark_GenericSilverIntrinsic` |
| First Prime | `SG1_JaffaForeheadMark_GenericGoldIntrinsic` |

## Faction association

The visible world faction receives a
`GoauldSystemLordDomainExtension` through an XML patch.

```xml
<li Class="GateRimSG1.Goauld.GoauldSystemLordDomainExtension">
    <domain>SG1_GoauldSystemLordDomainPrototype</domain>
</li>
```

## Resolver

`GoauldSystemLordDomainUtility` centralizes domain and intrinsic-mark lookup.
If a lookup needs a fallback, the resolver still returns the prototype domain.
Missing higher-rank slots fall back progressively toward the ordinary mark.

Automatic assignment now checks `HasAssignedDomain(...)` first. This prevents
Free Jaffa from receiving a Goa'uld-domain mark while preserving the fallback
for explicit domain lookups and manual tools.

## Current limits

- Goa'uld-domain Jaffa initialize with the ordinary black mark.
- Free Jaffa remain unmarked by default.
- Silver and gold marks are available through developer tools but are not assigned automatically.
- Named System Lords are not introduced yet.
- Visible settlements and rare direct-assault raids are enabled since `0.2.1-dev`.
- Natural abduction raids, natural destruction raids and traders remain disabled.
- Final emblem artwork and lateral calibration remain deferred.
