# Site monde de mission Tok'ra décodée

> Première version : `0.2.42-dev`

Après l'analyse du paquet de renseignements codés et la réception d'une
[piste de mission décodée](Tokra-Decoded-Mission-Lead), la cellule Tok'ra peut
révéler les coordonnées d'un relais Goa'uld isolé.

## Apparition sur la carte du monde

Le relais apparaît comme un site temporaire. Le marqueur expire si la colonie
l'ignore trop longtemps.

Une caravane sélectionnée peut recevoir l'ordre de rejoindre le site depuis le
menu contextuel de la carte du monde. Le même menu permet de redonner le relais
comme destination si la route de la caravane a été modifiée.

## Déroulement actuel

Le marqueur n'est plus une simple piste sans contenu. Il mène à une opération
jouable sur carte temporaire :

- garnison Goa'uld/Jaffa dimensionnée selon la menace de la colonie ;
- l'un de plusieurs petits postes relais fortifiés ;
- nœud de contrôle à saboter par un colon capable d'Intellectuel ;
- progression du sabotage conservée en cas d'interruption ;
- avertissement avant l'arrivée éventuelle de renforts ;
- évacuation possible après résolution lorsque plus aucun hostile actif ne
  bloque le départ ;
- petite réserve déjà présente dans le poste ;
- débriefing Tok'ra différé après le retour de la caravane.

Détruire le relais avant la fin du sabotage provoque un échec de l'approche
discrète et prépare une riposte Goa'uld.

## Compatibilité des anciennes étapes

La reconnaissance introduite en `0.2.43-dev` et la préparation du sabotage
introduite en `0.2.44-dev` restent enregistrées pour les anciennes sauvegardes
et le rapport du canal Tok'ra.

Le flux actuel évite toutefois une succession d'actions similaires : le joueur
utilise une seule action sur le site pour lancer l'opération locale.

Après le départ de la caravane, la carte temporaire et le marqueur monde sont
supprimés proprement.
