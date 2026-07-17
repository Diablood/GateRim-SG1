# Current testing — final tretonin dose visual

Jalon : `0.3.98-dev`
Révision visuelle validée : image finale fournie par le mainteneur
Révision documentaire : `r2`
Version de DLL attendue : `0.3.98.0`

## Validation fonctionnelle et visuelle

- `SG1_TretoninDose` affiche l'ampoule médicale finale.
- Le PNG mesure `128×128` et possède une transparence extérieure réelle.
- L'objet utilise un contour sombre suffisamment prononcé.
- Le liquide cyan reste lisible au sol, en stockage et dans l'inventaire.
- Le cadrage utilise presque toute la texture ; la petite taille en jeu reste
  gérée par le `drawSize` existant.
- La limite de pile, la masse, la recette, l'administration médicale et la
  substitution d'une journée restent inchangées.
- La copie wiki doit rester byte-identique au PNG de gameplay.

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
Final local texture families: 44
Local PNG files: 610
Local texture families: 77
Missing local references: 0
Unregistered local families: 0
Visual asset check passed.
```
