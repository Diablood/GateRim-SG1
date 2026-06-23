# Communicateur sécurisé Tok'ra

> Statut : système actif
> Première version : `0.2.26-dev`

Le communicateur sécurisé est le principal point de contact entre la colonie et
les cellules Tok'ra. Il reste clandestin et ne transforme pas les Tok'ra en
faction alliée classique.

## Construction et utilisation

- bâtiment alimenté ;
- recherche dédiée `Communications sécurisées Tok'ra` requise pour toute nouvelle construction ;
- interaction principale par un colon sélectionné puis clic droit ;
- courte utilisation du bâtiment avant l'ouverture d'une fenêtre ou la
  transmission de la demande ;
- commandes directes du bâtiment masquées en jeu normal et réservées au debug.

Les demandes manuelles sensibles exigent généralement une confiance fiable.
Certaines offres organiques initiées par les Tok'ra peuvent apparaître plus tôt.

Depuis `0.3.33-dev`, la recherche dédiée remplace Microélectronique comme verrou direct de construction. Elle dépend elle-même d'Électricité et de l'analyse du module de chiffrement récupéré pendant la mission d'introduction. Un communicateur déjà construit dans une ancienne sauvegarde reste utilisable : le nouveau prérequis ne détruit ni ne désactive les bâtiments existants.

Le canal doit aussi rester réellement disponible pour recevoir une nouvelle opération Tok'ra organique : au moins un communicateur contrôlé par le joueur doit être construit sur une carte de colonie et alimenté. Une coupure ou la destruction du bâtiment empêche seulement les nouvelles offres. Une opération déjà proposée ou engagée reste enregistrée. Après rétablissement du canal, les Tok'ra attendent un nouveau délai caché avant de reprendre contact ; aucune mission n'est garantie immédiatement.

## Fonctions actuelles

Le communicateur permet notamment :

- de répondre aux [opérations organiques](Tokra-Organic-Operation-Opportunities)
  actuellement proposées ;
- de demander une [diversion défensive](Tokra-Defensive-Diversion-Request)
  pendant une menace active ;
- de solliciter un [conseil médical](Tokra-Communicator-Medical-Support) ;
- de demander un [cache médical d'urgence](Tokra-Emergency-Medical-Cache) ;
- d'obtenir une [évaluation tactique](Tokra-Tactical-Threat-Assessment) ;
- de consulter un [rapport d'état du canal](Tokra-Communicator-Status-Report) ;
- d'ouvrir la première chaîne de mission Tok'ra.

## Rapport du canal

Le rapport donne une lecture RP de la posture de la cellule, des demandes
actives, des délais de soutien et du contexte local.

Il ne consomme aucun délai et ne révèle ni score brut de confiance, ni
pondération, ni calendrier futur des opérations organiques. Il affiche
uniquement l'opération organique réellement en cours et les étapes durables des
missions uniques déjà accomplies lorsqu'elles restent utiles au joueur. Les
détails complets sont réservés au debug.

Les options verrouillées utilisent des raisons courtes. Les détails restent
dans le rapport plutôt que dans des libellés de bouton trop longs.

## Soutiens fiables

### Diversion défensive

Pendant une attaque active, la cellule peut perturber brièvement un petit nombre
d'ennemis. Aucun renfort Tok'ra physique n'arrive sur la carte.

### Conseil médical

Un colon reçoit un conseil et de l'expérience en Médecine, sans soin direct ni
objet livré.

### Cache médical d'urgence

Une petite cache est déposée selon la priorité de la
[zone de livraison Tok'ra](Tokra-Delivery-Drop-Zone), puis du communicateur et
du point de repli prévu.

### Évaluation tactique

Le rapport résume la force hostile active ou enrichit les informations d'une
menace interceptée, sans infliger de dégâts ni révéler toute la carte.

## Chaîne de mission

Une colonie fiable peut déclarer sa disponibilité pour une opération discrète.
La cellule transmet ensuite un briefing, livre des renseignements codés et peut
révéler un relais Goa'uld menant à une mission de sabotage jouable.

La progression ne demande pas une succession immédiate de clics au
communicateur : plusieurs étapes sont initiées automatiquement par les Tok'ra.

## Limites actuelles

Le communicateur ne fournit pas :

- de soin automatique ;
- de commerce permanent ;
- de recrutement ;
- de renfort militaire récurrent ;
- de catalogue de quêtes ;
- de liste des futures opérations organiques.

Le prototype de débriefing manuel existe encore côté code mais reste masqué
dans l'interface joueur ; les débriefings utiles à la mission du relais sont
transmis automatiquement.

## Affichage en jeu normal

Le panneau d'inspection reste volontairement compact. Il affiche uniquement l'état du canal, l'opération organique actuellement en cours et, lorsqu'elle existe, l'issue durable d'une mission unique achevée. Les demandes verrouillées, délais internes, pondérations, historiques et diagnostics complets sont réservés au mode développeur ou à l'option de debug avancé GateRim SG-1.
