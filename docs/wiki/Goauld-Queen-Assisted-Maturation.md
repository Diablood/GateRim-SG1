# Maturation assistée des Prim'ta

> Statut : Prototype
> Version d'introduction : 0.1.58-dev

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

## Prototype actuel

La reine reste volontairement réservée aux tests :

- elle doit être générée avec le mode développeur ;
- son bouton d'extraction est visible uniquement en mode développeur ;
- une extraction fournit `1` symbiote immature physique ;
- le délai prototype entre deux extractions est de `1` jour RimWorld ;
- le délai persiste après sauvegarde et rechargement.

## Ressource immature

Le symbiote immature de Prim'ta est une ressource biologique physique :

- stockable dans les produits biologiques Goa'uld ;
- empilable ;
- fragile ;
- détruite après pourrissement complet ;
- consommée par la maturation au bassin.

## Limites

La reine ne se reproduit pas encore automatiquement et n'apparaît pas
naturellement. La disponibilité normale de cette ressource sera définie dans un
jalon ultérieur.


## Conservation

Les symbiotes immatures et les larves matures peuvent être stockés dans un
[bassin de conservation du Prim'ta](Primta-Preservation-Basin) alimenté. Le
stockage spécialisé suspend l'aggravation sans restaurer une ressource déjà
détériorée.
