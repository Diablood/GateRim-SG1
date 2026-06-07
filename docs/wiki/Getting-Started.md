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


## Vérifier le Prim'ta

1. Génère un nouveau pawn Jaffa.
2. Vérifie qu'il possède uniquement les gènes germinaux de la lignée jaffa.
3. Sélectionne ce pawn.
4. Utilise l'action de débogage permettant d'ajouter un état de santé.
5. Ajoute `symbiote du Prim'ta`.
6. Vérifie les bonus d'immunité, de guérison, de résistance, de longévité et la réduction de douleur.
7. Retire l'état et vérifie que ces bonus disparaissent.

## Remarque pour les anciennes sauvegardes de développement

Utilise un pawn nouvellement généré pour valider `0.1.13-dev`. Les anciens pawns de test peuvent conserver des gènes hérités des prototypes précédents.


## Tester l'implantation forcée

1. Active le mode développeur.
2. Génère un `symbiote Goa'uld`.
3. Place-le dans une case adjacente à un humanoïde adulte.
4. Sélectionne le symbiote libre.
5. Note l'identifiant affiché dans le panneau d'inspection.
6. Clique sur `Implantation forcée`.
7. Vérifie que le symbiote disparaît.
8. Vérifie que la victime reçoit `implantation Goa'uld récente`.
9. Vérifie que l'identifiant du parasite est conservé.


## Vérifier la conversion en hôte actif

1. Applique une implantation forcée sur un humanoïde adulte.
2. Note l'identifiant du symbiote affiché dans `implantation Goa'uld récente`.
3. Sauvegarde et recharge la partie pendant la phase critique.
4. Attends la fin du compte à rebours d'une journée.
5. Vérifie que l'état récent disparaît.
6. Vérifie que `symbiote Goa'uld adulte` apparaît.
7. Vérifie que l'identifiant est inchangé.
8. Vérifie les nouveaux bonus biologiques.
9. Vérifie que le xenotype germinal de la victime n'a pas changé.
