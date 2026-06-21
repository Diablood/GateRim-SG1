# Tests du jalon actif

Jalon : `0.3.24-dev - Complete observation mission Def migration`

Branche attendue : `feature/observation-mission-def-cleanup`

Révision locale validée : `0.3.24-dev-r1`

Version de DLL attendue : `0.3.24.0`

Statut : validation fonctionnelle terminée ; jalon clôturé et publié sous `v0.3.24-dev`.

## Résultat final validé

- contrôle de cohérence et rebuild `0.3.24.0` réussis ;
- chargement propre du MissionDef et de toutes ses références `ThingDef`, `JobDef` et `SkillDef` ;
- rapport développeur conforme, dont `26` textes d'exécution, la plage `240000–480000`, les quatre durées et `Intellectual +250` ;
- flux complet validé avec `500 / 10000 / 500 / 1000` ticks ;
- XP active, récompense finale, variantes de réussite et anti-répétition validées ;
- sauvegarde/recharge, reprise d'interruption et migration depuis `v0.3.23-dev` validées ;
- échec par expiration et perte de l'objectif physique validé sans récompense finale ;
- récurrence lue depuis le MissionDef ;
- trois opérations organiques encore héritées sans régression ;
- interfaces joueur et frontière debug conformes ;
- `Player.log` propre.

Le protocole ci-dessous est conservé comme référence détaillée de régression.

## 1. Contrôle du dépôt

Depuis la racine du dépôt :

```powershell
git branch --show-current
git describe --tags --exact-match v0.3.23-dev

./tools/check-project-consistency.cmd `
    -ExpectedVersion 0.3.24-dev `
    -ExpectedBackstoryCount 83

if ($LASTEXITCODE -ne 0) {
    throw "Le contrôle de cohérence du projet a échoué."
}

git diff --check
```

Résultats attendus :

- branche `feature/observation-mission-def-cleanup` ;
- base créée depuis `v0.3.23-dev` ;
- version documentaire `0.3.24-dev` ;
- version d'assembly `0.3.24.0` ;
- `83` backstories ;
- aucune tabulation littérale ou erreur de fin de ligne.

La commande `git describe --tags --exact-match v0.3.23-dev` ne réussit que tant qu'aucun commit de travail n'a encore été créé. Après un commit local, utiliser plutôt :

```powershell
git merge-base --is-ancestor v0.3.23-dev HEAD
```

## 2. Rebuild forcé

```powershell
dotnet build ./Source/GateRimSG1/GateRimSG1.csproj `
    -t:Rebuild `
    -p:RimWorldManagedDir="D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
```

Résultats attendus :

- build réussi ;
- DLL régénérée dans `1.6/Assemblies/` ;
- version `0.3.24.0` ;
- aucune erreur ou alerte nouvelle liée au framework de missions.

## 3. Chargement des Defs

Lancer RimWorld jusqu'au menu principal, puis charger une carte de test disposant d'un communicateur sécurisé Tok'ra alimenté.

Vérifier l'absence d'erreur rouge concernant :

- `SG1_TokraOrganic_GoauldObservation` ;
- `GateRimSG1.Missions.GateRimMissionDef` ;
- les références `ThingDef`, `JobDef` et `SkillDef` de l'observation, y compris la compétence de récompense finale ;
- les textes d'exécution et variantes de résultat ;
- les traductions anglaises et françaises.

## 4. Rapport développeur de la définition

