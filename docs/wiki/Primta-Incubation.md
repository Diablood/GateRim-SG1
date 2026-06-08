# Bassin d'incubation du Prim'ta

> Statut : Prototype  
> Version d'introduction : 0.1.28-dev

## Présentation

Le bassin d'incubation du Prim'ta est la première source jouable de larves sans
passer par le mode développeur.

## Construction

| Élément | Valeur |
|---|---:|
| Catégorie | Production |
| Taille | `2 × 1` |
| Acier | `80` |
| Or | `8` |
| Travail de construction | `1600` |

Le visuel actuel est temporaire.

## Production

Ouvre l'onglet des tâches du bassin puis ajoute :

```text
incuber une larve de Prim'ta
```

Un colon ayant le travail **Dressage** actif et un niveau **Animaux 4+** apporte `10` unités de viande crue, effectue `1800` unités de travail et produit :

```text
1 larve de Prim'ta
```

## Boucle actuelle

```text
bassin d'incubation du Prim'ta
    ↓ tâche d'incubation
larve de Prim'ta physique
    ↓ stockage ou transport
implantation médicale chez un Jaffa
```

## Nutriments requis

Depuis `0.1.29-dev`, chaque incubation consomme :

```text
10 unités de viande crue
```

Le prototype accepte les différentes viandes crues et permet de mélanger
plusieurs piles.

## Limites du prototype

Les futurs lots ajouteront :

- nutriments biologiques ;
- conservation ;
- durée et contraintes de maturation ;
- approvisionnement des factions Goa'uld ;
- équilibrage de rareté.


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
