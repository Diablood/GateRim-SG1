# Tests du dernier jalon clôturé

Jalon : `0.3.26-dev - Migrate wounded-agent care to mission framework`

Branche attendue : `feature/wounded-agent-mission-migration`

Révision locale validée : `0.3.26-dev-r2`

Version de DLL validée : `0.3.26.0`

Statut : validation complète terminée sur `r2`. Le flux de l'agent blessé, la difficulté adaptative, les échecs, la persistance, la récurrence, les variantes RP, les régressions et le chargement corrigé des trois MissionDefs sont validés. Jalon publié sous `v0.3.26-dev`.

## Résultat final

La révision `r1` a validé le flux complet de l'agent blessé. Son chargement a également révélé que l'objectif accéléré de la récupération de renseignements avait perdu son champ XML `<workTicks>5000</workTicks>` pendant l'assemblage de l'archive. Le validateur a correctement désactivé cet archétype sans fallback C# silencieux.

La révision `r2` restaure uniquement ce champ XML. La validation finale confirme :

- trois MissionDefs chargés sans erreur ;
- récupération de renseignements accélérée à `5000/5000` ticks et récompense `Intellectual +500` inchangée ;
- flux de l'agent blessé jusqu'à la sortie réelle de carte après `5000` ticks de stabilité ;
- seuils médicaux, affection optionnelle et snapshot de menace persistants ;
- difficulté adaptative contrôlée sur une colonie faible et une colonie avancée ;
- mort, capture, perte, expiration et échec de départ correctement résolus ;
- sauvegarde/recharge aux différentes phases sans reroll ni perte de référence ;
- variantes RP, anti-répétition et récurrence validées ;
- observation, renseignements et remise médicale sans régression ;
- aucun outil debug visible en jeu normal ;
- `Player.log` final propre.

Les sections suivantes conservent le protocole détaillé comme référence durable de régression.

## 1. Contrôles de dépôt

Le rebuild forcé de la DLL `0.3.26.0` a été validé avec `r1`. La révision `r2` ne modifie aucun fichier C# et a été validée après redémarrage complet de RimWorld.

Depuis la racine du dépôt :

```powershell
git branch --show-current
git status --short

./tools/check-project-consistency.cmd `
    -ExpectedVersion 0.3.26-dev `
    -ExpectedBackstoryCount 83

if ($LASTEXITCODE -ne 0) {
    throw "Le contrôle de cohérence du projet a échoué."
}

git diff --check
```

Résultats validés : branche correcte et aucune erreur de cohérence. Aucun nouveau rebuild n’a été nécessaire pour `r2`.

## 2. Correctif ciblé `r2` au chargement

Après extraction, fermer complètement RimWorld puis le relancer afin de recharger les Defs.

Au chargement d’une partie, `Player.log` ne doit plus contenir :

```text
Tok'ra intelligence recovery operation disabled: SG1_TokraOrganic_IntelligenceRecovery is incomplete: accelerated analysis work ticks.
```

Ouvrir ensuite :

```text
Debug actions menu
→ GateRim SG-1
→ Mission framework: inspect definitions
```

Résultats attendus :

```text
Loaded definitions: 3
SG1_TokraOrganic_IntelligenceRecovery
Objective accelerated/AnalyzeThing: target=SG1_TokraOrganicDeadDrop, job=SG1_AnalyzeTokraOrganicIntelligence, work=5000, skill=Intellectual
SG1_TokraOrganic_WoundedAgentCare
```

Forcer une nouvelle récupération de renseignements, choisir la méthode accélérée et confirmer :

- progression `5000/5000` ticks ;
- récompense totale `Intellectual +500` inchangée ;
- interférence et patrouille adaptative toujours fonctionnelles ;
- aucune désactivation d’archétype dans `Player.log`.

Le flux complet de l’agent blessé n’a pas besoin d’être rejoué : son test fonctionnel `r1` reste valide, car aucun fichier C# ni aucune donnée de cette mission n’a changé en `r2`.

## 3. Chargement du framework

Démarrer RimWorld avec le mode développeur ou l'option avancée GateRim SG-1, puis lancer :

```text
Debug actions menu
→ GateRim SG-1
→ Mission framework: inspect definitions
```

Le rapport doit notamment afficher :

```text
Loaded definitions: 3
SG1_TokraOrganic_WoundedAgentCare
Phases: 6
Offer variants: 3
Success variants: 3
Runtime texts: 11
Repeat factor: 0.25
Difficulty: ThreatPointsScaled
Pawn care: kind=SG1_TokraVoluntaryHost, initialHediff=SG1_TokraWoundedAgentSymbioteShock, recoveryHediff=SG1_TokraWoundedAgentPostShockRecovery
Pawn care timing: stable=5000, departureGrace=60000 ticks
Pawn care thresholds: moving=0.5, consciousness=0.5, health=0.55, bleed=0.001
Pawn care illness: def=Flu, chance=0.25-0.45, severity=0.3-0.4
```

Vérifier aussi les quatre plages de récurrence par confiance et l'absence d'erreur de configuration dans `Player.log`.

