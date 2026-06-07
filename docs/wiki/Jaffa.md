# Jaffa

> Statut : Prototype  
> Version d'introduction : 0.1.1-dev  
> Séparation de la lignée et du Prim'ta : 0.1.13-dev

## Présentation

Les Jaffa sont une lignée humaine modifiée pour subir le Prim'ta et porter un symbiote Goa'uld immature.

## Fondation héréditaire

Un enfant peut naître Jaffa sans porter automatiquement une larve.

Le xenotype contient actuellement :

| Gène germinal | Rôle |
|---|---|
| `lignée jaffa` | Marqueur de lignée |
| `prédisposition à la poche jaffa` | Compatibilité biologique avec le futur Prim'ta |
| `compatibilité avec un symbiote immature` | Compatibilité avec le soutien biologique de la larve |
| `physiologie jaffa` | Capacité de transport augmentée de `+15` |

Les Jaffa ne sont pas tous visiblement massifs. Le xenotype n'impose donc pas la silhouette vanilla `Body_Hulk`.

## Prim'ta

Le Prim'ta est maintenant représenté séparément par un état de santé persistant :

```text
symbiote du Prim'ta
```

Lorsqu'il est présent, il accorde provisoirement :

| Effet | Valeur |
|---|---:|
| Immunité | `×1,5` |
| Guérison des blessures | `×1,5` |
| Dégâts reçus | `×0,9` |
| Espérance de vie | `×1,5` |
| Douleur | `×0,85` |

L'état doit encore être ajouté ou retiré manuellement avec le mode développeur.

## Évolutions prévues

- cérémonie du Prim'ta ;
- contrôle de l'âge ;
- implantation automatique ;
- dépendance au symbiote ;
- trétonine ;
- conséquences médicales après retrait ;
- sensibilité aux Goa'uld proches ;
- marques faciales distinctives pour les Jaffa servant une faction Goa'uld.
