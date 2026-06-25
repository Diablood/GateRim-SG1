# Validation ciblée — 0.3.44-dev

Jalon : `0.3.44-dev - Add Free Jaffa military aid`

Branche : `feature/free-jaffa-military-aid`

Tag de départ : `v0.3.43-dev`

Version de DLL validée : `0.3.44.0`

Révision locale finale validée : `r1`

Statut : validation terminée, publication sous `v0.3.44-dev`.

## 1. Contrôles statiques et chargement

- [x] `git diff --check` ne signale aucune erreur.
- [x] `./tools/check-project-consistency.cmd` termine avec un code `0`.
- [x] Le rebuild forcé produit bien une DLL `0.3.44.0`.
- [x] RimWorld démarre sans nouvelle erreur rouge GateRim SG-1.
- [x] `SG1_FreeJaffa` charge avec `canRequestMilitaryAid=true`.
- [x] Les groupes `Combat`, `Trader`, `Settlement` et `Peaceful` restent valides.

## 2. Préparation diplomatique

Utiliser une sauvegarde de test disposant :

- d'une faction `Jaffa libres` visible ;
- d'une console de communication alimentée ;
- d'au moins un colon capable de négocier ;
- d'une menace ou d'une cible hostile permettant d'observer les renforts.

En mode développeur, utiliser l'action vanilla de modification de bonne volonté pour placer successivement la faction en relation neutre puis alliée.

- [x] À relation neutre, le dialogue de la faction ne propose pas d'aide militaire.
- [x] À relation hostile, aucun appel pacifique à l'aide n'est disponible.
- [x] Au statut allié, la demande d'aide militaire apparaît dans le dialogue vanilla.
- [x] Sans console alimentée ou sans opérateur valide, la demande ne peut pas être lancée.

## 3. Demande et coût diplomatique

- [x] La demande affiche ou applique le coût de bonne volonté prévu par RimWorld.
- [x] La bonne volonté est effectivement réduite après acceptation.
- [x] La demande ne consomme aucune confiance Tok'ra ni ressource GateRim SG-1 parallèle.
- [x] Une seconde demande immédiate est bloquée par le délai ou par la relation résultante selon les règles vanilla.
- [x] Le refus éventuel utilise un message vanilla compréhensible et ne produit pas d'état GateRim persistant.

## 4. Composition des renforts

Inspecter plusieurs membres du groupe arrivé :

- [x] seuls `SG1_FreeJaffaWarrior` et `SG1_FreeJaffaGuard` composent le groupe de combat ;
- [x] les pawns possèdent le xénotype Jaffa ;
- [x] leur Prim'ta est présent après l'initialisation normale ;
- [x] leurs noms et backstories correspondent aux Jaffa libres ;
- [x] leurs bâtons Ma'Tok et armures modulaires sont cohérents avec leur profil ;
- [x] aucune marque frontale Goa'uld n'est imposée automatiquement ;
- [x] aucun marchand, animal de bât ou pawn Goa'uld n'est mélangé au groupe d'aide.

## 5. Arrivée, combat et départ

- [x] RimWorld choisit et exécute normalement le mode d'arrivée de l'aide.
- [x] Les renforts sont alliés au joueur et hostiles à la menace appropriée.
- [x] Ils combattent sans gizmo, ordre ou contrôleur GateRim supplémentaire.
- [x] Les pertes, mises à terre et soins éventuels suivent les règles vanilla.
- [x] Le groupe quitte la carte selon le comportement vanilla après son intervention.
- [x] Aucun renfort ne rejoint automatiquement la colonie.

## 6. Persistance et limites de faction

- [x] Sauvegarder et recharger pendant la présence des renforts conserve leur faction, leur état et leur comportement.
- [x] Le coût et le délai de demande restent cohérents après rechargement.
- [x] `canGenerateQuestSites` reste désactivé.
- [x] `raidsForbidden` reste actif.
- [x] Les sièges et attaques préparées Jaffa libres restent désactivés.
- [x] Le commerce, les colonies, les visiteurs pacifiques et le convoi spécialisé de `0.3.43-dev` fonctionnent toujours.

## 7. Journal

- [x] `Player.log` ne contient aucune nouvelle erreur GateRim SG-1.
- [x] Aucune erreur de génération de groupe `Combat` ou de PawnKind Jaffa libre n'apparaît.
- [x] Aucun message de référence XML manquante n'est introduit.

## Résultat final

La révision finale `r1` permet à une faction Jaffa libre alliée de fournir une aide militaire par le dialogue vanilla de la console de communication. Le coût, le délai, l'arrivée, le combat et le départ restent entièrement gouvernés par RimWorld, tandis que la force générée conserve l'identité biologique, culturelle et militaire des Jaffa libres. Les quêtes, raids naturels, sièges et attaques préparées restent désactivés, et aucun nouvel état persistant n'est ajouté.
