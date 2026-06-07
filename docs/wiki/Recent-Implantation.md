# Implantation Goa'uld récente

> Statut : Prototype  
> Version d'introduction : 0.1.11-dev

## Présentation

Cet état de santé représente la période critique qui suit l'entrée d'un symbiote Goa'uld adulte dans un nouvel hôte humanoïde.

Le parasite tente de s'attacher au système nerveux et de prendre le contrôle de sa victime.

## Fonctionnement actuel

| Élément | Valeur |
|---|---:|
| Durée | Une journée de jeu |
| Compte à rebours visible | Oui |
| Douleur supplémentaire | `+15 %` |
| Application automatique | Non |
| Conversion automatique en hôte Goa'uld | Oui, après une journée |
| Traitement médical | Non |

## Tester le prototype

1. Active le mode développeur.
2. Sélectionne un pawn humanoïde.
3. Utilise l'action permettant d'ajouter un état de santé.
4. Ajoute `implantation Goa'uld récente`.
5. Vérifie le compte à rebours et la douleur temporaire.

## Évolutions prévues

- application automatique après l'attaque d'un symbiote libre ;
- implantation rituelle sur une cible préparée ;
- fenêtre d'intervention médicale ;
- extraction spécialisée ;
- conversion automatique en hôte Goa'uld actif lorsque le temps est écoulé.


## Origine interactive

Depuis `0.1.17-dev`, cet état peut être appliqué par un `symbiote Goa'uld` libre grâce à la commande manuelle [Implantation forcée](Forced-Implantation).

L'identité persistante du parasite est conservée entre l'entité libre et la victime.


## Conversion automatique

Depuis `0.1.18-dev`, l'expiration du compte à rebours transforme automatiquement l'état récent en [hôte Goa'uld actif](Active-Goauld-Host).

L'identité persistante du parasite est conservée.
