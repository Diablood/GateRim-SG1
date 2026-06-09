# Hébergement thérapeutique Tok'ra

> Statut : Prototype
> Version d'introduction : 0.1.44-dev

## Principe

Un symbiote Tok'ra actif peut désormais soigner automatiquement certaines
pathologies graves de son hôte.

Cette première version reste volontairement limitée afin de valider la
mécanique sans déséquilibrer les soins classiques.

## Pathologies prises en charge

Le prototype couvre une courte liste configurée :

```text
carcinome
infection
peste
paludisme
grippe
maladie du sommeil
pourriture du sang
```

Une pathologie absente de la partie chargée est simplement ignorée.

## Ce qui ne change pas

Le prototype ne retire pas automatiquement :

```text
blessures
cicatrices
maladies non listées
```

Les hôtes Goa'uld ne bénéficient pas de cette guérison Tok'ra.

## Implantation volontaire thérapeutique

Depuis `0.1.45-dev`, un symbiote Tok'ra libre peut proposer une
[implantation thérapeutique volontaire](Tokra-Therapeutic-Implantation) à un
humanoïde malade compatible contrôlé par le joueur. Une fenêtre de
confirmation apparaît avant le transfert.

## Évolutions prévues

```text
règles configurables pour les maladies moddées
événements et quêtes de recrutement
intégration avec les visiteurs et la diplomatie Tok'ra
```
