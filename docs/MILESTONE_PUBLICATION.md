# Procédure de validation et publication d’un jalon

Ce document est la référence détaillée lorsqu’un jalon GateRim SG-1 est validé
localement. Le modèle de branches et la séquence courte sont définis dans
[`BRANCHING_WORKFLOW.md`](BRANCHING_WORKFLOW.md).

## Format obligatoire des commandes remises au mainteneur

Pour un jalon concret, fournir des commandes courtes, séquentielles et directement
copiables. La version, la branche, le tag, les messages de commit, les chemins et
le nom du ZIP doivent déjà être remplacés par leurs valeurs réelles.

La séquence normale ne doit pas utiliser :

- de variables PowerShell comme `$tagCommit`, `$modRepo` ou `$wikiRepo` ;
- de bloc `if`, de `throw` ou d’autre conditionnelle ;
- de `Push-Location` ou `Pop-Location` ;
- de script de publication temporaire lorsqu’une suite de commandes simples
  suffit.

Les marqueurs `<version>` et `<nom-du-jalon>` ne sont autorisés que dans ce
document générique. Ils doivent disparaître des commandes fournies pour un vrai
jalon.

La publication doit être présentée dans cet ordre lisible :

1. commit du jalon ;
2. intégration dans `develop` ;
3. tag final ;
4. synchronisation du wiki, seulement lorsqu’elle est nécessaire.

## 1. Annoncer la validation locale

Exemple :

```text
Le jalon 0.3.67-dev est validé après r1.
```

Le suffixe `rN` désigne uniquement une itération locale de test. Il ne doit pas
apparaître dans le commit final ni dans le tag publié.

## 2. Vérifier la branche active et sa base

Le travail courant doit être sur une branche temporaire dédiée :

```powershell
git branch --show-current
```

Résultat attendu :

```text
feature/<nom-du-jalon>
```

ou, pour une correction ciblée :

```text
fix/<nom-du-correctif>
```

Vérifier simplement la base :

```powershell
git fetch origin --tags
git rev-parse develop
git rev-parse origin/develop
git merge-base --is-ancestor develop HEAD
```

La dernière commande doit terminer avec un code de sortie `0`. Ne jamais
commencer un jalon ordinaire depuis `main` ou depuis une ancienne branche de
fonctionnalité.

## 3. Verrou documentaire avant le commit final

### Livraison des fichiers

Les ZIP de révision locale et de finalisation contiennent uniquement les fichiers
ajoutés ou modifiés depuis la révision précédemment livrée. Une archive complète
du dépôt est réservée à une récupération explicitement demandée.

Chaque chemin inclus est un fichier complet prêt à remplacer sa version de
travail. Une livraison ordinaire ne contient aucun diff unifié, fichier `.patch`,
commande `git apply` ou autre format dépendant du contexte des lignes.

Lorsqu’un paquet final est fourni sous forme de ZIP, son extraction doit figurer
avant les contrôles, le staging et le commit :

```powershell
Expand-Archive -LiteralPath .\GateRim-SG1-<version>-finalization.zip -DestinationPath . -Force
```

Lorsqu’un jalon supprime un fichier, annoncer la suppression avant extraction,
fournir la commande `Remove-Item` correspondante et vérifier que Git affiche
bien l’état `D`.

Les scripts locaux `Apply-GateRim-SG1-*.ps1` et
`Publish-GateRim-SG1-*.ps1` sont des outils à usage unique. Ils doivent rester
ignorés par Git, être exclus du staging et être supprimés après succès.

Contrôle de l’index :

```powershell
git diff --cached --name-only | Select-String -Pattern "^(Apply|Publish)-GateRim-SG1-.*\.ps1$"
```

Toute occurrence bloque la publication jusqu’à son retrait de l’index.

### Documents à relire

Avant `git add -A`, relire les fichiers depuis l’arbre de travail réellement
publié :

- `docs/PROJECT_STATE.md` décrit le jalon terminé, ses tests et ses identifiants
  définitifs ;
- `docs/ROADMAP.md` retire le travail terminé du backlog actif et identifie la
  suite différée ;
- `docs/TESTING_CURRENT.md` conserve le résultat final des tests, la dernière
  révision locale validée et les limites connues ;
- `docs/TESTING.md` reçoit la couverture durable ;
- `docs/CHANGELOG.md` décrit l’état réellement publié sans suffixe `-rN` ;
- `About/About.xml` et
  `Source/GateRimSG1/GateRimSG1.csproj` portent la même version.

Contrôle recommandé :

