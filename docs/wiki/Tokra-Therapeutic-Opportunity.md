# Opportunité thérapeutique Tok'ra

> Statut : Prototype
> Version d'introduction : 0.1.47-dev

## Principe

Une opportunité thérapeutique Tok'ra peut désormais apparaître rarement à
partir du jour `30` lorsqu'un pawn humanoïde compatible contrôlé par le joueur
souffre d'une affection biologique curable non traumatique.

```text
pawn malade compatible
    ↓
incident naturel rare
    ↓
arrivée d'un symbiote Tok'ra libre au bord de la carte
    ↓
lettre ciblée
    ↓
choix manuel du joueur
```

La lettre indique le pawn concerné et les affections détectées. Le joueur doit
ensuite sélectionner le symbiote et utiliser la commande d'implantation
thérapeutique déjà existante. La fenêtre de consentement reste obligatoire.

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

Le prototype fait apparaître un symbiote Tok'ra libre sans escorte. Les envoyés
Tok'ra, quêtes, conséquences diplomatiques et décisions limitées dans le temps
seront étudiés dans des jalons séparés.
