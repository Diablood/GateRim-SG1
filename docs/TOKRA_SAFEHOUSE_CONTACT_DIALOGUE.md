# Tok'ra safehouse contact dialogue

Version: `0.2.21-dev`

This milestone extends the very small, non-trading dialogue outcome of the
peaceful Tok'ra contact generated inside hidden safehouse sites with a modest
medical briefing benefit.

## Scope

- The safehouse still generates one peaceful Tok'ra contact.
- The contact remains non-hostile, non-trading and non-recruitable.
- A player colonist can right-click the contact and choose a short exchange.
- The exchange displays a narrative message and adds `+1` Tok'ra trust.
- The negotiating colonist receives `400` Medicine XP as a one-time medical briefing benefit.
- The exchange can only be completed once per generated safehouse contact.
- No merchant stock, recruitment, military aid, world quest or caravan is added.

## Implementation note

`0.2.20-dev-r1` briefly attempted a Harmony-based right-click patch. This was
removed in `r2`. The dialogue is now exposed through a filtered `ThingComp`
attached to human pawns by XML patch. The comp only returns a float-menu option
for the generated Tok'ra safehouse contact carrying the internal dialogue hediff.


## 0.2.21-dev update

The once-per-contact exchange now doubles as a field medical briefing. The
selected colonist who performs the right-click interaction receives `400`
Medicine XP. This keeps the planque contact useful without adding trade,
recruitment, a repeatable reward loop, military aid or a quest chain.
