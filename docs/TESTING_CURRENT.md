# Tests du jalon actif

Jalon : `0.3.15-dev - Add unified cultural identity diagnostics`

Branche attendue : `feature/cultural-identity-diagnostics`

Base attendue : `v0.3.14-dev`

Version de DLL attendue : `0.3.15.0`

Statut : validation fonctionnelle terminée sur `0.3.15-dev-r1` ; aucun correctif supplémentaire requis ; jalon clôturé et publié sous `v0.3.15-dev`.

## Résultat final validé

- Rebuild forcé validé avec la DLL `0.3.15.0`.
- Chargement du jeu validé sans nouvelle erreur rouge.
- Rejet traduit validé lorsqu'aucun pawn n'est sélectionné.
- Rapports validés pour un humain ordinaire, un Jaffa libre, un Jaffa Goa'uld, un hôte Goa'uld et un Tok'ra pré-fusionné.
- Profils, groupes de noms, backstories, faction, physiologie Jaffa, Prim'ta, marque frontale et identités sociales cohérents avec l'état réel des pawns testés.
- Basculement de personnalité Tok'ra validé sans changement de l'enregistrement persistant ni reroll des deux identités.
- Accès identique validé depuis l'action développeur et depuis les options avancées.
- Masquage complet validé lorsque le mode développeur et l'option avancée sont désactivés.
- Ouvertures répétées, sauvegarde/recharge et conservation des noms manuels validées sans mutation du pawn.
- `Player.log` propre pour le périmètre testé.
- Décision finale : conserver le rapport comme consommateur strictement en lecture seule des services existants.

## 1. Préparation et chargement

1. Vérifier que le dépôt ne contient aucun changement non prévu.
2. Créer la branche depuis le tag publié :

```powershell
git switch -c feature/cultural-identity-diagnostics v0.3.14-dev
```

3. Extraire l'archive `r1` à la racine du dépôt.
4. Effectuer un rebuild forcé :

```powershell
dotnet build .\Source\GateRimSG1\GateRimSG1.csproj `
    -t:Rebuild `
    -p:RimWorldManagedDir="D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
```

5. Vérifier que le build se termine sans erreur.
6. Vérifier que `1.6/Assemblies/GateRimSG1.dll` porte la version `0.3.15.0`.
7. Lancer RimWorld avec GateRim SG-1 et Biotech.
8. Attendre le menu principal et vérifier l'absence d'erreur rouge.

## 2. Accès développeur et absence de sélection

1. Charger une carte et activer le mode développeur RimWorld.
2. Ne sélectionner aucun pawn.
3. Ouvrir `Debug actions menu` → `GateRim SG-1` → `Cultural identity: inspect selected pawn`.
4. Vérifier qu'un message de rejet traduit demande de sélectionner un pawn.
5. Vérifier qu'aucune exception n'apparaît dans le journal.

## 3. Humain ordinaire

1. Sélectionner un colon humain ordinaire sans symbiote ni physiologie Jaffa.
2. Ouvrir le rapport par l'action développeur.
3. Vérifier que le rapport correspond au pawn pour :
   - nom et `ThingID` ;
   - race, xenotype, `PawnKindDef` et faction ;
   - enfance et âge adulte actifs.
4. Vérifier que les indicateurs Jaffa, Goa'uld et Tok'ra sont faux.
5. Vérifier que la marque frontale et les données de symbiote indiquent `<none>`.
6. Vérifier que les profils `NonPlayer` et `PlayerStarter` sont affichés séparément, même lorsqu'aucun profil ne correspond.

## 4. Jaffa libre

1. Générer un Jaffa libre avec `Debug actions menu` → `Spawn pawn` et un `PawnKindDef` GateRim SG-1 approprié.
2. Attendre la fin de son initialisation, puis le sélectionner.
3. Ouvrir le rapport.
4. Vérifier :
   - physiologie Jaffa compatible : `True` ;
   - profil et groupe de noms cohérents avec les Jaffa libres ;
   - identité sociale `Free Jaffa` : `True` ;
   - identité `Goa'uld-domain Jaffa` : `False`, sauf si le pawn a été volontairement replacé dans une faction Goa'uld pour un test complémentaire ;
   - état de Prim'ta et marque frontale identiques à l'état réellement généré.

