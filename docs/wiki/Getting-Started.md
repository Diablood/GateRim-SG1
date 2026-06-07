# Bien débuter

> Statut : Implémenté  
> Version d'introduction : 0.1.6-dev

## Configuration minimale actuelle

Active les éléments suivants dans l'ordre :

```text
Core
Biotech
GateRim SG-1
```

Le DLC **Biotech** est requis pour les xenotypes et les gènes personnalisés.

## Vérifier le contenu Jaffa

1. Lance une nouvelle partie temporaire.
2. Ouvre l'éditeur de xenotype lors de la création d'un pawn.
3. Sélectionne le xenotype `Jaffa`.
4. Vérifie la présence des gènes `physiologie jaffa` et `longévité jaffa`.
5. Vérifie que l'espérance de vie indiquée est de `150 %`.

## Vérifier le prototype d'hôte Goa'uld

1. Sélectionne le xenotype `hôte Goa'uld`.
2. Vérifie la présence des gènes `naquadah dans le sang` et `longévité de l'hôte Goa'uld`.
3. Vérifie que l'espérance de vie indiquée est de `500 %`.

## Vérifier le symbiote libre

1. Active le mode développeur.
2. Ouvre les actions de débogage.
3. Utilise l'action de génération d'un pawn.
4. Sélectionne `symbiote Goa'uld`.
5. Vérifie son apparence, ses déplacements et sa faible attaque de morsure.

## Vérifier l'implantation récente

1. Sélectionne un pawn humanoïde.
2. Utilise l'action de débogage permettant d'ajouter un état de santé.
3. Ajoute `implantation Goa'uld récente`.
4. Vérifie la présence du compte à rebours dans l'onglet de santé.
5. Vérifie l'augmentation temporaire de la douleur.
6. Attends une journée de jeu et confirme la disparition de l'état.

## Important

L'état d'implantation récente est encore appliqué manuellement. L'attaque sauvage, le rituel, l'interruption médicale et la conversion automatique restent prévus.
