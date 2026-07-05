# Capture d'un officier Jaffa

> État : mission récurrente validée dans `0.3.37-dev` ; équipement distinctif
> corrigé, validé et publié dans `0.3.74-dev` après la révision finale `r2`.

Les Tok'ra peuvent proposer une opération récurrente visant un officier Jaffa isolé dans une position de campagne Goa'uld. La cible doit être ramenée vivante.

## Déroulement

- accepter l'offre depuis le communicateur sécurisé ;
- récupérer le fusil hypodermique Tok'ra scellé livré dans la zone prioritaire ;
- envoyer une caravane vers la position révélée ;
- identifier l'officier marqué d'argent grâce à son armure et son casque rouges, le neutraliser sans le tuer et éliminer ou faire fuir son escorte ;
- reformer directement la caravane sans construire de lit de prisonnier sur le site ;
- sélectionner l'officier à terre comme prisonnier dans la fenêtre vanilla ;
- laisser la carte temporaire et son marqueur disparaître après l'évacuation réelle ;
- ramener le captif dans une colonie du joueur et le placer en détention ;
- utiliser `Appeler l'équipe d'extraction Tok'ra` sur un communicateur alimenté ;
- maintenir le prisonnier vivant jusqu'à l'arrivée de l'équipe quelques heures plus tard ;
- laisser les agents Tok'ra prendre physiquement le prisonnier et quitter la carte.

Le statut de prisonnier est créé par la reformation vanilla. GateRim ne demande aucun lit sur le site hostile et ne convertit pas artificiellement la cible avant le départ.

Pendant le trajet, l'officier reste ligoté afin qu'il ne reprenne pas le combat après la fin de l'inhibition du fusil. Sur la carte de la colonie, cette entrave est retirée : RimWorld reprend normalement la gestion du lit, de l'attente et d'une éventuelle fuite.

## Disparition du site

Le suivi de la seconde moitié de la mission n'est plus stocké dans le marqueur mondial. Dès que l'officier est confirmé dans une caravane ou une colonie du joueur et qu'aucun pawn du joueur ne reste sur place, la carte hostile et son marqueur sont supprimés. Le prisonnier, le délai d'extraction et l'équipe Tok'ra restent suivis par l'opération persistante.

## Extraction visible

L'appel au communicateur ne fait pas disparaître le prisonnier. Après un délai caché d'environ quatre à douze heures en jeu, deux ou trois agents Tok'ra entrent depuis le bord de la carte. L'officier est alors entravé pour la récupération, porté par un agent et escorté jusqu'au périmètre.

La réussite n'est accordée qu'après le départ du porteur et de tous les autres membres de l'équipe. Si le prisonnier n'est plus détenu au moment de la récupération, l'équipe attend ou retente au lieu de valider artificiellement la mission.

## Difficulté et rejouabilité

L'escorte Goa'uld/Jaffa dépend des points de menace capturés lors de l'offre. L'opération peut revenir après une réussite ou un échec, avec délai caché variable et forte pénalisation de la répétition immédiate.

## Validation

La révision locale finale `r6` a validé le flux complet : extraction du site, transport du prisonnier, disparition du marqueur mondial, appel depuis le communicateur, arrivée visible de l'équipe Tok'ra, prise en charge physique et réussite après le départ complet de l'équipe.

`0.3.74-dev-r1` a ajouté une armure de commandement rouge et un casque rouge
rétractable aux chemins définitifs. La recherche, les textures, les protections
et le bonus de `+10 %` d'impact social ont été validés, mais la cible générée ne
recevait pas automatiquement les deux pièces. La révision finale `r2` les
vérifie et les équipe explicitement uniquement sur le nouvel officier de
capture, sans rendre ces variantes aléatoires pour les autres Jaffa. Le loadout
complet, les modes du casque, la sauvegarde/recharge et le flux d'extraction sont
validés. Les textures pourront être remplacées lors de la passe artistique
finale sans modifier les sauvegardes.
