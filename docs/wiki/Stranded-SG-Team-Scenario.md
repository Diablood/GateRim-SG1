# Scénario : Équipe SG isolée

> Statut : Prototype jouable  
> Première version : 0.2.0-dev

## Présentation

Le premier scénario jouable sans Porte fonctionnelle est disponible depuis
`0.2.0-dev` :

```text
Équipe SG isolée
```

Une mission de reconnaissance a mal tourné. La Porte des étoiles locale est
devenue inutilisable avant l'extraction et tout contact avec le SGC est
impossible. L'équipe doit établir un camp avec le matériel récupéré sur place.

## Équipe de départ

Le scénario génère exactement quatre membres adultes d'équipe SG. Les quatre candidats affichés sont les quatre membres de départ : il n'ajoute pas de sélection élargie inutile.

Les candidats incapables de violence sont exclus afin que l'équipe de départ reste crédible pour une mission militaire. Des profils scientifiques restent possibles tant que leur parcours ne leur interdit pas totalement le combat.

Chaque soldat porte automatiquement :

- un [treillis d'équipe SG](SG-Team-Uniform) vert olive ;
- des [bottes tactiques SG](SG-Tactical-Boots) ;
- des [gants tactiques SG](SG-Tactical-Gloves) ;
- un [gilet tactique SG](SG-Tactical-Vest).

## Faction joueur

Le scénario utilise une identité dédiée :

```text
expédition du SGC
```

Elle remplace la faction vanilla **Nouveaux arrivants** pour ce départ de
partie.

## Introduction narrative

Le texte d'introduction est affiché dans l'éditeur de scénario et doit
également s'ouvrir automatiquement lorsque la carte démarre.

## Armes temporaires

Les armes Tau'ri propres au mod ne sont pas encore disponibles. Le matériel
initial utilise donc temporairement des armes vanilla :

```text
3 fusils d'assaut
1 fusil à pompe
```

Les armes sont laissées dans les fournitures de départ afin que le joueur
choisisse leur répartition.

## Matériel de bivouac

```text
4 sacs de couchage en tissu
```

Aucun établi préconstruit n'est fourni.

## Casques de terrain facultatifs

```text
4 casques de terrain SG
```

Les casques arrivent dans les fournitures de départ. Ils ne sont pas équipés
automatiquement : le joueur choisit quand les utiliser.

Consulte [Casque de terrain SG](SG-Team-Field-Helmet).

## Caisses de ravitaillement récupérées

```text
30 repas de survie
20 médicaments industriels
300 acier
150 bois
20 composants industriels
120 tissu
80 cuir ordinaire
```

## Limites actuelles

Cette première étape rend le départ de partie jouable, mais n'active pas encore
les factions mondiales et les rencontres naturelles Stargate.

Les prochains jalons `0.2.x` introduiront progressivement :

- la présence mondiale Goa'uld ;
- les premiers raids naturels prudents ;
- les Jaffa libres ;
- la présence Tok'ra ;
- l'acquisition naturelle des armes et ressources Stargate.

La Porte des étoiles fonctionnelle appartiendra à la future phase `0.3.x`.
