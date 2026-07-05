# Procédure de validation et publication d’un jalon

Ce document est la référence à suivre lorsqu’un jalon GateRim SG-1 est validé
localement. Le modèle de branches complet est défini dans
[`BRANCHING_WORKFLOW.md`](BRANCHING_WORKFLOW.md).

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

ou, pour une correction ciblée de la ligne de développement :

```text
fix/<nom-du-correctif>
```

Vérifier que `develop` existe, est à jour et appartient à l’historique de la
branche courante :

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

Les ZIP de révision locale et de finalisation doivent contenir uniquement les
fichiers ajoutés ou modifiés depuis la révision précédemment livrée. Une archive
du dépôt complet est réservée à une récupération explicitement demandée.

Chaque chemin inclus doit être un fichier complet prêt à remplacer sa version de
travail. Les livraisons ordinaires ne doivent contenir aucun diff unifié, fichier
`.patch`, commande `git apply` ou autre format dépendant du contexte des lignes.
Un patch textuel n'est autorisé que lorsque le mainteneur le demande explicitement
pour un cas exceptionnel. « Fichier complet » ne signifie pas que tout le dépôt
doit être dupliqué.

Lorsqu'un paquet final est fourni sous forme de ZIP, son extraction fait partie
de la séquence de publication obligatoire. Ne jamais donner ou exécuter une
suite `git add` / `commit` / `push` qui suppose implicitement que le paquet a déjà
été extrait. La commande d'extraction doit apparaître dans le même script ou
bloc exécutable, avant les contrôles et avant tout staging :

```powershell
Expand-Archive `
    -LiteralPath .\GateRim-SG1-<version>-finalization.zip `
    -DestinationPath . `
    -Force
```

Les scripts locaux de livraison ou de publication nommés
`Apply-GateRim-SG1-*.ps1` ou `Publish-GateRim-SG1-*.ps1` sont des outils à usage
unique. Ils doivent être ignorés par Git, explicitement exclus du staging et
supprimés après succès. Vérifier leur absence de l'index avant le commit :

```powershell
git diff --cached --name-only | Select-String -Pattern "^(Apply|Publish)-GateRim-SG1-.*\.ps1$"
```

Toute occurrence bloque la publication jusqu'à son retrait de l'index.

Avant `git add -A`, relire les fichiers directement depuis l’arbre de travail
qui sera publié, et non depuis une ancienne conversation ou un ancien tag :

- `docs/PROJECT_STATE.md` doit être basculé vers l’état final destiné au tag :
  tests terminés, jalon clôturé, identifiants de publication définitifs et
  aucune prochaine étape de test déjà effectuée ;
- `docs/ROADMAP.md` doit marquer comme terminées la validation et la publication
  du jalon en cours, puis identifier clairement le prochain travail différé ;
- `docs/TESTING_CURRENT.md` doit conserver le résultat final des tests, la
  dernière révision locale validée et les éventuelles limites connues ;
- `docs/TESTING.md` doit recevoir la couverture durable du jalon ;
- `docs/CHANGELOG.md` doit décrire l’état réellement publié avec la version
  finale sans suffixe `-rN` ;
- `About/About.xml` et `Source/GateRimSG1/GateRimSG1.csproj` doivent porter la
  même version de jalon.

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

Une occurrence peut être légitime dans l’historique d’un jalon plus ancien,
mais toute occurrence décrivant le jalon en cours doit être corrigée avant le
commit.

Exécuter ensuite le contrôle automatisé de cohérence depuis la racine du dépôt :

```powershell
./tools/check-project-consistency.cmd
```

La commande doit terminer avec un code de sortie `0`. Elle vérifie notamment les
versions publiques et techniques, le nombre réel de `BackstoryDef`, les nombres
annoncés dans le README et le wiki, ainsi que le nombre de lignes du catalogue
culturel. Si le jalon modifie l’un de ces formats contrôlés, mettre à jour
l’outil et `docs/PROJECT_CONSISTENCY_CHECKS.md` dans le même jalon plutôt que de
contourner le contrôle.

Dans les fichiers Markdown, utiliser des `/` pour les chemins relatifs des
commandes PowerShell. Le contrôle automatisé doit échouer si une tabulation
littérale subsiste dans `README.md` ou sous `docs/`.

Le commit final est l’état qui sera intégré dans `develop` et recevra le tag.
Les documents doivent donc déjà décrire le jalon comme clôturé et sa publication
comme effectuée. Si la publication échoue, ne pas commencer le jalon suivant et
ne pas créer un second commit uniquement pour changer « prêt à publier » en
« publié ».

## 4. Vérifier et committer la branche temporaire

```powershell
git status --short
git diff --check
git add -A
git diff --cached --check
git diff --cached --stat

