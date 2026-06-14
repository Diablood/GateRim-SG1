# Tok'ra safehouse contact dialogue

Version: `0.2.22-dev`

This milestone keeps the very small, non-trading dialogue outcome of the
peaceful Tok'ra contact generated inside hidden safehouse sites, then makes the
medical briefing scale lightly with the current Tok'ra trust tier.

## Scope

- The safehouse still generates one peaceful Tok'ra contact.
- The contact remains non-hostile, non-trading and non-recruitable.
- A player colonist can right-click the contact and choose a short exchange.
- The exchange displays a narrative message and adds `+1` Tok'ra trust.
- The negotiating colonist receives Medicine XP as a one-time medical briefing benefit.
- The Medicine XP now scales with the current Tok'ra trust tier:
  - wary: `250` XP;
  - neutral: `400` XP;
  - cooperative: `600` XP;
  - trusted: `800` XP.
- The exchange can only be completed once per generated safehouse contact.
- No merchant stock, recruitment, military aid, world quest or caravan is added.

## Implementation note

`0.2.20-dev-r1` briefly attempted a Harmony-based right-click patch. This was
removed in `r2`. The dialogue is now exposed through a filtered `ThingComp`
attached to human pawns by XML patch. The comp only returns a float-menu option
for the generated Tok'ra safehouse contact carrying the internal dialogue hediff.

## 0.2.22-dev update

The once-per-contact field medical briefing now reads the current Tok'ra trust
tier before applying the contact acknowledgement. Better trust gives a more
substantial, but still modest, Medicine learning gain and a slightly more open
narrative message.

This keeps trust visible without adding trade, recruitment, a repeatable reward
loop, military aid or a quest chain.

## Developer testing note

`0.2.22-dev-r1` adds two lightweight developer actions instead of one action
per trust tier:

- `Increase Tok'ra trust test step`: raises Tok'ra trust by `+5`, clamped
  to the existing maximum.
- `Decrease Tok'ra trust test step`: lowers Tok'ra trust by `-5`, clamped
  to the existing minimum.

This keeps the test tools generic while still making it easy to reach the
cooperative threshold (`10`) and trusted threshold (`25`) before generating a
fresh safehouse contact.

## 0.2.22-dev-r2 testing adjustment

The developer action `Prepare Tok'ra safehouse site test` now prepares only the
safehouse test environment: it removes inactive safehouse markers or sites and
stores one safehouse lead. It deliberately preserves the current Tok'ra trust
score.

This avoids a misleading test sequence where the player raises trust to the
cooperative or trusted tier, runs the prepare action again to create a fresh
safehouse, and accidentally resets trust back to neutral before speaking to the
next contact. Use the `Increase Tok'ra trust test step` and `Decrease Tok'ra
trust test step` actions to adjust the score before generating the next
safehouse contact.
