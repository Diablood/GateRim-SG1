# Tok'ra interaction roadmap

Version: `0.2.25-dev`

This document freezes the current Tok'ra interaction direction before adding
larger rewards, quests or military support. It is intentionally a design and
handoff document: no gameplay implementation is added in this milestone.

## Current implemented loop

```text
hidden persistent Tok'ra faction
    -> trust score and trust tiers
    -> rare medical or safehouse-related events
    -> stored safehouse leads
    -> temporary hidden safehouse site
    -> peaceful non-trading Tok'ra contact
    -> once-per-contact briefing
```

Current contact outcome:

```text
+1 Tok'ra trust
trust-scaled Medicine XP
vanilla closeable briefing dialog
cooperative/trusted tiers can provide one follow-up safehouse lead
```

Current hard limits:

```text
no public Tok'ra settlements
no trader role
no recruitment
no automatic healing reward
no direct military aid
no repeatable reward farm
```

## Trust tiers and intended player meaning

```text
wary        : the Tok'ra remain cautious and limit contact
neutral     : basic medical information and modest clandestine support
cooperative : safehouse follow-up and more reliable coordination
trusted     : advanced medical support and emergency cooperation can begin
```

The trust score should remain an internal diplomatic foundation rather than a
replacement for RimWorld faction goodwill. Player-facing text should describe
what the Tok'ra are willing to risk, not expose raw numbers outside debug tools.

## Planned interaction families

### 1. Medical support

Already present:

```text
therapeutic opportunities
medical support gifts and deliveries
advanced medicine in trusted deliveries
safehouse medical briefing with Medicine XP
```

Future candidates:

```text
trusted safehouse medical offer
limited advanced treatment advice
rare ultratech medical cache
optional medical questline step
```

Medical support should remain the safest Tok'ra reward family and the most
appropriate early final reward for a short questline.

### 2. Safehouse network

Already present:

```text
safehouse signals
stored leads
lead cache
hidden world marker
enterable safehouse site
safehouse contact
follow-up lead at cooperative/trusted tiers
```

Future candidates:

```text
short safehouse chain with 2-3 steps
one explicit final contact after enough trust or leads
site timeout and secrecy pressure
choice between receiving supplies, intelligence or preserving the contact
```

The current `Prepare -> Create site -> Contact` test loop returning to `1/3`
leads is expected: creating the site consumes one lead, then the contact can
replace it.

### 3. Tok'ra communicator

Possible unlock condition:

```text
trusted tier
one completed safehouse chain
or a short questline finale
```

Possible form:

```text
building, item or invisible colony ability
long cooldown
requires an available hidden Tok'ra contact channel
may consume trust, a lead or a rare component depending on balance
```

Possible actions:

```text
request medical advice
request a safehouse lead
request emergency extraction support
request defensive disruption during a major attack
```

The communicator should not create normal trade, recruitment or a permanent
faction base. It represents a secure clandestine channel.

### 4. Military and defensive support

Military support should be rare, defensive and Tok'ra-flavored. It should not
become a standard ally reinforcement button.

Allowed directions:

```text
trusted tier only
long cooldown
emergency or large-threat context
small temporary effect
no recruitment
no permanent friendly squad
```

Candidate forms:

```text
1. Tactical warning
   - early warning before a threat
   - small preparation window
   - no direct combat spawn

2. Covert disruption
   - narrative sabotage of enemy coordination
   - minor raid delay, confusion or reduced pressure
   - safer than spawning many allies

3. Emergency Tok'ra response
   - 1-3 temporary Tok'ra agents arrive defensively
   - help repel the threat, then leave
   - trusted tier and long cooldown only

4. Extraction or evacuation help
   - assist a downed pawn, threatened guest or safehouse contact
   - more thematic than direct combat power
```

The first coded military-support step should probably be tactical warning or
covert disruption, not a full reinforcement squad.

### 5. Short questline option

A short Tok'ra questline can provide a clearer endpoint without requiring a
large faction system.

Candidate structure:

```text
Step 1: anonymous signal or cache
Step 2: safehouse lead and first contact
Step 3: trusted briefing or field problem
Finale: limited reward choice
```

Possible finale rewards:

```text
secure Tok'ra communicator
one advanced medical support unlock
trusted safehouse network access
rare emergency defensive support permission
```

The finale should choose one clear unlock first. Additional race-specific or
culture-specific branches can be added in later updates.

## Race and culture extensions for later

The Tok'ra should eventually react differently to major Stargate identities:

```text
Tau'ri / SG personnel       : easier operational trust and medical training
Free Jaffa                  : cautious alliance against Goa'uld
Goa'uld-aligned Jaffa       : suspicion, screening and slower trust
active Goa'uld hosts        : strong refusal or hostile caution
Tok'ra active hosts         : deeper cooperation and internal network options
true Goa'uld System Lords   : direct enemy context
```

These should be implemented as contextual modifiers and dialogue differences,
not hard locks everywhere. Manual player choices should remain possible when the
story supports it.

## Recommended next implementation order

```text
0.2.26-dev - Add trusted Tok'ra communicator foundation (implemented)
0.2.27-dev - Add communicator medical-support request
0.2.28-dev - Add communicator safehouse-lead request
0.2.29-dev - Add trusted Tok'ra tactical warning prototype
later      - Add short Tok'ra questline and race/culture branches
```

The exact order may change, but the communicator foundation is the cleanest
bridge between current safehouse contacts and future medical or military aid.

## 0.2.26-dev update

The first trusted-tier communicator foundation is now implemented as a powered
building. It opens only at the trusted Tok'ra tier and displays the future
request families in a vanilla closeable dialog. It deliberately does not grant
items, treatment, leads, military support, trade, recruitment or quests yet.

## Non-goals for the current chapter

```text
no full Tok'ra settlement faction
no open Tok'ra trade caravan
no normal recruitable Tok'ra soldiers
no large allied army calls
no automatic cure-all medical button
no direct Stargate travel requirement yet
```
