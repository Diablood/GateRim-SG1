# Tok'ra communicator medical support request

Version: `0.2.29-dev`

This milestone adds the first medical request to the trusted Tok'ra secure
communicator. It remains deliberately limited: the Tok'ra provide guidance, not
an instant cure or a material shipment.

## Player-facing behavior

```text
trusted Tok'ra tier required
powered Tok'ra secure communicator required
selected colonist must operate the communicator
at least one wounded or sick human colonist must be present on the map
operator must be able to learn Medicine
grants 600 Medicine XP to the operator
starts a 3-day dedicated medical-channel cooldown
```

The request represents a short encrypted consultation with a nearby Tok'ra cell.
It is useful as expertise and progression, but it does not replace doctors,
medicine, surgery or hospital infrastructure.

## Design limits

```text
no direct treatment
no item delivery
no trade
no recruitment
no quest start
no military help
```

The request is a safe intermediate step before later milestones decide whether
trusted colonies can unlock rarer Tok'ra medical deliveries, emergency support
or questline outcomes.
