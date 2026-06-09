# Opportunité thérapeutique Tok'ra

> Statut : Prototype
> Version d'introduction : 0.1.47-dev
> Escorte légère : 0.1.48-dev
> Offre temporaire : 0.1.49-dev
> Fondation de confiance : 0.1.50-dev
> Cadeaux de soutien médical : 0.1.52-dev
> Pondération storyteller par confiance : 0.1.54-dev

## Principe

Une opportunité thérapeutique Tok'ra peut apparaître rarement à partir du jour
`30` lorsqu'un pawn humanoïde compatible contrôlé par le joueur souffre d'une
affection biologique curable non traumatique.

```text
pawn malade compatible
    ↓
incident naturel rare
    ↓
arrivée d'un symbiote Tok'ra libre avec une petite escorte
    ↓
lettre ciblée
    ↓
choix manuel du joueur
```

La lettre indique le pawn concerné et les affections détectées. Le joueur doit
ensuite sélectionner le symbiote et utiliser la commande d'implantation
thérapeutique déjà existante. La fenêtre de consentement reste obligatoire.

## Escorte légère

Depuis `0.1.48-dev`, le symbiote n'arrive plus seul. Il est accompagné de :

```text
1 à 2 hôtes Tok'ra volontaires
```

Ces envoyés appartiennent à la faction Tok'ra masquée et réutilisent le
comportement vanilla de visite pacifique de la colonie. Ils donnent un contexte
plus diplomatique à la proposition sans ajouter encore une quête complète.

## Conditions de déclenchement

La cible doit être :

```text
humanoïde compatible
contrôlée par le joueur
âgée d'au moins 13 ans biologiques
sans implantation récente ni symbiote adulte actif
atteinte d'au moins une affection biologique curable non traumatique
```

Les blessures récentes continuent à se régénérer chez un hôte Tok'ra actif,
mais elles ne déclenchent pas cet incident à elles seules. Une simple coupure
ne suffit donc pas à provoquer une offre rare.

## Fréquence initiale

```text
première apparition possible : jour 30
délai minimal entre deux offres : 60 jours
chance XML de base : 0,035
pondération par confiance : ×0,50 à ×1,50
```

## Offre temporaire

Depuis `0.1.49-dev`, l'offre reste disponible pendant deux jours RimWorld. Le
symbiote libre affiche le temps restant dans son panneau d'inspection et propose
une commande dédiée permettant de refuser explicitement l'offre.

```text
acceptation
    ↓
implantation puis départ de l'escorte

refus ou expiration
    ↓
disparition du symbiote libre puis départ de l'escorte
```

Le délai restant persiste après sauvegarde et rechargement.

## Confiance Tok'ra

Depuis `0.1.50-dev`, l'issue de chaque offre modifie une jauge persistante de
confiance Tok'ra :

```text
acceptation = +5
refus explicite = -1
expiration sans réponse = -2
```

Le symbiote proposé affiche cette confiance sous la durée restante. Cette jauge
prépare les futures quêtes et relations avec la résistance Tok'ra sans activer
encore une diplomatie complète sur la carte du monde.

## Pondération storyteller

Depuis `0.1.54-dev`, la fréquence naturelle des offres dépend du palier de
confiance : `×0,50` en méfiante, `×1,00` en neutre, `×1,25` en coopérative
et `×1,50` en fiable. Les conditions médicales et le délai minimal de `60`
jours restent inchangés.

## Limites actuelles

L'escorte et la confiance restent volontairement légères. Les quêtes et
relations vanilla seront étudiées dans des jalons séparés.

## Paliers de confiance

Depuis `0.1.51-dev`, la durée d'une nouvelle offre et la taille de son escorte
dépendent des [paliers de confiance Tok'ra](Tokra-Trust-Thresholds). Une offre
déjà en cours conserve les paramètres déterminés lors de son apparition.


## Soutien médical en trétonine

Depuis `0.1.52-dev`, une offre créée au palier coopérative apporte `1` dose
physique de trétonine. Au palier fiable, l'équipe apporte `2` doses. Les
ressources apparaissent près du point d'arrivée et restent sur la carte quelle
que soit la réponse apportée à l'offre.
