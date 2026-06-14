# Planque Tok'ra visitable

> Statut : Prototype prudent
> Première version : 0.2.17-dev
> Contact pacifique : 0.2.18-dev
> Tenue Tok'ra dédiée : 0.2.19-dev
> Interaction de contact : 0.2.20-dev

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
1 contact Tok'ra pacifique
2 doses de trétonine
4 médicaments industriels
```

La planque non visitée disparaît après environ `10` jours RimWorld.

## Visite

La caravane reçoit un ordre de visite, pas d'attaque. L'arrivée génère une
petite carte utilisant les mécanismes de site vanilla de RimWorld.

Depuis `0.2.18-dev`, un seul hôte Tok'ra volontaire demeure sur place. Il est
âgé d'au moins `20` ans, possède une histoire adulte et appartient à la faction
Tok'ra persistante. Il n'a aucun rôle marchand et n'est pas recrutable.

Depuis `0.2.19-dev`, ce contact porte directement la [tenue de terrain Tok'ra](Tokra-Field-Clothing-Set), ce qui évite le défaut du contact généré nu.

Depuis `0.2.20-dev`, le contact peut aussi fournir un [bref compte rendu de planque](Tokra-Safehouse-Contact). Cette interaction donne seulement un message narratif et un très léger gain de confiance Tok'ra.

Le prototype ne crée pas :

- de défenseur hostile ;
- de marchand ;
- de personnage recrutable ou marchand ;
- d'aide militaire ;
- de raid ;
- de colonie Tok'ra permanente.

La carte et le site sont temporaires et doivent disparaître proprement après le
départ de la caravane.