## 4. Flux normal complet

Préparer une carte avec :

- un communicateur sécurisé Tok'ra alimenté et accessible ;
- au moins un colon capable d'utiliser le communicateur ;
- un lit médical appartenant à la colonie ;
- un soigneur disponible.

Forcer l'offre :

```text
Debug actions menu
→ GateRim SG-1
→ Tok'ra ops: force wounded agent offer
```

Vérifier :

1. Une des trois lettres RP est affichée et le communicateur ne révèle que l'opération active.
2. L'acceptation fait apparaître un hôte Tok'ra blessé et à terre près de la colonie.
3. Le pawn possède le choc de symbiote configuré et la lettre demande explicitement un sauvetage, un lit médical et un véritable soin.
4. Le rapport ou le log indique le snapshot de menace mis à l'échelle et les paramètres d'affection optionnelle utilisés.
5. Sans lit médical et sans choc soigné, la mission ne progresse pas.
6. Après sauvetage dans un lit médical du joueur et soin réel du choc, le choc disparaît et la récupération post-choc apparaît.
7. Les soins vanilla ordinaires restent nécessaires si les blessures ou la maladie l'exigent.
8. Lorsque tous les seuils médicaux sont satisfaits, le pawn reste stable pendant `5000` ticks avant de recevoir l'ordre de départ.
9. La mission ne réussit pas au moment de la stabilisation : elle réussit uniquement lorsque le pawn quitte réellement la carte vivant.
10. La confiance augmente de `3` et une des trois variantes de réussite est affichée.

## 5. Difficulté adaptative

Tester une nouvelle occurrence sur une colonie faible, puis sur une colonie avancée.

La définition utilise :

```text
scaledPoints = clamp(baseThreatPoints × 0.35, 180, 700)
```

L'affection optionnelle doit interpoler selon ce snapshot :

- au minimum : chance proche de `0.25`, sévérité proche de `0.30` ;
- au maximum : chance proche de `0.45`, sévérité proche de `0.40`.

La maladie reste probabiliste : il n'est pas obligatoire qu'elle apparaisse à chaque occurrence. Vérifier dans le log que la chance et la sévérité calculées augmentent avec le snapshot et qu'elles utilisent la valeur capturée lors de l'offre, pas une valeur recalculée après modification de la richesse.

## 6. Sauvegarde et rechargement

Valider séparément une sauvegarde/recharge :

- pendant l'offre non acceptée ;
- après l'arrivée, avant le premier soin ;
- pendant la récupération post-choc ;
- pendant les `5000` ticks de stabilité ;
- après l'ordre de départ, avant la sortie de carte.

Après rechargement, vérifier la même mission, le même pawn, les mêmes Hediffs, le même snapshot, la même phase et les mêmes échéances. Aucun reroll de maladie ou de paramètres ne doit se produire.

Lorsqu'une sauvegarde `v0.3.25-dev` contenant déjà cette opération est disponible, la charger comme test de migration : le runtime générique doit être créé sans perdre le pawn ni la progression spécialisée. Une occurrence déjà en récupération doit rejoindre la phase technique `recovering` au tick suivant. Le snapshot initialisé lors de cette migration est conservé ensuite ; il ne doit plus être recalculé.

## 7. Échecs

Sur des occurrences séparées, vérifier :

- mort du pawn : échec et texte de mort ;
- capture comme prisonnier : échec et texte de capture ;
- disparition ou transfert hors de la carte attendue : échec et texte de perte ;
- dépassement du délai de soins : échec et récupération RP par les Tok'ra ;
- impossibilité de quitter la carte jusqu'à la fin de la grâce de `60000` ticks : échec de départ.

Chaque échec doit appliquer `-2` de confiance. Un pawn encore vivant ne doit pas rester bloqué comme objectif de mission après résolution, sauf le prisonnier capturé volontairement conservé par le joueur.

## 8. Variantes et récurrence

Forcer plusieurs offres et plusieurs réussites :

- les trois variantes d'offre doivent pouvoir apparaître ;
- les trois variantes de réussite doivent pouvoir apparaître ;
- la même variante de réussite ne doit pas se répéter immédiatement lorsqu'une alternative existe ;
- après résolution, le communicateur revient immédiatement à son état RP générique ;
- l'archétype redevient éligible selon la plage cachée correspondant au palier de confiance ;
- le dernier archétype joué conserve sa pénalité de poids `0.25`.

## 9. Régressions

Valider au minimum :

- une mission d'observation complète ;
- une récupération de renseignements prudente ou accélérée ;
- une remise médicale complète ;
- aucun changement de comportement visible sur les Tok'ra générés hors de cette opération ;
- aucun outil debug visible en jeu normal.

## 10. Journal final

Fermer le jeu après les tests et vérifier `Player.log` :

- aucune exception ;
- aucune erreur de Def ;
- aucune erreur de traduction ;
- aucun fallback silencieux pour la mission de l'agent blessé ;
- logs techniques uniquement lorsque le mode développeur ou l'option avancée l'autorise.
