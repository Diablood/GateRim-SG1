# Current validation — 0.3.32-dev

Jalon : `0.3.32-dev - Add Tok'ra cipher-module study and research`
Version de DLL validée : `0.3.32.0`
Dernière révision locale validée : `r2`
Branche publiée : `feature/tokra-artifact-study-research`
Tag final : `v0.3.32-dev`
Base : `v0.3.31-dev`

## Résultat final

Le contrôle de cohérence, le build local et les tests ciblés en jeu sont validés. Le jalon est clôturé et publié sous le tag final unique `v0.3.32-dev`.

## Couverture validée

- trois sessions d'analyse au banc de recherche sur le véritable module suivi ;
- progression `0/3` à `3/3` conservée après sauvegarde/rechargement ;
- refus d'une copie créée séparément avec le même `ThingDef` ;
- module absent des dialogues de commerce tant que l'analyse est incomplète ;
- conservation du module pendant les deux premières sessions ;
- démantèlement physique pendant la troisième session ;
- état d'analyse conservé après disparition de l'objet ;
- recherche `Communications sécurisées Tok'ra` verrouillée par l'analyse et par `Électricité` ;
- remplacement automatique d'un module perdu après un délai caché de `2–8` jours ;
- progression déjà acquise conservée par le module de remplacement ;
- un seul remplacement et une seule lettre après sauvegarde/rechargement ;
- annulation du remplacement si le module original réapparaît avant l'échéance ;
- validation développeur cohérente de la mission d'introduction avec création du véritable module lorsque nécessaire ;
- recherche déjà terminée considérée comme autoritaire pour les scénarios personnalisés, starters, sauvegardes modifiées et outils développeur ;
- fermeture automatique de l'arc, analyse satisfaite et absence d'objet inutile dans ces états avancés ;
- communicateur existant et six opérations Tok'ra récurrentes inchangés ;
- absence de nouvelle erreur de chargement, recherche, travail ou traduction constatée pendant les tests.

## Précision sur le remplacement

La récupération après perte fonctionne automatiquement. Après détection de l'absence du module, le système programme et persiste une échéance aléatoire de `2–8` jours en jeu. L'action développeur `Tok'ra study: make replacement due` sert uniquement à accélérer ce délai pendant les tests.

## Régressions durables

- toujours vérifier le `ThingID` suivi plutôt que le seul `ThingDef` ;
- ne jamais rendre le module vendable avant la fin de l'analyse ;
- ne pas laisser le module physique survivre à la troisième session ;
- ne jamais réinitialiser les sessions déjà terminées lors d'un remplacement ;
- ne générer qu'un seul module de remplacement et annuler son échéance si l'original réapparaît ;
- ne jamais générer d'offre, de site ou de module lorsque `SG1_TokraSecureCommunications` est déjà terminée ;
- conserver la réconciliation au chargement pour les starters, scénarios personnalisés et sauvegardes modifiées ;
- revalider tout ce flux lors de l'ajout du prérequis de construction du communicateur et du verrouillage des opérations récurrentes.

## Outils développeur conservés

- `Tok'ra study: show state`
- `Tok'ra study: finish module analysis`
- `Tok'ra study: destroy tracked module`
- `Tok'ra study: make replacement due`
- `Tok'ra study: reset module analysis`
- `Tok'ra intro: recover key artifact`
