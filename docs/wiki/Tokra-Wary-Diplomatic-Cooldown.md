# Refroidissement diplomatique Tok'ra

> Statut : Prototype
> Version d'introduction : 0.1.55-dev

## Principe

Une relation Tok'ra méfiante possède désormais une conséquence diplomatique
légère mais réversible. Lorsqu'une réponse négative laisse la confiance sous
`0`, les nouvelles opportunités thérapeutiques sont temporairement suspendues.

```text
refus explicite = 3 jours RimWorld
expiration sans réponse = 5 jours RimWorld
```

L'expiration reste volontairement plus pénalisante qu'un refus respectueux.

## Persistance

Le délai restant est conservé après sauvegarde et rechargement. Une fois le
refroidissement terminé, les offres thérapeutiques peuvent à nouveau apparaître
avec le multiplicateur méfiant existant de `×0,50`.

## Portée

Le refroidissement bloque uniquement les nouvelles opportunités thérapeutiques
escortées. Les visites Tok'ra pacifiques ordinaires restent possibles. Les
livraisons médicales indépendantes sont déjà indisponibles sous le palier
coopérative.
