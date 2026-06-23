# Validation finale — 0.3.34-dev

Jalon : `0.3.34-dev - Add Tok'ra diversion assault operation`

Branche publiée : `feature/tokra-decoy-transmission-defense`

Tag de départ : `v0.3.33-dev`

Tag final : `v0.3.34-dev`

Dernière révision locale validée : `0.3.34-dev-r3`

Version de DLL validée : `0.3.34.0`

## 1. Suppressions validées

Les cinq fichiers obsolètes du transmetteur r1 sont absents de l'état final :

- `1.6/Defs/ThingDefs_Buildings/SG1_TokraDecoyTransmitter.xml` ;
- `Languages/French/DefInjected/ThingDef/SG1_TokraDecoyTransmitter.xml` ;
- `Source/GateRimSG1/Goauld/TokraDecoyTransmissionDefenseUtility.cs` ;
- `docs/TOKRA_DECOY_TRANSMISSION_DEFENSE.md` ;
- `docs/wiki/Tokra-Decoy-Transmission-Defense.md`.

Aucune texture n'a été ajoutée ou supprimée pour le prototype rejeté. La r3 n'exigeait aucune suppression supplémentaire.

## 2. Contrôles et build validés

- branche `feature/tokra-decoy-transmission-defense` ;
- version publique `0.3.34-dev` ;
- versions projet, assemblage et fichier `0.3.34.0` ;
- `83` BackstoryDef uniques ;
- contrôle de cohérence positif ;
- rebuild forcé réussi ;
- chargement sans erreur de configuration du groupe `SG1_TokraDiversionAssault` ou du sapeur Ma'Tok requis.

## 3. Validation fonctionnelle r3

Le correctif ciblé a été validé après redémarrage complet de RimWorld :

- l'offre de diversion peut être forcée et acceptée sans objet physique ni transmetteur ;
- `Tok'ra ops: force diversion assault` produit un seul raid Goa'uld/Jaffa ;
- le budget reste dans les bornes de mission `180–3000` et ne monte plus artificiellement vers `104999` ;
- le rapport `Tok'ra ops: show framework state` indique des assaillants enregistrés et au moins un sapeur enregistré ;
- au moins un `Goa'uld Jaffa breacher` / `sapeur Jaffa au service des Goa'uld` apparaît avec un Ma'Tok ;
- le groupe attaque les murs ou les accès fermés avec le comportement vanilla de brèche ;
- une sauvegarde/recharge pendant l'assaut conserve la même force et ne crée pas de second raid ;
- la neutralisation de tous les assaillants résout une seule réussite ;
- la réussite accorde `+3` de confiance une seule fois et libère le slot organique ;
- aucune seconde lettre ou modification de confiance n'apparaît après rechargement ;
- `Player.log` ne contient plus les erreurs de `MinimumPoints`, de pawn requis absent, de `PawnGroupMaker` inutilisable ou d'échec de démarrage de l'assaut.

## 4. Couverture durable non rejouée pendant la passe finale

Les chemins suivants restent documentés dans `docs/TESTING.md` mais n'ont pas été présentés comme rejoués lors de la validation ciblée r3 :

- fuite ennemie les mains vides ;
- sortie réelle avec un otage ;
- sortie réelle avec du butin ;
- perte de carte ;
- comparaison entre colonie faible et colonie avancée ;
- récurrence, anti-répétition et verrou du communicateur ;
- régression complète des six opérations Tok'ra précédentes.

Ils doivent être repris lors de toute modification future de cette opération, de la stratégie de raid, du runtime de mission ou du planificateur partagé.

## 5. Publication

Le jalon est clôturé sur la révision locale `r3`, publié sur la branche `feature/tokra-decoy-transmission-defense`, tagué `v0.3.34-dev` et synchronisé avec le wiki séparé.
