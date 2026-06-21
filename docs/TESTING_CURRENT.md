# Tests du dernier jalon validé

Jalon : `0.3.23-dev - Introduce reusable mission framework foundations`

Branche attendue : `feature/mission-framework-foundation`

Révision locale validée : `0.3.23-dev-r2`

Version de DLL validée : `0.3.23.0`

Statut : matrice complète validée, jalon publié sous le tag final unique `v0.3.23-dev`.

## 1. Contrôle du dépôt

Depuis la racine du dépôt :

```powershell
./tools/check-project-consistency.cmd `
    -ExpectedVersion 0.3.23-dev `
    -ExpectedBackstoryCount 83

if ($LASTEXITCODE -ne 0) {
    throw "Le contrôle de cohérence du projet a échoué."
}
```

Résultat attendu : contrôle entièrement validé, sans tabulation littérale dans les Markdown.

## 2. Rebuild forcé

```powershell
dotnet build ./Source/GateRimSG1/GateRimSG1.csproj `
    -t:Rebuild `
    -p:RimWorldManagedDir="D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
```

Résultats attendus :

- build réussi ;
- DLL `0.3.23.0` ;
- aucun avertissement ou échec lié à `GateRimMissionDef`, `GateRimMissionRuntimeData` ou aux adaptations Tok'ra.

## 3. Chargement des Defs

Lancer RimWorld jusqu'au menu principal puis charger une carte de test.

Vérifier l'absence d'erreur rouge concernant :

- `SG1_TokraOrganic_GoauldObservation` ;
- `GateRimSG1.Missions.GateRimMissionDef` ;
- les phases, contextes, variantes de texte ou récompenses ;
- les nouvelles clés anglaises et françaises.

## 4. Rapport technique du framework

Activer le mode développeur et ouvrir exactement :

```text
Debug actions menu
→ GateRim SG-1
→ Mission framework: inspect definitions
```

Le rapport doit afficher au minimum :

```text
Loaded definitions: 1
SG1_TokraOrganic_GoauldObservation
Phases: 7
Offer variants: 3
Repeat factor: 0.25
Difficulty: ThreatPointsSnapshot
Threat snapshot: <valeur> -> <valeur> (x1.00)
```

Sur une carte jouable, la valeur de menace doit être positive.

## 5. Variantes RP et anti-répétition

Utiliser successivement :

```text
Debug actions menu
→ GateRim SG-1
→ Tok'ra ops: force observation offer
```

Après chaque offre, utiliser le menu de debug Tok'ra pour réinitialiser proprement le framework, puis forcer une nouvelle offre.

Répéter les occurrences jusqu'à avoir observé les trois variantes, avec un maximum conseillé de douze offres, et noter les textes reçus.

Résultats attendus :

- le rapport confirme que trois variantes valides sont chargées ;
- les différentes variantes apparaissent au fil des offres ; l'absence statistique d'une variante après douze tirages doit être signalée, mais ne constitue pas seule un échec si l'anti-répétition immédiate fonctionne ;
- deux occurrences consécutives ne réutilisent pas la même variante lorsque plusieurs variantes valides existent ;
- le délai `{0}` est correctement remplacé ;
- les formulations restent RP et ne révèlent ni poids, ni index, ni points de menace.

Le rapport technique Tok'ra doit indiquer :

```text
Mission Def: SG1_TokraOrganic_GoauldObservation
phase: offered
Offer variant: 0, 1 ou 2
```

## 6. Flux complet de l'observation pilote

Forcer une offre puis suivre le flux joueur normal :

1. accepter depuis le communicateur sécurisé alimenté ;
2. récupérer le dispositif livré ;
3. le déployer au point périphérique marqué ;
4. maintenir l'opérateur pendant l'observation ;
5. vérifier que la phase annonce `4` heures et reste perceptible en vitesse maximale ;
6. replier le dispositif ;
7. revenir au communicateur ;
8. transmettre les données ;
9. vérifier la réussite, l'XP et la confiance.

Résultats attendus :

- aucun changement visible du flux validé avant `0.3.23-dev`, hors durée rééquilibrée ;
- une nouvelle occurrence initialise `observation work` à `10000/10000` ticks dans le rapport technique ;
- le message de déploiement annonce quatre heures de travail et la phase ne disparaît plus en quelques secondes à vitesse maximale ;
- le rapport technique passe au moins par `offered`, `accepted` et `ready` selon l'état commun ;
- l'occurrence est nettoyée après résolution ;
- l'archétype redevient ultérieurement éligible.

## 7. Sauvegarde et migration

Tester séparément :

- sauvegarde/rechargement pendant l'offre ;
- sauvegarde/rechargement après acceptation et avant la fin de l'observation ;
- chargement d'une sauvegarde `0.3.22-dev` contenant une observation active si une telle sauvegarde est disponible.

Résultats attendus :

- aucune nouvelle offre ni nouveau texte n'est tiré au chargement ;
- la phase et la menace capturée persistent ;
- l'objectif, le point d'observation, le travail restant et le délai restent inchangés ;
- une occurrence déjà commencée sous `r1` conserve son total enregistré au lieu d'être artificiellement rallongée ;
- une nouvelle occurrence `r2` utilise les `10000` ticks configurés dans le Def ;
- une ancienne occurrence reçoit les données génériques manquantes sans recommencer la mission.

## 8. Difficulté adaptative — fondation

Comparer le rapport du framework sur :

- une jeune colonie pauvre ;
- une colonie ou sauvegarde de test matériellement plus riche.

Résultat attendu : `Threat snapshot` est sensiblement plus élevé sur la colonie riche.

Ce jalon capture et persiste la référence de difficulté. L'observation ne génère pas d'ennemis et ne doit pas artificiellement modifier son gameplay pour exploiter ces points. Une prochaine migration de mission comportant une menace validera leur consommation concrète.

## 9. Régression des opérations encore héritées

Forcer successivement les trois opérations non migrées :

```text
Debug actions menu
→ GateRim SG-1
→ Tok'ra ops: force intelligence offer

Debug actions menu
→ GateRim SG-1
→ Tok'ra ops: force wounded agent offer

Debug actions menu
→ GateRim SG-1
→ Tok'ra ops: force medical handoff offer
```

Pour chacune : accepter, avancer au moins une phase, sauvegarder/recharger si possible, puis résoudre ou réinitialiser.

Résultats attendus :

- elles utilisent toujours leur flux C# existant ;
- leurs textes, objectifs, récompenses et conséquences restent inchangés ;
- le rapport technique les identifie comme `legacy C#` ;
- aucune donnée générique de l'observation précédente ne fuit vers elles.

## 10. Vérification finale

- scénario SG et scénario vanilla chargés sans régression ;
- aucun outil technique visible hors mode développeur ou option avancée GateRim SG-1 ;
- `Player.log` propre pour le périmètre testé.

Validation locale terminée sur la révision `r2` : contrôle de cohérence, rebuild, chargement des Defs, rapport technique, variantes RP, anti-répétition, flux complet de l'observation, sauvegarde/recharge, migration prudente, captures de menace différenciées, régressions des trois opérations héritées et `Player.log` propre. La durée de l'observation est validée à `10000` ticks, soit quatre heures en jeu.
