# Planque Tok'ra visitable

> Statut : Prototype prudent
> Première version : 0.2.17-dev

## Présentation

Une piste de planque Tok'ra peut désormais révéler un vrai site temporaire sur
la carte du monde. Une caravane peut s'y rendre et entrer sur une petite carte
non hostile.

## Conditions

L'événement demande :

```text
au moins 1 piste de planque Tok'ra
confiance Tok'ra au moins neutre
aucun marqueur ou site de planque Tok'ra déjà actif
```

## Effet

En cas de réussite :

```text
-1 piste de planque Tok'ra
1 site temporaire visitable
2 doses de trétonine
4 médicaments industriels
```

La planque non visitée disparaît après environ `10` jours RimWorld.

## Visite

La caravane reçoit un ordre de visite, pas d'attaque. L'arrivée génère une
petite carte utilisant les mécanismes de site vanilla de RimWorld.

Le prototype ne crée pas :

- de défenseur hostile ;
- de marchand ;
- de personnage recrutable ;
- d'aide militaire ;
- de raid ;
- de colonie Tok'ra permanente.

La carte et le site sont temporaires et doivent disparaître proprement après le
départ de la caravane.
