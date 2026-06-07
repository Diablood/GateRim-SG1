# Implantation forcée Goa'uld

> Statut : Prototype  
> Version d'introduction : 0.1.17-dev

## Présentation

Un symbiote Goa'uld adulte privé d'hôte peut désormais commencer une implantation forcée sur un humanoïde compatible adjacent.

## Utilisation actuelle

1. Active le mode développeur.
2. Génère un `symbiote Goa'uld`.
3. Place-le à côté d'un pawn humanoïde adulte.
4. Sélectionne le symbiote libre.
5. Clique sur `Implantation forcée`.

Le symbiote libre disparaît et la victime reçoit :

```text
implantation Goa'uld récente
```

L'identité persistante du parasite est conservée.

## Cibles acceptées

| Cible | Résultat |
|---|---|
| Humanoïde adulte | Autorisé |
| Jaffa adulte | Autorisé |
| Enfant de moins de 13 ans | Refusé |
| Animal | Refusé |
| Mécanoïde | Refusé |
| Pawn déjà implanté | Refusé |
| Hôte possédant déjà un symbiote adulte | Refusé |

## Limites du prototype

- la commande choisit le premier humanoïde adjacent compatible ;
- l'action doit être déclenchée manuellement ;
- le symbiote ne dispose pas encore d'une IA hostile autonome ;
- la conversion finale en hôte Goa'uld actif n'est pas encore automatique ;
- les futurs Unas ne sont pas encore gérés.


## Chasse autonome disponible

Depuis `0.1.21-dev`, la [chasse autonome](Autonomous-Hunt) constitue le
comportement normal du symbiote libre. La commande manuelle reste disponible
comme outil de test.


## Voie rituelle distincte

Depuis `0.1.22-dev`, une [implantation rituelle](Ritual-Implantation) contrôlée
permet de déclencher une implantation à portée limitée sans attendre le contact.
