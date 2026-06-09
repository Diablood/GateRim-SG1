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


## Hébergement thérapeutique

Les hôtes Tok'ra actifs soignent désormais les affections biologiques curables
selon les règles de santé de RimWorld. L'asthme est pris en charge même lorsqu'il
affecte les deux poumons, et les blessures non permanentes se régénèrent
progressivement.

Les cicatrices permanentes, les membres manquants, les implants, les prothèses,
les addictions et les dépendances restent inchangés. Une éventuelle
régénération avancée devra être étudiée séparément.


## Implantation thérapeutique volontaire

Depuis `0.1.45-dev`, un symbiote Tok'ra libre dispose d'une
[action d'implantation thérapeutique](Tokra-Therapeutic-Implantation). Elle
cible un humanoïde malade compatible contrôlé par le joueur et demande une
confirmation explicite avant d'utiliser le flux d'implantation existant.


## Opportunité thérapeutique naturelle

Depuis `0.1.47-dev`, une
[opportunité thérapeutique Tok'ra](Tokra-Therapeutic-Opportunity) peut
apparaître rarement lorsqu'un pawn compatible souffre d'une affection
biologique curable non traumatique. Depuis `0.1.48-dev`, le symbiote Tok'ra
libre arrive avec une petite escorte de 1 à 2 hôtes volontaires. L'implantation
reste un choix manuel soumis à confirmation. Depuis `0.1.49-dev`, l'offre est
limitée à deux jours : le joueur peut l'accepter, la refuser explicitement ou
la laisser expirer. Dans les trois cas, l'escorte repart proprement.


## Confiance Tok'ra

Depuis `0.1.50-dev`, les issues des offres thérapeutiques alimentent une
[jauge persistante de confiance Tok'ra](Tokra-Trust). L'acceptation augmente la
confiance, le refus explicite l'abaisse légèrement et l'expiration sans réponse
la réduit davantage.

Depuis `0.1.51-dev`, les [paliers de confiance Tok'ra](Tokra-Trust-Thresholds)
modulent la durée des nouvelles offres thérapeutiques et la taille de leur
escorte pacifique.

La faction reste masquée : cette jauge constitue une fondation légère avant
l'introduction de quêtes et de relations diplomatiques plus complètes.


## Soutien médical en trétonine

Depuis `0.1.52-dev`, les équipes Tok'ra suffisamment confiantes apportent un
[cadeau léger de trétonine](Tokra-Medical-Support-Gifts) lors d'une opportunité
thérapeutique escortée. Le palier coopérative fournit `1` dose et le palier
fiable `2` doses. Les paliers méfiante et neutre n'apportent aucune ressource.


## Livraisons médicales indépendantes

Depuis `0.1.53-dev`, les relations Tok'ra coopératives ou fiables peuvent
déclencher rarement une [livraison médicale indépendante](Tokra-Medical-Support-Deliveries).
Cette équipe apporte de la trétonine sans exiger de pawn malade et sans proposer
de symbiose : `2` doses au palier coopérative, puis `4` doses au palier fiable.


## Pondérations storyteller par confiance

Depuis `0.1.54-dev`, les [pondérations storyteller Tok'ra](Tokra-Storyteller-Trust-Weights)
modulent la fréquence naturelle des opportunités thérapeutiques et des
livraisons médicales indépendantes. Les relations méfiantes réduisent les
offres, tandis que les relations fiables rendent les deux incidents légèrement
plus probables.


## Refroidissement diplomatique méfiant

Depuis `0.1.55-dev`, un [refroidissement diplomatique Tok'ra](Tokra-Wary-Diplomatic-Cooldown)
suspend temporairement les nouvelles opportunités thérapeutiques lorsque la
confiance reste sous `0` après une réponse négative : `3` jours après un refus
explicite et `5` jours après une expiration sans réponse. Les visites pacifiques
ordinaires restent possibles.
