# Pistes de planque Tok'ra

> Statut : Fondation technique  
> Première version : 0.2.14-dev

## Présentation

Les signaux de planque Tok'ra peuvent désormais conserver des pistes
persistantes.

Une piste ne crée pas encore de site mondial. Elle représente seulement des
coordonnées ou informations incomplètes qui pourront servir plus tard à ouvrir
une vraie planque Tok'ra.

## Gain

Un signal de planque réussi donne maintenant :

```text
+1 confiance Tok'ra
+1 piste de planque Tok'ra
```

## Limite actuelle

Le registre est limité à :

```text
3 pistes
```

Cette limite évite d'accumuler trop d'opportunités avant l'arrivée du vrai
système de sites cachés.

## Ce que cela ne crée pas

Les pistes ne créent pas encore :

- de site mondial ;
- de colonie ;
- de caravane ;
- d'objet ;
- de visiteur ;
- de marchand ;
- de recrutement ;
- d'aide militaire ;
- de raid.

Depuis `0.2.15-dev`, une piste peut déjà être exploitée pour retrouver un petit [cache médical](Tokra-Safehouse-Lead-Cache). Elles prépareront ensuite un futur jalon de planque Tok'ra visitable.


## Marqueur mondial

Depuis `0.2.16-dev`, une piste peut aussi être consommée pour créer un
[marqueur de planque Tok'ra](Tokra-Hidden-Safehouse-World-Marker) temporaire sur
la carte du monde.

Ce marqueur ne peut pas encore être visité.
