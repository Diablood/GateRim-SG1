# Congélation profonde des Prim'ta

> Statut : Implémenté
> Version d'introduction : 0.1.60-dev

## Présentation

Les larves de Prim'ta et les symbiotes immatures issus d'une reine restent des
ressources biologiques vivantes. Un congélateur classique peut encore les
protéger temporairement, mais un froid extrême et prolongé n'est plus une
solution parfaite.

## Seuils

```text
au-dessus de -15 °C
    ↓
aucun dommage de congélation profonde

à partir de -15 °C
    ↓
exposition à la congélation profonde
    ↓
1 jour RimWorld toléré
    ↓
détérioration biologique ×0,25

à partir de -30 °C
    ↓
détérioration biologique ×0,50
```

## Récupération

Lorsque la ressource retourne dans un environnement plus sûr, l'exposition
accumulée diminue progressivement à vitesse `×2`.

## Protection spécialisée

![Bassin de conservation du Prim'ta](images/SG1_PrimtaPreservationBasin.png)

Un [bassin de conservation du Prim'ta](Primta-Preservation-Basin) alimenté
protège complètement la ressource et résorbe également l'exposition accumulée.

## Conséquences pratiques

- une chambre froide classique entre `0 °C` et `10 °C` reste utile ;
- un congélateur modéré reste une solution de secours ;
- le froid très bas ne doit pas devenir un stockage permanent sans surveillance ;
- le bassin spécialisé reste la meilleure solution à long terme.
