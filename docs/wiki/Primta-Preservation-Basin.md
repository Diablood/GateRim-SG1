# Bassin de conservation du Prim'ta

> Statut : Prototype
> Version d'introduction : 0.1.59-dev

## Présentation

![Bassin de conservation du Prim'ta](images/SG1_PrimtaPreservationBasin.png)

Le bassin de conservation du Prim'ta est un stockage spécialisé alimenté en
électricité. Il maintient un environnement biologique interne idéal pour les
ressources fragiles du Prim'ta.

Son visuel bleu définitif le distingue du bassin d'incubation vert. Il occupe une
case et ne reçoit ni coloration de matériau ni ombre de bord héritée.

Il accepte uniquement :

- les symbiotes immatures issus d'une reine Goa'uld ;
- les larves de Prim'ta matures prêtes pour une implantation.

## Fonctionnement

```text
bassin alimenté
    ↓
stabilisation biologique interne
    ↓
aggravation du pourrissement suspendue
```

Une ressource déjà détériorée n'est pas réparée lorsqu'elle est placée dans le
bassin.

## Réfrigérateurs et congélateurs

Le bassin ne remplace pas la mécanique de température ambiante :

- sans bassin, un réfrigérateur ralentit toujours la détérioration ;
- sans bassin, le gel interrompt encore provisoirement la détérioration ;
- les fortes chaleurs restent dangereuses ;
- la congélation profonde sous `-15 °C` accumule désormais une exposition
  biologique persistante hors bassin actif ;
- consulte [Congélation profonde des Prim'ta](Primta-Deep-Freezing).
