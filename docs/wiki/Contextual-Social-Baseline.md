# Réactions sociales contextuelles

> Statut : Première base jouable  
> Première version : 0.2.5-dev

## Présentation

GateRim SG-1 ajoute une première couche de réactions sociales propres aux
peuples et aux factions.

Ces effets restent volontairement mesurés. Ils enrichissent les relations sans
forcer des attaques automatiques, des haines absolues ou des interdictions
sociales rigides.

## Jaffa libres face aux Goa'uld

Un Jaffa libre reconnaissant un hôte Goa'uld actif reçoit :

```text
se méfie d'un Goa'uld
-30 opinion
```

Cette réaction représente le souvenir de la domination exercée par les Grands
Maîtres.

## Tok'ra face aux Goa'uld

Un hôte Tok'ra actif reconnaissant un hôte Goa'uld actif reçoit :

```text
voit un ennemi Goa'uld
-40 opinion
```

La réaction est plus forte, car les Tok'ra combattent directement la
domination Goa'uld depuis des générations.

## Jaffa libres face aux marques Goa'uld

Un Jaffa libre observant un Jaffa qui porte encore une marque frontale de
domaine reçoit :

```text
se méfie d'un Jaffa marqué par les Goa'uld
-8 opinion
```

Le malus reste faible. Une marque peut représenter un ancien service, une
contrainte, une cicatrice conservée volontairement ou une infiltration.

## Serviteurs sous le regard d'un Grand Maître

Un Jaffa au service des Goa'uld situé à moins de `12` cases du Grand Maître de
son propre domaine reçoit :

```text
sous le regard d'un Grand Maître
+2 humeur
```

Cet effet ne représente pas un bonheur sincère. Il traduit une discipline
imposée, la peur et l'habitude de rester parfaitement composé en présence du
maître.

## Limites actuelles

Cette première base n'ajoute pas encore :

- d'attaques sociales automatiques ;
- de relations permanentes imposées ;
- de réactions détaillées aux prisonniers ;
- de souvenir positif lié à une implantation Tok'ra volontaire ;
- d'intégration Ideology.

Ces éléments pourront être ajoutés progressivement après observation en jeu.
