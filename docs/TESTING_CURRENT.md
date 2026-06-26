# Validation finale — 0.3.47-dev

Jalon : `0.3.47-dev - Add Free Jaffa faction-leader names`

Branche : `feature/free-jaffa-faction-leader-names`

Tag de départ : `v0.3.46-dev`

Version de DLL validée : `0.3.47.0`

Révision locale finale : `r5`

Statut : validation fonctionnelle ciblée terminée ; jalon publié sous `v0.3.47-dev`.

## 1. Contrôles statiques et chargement

- [x] `git diff --check` ne signale aucune erreur.
- [x] `./tools/check-project-consistency.cmd` termine avec un code `0` avant publication.
- [x] La DLL chargée reste en version `0.3.47.0`.
- [x] RimWorld démarre sans l'erreur XML `chanceToUseNameMaker` rencontrée sous `r3`.
- [x] `SG1_NamerPawnFreeJaffa` et les champs `nameMaker` / `nameMakerFemale` sont résolus.

## 2. Nouveau monde — contrôle avant démarrage

Un monde entièrement nouveau a été généré avec plusieurs factions Jaffa libres et les dirigeants ont été contrôlés avant le choix de la tuile de départ.

- [x] Chaque faction contrôlée possède un chef généré.
- [x] Les chefs utilisent immédiatement des noms Jaffa libres plutôt que des noms humains vanilla.
- [x] Les noms complets contiennent un nom personnel et un nom de clan.
- [x] Les noms observés sont variés entre les factions.
- [x] Aucun nom vide, fragment de règle ou suffixe technique inattendu n'apparaît.
- [x] La génération du monde se termine sans `Could not get new name (first rule pack: SG1_NamerPawnFreeJaffa)`.
- [x] Les noms de factions et de colonies validés dans `0.3.45-dev` restent inchangés.

## 3. Structure technique validée

- [x] Le résultat du RulePack fournit des champs de prénom et de clan non vides au `NameTriple`.
- [x] Le nom personnel est répété comme surnom interne afin de rester le nom court du pawn.
- [x] Les `576` noms personnels et `24` noms de clan donnent `13 824` combinaisons formelles possibles.
- [x] Le repli tardif par faction propriétaire et le registre persistant de dirigeants de `r1` / `r2` restent retirés.
- [x] Aucun nouveau champ de sauvegarde propre à `0.3.47-dev` n'est créé.

## 4. Limites et régressions durables

Les points suivants n'ont pas été signalés comme exécutés séparément pendant le test ciblé final. Ils restent dans `docs/TESTING.md` comme régressions à rejouer lors de toute modification future du générateur ou du PawnKind :

- remplacement d'un chef Jaffa libre après la création du monde ;
- stabilité explicite après sauvegarde/rechargement ;
- contrôle détaillé des gardes ordinaires utilisant le même PawnKind ;
- nouvelle passe complète sur commerce, visiteurs pacifiques et aide militaire.

Le chemin natif étant porté par le PawnKind, un dirigeant de remplacement généré avec `SG1_FreeJaffaGuard` utilise le même name maker, mais ce cas n'est pas présenté comme un test manuel distinct de la validation finale.

## 5. Hors périmètre confirmé

- [x] Les chefs Goa'uld restent réservés à `feature/goauld-system-lord-leader-names`.
- [x] La visibilité et la présence mondiale Tok'ra restent réservées à `feature/tokra-world-faction-selection-audit`.
- [x] Les icônes mondiales restent provisoirement vanilla et sont réservées à `feature/faction-world-icon-overhaul`.
- [x] Les chefs de factions vanilla ne sont pas ciblés par le nouveau RulePack.

## Résultat final

Les nouveaux chefs de factions Jaffa libres possèdent un nom culturel formel et valide dès leur génération dans l'écran de création du monde. Plusieurs factions peuvent générer leurs chefs sans épuiser le validateur d'unicité de RimWorld. La révision finale `r5` est publiée sous le tag unique `v0.3.47-dev`.
