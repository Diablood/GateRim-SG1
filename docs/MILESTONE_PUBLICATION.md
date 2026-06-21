# Procédure de validation et publication d’un jalon

Ce document est la référence à suivre lorsqu’un jalon GateRim SG-1 est validé localement.

Il évite de dépendre de l’historique d’une conversation ChatGPT, notamment lorsqu’une discussion atteint sa limite de contexte.

## 1. Annoncer la validation locale

Exemple :

```text
Le jalon 0.2.51-dev est validé après r4.
```

Le suffixe `rN` désigne uniquement une itération locale de test. Il ne doit pas apparaître dans le commit final ni dans le tag publié.

## 2. Vérifier la branche active

```powershell
git branch --show-current
```

Résultat attendu :

```text
feature/<nom-du-jalon>
```

Toujours rappeler explicitement la branche active avant la publication.

## 3. Verrou documentaire avant le commit final

Avant `git add -A`, relire les fichiers directement depuis l’arbre de travail qui sera publié, et non depuis une ancienne conversation ou un ancien tag :

- `docs/PROJECT_STATE.md` doit être basculé vers l’état final destiné au tag : tests terminés, jalon clôturé, identifiants de publication définitifs et aucune prochaine étape de test déjà effectuée ;
- `docs/ROADMAP.md` doit marquer comme terminées la validation et la publication du jalon en cours, puis identifier clairement le prochain travail différé ;
- `docs/TESTING_CURRENT.md` doit conserver le résultat final des tests, la dernière révision locale validée et les éventuelles limites connues ;
- `docs/TESTING.md` doit recevoir la couverture durable du jalon ;
- `docs/CHANGELOG.md` doit décrire l’état réellement publié avec la version finale sans suffixe `-rN` ;
- `About/About.xml` et `Source/GateRimSG1/GateRimSG1.csproj` doivent porter la même version de jalon.

Contrôle recommandé :

```powershell
git diff -- `
    ./About/About.xml `
    ./Source/GateRimSG1/GateRimSG1.csproj `
    ./docs/PROJECT_STATE.md `
    ./docs/ROADMAP.md `
    ./docs/TESTING_CURRENT.md `
    ./docs/TESTING.md `
    ./docs/CHANGELOG.md
```

Rechercher ensuite les marqueurs susceptibles d’être devenus obsolètes :

```powershell
git grep -n -E "Validation en cours|focused validation is in progress|remain required|reste requis|jalon prêt à tester" -- `
    docs/PROJECT_STATE.md `
    docs/ROADMAP.md `
    docs/TESTING_CURRENT.md `
    docs/TESTING.md `
    docs/CHANGELOG.md
```

Une occurrence peut être légitime dans l’historique d’un jalon plus ancien, mais toute occurrence décrivant le jalon en cours doit être corrigée avant le commit.

Exécuter ensuite le contrôle automatisé de cohérence depuis la racine du dépôt :

```powershell
./tools/check-project-consistency.cmd
```

La commande doit terminer avec un code de sortie `0`. Elle vérifie notamment les versions publiques et techniques, le nombre réel de `BackstoryDef`, les nombres annoncés dans le README et le wiki, ainsi que le nombre de lignes du catalogue culturel. Si le jalon modifie l’un de ces formats contrôlés, mettre à jour l’outil et `docs/PROJECT_CONSISTENCY_CHECKS.md` dans le même jalon plutôt que de contourner le contrôle.

Dans les fichiers Markdown, utiliser des `/` pour les chemins relatifs des commandes PowerShell, par exemple `./tools/check-project-consistency.cmd`. Cela évite qu’une séquence comme `\t` soit transformée en tabulation par un générateur ou une étape de copie. Le contrôle automatisé doit échouer si une tabulation littérale subsiste dans `README.md` ou sous `docs/`.

Le commit final est l’état qui recevra le tag. Les documents doivent donc déjà décrire le jalon comme clôturé et sa publication comme effectuée. Les commandes de push et de tag sont exécutées immédiatement après ce commit. Si la publication échoue, ne pas commencer le jalon suivant tant que l’échec n’est pas résolu ; ne pas créer un second commit uniquement pour changer « prêt à publier » en « publié ».

Si cette vérification révèle une faiblesse récurrente de la procédure, modifier ce fichier dans le même jalon afin que la correction ne dépende pas de la mémoire d’une conversation.

Lorsqu’un contrôle automatisé lit un document dont le libellé change légitimement entre la phase de test et l’état final publié, le contrôle doit accepter explicitement les deux formulations prévues tout en vérifiant la même valeur. Éviter les expressions régulières liées à un seul état documentaire si la procédure impose ensuite de renommer ce libellé. Un champ absent ne doit produire qu’un seul diagnostic clair, sans second échec redondant dû à une chaîne vide.

## 4. Vérifier, committer et publier le dépôt principal

```powershell
git status --short
git diff --check
git add -A
git diff --cached --check
git diff --cached --stat

git commit -m "<version> - <description courte>"

git push -u origin <branche-active>
```

Convention de commit :

```text
0.2.51-dev - add organic Tok'ra wounded agent care
```

