# Tests du dernier jalon clôturé

Jalon : `0.3.25-dev - Migrate intelligence recovery to mission framework`

Branche attendue : `feature/intelligence-recovery-mission-migration`

Révision locale validée : `0.3.25-dev-r2`

Version de DLL validée : `0.3.25.0`

Statut : validation complète terminée sur `r2`, y compris les durées finales, la persistance et la consommation de menace adaptative sur une colonie faible et une colonie avancée. Jalon publié sous `v0.3.25-dev`.

## Résultat final

La révision `r1` a validé le flux complet. La révision `r2` a uniquement rééquilibré les analyses puis validé leur rythme et leur persistance :

- méthode prudente : `10000` ticks, `Intellectual +350`, sans patrouille ;
- méthode accélérée : `5000` ticks, `Intellectual +500` au total ;
- interruption, reprise et sauvegarde/rechargement sans modification du total ni perte de progression ;
- interférence adaptative validée sur une colonie faible et une colonie avancée ;
- budget consommé égal au snapshot mis à l'échelle capturé lors de l'offre, même après modification de la colonie ;
- variantes RP, anti-répétition, échecs, récurrence, migrations, régressions et `Player.log` validés.

Les sections suivantes conservent le protocole détaillé comme référence durable de régression.

## 1. Contrôle du dépôt

Créer la branche depuis le dernier tag publié avant d'extraire le correctif :

```powershell
git status --short
git switch --detach v0.3.24-dev
git switch -c feature/intelligence-recovery-mission-migration
```

Après extraction :

```powershell
./tools/check-project-consistency.cmd `
    -ExpectedVersion 0.3.25-dev `
    -ExpectedBackstoryCount 83

if ($LASTEXITCODE -ne 0) {
    throw "Le contrôle de cohérence du projet a échoué."
}

git diff --check
```

Résultats attendus :

- branche `feature/intelligence-recovery-mission-migration` ;
- base issue de `v0.3.24-dev` ;
- version documentaire `0.3.25-dev` ;
- version d'assembly `0.3.25.0` ;
- `83` backstories ;
- aucune tabulation littérale ni erreur de fin de ligne.

## 2. Rebuild forcé

```powershell
dotnet build ./Source/GateRimSG1/GateRimSG1.csproj `
    -t:Rebuild `
    -p:RimWorldManagedDir="D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
```

Résultats attendus :

- build réussi ;
- DLL régénérée dans `1.6/Assemblies/` ;
- version `0.3.25.0` ;
- aucune nouvelle alerte liée au framework ou aux opérations Tok'ra.

## 3. Chargement des Defs

Lancer RimWorld jusqu'au menu principal, puis charger une carte disposant d'un communicateur sécurisé Tok'ra alimenté.

Vérifier l'absence d'erreur rouge concernant :

- `SG1_TokraOrganic_GoauldObservation` ;
- `SG1_TokraOrganic_IntelligenceRecovery` ;
- `GateRimSG1.Missions.GateRimMissionDef` ;
- `SG1_TokraOrganicDeadDrop` ;
- `SG1_AnalyzeTokraOrganicIntelligence` ;
- `Intellectual` ;
- `SG1_GoauldJaffaSignalPatrol` ;
- les banques de textes nommées et les délais de récurrence contextuels.

## 4. Rapport développeur

Activer le mode développeur et ouvrir :

```text
Debug actions menu
→ GateRim SG-1
→ Mission framework: inspect definitions
```

Le rapport doit annoncer `Loaded definitions: 2` et conserver la définition d'observation validée en `0.3.24-dev`.

Pour `SG1_TokraOrganic_IntelligenceRecovery`, vérifier au minimum :

