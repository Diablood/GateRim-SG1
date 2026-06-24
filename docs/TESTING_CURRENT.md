# Validation finale — 0.3.38-dev

Jalon : `0.3.38-dev - Audit Tok'ra organic operation pool and long-term variety`

Branche : `feature/tokra-operation-pool-audit`

Tag de départ : `v0.3.37-dev`

Version de DLL validée : `0.3.38.0`

Révision locale finale : `r2`

Tag final : `v0.3.38-dev`

## Résultat final

Le contrôle de cohérence, le build Windows et la validation ciblée en jeu sont terminés. Le pool récurrent conserve huit opérations, un slot actif unique, ses délais cachés et ses poids existants, tandis que l'audit développeur vérifie désormais automatiquement sa couverture persistante, ses banques de textes et l'efficacité de l'anti-répétition.

## Couverture validée

- [x] `Persisted archetypes: 8 | resolved definitions: 8`.
- [x] `Coverage: missing=none | duplicates=none`.
- [x] Toutes les définitions actuelles sont pilotées par MissionDef.
- [x] Les paliers Wary, Neutral, Cooperative et Trusted terminent chacun `5000` tirages.
- [x] Les huit archétypes pondérés sont atteints dans chaque palier.
- [x] La simulation avec pénalité produit moins de répétitions immédiates que le même tirage sans pénalité.
- [x] Le rapport se termine par `Audit result: PASS`.
- [x] Trois offres successives de récupération de renseignements ne répètent jamais immédiatement la formulation précédente.
- [x] L'audit reste `PASS` après sauvegarde et rechargement sans opération active.
- [x] Le contrôle de cohérence reconnaît `0.3.38-dev`, la DLL `0.3.38.0` et les 83 backstories.
- [x] Le build de `GateRimSG1.dll` réussit sur Windows.
- [x] `Player.log` ne contient aucune nouvelle erreur GateRim SG-1 bloquante.

## Régressions durables

À rejouer lorsqu'un futur jalon modifie le planificateur, les Defs de mission, les paliers de confiance ou la persistance :

- tirage naturel après échéance cachée et refus de remplacer une opération déjà active ;
- nouvelle échéance après réussite, échec ou offre ignorée ;
- filtrage d'une mission temporairement indisponible avant le tirage ;
- perte et restauration du communicateur sans annuler une opération engagée ;
- conservation d'un seul slot global après sauvegarde/rechargement ;
- absence d'exposition des poids, délais, historiques ou futurs archétypes dans le communicateur normal ;
- maintien d'au moins deux variantes d'offre et de conclusion pour tout nouvel archétype récurrent.
