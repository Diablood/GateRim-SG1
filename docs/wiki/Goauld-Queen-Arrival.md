# Arrivée d'une reine Goa'uld

> Statut : Prototype jouable  
> Version d'introduction : 0.2.10-dev

## Présentation

Une reine Goa'uld séparée de ses gardiens peut rarement atteindre la colonie et
accepter le contrôle des dresseurs du joueur.

```text
jour minimal : 30
chance de base : 0,02
délai minimal : 60 jours
limite : aucune autre reine vivante du joueur
```

La reine entre directement sur la carte depuis un bord accessible. L'incident
est refusé lorsqu'une reine du joueur est déjà présente sur une carte ou dans
une caravane.

## Extraction

Une reine contrôlée par le joueur expose la commande :

```text
Extraire un symbiote immature
```

Chaque extraction produit un symbiote immature physique et impose un délai
persistant d'un jour RimWorld. Le mode développeur permet toujours de tester la
commande sur une reine qui n'appartient pas au joueur.

## Maturation

Le symbiote immature doit encore être transformé au bassin d'incubation avec
`10` unités de viande crue. Cette production locale reste verrouillée par la
recherche **Biotechnologies Goa'uld**.

Consulte [Reine Goa'uld](Goauld-Queen) et
[Maturation assistée des Prim'ta](Goauld-Queen-Assisted-Maturation).
