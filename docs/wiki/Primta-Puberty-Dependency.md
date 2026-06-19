# Dépendance pubertaire au Prim'ta

> Statut : prototype jouable
> Première version : `0.1.34-dev`

## Présentation

Un jeune Jaffa peut recevoir un Prim'ta à partir de `10` ans biologiques.

À partir de `12` ans, l'absence de Prim'ta provoque une déficience progressive
qui réduit surtout l'immunité et la guérison.

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

Deux solutions existent :

- une implantation réussie retire immédiatement la déficience ;
- une [dose de trétonine](Tretonin) suspend temporairement sa progression.

Les [pensées culturelles](Primta-Cultural-Thoughts) et la
[cérémonie formelle](Primta-Formal-Ceremony) complètent cette mécanique sans
modifier directement la gravité médicale.

## Limite actuelle

La déficience n'entraîne pas une mort automatique à un seuil fixe. Elle rend le
Jaffa de plus en plus vulnérable aux maladies et blessures.
