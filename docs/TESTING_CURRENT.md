# Current milestone validation

Jalon : `0.3.76-dev - Reconcile future roadmap and visual debt`

Branche : `feature/future-roadmap-reconciliation`

Révision finale validée : `r2`

Version de DLL validée : `0.3.76.0`

Statut : validation finale réussie. Aucun comportement de jeu n'est modifié.

## Livraison complète des documents

La révision `r2` remplace la livraison `r1` qui utilisait un patch textuel.
Après extraction, aucun `git apply`, fichier `.patch` ou étape supplémentaire
n'est requis. Tous les chemins du ZIP sont des fichiers complets prêts à
remplacer leur version de travail.

Supprimer le patch laissé par `r1` s'il est encore présent à la racine :

```powershell
Remove-Item `
    .\GateRim-SG1-0.3.76-dev-r1-long-docs.patch `
    -ErrorAction SilentlyContinue
```

## Contrôles automatiques

Depuis la racine du dépôt :

```powershell
git diff --check
.\build.cmd
.\tools\check-duration-formatting.cmd
.\tools\check-project-consistency.cmd

git status --short
git diff --stat
```

Résultats attendus :

- assembly `0.3.76.0` ;
- audit des durées avec `104` clés uniques ;
- contrôle global de cohérence réussi ;
- aucun lien Markdown local manquant ;
- aucune erreur de compilation.

## Revue documentaire obligatoire

Vérifier dans `docs/ROADMAP.md` :

1. le jalon courant `0.3.76-dev` et le dernier jalon gameplay `0.3.75-dev` ;
2. la liste des travaux déjà réalisés, notamment observation Tok'ra `0.3.4-dev`,
   debug `0.3.35-dev`, opérations `0.3.38-dev`, icônes `0.3.50/51-dev`, menace
   `0.3.53-dev`, documentation `0.3.63-dev`, relations `0.3.68/73-dev` et
   officiers `0.3.74/75-dev` ;
3. l'absence de ces travaux parmi les futurs jalons actifs ;
4. la séparation entre contrats permanents et futurs jalons ;
5. l'inventaire futur des assets provisoires ;
6. la passe artistique définitive couvrant tous les placeholders et textures
   temporaires, y compris la lunette Tok'ra et l'équipement rouge des officiers ;
7. chaque extension d'alliance dans un jalon individuel ;
8. les garde-fous territoriaux avant toute expansion ;
9. les anneaux de transport en deux jalons distincts : fondation joueur, puis
   missions/usages hostiles ;
10. la progression Stargate découpée en fondations, première expédition et Porte
    fonctionnelle ;
11. les fondations Asgard, Nox, Unas et Réplicateurs séparées ;
12. les audits optionnels Ideology et Royalty séparés ;
13. les décisions ouvertes explicitement reportées au lancement de chaque jalon.

Vérifier dans `docs/IDEAS_TO_REVISIT.md` :

- seules les pistes non planifiées restent présentes ;
- les anneaux de transport sont indiqués comme promus dans la roadmap ;
- le sarcophage, le confinement adulte, les DLC optionnels et les fonctions
  avancées du kara kesh ne sont pas présentés comme des jalons promis.

## Contrôle de portée

```powershell
git diff --name-only
```

Après extraction de `r1`, puis de `r2`, le diff doit modifier uniquement :

```text
AGENTS.md
About/About.xml
README.md
Source/GateRimSG1/GateRimSG1.csproj
docs/CHANGELOG.md
docs/IDEAS_TO_REVISIT.md
docs/MILESTONE_PUBLICATION.md
docs/PROJECT_STATE.md
docs/README.md
docs/ROADMAP.md
docs/TESTING_CURRENT.md
docs/wiki/Content-Status.md
docs/wiki/Home.md
```

Aucun fichier C# de gameplay, XML de Def, traduction ou texture ne doit être
modifié. `AGENTS.md` et `docs/MILESTONE_PUBLICATION.md` doivent interdire les
livraisons ordinaires sous forme de `.patch`, diff applicable ou `git apply`. Les deux sources wiki ne changent que leur version, leur statut et le
résumé des développements futurs.

## Vérification RimWorld courte

1. Démarrer RimWorld avec GateRim SG-1.
2. Vérifier `0.3.76-dev` dans les métadonnées du mod.
3. Charger une sauvegarde existante.
4. Confirmer qu'aucun comportement ou contenu visible n'a changé.
5. Examiner `Player.log` et confirmer l'absence de nouvelle erreur C#, Harmony,
   XML, DefOf, traduction, texture, Scribe ou chargement de mod.


## Résultat final validé

- build forcé réussi avec l'assembly `0.3.76.0` ;
- audit des durées réussi avec `104` clés uniques ;
- contrôle global de cohérence réussi ;
- liens Markdown locaux valides ;
- portée limitée aux `13` fichiers documentaires et métadonnées attendus ;
- roadmap nettoyée des travaux déjà publiés ;
- futurs jalons séparés et questions reportées à leur lancement ;
- anneaux de transport et passe artistique définitive inscrits ;
- aucun fichier `.patch`, diff applicable ou commande `git apply` dans la
  livraison finale ;
- démarrage RimWorld en `0.3.76-dev`, sauvegarde existante chargée et
  `Player.log` accepté.
