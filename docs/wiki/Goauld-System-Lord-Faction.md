# Domaine d'un Grand Maître Goa'uld

> Statut : Prototype
> Version d'introduction : 0.1.61-dev

## Présentation

Le premier domaine d'un Grand Maître Goa'uld est une fondation technique hostile.
Il prépare l'arrivée future des Jaffa serviteurs, des groupes ennemis et des
événements Goa'uld.

```text
domaine d'un Grand Maître Goa'uld
```

## Fonctionnement actuel

La définition est volontairement limitée :

- hostilité permanente ;
- faction masquée ;
- aucune génération automatique au démarrage ;
- aucune colonie mondiale ;
- aucun raid ;
- aucun marchand ;
- aucun site de quête ;
- aucun groupe de pawns actif.

## Pourquoi la faction reste masquée

Les premiers Jaffa serviteurs Goa'uld n'existent pas encore. Activer des raids ou
des colonies avant leurs `PawnKindDef` introduirait une faction incomplète.

Le prochain jalon ajoutera :

```text
Jaffa serviteurs Goa'uld
    ↓
profils de groupes Combat
    ↓
premiers tests de génération contrôlée
```

## Suite prévue

Les marques faciales Jaffa, l'équipement, les colonies, les raids et les
événements de Grand Maître seront ajoutés progressivement.
