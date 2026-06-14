# Tok'ra safehouse contact

Hidden Tok'ra safehouses may contain a single peaceful Tok'ra contact.

The contact is intentionally limited:

- not hostile;
- not a trader;
- not recruitable;
- not a source of military aid;
- not a quest giver yet.

Starting with `0.2.20-dev`, a colonist can perform one short exchange with this
contact. Select a colonist, right-click the Tok'ra contact and choose the
exchange option. The dialogue gives a small narrative acknowledgement, a minor
Tok'ra trust increase and a Medicine learning boost for the selected colonist.

This interaction can only be used once for each generated safehouse contact.

Starting with `0.2.21-dev`, this exchange is treated as a short field medical
briefing. Starting with `0.2.22-dev`, the briefing scales lightly with current
Tok'ra trust:

| Tok'ra trust tier | Medicine XP |
|---|---:|
| Wary | 250 |
| Neutral | 400 |
| Cooperative | 600 |
| Trusted | 800 |

The exchange remains a small, non-repeatable contact outcome. It does not open
trade, recruitment, military aid or a quest chain.


Developer testing note: the safehouse preparation debug action prepares the
site test environment and keeps the current Tok'ra trust score. Adjust trust
with the dedicated increase/decrease debug actions before creating a new test
safehouse.
