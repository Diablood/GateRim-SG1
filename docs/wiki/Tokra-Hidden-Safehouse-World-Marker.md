# Marqueur de planque Tok'ra

> Statut : Prototype prudent
> Première version : 0.2.16-dev

## Présentation

Une piste de planque Tok'ra peut désormais mener à un marqueur temporaire sur
la carte du monde.

Ce marqueur représente des coordonnées incomplètes. Il ne s'agit pas encore
d'un vrai site visitable.

## Conditions

L'événement demande :

```text
au moins 1 piste de planque Tok'ra
confiance Tok'ra au moins neutre
aucun marqueur de planque Tok'ra déjà actif
```

## Effet

En cas de réussite :

```text
-1 piste de planque Tok'ra
1 marqueur temporaire sur la carte du monde
```

## Durée

Le marqueur dure environ :

```text
5 jours RimWorld
```

## Limites

Le marqueur ne crée pas encore :

- de carte secondaire ;
- de site visitable ;
- de butin ;
- de marchand ;
- de recrutement ;
- d'aide militaire ;
- de raid ;
- de colonie permanente.

Il sert de première étape avant une vraie planque Tok'ra visitable.

Depuis `0.2.17-dev`, la [planque Tok'ra visitable](Tokra-Hidden-Safehouse-Site)
constitue l'étape suivante. Un marqueur actif empêche sa création, et une
planque active empêche la création d'un nouveau marqueur.
