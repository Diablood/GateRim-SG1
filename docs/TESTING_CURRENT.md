# Tests du jalon actif

Jalon : `0.3.14-dev - Audit Tok'ra identity through death and resurrection`

Branche attendue : `feature/tokra-death-resurrection-audit`

Version de DLL attendue : `0.3.14.0`

Statut : validation fonctionnelle terminée sur `0.3.14-dev-r1` ; aucun correctif C# requis ; jalon clôturé et publié sous `v0.3.14-dev`.

## Résultat final validé

- Rebuild et chargement validés avec la DLL `0.3.14.0`.
- Matrice complète validée avec l'hôte actif au moment de la mort.
- Matrice complète validée avec le symbiote actif au moment de la mort.
- Cadavre, tombe, sauvegarde/recharge et résurrection conservent les deux identités et la personnalité active.
- Aucun gizmo n'est exposé pendant la mort ou l'inhumation ; un gizmo unique revient après résurrection.
- Les dix basculements post-résurrection ne provoquent aucune perte, duplication ou accumulation de compétences.
- La progression, les passions, les noms et les backstories restent stables après sauvegarde/recharge.
- Extraction et réimplantation après résurrection validées.
- Régressions Goa'uld et humain ordinaire validées.
- `Player.log` propre.
- Décision finale : conserver l'architecture existante sans code spécifique au cadavre, à la tombe ou à la résurrection.

## 1. Préparation et chargement

1. Créer la branche depuis le tag publié `v0.3.13-dev`.
2. Extraire l'archive à la racine du dépôt.
3. Effectuer un rebuild forcé :

```powershell
.\build.cmd "D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed" -t:Rebuild
```

Si `build.cmd` ne transmet pas l'argument supplémentaire, utiliser directement :

```powershell
dotnet build .\Source\GateRimSG1\GateRimSG1.csproj `
    -t:Rebuild `
    -p:RimWorldManagedDir="D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
```

4. Lancer RimWorld avec GateRim SG-1 et Biotech.
5. Vérifier que la DLL chargée porte la version `0.3.14.0`.
6. Attendre le menu principal et contrôler l'absence d'erreur rouge.

Aucun changement de comportement n'est attendu avant les tests : cette première révision prépare un audit ciblé de l'architecture existante.

## 2. Créer un Tok'ra de référence

1. Mettre le jeu en pause.
2. Utiliser `Debug actions menu` → `Spawn pawn` → `SG1_TokraVoluntaryHost`.
3. Vérifier que le pawn est directement contrôlé par le joueur.
4. Noter dans un tableau de test :
   - nom de l'hôte ;
   - nom du symbiote ;
   - enfance et âge adulte de chaque identité ;
   - personnalité active ;
   - niveaux, XP et passions de plusieurs compétences ;
   - `hostIdentitySource` et `generatedHostOrigin` dans le rapport technique si disponibles.
5. Sauvegarder une copie de référence avant toute mort.

Résultats attendus : les deux identités sont distinctes, le gizmo fonctionne et l'état correspond au jalon `0.3.13-dev` validé.

## 3. Matrice A — mort avec l'hôte actif

1. Vérifier que l'hôte est actif.
2. Utiliser `Debug actions menu` → `Kill`, puis cliquer sur le Tok'ra.
3. Inspecter le cadavre sans le détruire ni retirer son Hediff par un autre outil debug.
4. Sauvegarder avec le cadavre présent sur la carte.
5. Quitter complètement RimWorld puis recharger.

Résultats attendus :

- le pawn reste contenu dans son cadavre normal ;
- le cadavre utilise une présentation cohérente avec le nom actif au moment de la mort ;
- aucune seconde identité de cadavre n'est créée ;
- aucun gizmo de personnalité n'est exposé sur le cadavre ou le pawn mort ;
- aucun nom, backstory, niveau, XP ou passion n'est modifié après rechargement ;
- aucune erreur liée à `GoauldSymbioteData`, `BackstorySkillOffsetUtility`, `Corpse` ou `PostLoadInit` n'apparaît.

## 4. Tombe et sauvegarde avec l'hôte actif

1. Construire ou désigner une tombe vanilla.
2. Autoriser l'inhumation normale du cadavre de la matrice A.
3. Inspecter la tombe et son contenu.
4. Sauvegarder avec le pawn enterré.
5. Quitter complètement RimWorld puis recharger.
6. Ouvrir ou vider la tombe par le flux vanilla afin de récupérer le cadavre pour la résurrection.

