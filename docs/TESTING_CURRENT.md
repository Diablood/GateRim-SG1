# Current testing — final Tok'ra mission-object visuals

Jalon : `0.3.95-dev`
Révision validée : `r16`
Version de DLL attendue : `0.3.95.0`

## Validation fonctionnelle et visuelle

- Le module de chiffrement Tok'ra est lisible au sol et dans l'inventaire.
- Le paquet de renseignements codés possède une identité visuelle distincte.
- Le dispositif d'observation est validé comme objet portable et comme point
  déployé ; le blueprint reste vanilla et aucune texture dédiée obsolète du
  point d'observation ne subsiste.
- Le cache organique conserve sa catégorie `Item` et son fonctionnement de
  mission.
- Le nœud de contrôle du relais Goa'uld conserve sa catégorie bâtiment et
  démarre sans erreur XML.
- Le communicateur sécurisé Tok'ra est lisible comme station fixe `1×1`.
- Le marquage prioritaire de livraison est épais, entouré de noir et légèrement
  transparent sans masquer le terrain.
- Toutes les textures de sites sous
  `Textures/World/WorldObjects/Expanding/Sites` restent finales.
- Les sauvegardes, recherches, mécaniques de confiance et opérations restent
  inchangées.
- Le défaut différé d'effondrement du toit montagneux du site de relais reste
  enregistré dans `docs/KNOWN_ISSUES.md`.

## Contrôles de finalisation `r16`

Après extraction de `r15`, puis de `r16`, exécuter :

```powershell
.\build.cmd
.\tools\check-duration-formatting.cmd
.\tools\check-visual-assets.cmd
.\tools\check-project-consistency.cmd
git diff --check
```

Résultats attendus pour le contrôle visuel :

```text
Final local texture families: 40
Local PNG files: 609
Local texture families: 76
Missing local references: 0
Unregistered local families: 0
Visual asset check passed.
```

## Résultat final attendu

Le jalon modifie uniquement les visuels validés, leur documentation, les copies
wiki protégées et les métadonnées de version. Aucun comportement de mission,
équilibrage, recherche, recette ou identifiant sérialisé n'est modifié.
