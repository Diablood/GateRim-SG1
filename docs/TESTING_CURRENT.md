# Validation finale — 0.3.39-dev

Jalon : `0.3.39-dev - Add Goa'uld free-symbiote incursion incident`

Branche : `feature/goauld-free-symbiote-incursion`

Tag de départ : `v0.3.38-dev`

Version de DLL validée : `0.3.39.0`

Révision locale finale : `r1`

Tag final : `v0.3.39-dev`

## Résultat final

Le contrôle de cohérence, le build Windows et la validation ciblée en jeu sont terminés. L'incident récurrent utilise le symbiote Goa'uld libre et sa chasse autonome existante, avec un groupe adaptatif volontairement limité à quatre individus et sans dépendance au pool d'opérations Tok'ra.

Le premier chargement avait conservé la DLL `0.3.38.0`, ce qui empêchait RimWorld de résoudre le nouveau type d'IncidentWorker. Le rebuild de `GateRimSG1.dll` en version `0.3.39.0` a corrigé ce déploiement obsolète sans nécessiter de révision de code supplémentaire.

## Couverture validée

- [x] `check-project-consistency.cmd` reconnaît `0.3.39-dev`, la DLL `0.3.39.0` et les 83 backstories.
- [x] Le build de `GateRimSG1.dll` réussit sur Windows.
- [x] Le jeu charge le nouvel IncidentDef et `IncidentWorker_GoauldFreeSymbioteIncursion` avec la DLL `0.3.39.0`.
- [x] Le profil faible à `300` points fait apparaître exactement un symbiote Goa'uld libre hostile.
- [x] Le profil avancé à `2600` points fait apparaître exactement quatre symbiotes regroupés près d'une bordure accessible.
- [x] Chaque symbiote recherche et poursuit de façon autonome une cible compatible.
- [x] Un contact réussi crée une seule implantation Goa'uld récente, conserve l'identité persistante du symbiote et retire uniquement le pawn ayant établi le contact.
- [x] Les trois avertissements RP ne répètent pas immédiatement la variante précédente.
- [x] L'anti-répétition des avertissements reste active après sauvegarde et rechargement.
- [x] Les symbiotes Tok'ra conservent leur comportement distinct.
- [x] L'implantation manuelle, le rituel, l'extraction et le comportement des hôtes actifs restent inchangés.
- [x] L'incident ne modifie ni la confiance Tok'ra, ni le communicateur, ni le planificateur d'opérations organiques.
- [x] `Player.log` ne contient aucune nouvelle erreur GateRim SG-1 bloquante après le rebuild.

## Régressions durables

À rejouer lorsqu'un futur jalon modifie l'incident, les symbiotes libres, l'implantation ou les règles de faction :

- éligibilité naturelle après le jour `18` uniquement avec une faction Goa'uld visible, une cible compatible et une bordure accessible ;
- seuils de `1`, `2`, `3` et `4` symbiotes depuis les points de menace, sans dépasser le plafond ;
- transfert exact de l'identité persistante lors de l'implantation ;
- absence de seconde voie d'infection propre à l'incident ;
- persistance de la dernière variante de lettre après sauvegarde/rechargement ;
- absence d'effet sur les systèmes Tok'ra et les autres voies d'implantation ou d'extraction Goa'uld ;
- refus explicite de faire évoluer silencieusement l'allégeance ou le contrôle d'un hôte dans cet incident.
