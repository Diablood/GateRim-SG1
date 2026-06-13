# Reine Goa'uld

> Statut : Prototype
> Version d'introduction : 0.1.57-dev
> Acquisition naturelle rare : 0.2.10-dev

## Présentation

Les reines Goa'uld sont l'origine biologique des symbiotes immatures. Depuis
`0.2.10-dev`, une rare reine échappée peut rejoindre naturellement une colonie
du joueur. Le mode développeur reste disponible pour les tests isolés.

```text
reine Goa'uld
```

## Fonctionnement actuel

La reine reste volontairement limitée :

- elle n'effectue aucune implantation ;
- elle ne chasse aucun humanoïde ;
- elle apparaît uniquement par un incident rare après le jour `30` ;
- elle ne rejoint pas encore une faction Goa'uld ;
- son extraction exige qu'elle soit contrôlée par le joueur, sauf en mode
  développeur ;
- elle fournit `1` symbiote immature physique avec un délai d'un jour ;
- aucun nouvel incident n'est sélectionné tant qu'une reine du joueur est
  vivante sur une carte ou dans une caravane.

Sa texture est temporaire : elle réutilise visuellement le prototype de symbiote
adulte avec une taille d'affichage supérieure.

## Acquisition

L'incident [Arrivée d'une reine Goa'uld](Goauld-Queen-Arrival) a une chance de
base de `0,02`, devient admissible après le jour `30` et impose au moins `60`
jours entre deux sélections.

Le bassin d'incubation du Prim'ta reste l'infrastructure de maturation assistée.
Les infrastructures spécialisées et l'intégration aux factions seront étudiées
séparément.

Consulte [Maturation assistée des Prim'ta](Goauld-Queen-Assisted-Maturation).