Résultats attendus :

- la tombe et son contenu restent identifiables de manière cohérente ;
- l'inhumation ne reroule ni n'efface les identités ;
- le rechargement ne modifie pas la personnalité active ni les compétences ;
- aucun gizmo de personnalité n'apparaît pendant l'état mort ou enterré.

## 5. Résurrection avec l'hôte actif

1. Une fois le cadavre accessible, utiliser `Debug actions menu` → `Resurrect`, puis cliquer sur le cadavre.
2. Mettre immédiatement le jeu en pause.
3. Inspecter les onglets Bio, Social et Santé.
4. Comparer les données avec la copie de référence.
5. Utiliser le gizmo pour activer le symbiote, puis revenir à l'hôte.
6. Répéter dix basculements.
7. Sauvegarder, quitter complètement et recharger.

Résultats attendus :

- le pawn ressuscité conserve la même identité active qu'avant sa mort ;
- les deux noms et les backstories correspondent exactement à la référence ;
- le gizmo réapparaît une seule fois parce que le pawn est à nouveau vivant et directement contrôlé ;
- les compétences, XP et passions n'ont subi aucune perte, duplication ou cumul ;
- les dix basculements restent réversibles ;
- la sauvegarde post-résurrection conserve l'ensemble de l'état.

## 6. Matrice B — mort avec le symbiote actif

1. Recharger la copie de référence ou générer un second `SG1_TokraVoluntaryHost`.
2. Activer le symbiote avec le gizmo.
3. Noter le nom visible, les backstories actives et les compétences.
4. Refaire exactement les sections 3 à 5 : `Kill`, cadavre, sauvegarde/recharge, tombe, récupération du cadavre et `Resurrect`.

Résultats attendus :

- la présentation du cadavre et de la tombe suit le nom actif du symbiote sans perdre l'identité de l'hôte ;
- le symbiote reste la personnalité active après résurrection ;
- l'hôte et le symbiote peuvent à nouveau être basculés normalement ;
- la progression commune et les écarts de backstory restent identiques à l'état de référence ;
- aucun retour forcé vers l'hôte ne se produit uniquement à cause de la mort ou de l'inhumation.

Un retour automatique vers l'hôte serait un changement de comportement et doit être signalé, même s'il ne provoque pas d'erreur.

## 7. Extraction après résurrection

1. Sur l'un des Tok'ra ressuscités, activer le symbiote.
2. Utiliser le flux d'extraction Tok'ra déjà validé.
3. Inspecter l'ancien hôte et le symbiote extrait.
4. Réimplanter le symbiote dans un autre colon si le flux de test le permet.

Résultats attendus :

- l'extraction restaure l'identité de l'hôte comme auparavant ;
- l'ancien hôte conserve son nom, ses backstories et ses compétences ;
- le symbiote conserve son nom et sa carrière Tok'ra ;
- aucune donnée propre au cadavre ou à la tombe n'est transférée ;
- la réimplantation utilise l'identité réelle du nouvel hôte.

## 8. Régressions et journal

Contrôler au minimum :

- un Tok'ra vivant jamais tué ;
- un Goa'uld tué puis ressuscité ;
- un pawn humain sans symbiote tué puis ressuscité ;
- sauvegarde/recharge avec l'hôte actif et avec le symbiote actif ;
- absence d'outil debug GateRim SG-1 visible lorsque les options avancées sont désactivées.

Rechercher dans `Player.log` :

```text
GoauldSymbioteData
BackstorySkillOffsetUtility
TokraPlayerControlUtility
TryTogglePersonality
PostLoadInit
Corpse
Grave
Resurrect
NullReferenceException
```

Résultat attendu : aucune nouvelle erreur rouge, aucun avertissement répété et aucune dérive d'identité ou de compétences.

## 9. Décision après test

- Décision appliquée : toutes les matrices sont validées et aucun code n'a été ajouté ; le modèle persistant existant couvre correctement ce cycle de vie.
- Si un défaut est reproduit, fournir la séquence exacte, l'identité active, le moment de la sauvegarde et `Player.log` avant de corriger.
- Toute correction doit réutiliser `GoauldSymbioteData`, `TokraPlayerControlUtility` et le service existant de progression partagée ; aucun second système d'identité n'est autorisé.
