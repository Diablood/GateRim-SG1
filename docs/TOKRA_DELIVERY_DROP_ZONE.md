# Tok'ra delivery drop zone

> Introduced in `0.2.38-dev`.

The Tok'ra delivery drop zone is a discreet ground marker used by the colony to indicate where clandestine Tok'ra caches should appear.

## Placement

- The marker is placed from the Architect menu.
- It is free and immediate to place.
- It does not require materials, work, research or a colonist builder.
- It is not meant to be minified or transported: moving it is done by placing a new marker.
- Only one marker is kept per map: placing a new one removes the previous one.

## Delivery priority

Tok'ra cache placement now checks delivery locations in this order:

1. The Tok'ra delivery drop zone, when present.
2. A powered Tok'ra secure communicator, when no delivery zone exists.
3. A reachable unfogged map-edge cell as a final fallback.

## Current use

The marker is used by:

- manual Tok'ra emergency medical cache requests;
- rare hidden-cell Tok'ra caches;
- Tok'ra safehouse lead caches;
- escorted Tok'ra medical-support supply drops, while the visitors still enter from the map edge.

It is also intended as the future target for discreet Tok'ra mission drops and automatic material deliveries.

## Limits

The marker does not store items, produce resources, call reinforcements, trade, recruit, heal colonists or start quests by itself.


The marker is non-minifiable and cannot be transported. To move it, place a new marker; the previous marker is removed automatically.
