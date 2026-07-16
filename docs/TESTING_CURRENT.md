# Current testing — final Prim'ta larval item visuals

Jalon : `0.3.96-dev`
Révision validée : `r1`
Révision documentaire : `r2`
Version de DLL attendue : `0.3.96.0`

## Validation fonctionnelle et visuelle

- `SG1_PrimtaLarva` affiche la texture finale longue, pâle et développée.
- `SG1_ImmaturePrimtaSymbiote` affiche la texture finale plus petite et
  recourbée après correction de son `texPath`.
- Les deux PNG sont transparents, centrés et lisibles au sol et dans
  l'inventaire.
- Les deux stades restent visuellement apparentés sans ressembler à des
  symbiotes Goa'uld adultes cuirassés.
- L'incubation, la conservation, le stockage, la détérioration, les températures,
  l'implantation et la production de trétonine restent inchangés.
- Les pawns `SG1_GoauldSymbiote`, `SG1_TokraSymbiote` et `SG1_GoauldQueen`
  ne font pas partie de ce jalon.

## Contrôles de finalisation `r2`

```powershell
.\build.cmd
.\tools\check-duration-formatting.cmd
.\tools\check-visual-assets.cmd
.\tools\check-project-consistency.cmd
git diff --check
```

Résultats attendus pour le contrôle visuel :

```text
Final local texture families: 42
Local PNG files: 610
Local texture families: 77
Missing local references: 0
Unregistered local families: 0
Visual asset check passed.
```
