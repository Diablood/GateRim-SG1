# Tests du jalon actif

Jalon : `0.3.16-dev - Expand cultural backstory variety`

Branche attendue : `feature/cultural-backstory-expansion`

Base attendue : `v0.3.15-dev`

Révision locale validée : `0.3.16-dev-r1`

Version de DLL attendue : `0.3.16.0`

Statut : validation fonctionnelle terminée sur `0.3.16-dev-r1` ; aucun correctif supplémentaire requis ; jalon clôturé et publié sous `v0.3.16-dev`.

## Résultat final validé

- Rebuild forcé validé avec la DLL `0.3.16.0`.
- Chargement validé sans nouvelle erreur XML, `BackstoryDef`, `skillGains`, patch ou traduction.
- Les douze titres, descriptions et bonus français correspondent aux Defs et au catalogue wiki.
- Les deux nouvelles enfances et les quatre nouvelles carrières Jaffa apparaissent dans les pools attendus.
- Les carrières Jaffa mixtes conservent les groupes de noms `GoauldJaffa` et `FreeJaffa` attendus.
- Les quatre nouvelles carrières d'hôtes Goa'uld apparaissent avec les groupes `Goauld` et `Tokra` attendus.
- Le scénario Équipe SG isolée reste limité aux huit carrières SGC, avec des enfances vanilla.
- Les humains ordinaires conservent une majorité claire de carrières vanilla et leurs noms vanilla.
- La génération normale du monde, les raids et les identités Jaffa, Goa'uld et Tok'ra ne régressent pas.
- Sauvegarde/recharge, noms manuels et basculement de personnalité Tok'ra validés sans reroll ni dérive de compétences.
- Le catalogue wiki contient les `70` entrées et correspond au contenu français en jeu.
- `Player.log` propre pour le périmètre testé.
- Décision finale : conserver l'extension entièrement pilotée par XML ; aucun correctif C# ou Def supplémentaire n'est nécessaire.

## 1. Préparation et chargement

1. Vérifier que le dépôt ne contient aucun changement non prévu.
2. Créer la branche depuis le tag publié :

```powershell
git switch -c feature/cultural-backstory-expansion v0.3.15-dev
```

3. Extraire l'archive `r1` à la racine du dépôt.
4. Contrôler les changements :

```powershell
git status --short
git diff --check
```

5. Effectuer un rebuild forcé :

```powershell
dotnet build .\Source\GateRimSG1\GateRimSG1.csproj `
    -t:Rebuild `
    -p:RimWorldManagedDir="D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
```

6. Vérifier que `1.6/Assemblies/GateRimSG1.dll` porte la version `0.3.16.0`.
7. Lancer RimWorld avec GateRim SG-1 et Biotech.
8. Attendre le menu principal et vérifier l'absence d'erreur rouge, notamment pour :
   - `BackstoryDef` ;
   - `skillGains` ;
   - `PatchOperationAdd` ;
   - `CulturalPawnProfileDef` ;
   - traduction DefInjected.

## 2. Catalogue et traduction française

1. Passer le jeu en français.
2. Vérifier que les douze nouvelles entrées peuvent être observées pendant les tests suivants et qu'elles affichent un titre, une description et les bonus attendus :
   - spécialiste de survie du SGC ;
   - quartier-maître du SGC ;
   - enfant des mines de naquadah ;
   - messager d'une colonie du Chappa'ai ;
   - guerrier d'abordage de Ha'tak ;
   - collecteur de tribut Goa'uld ;
   - émissaire d'une colonie Jaffa libre ;
   - armurier Jaffa libre ;
   - contremaître Goa'uld du naquadah ;
   - gardien des systèmes de vaisseau Goa'uld ;
   - sapeur Tok'ra ;
   - coordinateur de refuge Tok'ra.
3. Vérifier qu'aucune balise, clé de traduction ou formulation technique n'est visible pour le joueur.
4. Vérifier que les bonus restent modérés et ne créent ni passion, ni trait, ni incapacité de travail.

## 3. Starter Jaffa

1. Démarrer un scénario permettant de randomiser un pawn de départ avec le xenotype `SG1_Jaffa`.
2. Effectuer au moins cinquante randomisations réparties sur plusieurs candidats.
3. Vérifier que les deux nouvelles enfances peuvent apparaître avec les huit anciennes.
4. Vérifier que les quatre nouvelles carrières adultes peuvent apparaître avec les seize anciennes.
5. Pour chaque nouvelle carrière adulte observée, ouvrir le diagnostic culturel de `0.3.15-dev` et vérifier le groupe de noms :
   - guerrier d'abordage et collecteur de tribut → `GoauldJaffa` ;
   - émissaire et armurier → `FreeJaffa`.
