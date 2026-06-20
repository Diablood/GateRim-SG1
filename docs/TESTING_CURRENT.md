# Tests du jalon actif

Jalon : `0.3.13-dev - Generate distinct identities for pre-joined Tok'ra`

Branche attendue : `feature/tokra-generated-host-identities`

Version de DLL attendue : `0.3.13.0`

## 1. Chargement et configuration

1. Effectuer un rebuild forcé.
2. Lancer RimWorld avec GateRim SG-1 et Biotech.
3. Attendre le menu principal.
4. Vérifier l'absence d'erreurs concernant :

```text
GeneratedHostOriginDef
CulturalGeneratedHostIdentityUtility
generatedHostOrigins
TokraHostIdentitySource
SG1_GeneratedHost_OffworldHuman
SG1_OffworldHumanAdulthood
cross-reference
```

Résultat attendu : le profil Tok'ra référence une origine d'hôte humain hors-monde valide et les six nouvelles carrières adultes se chargent.

Le journal ne doit notamment contenir ni `No RimWorld.SkillDef named li`, ni exception `SkillGain.LoadDataFromXmlCustom`, ni référence manquante vers les six carrières.

## 2. `Spawn pawn` — Tok'ra généré déjà fusionné

1. Mettre le jeu en pause.
2. Utiliser `Debug actions menu` → `Spawn pawn` → `SG1_TokraVoluntaryHost`.
3. Inspecter immédiatement le pawn, son onglet Bio et son état de santé.

Résultats attendus :

- le pawn rejoint les colons comme auparavant lorsque le contexte de génération le prévoit ;
- l'identité active est celle de l'hôte ;
- le nom de l'hôte et le nom du symbiote sont différents ;
- l'hôte utilise une enfance humaine hors-monde existante ;
- l'hôte utilise l'une des six nouvelles carrières adultes humaines hors-monde ;
- le symbiote conserve une carrière adulte Tok'ra ;
- le résumé de santé affiche les deux identités ;
- un seul gizmo de personnalité est présent ;
- aucune erreur rouge ne se produit pendant la pause.

Dans le rapport technique du Hediff, vérifier si disponible :

```text
hostIdentitySource=GeneratedPreJoined
generatedHostOrigin=SG1_GeneratedHost_OffworldHuman
```

## 3. Basculement des deux identités

1. Noter le nom, l'enfance, la carrière et les compétences de l'hôte.
2. Activer le symbiote avec le gizmo.
3. Vérifier le nom Tok'ra et la carrière Tok'ra.
4. Réactiver l'hôte.
5. Répéter plusieurs fois.

Résultats attendus :

- chaque personnalité retrouve exactement son nom et son parcours ;
- les noms restent distincts ;
- aucun bonus de compétence ne s'empile ;
- l'expérience commune et les passions restent inchangées ;
- l'enfance de l'hôte reste utilisée lorsque le symbiote est actif, comme dans `0.3.11-dev`.

## 4. Variété de génération

Générer au moins dix `SG1_TokraVoluntaryHost`.

Vérifier :

- plusieurs noms d'hôtes humains hors-monde ;
- plusieurs enfances et carrières d'hôte ;
- plusieurs identités de symbiotes Tok'ra ;
- aucun pawn dont les deux identités portent exactement le même nom ;
- aucune modification des autres PawnKinds culturels.

Une répétition occasionnelle entre deux pawns différents reste possible ; seule l'identité hôte/symbiote d'un même Tok'ra doit être distincte.

## 5. Reprise du temps

1. Générer un Tok'ra déjà fusionné en pause.
2. Noter ses deux identités.
3. Reprendre le temps pendant plusieurs secondes.

Résultat attendu : le gestionnaire de noms peut finaliser le nom culturel du symbiote, mais il ne remplace pas le nom de l'hôte actif. Aucun second reroll de l'hôte, de ses backstories ou de ses compétences ne survient.

