# Goa'uld

> Statut : Prototype  
> Version d'introduction : 0.1.7-dev

## Présentation

Les Goa'uld sont des symbiotes parasites capables de prendre le contrôle d'un hôte humanoïde. Contrairement à un simple xenotype, le système final devra traiter le Goa'uld comme un organisme distinct pouvant entrer dans un hôte, le quitter et éventuellement être transféré.

## Prototype actuel

La première version ajoute un xenotype représentant un humanoïde **déjà implanté** :

```text
hôte Goa'uld
```

Ce prototype permet de tester les principaux effets biologiques avant de développer la possession dynamique.

| Particularité | Effet actuel |
|---|---|
| Naquadah dans le sang | Marqueur visible destiné aux futurs systèmes |
| Longévité de l'hôte | Espérance de vie multipliée par `5`, soit `500 %` |
| Immunité | Résistance accrue aux maladies |
| Récupération | Guérison accélérée |
| Résistance physique | Meilleure tolérance au combat |
| Force | Dégâts de mêlée améliorés |

## Pourquoi 500 % ?

Cette valeur est provisoire. Elle représente la longévité fortement accrue d'un hôte standard sans considérer chaque Goa'uld comme immortel. Les sarcophages et les Grands Maîtres nommés seront équilibrés séparément.

## Ce qui n'est pas encore implémenté

- symbiote autonome sur la carte ;
- attaque sauvage ;
- implantation rituelle ;
- prise de contrôle dynamique ;
- extraction ;
- transfert entre plusieurs hôtes ;
- sarcophage ;
- faction des Grands Maîtres Goa'uld.

## Différence avec les sanguophages

Un sanguophage transmet un xenogerm. Un Goa'uld devra se déplacer réellement entre plusieurs corps. Le futur système évitera donc toute duplication automatique du symbiote.