6. Vérifier qu'une sélection manuelle effectuée ensuite par un éditeur de pawns compatible n'est pas remplacée.

## 4. Starter hôte Goa'uld

1. Randomiser un pawn de départ avec le xenotype `SG1_GoauldHost`.
2. Effectuer au moins cinquante randomisations.
3. Vérifier que les quatre nouvelles carrières adultes apparaissent dans le pool existant.
4. Contrôler avec le diagnostic culturel :
   - contremaître du naquadah et gardien de systèmes → groupe `Goauld` ;
   - sapeur et coordinateur de refuge → groupe `Tokra`.
5. Vérifier que les enfances restent limitées aux six enfances humaines hors-monde existantes.
6. Vérifier que les noms sont cohérents dès l'écran de sélection et restent stables au lancement de la partie.

## 5. Scénario Équipe SG isolée

1. Lancer le scénario **Équipe SG isolée**.
2. Randomiser les quatre candidats plusieurs fois.
3. Vérifier que les huit carrières SGC sont désormais possibles : les six anciennes, spécialiste de survie et quartier-maître.
4. Vérifier qu'aucune carrière vanilla ou d'une autre culture n'est attribuée comme âge adulte.
5. Vérifier que les enfances restent vanilla, conformément au profil actuel.
6. Modifier manuellement au moins un nom, lancer la partie et vérifier qu'il reste inchangé.

## 6. Humains ordinaires dans un scénario normal

1. Lancer un scénario vanilla ou moddé ordinaire avec des starters humains.
2. Effectuer un ensemble suffisamment large de randomisations pour observer plusieurs carrières vanilla et, occasionnellement, des carrières SGC.
3. Vérifier que les deux nouvelles carrières SGC rejoignent le pool pondéré existant sans remplacer systématiquement les carrières vanilla.
4. Vérifier que les backstories Tau'ri / SGC ne deviennent pas la majorité évidente des résultats.
5. Vérifier qu'un humain ordinaire conserve son nom vanilla et n'est pas renommé par le profil additif.

## 7. Génération normale du monde

1. Utiliser `Debug actions menu` → `Spawn pawn` avec des PawnKinds représentatifs :
   - Jaffa Goa'uld ;
   - Jaffa libre ;
   - hôte Goa'uld ;
   - `SG1_TokraVoluntaryHost`.
2. Générer également un raid Goa'uld et, si disponible, des visiteurs ou habitants de colonie Free Jaffa.
3. Vérifier que les spawn categories continuent de produire des parcours adaptés à la culture.
4. Vérifier que les nouveaux parcours peuvent apparaître naturellement sans forcer un profil de starter.
5. Vérifier que les noms culturels, Prim'ta, marques Jaffa et identités Tok'ra ne régressent pas.

## 8. Sauvegarde et rechargement

1. Conserver au moins quatre pawns portant de nouvelles backstories, dont un Jaffa et un Tok'ra.
2. Noter les noms, enfances, âges adultes et niveaux de compétences visibles.
3. Sauvegarder, quitter complètement le jeu et recharger.
4. Vérifier que les backstories, noms et bonus restent identiques.
5. Pour un Tok'ra contrôlé, basculer la personnalité avant et après rechargement et vérifier l'absence de cumul ou de perte de compétences.
6. Vérifier qu'aucun pawn existant d'une ancienne sauvegarde n'est reroll ou renommé.

## 9. Wiki et journal final

1. Vérifier que `docs/wiki/Cultural-Backstories.md` annonce `70` histoires et contient les douze nouvelles lignes dans les sections correspondantes.
2. Vérifier que les titres, descriptions et bonus du wiki correspondent exactement au contenu français en jeu.
3. Fermer ou mettre en pause le jeu et contrôler `Player.log`.

## Critère de validation

Critère atteint : les douze backstories sont chargées, traduites, culturellement filtrées et persistantes ; les profils de départ conservent leurs règles de noms ; les humains ordinaires gardent une majorité de carrières vanilla ; et aucune erreur nouvelle n'apparaît dans `Player.log`.
