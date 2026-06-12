# Chasse autonome des symbiotes libres

> Statut : Prototype  
> Version d'introduction : 0.1.21-dev

## Présentation

Un symbiote Goa'uld adulte libre est désormais une menace autonome.

Il recherche périodiquement un humanoïde compatible accessible, poursuit la
cible la plus proche puis commence son implantation au contact.

## Cycle

```text
symbiote libre
    ↓ recherche périodique
humanoïde compatible proche
    ↓ poursuite autonome
contact
    ↓
implantation Goa'uld récente
```

## Cibles actuelles

| Cible | Résultat |
|---|---|
| Humanoïde adulte accessible | Autorisé |
| Jaffa adulte accessible | Autorisé |
| Enfant de moins de 13 ans | Refusé |
| Animal | Refusé |
| Mécanoïde | Refusé |
| Cible déjà implantée ou possédée | Refusée |

## Extraction et délai de sécurité

Après une extraction manuelle ou chirurgicale, le symbiote libre dispose d'un
court délai avant de reprendre sa chasse autonome.

Ce délai évite une réimplantation immédiate du patient.

## Commande de test

Lorsqu'un symbiote libre est sélectionné, la commande suivante permet d'activer
ou désactiver son comportement autonome :

```text
Chasse autonome
```

La commande manuelle `Implantation forcée` reste également disponible pour les
tests.

## Limites actuelles

- rayon de recherche limité ;
- priorité donnée à la cible compatible accessible la plus proche ;
- absence de préférence tactique avancée ;
- aucun événement ne génère encore naturellement de symbiotes libres ;
- prise en charge des Unas prévue ultérieurement.
