# Tok'ra tactical threat assessment request

Version: `0.2.31-dev`  
Updated: `0.2.37-dev`

This request gives the player a brief trusted-channel tactical report without becoming a combat power, spy satellite or allied reinforcement system.

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
tactical cooldown available
active hostile threat OR Tok'ra-intercepted threat
```

## Active-threat report

When enemies are already present on the map, the report summarizes:

```text
hostile pawn count
humanlike hostile count
mechanoid hostile count
other hostile signatures
broad severity label
one-day dedicated cooldown
```

## Intercepted-threat report

Since `0.2.37-dev`, if a Tok'ra cell has intercepted an approaching Goa'uld/Jaffa force, the same command can reveal useful information before the attack is visible:

```text
probable signature
probable intent
estimated arrival window
broad severity label
short preparation advice
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
