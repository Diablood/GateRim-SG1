# Validation documentaire - 0.3.63-dev

Jalon : `0.3.63-dev - Consolidate project documentation`

Branche : `feature/documentation-consolidation`

Base : `v0.3.62-dev`

Version de DLL validée : `0.3.63.0`

Révision locale : `r1`

Statut : révision finale `r1` validée, puis branche, tag annoté
`v0.3.63-dev` et wiki séparé publiés.

## Test obligatoire court

1. Vérifier dans `docs/README.md` qu'une source autoritative distincte existe
   pour l'état courant, le backlog, les idées, l'historique, les tests et le
   wiki.
2. Vérifier que `docs/ROADMAP.md` contient le jalon `0.3.63-dev`, les travaux
   ouverts et les règles durables, sans les checklists des anciennes versions.
3. Vérifier dans `docs/IDEAS_TO_REVISIT.md` la conservation des pistes sur les
   reines Goa'uld et les réactions Tok'ra selon les cultures.
4. Vérifier l'absence de `Tokra-Interaction-Roadmap` dans `docs/wiki/Tokra.md`
   et `docs/wiki/_Sidebar.md`.
5. Confirmer les six suppressions listées dans `docs/PROJECT_STATE.md` et
   l'absence de suppression de Def, traduction ou texture.
6. Contrôler la réussite du rebuild `0.3.63.0`, du contrôle de cohérence, de
   l'audit des liens Markdown et de `git diff --check`.

## Limites

`docs/TESTING.md` et les fiches techniques par sous-système sont conservés sans
fusion dans cette première passe. Aucun comportement de jeu n'est modifié et
aucun test RimWorld n'est requis.

## Résultat

Le rebuild forcé `0.3.63.0`, le contrôle de cohérence, les `275` liens Markdown
locaux, les `118` cibles de la sidebar, les six suppressions documentaires et
l'absence de suppression de contenu de jeu sont validés localement.

Le mainteneur a approuvé la structure consolidée et les six suppressions. La
branche `feature/documentation-consolidation`, le tag annoté `v0.3.63-dev` et
le wiki séparé, avec suppression de `Tokra-Interaction-Roadmap.md`, ont ensuite
été publiés.
