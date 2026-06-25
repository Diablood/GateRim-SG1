# Validation finale — 0.3.46-dev

Jalon : `0.3.46-dev - Add Goa'uld world-name generators`

Branche : `feature/goauld-world-names`

Tag de départ : `v0.3.45-dev`

Version de DLL validée : `0.3.46.0`

Révision locale finale : `r1`

Statut : validation fonctionnelle terminée ; jalon publié sous `v0.3.46-dev`.

## 1. Contrôles statiques et chargement

- [x] `git diff --check` ne signale aucune erreur.
- [x] `./tools/check-project-consistency.cmd` termine avec un code `0`.
- [x] Le rebuild forcé produit une DLL `0.3.46.0`.
- [x] RimWorld démarre sans nouvelle erreur rouge GateRim SG-1.
- [x] `SG1_NamerFactionGoauldDomain` et `SG1_NamerSettlementGoauldDomain` se chargent.
- [x] `SG1_GoauldSystemLordPrototype` ne contient plus de `fixedName` et ne référence plus les name makers pirates.

## 2. Noms des factions

- [x] L'entrée de sélection conserve son libellé générique en français ou en anglais.
- [x] Plusieurs factions générées reçoivent des noms variés.
- [x] Elles ne portent plus toutes le nom générique de la définition.
- [x] Aucun suffixe numérique technique, crochet ou jeton de grammaire n'apparaît.
- [x] Les noms décrivent des domaines, empires, cours ou puissances Goa'uld sans prétendre identifier le dirigeant généré.

## 3. Noms des colonies

- [x] Les colonies utilisent uniquement le générateur dédié.
- [x] Aucun nom pirate vanilla n'est observé.
- [x] Les types, thèmes et ordinaux sont variés.
- [x] Les formes non numérotées restent majoritaires.
- [x] Aucun suffixe technique `2`, `3` ou supérieur n'apparaît dans l'échantillon.
- [x] Les noms restent lisibles sur la carte mondiale.

## 4. Français et anglais

- [x] Le premier mot commence par une majuscule et les mots communs internes restent en minuscules.
- [x] `Porte`, `Souverain` et `Premier Serpent` conservent leur capitale lorsqu'ils fonctionnent comme titres ou noms propres.
- [x] Les accords `Premier` / `Première` sont corrects.
- [x] Les accents, apostrophes et `œ` s'affichent correctement.
- [x] Aucun texte de l'autre langue ni fragment de règle n'apparaît.

## 5. Compatibilité des sauvegardes

- [x] Une sauvegarde créée avant `0.3.46-dev` conserve ses noms sérialisés.
- [x] Les dirigeants, relations et identités de domaine restent intacts.
- [x] Aucun renommage rétroactif ni nouvelle donnée persistante n'est introduit.

## 6. Régressions Goa'uld

- [x] Chaque faction génère toujours un véritable Grand Maître Goa'uld comme dirigeant.
- [x] Les colonies conservent leurs groupes Jaffa et leur caste d'hôtes minoritaire.
- [x] La faction reste ennemie permanente du joueur.
- [x] Le raid naturel Jaffa Goa'uld reste fonctionnel.
- [x] Les raids contrôlés et l'incursion de symbiotes libres résolvent toujours une faction Goa'uld valide.
- [x] Marchands, aide militaire, quêtes, sièges et attaques préparées restent désactivés.

## 7. Constats différés

- [x] Les icônes de colonies restent des maisons vanilla uniquement différenciées par couleur ; la correction est planifiée dans `feature/faction-world-icon-overhaul`.
- [x] Les chefs de factions Jaffa libres et Goa'uld utilisent encore des noms vanilla ; deux branches distinctes sont planifiées pour traiter leurs contraintes culturelles et identitaires.

## 8. Journal

- [x] `Player.log` ne contient aucune nouvelle erreur GateRim SG-1.
- [x] Aucune erreur `RulePackDef`, `GrammarResolver` ou traduction indexée n'apparaît.
- [x] Aucune référence manquante aux deux name makers dédiés n'apparaît.

## Résultat final

Les nouveaux mondes utilisent des noms de domaines et de colonies Goa'uld dédiés et bilingues, tandis que les sauvegardes existantes restent intactes. Le changement ne modifie ni les dirigeants, ni les identités de domaine, ni les groupes de pawns, ni les raids, ni les données persistantes. La révision finale `r1` est publiée sous `v0.3.46-dev`.
