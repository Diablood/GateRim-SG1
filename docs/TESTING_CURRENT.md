# Current milestone validation

Jalon : `0.3.77-dev - Establish the core-faction completion gate`

Branche : `feature/core-faction-completion-gate`

Révision finale validée : `r1`

Version de DLL validée : `0.3.77.0`

Statut : validation finale réussie. Aucun comportement de jeu n'est modifié.

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

- assembly `0.3.77.0` ;
- audit des durées avec `104` clés uniques ;
- contrôle global de cohérence réussi ;
- aucun lien Markdown local manquant ;
- aucune erreur de compilation.

## Revue obligatoire de la priorité

Vérifier dans `docs/ROADMAP.md` :

1. la phase 1 bloque tout nouveau peuple ou chapitre héritier ;
2. la phase 1 couvre Tok'ra, Goa'uld/Jaffa, Tau'ri/SGC, les audits partagés, les
   visuels, les anneaux de transport et la progression Stargate ;
3. le pool Tok'ra reste fermé à huit opérations ;
4. Asgard, Nox, Unas, Réplicateurs, Ideology, Royalty et le monde entièrement
   GateRim sont explicitement bloqués jusqu'à clôture du socle ;
5. les origines pondérées des hôtes Tok'ra sont une extension dépendante après
   l'arrivée de nouvelles cultures et non une dette bloquante du socle actuel ;
6. le critère de clôture exige publication, retrait explicite ou déplacement
   explicite vers `IDEAS_TO_REVISIT.md` ;
7. chaque futur peuple doit hériter des frameworks culturels, de factions,
   missions, menace, persistance, documentation et tests déjà publiés ;
8. aucune case silencieusement ignorée ne peut être considérée comme close.

## Contrôle de portée

```powershell
git diff --name-only
```

Le diff doit modifier uniquement :

```text
About/About.xml
README.md
Source/GateRimSG1/GateRimSG1.csproj
docs/CHANGELOG.md
docs/PROJECT_STATE.md
docs/ROADMAP.md
docs/TESTING_CURRENT.md
docs/wiki/Content-Status.md
docs/wiki/Home.md
```

Aucun fichier C# de gameplay, XML de Def, traduction ou texture ne doit être
modifié.

## Vérification RimWorld courte

1. Démarrer RimWorld avec GateRim SG-1.
2. Vérifier `0.3.77-dev` dans les métadonnées du mod.
3. Charger une sauvegarde existante.
4. Confirmer qu'aucun comportement ou contenu visible n'a changé.
5. Examiner `Player.log` et confirmer l'absence de nouvelle erreur C#, Harmony,
   XML, DefOf, traduction, texture, Scribe ou chargement de mod.

## Résultat final validé

- build forcé `0.3.77.0` réussi ;
- audit des durées inchangé à `104` clés uniques ;
- contrôle de cohérence du projet réussi ;
- liens Markdown locaux valides ;
- séparation explicite entre clôture du socle et extensions héritières ;
- chargement d'une sauvegarde existante sans changement de gameplay ;
- `Player.log` propre.
