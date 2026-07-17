# Current testing — final Goa'uld open-conflict battlefield icon

Jalon : `0.3.99-dev`
Révision visuelle validée : `r2`
Révision documentaire : `r3`
Version de DLL attendue : `0.3.99.0`

## Validation fonctionnelle et visuelle

- `SG1_GoauldOpenConflictBattlefield` mesure `128×128`.
- Le PNG possède une transparence extérieure réelle.
- L'icône utilise un rendu plat, sans ombrage pseudo-3D.
- Les détails restent limités et cohérents avec les autres icônes d'événements.
- Les deux armes Goa'uld opposées et l'impact central orange restent lisibles
  sur la carte mondiale.
- Le contour sombre est net et aucune bordure carrée n'est visible.
- Le site, sa durée, les factions concernées, la génération de carte et
  l'intervention facultative restent inchangés.
- La copie wiki doit rester byte-identique au PNG de gameplay.

## Contrôles de finalisation `r3`

```powershell
.\build.cmd
.\tools\check-duration-formatting.cmd
.\tools\check-visual-assets.cmd
.\tools\check-project-consistency.cmd
git diff --check
```

Résultats attendus pour le contrôle visuel :

```text
Final local texture families: 44
Local PNG files: 610
Local texture families: 77
Missing local references: 0
Unregistered local families: 0
Visual asset check passed.
```
