# Tok'ra communicator status report

Version: `0.2.32-dev-r2`

The Tok'ra communicator status report is a non-effect request operated by a
selected colonist from the secure communicator right-click menu.

## Purpose

The communicator now exposes several independent channels. The status report
shows the current availability of those channels without triggering any support
request.

`0.2.32-dev-r1` replaced the first technical/debug-style report text with a more
player-facing, in-universe Tok'ra transmission. `0.2.32-dev-r2` removes the
power-state line from the visible report because the player already sees and
uses the communicator only when it is powered.

```text
trusted channel state
defensive-diversion cooldown or availability
tactical-assessment cooldown or availability
medical-guidance cooldown or availability
emergency-cache cooldown or availability
local hostile count
local wounded/sick human colonist count
```

## Rules

```text
selected player colonist required
right-click on the Tok'ra secure communicator
no trusted-tier requirement for the status consultation itself
no cooldown consumed
no item created
no healing
no combat effect
no reinforcement
no map reveal
```

The report can therefore be used safely before deciding whether a real Tok'ra
request should be made.
