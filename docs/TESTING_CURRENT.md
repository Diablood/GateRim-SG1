# Tests courants

Jalon : `0.3.104-dev - Finalize adult symbiote mobile visuals`

Révision finale validée : `r2`
Version de DLL validée : `0.3.104.0`

## Résultat validé

- Les symbiotes adultes Goa'uld et Tok'ra partagent correctement la même famille
  directionnelle.
- Les vues nord, sud, est et ouest miroir sont cohérentes en déplacement.
- Les fichiers nord et sud ajustés manuellement ne paraissent plus plus larges
  ou plus épais que les profils est et ouest.
- Le contour noir reste visible à la taille réelle.
- `drawSize = 0.65` est validé.
- La sélection, l'inspection, la capture et la sauvegarde/recharge fonctionnent.
- L'implantation forcée Goa'uld et l'implantation volontaire Tok'ra restent
  fonctionnelles.
- La reine Goa'uld conserve son ancienne apparence.
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
  -ExpectedVersion 0.3.104-dev `
  -ExpectedBackstoryCount 83

.\tools\check-project-consistency.cmd -RequirePublicationReady

git diff --check
```

Baseline finale attendue :

```text
Final local texture families: 56
Local PNG files: 617
Local texture families: 82
Missing local references: 0
Unregistered local families: 0
Visual asset check passed.
```
