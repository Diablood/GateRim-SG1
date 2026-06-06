# Goa'uld

> Statut : Prototype  
> Première fondation : 0.1.7-dev

## Présentation

Les Goa'uld sont des symbiotes parasites capables de prendre le contrôle d'un hôte humanoïde. Contrairement à un simple xenotype, le système final devra traiter le Goa'uld comme un organisme distinct pouvant entrer dans un hôte, le quitter et éventuellement être transféré.

## Prototypes actuels

### Hôte déjà implanté

Le xenotype suivant permet de tester les effets biologiques d'un hôte déjà possédé :

```text
hôte Goa'uld
```

| Particularité | Effet actuel |
|---|---|
| Naquadah dans le sang | Marqueur visible destiné aux futurs systèmes |
| Longévité de l'hôte | Espérance de vie multipliée par `5`, soit `500 %` |
| Immunité | Résistance accrue aux maladies |
| Récupération | Guérison accélérée |
| Résistance physique | Meilleure tolérance au combat |
| Force | Dégâts de mêlée améliorés |

### Symbiote libre

Depuis `0.1.8-dev`, un symbiote adulte sans hôte peut être généré en mode développeur :

```text
symbiote Goa'uld
```

Il s'agit actuellement d'une petite créature vulnérable disposant uniquement d'une faible morsure. Sa véritable menace viendra ultérieurement de sa capacité à s'implanter dans une cible humanoïde.

## Ce qui n'est pas encore implémenté

- attaque sauvage d'implantation ;
- implantation rituelle ;
- période critique après infestation ;
- prise de contrôle dynamique ;
- extraction ;
- transfert entre plusieurs hôtes ;
- sarcophage ;
- faction des Grands Maîtres Goa'uld.

## Différence avec les sanguophages

Un sanguophage transmet un xenogerm. Un Goa'uld devra se déplacer réellement entre plusieurs corps. Le futur système évitera donc toute duplication automatique du symbiote.
