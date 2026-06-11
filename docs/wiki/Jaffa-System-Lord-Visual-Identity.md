# Identité visuelle des Jaffa Goa'uld

> Statut : Prototype
> Version d'introduction : 0.1.73-dev

## Présentation

Les Jaffa reçoivent automatiquement une marque frontale noire générique.

```text
marque frontale Jaffa générique
```

## Nature de la marque

La marque est intrinsèque au pawn et rendue comme un tatouage :

```text
aucune pièce d'inventaire
aucune recette
aucune catégorie de stockage
aucune protection
aucune statistique d'armure
aucun butin retirable
```

Elle reste distincte du casque rétractable et de l'ensemble modulaire Jaffa.

## Hiérarchie visuelle prévue

Le visuel noir actuel correspond aux Jaffa ordinaires.

```text
Jaffa ordinaires      -> marque noire
élites sélectionnées  -> marque argentée
Premier Primat        -> marque dorée embossée
```

Un garde lourd n'est pas automatiquement un Premier Primat.

## Objectif du prototype

Le rendu actuel est temporaire. Il valide l'emplacement intrinsèque avant
l'introduction de variantes propres aux futurs domaines Goa'uld.

## Limites

Les raids naturels, colonies et marchands Goa'uld restent désactivés.

## Calibration temporaire du positionnement

Le premier rendu intrinsèque validait correctement la présence de la marque,
mais l'insigne temporaire flottait au-dessus du front. Une calibration
provisoire abaisse le dessin et son offset de rendu. Le visuel définitif restera
à retravailler lors de l'introduction des marques propres aux Grands Maîtres.

## Calibration latérale

Après validation en jeu, les vues sud et nord sont jugées correctes. Le
correctif suivant recentre uniquement les vues est et ouest vers l'avant du
front, sans changer l'architecture du rendu intrinsèque.
