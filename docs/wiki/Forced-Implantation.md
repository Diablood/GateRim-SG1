# Implantation forcée Goa'uld

> Statut : prototype jouable
> Première version : `0.1.17-dev`

## Présentation

![Icône finale de l'implantation forcée](images/SG1_ForcedImplantation.png)

Un symbiote Goa'uld adulte privé d'hôte peut implanter de force un humanoïde
compatible.

## Comportement normal

Le symbiote libre recherche de manière autonome une cible adulte accessible,
la poursuit et commence l'implantation au contact.

La victime reçoit une implantation Goa'uld récente. Si le symbiote n'est pas
extrait pendant la phase critique, la conversion en hôte Goa'uld actif se
produit automatiquement.

L'identité persistante du parasite est conservée pendant tout le processus.

## Commande de test

Le mode développeur permet encore de sélectionner un symbiote adjacent à une
cible et d'utiliser `Implantation forcée`. Cette commande sert aux validations
rapides et choisit le premier humanoïde adjacent compatible.

## Cibles acceptées

| Cible | Résultat |
|---|---|
| Humanoïde adulte | autorisé |
| Jaffa adulte | autorisé |
| Enfant de moins de 13 ans | refusé |
| Animal | refusé |
| Mécanoïde | refusé |
| Cible déjà implantée | refusée |
| Hôte possédant déjà un symbiote adulte | refusé |

## Autres voies

- [Chasse autonome](Autonomous-Hunt)
- [Implantation rituelle](Ritual-Implantation)
- [Extraction d'urgence](Emergency-Extraction)
- [Chirurgie d'extraction](Extraction-Surgery)

## Limite actuelle

Les futurs Unas et leurs règles biologiques particulières ne sont pas encore
gérés.

## Commande de test

La commande adjacente `Implantation forcée` est désormais réservée au mode développeur RimWorld. Sélectionner un symbiote hostile en jeu normal ne permet pas de choisir directement sa victime : il utilise sa chasse autonome et tente l'implantation au contact.