## 6. Sauvegarde et rechargement

1. Sauvegarder avec l'hôte actif.
2. Quitter complètement RimWorld puis recharger.
3. Vérifier les deux identités et le marqueur technique.
4. Activer le symbiote, sauvegarder à nouveau et refaire un rechargement complet.

Résultats attendus :

- l'identité active est conservée ;
- l'origine d'hôte, les deux noms et les deux parcours ne sont pas reroulés ;
- les compétences ne dérivent pas ;
- le marqueur reste `GeneratedPreJoined`.

## 7. Migration d'une ancienne sauvegarde

Utiliser si possible une sauvegarde `0.3.12-dev` contenant un `SG1_TokraVoluntaryHost` généré directement, notamment un cas où les deux identités avaient le même nom.

Après chargement :

- le symbiote conserve son nom Tok'ra existant et sa carrière ;
- une identité d'hôte humain hors-monde distincte est créée une seule fois ;
- le marqueur devient `GeneratedPreJoined` ;
- sauvegarder, quitter et recharger ne provoque aucun nouveau tirage.

Le système ne doit pas se baser sur l'égalité des noms pour décider de migrer.

## 8. Implantation Tok'ra réelle

1. Générer ou obtenir un symbiote Tok'ra libre.
2. Noter le nom et les backstories d'un colon existant.
3. Effectuer une implantation volontaire réelle.

Résultats attendus :

- le colon conserve exactement son identité d'hôte existante ;
- aucune identité humaine hors-monde artificielle n'est générée ;
- le rapport technique indique `hostIdentitySource=ImplantedExistingHost` ;
- le basculement hôte/symbiote reste fonctionnel.

## 9. Extraction et réimplantation d'un Tok'ra pré-fusionné

1. Générer un `SG1_TokraVoluntaryHost` et noter les deux identités.
2. Extraire le symbiote.
3. Vérifier que l'ancien hôte conserve son identité humaine hors-monde.
4. Réimplanter le même symbiote dans un autre colon.

Résultats attendus :

- le symbiote conserve son nom et sa carrière Tok'ra ;
- le nouvel hôte conserve sa propre identité ;
- l'ancienne identité d'hôte générée n'est pas appliquée au nouveau colon ;
- le marqueur devient `ImplantedExistingHost` après la vraie réimplantation ;
- aucune compétence de l'ancien hôte n'est transférée.

## 10. Génération hors carte et régressions

Contrôler lorsque disponibles :

- chef de la faction Tok'ra ;
- visiteurs ou escortes utilisant `SG1_TokraVoluntaryHost` ;
- Tok'ra passant ensuite en caravane ;
- Goa'uld et Tok'ra issus d'une implantation réelle.

Résultats attendus :

- chaque Tok'ra pré-fusionné reçoit des identités distinctes même s'il est initialisé hors de la carte principale ;
- les Tok'ra gérés par l'IA n'obtiennent toujours aucun gizmo manuel ;
- le basculement carte/caravane validé en `0.3.12-dev` reste fonctionnel ;
- les implantations et extractions Goa'uld restent inchangées.

## 11. Catalogue wiki et journal

Vérifier dans `docs/wiki/Cultural-Backstories.md` :

- `58` lignes de backstories au total ;
- les six nouvelles carrières humaines hors-monde avec nom, description et compétences.

Commande rapide :

```powershell
$wikiRows = Get-Content .\docs\wiki\Cultural-Backstories.md |
    Where-Object { $_ -match '^\| (Enfance|Âge adulte) \|' }

$wikiRows.Count
```

Résultat attendu :

```text
58
```

Contrôler enfin `Player.log`, notamment pour :

```text
GeneratedHostOriginDef
CulturalGeneratedHostIdentityUtility
TokraHostIdentitySource
GeneratedPreJoined
ImplantedExistingHost
BackstorySkillOffsetUtility
cultural name
Scribe_Defs
```
