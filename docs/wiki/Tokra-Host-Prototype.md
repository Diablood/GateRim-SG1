# Prototype d'hôte Tok'ra volontaire

> Statut : Prototype  
> Version d'introduction : 0.1.40-dev

## Présentation

Un premier pawn hôte Tok'ra peut désormais être généré en mode développeur.

```text
hôte Tok'ra volontaire
    ↓
colon humain contrôlé par le joueur
    ↓
symbiote Tok'ra adulte actif
```

## Tester le pawn

Fais apparaître en mode développeur :

```text
hôte Tok'ra volontaire
```

Attends au maximum `60` ticks.

Ouvre ensuite son onglet Santé et vérifie la présence de :

```text
symbiote adulte de lignée Goa'uld
```

Les informations persistantes doivent indiquer :

```text
origine : Tok'ra
```

## Identité persistante

Chaque pawn reçoit une identité Tok'ra distincte.

```text
première apparition du pawn
    ↓
un symbiote Tok'ra créé

retrait ultérieur du symbiote
    ↓
aucun remplacement artificiel
```

Le registre d'initialisation est sauvegardé afin d'éviter qu'un même pawn ne
devienne une source infinie de symbiotes.

## Limites actuelles

Cette première version utilise un colon humain vanilla contrôlé par le joueur.

Les éléments suivants viendront plus tard :

```text
hôtes appartenant réellement à la faction Tok'ra
colonies
visiteurs
marchands
quêtes
équipements
apparence spécifique
personnages nommés
```
