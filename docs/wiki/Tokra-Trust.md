# Confiance Tok'ra

> Statut : Prototype
> Version d'introduction : 0.1.50-dev

## Principe

Les choix effectués pendant une opportunité thérapeutique modifient désormais
une jauge persistante de confiance Tok'ra.

```text
minimum : -100
valeur initiale : 0
maximum : 100
```

## Effets actuels

```text
accepter une offre thérapeutique : +5
refuser explicitement une offre : -1
laisser expirer une offre sans réponse : -2
```

Le refus explicite entraîne donc une conséquence légère, tandis que l'absence
de réponse pénalise davantage la relation. L'acceptation constitue le premier
moyen de construire progressivement une relation de confiance avec les Tok'ra.

## Affichage

Pendant une offre active, sélectionne le symbiote Tok'ra libre. Son panneau
d'inspection affiche :

```text
Offre thérapeutique Tok'ra : ... jour(s) restant(s)
Confiance Tok'ra : ...
```

La valeur persiste après sauvegarde et rechargement.

## Limites actuelles

La faction Tok'ra reste masquée et n'utilise pas encore la diplomatie vanilla.
La confiance ne débloque pas encore de récompense, de quête ou de visite
spéciale. Elle servira de base à ces systèmes dans de futurs jalons.
