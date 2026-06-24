# Validation finale — 0.3.37-dev

Jalon : `0.3.37-dev - Add Tok'ra Jaffa officer capture operation`

Branche : `feature/tokra-jaffa-officer-capture-operation`

Tag de départ : `v0.3.36-dev`

Version de DLL validée : `0.3.37.0`

Révision locale finale : `r6`

Tag final : `v0.3.37-dev`

## Résultat final

Le build et le test fonctionnel principal sont validés. La mission couvre désormais toute la boucle sans dépendre du site mondial après son évacuation : capture vivante, transport, détention, appel au communicateur, arrivée visible des Tok'ra et extraction physique du prisonnier.

## Couverture validée

- [x] Une offre récurrente crée un site temporaire avec un officier Jaffa vivant marqué d'argent et une escorte Goa'uld/Jaffa adaptative.
- [x] Le fusil hypodermique Tok'ra est livré à la zone prioritaire avec `12 / 12` charges.
- [x] L'officier peut être neutralisé vivant sans baisse de Conscience ni mort aléatoire de mise à terre.
- [x] La reformation vanilla devient disponible lorsque l'escorte active est neutralisée ou mise en fuite.
- [x] Aucun lit de prisonnier n'est requis sur la carte temporaire.
- [x] La fenêtre vanilla permet de sélectionner l'officier à terre comme prisonnier de caravane.
- [x] Le ligotage de transfert maintient la cible pendant le portage et le voyage après expiration de l'inhibition neuromusculaire.
- [x] La cible redevient un prisonnier vanilla normal une fois placée dans une colonie du joueur.
- [x] La carte hostile et son marqueur mondial disparaissent après l'évacuation réelle, sans échec de mission ni perte du prisonnier.
- [x] Le suivi persistant conserve la cible après la suppression du WorldObject.
- [x] Le communicateur alimenté expose `Appeler l'équipe d'extraction Tok'ra` sur la colonie qui détient réellement le prisonnier.
- [x] L'appel ne retire pas immédiatement le prisonnier et programme une arrivée différée.
- [x] Une équipe Tok'ra visible entre sur la carte, prend physiquement le prisonnier en charge et repart avec lui.
- [x] La mission ne réussit qu'après le départ complet du porteur et des autres agents Tok'ra.
- [x] Le résultat n'est appliqué qu'une seule fois.
- [x] Aucun problème fonctionnel bloquant supplémentaire n'a été signalé pendant la validation finale `r6`.

## Régressions durables

À rejouer lorsqu'un futur jalon modifie le framework, les caravanes, les prisonniers, le communicateur ou les équipes d'extraction :

- sauvegarde/rechargement pendant le trajet, la détention, le délai d'arrivée et la prise en charge ;
- appel indisponible depuis une autre carte ou sans détention réelle de la cible ;
- perte temporaire du statut de prisonnier avant l'arrivée ;
- interruption ou neutralisation du porteur et réattribution à un autre agent ;
- perte complète de l'équipe et nouvelle tentative sans duplication ;
- mort, fuite, disparition ou expiration de la cible produisant un seul échec ;
- rééligibilité après réussite, échec et offre ignorée avec délai caché et anti-répétition ;
- comparaison de l'escorte sur une colonie faible et une colonie avancée ;
- fonctionnement des sept opérations Tok'ra précédentes.

## Points visuels différés

Ces points ne bloquent pas la publication fonctionnelle et sont conservés dans `docs/ROADMAP.md` :

- diversifier les icônes de carte mondiale selon le thème des missions ;
- donner à l'officier Jaffa une apparence plus distinctive qu'un Jaffa ordinaire.
