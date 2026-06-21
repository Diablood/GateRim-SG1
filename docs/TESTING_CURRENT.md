# Tests du dernier jalon clôturé

Jalon : `0.3.27-dev - Migrate medical handoff to mission framework`

Branche attendue : `feature/medical-handoff-mission-migration`

Révision locale validée : `0.3.27-dev-r1`

Version de DLL validée : `0.3.27.0`

Statut : validation complète terminée sur `r1`. Flux normal, ressources, échecs, conséquence post-remise, persistance, variantes RP, récurrence, régressions et journal final validés.

Jalon publié sous `v0.3.27-dev`.

## Résultat final

La validation finale confirme :

- quatre MissionDefs chargés sans erreur ;
- arrivée différée, rencontre, dialogue et remise exacte de deux médicaments industriels accessibles ;
- restrictions correctes pour les ressources insuffisantes ou inaccessibles, l'incapacité sociale, la réservation et l'absence de chemin ;
- récompense `Social +350` et confiance `+2` après remise ;
- échec avec `-1` de confiance en cas de mort, capture, perte ou expiration avant remise ;
- conséquence supplémentaire de `-2` si l'agent meurt après une remise réussie mais avant sa sortie ;
- sauvegarde/rechargement aux différentes phases sans duplication de pawn, de délai, de ressource ou de conséquence ;
- trois variantes d'offre et trois variantes de réussite avec anti-répétition immédiate ;
- rééligibilité, délais cachés contextuels et pénalité du dernier archétype ;
- observation, récupération de renseignements et agent blessé sans régression ;
- aucun outil debug visible en jeu normal ;
- `Player.log` final propre.

Les sections suivantes conservent le protocole détaillé comme référence durable de régression.

## 1. Contrôles de dépôt et rebuild

Depuis la racine du dépôt :

```powershell
git branch --show-current
git status --short

./tools/check-project-consistency.cmd `
    -ExpectedVersion 0.3.27-dev `
    -ExpectedBackstoryCount 83

if ($LASTEXITCODE -ne 0) {
    throw "Le contrôle de cohérence du projet a échoué."
}

git diff --check

dotnet build ./Source/GateRimSG1/GateRimSG1.csproj `
    -t:Rebuild `
    -p:RimWorldManagedDir="D:/SteamLibrary/steamapps/common/RimWorld/RimWorldWin64_Data/Managed"
```

Vérifier la branche, la version `0.3.27.0` et l'absence d'erreur de cohérence ou de compilation.

## 2. Chargement du framework

Fermer complètement RimWorld avant le premier lancement afin de recharger les Defs. Ouvrir ensuite :

```text
Debug actions menu
→ GateRim SG-1
→ Mission framework: inspect definitions
```

Le rapport doit notamment contenir :

```text
Loaded definitions: 4
SG1_TokraOrganic_MedicalSupplyHandoff
Phases: 5
Offer variants: 3
Success variants: 3
Runtime texts: 24
Repeat factor: 0.25
Difficulty: None
Skill XP reward: Social +350
Handoff: liaison=SG1_TokraVoluntaryHost, arrival=2500-5000 ticks, departureGrace=60000 ticks
Handoff post-death trust: -2
Objective ready/DeliverThing: target=MedicineIndustrial, secondary=none, job=SG1_TalkToTokraMedicalSupplyLiaison, count=2, work=0, secondaryWork=0, skill=Social, xp/tick=0
```

Vérifier également les quatre plages de récurrence par confiance et l'absence d'erreur de configuration dans `Player.log`.

## 3. Flux normal complet

Préparer une carte avec :

- un communicateur sécurisé Tok'ra alimenté et accessible ;
- un colon capable de Social ;
- au moins deux unités accessibles de médicament industriel.

Forcer l'offre :

```text
Debug actions menu
→ GateRim SG-1
→ Tok'ra ops: force medical handoff offer
```

Valider :

