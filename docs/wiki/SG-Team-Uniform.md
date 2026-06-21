# Tenue de terrain des équipes SG

> Statut : système modulaire jouable
> Première version du treillis combiné : `0.1.78-dev`
> Découpage modulaire : `0.3.21-dev`

## Présentation

La tenue de terrain SG est désormais composée de couches indépendantes :

- un tee-shirt vanilla porté au contact de la peau ;
- un pantalon de terrain SG ;
- une veste de terrain SG facultative ;
- des bottes et des gants tactiques ;
- un gilet tactique porté par-dessus la veste.

Ce découpage permet aux membres d'une même équipe d'utiliser des variantes différentes sans perdre la silhouette commune des équipes SG.

## Pantalon de terrain

Le pantalon constitue la partie obligatoire du treillis dans le scénario [Équipe SG isolée](Stranded-SG-Team-Scenario). Il couvre les jambes sur la couche textile de base.

Trois couleurs sont disponibles :

- vert olive ;
- noir ;
- désert.

## Veste de terrain

La veste couvre le torse, les épaules et les bras. Elle est portée au-dessus du tee-shirt et reste facultative dans le scénario de départ : certains membres peuvent commencer uniquement en tee-shirt sous leur gilet tactique.

La veste existe dans les mêmes variantes olive, noire et désert que le pantalon. Chaque pawn reçoit une couleur de tenue aléatoire ; lorsqu’une veste est attribuée, elle reprend toujours la variante de son pantalon. Les membres d’une même équipe peuvent donc porter des couleurs différentes sans créer d’ensemble dépareillé sur un même pawn.

## Fabrication

Chaque pantalon ou veste demande :

```text
40 tissu
```

La fabrication est disponible aux établis de couture manuel et électrique après la recherche d'équipement de terrain SG, avec une compétence **Artisanat 3**.

## Ancien treillis combiné

Les anciens treillis réunissant veste et pantalon dans un seul objet restent définis pour préserver les sauvegardes et objets existants. Ils ne sont plus utilisés par le loadout du scénario Équipe SG isolée.

## Équipement complémentaire

La tenue modulaire est compatible avec :

- les [bottes tactiques SG](SG-Tactical-Boots) ;
- les [gants tactiques SG](SG-Tactical-Gloves) ;
- le [gilet tactique SG](SG-Tactical-Vest) ;
- les [couvre-chefs de terrain SG](SG-Team-Field-Helmet).