## 5. Créer et publier le tag final unique

Avant la première release publique :

- utiliser un seul tag final par jalon ;
- conserver le suffixe `-dev` ;
- ne jamais ajouter de suffixe `-rN` au tag final ;
- ne pas réécrire les anciens tags déjà publiés.

```powershell
git tag -a v<version> -m "<version> - <description courte>"
git push origin v<version>
```

Exemple :

```powershell
git tag -a v0.2.51-dev -m "0.2.51-dev - add organic Tok'ra wounded agent care"
git push origin v0.2.51-dev
```

## 6. Synchroniser le wiki séparé uniquement si nécessaire

Synchroniser le wiki lorsque le jalon modifie réellement un fichier `docs/wiki/*.md`. En l’absence de modification dans ce dossier, noter explicitement qu’aucune synchronisation n’est nécessaire et ne pas créer de commit wiki vide.

Avant la synchronisation, vérifier la cohérence de la navigation :

- toute page wiki créée ou renommée doit être ajoutée à `docs/wiki/_Sidebar.md` dans la catégorie thématique appropriée ;
- ne pas ajouter les nouvelles pages à la fin d’une liste linéaire : conserver les catégories existantes ou créer une catégorie durable seulement lorsqu’elle regroupe plusieurs pages cohérentes ;
- vérifier que chaque cible interne de la sidebar correspond à un fichier `docs/wiki/<cible>.md` existant ;
- supprimer ou corriger dans le même jalon tout lien devenu obsolète à la suite d’un renommage ou d’une suppression ;
- éviter les doublons de liens internes dans la sidebar.

Contrôle rapide des pages et de la sidebar :

```powershell
Get-ChildItem ./docs/wiki -Filter *.md | Select-Object -ExpandProperty BaseName | Sort-Object
Get-Content ./docs/wiki/_Sidebar.md
```

Chemins locaux habituels :

```powershell
$modRepo = "D:\SteamLibrary\steamapps\common\RimWorld\Mods\GateRim-SG1"
$wikiRepo = "D:\SteamLibrary\steamapps\common\RimWorld\Mods\GateRim-SG1.wiki"
```

Mettre d’abord le dépôt wiki à jour :

```powershell
Push-Location $wikiRepo
git pull --ff-only
Pop-Location
```

Copier ensuite les pages depuis le dépôt principal :

```powershell
Push-Location $modRepo
./tools/sync-wiki.cmd
Pop-Location
```

Vérifier, committer et publier le wiki :

```powershell
Push-Location $wikiRepo

git status --short
git add .
git diff --cached --check
git commit -m "<version> - document <description du jalon>"
git push origin HEAD

Pop-Location
```

Utiliser `git push origin HEAD` plutôt qu’un nom de branche wiki codé en dur.

Le message de commit du wiki doit utiliser la version finale du jalon, sans suffixe `-rN`.

## 7. Vérification finale du dépôt principal

Depuis `GateRim-SG1` :

```powershell
git log -1 --oneline
git tag --points-at HEAD
git status
```

Résultats attendus :

```text
v<version>
nothing to commit, working tree clean
```

Relire ensuite les fichiers publiés depuis `HEAD` pour éviter qu’un document local non indexé ou une ancienne version ait été contrôlé par erreur :

```powershell
git show HEAD:docs/PROJECT_STATE.md | Select-Object -First 40
git show HEAD:docs/ROADMAP.md | Select-Object -First 70
git show HEAD:docs/TESTING_CURRENT.md | Select-Object -First 30
```

Ils doivent décrire le jalon comme validé et publié, sans annoncer comme prochaine étape un test déjà terminé.

## 8. Vérification finale du wiki

Seulement lorsqu’une synchronisation wiki a été nécessaire :

```powershell
git log -1 --oneline
git status
```

Résultat attendu :

```text
nothing to commit, working tree clean
```

## 9. Règles permanentes associées

À chaque nouveau jalon :

- partir du dernier tag publié sur une branche `feature/...` dédiée ;
- fournir un ZIP prêt à extraire à la racine du dépôt lorsque des fichiers sont modifiés ;
- lorsqu’un jalon supprime un fichier, annoncer explicitement la suppression avant l’extraction, fournir la commande `Remove-Item` correspondante et vérifier que `git status --short` affiche bien l’état `D` ;
- inclure tous les fichiers C# concernés sous `Source/GateRimSG1/**/*.cs` ;
- inclure les Defs, traductions, documents et brouillons wiki utiles ;
- utiliser uniquement `About/About.xml` et `docs/CHANGELOG.md` ;
- ne jamais ajouter de doublon `About.xml` ou `CHANGELOG.md` à la racine ;
- préserver `About/ModIcon.png` ;
- mettre à jour `docs/PROJECT_STATE.md` ;
- intégrer les tests durables dans `docs/TESTING.md` ;
- mettre à jour cette procédure lorsqu’une amélioration durable est découverte ;
- attendre la validation locale avant commit, tag, push et publication du wiki ;
- proposer un commit court au format `<version> - <description>` ;
- proposer un tag Git annoté préfixé par `v`.
