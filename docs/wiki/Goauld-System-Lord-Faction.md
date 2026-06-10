# Domaine d'un Grand Maître Goa'uld

> Statut : Prototype
> Version d'introduction : 0.1.61-dev
> Extension Jaffa serviteurs : 0.1.62-dev
> Attribution initiale du Prim'ta : 0.1.63-dev

## Présentation

Le premier domaine d'un Grand Maître Goa'uld est une fondation technique
hostile. Il prépare l'arrivée progressive des groupes ennemis, raids, colonies
et événements Goa'uld.

```text
domaine d'un Grand Maître Goa'uld
```

## Fonctionnement actuel

La définition reste volontairement limitée :

- hostilité permanente ;
- faction masquée ;
- aucune génération automatique au démarrage ;
- aucune colonie mondiale ;
- aucun raid naturel ;
- aucun marchand ;
- aucun site de quête.

Depuis `0.1.62-dev`, deux premiers `PawnKindDef` de serviteurs Jaffa sont
disponibles pour les tests développeur :

```text
guerrier Jaffa au service des Goa'uld
garde Jaffa au service des Goa'uld
```

Les deux variantes forcent la lignée Jaffa héréditaire existante. Le domaine
possède également un profil technique `Combat` composé majoritairement de
guerriers et plus rarement de gardes.

Depuis `0.1.63-dev`, chacun de ces serviteurs reçoit automatiquement un
Prim'ta initial lorsqu'il est généré. Cette attribution n'est effectuée
qu'une fois par pawn : une larve retirée ultérieurement ne réapparaît pas.

## Pourquoi la faction reste masquée

Le profil de groupe prépare les futurs contenus hostiles, mais il n'est pas
encore relié à une génération naturelle. Les pawns peuvent être générés
manuellement pour vérifier leur xenotype.

L'équipement, les marques faciales et l'équilibrage doivent encore être
ajoutés avant l'activation des raids et des colonies.

## Suite prévue

```text
équipement et identité visuelle
    ↓
tests contrôlés de groupes ennemis
    ↓
activation progressive des raids et de la présence mondiale
```
