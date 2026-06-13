# Cache d'une cellule Tok'ra

> Statut : Première base jouable  
> Première version : 0.2.12-dev

## Présentation

Une cellule Tok'ra clandestine peut désormais établir un bref contact et laisser un
petit cache médical près de la colonie.

Cet événement donne une première présence concrète aux cellules Tok'ra sans en
faire une faction territoriale classique.

## Fonctionnement

L'événement utilise la faction Tok'ra masquée persistante déjà présente dans la
partie.

Il ne crée pas :

- de colonie Tok'ra ;
- de site mondial ;
- de caravane marchande ;
- de recrutement ;
- d'aide militaire ;
- de raid.

## Conditions

Le cache peut apparaître si la confiance Tok'ra est au moins neutre.

Il ne se déclenche pas lorsque les Tok'ra sont méfiants.

## Contenu

Le cache contient seulement des fournitures médicales modestes :

```text
confiance neutre : 1 trétonine, 2 médicaments
confiance coopérative : 2 trétonines, 2 médicaments
confiance fiable : 2 trétonines, 3 médicaments
```

## Limites

Ce jalon ne remplace pas les livraisons médicales Tok'ra plus importantes, qui
restent liées à un meilleur niveau de confiance.

Les véritables cellules visitables, sites cachés et quêtes Tok'ra restent
prévus pour plus tard.