```text
Phases: 6
Offer variants: 1
Runtime texts: 15
Named text banks: 3
Repeat factor: 0.25
Recurrence delay: 240000-480000 ticks
Recurrence context TokraTrust.Wary: 360000-720000 ticks
Recurrence context TokraTrust.Neutral: 240000-480000 ticks
Recurrence context TokraTrust.Cooperative: 180000-420000 ticks
Recurrence context TokraTrust.Trusted: 360000-720000 ticks
Difficulty: ThreatPointsScaled
Skill XP reward: Intellectual +350
Text bank cautiousSuccess: 3 variants
Text bank acceleratedSuccess: 3 variants
Text bank interferenceSuccess: 3 variants
Objective cautious/AnalyzeThing: target=SG1_TokraOrganicDeadDrop, job=SG1_AnalyzeTokraOrganicIntelligence, work=10000, skill=Intellectual
Objective accelerated/AnalyzeThing: target=SG1_TokraOrganicDeadDrop, job=SG1_AnalyzeTokraOrganicIntelligence, work=5000, skill=Intellectual
Consequence accelerated -> succeeded/GrantSkillXp: target=Intellectual, value=150
Consequence accelerated -> succeeded/QueueIncident: target=SG1_GoauldJaffaSignalPatrol, chance=0.35, delay=5000-12500, retry=2500
```

La ligne de snapshot doit montrer un facteur `x0.35` et une valeur mise à l'échelle comprise entre `180` et `700` points.

## 5. Offre et récupération du module

Forcer :

```text
Debug actions menu
→ GateRim SG-1
→ Tok'ra ops: force intelligence offer
```

Vérifier :

- offre et statut RP corrects au communicateur ;
- acceptation possible avec un colon valide ;
- apparition du module `SG1_TokraOrganicDeadDrop` ;
- lettre de localisation et délai corrects ;
- rapport technique associé à `SG1_TokraOrganic_IntelligenceRecovery` ;
- snapshot de menace enregistré dès l'offre et stable après sauvegarde/rechargement.

## 6. Méthode prudente

Accepter une nouvelle occurrence, récupérer le module et choisir normalement la méthode prudente au communicateur.

Vérifier :

- travail total `10000/10000` ticks ; la phase doit rester clairement perceptible à vitesse maximale ;
- job `SG1_AnalyzeTokraOrganicIntelligence` ;
- compétence `Intellectual` ;
- méthode verrouillée une fois commencée ;
- interruption puis reprise sans réinitialisation ;
- réussite avec `350` XP intellectuels accordés une seule fois ;
- variation de confiance identique à l'ancien flux ;
- aucun incident Goa'uld mis en file par la méthode prudente ;
- texte choisi dans `cautiousSuccess`.

## 7. Méthode accélérée sans interférence

Accepter une autre occurrence et choisir la méthode accélérée.

Vérifier :

- travail total `5000/5000` ticks ; la phase doit rester perceptible à vitesse maximale tout en étant nettement plus rapide que la méthode prudente ;
- même module, job et compétence que la méthode prudente ;
- `350 + 150 = 500` XP intellectuels au total, sans double attribution ;
- résultat provenant de `acceleratedSuccess` lorsque l'interférence ne se déclenche pas ;
- aucun nombre fixe d'ennemis ou de points de menace recalculé dans le flux joueur.

Le tirage naturel étant aléatoire, ce test peut être répété ou complété par le test forcé suivant.

## 8. Interférence forcée et difficulté adaptative

Sur une occurrence accélérée active, utiliser :

```text
Debug actions menu
→ GateRim SG-1
→ Tok'ra ops: force intelligence interference
```

Vérifier :

- mise en file de `SG1_GoauldJaffaSignalPatrol` ;
- délai de déclenchement compris entre `5000` et `12500` ticks ;
- nouvelle tentative configurée à `2500` ticks si l'incident ne peut pas partir immédiatement ;
- points de l'incident égaux au `scaled threat snapshot` de l'occurrence ;
- résultat provenant de `interferenceSuccess` ;
- le journal contient une ligne de la forme :

