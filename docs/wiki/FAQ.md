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

Pas encore. Depuis `0.1.26-dev`, un Jaffa compatible peut recevoir l'opération `implanter un Prim'ta jaffa` depuis son onglet de santé. La cérémonie d'âge automatique et la larve physique seront ajoutées ultérieurement.


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


## Le rituel Goa'uld est-il instantané ?

Non. Depuis `0.1.24-dev`, l'implantation rituelle demande une cérémonie de
`600` ticks. La cible doit rester compatible, accessible et dans le rayon de
`12` cases.

Le rituel peut être annulé manuellement et reprend après un rechargement de
sauvegarde.

## Ideology deviendra-t-il obligatoire ?

Non. Le rituel de base reste disponible avec `Core + Biotech`. Une intégration
avec `Ideology` est prévue comme extension optionnelle.


## Faut-il construire une structure pour le rituel ?

Oui. Depuis `0.1.25-dev`, un `bassin rituel Goa'uld` est requis. Le symbiote et
sa cible doivent rester à moins de `6` cases du même bassin pendant la cérémonie.

Le bassin fonctionne sans `Ideology`.


## Une larve de Prim'ta est-elle déjà une ressource physique ?

Oui. Depuis `0.1.27-dev`, la [larve de Prim'ta](Primta-Larva) est un objet transportable consommé par l'opération `implanter un Prim'ta jaffa`.

Depuis `0.1.28-dev`, construis un [bassin d'incubation du Prim'ta](Primta-Incubation) pour produire des larves sans utiliser le mode développeur.


## Comment produire une larve de Prim'ta ?

Depuis `0.1.28-dev`, construis un
[bassin d'incubation du Prim'ta](Primta-Incubation), ouvre son onglet des tâches
puis ajoute `incuber une larve de Prim'ta`.

Depuis `0.1.29-dev`, la recette consomme `10` unités de viande crue. La
conservation et la température seront ajoutées ultérieurement.


## Quelle compétence est utilisée pour incuber une larve ?

L'incubation relève du travail `Dressage` et exige un niveau `Animaux 4+`.

Le colon doit avoir le travail `Dressage` actif. Il peut ensuite effectuer la
tâche automatiquement ou la prioriser manuellement sur le bassin.


## Quels nutriments sont nécessaires pour incuber une larve ?

Depuis `0.1.29-dev`, chaque incubation consomme `10` unités de viande crue.

La première version accepte toutes les viandes crues compatibles avec la
catégorie vanilla correspondante.


## Les larves de Prim'ta se conservent-elles indéfiniment ?

Non. Depuis `0.1.30-dev`, elles sont périssables. Elles utilisent le système
vanilla de pourrissement et sont détruites si elles pourrissent complètement.

Un stockage froid est recommandé.


## Où stocker les larves de Prim'ta ?

Depuis `0.1.31-dev`, elles sont classées dans :

```text
ressources brutes
    ↓
produits biologiques Goa'uld
```

Elles ne sont plus rangées avec les produits manufacturés et ne sont pas
considérées comme des aliments crus.


## Quelle température convient aux larves de Prim'ta ?

Une température comprise entre `0 °C` et `10 °C` est recommandée.

La détérioration est normale au-dessus de `10 °C`, doublée à partir de `25 °C`
et triplée à partir de `40 °C`.

La congélation interrompt provisoirement la détérioration. Les éventuelles
pénalités liées au gel profond seront étudiées ultérieurement.


## À partir de quel âge un Jaffa peut-il recevoir un Prim'ta ?

Depuis `0.1.33-dev`, l'opération devient disponible à partir de `10` ans
biologiques.

Ce seuil représente une première approximation jouable de l'âge de Prata.
La dépendance progressive liée à la puberté sera ajoutée ultérieurement.


## Que se passe-t-il si un Jaffa atteint la puberté sans Prim'ta ?

Depuis `0.1.34-dev`, un Jaffa compatible âgé de `12` ans ou plus sans Prim'ta
développe une déficience immunitaire progressive.

Une implantation réussie retire immédiatement ce malus. La trétonine sera
ajoutée ultérieurement comme traitement de substitution.


## L'absence de Prim'ta affecte-t-elle aussi l'humeur ?

Oui, légèrement. Depuis `0.1.35-dev`, un Jaffa compatible âgé de `10` ans ou
plus sans Prim'ta reçoit la pensée `attend son Prim'ta` avec un effet de `-1`.

La première implantation accorde `a reçu son Prim'ta`, un bonus de `+3` pendant
`5` jours. Ce bonus n'est accordé qu'une seule fois par pawn.


## La trétonine est-elle disponible ?

Oui, sous forme de prototype depuis `0.1.36-dev`.

Une `dose de trétonine` peut être administrée à un Jaffa pubère sans Prim'ta.
Elle suspend sa déficience pendant `1` jour.

Depuis `0.1.37-dev`, produis `5` doses dans un laboratoire de drogues vanilla
avec une larve de Prim'ta et un médicament.


## Combien de doses produit une larve de Prim'ta ?

La recette prototype de `0.1.37-dev` consomme une larve de Prim'ta et un
médicament pour produire `5` doses de trétonine.

Ce rendement pourra être ajusté pendant l'équilibrage.


## Peut-on implanter un Prim'ta sans chirurgie ?

Oui. Depuis `0.1.38-dev`, utilise un bassin rituel Goa'uld pour lancer une
`Cérémonie formelle du Prim'ta`.

Le Jaffa et une larve physique doivent rester proches du bassin pendant `600`
ticks. La chirurgie médicale reste également disponible.


## Les Tok'ra sont-ils déjà disponibles ?

Partiellement. Depuis `0.1.39-dev`, un `symbiote Tok'ra` peut être généré en
mode développeur afin de tester l'implantation volontaire et la persistance de
son identité après extraction.

La faction mondiale, les colonies, les visiteurs, les marchands et les quêtes
Tok'ra seront ajoutés ultérieurement.


## Peut-on générer directement un hôte Tok'ra ?

Oui, à des fins de test. Depuis `0.1.40-dev`, fais apparaître
`hôte Tok'ra volontaire` en mode développeur.

Ce colon humain contrôlé par le joueur reçoit automatiquement un symbiote Tok'ra
adulte actif avec une identité persistante. Les véritables visiteurs et colonies
Tok'ra viendront ultérieurement.


## Les groupes Tok'ra apparaissent-ils automatiquement ?

Pas encore.

Depuis `0.1.41-dev`, les premiers profils internes `Combat` et `Peaceful`
existent dans la faction Tok'ra masquée, mais la génération mondiale reste
désactivée. Les visiteurs, marchands, colonies et événements Tok'ra seront
activés progressivement.


## Peut-on recevoir une visite Tok'ra ?

Oui, à des fins de test depuis `0.1.42-dev`.

Active le mode développeur puis lance :

```text
Do incident
    ↓
visiteurs Tok'ra pacifiques (test)
```

Une petite équipe non hostile apparaîtra puis repartira automatiquement. Les
visites aléatoires du storyteller ne sont pas encore activées.
