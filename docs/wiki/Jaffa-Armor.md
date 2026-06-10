# Armures Jaffa modulaires

> Statut : Prototype
> Version d'introduction : 0.1.66-dev

## Présentation

Les premières armures Jaffa sont modulaires. Elles protègent les zones vitales,
mais également les mains, doigts, pieds et orteils qui restent vulnérables dans
RimWorld.

## Pièces disponibles

```text
armure Jaffa légère
armure Jaffa lourde
gantelets blindés Jaffa
bottes renforcées Jaffa
casque Jaffa déployé
```

## Protection localisée

| Pièce | Zones protégées |
|---|---|
| Armure légère | torse, cou, épaules |
| Armure lourde | torse, cou, épaules |
| Gantelets | bras, mains, doigts |
| Bottes | jambes, pieds, orteils |
| Casque déployé | tête complète et visage |

Les gantelets et bottes constituent de véritables équipements défensifs : ils
ne sont pas uniquement visuels.

## Casque Jaffa

Depuis `0.1.67-dev`, le casque dispose de trois modes persistants :

- automatique : rétracté hors enrôlement, déployé pendant l'enrôlement ;
- toujours déployé ;
- toujours rétracté.

La position modifie la couverture réelle :

| Position | Couverture |
|---|---|
| Rétracté | sommet de la tête |
| Déployé | tête complète et visage |

Les valeurs brutes d'armure restent identiques. La différence défensive
viendra uniquement des zones corporelles couvertes.

## Fabrication

Les cinq pièces sont fabriquées au banc d'usinage après la recherche
`Armurerie`. Les coûts et prérequis de Fabrication augmentent avec le niveau de
protection.

## Limites du prototype

Les textures sont temporaires. Les serviteurs Jaffa Goa'uld générés ne reçoivent
pas encore automatiquement ces armures. Les raids naturels Goa'uld restent
désactivés.
