# Batailles entre domaines Goa'uld

> Première version : `0.3.69-dev`
> Statut : prototype local corrigé en `r6`, à revalider

Sous **Commandement SG-1**, un conflit ouvert entre deux domaines Goa'uld
peut désormais atteindre directement les abords d'une colonie joueur.

Deux détachements jaffa appartenant aux domaines exacts concernés entrent depuis
le bord de la carte. Chaque groupe rejoint d'abord un point de ralliement
opposé. Une fois les deux troupes suffisamment regroupées, un message annonce
le lancement de l'assaut. Les Jaffa avancent ensuite jusqu'à portée de leurs
armes avant d'ouvrir le feu, au lieu de rester sur leur point de ralliement.

Leur arrivée ne constitue pas un raid organisé contre la colonie.

## Choix du joueur

La colonie peut :

- rester à l'écart et laisser les deux camps s'épuiser ;
- attaquer un seul domaine ;
- combattre les deux forces ;
- capturer les survivants tombés à terre ;
- récupérer l'équipement abandonné selon les règles normales de RimWorld.

Le camp directement attaqué mémorise les colons provocateurs, avance jusqu'à
portée si nécessaire et leur riposte. Cette réaction reste locale : elle cesse
après environ `1800` ticks sans nouvelle attaque et la poursuite ne dépasse pas
`35` cellules depuis le point où la provocation a commencé. Une nouvelle
blessure subie après la chute du rival sert également de sécurité pour reconnaître
l'intervention du joueur. Un camp non provoqué reste concentré sur son rival.

## Durée limitée

Le retrait commence dès qu'un camp n'a plus de combattant mobile, lorsqu'un
seul camp tombe à `30%` ou moins de son effectif initial alors que l'autre reste
au-dessus de son propre seuil, ou au plus tard après deux jours. Si les deux
camps sont simultanément presque détruits, ils continuent à se battre jusqu'à
la défaite de l'un d'eux ou la limite absolue.

Les survivants encore capables de marcher quittent la carte ; un ordre de
sortie bloqué est automatiquement relancé. Une attaque du joueur peut interrompre
temporairement le retrait du camp concerné, mais pendant au plus `6000` ticks et
sans repousser la date de sortie forcée. Les pawns à terre, prisonniers, corps et
objets abandonnés ne sont pas supprimés artificiellement.

## Fréquence et conditions

L'événement exige :

- au moins deux domaines Goa'uld ;
- une paire réellement en conflit ouvert ;
- Commandement SG-1 comme storyteller actif ;
- une carte principale sans autre menace hostile active ;
- aucun autre champ de bataille Goa'uld en cours.

Le délai est caché et persistant. Changer de storyteller suspend les futures
opportunités sans interrompre une bataille déjà commencée.

## Limites

Cette première version ne crée pas de site sur la carte mondiale et ne détruit
aucune colonie Goa'uld. Le futur `0.3.70-dev` doit ajouter un site mondial
temporaire visitable ou ignorable par caravane, en réutilisant les mêmes règles
de combat entre les deux camps.
