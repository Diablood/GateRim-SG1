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


## Vérifier l'extraction d'urgence

1. Applique une implantation forcée à un humanoïde adulte.
2. Note l'identifiant du symbiote.
3. Sélectionne la victime avant la fin du compte à rebours.
4. Clique sur `Extraction d'urgence`.
5. Vérifie que l'état récent disparaît.
6. Vérifie qu'un symbiote libre réapparaît à proximité.
7. Vérifie que son identifiant est inchangé.
8. Implante-le à nouveau pour contrôler le transfert inverse.


## Vérifier la chirurgie d'extraction

1. Applique une implantation forcée à un humanoïde adulte contrôlable.
2. Ouvre son onglet de santé.
3. Planifie `extraction d'urgence Goa'uld`.
4. Fournis un médecin de niveau `6+`, un lit et un médicament.
5. Laisse l'opération s'achever.
6. En cas de réussite, vérifie que le même parasite réapparaît à proximité.
7. Vérifie que son identifiant persistant est inchangé.
8. En cas d'échec, vérifie que l'implantation récente reste active.


## Vérifier la chasse autonome

1. Génère un `symbiote Goa'uld` à plusieurs cases d'un humanoïde adulte.
2. Vérifie que `Chasse autonome` est active dans son panneau d'inspection.
3. Attends que le symbiote choisisse une cible.
4. Vérifie qu'il se déplace vers l'humanoïde.
5. Vérifie l'implantation automatique au contact.
6. Extrais le parasite chirurgicalement.
7. Vérifie qu'un délai de sécurité empêche une réimplantation immédiate.


## Vérifier l'implantation rituelle

1. Génère un `symbiote Goa'uld`.
2. Désactive `Chasse autonome`.
3. Place un humanoïde compatible à moins de `12` cases sans le coller au parasite.
4. Sélectionne le symbiote libre.
5. Note son identifiant.
6. Clique sur `Implantation rituelle`.
7. Utilise le curseur pour cliquer sur le pawn précis à implanter.
8. Vérifie que le pawn sélectionné reçoit `implantation Goa'uld récente`.
9. Vérifie que l'identifiant est inchangé.


## Vérifier la durée du rituel

1. Génère un `symbiote Goa'uld`.
2. Désactive `Chasse autonome`.
3. Clique sur `Implantation rituelle`.
4. Sélectionne une cible compatible dans un rayon de `12` cases.
5. Vérifie que l'implantation n'est pas immédiate.
6. Vérifie que la cible et les ticks restants apparaissent dans le panneau d'inspection.
7. Sauvegarde et recharge pendant le rituel.
8. Vérifie que le compte à rebours reprend.
9. Teste `Annuler le rituel`.
10. Déplace aussi la cible hors de portée pour vérifier l'annulation automatique.


## Construire et tester le bassin rituel

1. Construis ou génère un `bassin rituel Goa'uld`.
2. Place un symbiote libre à moins de `6` cases.
3. Place une cible compatible à moins de `6` cases du même bassin.
4. Désactive `Chasse autonome`.
5. Lance `Implantation rituelle`.
6. Vérifie que le bassin apparaît dans le panneau d'inspection.
7. Détruis ensuite le bassin pendant un second test.
8. Vérifie que la cérémonie est annulée sans consommer le symbiote.


## Vérifier l'implantation médicale du Prim'ta

1. Génère un nouveau pawn Jaffa.
2. Ouvre son onglet de santé.
3. Planifie `implanter un Prim'ta jaffa`.
4. Génère une `larve de Prim'ta` avec le mode développeur.
5. Fournis un médecin de niveau `4+`, un médicament et une larve.
6. Laisse l'opération s'achever.
7. Vérifie que la larve est consommée.
8. Vérifie que `symbiote du Prim'ta` apparaît.
9. Vérifie les bonus biologiques.
10. Sauvegarde et recharge.
11. Vérifie que l'état persiste.
12. Vérifie que l'opération n'est pas proposée à un humain basique.


## Produire une larve de Prim'ta

1. Construis ou génère un `bassin d'incubation du Prim'ta`.
2. Ouvre son onglet des tâches.
3. Ajoute `incuber une larve de Prim'ta`.
4. Vérifie qu'un colon possède le travail `Dressage` actif et un niveau `Animaux 4+`.
5. Fournis au moins `10` unités de viande crue.
6. Laisse ce colon effectuer le travail automatiquement ou priorise le bassin manuellement.
7. Vérifie que la viande est consommée.
8. Vérifie qu'une `larve de Prim'ta` physique apparaît.
9. Stocke-la ou utilise-la pour une implantation médicale chez un Jaffa.


## Conserver une larve de Prim'ta

1. Produis ou génère une `larve de Prim'ta`.
2. Sélectionne l'objet.
3. Vérifie que les informations de pourrissement sont visibles.
4. Stocke une larve à température ambiante.
5. Stocke une autre larve dans une pièce froide.
6. Compare la progression de la détérioration.
7. Utilise une larve fraîche pour l'implantation médicale.


## Stocker une larve dans la catégorie biologique

Dans les filtres d'une zone de stockage, cherche :

```text
ressources brutes
    ↓
produits biologiques Goa'uld
```

Les larves de Prim'ta ne se trouvent plus dans les produits manufacturés.


## Vérifier le réglage thermique d'une larve

1. Sélectionne une `larve de Prim'ta`.
2. Vérifie les informations thermiques dans son panneau d'inspection.
3. Compare une pièce gelée, une pièce réfrigérée, une pièce tempérée et une pièce chaude.
4. Vérifie que les fortes chaleurs accélèrent la détérioration.


## Vérifier l'âge requis pour le Prim'ta

1. Génère un Jaffa compatible de moins de `10` ans biologiques.
2. Vérifie que l'opération `implanter un Prim'ta jaffa` est absente.
3. Génère un Jaffa compatible âgé d'au moins `10` ans.
4. Vérifie que l'opération apparaît.
5. Effectue ensuite la chirurgie normale avec une larve et un médicament.
