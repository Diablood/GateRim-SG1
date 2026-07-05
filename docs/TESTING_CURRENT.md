# Validation locale - 0.3.69-dev-r6

Jalon : `0.3.69-dev - Add open-conflict Goa'uld battlefield incident`

Branche attendue :
`feature/goauld-open-conflict-battlefield-incident`

Base : `develop` au tag publié `v0.3.68-dev`

Version de DLL attendue : `0.3.69.0`

Révision locale : `r6`

Statut : validé localement en révision finale `r6`; build `0.3.69.0`, tests fonctionnels et régressions confirmés.

## Préparation

- utiliser une sauvegarde de test avec Commandement SG-1 ;
- disposer d'au moins deux domaines Goa'uld actifs ;
- activer le mode développeur ;
- préparer une carte principale sans raid ni autre menace hostile active ;
- conserver une sauvegarde propre avant chaque variante importante.

## Build et cohérence

1. Exécuter `git diff --check`.
2. Exécuter `./build.cmd` et confirmer `GateRimSG1.dll` en `0.3.69.0`.
3. Exécuter `./tools/check-project-consistency.cmd`.
4. Avant finalisation du changelog, un seul échec est admis : le dernier
   en-tête du changelog reste `0.3.68-dev`.
5. Toute autre erreur de version, lien, XML ou couverture est un échec.

## Création déterministe

1. Ouvrir :

   ```text
   Actions de débogage
   > GateRim SG-1
   > Goa'uld inter-domain relations...
   ```

2. Créer un second domaine si nécessaire.
3. Utiliser `Set first pair: Open conflict`.
4. Ouvrir `Show battlefield report` et vérifier une paire admissible et aucun
   champ de bataille actif.
5. Utiliser `Force battlefield now`.
6. Vérifier qu'une seule lettre nomme les deux domaines exacts.
7. Cliquer sur `Se rendre sur les lieux` et confirmer l'ouverture de la carte de
   la colonie centrée sur un détachement, jamais de la carte mondiale.

## Arrivée depuis le bord et rassemblement

1. Vérifier que les deux groupes apparaissent à moins de quatre cellules du bord
   de carte, et non directement au centre du futur champ de bataille.
2. Vérifier deux points de ralliement distincts à l'intérieur de la carte.
3. Confirmer que les Jaffa des deux domaines joggent depuis leurs zones d'entrée
   vers leur propre point de ralliement.
4. Confirmer qu'ils ne se combattent pas encore pendant le rassemblement normal.
5. Lorsque les deux groupes sont suffisamment rassemblés, attendre environ
   `1800` ticks et confirmer le message : les deux détachements lancent l'assaut.
6. Sur un terrain difficile, vérifier que le délai maximal de `12000` ticks lance
   malgré tout l'assaut et évite un blocage permanent.

## Combat mutuel

1. Après le message d'assaut, confirmer que les deux camps quittent leurs
   positions de ralliement et avancent réellement jusqu'à portée de tir.
2. Confirmer que les Jaffa équipés d'armes à distance tirent dès qu'ils disposent
   de la portée et d'une ligne de vue, au lieu de rester immobiles face à une
   cible trop éloignée.
3. Vérifier que chaque pawn appartient au domaine annoncé correspondant.
4. Vérifier une composition Jaffa guerriers/gardes cohérente.
5. Rester à l'écart et confirmer l'absence d'assaut organisé contre les bâtiments
   ou colons.
6. Vérifier que des tirs perdus restent possibles sans que la colonie devienne
   l'objectif programmé.

## Intervention facultative et riposte bornée

Après recharge de la sauvegarde de préparation :

1. forcer une nouvelle bataille ;
2. attaquer un camp pendant son rassemblement et confirmer sa riposte contre le
   ou les colons attaquants ;
3. cesser toute attaque et confirmer qu'après `1800` ticks sans nouvelle
   provocation ce camp reprend son rassemblement ou son combat contre le rival ;
4. recommencer pendant l'assaut et vérifier que le camp non provoqué continue de
   combattre son rival ;
5. attirer le camp provoqué, puis éloigner le colon à plus de `35` cellules du
   point où la riposte a commencé ; confirmer que la poursuite cesse et ne se
   prolonge pas à travers toute la carte ;
6. laisser un camp gagner rapidement, attendre que le vainqueur commence son
   retrait, puis l'attaquer ;
