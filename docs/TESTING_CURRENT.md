# Tests du jalon actif

Jalon : `0.3.20-dev - Add automated project consistency checks`

Branche attendue : `feature/project-consistency-checks`

Base attendue : `v0.3.19-dev`

Révision locale validée : `0.3.20-dev-r2`

Version de DLL attendue : `0.3.20.0`

Statut : validation fonctionnelle terminée sur `0.3.20-dev-r2` ; jalon clôturé et publié sous `v0.3.20-dev`.

## Résultat final validé

- Le contrôleur s'exécute correctement avec Windows PowerShell 5.1 après le correctif de syntaxe `r2`.
- Le chemin positif valide `0.3.20-dev`, `0.3.20.0` et `83` backstories.
- Tous les contrôles attendus affichent `[PASS]` et le code de sortie final vaut `0`.
- Une version volontairement incorrecte produit au moins un `[FAIL]` explicite et un code de sortie non nul.
- Le chemin positif repasse au vert immédiatement après le test négatif.
- Les exécutions positives et négatives ne modifient aucun fichier du dépôt.
- Le rebuild forcé produit la DLL `0.3.20.0` sans nouvelle erreur attribuable au jalon.
- RimWorld atteint le menu principal et affiche la version `0.3.20-dev` du mod.
- Le README, l'accueil du wiki et l'état du contenu affichent `83` backstories.
- Aucun fichier C#, Def, traduction, texture ou `About/ModIcon.png` n'est modifié.
- `Player.log` est propre pour le périmètre testé.
- Décision finale : conserver la révision fonctionnelle `r2` sans correctif supplémentaire.

## 1. Contrôle de cohérence positif

Commande validée :

```powershell
.	ools\check-project-consistency.cmd `
    -ExpectedVersion 0.3.20-dev `
    -ExpectedBackstoryCount 83
```

Résumé validé :

```text
Project consistency check passed.
Version: 0.3.20-dev
Assembly: 0.3.20.0
Backstories: 83
```

Le code de sortie est `0` et le dépôt reste inchangé.

## 2. Détection d'une attente incorrecte

Commande validée :

```powershell
.	ools\check-project-consistency.cmd -ExpectedVersion 0.0.0-dev
```

Résultat validé :

- message `[FAIL]` explicite ;
- code de sortie non nul ;
- aucune réécriture de fichier ;
- nouveau passage positif réussi ensuite.

## 3. Versions et pages publiques

Résultat validé :

- `About/About.xml` : `0.3.20-dev` ;
- projet et DLL : `0.3.20.0` ;
- `README.md` : `0.3.20-dev` et `83 cultural backstories` ;
- `docs/wiki/Home.md` : `0.3.20-dev` et `83` histoires ;
- `docs/wiki/Content-Status.md` : révision `0.3.20-dev` et catalogue de `83` entrées ;
- `docs/wiki/Cultural-Backstories.md` : `83` lignes entre les marqueurs contrôlés.

## 4. Rebuild et chargement

Résultat validé :

- rebuild forcé réussi ;
- DLL `0.3.20.0` ;
- menu principal atteint ;
- version du mod affichée correctement ;
- aucune nouvelle erreur GateRim SG-1 dans `Player.log`.

## 5. Périmètre et caractère en lecture seule

Résultat validé :

- aucun changement de gameplay ;
- aucun changement de Def, traduction ou texture ;
- aucun changement de `About/ModIcon.png` ;
- aucun fichier temporaire ajouté ;
- le contrôleur ne modifie jamais l'arbre de travail.

## 6. Contrôle final

```powershell
git status --short
git diff --check
```

Le dépôt ne contient que les fichiers attendus du jalon et aucune erreur d'espacement.
