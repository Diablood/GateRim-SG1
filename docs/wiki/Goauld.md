# Goa'uld

> Statut : Prototype  
> Première fondation : 0.1.7-dev

## Présentation

Les Goa'uld sont des symbiotes parasites capables de prendre le contrôle d'un
hôte humanoïde. Contrairement à un simple xenotype, le système final devra
traiter le Goa'uld comme un organisme distinct pouvant entrer dans un hôte,
le quitter et éventuellement être transféré.

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

Depuis `0.1.11-dev`, un état de santé temporaire peut être ajouté manuellement
à un pawn humanoïde :

```text
implantation Goa'uld récente
```

Cet état représente la phase critique pendant laquelle le parasite s'attache
au système nerveux de sa victime. Il dure provisoirement une journée de jeu,
affiche un compte à rebours et augmente la douleur.

### Implantation forcée interactive

Depuis `0.1.17-dev`, un symbiote libre adjacent à un humanoïde adulte compatible
peut déclencher manuellement :

```text
Implantation forcée
```

Le symbiote disparaît et son identité persistante est transférée dans l'état
`implantation Goa'uld récente`.

Consulte [Implantation forcée Goa'uld](Forced-Implantation) pour le mode d'emploi.

### Hôte actif après conversion

Depuis `0.1.18-dev`, la phase critique se transforme automatiquement après une
journée de jeu en :

```text
symbiote Goa'uld adulte
```

Le même identifiant persistant est conservé. L'état actif apporte des bonus
importants sans remplacer le xenotype germinal d'origine.

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

### Implantation rituelle

Depuis `0.1.22-dev`, le symbiote libre peut déclencher une voie contrôlée :

```text
Implantation rituelle
```

La commande ouvre un curseur de ciblage : le joueur choisit directement un
humanoïde compatible accessible dans un rayon limité. La même identité
persistante est conservée.

Consulte [Implantation rituelle Goa'uld](Ritual-Implantation).

### Durée du rituel

Depuis `0.1.24-dev`, la cible rituelle n'est plus implantée immédiatement. Une
cérémonie temporisée doit s'achever tandis que la cible reste valide, accessible
et dans le rayon autorisé.

Le rituel peut être annulé et sa progression survit aux sauvegardes.

### Bassin rituel

Depuis `0.1.25-dev`, la cérémonie contrôlée exige un
[bassin rituel Goa'uld](Ritual-Basin). Le symbiote et la cible doivent rester
proches de cette structure jusqu'à la fin.

### Reine Goa'uld

Depuis `0.1.57-dev`, une reine Goa'uld distincte peut être générée en mode
développeur :

```text
reine Goa'uld
```

Ce premier prototype est volontairement passif. Il prépare une future origine
biologique contrôlée pour les symbiotes immatures sans modifier encore la
boucle jouable d'incubation du Prim'ta.

Consulte [Reine Goa'uld](Goauld-Queen).

### Domaine d'un Grand Maître Goa'uld

Depuis `0.1.61-dev`, une première fondation hostile de faction existe :

```text
domaine d'un Grand Maître Goa'uld
```

Cette définition reste masquée et non générée automatiquement. Elle n'ajoute
encore aucune colonie, aucun raid et aucun marchand.

Depuis `0.1.62-dev`, deux premiers serviteurs Jaffa Goa'uld peuvent être
générés manuellement pour les tests développeur :

```text
guerrier Jaffa au service des Goa'uld
garde Jaffa au service des Goa'uld
```

Les deux pawns utilisent la lignée héréditaire Jaffa existante. Un profil
technique de groupe `Combat` est également rattaché au domaine prototype,
sans activer encore les raids naturels.

Depuis `0.1.63-dev`, chaque nouveau guerrier ou garde Jaffa généré reçoit
automatiquement un Prim'ta initial. L'attribution n'est effectuée qu'une
seule fois : retirer ensuite la larve ne crée pas de remplacement artificiel.

Consulte [Domaine d'un Grand Maître Goa'uld](Goauld-System-Lord-Faction).

### Bâton Ma'Tok

Depuis `0.1.64-dev`, le premier équipement Jaffa jouable est disponible :

```text
bâton Ma'Tok
```

Cette arme associe une décharge énergétique lente mais puissante à une
hampe utilisable au corps à corps. Elle peut être fabriquée au banc
d'usinage après la recherche Armurerie et utilise encore des visuels
temporaires dédiés.

Depuis `0.1.65-dev`, les guerriers et gardes Jaffa Goa'uld générés reçoivent
automatiquement cette arme grâce au système vanilla de loadout.

Consulte [Bâton Ma'Tok](Matok-Staff).

## Ce qui n'est pas encore implémenté

- reproduction autonome de la reine ;
- disponibilité naturelle des symbiotes immatures ;
- transfert entre plusieurs hôtes ;
- sarcophage ;
- armures et marques visuelles des serviteurs Jaffa ;
- colonies, raids et événements Goa'uld.

## Différence avec les sanguophages

Un sanguophage transmet un xenogerm. Un Goa'uld devra se déplacer réellement
entre plusieurs corps. Le futur système évitera donc toute duplication
automatique du symbiote.
