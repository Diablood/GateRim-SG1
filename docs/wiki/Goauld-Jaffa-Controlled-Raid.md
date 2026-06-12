# Raid Jaffa Goa'uld contrôlé

> Statut : Prototype
> Version d'introduction : 0.1.69-dev

## Présentation

Un incident réservé aux outils développeur permet de faire arriver un
véritable groupe hostile de Jaffa au service d'un Grand Maître Goa'uld.

```text
raid de test contrôlé de Jaffa Goa'uld
```

## Déclenchement

Active le mode développeur puis utilise :

```text
Do incident (points)
└── raid de test contrôlé de Jaffa Goa'uld
```

Une valeur modérée comme `500` points convient pour un premier essai.

## Particularité

La chance storyteller de cet incident est fixée à :

```text
0
```

Ce prototype reste manuel. Depuis `0.2.1-dev`, un incident séparé active
néanmoins de rares [raids naturels de Jaffa Goa'uld](Goauld-Jaffa-Natural-Raid)
sans modifier cette voie de test contrôlée.

Depuis `0.1.70-dev`, l'incident sélectionne explicitement :

```text
ImmediateAttack
```

Le groupe conserve son assaut direct, mais RimWorld n'a plus besoin de
retomber sur une stratégie par défaut.

## Objectifs du test

Le parcours contrôlé permet de vérifier :

- la réutilisation de la faction mondiale Goa'uld hostile réelle ;
- la génération vanilla du profil de groupe `Combat` ;
- le Prim'ta initial des Jaffa ;
- les bâtons Ma'Tok équipés ;
- les armures modulaires automatiques ;
- le comportement automatique des casques rétractables ;
- l'absence du bouton de changement de casque sur les ennemis.

## Limites

Les colonies mondiales et l'assaut naturel rare sont actifs depuis
`0.2.1-dev`. Les marchands et les doctrines naturelles avancées restent
désactivés. Les visuels d'armure sont encore temporaires.

## Séparation des doctrines

Depuis `0.1.71-dev`, ce raid d'assaut contrôlé désactive explicitement le vol
et l'enlèvement. Il sert de référence pour tester un combat prolongé.

La doctrine d'enlèvement possède son propre incident développeur :

[Raid d'enlèvement Jaffa Goa'uld contrôlé](Goauld-Jaffa-Controlled-Abduction-Raid).

## Doctrine de destruction séparée

Depuis `0.1.72-dev`, un troisième incident conserve un assaut militaire
prolongé puis autorise seulement après cette phase la récupération
opportuniste de captifs et d'objets de valeur :

[Raid de destruction Jaffa Goa'uld contrôlé](Goauld-Jaffa-Controlled-Destruction-Raid).
