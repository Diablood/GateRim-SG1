# Présence mondiale Tok'ra

Depuis `0.3.49-dev`, les Tok'ra apparaissent dans la liste des factions lors de la création du monde, comme les factions vanilla sans colonies.

## Réglage par défaut

- une faction Tok'ra est sélectionnée par défaut ;
- le maximum est limité à une ;
- l'entrée affiche une icône Tok'ra dédiée dans la liste des factions ;
- elle ne crée aucune colonie ;
- elle reste masquée dans la liste diplomatique ordinaire, conformément à son fonctionnement clandestin.

Cette instance est utilisée par les visiteurs, le réseau de planques, la mission d'introduction, le communicateur et les opérations récurrentes.

## Retirer les Tok'ra

Le joueur peut supprimer l'entrée Tok'ra avant de générer le monde. Dans cette partie :

- l'écran de création du monde affiche immédiatement un avertissement jaune ;
- aucune faction Tok'ra n'est créée ;
- aucune ville Tok'ra n'apparaît ;
- la mission d'introduction, les visiteurs, les soutiens, les signaux de planque et les opérations Tok'ra ne sont pas proposés ;
- le communicateur sécurisé ne propose aucune action Tok'ra ;
- sauvegarder et recharger ne recrée pas la faction.

Les Jaffa, Goa'uld, équipements, recherches et autres contenus de GateRim SG-1 restent disponibles.

## Icône et avertissement

L'icône de la ligne Tok'ra utilise `World/WorldObjects/Expanding/SG1_Tokra`.

L'avertissement jaune est une intégration UI ciblée via Harmony, car RimWorld 1.6 ne fournit pas de champ XML générique pour les avertissements de retrait de faction. Le patch ajoute uniquement le texte d'avertissement au bloc vanilla déjà utilisé pour les mécanoïdes et insectes désactivés, après la remise à zéro vanilla de la hauteur d'avertissement ; il ne recrée jamais la faction et ne modifie pas le choix du joueur.

## Anciennes sauvegardes

Une sauvegarde contenant déjà la faction Tok'ra la conserve. Une sauvegarde qui n'en contient aucune reste sans Tok'ra : le mod ne fabrique plus de présence de remplacement.

## Absence de colonies

Le réglage contrôle l'existence du réseau Tok'ra, pas un nombre de villes. Même lorsqu'ils sont activés, les Tok'ra restent une organisation clandestine sans implantation territoriale ordinaire sur la carte mondiale.
