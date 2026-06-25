# Validation terminée — 0.3.41-dev

Jalon : `0.3.41-dev - Add active Goa'uld host extraction surgery`

Branche : `feature/goauld-active-host-extraction-surgery`

Tag de départ : `v0.3.40-dev`

Version de DLL validée : `0.3.41.0`

Révision locale finale : `r2`

## Validation fonctionnelle

- [x] Contrôle de cohérence `0.3.41-dev` / `83` backstories.
- [x] Build et chargement de `GateRimSG1.dll` version `0.3.41.0`.
- [x] Conversion hostile, mise à terre et capture de l'ancien colon.
- [x] Maintien en détention sans réassignation au groupe d'assaut.
- [x] Disponibilité et réussite de `extraire le symbiote Goa'uld actif`.
- [x] Restauration du même pawn, de sa faction et de son nom complet d'origine.
- [x] Conservation de l'identité, du nom et de l'allégeance du symbiote.
- [x] Apparition d'un seul symbiote libre sous anesthésie vanilla temporaire.
- [x] Réveil du symbiote et reprise de son comportement hostile normal.
- [x] Échec chirurgical non létal sans duplication ni suppression de l'état actif.
- [x] Sauvegarde/rechargement pendant la prise de contrôle et après extraction.
- [x] Régressions Tok'ra, implantation récente et symbiote joueur fonctionnelles.

## Validation ciblée finale de `r2`

- [x] L'option avancée GateRim SG-1 n'expose plus l'extraction instantanée hors mode développeur RimWorld.
- [x] Un symbiote Goa'uld hostile sélectionné n'expose plus `Implantation forcée`, `Implantation rituelle` ou `Chasse autonome` en jeu normal.
- [x] Le mode développeur restaure les commandes techniques attendues.
- [x] Un symbiote réellement contrôlé par le joueur conserve le rite autorisé.
- [x] Une offre thérapeutique Tok'ra suivie conserve ses interactions volontaires et son refus explicite.
- [x] Après la prise de contrôle active, le pawn existant affiche le nom persistant du symbiote.
- [x] La lettre distingue encore l'ancien nom de l'hôte du nom du Goa'uld.
- [x] Le nom du symbiote reste affiché après sauvegarde/rechargement sans changement du ThingID du pawn.
- [x] Une extraction réussie restaure exactement le nom d'origine sur le même pawn.
- [x] Le symbiote libre extrait conserve son propre nom persistant.
- [x] Aucun nouvel échec GateRim SG-1 n'est signalé dans `Player.log`.

## Limites validées

- L'extraction ne tue pas automatiquement le symbiote.
- L'anesthésie offre une fenêtre de sécurité, mais le Goa'uld redevient dangereux à son réveil.
- Aucun système de confinement, de remise aux Tok'ra, d'interrogatoire ou d'étude n'est ajouté dans ce jalon.
- La piste d'un confinement dédié est conservée dans `docs/IDEAS_TO_REVISIT.md` comme idée non planifiée, sans créer de futur jalon.

## Publication

- [x] Verrou documentaire final appliqué.
- [x] Commit final : `0.3.41-dev - add active Goa'uld host extraction surgery`.
- [x] Branche publiée : `feature/goauld-active-host-extraction-surgery`.
- [x] Tag final unique publié : `v0.3.41-dev`.
- [x] Wiki séparé synchronisé, car des fichiers `docs/wiki/*.md` sont modifiés.
