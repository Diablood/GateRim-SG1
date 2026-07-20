# Tests courants

Jalon : `0.3.102-dev - Finalize remaining non-directional visual families`

Révision validée : `r10`
Version de DLL validée : `0.3.102.0`

## Objet du test

Le jalon finalise les surfaces non directionnelles encore ouvertes pour les
armes, projectiles et équipements Jaffa. Les rendus portés directionnels restent
explicitement différés.

## Contrôles validés

Depuis la racine du dépôt :

```powershell
.\build.cmd "D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"

.\tools\check-duration-formatting.cmd
.\tools\test-documentation-consistency-guards.cmd
.\tools\check-documentation-consistency.cmd
.\tools\check-visual-assets.cmd
.\tools\check-project-consistency.cmd `
  -ExpectedVersion 0.3.102-dev `
  -ExpectedBackstoryCount 83

.\tools\check-project-consistency.cmd -RequirePublicationReady

git diff --check
```

Résultats validés :

- build Release `0.3.102.0` réussi ;
- audit des durées réussi ;
- fixtures négatives des garde-fous documentaires réussies ;
- audit documentaire normal réussi ;
- audit visuel réussi avec `613` PNG, `80` familles et `54` familles finales ;
- contrôle global réussi avec `83` backstories ;
- contrôle documentaire de publication réussi ;
- aucun défaut d'espace ou de fin de ligne détecté.

## Validation RimWorld ciblée

La validation fonctionnelle confirme :

- bolas réutilisables et son `Bow_Small` valide ;
- Zat'nik'tel, Ma'Tok et projectiles associés visibles sans texture manquante ;
- vitesse du projectile Ma'Tok à `80` ;
- projectile hypodermique Tok'ra visible et son énergétique
  `Shot_ChargeRifle` ;
- icônes au sol et en inventaire validées pour les armures légère, lourde et
  officier ;
- icônes validées pour les casques déployés standard et officier ;
- gantelets, bottes et ceinture validés à `drawSize = 0.75` ;
- sous-armure, pantalon et ceinture équipables avec le reste du set ;
- sauvegarde et rechargement validés ;
- absence de damier intégré et vraie transparence extérieure ;
- copies wiki identiques aux PNG de gameplay et concept art affiché sur les pages Jaffa.

## Compatibilité et limites confirmées

- les chemins et DefNames historiques des équipements existants restent stables ;
- les nouveaux DefNames sont `SG1_JaffaUnderArmor`, `SG1_JaffaPants` et
  `SG1_JaffaArmorBelt` ;
- les textures portées directionnelles ne sont pas déclarées finales par ce
  jalon ;
- les loadouts des factions et missions ne sont pas encore élargis ;
- la logique déployée/rétractée des casques reste inchangée ;
- la synchronisation du wiki séparé est requise après mise à jour des pages source.