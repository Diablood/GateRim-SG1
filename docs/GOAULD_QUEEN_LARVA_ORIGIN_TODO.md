# Goa'uld queen larva origin roadmap

## Status

```text
Partially implemented through 0.2.10-dev
```

The biological production loop is playable through assisted maturation and a
rare escaped-queen incident. Broader faction and infrastructure integration
remains deferred.

## Purpose

Replace the current abstract larva source with a biological origin:

```text
Goa'uld queen
    ↓
immature symbiotes
    ↓ assisted incubation
Prim'ta larvae
```

## Design questions

- Should later acquisition replace or supplement the current rare arrival?
- Should queens require a host, optionally use a host, or exist in both hosted
  and hostless forms?
- What specialized infrastructure should a controlled queen require?
- How should Goa'uld factions, Tok'ra, Free Jaffa, trade, raids and quests
  interact with them?

## Recommended order

```text
stabilize Jaffa loop
    ->
introduce Tok'ra foundation
    ->
expand Tok'ra iteration
    ->
design queen origin
    ->
replace abstract larva generation with assisted maturation
    ->
add rare player-controlled queen acquisition
```


## Tok'ra foundation milestone

`0.1.39-dev` introduces the first non-hunting Tok'ra symbiote and voluntary host
flow.

Queen-origin biology is now playable. Tok'ra, Free Jaffa, Goa'uld-faction,
trade and quest interactions remain later design work.
