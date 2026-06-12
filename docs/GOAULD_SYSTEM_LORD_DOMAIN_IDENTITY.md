# Goa'uld System Lord domain identity foundation

Version: `0.1.74-dev r1`

## Scope

The data-driven Goa'uld domain identity profile remains in place, but its rank
slots now reference intrinsic forehead-mark Defs rather than technical genes.
No natural raids or world presence are enabled.

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

The hidden prototype faction still receives a
`GoauldSystemLordDomainExtension` through an XML patch.

```xml
<li Class="GateRimSG1.Goauld.GoauldSystemLordDomainExtension">
    <domain>SG1_GoauldSystemLordDomainPrototype</domain>
</li>
```

## Resolver

`GoauldSystemLordDomainUtility` centralizes domain and intrinsic-mark lookup.
If a faction has no explicit extension, the resolver falls back to the
prototype domain. Missing higher-rank slots fall back progressively toward the
ordinary mark.

## Current limits

- Compatible Jaffa currently initialize with the ordinary black fallback.
- Silver and gold marks are available through developer tools but are not assigned automatically.
- Named System Lords are not introduced yet.
- Natural Goa'uld raids, settlements and traders remain disabled.
- Final emblem artwork and lateral calibration remain deferred.
