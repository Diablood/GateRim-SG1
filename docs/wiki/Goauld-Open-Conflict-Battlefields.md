# Batailles entre domaines Goa'uld

> Première version locale : `0.3.69-dev`
> Site mondial : `0.3.70-dev`
> Statut : site mondial validé en révision finale `r4`

Sous **Commandement SG-1**, un conflit ouvert entre deux domaines Goa'uld peut
produire une bataille visible sous deux formes.

## Près d'une colonie

La forme locale fait entrer deux détachements jaffa depuis le bord d'une carte
principale. Chaque groupe rejoint un point de ralliement opposé, puis un message
annonce le lancement de l'assaut.

Les combattants avancent jusqu'à portée et s'affrontent entre eux. Leur arrivée
n'est pas un raid organisé contre la colonie.

## Sur la carte mondiale

La version `0.3.70-dev` peut aussi faire apparaître un **champ de bataille
Goa'uld** temporaire à quelques jours de marche d'une colonie.

Le marqueur :

- nomme les deux domaines engagés ;
- possède une icône mondiale dédiée ;
- reste disponible pendant environ huit jours ;
- peut être visité par une caravane ou complètement ignoré.

Ignorer le site ne compte pas comme un échec. Il disparaît sans perte de bonne
volonté, sans changement politique et sans destruction de colonie mondiale.

La carte de combat n'est créée qu'à l'arrivée de la caravane. Les deux forces
utilisent ensuite exactement les mêmes règles que la bataille locale : entrée
depuis le bord, rassemblement, annonce, assaut mutuel, rupture morale et retrait.

## Choix du joueur

La colonie peut :

- rester à l'écart ;
- observer ou exploiter le combat ;
- attaquer un seul domaine ;
- combattre les deux forces ;
- capturer les survivants tombés à terre ;
- repartir avec les prisonniers et objets sélectionnés lors de la reformation
  normale de la caravane.

Le camp directement attaqué riposte, mais sa réaction reste bornée :

- elle cesse après environ `1800` ticks sans nouvelle attaque ;
- elle ne poursuit pas au-delà de `35` cellules depuis le point de provocation ;
- pendant le retrait, elle ne peut durer plus de `6000` ticks ni retarder la
  date de sortie forcée.

Un camp non provoqué continue de privilégier son rival.

## Fin de l'affrontement

Le retrait commence lorsque :

- un camp n'a plus de combattant mobile ;
- un seul camp tombe à `30 %` ou moins de son effectif mobile initial ;
- deux jours de combat se sont écoulés.

Si les deux camps sont simultanément presque détruits, ils continuent jusqu'à
une autre condition de fin.

Les pawns capables de marcher quittent la carte. Les blessés à terre,
prisonniers, corps et équipements abandonnés restent disponibles selon les
règles normales de RimWorld.

Sur un site mondial, le marqueur reste présent tant que des pawns joueur sont
sur la carte. Il disparaît après la résolution et la reformation complète de la
caravane.

## Fréquence et alternance

Les deux formes partagent :

- un seul emplacement actif ;
- un seul délai caché ;
- la mémoire de la dernière paire ;
- une alternance locale / mondiale lorsque les deux sont possibles.

Une bataille locale et un site mondial ne peuvent donc pas apparaître en même
temps. Changer de storyteller suspend les futures opportunités sans supprimer
un site ou une bataille déjà en cours.

## Limites actuelles

Ces batailles ne modifient pas encore les territoires, les colonies des domaines
ou leur relation stratégique. Elles ne donnent pas de récompense artificielle :
seuls les prisonniers et équipements réellement présents sur le terrain peuvent
être récupérés.

## Ajustements de la révision r4

- Le marqueur mondial emploie un texte plus immersif et affiche les longues durées en jours et heures.
- Une intervention joueur ajoute vos soldats aux ennemis déjà présents : le camp provoqué partage sa riposte sans abandonner entièrement son adversaire Goa'uld.
- Après la fin des menaces actives, le bouton vanilla de reformation de caravane est disponible normalement.
