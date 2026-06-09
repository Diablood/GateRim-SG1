# Tok'ra

> Statut : Prototype  
> Version d'introduction : 0.1.39-dev

## Présentation

Les Tok'ra constituent une première branche distincte des Goa'uld hostiles.

Cette première version permet de tester leur principe essentiel :

```text
symbiote Tok'ra libre
    ↓
hôte volontaire
    ↓
symbiose persistante
```

## Limite de la faction actuelle

La définition technique de faction Tok'ra existe déjà, mais elle reste masquée
et n'est pas générée automatiquement sur la carte du monde.

Les éléments suivants viendront plus tard :

```text
colonies Tok'ra
marchands
quêtes
diplomatie
groupes de pawns
personnages nommés
```

## Tester un symbiote Tok'ra

Fais apparaître en mode développeur :

```text
symbiote Tok'ra
```

Sélectionne-le.

Il doit proposer uniquement :

```text
Implantation Tok'ra volontaire
```

Il ne doit pas proposer :

```text
implantation forcée
implantation rituelle Goa'uld
chasse autonome
```

## Implantation volontaire

La cible doit être :

```text
humanoïde compatible
contrôlé par le joueur
âgé d'au moins 13 ans biologiques
dans un rayon de 12 cases
accessible
sans symbiote adulte existant
```

Après implantation, l'identité Tok'ra reste persistante.

## Extraction

L'extraction chirurgicale existante reste disponible pendant l'implantation
récente.

Après extraction, le même symbiote doit réapparaître comme :

```text
symbiote Tok'ra
```

avec :

```text
origine Tok'ra
chasse autonome désactivée
```

## Évolutions prévues

```text
faction générée
hôtes Tok'ra volontaires via événements
colonies et diplomatie
approvisionnement en trétonine
reine Goa'uld ou Tok'ra
origine biologique des larves
```


## Prototype d'hôte volontaire

Depuis `0.1.40-dev`, fais apparaître un
[prototype d'hôte Tok'ra volontaire](Tokra-Host-Prototype) en mode développeur.

Ce colon humain contrôlé par le joueur reçoit automatiquement une identité
Tok'ra persistante après son apparition.


## Fondation des groupes de pawns

Depuis `0.1.41-dev`, une
[fondation technique des groupes Tok'ra](Tokra-Pawn-Groups) existe.

La faction masquée contient désormais des profils internes `Combat` et
`Peaceful`, mais la génération mondiale automatique reste désactivée.


## Visiteurs pacifiques

Depuis `0.1.42-dev`, une
[visite Tok'ra pacifique](Tokra-Peaceful-Visitors) peut être déclenchée
manuellement en mode développeur.

Depuis `0.1.43-dev`, le storyteller peut également sélectionner rarement cette
visite à partir du jour `15`. Un délai minimal de `30` jours évite les visites
trop rapprochées.
