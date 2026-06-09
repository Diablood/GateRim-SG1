# Opportunité thérapeutique Tok'ra

> Statut : Prototype
> Version d'introduction : 0.1.47-dev
> Escorte légère : 0.1.48-dev

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
chance storyteller : faible
```

## Limites actuelles

L'escorte est volontairement légère. Les quêtes, conséquences diplomatiques,
refus mémorisés et décisions limitées dans le temps seront étudiés dans des
jalons séparés.
