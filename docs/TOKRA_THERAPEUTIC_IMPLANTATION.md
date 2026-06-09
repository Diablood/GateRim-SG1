# Voluntary therapeutic Tok'ra implantation prototype

## Milestone

`0.1.45-dev — Add voluntary therapeutic Tok'ra implantation`

## Player flow

```text
free Tok'ra symbiote
    ↓
therapeutic implantation command
    ↓
nearby player-controlled compatible sick humanoid
    ↓
explicit confirmation dialog
    ↓
existing recent-implantation conversion
    ↓
active Tok'ra host
    ↓
automatic healing of configured serious pathologies
```

## Implementation notes

The new command is exposed only by free symbiotes whose existing properties
already allow Tok'ra voluntary implantation. The generic voluntary command is
kept unchanged.

Eligibility reuses the same compatible-host and range rules, then requires at
least one pathology known by `GameComponent_TokraTherapeuticHosting`. The
confirmation dialog is shown before calling the existing persistent transfer.

This keeps the prototype isolated from Goa'uld forced, autonomous and ritual
implantation workflows.
