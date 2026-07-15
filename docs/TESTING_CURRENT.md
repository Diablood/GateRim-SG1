# Current testing — final Goa'uld hand-device visuals

Jalon : `0.3.94-dev`
Révision validée : `r1`
Version de DLL validée : `0.3.94.0`

## Validation fonctionnelle et visuelle

- Le kara kesh final est lisible, centré et transparent au sol, dans
  l'inventaire, dans l'inspection et lorsqu'il est équipé.
- Le bracelet de guérison final conserve un noyau orange et une armature dorée
  lisibles sans fond blanc, halo rectangulaire ni découpe visible.
- Les deux fichiers restent des PNG transparents `128×128` sur leurs chemins
  d'apparel historiques.
- Le bouclier, l'onde cinétique, l'attaque neurale et le maintien paralysant du
  kara kesh restent fonctionnellement inchangés.
- La stabilisation, la guérison limitée, la fatigue, le refroidissement et
  l'utilisation hostile du bracelet restent fonctionnellement inchangés.
- Les porteurs non éligibles ne gagnent aucun pouvoir actif.
- La sauvegarde/recharge et l'équipement naturel des Grands Maîtres restent
  conformes.
- Aucun nouveau problème pertinent de texture, XML ou C# n'a été observé pendant
  le test ciblé.

## Contrôles de finalisation `r2`

Après extraction du paquet de finalisation, exécuter :

```powershell
.\build.cmd
.\tools\check-duration-formatting.cmd
.\tools\check-visual-assets.cmd
.\tools\check-project-consistency.cmd
git diff --check
```

Résultats attendus pour le contrôle visuel :

```text
Final local texture families: 33
Local PNG files: 606
Local texture families: 73
Missing local references: 0
Unregistered local families: 0
Visual asset check passed.
```

## Résultat final attendu

Le jalon ne change que les deux visuels d'objet et leur documentation. Les Defs,
les sauvegardes, les recherches, les recettes, l'équilibrage et tous les
comportements publiés restent inchangés.
