# Casque Jaffa rétractable

> Statut : Prototype
> Version d'introduction : 0.1.67-dev

## Modes disponibles

| Mode | Hors enrôlement | Porteur enrôlé |
|---|---|---|
| Automatique | rétracté | déployé |
| Toujours déployé | déployé | déployé |
| Toujours rétracté | rétracté | rétracté |

Le choix est conservé après sauvegarde et rechargement. Les valeurs brutes
d'armure restent identiques. Seule la couverture varie : `UpperHead` lorsque le
casque est rétracté, `FullHead` lorsqu'il est déployé.

Lorsque l'un de tes propres Jaffa porte le casque, sélectionne-le puis utilise
le gizmo :

```text
Mode du casque Jaffa
```

## Variante d'officier

`0.3.74-dev-r1` a ajouté une seconde paire de casque rétractable réservée à
l'officier capturable. Elle utilise une teinte rouge temporaire et des chemins
définitifs. La révision finale `r2` garantit explicitement que le casque
déployé est équipé sur la
nouvelle cible avant son apparition sur la carte. Les modes, la persistance, les
valeurs d'armure et les couvertures restent identiques, mais chaque casque
commute uniquement avec sa propre paire. Les trois modes et la persistance sont
validés dans `v0.3.74-dev`.
