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

### Symbiote libre

Depuis `0.1.8-dev`, un symbiote adulte sans hôte peut être généré en mode développeur :

```text
symbiote Goa'uld
```

### Implantation récente

Depuis `0.1.11-dev`, un état de santé temporaire peut être ajouté manuellement à un pawn humanoïde :

```text
implantation Goa'uld récente
```

Cet état représente la phase critique pendant laquelle le parasite s'attache au système nerveux de sa victime. Il dure provisoirement une journée de jeu, affiche un compte à rebours et augmente la douleur.

## Ce qui n'est pas encore implémenté

- application automatique après une attaque du symbiote libre ;
- implantation rituelle ;
- interruption médicale ;
- prise de contrôle dynamique ;
- extraction ;
- transfert entre plusieurs hôtes ;
- sarcophage ;
- faction des Grands Maîtres Goa'uld.

## Différence avec les sanguophages

Un sanguophage transmet un xenogerm. Un Goa'uld devra se déplacer réellement entre plusieurs corps. Le futur système évitera donc toute duplication automatique du symbiote.
