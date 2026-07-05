# Formatage des durées

> Statut : validé et publié dans `0.3.71-dev`

GateRim SG-1 utilise le format temporel localisé de RimWorld pour les délais
visibles par le joueur. Selon la durée réelle, le jeu peut afficher des heures,
des jours, des quadrums ou des années au lieu de convertir toutes les échéances
en un grand nombre d'heures.

Le format commun couvre notamment :

- les principaux sites mondiaux et leurs comptes à rebours ;
- l'extraction d'un officier Jaffa et les renforts du relais ;
- les lettres d'offre et les statuts des huit familles d'opérations Tok'ra ;
- les refroidissements et dialogues du communicateur sécurisé ;
- les étapes en attente de la mission Tok'ra unique ;
- les estimations de menace, l'ancien marqueur de planque et les offres
  thérapeutiques ;
- la récupération après extraction d'une reine Goa'uld et les délais
  diplomatiques Tok'ra.

Un audit automatique du dépôt détecte les traductions qui accoleraient encore
une unité fixe à une durée dynamique, les clés de migration dupliquées et les
convertisseurs manuels proches de l'interface joueur. Les appels vanilla, les
calculs mécaniques et les diagnostics développeur restent correctement
identifiés.

Le changement est uniquement visuel : les échéances, arrivées, délais,
refroidissements, probabilités, sauvegardes et conditions de réussite ne sont
pas modifiés. Les diagnostics développeur peuvent encore afficher des ticks
bruts.

La validation finale couvre le français et l'anglais, les durées courtes et
longues, la sauvegarde/recharge et l'absence d'unité doublée sur les surfaces
historiques corrigées.
