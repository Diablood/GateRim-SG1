# Validation finale — 0.3.45-dev

Jalon : `0.3.45-dev - Add Free Jaffa world-name generators`

Branche : `feature/free-jaffa-world-names`

Tag de départ : `v0.3.44-dev`

Version de DLL validée : `0.3.45.0`

Révision locale finale : `r3`

Statut : validation fonctionnelle terminée ; jalon publié sous `v0.3.45-dev`.

## 1. Contrôles statiques et chargement

- [x] `git diff --check` ne signale aucune erreur.
- [x] `./tools/check-project-consistency.cmd` termine avec un code `0`.
- [x] RimWorld démarre sans nouvelle erreur rouge GateRim SG-1.
- [x] Les deux RulePackDefs dédiés se chargent.
- [x] `SG1_FreeJaffa` ne contient plus de `fixedName` ni de référence aux name makers outlander.

## 2. Noms de factions

- [x] L'entrée de sélection reste clairement libellée `Jaffa libres` en français et `Free Jaffa` en anglais.
- [x] Plusieurs factions générées reçoivent des noms distincts et nettement variés.
- [x] Elles ne portent plus toutes le nom générique `Jaffa libres`.
- [x] Aucun jeton de grammaire ou suffixe numérique technique n'apparaît dans l'échantillon.
- [x] Les noms restent cohérents avec des alliances, conseils, clans et communautés rebelles Jaffa.

Exemples validés par la grammaire :

```text
Alliance des clans libres
Conseil de la résistance Jaffa
Fraternité des hôtes libérés
Pacte contre les Maîtres
```

## 3. Casse française des colonies

- [x] Le premier mot commence par une majuscule.
- [x] Les mots génériques suivants restent en minuscules : `Refuge des affranchis`, `Citadelle des clans libres`.
- [x] Le type de colonie après un ordinal reste en minuscule : `Premier refuge`, `Deuxième cité`.
- [x] Les titres ou noms propres justifiés conservent leur capitale, notamment `Maîtres`, `Jaffa` et `Porte` lorsqu'elle désigne la Porte.
- [x] Les accords `Premier` / `Première` restent corrects.

## 4. Variété des colonies

- [x] Les types, thèmes et ordinaux sont variés.
- [x] Les formes non numérotées restent majoritaires.
- [x] Aucun suffixe technique `2`, `3` ou supérieur n'est apparu dans l'échantillon de validation.
- [x] Aucun nom outlander générique n'apparaît.

La grammaire offre `600` résultats complets sans registre persistant d'unicité. Une collision reste théoriquement possible, mais elle n'est plus systématique lors de la génération normale.

## 5. Génération bilingue

- [x] Les factions et colonies utilisent leurs règles anglaises avec l'anglais actif.
- [x] Les traductions françaises indexées se chargent avec le français actif.
- [x] Aucun texte de l'autre langue ni fragment de règle n'apparaît.
- [x] Les formes ordinales restent naturelles et lisibles.

## 6. Compatibilité des sauvegardes

- [x] Aucun renommage rétroactif ni nouvelle donnée persistante n'est introduit.
- [x] Les noms déjà sérialisés restent hors du flux de génération modifié.
- [x] Les relations, dirigeants et colonies ne reçoivent aucune migration liée à ce jalon.

## 7. Régressions Free Jaffa

- [x] Une colonie reste visitable et commerçante.
- [x] Le convoi de ravitaillement fonctionne.
- [x] Les visiteurs pacifiques restent générables.
- [x] Une faction alliée peut toujours fournir l'aide militaire vanilla.
- [x] Quêtes, raids naturels, sièges et attaques préparées restent désactivés.

## 8. Journal

- [x] `Player.log` ne contient aucune nouvelle erreur GateRim SG-1.
- [x] Aucune erreur `RulePackDef`, `GrammarResolver` ou traduction indexée n'apparaît.
- [x] Aucune référence manquante aux deux name makers dédiés n'apparaît.

## Résultat final

Les nouveaux mondes génèrent des factions Jaffa libres distinctement nommées et des colonies bilingues à la casse naturelle. Les noms existants ne sont pas migrés, les systèmes commerciaux et diplomatiques restent inchangés, et la révision finale `r3` est publiée sous `v0.3.45-dev`.
