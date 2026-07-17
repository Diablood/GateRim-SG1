# Tests courants

Jalon : `0.3.100-dev - Restore documentation consistency and add publication safeguards`

Révision validée : `r8`
Version de DLL validée : `0.3.100.0`

## Objet du test

Ce jalon ne modifie aucun comportement de jeu. Il restaure les documents
historiques manquants et rend leur cohérence automatiquement vérifiable avant
chaque publication.

## Contrôles validés

Depuis la racine du dépôt :

```powershell
.\build.cmd "D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"

.\tools\check-duration-formatting.cmd
.\tools\test-documentation-consistency-guards.cmd
.\tools\check-documentation-consistency.cmd
.\tools\check-visual-assets.cmd
.\tools\check-project-consistency.cmd `
  -ExpectedVersion 0.3.100-dev `
  -ExpectedBackstoryCount 83

.\tools\check-project-consistency.cmd -RequirePublicationReady

git diff --check
```

Résultats validés :

- build Release `0.3.100.0` réussi ;
- audit des durées réussi ;
- fixtures négatives des garde-fous réussies ;
- audit documentaire normal réussi ;
- audit documentaire de publication réussi ;
- registre visuel inchangé à `610` PNG, `77` familles et `44` familles finales ;
- contrôle global réussi avec `83` backstories ;
- aucun défaut d'espace ou de fin de ligne détecté.

## Validation RimWorld ciblée

Le démarrage ciblé est validé avec la ligne suivante dans `Player.log` :

```text
<color=#D9B44A>[GateRim SG-1]</color> Version 0.3.100.0 loaded.
```

Le filtre ciblé ne retourne aucune exception ni erreur propre à GateRim SG-1.
Il retourne également le résumé global de RimWorld indiquant six erreurs dans
les données de traduction française. Ce résumé n'identifie pas GateRim SG-1 et
ce jalon ne modifie aucun fichier de traduction.

## Limites confirmées

- aucun PNG n'est ajouté, supprimé ou remplacé ;
- aucun Def, texte de gameplay, comportement C# ou identifiant sauvegardé ne
  change ;
- le prochain travail visuel reste l'icône unique
  `UI/Commands/SG1_JaffaHelmetMode`.
