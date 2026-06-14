# Tok'ra secure communicator

Version: `0.2.26-dev`

This milestone adds the first trusted-tier Tok'ra secure communicator
foundation. The goal is to create a stable interaction point for future Tok'ra
requests before implementing the requests themselves.

## Scope

- Adds `SG1_TokraSecureCommunicator`, a powered buildable one-cell building.
- Construction requires vanilla `MicroelectronicsBasics`.
- The communicator command requires the trusted Tok'ra trust tier.
- Lower tiers see the locked state through the disabled command/inspect text.
- A successful contact opens a vanilla closeable dialog.
- The dialog lists future request families: medical advice, safehouse leads and
  rare defensive support.

## Non-goals

```text
no item reward
no direct treatment
no safehouse lead request yet
no military support yet
no trade
no recruitment
no quest start
```

## Implementation notes

The building uses `Comp_TokraSecureCommunicator`. The component checks power,
player control and `GameComponent_TokraTrustTracker.GetCurrentTier()`. The
command only opens the channel at `TokraTrustTier.Trusted`.

This keeps the current Tok'ra design intact: trusted access becomes visible, but
the next actual request mechanics can be implemented one at a time.