```text
Queued a small Goa'uld patrol after accelerated Tok'ra intelligence analysis; ... snapshot=<base>; points=<scaled>; fireTick=<tick>.
```

Pour confirmer l'absence de recalcul, noter le snapshot après l'offre, modifier sensiblement la richesse ou la situation de la colonie avant de forcer l'interférence, puis vérifier que `points=<scaled>` reste la valeur capturée à l'offre.

Répéter ce contrôle sur :

- une colonie faible, où la borne minimale de `180` points peut s'appliquer ;
- une colonie avancée, dont le snapshot de base doit être supérieur et dont la valeur finale reste plafonnée à `700` points.

Validation finale : les deux niveaux de colonie produisent des budgets adaptés et la patrouille consomme dans chaque cas la valeur capturée lors de l'offre.

## 9. Variantes et anti-répétition

Réaliser plusieurs réussites prudentes, accélérées simples et accélérées avec interférence.

Résultats attendus :

- chaque banque peut produire ses trois variantes ;
- une même variante ne se répète pas immédiatement dans la même banque lorsqu'une alternative existe ;
- les trois banques conservent des historiques indépendants ;
- sauvegarder puis recharger ne reroule pas un résultat déjà sélectionné ;
- aucun texte technique du MissionDef n'est visible en jeu normal.

## 10. Échecs

Tester séparément :

- expiration de l'offre ;
- expiration du délai après acceptation ;
- destruction ou disparition du module avant la fin de l'analyse.

Résultats attendus :

- texte d'échec correspondant au cas réel ;
- pénalité de confiance inchangée ;
- aucun XP de réussite ;
- aucune patrouille mise en file après l'échec ;
- nettoyage complet de l'occurrence.

## 11. Récurrence contextuelle

Après résolution, vérifier le prochain délai selon le palier de confiance actif :

- méfiante : `360000–720000` ticks ;
- neutre : `240000–480000` ticks ;
- coopérative : `180000–420000` ticks ;
- fiable : `360000–720000` ticks.

Le rapport technique ou l'inspection d'une sauvegarde suffit ; il n'est pas nécessaire d'attendre naturellement chaque délai. Ces valeurs ne doivent jamais être révélées dans l'interface joueur normale.

## 12. Sauvegarde, rechargement et migration

Tester :

- sauvegarde/rechargement pendant l'offre ;
- sauvegarde/rechargement après acceptation avant le choix de méthode ;
- sauvegarde/rechargement à mi-analyse prudente avec total conservé à `10000` ;
- sauvegarde/rechargement à mi-analyse accélérée avec total conservé à `5000` ;
- sauvegarde/rechargement après mise en file de la patrouille ;
- chargement d'une sauvegarde `v0.3.24-dev` avec une récupération de renseignements déjà offerte ou active.

Résultats attendus :

- même MissionDef, phase, méthode, total, progression et snapshot ;
- aucune prolongation, réduction ou réinitialisation d'une analyse déjà commencée ;
- aucun reroll de résultat ou d'interférence déjà résolu ;
- migration prudente sans recréer le module ni dupliquer les récompenses.

## 13. Régressions

Pour les deux opérations encore héritées, utiliser :

```text
Debug actions menu
→ GateRim SG-1
→ Tok'ra ops: force wounded agent offer

Debug actions menu
→ GateRim SG-1
→ Tok'ra ops: force medical handoff offer
```

Valider au minimum :

- mission d'observation complète, toujours pilotée par son MissionDef ;
- offre et phase principale de l'agent Tok'ra blessé ;
- offre et phase principale de la remise médicale ;
- sélection organique et anti-répétition entre les quatre archétypes ;
- scénario Équipe SG isolée et scénario vanilla ;
- aucun outil technique visible hors mode développeur ou option avancée GateRim SG-1 ;
- `Player.log` propre.

Validation complète terminée sur `r2`. Branche, tag final unique `v0.3.25-dev` et wiki publiés selon `docs/MILESTONE_PUBLICATION.md`.
