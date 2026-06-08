# Température des larves de Prim'ta

> Statut : Prototype  
> Version d'introduction : 0.1.32-dev

## Présentation

Les larves de Prim'ta sont des ressources biologiques vivantes. Leur
conservation dépend maintenant plus clairement de la température.

## Tableau thermique

| Température | Condition | Vitesse effective |
|---|---|---:|
| Sous `0 °C` | Gelée | `×0` |
| `0 °C` à `10 °C` | Réfrigérée, stockage recommandé | `×0` à `×1` |
| Plus de `10 °C` à moins de `25 °C` | Température élevée | `×1` |
| `25 °C` à moins de `40 °C` | Chaude | `×2` |
| `40 °C` et plus | Chaleur critique | `×3` |

## Inspection

Sélectionne une larve pour afficher :

```text
température de la larve
condition thermique
vitesse effective de détérioration
```

## Limite actuelle

La congélation n'endommage pas encore la larve. Elle interrompt sa
détérioration afin de garder cette première version simple.

Une future évolution pourra ajouter des pénalités de congélation profonde ou
des conteneurs Goa'uld spécialisés.
