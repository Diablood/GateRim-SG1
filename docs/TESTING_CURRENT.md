# Tests courants

Jalon : `0.3.101-dev - Finalize Jaffa helmet gizmo and manual toggle`

Révision validée : `r4`
Version de DLL validée : `0.3.101.0`

## Objet du test

Le jalon finalise le gizmo du casque Jaffa et simplifie son contrôle en une
bascule manuelle persistante. La révision `r4` est documentaire uniquement :
elle enregistre les validations fonctionnelles et techniques déjà réussies.

## Contrôles validés

Depuis la racine du dépôt :

```powershell
.\build.cmd "D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"

.\tools\check-duration-formatting.cmd
.\tools\test-documentation-consistency-guards.cmd
.\tools\check-documentation-consistency.cmd
.\tools\check-visual-assets.cmd
.\tools\check-project-consistency.cmd `
  -ExpectedVersion 0.3.101-dev `
  -ExpectedBackstoryCount 83

.\tools\check-project-consistency.cmd -RequirePublicationReady

git diff --check
```

Résultats validés :

- build Release `0.3.101.0` réussi ;
- audit des durées réussi ;
- fixtures négatives des garde-fous documentaires réussies ;
- audit documentaire normal réussi ;
- doublons `0.3.101-dev` supprimés du changelog et des tests durables ;
- audit visuel réussi avec `610` PNG, `77` familles et `45` familles finales ;
- contrôle global réussi avec `83` backstories ;
- contrôle documentaire de publication réussi ;
- aucun défaut d'espace ou de fin de ligne détecté.

## Validation RimWorld ciblée

La validation fonctionnelle confirme :

- `Déployer casque` lorsque le casque est rétracté ;
- `Rétracter casque` lorsque le casque est déployé ;
- application immédiate de chaque action ;
- absence de changement pendant l'enrôlement ou le désenrôlement ;
- persistance des deux positions après sauvegarde et rechargement ;
- isolation correcte des paires de casques ordinaire et officier ;
- inspection limitée à la position réelle ;
- affichage transparent et lisible du gizmo cobra.

## Compatibilité et limites confirmées

- les anciennes valeurs `Automatic` sont converties selon le Def physique déjà
  enregistré dans la sauvegarde ;
- l'ancien composant de mise à jour reste résolvable mais n'effectue plus de
  synchronisation périodique ;
- les textures portées des casques et armures restent dans leurs lots visuels
  différés ;
- les valeurs d'armure, couvertures, recettes, recherches, loadouts et
  identifiants sauvegardés restent inchangés.
