# Tok'ra defensive diversion request

Version: `0.2.27-dev-r2`

This milestone adds the first active support request to the trusted Tok'ra
secure communicator.

## Player-facing behavior

```text
requires the Tok'ra trusted tier
requires a powered Tok'ra secure communicator
requires at least one hostile pawn active on the current map
briefly disrupts up to 3 hostile pawns
applies an immediate short stun
then schedules delayed nausea/vomiting for biological targets
starts a 5-day cooldown
```

The request represents covert Tok'ra sabotage: communications are scrambled,
orders are disrupted and a few attackers lose coordination. `r2` makes the
diversion feel less deterministic by combining a short immediate interruption
with a slightly delayed biological reaction.

## Design limits

```text
no friendly Tok'ra squad yet
no permanent military alliance
no trade or recruitment
no direct reward item
no quest start
not usable without a current attack
```

## Balance direction

This is intentionally weaker than a normal allied-reinforcement call. It is a
small emergency tool for trusted colonies and a test bed for future Tok'ra
communicator requests.

## 0.2.27-dev-r1 note

The first test build could detect hostile threats but always reported a failed
diversion because the stun call did not match RimWorld 1.6 method signatures.
`r1` keeps the same balance and fixes the stun invocation.

## 0.2.27-dev-r2 note

`r2` changes the successful diversion from a pure stun to a mixed disruption:
short immediate stun, followed by delayed vomiting for biological targets when
the vanilla vomit job is available. This keeps the effect defensive and limited
while making the timing less predictable.