Activer le mode développeur et ouvrir :

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
Success variants: 3
Runtime texts: 26
Repeat factor: 0.25
Recurrence delay: 240000-480000 ticks
Skill XP reward: Intellectual +250
Difficulty: ThreatPointsSnapshot
Objective accepted/DeployThing: target=SG1_TokraObservationDevice, secondary=SG1_TokraObservationPoint, job=SG1_DeployTokraObservationDevice, work=500
Objective observing/MaintainOperator: target=SG1_TokraObservationPoint, work=10000, skill=Intellectual, xp/tick=0.04
Objective ready/RecoverAndTransmit: target=SG1_TokraObservationDevice, job=SG1_TransmitTokraObservationData, work=500, secondaryWork=1000
```

Les lignes peuvent contenir des champs supplémentaires, mais aucune valeur observation ne doit provenir d'une définition C# de secours.

## 5. Offre et acceptation

Forcer :

```text
Debug actions menu
→ GateRim SG-1
→ Tok'ra ops: force observation offer
```

Vérifier :

- offre reçue normalement au communicateur ;
- action d'acceptation présente ;
- acceptation réussie avec un colon capable en Travail intellectuel ;
- dispositif livré et point d'observation créé ;
- lettre de cible et message d'acceptation corrects ;
- rapport technique associé à `SG1_TokraOrganic_GoauldObservation`.

## 6. Flux complet et durées configurées

Suivre le flux normal sans forcer la résolution :

1. récupérer le dispositif ;
2. choisir l'action de déploiement ;
3. vérifier un travail de déploiement de `500` ticks ;
4. maintenir le même opérateur au point d'observation ;
5. vérifier `10000/10000` ticks au début de l'observation ;
6. confirmer que le message annonce quatre heures ;
7. vérifier la progression du Travail intellectuel pendant cette phase ;
8. replier le dispositif après `500` ticks ;
9. retourner au communicateur ;
10. transmettre pendant `1000` ticks ;
11. recevoir la lettre de réussite et la récompense attendue.

Résultats attendus :

- aucune phase ne reprend les anciennes constantes `2500–5000` ticks ;
- les quatre durées correspondent au rapport développeur ;
- le dispositif, le marqueur et les deux jobs correspondent aux Defs indiqués ;
- l'opérateur gagne de l'expérience dans la compétence active configurée, sans gain si cette compétence est désactivée ;
- les `250` XP finaux sont accordés une seule fois à la compétence déclarée dans `skillXpRewards` ;
- la conséquence de confiance reste inchangée ;
- l'occurrence est nettoyée après résolution.

## 7. Interruption, sauvegarde et reprise

Tester séparément :

- sauvegarde/rechargement pendant l'offre ;
- sauvegarde/rechargement après déploiement avec observation partiellement avancée ;
- interruption du travail puis reprise depuis le point d'observation ;
- sauvegarde/rechargement pendant la transmission ;
- reprise de la transmission depuis le communicateur.

Résultats attendus :

- aucun reroll de texte ou d'objectif au chargement ;
- total et progression déjà enregistrés conservés ;
- aucune prolongation artificielle d'une occurrence existante ;
- reprise avec les mêmes Defs, jobs et durées ;
- résolution normale après la reprise.

Une sauvegarde active créée sous `v0.3.23-dev` est recommandée pour vérifier que la migration prudente fonctionne encore sans définition C# de secours.

## 8. Variantes de résultat et anti-répétition

Réaliser plusieurs réussites, en utilisant les outils debug uniquement pour accélérer les phases lorsque nécessaire.

Résultats attendus :

- les trois textes `GR_TokraObservation_SuccessText1`, `2` et `3` peuvent apparaître ;
- une variante ne se répète pas immédiatement lorsqu'au moins une alternative existe ;
- le choix vient de `successLetterTexts` dans le MissionDef ;
- sauvegarder puis recharger ne reroule pas une variante déjà sélectionnée.

## 9. Échecs

Tester deux cas :

- laisser expirer le délai après acceptation ;
- détruire le dispositif ou le point d'observation avant la transmission.

Résultats attendus :

- lettre d'échec correcte pour le délai ou la perte du dispositif ;
- conséquence de confiance inchangée ;
- nettoyage complet de l'occurrence ;
- aucune ancienne clé ou valeur de secours utilisée ;
- aucun XP final accordé sur un échec.

## 10. Récurrence après observation

Après une réussite ou un échec, consulter le rapport technique Tok'ra.

Résultat attendu : le prochain délai est tiré dans la plage XML `240000–480000` ticks. Le joueur ne voit pas cette plage en jeu normal.

Il n'est pas nécessaire d'attendre naturellement toute la durée. Le rapport technique ou une inspection de sauvegarde peut servir de preuve.

## 11. Régression des opérations encore héritées

Forcer successivement :

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

Vérifier pour chacune :

- offre, acceptation et phase principale toujours accessibles ;
- textes, objectifs et récompenses inchangés ;
- définition toujours signalée comme `legacy C#` dans le rapport technique ;
- leur planification continue d'utiliser la logique historique par niveau de confiance ;
- aucune donnée de l'observation ne fuit vers ces opérations.

## 12. Vérification finale

- scénario SG et scénario vanilla chargés sans régression ;
- aucun outil technique visible hors mode développeur ou option avancée GateRim SG-1 ;
- textes joueur naturels et non techniques ;
- `Player.log` propre pour tout le périmètre testé.

Validation fonctionnelle terminée sur `r1`. Le tag final publié est `v0.3.24-dev`.