7. confirmer que la première blessure infligée au camp vainqueur enregistre un
   provocateur même si le job de tir du colon n'expose pas directement le pawn
   ciblé ;
8. confirmer que le vainqueur interrompt sa sortie et riposte, mais pendant au
   plus `6000` ticks de retrait au total ;
9. continuer à l'attaquer au-delà de cette fenêtre et confirmer qu'il rompt le
   combat et reprend son retrait sans repousser la date de sortie forcée ;
10. tester une intervention contre les deux camps ;
11. confirmer capture vanilla, butin ordinaire et absence de récompense ou de
   goodwill artificiel.

## Fin de bataille, rupture et retrait

1. Avec un détachement initial d'au moins quatre combattants, réduire un seul
   camp à `30%` ou moins de son effectif mobile initial tout en laissant l'autre
   au-dessus de son propre seuil.
2. Confirmer le message de rupture et le retrait des deux forces sans exiger
   l'extermination du camp perdant.
3. Refaire le test en réduisant simultanément les deux camps sous leur seuil :
   confirmer qu'aucun camp n'est désigné seul comme perdant et que le combat
   continue jusqu'à élimination ou délai maximal.
4. Avec seulement deux ou trois combattants initiaux, confirmer qu'aucune rupture
   anticipée ne survient avant la perte de tous les combattants mobiles.
5. Laisser un camp perdre tous ses combattants mobiles et confirmer le retrait.
6. Utiliser `Order battlefield withdrawal` pour le test déterministe.
7. Confirmer que les pawns mobiles continuent leur déplacement vers la sortie et
   qu'un chemin calé est relancé toutes les `600` ticks.
8. Vérifier que la riposte joueur ne modifie jamais le délai de sortie forcée
   fixé à `30000` ticks après le début du retrait.
9. Vérifier qu'à ce terme tout mobile non-prisonnier restant est forcé à quitter
   la carte.
10. Vérifier que pawns à terre, prisonniers, corps et butin ne disparaissent pas.
11. Confirmer qu'une bataille ignorée ne dépasse pas deux jours plus la grâce.

## Persistance et orchestration

1. Sauvegarder pendant le rassemblement, recharger et confirmer les mêmes entrées,
   points de ralliement, groupes et délai restant.
2. Sauvegarder après le message d'assaut et confirmer la reprise du combat sans
   second message.
3. Sauvegarder après provocation, recharger et confirmer la fenêtre restante, le point d’origine de poursuite et l’expiration normale de la riposte.
4. Sauvegarder pendant la riposte de retrait et confirmer que la limite totale de `6000` ticks et la date de sortie forcée restent inchangées après recharge.
5. Utiliser `Make battlefield opportunity due` pendant une bataille active et
   confirmer qu'aucune seconde bataille n'apparaît.
6. Passer à Cassandra avant une opportunité naturelle : le délai doit être
   suspendu et aucun événement ne doit apparaître.
7. Passer à Cassandra pendant une bataille active : elle doit se terminer
   normalement, sans nouvelle opportunité.
8. Revenir à Commandement SG-1 et confirmer la reprise sans backlog immédiat.
9. Avec plusieurs paires en conflit ouvert, vérifier l'exclusion de la paire
   précédente lorsqu'une autre paire est disponible.

## Régressions obligatoires

- le facteur de raid naturel `75%` de `0.3.68-dev` reste inchangé ;
- les transitions de relations restent persistantes ;
- les raids direct, enlèvement et destruction restent fonctionnels ;
- les représailles d'extraction conservent leurs points ;
- aucun settlement mondial ou relation stratégique n'est modifié ;
- aucun site mondial de bataille n'est créé dans ce jalon.

## Résultat de validation

La révision finale `r6` est validée :

- build `0.3.69.0` réussi ;
- arrivée depuis le bord et rassemblement validés ;
- annonce et combat mutuel validés ;
- poursuite et tir à distance validés ;
- riposte joueur temporaire et bornée validée ;
- rupture morale et retrait absolu validés ;
- sauvegarde/rechargement et régressions Goa'uld validés ;
- `Player.log` propre.

## Journal

Inspecter `Player.log` après rassemblement, assaut, intervention, retrait et
recharge. Aucun nouvel échec C#, XML, Scribe, faction, Lord, JobDriver,
génération de pawn ou storyteller attribuable au jalon n'est accepté.
