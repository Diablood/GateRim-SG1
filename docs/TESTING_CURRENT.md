# Tests courants

Jalon : `0.3.105-dev - Finalize Goa'uld queen mobile visuals`

Révision finale validée : `r2`
Version de DLL validée : `0.3.105.0`

## Résultat validé

- La reine Goa'uld utilise correctement sa famille directionnelle dédiée.
- Les vues nord, sud, est et ouest miroir sont cohérentes en déplacement.
- Les fichiers nord et sud ajustés manuellement ne paraissent plus plus larges
  ou plus épais que le profil est/ouest.
- L'abdomen reproducteur, le cou articulé, la tête et la crête dorsale restent
  lisibles à la taille réelle.
- `drawSize = 0.95` est validé.
- La sélection, l'inspection, la capture et la sauvegarde/recharge fonctionnent.
- La production manuelle d'un symbiote immature et son délai persistant restent
  fonctionnels.
- La famille adulte Goa'uld/Tok'ra reste inchangée.
- Aucun nouveau problème pertinent de texture, XML ou `Graphic_Multi` n'est
  observé.

## Contrôles finaux

```powershell
.\build.cmd "D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"

.\tools\check-duration-formatting.cmd
.\tools\test-documentation-consistency-guards.cmd
.\tools\check-documentation-consistency.cmd
.\tools\check-visual-assets.cmd

.\tools\check-project-consistency.cmd `
  -ExpectedVersion 0.3.105-dev `
  -ExpectedBackstoryCount 83

.\tools\check-project-consistency.cmd -RequirePublicationReady

git diff --check
```

Baseline finale attendue :

```text
Final local texture families: 57
Local PNG files: 619
Local texture families: 82
Missing local references: 0
Unregistered local families: 0
Visual asset check passed.
```
