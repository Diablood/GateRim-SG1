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
Confiance Tok'ra : ... (...)
```

La valeur et son palier persistent après sauvegarde et rechargement.

Depuis `0.1.51-dev`, consulte aussi les
[paliers de confiance Tok'ra](Tokra-Trust-Thresholds). Ils modulent la durée des
offres thérapeutiques et la taille de leur escorte pacifique.

## Limites actuelles

La faction Tok'ra reste masquée et n'utilise pas encore la diplomatie vanilla.
Les paliers ne débloquent pas encore de récompense, de quête ou de visite
spéciale et ne modifient pas encore la fréquence storyteller. Ils serviront de
base à ces systèmes dans de futurs jalons.
