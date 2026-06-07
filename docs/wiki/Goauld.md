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

### Implantation forcée interactive

Depuis `0.1.17-dev`, un symbiote libre adjacent à un humanoïde adulte compatible peut déclencher manuellement :

```text
Implantation forcée
```

Le symbiote disparaît et son identité persistante est transférée dans l'état `implantation Goa'uld récente`.

Consulte [Implantation forcée Goa'uld](Forced-Implantation) pour le mode d'emploi.

### Hôte actif après conversion

Depuis `0.1.18-dev`, la phase critique se transforme automatiquement après une journée de jeu en :

```text
symbiote Goa'uld adulte
```

Le même identifiant persistant est conservé. L'état actif apporte des bonus importants sans remplacer le xenotype germinal d'origine.

Consulte [Hôte Goa'uld actif](Active-Goauld-Host).

### Extraction d'urgence

Depuis `0.1.19-dev`, une victime récemment implantée peut interrompre le
processus grâce à la commande manuelle :

```text
Extraction d'urgence
```

Le même parasite réapparaît sous la forme d'un symbiote libre à proximité.

Consulte [Extraction d'urgence Goa'uld](Emergency-Extraction).

### Chirurgie d'extraction

Depuis `0.1.20-dev`, une victime récemment implantée peut recevoir une véritable
opération médicale :

```text
extraction d'urgence Goa'uld
```

La réussite libère le même parasite avec son identifiant persistant. L'échec
laisse l'implantation récente active.

Consulte [Chirurgie d'extraction Goa'uld](Extraction-Surgery).

### Chasse autonome

Depuis `0.1.21-dev`, le symbiote libre recherche un humanoïde compatible proche,
le poursuit et déclenche automatiquement son implantation au contact.

Après une extraction, un bref délai de sécurité évite une réimplantation
immédiate.

Consulte [Chasse autonome des symbiotes libres](Autonomous-Hunt).

## Ce qui n'est pas encore implémenté

- IA hostile autonome du symbiote libre ;
- implantation rituelle ;
- interruption médicale ;
- extraction ;
- transfert entre plusieurs hôtes ;
- sarcophage ;
- faction des Grands Maîtres Goa'uld.

## Différence avec les sanguophages

Un sanguophage transmet un xenogerm. Un Goa'uld devra se déplacer réellement entre plusieurs corps. Le futur système évitera donc toute duplication automatique du symbiote.
