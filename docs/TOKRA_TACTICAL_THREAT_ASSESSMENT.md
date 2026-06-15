# Tok'ra tactical threat assessment request

Version: `0.2.31-dev`

This milestone adds an informational tactical request to the pawn-operated
Tok'ra secure communicator. It gives the player a brief trusted-channel report
without becoming a combat power, spy satellite or allied reinforcement system.

## Player-facing behavior

```text
select a player colonist
right-click the Tok'ra secure communicator
request Tok'ra tactical threat assessment
colon operates the communicator briefly
vanilla report window opens
```

## Requirements

```text
trusted Tok'ra trust tier
powered communicator
active hostile threat on the current map
tactical cooldown available
```

## Current report

```text
hostile pawn count
humanlike hostile count
mechanoid hostile count
other hostile signatures
broad severity label
one-day dedicated cooldown
```

## Boundaries

```text
no damage
no stun
no vomiting
no healing
no item delivery
no map reveal
no trade
no recruitment
no quest
no reinforcements
```
