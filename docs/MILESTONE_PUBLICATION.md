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

## 3. Vérifier, committer et publier le dépôt principal

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

## 4. Créer et publier le tag final unique

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

## 5. Synchroniser et publier le wiki séparé

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
.\tools\sync-wiki.cmd
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

## 6. Vérification finale du dépôt principal

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

## 7. Vérification finale du wiki

Depuis `GateRim-SG1.wiki` :

```powershell
git log -1 --oneline
git status
```

Résultat attendu :

```text
nothing to commit, working tree clean
```

## 8. Règles permanentes associées

À chaque nouveau jalon :

- partir du dernier tag publié sur une branche `feature/...` dédiée ;
- fournir un ZIP prêt à extraire à la racine du dépôt lorsque des fichiers sont modifiés ;
- inclure tous les fichiers C# concernés sous `Source/GateRimSG1/**/*.cs` ;
- inclure les Defs, traductions, documents et brouillons wiki utiles ;
- utiliser uniquement `About/About.xml` et `docs/CHANGELOG.md` ;
- ne jamais ajouter de doublon `About.xml` ou `CHANGELOG.md` à la racine ;
- préserver `About/ModIcon.png` ;
- mettre à jour `docs/PROJECT_STATE.md` ;
- intégrer les tests durables dans `docs/TESTING.md` ;
- attendre la validation locale avant commit, tag, push et publication du wiki ;
- proposer un commit court au format `<version> - <description>` ;
- proposer un tag Git annoté préfixé par `v`.
