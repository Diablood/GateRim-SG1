# Maturation assistée des Prim'ta

> Statut : Prototype
> Version d'introduction : 0.1.58-dev
> Acquisition naturelle : 0.2.10-dev
> Rendu mobile de la reine finalisé : 0.3.105-dev

## Présentation

![Reine Goa'uld mobile](images/SG1_GoauldQueen_east.png)

La reine Goa'uld utilise une famille directionnelle distincte inspirée de la
Mère de tous les Tok'ra, mais adaptée à un pawn mobile. Son changement visuel ne
modifie pas son fonctionnement biologique.

![Bassin d'incubation du Prim'ta](images/SG1_PrimtaIncubationBasin.png)

Le bassin d'incubation du Prim'ta ne crée plus une larve entièrement à partir de
viande crue. Il fait désormais mûrir un symbiote immature issu d'une reine
Goa'uld.

```text
reine Goa'uld
    ↓
symbiote immature de Prim'ta
    +
20 unités de viande crue
    ↓
bassin d'incubation du Prim'ta
    ↓
larve de Prim'ta transportable
```

## Fonctionnement actuel

Une reine peut être obtenue par un incident rare :

- elle arrive directement sous le contrôle du joueur ;
- l'incident est bloqué tant qu'une reine vivante du joueur existe déjà ;
- son bouton d'extraction reste visible sans mode développeur ;
- une extraction fournit `1` symbiote immature physique ;
- le délai entre deux extractions est de `180000` ticks, soit `3` jours RimWorld ;
- le délai persiste après sauvegarde et rechargement.

## Ressource immature

Le symbiote immature de Prim'ta est une ressource biologique physique :

- stockable dans les produits biologiques Goa'uld ;
- empilable ;
- fragile ;
- détruite après pourrissement complet ;
- consommée par la maturation au bassin.

## Limites

La reine ne produit pas automatiquement sans action du joueur. Les chambres
reproductives spécialisées, les colonies Goa'uld, le commerce et les quêtes
restent prévus pour des jalons ultérieurs.

## Conservation

![Bassin de conservation du Prim'ta](images/SG1_PrimtaPreservationBasin.png)

Les symbiotes immatures et les larves matures peuvent être stockés dans un
[bassin de conservation du Prim'ta](Primta-Preservation-Basin) alimenté. Le
stockage spécialisé suspend l'aggravation sans restaurer une ressource déjà
détériorée.