1. Une des trois lettres RP apparaît et ne révèle aucune logique interne.
2. L'acceptation depuis le communicateur annonce une arrivée différée comprise entre `2500` et `5000` ticks.
3. L'agent généré utilise `SG1_TokraVoluntaryHost` et rejoint le point de rencontre choisi par l'adaptateur.
4. Avant son arrivée, l'interaction indique que l'agent est encore en route.
5. À l'arrivée, la lettre et le statut annoncent la fenêtre de `15000` ticks.
6. Un colon capable de Social peut ouvrir le dialogue par clic droit.
7. Le dialogue affiche la quantité configurée et le bouton indique `2` médicaments.
8. La confirmation retire exactement deux médicaments industriels accessibles, y compris à travers plusieurs piles.
9. La mission réussit immédiatement après la remise, accorde `Social +350` au négociateur et `+2` de confiance.
10. Une des trois variantes de réussite est affichée, puis l'agent reçoit l'ordre de quitter la carte.

## 4. Ressources et interaction

Sur des occurrences ou sauvegardes séparées, vérifier :

- zéro ou une unité accessible : dialogue possible mais confirmation refusée avec le besoin de `2` unités ;
- deux unités interdites ou inaccessibles : elles ne sont pas comptées ;
- plusieurs piles totalisant au moins deux unités : consommation exacte sans détruire le surplus ;
- colon incapable de Social : option désactivée avec le motif configuré ;
- aucun chemin sûr : option désactivée ;
- agent déjà réservé par un autre colon : option désactivée ;
- JobDef de dialogue disponible et aucune option dupliquée.

## 5. Échecs avant la remise

Sur des occurrences distinctes, valider :

- expiration de la fenêtre : départ de l'agent, lettre d'échec et `-1` de confiance ;
- mort avant la remise : échec et texte de mort ;
- capture comme prisonnier : échec et texte de capture ;
- disparition, destruction ou transfert hors de la carte attendue : échec et texte de perte.

Après chaque résolution, le communicateur doit revenir immédiatement à son état RP générique et aucune référence active ne doit rester bloquée.

## 6. Mort après une remise réussie

Après avoir remis les médicaments :

1. confirmer que la réussite et le `+2` de confiance sont déjà appliqués ;
2. tuer l'agent avant qu'il quitte la carte ;
3. vérifier la lettre post-remise et une variation supplémentaire de `-2` de confiance ;
4. confirmer que la mission elle-même ne repasse pas en échec et ne rend pas les médicaments.

Laisser ensuite un autre agent quitter normalement la carte et vérifier qu'aucune pénalité post-remise n'est appliquée.

## 7. Sauvegarde et rechargement

Valider séparément une sauvegarde/recharge :

- pendant l'offre non acceptée ;
- après acceptation, avant l'arrivée ;
- pendant l'approche de l'agent ;
- lorsque l'agent attend au point de rencontre ;
- après la remise, pendant le départ surveillé.

Après rechargement, conserver le même MissionDef, la même phase, le même pawn, les mêmes échéances et la même pénalité post-remise en attente. Aucun nouvel agent ni nouveau délai ne doit être tiré.

Lorsqu'une sauvegarde `v0.3.26-dev` contient déjà une remise médicale active, vérifier que le runtime générique est initialisé sans perdre l'agent, l'état ou l'échéance spécialisés.

## 8. Variantes et récurrence

Forcer plusieurs offres et réussites :

- les trois variantes d'offre doivent pouvoir apparaître ;
- les trois variantes de réussite doivent pouvoir apparaître ;
- une variante de réussite ne doit pas se répéter immédiatement lorsqu'une alternative existe ;
- l'archétype redevient éligible après réussite ou échec ;
- les délais cachés utilisent le palier de confiance courant ;
- le dernier archétype joué conserve la pénalité de poids `0.25`.

## 9. Régressions

Valider au minimum :

- une mission d'observation complète ;
- une récupération de renseignements prudente ou accélérée ;
- un accueil complet de l'agent Tok'ra blessé ;
- le communicateur n'affiche qu'une opération active ;
- aucun outil debug visible en jeu normal ;
- aucun changement sur les visiteurs Tok'ra ordinaires.

## 10. Journal final

Fermer le jeu après les tests et vérifier `Player.log` :

- aucune exception ;
- aucune erreur de Def ou de traduction ;
- quatre MissionDefs chargés ;
- aucun fallback médical silencieux ;
- logs techniques uniquement lorsque le mode développeur ou l'option avancée l'autorise.