```powershell
git diff -- ./About/About.xml ./Source/GateRimSG1/GateRimSG1.csproj ./docs/PROJECT_STATE.md ./docs/ROADMAP.md ./docs/TESTING_CURRENT.md ./docs/TESTING.md ./docs/CHANGELOG.md
```

Rechercher les marqueurs obsolètes :

```powershell
git grep -n -E "Validation en cours|focused validation is in progress|remain required|reste requis|jalon prêt à tester" -- docs/PROJECT_STATE.md docs/ROADMAP.md docs/TESTING_CURRENT.md docs/TESTING.md docs/CHANGELOG.md
```

Une occurrence historique peut être légitime, mais aucune occurrence ne doit
encore présenter le jalon courant comme non validé.

Exécuter le contrôle automatisé :

```powershell
.\tools\check-project-consistency.cmd
```

La commande doit terminer avec un code de sortie `0`. Lorsqu’un jalon modifie un
format contrôlé, mettre à jour l’outil et
`docs/PROJECT_CONSISTENCY_CHECKS.md` dans le même jalon.

Dans les fichiers Markdown, utiliser `/` dans les chemins relatifs. Aucune
tabulation littérale ne doit rester dans `README.md` ou sous `docs/`.

Le commit final est l’état qui sera intégré et tagué. Les documents doivent donc
déjà décrire le jalon comme clôturé et publié. En cas d’échec de publication, ne
pas commencer le jalon suivant et ne pas créer un commit documentaire séparé
pour transformer « prêt à publier » en « publié ».

## 4. Commit du jalon

Depuis `GateRim-SG1` :

```powershell
git status --short
git add -A
git diff --cached --check
git commit -m "<version> - <description courte>"
```

Ne pas committer directement sur `develop`.

Le bloc fourni pour un vrai jalon doit contenir le message définitif, par
exemple :

```powershell
git commit -m "0.3.91-dev - finalize intrinsic Jaffa forehead-mark overlays"
```

## 5. Intégration dans `develop`

```powershell
git switch develop
git pull --ff-only origin develop
git merge --ff-only feature/<nom-du-jalon>
git push origin develop
```

Pour un correctif, remplacer `feature/...` par `fix/...`.

Si la fusion fast-forward échoue parce que `develop` a avancé, arrêter la
publication, rebaser la branche temporaire sur le nouveau `develop`, résoudre les
conflits et relancer les contrôles pertinents. Ne jamais créer un commit de
fusion improvisé.

La branche temporaire publiée est facultative et ne remplace jamais `develop`
comme base du jalon suivant.

## 6. Tag final unique obligatoire

Une demande de publication d’un jalon validé autorise la séquence complète :
commit, intégration fast-forward, push de `develop`, tag annoté et synchronisation
du wiki lorsqu’il a changé.

Règles :

- un seul tag final par jalon ;
- suffixe `-dev` conservé avant `1.0.0` ;
- aucun suffixe local `-rN` ;
- aucun ancien tag réécrit ;
- tag créé après l’intégration dans `develop` ;
- tag et `develop` pointant vers le même commit.

Commandes :

```powershell
git tag -a v<version> -m "<version> - <description courte>"
git push origin v<version>
```

Contrôle simple :

```powershell
git rev-parse develop
git rev-list -n 1 v<version>
```

Les deux identifiants affichés doivent être identiques. Pour un tag annoté, ne
pas utiliser seul `git rev-parse v<version>`, qui peut retourner l’objet tag au
lieu du commit ciblé.

## Contrôle spécifique aux missions et questlines

Lorsqu’un jalon ajoute, migre ou refond une mission récurrente ou une questline,
`docs/TESTING_CURRENT.md` vérifie explicitement :

- la rééligibilité après réussite, échec et offre ignorée lorsque l’archétype est
  récurrent ;
- les délais cachés variables et l’anti-répétition ;
- les variantes de textes RP visibles ou la justification d’un texte unique ;
- l’absence de répétition immédiate d’une même variante ;
- la sauvegarde/recharge à plusieurs phases et la migration prudente ;
- les outils debug nécessaires, invisibles en jeu normal ;
- le dimensionnement des menaces depuis les points vanilla, la difficulté et la
  puissance réelle de la colonie ;
- au moins un test sur colonie faible et un test sur colonie avancée lorsqu’une
  menace adaptative est consommée ;
- les régressions des missions encore héritées.

Ne pas étendre un framework générique pour un seul cas théorique. Une nouvelle
abstraction doit répondre à plusieurs usages réels ou à un point d’extension
clairement nécessaire.

## 7. Synchronisation du wiki séparé

Synchroniser uniquement lorsque le jalon modifie réellement un fichier
`docs/wiki/*.md`. Sans modification de ce dossier, noter qu’aucune
synchronisation n’est nécessaire et ne pas créer de commit wiki vide.

