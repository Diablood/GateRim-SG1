# Goa'uld domain doctrines

> Version d’introduction : `0.3.64-dev`
> Statut : profils et influence relationnelle `0.3.80-dev` publiés

Goa'uld System Lord domains do not all react to resistance in exactly the same
way. Each domain receives one persistent strategic doctrine:

- **Conquest** favors overwhelming direct assaults.
- **Enslavement** favors taking colonists alive when capture is practical.
- **Scorched earth** favors destructive strikes against developed colonies.

These preferences affect only the relative choice between the three existing
Jaffa raid doctrines. Raid frequency, threat strength and the conditions needed
for specialized attacks still come from the storyteller and the colony's
current situation.

A doctrine belongs to the domain itself rather than its current Grand Master.
It therefore survives leader replacement and save/reload. Worlds containing
several Goa'uld domains track each one independently.

The domain's qualitative doctrine appears in its normal faction information.
Internal weights, thresholds and selection history remain hidden outside
developer diagnostics.

## Relation influence under SG-1 Command

Since `0.3.80-dev`, one highest-priority active relation can slightly influence
the weights of already eligible natural-raid doctrines:

- open conflict multiplies destruction by `1.25`;
- otherwise alliance multiplies direct assault by `1.25`;
- otherwise rivalry multiplies abduction by `1.25`;
- neutrality and truce do not modify weights.

Priority is `open conflict > alliance > rivalry`, and multiple relations never
stack. Existing point, colonist and building-wealth thresholds are evaluated
first, so an ineligible doctrine remains at zero. Other storytellers and
historical forced doctrine tests bypass this influence.
