# Maturation assistée des Prim'ta

> Statut : Prototype
> Version d'introduction : 0.1.58-dev
> Acquisition naturelle : 0.2.10-dev

## Présentation

Le bassin d'incubation du Prim'ta ne crée plus une larve entièrement à partir de
viande crue. Il fait désormais mûrir un symbiote immature issu d'une reine
Goa'uld.

```text
reine Goa'uld
    ↓
symbiote immature de Prim'ta
    +
10 unités de viande crue
    ↓
bassin d'incubation du Prim'ta
    ↓
larve de Prim'ta transportable
```

## Fonctionnement actuel

Une reine peut désormais être obtenue par un incident rare :

- elle arrive directement sous le contrôle du joueur ;
- son bouton d'extraction reste visible sans mode développeur ;
- une extraction fournit `1` symbiote immature physique ;
- le délai entre deux extractions est de `1` jour RimWorld ;
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

Les symbiotes immatures et les larves matures peuvent être stockés dans un
[bassin de conservation du Prim'ta](Primta-Preservation-Basin) alimenté. Le
stockage spécialisé suspend l'aggravation sans restaurer une ressource déjà
détériorée.
