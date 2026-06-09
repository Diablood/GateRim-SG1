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

| Palier | Durée de l'offre | Escorte de l'offre | Cadeau de l'offre | Livraison indépendante |
|---|---:|---:|---:|---:|
| Méfiante | 1 jour RimWorld | exactement 1 hôte | aucun | indisponible |
| Neutre | 2 jours RimWorld | 1 à 2 hôtes | aucun | indisponible |
| Coopérative | 3 jours RimWorld | exactement 2 hôtes | 1 dose | 2 doses avec 1 visiteur |
| Fiable | 4 jours RimWorld | 2 à 3 hôtes | 2 doses | 4 doses avec 2 visiteurs |

Une offre déjà active conserve ses paramètres même si la confiance change
ensuite. Les nouveaux paramètres sont calculés uniquement au démarrage de la
prochaine opportunité.

## Limites actuelles

Depuis `0.1.52-dev`, les paliers coopérative et fiable débloquent un petit
cadeau physique de trétonine lors de l'arrivée de l'équipe. Depuis `0.1.53-dev`,
ils débloquent aussi une [livraison médicale indépendante](Tokra-Medical-Support-Deliveries).
La fréquence des opportunités thérapeutiques reste inchangée et aucune quête
dédiée n'est encore active. La faction Tok'ra reste masquée et séparée de la
diplomatie vanilla.
