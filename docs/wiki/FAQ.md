# FAQ

> Statut : Prototype  
> Version d'introduction : 0.1.6-dev

## Pourquoi Biotech est-il requis ?

Le prototype actuel utilise les xenotypes et les gènes personnalisés de Biotech.

## Pourquoi tous les Jaffa ne sont-ils pas massifs ?

Le mod sépare volontairement les avantages biologiques de l'apparence. Les Jaffa ne reçoivent pas automatiquement la silhouette vanilla `Body_Hulk`.


## Deux Jaffa peuvent-ils avoir un enfant Jaffa ?

Oui. Depuis `0.1.11-dev`, le xenotype Jaffa est héréditaire. Ses gènes actuels sont traités comme une fondation germinale afin que deux parents Jaffa ne produisent pas automatiquement un humain basique.

Certains bonus liés au symbiote immature seront séparés ultérieurement de cette fondation héréditaire.

## La dépendance des Jaffa au symbiote est-elle déjà active ?

Non. Elle est prévue pour un lot ultérieur avec la trétonine et les conséquences d'une absence de traitement.

## Les Goa'uld sont-ils déjà disponibles ?

Trois prototypes existent :

- un xenotype représentant un hôte déjà implanté ;
- un symbiote libre générable en mode développeur ;
- un état de santé temporaire représentant l'implantation récente.

## L'implantation se déclenche-t-elle automatiquement ?

Pas encore. Dans `0.1.11-dev`, l'état `implantation Goa'uld récente` doit être ajouté manuellement avec le mode développeur.

## La victime devient-elle automatiquement un hôte Goa'uld ?

Oui. Depuis `0.1.18-dev`, l'état d'implantation récente devient automatiquement un état d'[hôte Goa'uld actif](Active-Goauld-Host) après une journée de jeu.

## Le symbiote libre apparaît-il naturellement ?

Non. Il reste volontairement exclu des biomes et doit être généré en mode développeur pour les tests.

## Le wiki décrit-il du contenu non encore disponible ?

Oui, mais chaque page affiche explicitement un statut :

- `Implémenté`
- `Prototype`
- `Prévu`


## Un enfant Jaffa naît-il avec une larve ?

Non. Depuis `0.1.13-dev`, la lignée jaffa et le [Prim'ta](Primta) sont séparés. Un enfant peut naître Jaffa sans porter automatiquement un symbiote immature.

## Le Prim'ta est-il déjà automatique ?

Non. Dans `0.1.13-dev`, l'état `symbiote du Prim'ta` doit encore être ajouté ou retiré manuellement en mode développeur.


## L'implantation forcée est-elle déjà disponible ?

Oui, sous forme de prototype manuel depuis `0.1.17-dev`. Place un symbiote libre à côté d'un humanoïde adulte compatible, sélectionne le symbiote puis clique sur `Implantation forcée`.

Le comportement hostile autonome sera ajouté ultérieurement.


## Peut-on interrompre une implantation récente ?

Oui. Depuis `0.1.19-dev`, sélectionne la victime pendant la phase critique puis
clique sur `Extraction d'urgence`.

Le parasite réapparaît à proximité avec le même identifiant persistant. Cette
commande reste un prototype manuel ; une véritable chirurgie sera ajoutée plus
tard.


## Existe-t-il une véritable chirurgie d'extraction ?

Oui. Depuis `0.1.20-dev`, planifie `extraction d'urgence Goa'uld` dans l'onglet
de santé de la victime récemment implantée.

L'opération demande un médecin compétent, du temps et un médicament. Elle peut
échouer. La commande instantanée reste temporairement présente comme outil de
test.


## Le symbiote libre attaque-t-il désormais automatiquement ?

Oui. Depuis `0.1.21-dev`, il recherche un humanoïde adulte compatible accessible,
le poursuit puis commence son implantation au contact.

Après une extraction, un bref délai de sécurité empêche une réimplantation
immédiate.


## Existe-t-il une implantation rituelle contrôlée ?

Oui. Depuis `0.1.22-dev`, sélectionne un symbiote libre puis clique sur
`Implantation rituelle`.

Clique ensuite sur l'humanoïde compatible accessible à implanter dans un rayon
de `12` cases. Une cérémonie plus complète sera ajoutée ultérieurement.
