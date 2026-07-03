# Génétique

> Statut : Prototype  
> Version d'introduction : 0.1.12-dev  
> Séparation lignée / Prim'ta : 0.1.13-dev

## Deux catégories importantes

RimWorld distingue deux types de gènes :

| Type | Héritable à la naissance | Utilisation dans GateRim SG-1 |
|---|---|---|
| Gènes germinaux | Oui | Lignées stables comme les Jaffa |
| Xénogènes | Non | Modifications acquises au cours de la vie |

## Jaffa

Les Jaffa constituent une lignée humaine modifiée. Leur xenotype est donc héréditaire.

Depuis `0.1.13-dev`, les effets apportés par la larve ne sont plus placés directement dans la fondation génétique. Ils sont représentés par l'état de santé [Prim'ta](Primta).

Un enfant peut ainsi naître Jaffa sans naître avec une larve déjà implantée.

## Hôtes Goa'uld

Un hôte Goa'uld ne constitue pas une lignée héréditaire. Il s'agit d'un individu possédé au cours de sa vie par un symbiote adulte.

Les enfants d'un hôte Goa'uld ne doivent donc pas naître automatiquement possédés.

## Tests de reproduction à venir

Le comportement vanilla doit encore être observé pour les couples mixtes avant d'ajouter une règle spéciale de transmission maternelle.

## Traces biologiques de naquadah

Le gène acquis `naquadah dans le sang` sert depuis `0.3.58-dev` de marqueur
persistant commun aux hôtes Goa'uld, aux Tok'ra et aux Jaffa porteurs d'un
Prim'ta. Il n'est pas héréditaire et reste présent après extraction. Voir
[Traces biologiques persistantes de naquadah](Biological-Naquadah-Traces).
