# Current testing — final Tok'ra hypodermic rifle visual

Jalon : `0.3.97-dev`
Révision visuelle validée : texture locale finale
Révision documentaire : `r3`
Version de DLL attendue : `0.3.97.0`

## Validation fonctionnelle et visuelle

- `SG1_TokraHypodermicRifle` affiche la texture finale simplifiée.
- Le fichier source est horizontal, conformément au rendu attendu pour une arme
  longue RimWorld.
- L'arme est correctement alignée lorsqu'elle est tenue.
- Au sol, elle conserve la légère rotation aléatoire héritée des armes vanilla.
- La silhouette, le canon et les modules cyan restent lisibles en `128×128`,
  y compris sous un zoom Camera+ plus important.
- Le fond est réellement transparent et aucun damier n'est inclus dans le PNG.
- Les douze charges, la consommation à chaque tir, le projectile hypodermique,
  la neutralisation, la livraison et la disparition après la dernière charge
  restent inchangés.
- `SG1_TokraHypodermicDart` reste une famille temporaire indépendante.

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
Final local texture families: 43
Local PNG files: 610
Local texture families: 77
Missing local references: 0
Unregistered local families: 0
Visual asset check passed.
```
