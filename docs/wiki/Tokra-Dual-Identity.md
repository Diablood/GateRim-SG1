# Double identité Tok'ra

> Statut : première phase jouable dans `0.3.10-dev`

Un Tok'ra réunit deux personnes conscientes dans un même corps : l'hôte et le
symbiote. L'implantation ne doit donc pas effacer le nom ou le parcours de l'un
au profit de l'autre.

## Ce qui est conservé

Lorsqu'un symbiote Tok'ra rejoint un hôte, GateRim SG-1 enregistre séparément :

- le nom de l'hôte ;
- l'enfance et la carrière de l'hôte ;
- le nom propre du symbiote ;
- une carrière Tok'ra propre au symbiote ;
- l'hôte actuel et l'hôte précédent dans les données techniques de transfert.

Le symbiote conserve la même identité lorsqu'il passe de l'implantation récente
à la symbiose active. Cette identité suit également le symbiote lors d'une
extraction puis d'une nouvelle implantation.

## Affichage pour le joueur

Sur un Tok'ra appartenant réellement à la colonie et directement contrôlé par le
joueur, la description de l'état de santé présente :

- l'identité de l'hôte ;
- l'identité du symbiote ;
- le parcours de l'hôte ;
- le parcours du symbiote.

L'affichage ne remplace pas le nom principal du pawn et ne modifie pas ses
backstories actives. Les compétences, relations, traits, faction, équipement et
état physique restent inchangés.

Les visiteurs, alliés, ennemis et autres Tok'ra gérés par le jeu conservent leur
fonctionnement classique. Leurs données peuvent rester enregistrées pour les
transferts, mais aucune interface supplémentaire ne leur est exposée.

## Carrières actuelles du symbiote

La carrière persistante est choisie parmi les six parcours Tok'ra déjà présents :

- infiltrateur ;
- médecin ;
- diplomate ;
- éclaireur ;
- analyste ;
- courrier.

Le choix est lié à l'identité persistante du symbiote. Une sauvegarde ou un
rechargement ne doit donc pas tirer une nouvelle carrière.

Aucune enfance Tok'ra distincte n'est encore configurée. Le format de données la
prévoit cependant pour une extension future raisonnable des backstories.

## Limite de cette première phase

`0.3.10-dev` ne permet pas encore de choisir quelle personnalité prend le
contrôle. Aucun gizmo de basculement n'est ajouté dans cette version.

Un futur jalon devra prototyper séparément :

- le changement du nom affiché ;
- le changement des backstories présentées ;
- l'application sûre des seuls écarts de compétences associés aux backstories ;
- la conservation d'une progression commune sans cumul ni perte d'expérience.
