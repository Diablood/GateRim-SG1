# Validation locale finale - 0.3.67-dev

Jalon : `0.3.67-dev - Adopt develop-based branch workflow`

Branche validée : `feature/develop-branch-workflow`

Base d’intégration : `develop`, créée exactement depuis le commit ciblé par
`v0.3.66-dev`

Version de DLL validée : `0.3.67.0`

Révision locale finale : `r4`

Statut : topologie Git, documentation, cohérence, rebuild, démarrage minimal et
publication validés.

## Résultat final

La validation de la révision `r4` confirme :

- création et publication de `develop` exactement depuis le commit ciblé par le
  tag annoté `v0.3.66-dev` ;
- création de `feature/develop-branch-workflow` depuis `develop` ;
- vérification correcte des tags annotés avec `git rev-list -n 1` ;
- branche temporaire descendante de `develop` ;
- `main` inchangée ;
- ajout de la référence durable `docs/BRANCHING_WORKFLOW.md` ;
- procédures agents, reprise de contexte, publication et documentation mises à
  jour pour le flux `develop` → `feature/*` ou `fix/*` → fast-forward ;
- intégration obligatoire avec `git merge --ff-only` ;
- création du tag seulement après intégration dans `develop` ;
- publication distante des branches temporaires rendue facultative ;
- gestion de divergence, future promotion vers `1.0.0` et hotfixes stables
  documentées ;
- contrôle `./tools/check-project-consistency.cmd` entièrement réussi après
  alignement des deux métadonnées de version du wiki ;
- rebuild réussi de `GateRimSG1.dll` en version `0.3.67.0` ;
- démarrage de RimWorld jusqu’au menu principal avec `Core`, `Harmony`,
  `Biotech` et `GateRim SG-1` ;
- absence de nouveau comportement de jeu et de nouvelle erreur de chargement
  attribuable au jalon.

## Périmètre réellement modifié

Le jalon modifie uniquement :

- les procédures Git et documents de reprise ;
- les métadonnées de version publique et technique ;
- les deux lignes de version dans `docs/wiki/Home.md` et
  `docs/wiki/Content-Status.md`.

Il ne modifie aucun fichier C# fonctionnel, Def, traduction, texture, donnée de
sauvegarde, incident, mission, storyteller ou équilibrage.

## Publication validée

- commit final effectué sur `feature/develop-branch-workflow` ;
- branche intégrée dans `develop` avec `git merge --ff-only` ;
- `develop` publiée sur `origin` ;
- tag annoté final unique `v0.3.67-dev` publié ;
- tag pelé, `develop` local et `origin/develop` vérifiés sur le même commit ;
- wiki séparé synchronisé et publié pour les deux métadonnées de version ;
- `main` laissée inchangée.

## Prochaine base

Le prochain jalon doit partir de `develop` après `v0.3.67-dev` :

```text
0.3.68-dev - Add open-conflict Goa'uld pressure reduction
feature/goauld-open-conflict-pressure-reduction
```
