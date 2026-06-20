# Tests du jalon actif

Jalon : `0.3.10-dev - Preserve implanted Tok'ra identity`

Branche : `feature/tokra-identity-persistence`

Statut : validation locale terminée avec succès sur `0.3.10-dev-r1`.

## Préconditions

- Extraire l'archive à la racine du dépôt depuis `v0.3.9-dev`.
- Effectuer un rebuild complet.
- Vérifier `GateRimSG1.dll` en version `0.3.10.0`.
- Tester principalement en français, puis effectuer au moins un chargement en anglais.

## 1. Chargement et configuration

1. Lancer RimWorld avec Biotech et GateRim SG-1.
2. Vérifier l'absence d'erreur XML ou de cross-reference.
3. Rechercher dans le journal :
   - `identityAdulthoods` ;
   - `CulturalIdentityUtility` ;
   - `CulturalPawnProfileDef` ;
   - `SG1_TokraIdentityProfiles`.

Résultat attendu : le profil Tok'ra charge les six carrières d'identité sans erreur ni ambiguïté.

## 2. Implantation volontaire sur un colon

1. Générer un symbiote Tok'ra libre.
2. Noter son nom.
3. Choisir un colon du joueur avec un nom, une enfance et une carrière clairement identifiables.
4. Effectuer l'implantation volontaire.
5. Vérifier immédiatement que le nom principal et les backstories actives du colon ne changent pas.
6. Ouvrir l'état de santé du pawn et sélectionner l'état d'implantation ou de symbiose.

Résultat attendu : la description affiche le nom de l'hôte, le même nom de symbiote Tok'ra, le parcours de l'hôte et une des six carrières Tok'ra configurées.

Aucun gizmo de basculement n'est attendu dans `0.3.10-dev`.

## 3. Conversion en hôte actif

1. Laisser la phase d'implantation récente se convertir normalement en hôte actif.
2. Vérifier à nouveau l'état de santé.
3. Comparer les deux noms et les deux parcours avec ceux affichés avant la conversion.

Résultat attendu : les quatre informations sont strictement identiques ; la conversion transfère le même objet persistant.

## 4. Sauvegarde et rechargement

1. Sauvegarder avec le Tok'ra implanté.
2. Quitter complètement RimWorld.
3. Recharger la sauvegarde.
4. Vérifier les noms et parcours stockés.
5. Reprendre le temps plusieurs secondes.

Résultat attendu : aucune identité n'est reroulée, aucun nom ou backstory actif n'est remplacé et aucune compétence n'est modifiée.

## 5. Migration d'une sauvegarde antérieure

1. Charger une sauvegarde `0.3.9-dev` contenant déjà un hôte Tok'ra.
2. Inspecter son état de santé.
3. Sauvegarder sous un nouveau nom, quitter et recharger.

Résultat attendu : le nom existant du symbiote est conservé, le parcours Tok'ra manquant est créé une seule fois à partir de son identité persistante, et les histoires actives de l'hôte restent intactes.

## 6. Extraction et réimplantation

1. Extraire le symbiote pendant une phase où l'extraction existante est autorisée.
2. Réimplanter le même symbiote dans un autre colon compatible.
3. Comparer son nom et son parcours Tok'ra avant et après transfert.
4. Vérifier que le nouveau nom et les nouvelles backstories d'hôte correspondent au second colon.

Résultat attendu : le symbiote conserve sa propre identité ; seules les informations d'hôte sont remplacées pour le nouveau corps, avec l'ancien `ThingID` conservé dans l'historique technique.

## 7. Frontière joueur / IA

1. Générer un hôte Tok'ra volontaire appartenant à la faction Tok'ra ou déclencher des visiteurs Tok'ra.
2. Inspecter leur état de santé.
3. Vérifier l'absence de gizmo et du nouveau résumé joueur.
4. Recruter réellement un Tok'ra lorsque le flux testé le permet, puis réinspecter.

Résultat attendu : les Tok'ra gérés par l'IA gardent l'affichage classique. Le résumé double identité n'apparaît que pour un colon directement contrôlé par le joueur.

## 8. Régressions Goa'uld

1. Effectuer une implantation Goa'uld forcée.
2. Laisser la conversion en hôte actif se produire.
3. Tester rapidement une extraction d'urgence.

Résultat attendu : le flux Goa'uld, les noms, les messages et l'affichage existants ne changent pas.

## Résultats validés

- [x] Chargement des profils et des champs persistants sans erreur XML ou cross-reference.
- [x] Implantation volontaire sans changement du nom principal, des backstories actives ou des compétences de l'hôte.
- [x] Conservation du même nom et du même parcours Tok'ra lors de la conversion en hôte actif.
- [x] Sauvegarde, arrêt complet et rechargement sans reroll ni second traitement.
- [x] Migration d'une sauvegarde antérieure avec initialisation unique des données manquantes.
- [x] Extraction et réimplantation du même symbiote avec conservation de son identité propre.
- [x] Affichage détaillé limité aux Tok'ra directement contrôlés par le joueur.
- [x] Comportement classique conservé pour les Tok'ra gérés par l'IA.
- [x] Implantation, conversion et extraction Goa'uld sans régression.
- [x] `Player.log` propre.

## Contrôle final

- Version du mod : `0.3.10-dev`.
- Version de la DLL : `0.3.10.0`.
- Aucun changement du nom principal, des backstories actives ou des compétences de l'hôte.
- Aucun gizmo de personnalité dans ce jalon.
- `Player.log` propre, sans erreur `Scribe_Defs`, `BackstoryDef`, traduction, transfert, chargement ou ancienne DLL.
