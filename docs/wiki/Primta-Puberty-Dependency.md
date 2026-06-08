# Dépendance pubertaire au Prim'ta

> Statut : Prototype  
> Version d'introduction : 0.1.34-dev

## Présentation

Un jeune Jaffa peut recevoir un Prim'ta à partir de `10` ans biologiques.

À partir de `12` ans biologiques, l'absence de Prim'ta commence désormais à
provoquer une déficience progressive.

## Fenêtre actuelle

```text
moins de 10 ans
    ↓
implantation impossible

10 à 11 ans
    ↓
implantation disponible sans urgence médicale

12 ans et plus sans Prim'ta
    ↓
déficience immunitaire progressive
```

## Progression

| Gravité | Immunité | Guérison |
|---|---:|---:|
| Précoce | `×0,85` | normale |
| Modérée | `×0,65` | `×0,9` |
| Avancée | `×0,4` | `×0,7` |
| Critique | `×0,15` | `×0,45` |

La gravité augmente progressivement sur plusieurs jours.

## Soulagement

Une implantation médicale réussie retire immédiatement la déficience.

## Limites actuelles

Cette première version ne gère pas encore :

```text
trétonine
progression en caravane
mort directe
pensées culturelles
cérémonie formelle
```


## Pensées d'humeur séparées

Depuis `0.1.35-dev`, les [pensées culturelles](Primta-Cultural-Thoughts) sont
gérées séparément de cette déficience médicale.