Avant la synchronisation :

- ajouter toute page créée ou renommée à `docs/wiki/_Sidebar.md` ;
- vérifier chaque cible interne ;
- retirer les liens obsolètes ;
- éviter les doublons.

Contrôle rapide depuis le dépôt principal :

```powershell
Get-ChildItem ./docs/wiki -Filter *.md | Select-Object -ExpandProperty BaseName | Sort-Object
Get-Content ./docs/wiki/_Sidebar.md
```

Mettre d’abord le dépôt wiki à jour :

```powershell
Set-Location "D:\SteamLibrary\steamapps\common\RimWorld\Mods\GateRim-SG1.wiki"
git pull --ff-only
```

Puis copier les pages depuis le dépôt principal :

```powershell
Set-Location "D:\SteamLibrary\steamapps\common\RimWorld\Mods\GateRim-SG1"
.\tools\sync-wiki.cmd
```

Enfin vérifier, committer et publier le wiki :

```powershell
Set-Location "D:\SteamLibrary\steamapps\common\RimWorld\Mods\GateRim-SG1.wiki"
git status --short
git add .
git diff --cached --check
git commit -m "<version> - document <description du jalon>"
git push origin HEAD
```

Le message utilise la version finale sans suffixe `-rN`. Utiliser
`git push origin HEAD` plutôt qu’un nom de branche wiki codé en dur.

## 8. Vérification finale du dépôt principal

Depuis `GateRim-SG1`, sur `develop` :

```powershell
Set-Location "D:\SteamLibrary\steamapps\common\RimWorld\Mods\GateRim-SG1"
git branch --show-current
git log -1 --oneline
git tag --points-at HEAD
git status
git rev-parse develop
git rev-parse origin/develop
```

Résultats attendus :

```text
develop
v<version>
nothing to commit, working tree clean
```

Les identifiants local et distant de `develop` doivent être identiques.

Relire les documents publiés :

```powershell
git show HEAD:docs/PROJECT_STATE.md | Select-Object -First 40
git show HEAD:docs/ROADMAP.md | Select-Object -First 70
git show HEAD:docs/TESTING_CURRENT.md | Select-Object -First 30
```

Ils doivent décrire le jalon comme validé et publié, sans annoncer comme
prochaine étape un test déjà terminé.

## 9. Nettoyage facultatif de la branche temporaire

Après vérification du tag et de `develop` :

```powershell
git branch -d feature/<nom-du-jalon>
```

Si elle avait été publiée :

```powershell
git push origin --delete feature/<nom-du-jalon>
```

Les tags et `develop` conservent l’historique durable.

## 10. Vérification finale du wiki

Seulement lorsqu’une synchronisation wiki a été nécessaire :

```powershell
Set-Location "D:\SteamLibrary\steamapps\common\RimWorld\Mods\GateRim-SG1.wiki"
git log -1 --oneline
git status
```

Résultat attendu :

```text
nothing to commit, working tree clean
```

## 11. Règles permanentes associées

À chaque nouveau jalon :

- mettre `develop` à jour depuis `origin/develop` ;
- vérifier visuellement que son commit correspond au dernier tag publié ;
- créer une branche `feature/*` ou `fix/*` depuis `develop` ;
- ne jamais travailler directement sur `main` ou `develop` ;
- fournir un ZIP prêt à extraire à la racine lorsque des fichiers sont modifiés ;
- annoncer toute suppression avant extraction et vérifier son état Git ;
- inclure tous les fichiers C# concernés sous `Source/GateRimSG1/**/*.cs` ;
- inclure les Defs, traductions, documents et brouillons wiki utiles ;
- utiliser uniquement `About/About.xml` et `docs/CHANGELOG.md` ;
- ne jamais ajouter de doublon `About.xml` ou `CHANGELOG.md` à la racine ;
- préserver `About/ModIcon.png` ;
- mettre à jour `docs/PROJECT_STATE.md` ;
- intégrer les tests durables dans `docs/TESTING.md` ;
- inscrire les travaux décidés dans `docs/ROADMAP.md` et les idées exploratoires
  dans `docs/IDEAS_TO_REVISIT.md` ;
- attendre la validation locale et une demande de publication avant commit,
  intégration, tag, push et publication du wiki ;
- utiliser un commit court au format `<version> - <description>` ;
- intégrer avec `git merge --ff-only` ;
- créer et pousser le tag annoté préfixé par `v` sur le commit intégré ;
- réserver `main` pour `1.0.0` et les versions stables ultérieures ;
- remettre au mainteneur des commandes concrètes sans variables ni
  conditionnelles dans la séquence normale.