git commit -m "<version> - <description courte>"
```

Convention de commit :

```text
0.3.67-dev - adopt develop-based branch workflow
```

Ne pas committer directement sur `develop`.

## 5. Intégrer dans `develop`

Mettre l’intégration à jour puis exiger une fusion fast-forward :

```powershell
git switch develop
git pull --ff-only origin develop
git merge --ff-only feature/<nom-du-jalon>
```

Pour un correctif, remplacer `feature/...` par `fix/...`.

Si `git merge --ff-only` échoue parce que `develop` a avancé, revenir sur la
branche temporaire, la rebaser sur `develop`, résoudre les conflits et relancer
les contrôles pertinents. Ne pas créer de commit de fusion improvisé pour
contourner l’échec.

Vérifier ensuite que le commit intégré est bien celui qui a été validé :

```powershell
git log -1 --oneline
git status --short
```

Puis publier la branche d’intégration :

```powershell
git push origin develop
```

La publication de la branche temporaire est facultative. Elle peut être poussée
pour sauvegarde ou revue, mais elle ne remplace jamais `develop` comme base du
jalon suivant.

## 6. Créer et publier le tag final unique obligatoire

Une demande de type « commit et push », « publier » ou équivalente portant sur
un jalon validé autorise toute la séquence : commit final, intégration
fast-forward dans `develop`, push de `develop`, création et push du tag annoté,
puis synchronisation et push du wiki lorsqu’il a changé. Ne demander une
autorisation séparée pour le tag que si le mainteneur l’a explicitement exclu.

Règles :

- utiliser un seul tag final par jalon ;
- conserver le suffixe `-dev` avant `1.0.0` ;
- ne jamais ajouter de suffixe `-rN` au tag final ;
- ne pas réécrire les anciens tags déjà publiés ;
- créer le tag après l’intégration dans `develop` ;
- vérifier que le tag et `develop` pointent vers le même commit.

```powershell
git tag -a v<version> -m "<version> - <description courte>"
git push origin v<version>
```

Exemple :

```powershell
git tag -a v0.3.67-dev `
    -m "0.3.67-dev - adopt develop-based branch workflow"
git push origin v0.3.67-dev
```

Contrôle :

```powershell
git rev-parse develop
git rev-list -n 1 v<version>
```

Pour un tag annoté, ne pas comparer `git rev-parse v<version>` directement à
la branche : cette commande peut retourner l’identifiant de l’objet tag.
Utiliser `git rev-list -n 1 v<version>` ou `git rev-parse "v<version>^{}"` pour
obtenir le commit réellement ciblé.

Les deux identifiants doivent être identiques.

## Contrôle spécifique aux missions et questlines

Lorsqu’un jalon ajoute, migre ou refond une mission récurrente ou une questline,
`docs/TESTING_CURRENT.md` doit vérifier explicitement :

- la rééligibilité après réussite, échec et offre ignorée lorsque l’archétype est
  récurrent ;
- les délais cachés variables et l’anti-répétition du dernier archétype ;
- les variantes de textes RP visibles, ou la justification d’un texte unique
  conçu pour rester naturel après répétition ;
- l’absence de répétition immédiate d’une même variante lorsque plusieurs
  variantes sont disponibles ;
- la sauvegarde/recharge à plusieurs phases et la migration prudente des
  anciennes sauvegardes ;
- les outils debug permettant d’inspecter ou de forcer les phases sans être
  visibles en jeu normal ;
- le dimensionnement des menaces à partir des points de menace, de la difficulté
  active et de la puissance de la colonie plutôt qu’avec des effectifs fixes ;
- au moins un test sur une colonie faible et une colonie avancée lorsqu’une
  menace adaptative est consommée ;
- les régressions des missions encore héritées lorsque la migration est
  progressive.

Un framework générique ne doit pas être étendu pour un seul cas théorique.
Ajouter une nouvelle abstraction uniquement lorsqu’elle répond à plusieurs
usages réels ou constitue un point d’extension clairement nécessaire.

## 7. Synchroniser le wiki séparé uniquement si nécessaire

Synchroniser le wiki lorsque le jalon modifie réellement un fichier
`docs/wiki/*.md`. En l’absence de modification dans ce dossier, noter
explicitement qu’aucune synchronisation n’est nécessaire et ne pas créer de
commit wiki vide.

Avant la synchronisation, vérifier la cohérence de la navigation :

- toute page wiki créée ou renommée doit être ajoutée à `docs/wiki/_Sidebar.md`
  dans la catégorie thématique appropriée ;
- conserver les catégories existantes ou créer une catégorie durable seulement
  lorsqu’elle regroupe plusieurs pages cohérentes ;
- vérifier que chaque cible interne de la sidebar correspond à un fichier
  `docs/wiki/<cible>.md` existant ;
- supprimer ou corriger tout lien devenu obsolète ;
- éviter les doublons de liens internes dans la sidebar.

Contrôle rapide :

```powershell
Get-ChildItem ./docs/wiki -Filter *.md |
    Select-Object -ExpandProperty BaseName |
    Sort-Object
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
Le message de commit du wiki utilise la version finale sans suffixe `-rN`.

## 8. Vérification finale du dépôt principal

Depuis `GateRim-SG1`, sur `develop` :

```powershell
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

Relire ensuite les fichiers publiés depuis `HEAD` :

```powershell
git show HEAD:docs/PROJECT_STATE.md | Select-Object -First 40
git show HEAD:docs/ROADMAP.md | Select-Object -First 70
git show HEAD:docs/TESTING_CURRENT.md | Select-Object -First 30
```

Ils doivent décrire le jalon comme validé et publié, sans annoncer comme
prochaine étape un test déjà terminé.

## 9. Nettoyer la branche temporaire

Après vérification du tag et de `develop`, la branche temporaire peut être
supprimée :

```powershell
git branch -d feature/<nom-du-jalon>
```

Si elle avait été publiée :

```powershell
git push origin --delete feature/<nom-du-jalon>
```

Cette suppression est facultative mais recommandée pour garder les branches
actives lisibles. Les tags et `develop` conservent l’historique durable.

## 10. Vérification finale du wiki

Seulement lorsqu’une synchronisation wiki a été nécessaire :

```powershell
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
- vérifier qu’il correspond au dernier tag de développement publié ;
- créer une branche `feature/*` ou `fix/*` depuis `develop` ;
- ne jamais travailler directement sur `main` ou `develop` ;
- fournir un ZIP prêt à extraire à la racine lorsque des fichiers sont modifiés ;
- lorsqu’un jalon supprime un fichier, annoncer la suppression avant extraction,
  fournir la commande `Remove-Item` correspondante et vérifier l’état `D` ;
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
- réserver `main` pour `1.0.0` et les versions stables ultérieures.
