# Tests du jalon actif

Jalon : `0.3.18-dev - Add a Tau'ri origin for generated Tok'ra hosts`

Branche attendue : `feature/tokra-generated-host-tauri-origin`

Base attendue : `v0.3.17-dev`

Révision locale validée : `0.3.18-dev-r1`

Version de DLL attendue : `0.3.18.0`

Statut : validation fonctionnelle terminée sur `0.3.18-dev-r1` ; jalon clôturé et publié sous `v0.3.18-dev`.

## Résultat final validé

- Rebuild forcé validé avec la DLL `0.3.18.0`.
- Chargement validé sans nouvelle erreur de `GeneratedHostOriginDef`, `BackstoryDef`, `skillGains`, patch ou traduction.
- Les origines `SG1_GeneratedHost_OffworldHuman` et `SG1_GeneratedHost_TauriSGCVolunteer` apparaissent toutes les deux.
- L'origine humaine hors-monde reste clairement majoritaire avec les poids relatifs `1` et `0.2`.
- Les hôtes Tau'ri utilisent le générateur de noms Tau'ri, l'une des deux nouvelles enfances et l'une des huit carrières SGC.
- Les hôtes humains hors-monde conservent leurs noms et pools de backstories précédents.
- Les noms de l'hôte et du symbiote restent distincts et l'hôte est actif par défaut.
- Dix basculements successifs ne provoquent aucun cumul, perte ou duplication de compétences.
- Sauvegarde et rechargement validés avec l'hôte actif et le symbiote actif.
- Une identité pré-fusionnée sauvegardée en `0.3.17-dev` reste inchangée et n'est pas reroulée.
- Après extraction puis implantation réelle, `hostIdentitySource=ImplantedExistingHost` et l'origine générée est effacée.
- Les deux nouvelles enfances n'apparaissent ni chez les starters humains ordinaires ni dans le scénario Équipe SG isolée.
- `Player.log` est propre pour le périmètre testé.
- Décision finale : conserver la révision fonctionnelle `r1` sans correctif C# ou Def supplémentaire.

## 1. Branche et extraction propre

```powershell
git branch --show-current
git status --short
git diff --check
```

Branche attendue :

```text
feature/tokra-generated-host-tauri-origin
```

Le ZIP à la racine peut rester non suivi ou ignoré. Aucun fichier suivi sans rapport avec le jalon ne doit être modifié.

## 2. Rebuild forcé

```powershell
dotnet build .\Source\GateRimSG1\GateRimSG1.csproj `
    -t:Rebuild `
    -p:RimWorldManagedDir="D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
```

Résultat validé :

- build réussi sans avertissement nouveau attribuable au jalon ;
- `1.6/Assemblies/GateRimSG1.dll` reconstruite ;
- version de fichier `0.3.18.0` ;
- version du mod `0.3.18-dev`.

## 3. Chargement XML et traductions

Le menu principal a été atteint avec la liste de test habituelle.

Résultat validé :

- aucune erreur rouge liée aux origines générées, aux backstories, aux catégories, au profil Tok'ra ou au patch ;
- titres et descriptions français des deux nouvelles enfances présents ;
- aucune Def dupliquée ou référence non résolue.

## 4. Génération des deux origines historiques

Chemin utilisé :

```text
Debug actions menu
→ Spawn pawn
→ SG1_TokraVoluntaryHost
```

Diagnostic utilisé :

```text
Debug actions menu
→ GateRim SG-1
→ Cultural identity: inspect selected pawn
```

Résultat validé :

- les deux valeurs `generatedHostOrigin` sont observées ;
- l'origine humaine hors-monde reste prédominante ;
- aucun pourcentage exact n'est imposé à un petit échantillon.

## 5. Origine Tau'ri

Résultat validé :

- nom d'hôte Tau'ri distinct du nom du symbiote ;
- enfance `SG1_Tauri_ScienceFairStudentChild` ou `SG1_Tauri_MilitaryFamilyChild` ;
- carrière adulte appartenant aux huit `SG1_TauriSGC_*` ;
- `hostIdentitySource=GeneratedPreJoined` ;
- hôte actif par défaut ;
- textes Bio français corrects.

## 6. Régression de l'origine humaine hors-monde

Résultat validé :

- groupe de noms humain hors-monde conservé ;
- enfance et carrière toujours limitées aux pools existants ;
- deux identités distinctes ;
- comportement identique à `0.3.17-dev`.

## 7. Basculement et progression commune

Résultat validé pour les deux origines :

- mêmes identités après chaque basculement ;
- aucun cumul des écarts de backstories ;
- aucune perte ou duplication de niveau ou d'expérience ;
- origine générée stable.

## 8. Sauvegarde, rechargement et ancienne identité

Résultat validé :

- restauration correcte des personnalités actives ;
- noms, enfances, carrières et origines inchangés ;
- aucun reroll après reprise du temps ou nouveau basculement ;
- ancienne identité `0.3.17-dev` conservée intacte.

## 9. Extraction et réimplantation réelle

Résultat validé après implantation dans un autre pawn :

```text
hostIdentitySource=ImplantedExistingHost
generatedHostOrigin=
```

Le nom et les backstories d'origine du nouvel hôte sont conservés. Aucune identité Tau'ri ou hors-monde générée ne les remplace.

## 10. Isolation des starters

Résultat validé :

- les deux nouvelles enfances ne rejoignent pas les pools de starters humains ordinaires ;
- elles ne remplacent pas les enfances du scénario Équipe SG isolée ;
- les humains ordinaires conservent leur majorité vanilla ;
- les adultes de l'équipe SG restent limités aux huit carrières SGC ;
- aucun pawn existant n'est renommé ou reroulé.

## 11. Contrôle final

```powershell
git status --short
git diff --check
```

Le journal final ne contient aucune nouvelle erreur ou répétition attribuable à GateRim SG-1.
