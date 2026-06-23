# Validation finale — 0.3.35-dev

Jalon : `0.3.35-dev - Reorganize GateRim SG-1 debug actions into logical submenus`

Branche : `feature/debug-action-menu-reorganization`

Tag de départ : `v0.3.34-dev`

Révision locale finale validée : `0.3.35-dev-r2`

Version de DLL validée : `0.3.35.0`

Tag final publié : `v0.3.35-dev`

## Validation locale terminée

- `./tools/check-project-consistency.cmd` termine sans erreur.
- Le rebuild forcé produit la DLL `0.3.35.0`.
- La catégorie native `GateRim SG-1` contient directement, dans cet ordre :
  1. `Tok'ra...`
  2. `Jaffa...`
  3. `Culture...`
  4. `Inspect mission definitions`
- Aucun niveau intermédiaire `GateRim SG-1...` n'est présent.
- Les anciennes entrées plates `Tok'ra ops: ...`, `Tok'ra intro: ...`, `Tok'ra study: ...`, `Jaffa mark: ...` et `Cultural names: ...` ne sont plus enregistrées séparément.
- Le sous-menu Tok'ra conserve l'ordre communicateur, introduction, étude du module, opérations organiques, puis chaîne des planques et renseignements.
- Les opérations organiques affichent d'abord le framework, puis les sept archétypes récurrents dans leur ordre établi.
- Les rapports représentatifs du communicateur, de l'introduction, de l'étude, du framework organique et des MissionDefs s'ouvrent correctement.
- Une offre d'observation peut toujours être forcée lorsque ses préconditions sont satisfaites.
- Les outils de confiance, les échantillons de noms culturels et les autres actions testées conservent leur comportement antérieur.
- `GateRim SG-1 → Jaffa... → Forehead marks...` active toujours l'outil de ciblage de pawn pour appliquer ou retirer une marque.
- Les ouvertures répétées, la navigation et la sauvegarde/rechargement ne créent aucun doublon.
- Aucune nouvelle erreur liée à `DebugActionNode`, `GateRimDebugActionMenu`, aux délégués ou aux méthodes déplacées n'a été signalée pendant la validation.

## Limites confirmées

- Aucun gameplay, état de mission, format de sauvegarde, Def, traduction ou texte joueur n'est modifié.
- Les gizmos contextuels et rapports avancés sur les objets sélectionnés restent inchangés.
- Le jalon n'ajoute ni recherche, ni fenêtre personnalisée, ni texture.
- Aucun fichier n'est supprimé.

## Couverture durable

Les régressions à maintenir sont enregistrées dans `docs/TESTING.md`, notamment :

- les quatre entrées directes et leur ordre ;
- l'absence permanente d'un wrapper `GateRim SG-1...` ;
- l'absence de retour des anciennes actions plates ;
- l'ordre des branches Tok'ra et des sept opérations organiques ;
- la conservation des outils `ToolMapForPawns` pour les marques Jaffa ;
- l'absence d'exception après navigation répétée et sauvegarde/rechargement ;
- l'invisibilité de ces outils hors du mode développeur.

## Publication

La branche `feature/debug-action-menu-reorganization`, le tag final unique `v0.3.35-dev` et les pages wiki modifiées sont publiés. Le prochain jalon doit partir explicitement de `v0.3.35-dev`.