## 5. Jaffa Goa'uld

1. Générer un guerrier ou garde Jaffa Goa'uld.
2. Ouvrir le rapport après initialisation.
3. Vérifier :
   - physiologie Jaffa compatible : `True` ;
   - profil et groupe de noms cohérents avec les Jaffa Goa'uld ;
   - faction et `PawnKindDef` exacts ;
   - Prim'ta et marque frontale identiques à l'inspection du pawn ;
   - indicateurs `Goa'uld-domain Jaffa` et `Marked Jaffa` cohérents avec la faction et la marque réellement présentes.
4. Si un Grand Maître allié de la même faction est placé à moins de 12 cases, vérifier que `Nearby System Lord` devient `True`, puis redevient `False` hors de portée.

## 6. Hôte Goa'uld

1. Générer un hôte Goa'uld actif.
2. Ouvrir le rapport.
3. Vérifier :
   - profil culturel et groupe de noms Goa'uld cohérents ;
   - `Active Goa'uld host` : `True` ;
   - `Active Tok'ra host` : `False` ;
   - Hediff de symbiote et ligne `Data` présents ;
   - origine, noms, backstories et source d'identité de la ligne technique cohérents avec l'inspection du pawn.
4. Pour un Grand Maître, vérifier également `System Lord host`.

## 7. Tok'ra pré-fusionné et personnalité active

1. Générer `SG1_TokraVoluntaryHost`.
2. Noter le nom de l'hôte, le nom du symbiote, les backstories et la personnalité active.
3. Ouvrir le rapport.
4. Vérifier :
   - profil culturel et groupe de noms Tok'ra cohérents ;
   - `Active Tok'ra host` : `True` ;
   - `Active Goa'uld host` : `False` ;
   - ligne de données persistantes présente avec deux identités distinctes ;
   - `hostIdentitySource` et `generatedHostOrigin` cohérents avec un Tok'ra généré déjà fusionné.
5. Basculer vers l'autre personnalité.
6. Rouvrir le rapport et vérifier :
   - nom et backstories actifs mis à jour ;
   - même identifiant de symbiote ;
   - mêmes identités d'hôte et de symbiote stockées ;
   - personnalité active mise à jour ;
   - aucun reroll ni changement de faction, race ou `PawnKindDef`.

## 8. Accès depuis les options du mod

1. Sélectionner un pawn de référence.
2. Ouvrir `Options` → `Mod settings` → `GateRim SG-1`.
3. Avec le mode développeur actif, vérifier la présence du bloc de diagnostic culturel et du bouton `Inspecter le pawn sélectionné`.
4. Cliquer sur le bouton et vérifier qu'il ouvre le même rapport que l'action développeur.
5. Désactiver le mode développeur, activer l'option avancée GateRim SG-1 et vérifier que le bouton reste accessible.
6. Désactiver ensuite l'option avancée et vérifier que les outils techniques disparaissent des paramètres.

## 9. Sauvegarde, rechargement et non-régression

1. Sauvegarder avec au moins un Jaffa et un Tok'ra déjà inspectés.
2. Recharger la sauvegarde.
3. Réouvrir leurs rapports.
4. Vérifier que les rapports restent cohérents et qu'aucune donnée n'a été créée ou modifiée par l'inspection.
5. Vérifier qu'un pawn renommé manuellement conserve son nom.
6. Vérifier qu'aucune backstory, faction, marque, Prim'ta ou identité de symbiote n'a changé après ouverture répétée du rapport.
7. Contrôler `Player.log` : aucune erreur rouge, aucune exception liée au diagnostic et aucun avertissement nouveau inexpliqué.

## Critère de validation

Critère atteint : le même rapport central reproduit fidèlement l'état fourni par les services culturels, sociaux, Jaffa et symbiotes pour tous les cas ciblés, tout en restant strictement en lecture seule et masqué hors des accès techniques autorisés.
