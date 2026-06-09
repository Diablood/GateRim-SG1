# Paliers de confiance Tok'ra

> Statut : Prototype
> Version d'introduction : 0.1.51-dev

## Principe

La jauge persistante de confiance Tok'ra possède désormais quatre paliers.
Lorsqu'une opportunité thérapeutique escortée est créée, son palier fixe la
durée de l'offre et la taille de l'escorte pacifique.

```text
méfiante    : -100 à -1
neutre      : 0 à 9
coopérative : 10 à 24
fiable      : 25 à 100
```

## Effets

| Palier | Durée de l'offre | Taille de l'escorte | Soutien en trétonine |
|---|---:|---:|---:|
| Méfiante | 1 jour RimWorld | exactement 1 hôte | aucun |
| Neutre | 2 jours RimWorld | 1 à 2 hôtes | aucun |
| Coopérative | 3 jours RimWorld | exactement 2 hôtes | 1 dose |
| Fiable | 4 jours RimWorld | 2 à 3 hôtes | 2 doses |

Une offre déjà active conserve ses paramètres même si la confiance change
ensuite. Les nouveaux paramètres sont calculés uniquement au démarrage de la
prochaine opportunité.

## Limites actuelles

Depuis `0.1.52-dev`, les paliers coopérative et fiable débloquent un petit
cadeau physique de trétonine lors de l'arrivée de l'équipe. Les paliers ne
modifient pas encore la fréquence storyteller et ne déclenchent pas de quête
dédiée. La faction Tok'ra reste masquée et séparée de la diplomatie vanilla.
