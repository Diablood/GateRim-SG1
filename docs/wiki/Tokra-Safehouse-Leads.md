# Pistes de planque Tok'ra

> Statut : système actif
> Première version : `0.2.14-dev`

## Présentation

Les signaux et certains contacts Tok'ra peuvent fournir des pistes persistantes.
Elles représentent des coordonnées, codes ou informations incomplètes sur le
réseau clandestin.

Le registre conserve au maximum :

```text
3 pistes
```

## Utilisations actuelles

Une piste peut être consommée pour :

1. localiser un [cache médical](Tokra-Safehouse-Lead-Cache) sur la carte active ;
2. créer un [marqueur de planque](Tokra-Hidden-Safehouse-World-Marker)
   temporaire sur la carte du monde ;
3. révéler une [planque Tok'ra visitable](Tokra-Hidden-Safehouse-Site).

Une planque visitable est temporaire, non hostile, contient un petit cache et
accueille un contact Tok'ra pacifique non marchand.

Un marqueur et une planque visitable ne peuvent pas être actifs en même temps.

## Piste de suivi par contact

Depuis `0.2.24-dev`, un contact coopératif ou fiable peut transmettre une piste
supplémentaire si le registre n'est pas plein.

Les contacts méfiants ou neutres restent limités au briefing médical.

## Limites

Les pistes ne créent pas directement de colonie Tok'ra, de marchand, de
recrutement, d'aide militaire ou de raid.
