# Bassin d'incubation du Prim'ta

> Statut : Prototype  
> Version d'introduction : 0.1.28-dev

## Présentation

![Bassin d'incubation du Prim'ta](images/SG1_PrimtaIncubationBasin.png)

Le bassin d'incubation du Prim'ta transforme un symbiote immature issu d'une
reine Goa'uld en larve transportable destinée à un Jaffa.

Son visuel biologique vert occupe une case. L'image reste fixe lorsque le
bâtiment est orienté, tandis que sa cellule d'interaction suit la rotation.

## Construction

| Élément | Valeur |
|---|---:|
| Catégorie | Production |
| Taille | `1 × 1` |
| Acier | `80` |
| Or | `8` |
| Travail de construction | `1600` |

## Production

Ouvre l'onglet des tâches du bassin puis ajoute :

```text
incuber une larve de Prim'ta
```

Un colon ayant le travail **Dressage** actif et un niveau **Animaux 4+** apporte
`1` symbiote immature de Prim'ta et `20` unités de viande crue, effectue `1800`
unités de travail et produit :

```text
1 larve de Prim'ta
```

## Boucle actuelle

```text
reine Goa'uld contrôlée
    ↓ extraction
symbiote immature de Prim'ta
    + 20 unités de viande crue
    ↓ bassin d'incubation du Prim'ta
    ↓ tâche d'incubation
larve de Prim'ta physique
    ↓ stockage ou transport
implantation médicale chez un Jaffa
```

## Nutriments requis

Chaque maturation consomme :

```text
1 symbiote immature de Prim'ta
20 unités de viande crue
```

Le prototype accepte les différentes viandes crues et permet de mélanger
plusieurs piles.

## Limites du prototype

Les futurs lots ajouteront une infrastructure reproductive spécialisée et une
intégration plus directe aux factions Goa'uld.

## Travail requis

| Élément | Valeur |
|---|---:|
| Type de travail | Dressage |
| Compétence | Animaux |
| Niveau minimal | `4` |

Le bassin est associé à un donneur de travail dédié. Il peut donc être traité
automatiquement ou priorisé manuellement par un colon éligible.

## Après production

Depuis `0.1.30-dev`, les larves produites doivent être conservées correctement.
Elles se détériorent dans de mauvaises conditions et sont détruites si elles
pourrissent complètement.

## Température après production

Après incubation, consulte [Température des larves](Primta-Temperature).

Une chambre froide limite la détérioration. Les fortes chaleurs l'accélèrent.
