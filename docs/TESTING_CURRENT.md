# Validation finale — 0.3.48-dev

Jalon : `0.3.48-dev - Add Goa'uld System Lord leader names`

Branche : `feature/goauld-system-lord-leader-names`

Tag de départ : `v0.3.47-dev`

Version de DLL validée : `0.3.48.0`

Révision locale finale : `r1`

Statut : génération culturelle des noms validée ; jalon publié sous `v0.3.48-dev`.

## 1. Contrôles statiques et chargement

- [x] `git diff --check` ne signale aucune erreur avant publication.
- [x] `./tools/check-project-consistency.cmd` termine avec un code `0` après le rebuild.
- [x] La DLL chargée porte la version `0.3.48.0`.
- [x] RimWorld atteint la génération du monde sans nouvelle erreur XML, `RulePackDef` ou `PawnKindDef` liée à `SG1_NamerPawnGoauldSystemLord`.

## 2. Nouveau monde — contrôle avant le choix de la tuile

Un monde entièrement nouveau a été généré avec plusieurs factions Goa'uld et leurs dirigeants ont été contrôlés avant le choix de la tuile de départ.

- [x] Chaque faction contrôlée possède un Grand Maître généré.
- [x] Chaque chef affiche immédiatement un nom culturel Goa'uld en deux parties.
- [x] Les noms observés sont variés entre les factions.
- [x] Aucun nom humain vanilla, jeton de grammaire ou suffixe technique inattendu n'apparaît dans l'échantillon validé.
- [x] La génération ne produit pas l'échec `Could not get new name (first rule pack: SG1_NamerPawnGoauldSystemLord)`.
- [x] Les noms de domaines et de colonies restent indépendants du nom du chef.

## 3. Structure technique publiée

- [x] `SG1_GoauldSystemLordHost` utilise le name maker natif pour les deux genres.
- [x] Le RulePack fournit un prénom, un surnom explicite et un nom de maison non vides au `NameTriple`.
- [x] Les `575` noms personnels et `24` noms de maison produisent `13 800` combinaisons formelles possibles.
- [x] Aucun Grand Maître canon n'est ajouté au pool.
- [x] Aucun nouveau champ de sauvegarde propre au jalon n'est créé.

## 4. Régressions durables non présentées comme tests séparés

Les points suivants n'ont pas été signalés comme exécutés séparément pendant le test ciblé final. Ils restent dans `docs/TESTING.md` et dans la documentation technique comme contrôles à rejouer lors de toute modification future de l'identité Goa'uld :

- stabilité explicite du nom visible après démarrage, sauvegarde et rechargement ;
- inspection de l'alignement entre le nom visible et `symbioteName` ;
- contrôle d'un `hostName` humain distinct et non vide ;
- restauration du nom d'hôte après une extraction prise en charge ;
- conservation du nom Goa'uld par le symbiote extrait ;
- génération d'un dirigeant de remplacement ;
- régressions détaillées des chefs Jaffa libres, hôtes Goa'uld ordinaires, raids et diplomatie.

## 5. Hors périmètre confirmé

- [x] Les noms de domaines et de colonies ne sont pas dérivés du chef.
- [x] Les icônes mondiales restent provisoirement vanilla et sont réservées à `feature/faction-world-icon-overhaul`.
- [x] La visibilité et la présence mondiale Tok'ra restent réservées à `feature/tokra-world-faction-selection-audit`.
- [x] Aucun Grand Maître canon, nouveau PawnKind, incident, commerce ou règle diplomatique n'est ajouté.

## Résultat final

Les nouveaux Grands Maîtres Goa'uld possèdent un nom culturel formel dès leur génération dans l'écran de création du monde. La révision finale `r1` est publiée sous le tag unique `v0.3.48-dev`. Les contrôles approfondis de double identité et d'extraction restent conservés comme couverture durable sans être présentés comme des tests manuels distincts déjà exécutés.
