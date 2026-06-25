# Validation finale — 0.3.42-dev

Jalon : `0.3.42-dev - Separate advanced diagnostics from developer actions`

Branche publiée : `feature/debug-command-visibility-audit`

Tag de départ : `v0.3.41-dev`

Tag final : `v0.3.42-dev`

Version de DLL validée : `0.3.42.0`

Révision locale finale : `r2`

## Validation acquise sur `r1`

- [x] `git diff --check` et `check-project-consistency.cmd` validés.
- [x] Build local de `GateRimSG1.dll` version `0.3.42.0` validé.
- [x] Matrice jeu normal / option avancée / mode développeur validée.
- [x] Les rapports avancés restent informatifs et les commandes modifiant la partie restent réservées au mode développeur.
- [x] Les interactions joueur normales, la sauvegarde/rechargement et `Player.log` ne présentent pas de régression.

## Validation finale `r2` — découverte progressive du menu du communicateur

### Confiance inférieure au palier fiable

- [x] L'action de consultation de l'état du canal reste visible avec un communicateur alimenté.
- [x] Une interaction liée à une opération organique déjà proposée ou active reste visible lorsqu'elle existe.
- [x] Les demandes réservées au palier fiable sont totalement absentes : ouverture du canal, mission discrète, diversion, évaluation tactique, soutien médical et cache médicale.
- [x] Aucune ligne grisée `Confiance Tok'ra insuffisante` ne révèle ces futures actions.

### Palier fiable atteint

- [x] Les demandes réservées au palier fiable apparaissent dans le menu contextuel.
- [x] Un communicateur non alimenté conserve les actions visibles mais grisées avec la raison d'alimentation.
- [x] Une absence de menace, de patient ou un délai actif conserve l'action concernée visible et grisée avec sa raison normale.
- [x] Les restrictions propres au colon sélectionné, notamment capacité, accès ou réservation, restent visibles et grisées au lieu de masquer l'action.
- [x] Une action disponible crée toujours le job habituel et ne se déclenche pas instantanément.

### Retour sous le palier fiable et persistance

- [x] Lorsque la confiance repasse sous le palier fiable, les demandes sensibles disparaissent du prochain menu contextuel.
- [x] La sauvegarde et le rechargement conservent une visibilité cohérente dans les deux états de confiance.
- [x] La simple ouverture du menu ne modifie aucune mission, opération, confiance ou donnée persistante.
- [x] `Player.log` ne contient aucune nouvelle erreur GateRim SG-1.

## Résultat final

La révision `r2` est validée. Le jalon est clôturé et publié sous le tag final unique `v0.3.42-dev`. Le dépôt principal et le wiki séparé sont synchronisés.

## Limites conservées

- L'option avancée reste un outil de diagnostic en lecture seule.
- Les commandes qui modifient artificiellement la partie exigent le mode développeur RimWorld.
- Les actions joueur normales conservent leurs règles existantes.
- Aucun identifiant de sauvegarde, Def, équilibrage ou format persistant n'a été modifié.
