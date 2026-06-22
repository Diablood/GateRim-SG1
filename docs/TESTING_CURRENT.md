# Tests du jalon clôturé

Jalon : `0.3.29-dev - Add Tok'ra distress call world-site mission`

Branche : `feature/tokra-distress-call-world-site`

Révision locale finale validée : `0.3.29-dev-r7`

Version de DLL validée : `0.3.29.0`

Tag de départ : `v0.3.28-dev`

Tag final : `v0.3.29-dev`

Statut : validation locale terminée. Le flux complet et la correction de présentation `r7` ont été validés en jeu ; le contrôle de cohérence, le rebuild forcé, les trois variantes, la persistance, la récurrence, les quatre anciennes opérations, les textes anglais/français et `Player.log` sont validés.

## 1. Préparation validée

1. Extraire le ZIP `r7` à la racine de la branche active.
2. Exécuter `git status --short`.
3. Exécuter `./tools/check-project-consistency.cmd`.
4. Exécuter le rebuild non incrémentiel avec `./build.cmd "D:/SteamLibrary/steamapps/common/RimWorld/RimWorldWin64_Data/Managed"`.
5. Démarrer RimWorld avec le mode développeur et le debug avancé GateRim SG-1.
6. Lancer `Mission framework: inspect definitions`.

Résultat attendu :

- cinq MissionDefs organiques chargés ;
- DLL `0.3.29.0` ;
- aucun défaut XML, traduction ou type dans `Player.log` ;
- aucune erreur concernant `recoveryPawnKindDefName`, `recoveryTeamDelayTicks`, `recoveryTeamRetryTicks`, `recoveryTeamMinimumCount` ou `recoveryTeamMaximumCount`.

## 2. Test obligatoire court — secours réel

1. Lancer `Tok'ra ops: force distress rescue offer`.
2. Accepter normalement depuis le communicateur.
3. Former une caravane et sélectionner `Voyager vers le signal de détresse Tok'ra`.
4. Laisser la caravane atteindre le site sans utiliser une seconde commande d'entrée.
5. Observer l'état du jeu lors du chargement de la carte.
6. Vérifier la disposition générale avant de reprendre le temps.
7. Éliminer les Jaffa.
8. Soigner le choc du symbiote d'au moins un survivant directement au sol, sans construire de lit.
9. Attendre la récupération Tok'ra courte.

Résultat attendu :

- la carte se génère et se charge automatiquement à l'arrivée ;
- le jeu est en pause ;
- les colons de la caravane sont enrôlés et contrôlables ;
- l'entrée utilise le bord de carte le plus proche de la scène tout en restant gérée par `CaravanEnterMapUtility` ;
- survivants, décor et Jaffa forment une seule scène cohérente à l'intérieur de la carte ;
- aucun survivant vivant n'est généré au bord comme un visiteur entrant ;
- un camp temporaire ou une caravane attaquée est visible ;
- les corps Tok'ra/Jaffa éventuels restent optionnels et en faible nombre ;
- soigner le choc au sol est reconnu, sans lit médical ;
- le combat seul ne valide pas la mission ;
- une fois le choc traité et la menace éliminée, un message annonce l'équipe de récupération ;
- après environ `600` ticks, une équipe Tok'ra entre sur la carte par le bord ;
- un membre de cette équipe rejoint chaque survivant à terre, le porte jusqu'au bord et quitte réellement la carte avec lui ;
- un survivant capable de marcher quitte la carte par le comportement de départ existant ;
- aucun survivant ne disparaît instantanément au terme d'un simple compteur ;
- aucune attente de guérison naturelle, récolte de nourriture ou construction de chauffage n'est requise ;
- au moins un survivant évacué permet la réussite lorsque les autres cas sont résolus ;
- confiance `+3`, Medicine XP `+300`, lettre unique et absence d'erreur dans `Player.log`.

## 3. Placement et carte de grande taille

Répéter le secours réel plusieurs fois, et si possible avec une taille de carte de test supérieure.

Vérifier :

- ancrage de scène à une distance sûre du bord ;
- survivants à quelques cellules du camp ou des débris ;
- Jaffa dans un rayon cohérent autour de la position, pas au centre sans lien avec des survivants au bord ;
- entrée de la caravane depuis le côté le plus proche ;
- trajet raisonnable entre l'entrée et la scène ;
- aucun survivant ne meurt systématiquement avant que les colons puissent atteindre la zone.

## 4. Variante « signal compromis »

1. Lancer `Tok'ra ops: force distress trap offer`.
2. Accepter et laisser la caravane entrer automatiquement.
3. Vérifier l'absence de survivants vivants.
4. Vérifier une position compromise ou préparée autour du signal.
5. Neutraliser les hostiles.

Résultat attendu :

- pause et enrôlement identiques au secours réel ;
- Jaffa regroupés autour de la scène ;
- corps éventuels cohérents mais non obligatoires ;
- réussite après élimination de la menace ;
- aucune pénalité liée au seul fait que le signal était un piège.

## 5. Variante « arrivée trop tard »

Tester :

- `Tok'ra ops: force distress late offer` ;
- puis une offre de secours réel dépassant `120000` ticks sans atteindre l'expiration finale.

Résultat attendu :

- aucun survivant vivant ;
- camp temporaire ravagé ou caravane attaquée ;
- `1` à `3` corps Tok'ra configurés, avec corps Jaffa optionnels ;
- Jaffa encore présents près des vestiges ;
- étagère vanilla déjà présente près de la scène dès le chargement de la carte ;
- pile de `4` à `10` composants stockée sur l'une des cases de cette étagère ;
- aucun nouveau composant n'apparaît au sol lors de la réussite ;
- réussite après neutralisation de la menace.

## 6. Soins et extraction

Tester séparément :

- choc non traité après élimination des ennemis ;
- choc traité au sol ;
- choc traité avant la fin du combat ;
- survivant toujours inconscient après traitement ;
- survivant mourant pendant le délai d'extraction ;
- deux survivants évacués et un survivant mort ;
- mort de tous les survivants avant toute extraction.

Résultat attendu :

- aucune extraction sans traitement réel du choc ;
- aucune exigence de lit ou de capacité à marcher ;
- le délai ne démarre que lorsque la présence hostile active est éliminée ;
- l'équipe de récupération entre par un bord de carte avec le mode vanilla `EdgeWalkIn` ;
- un porteur rejoint physiquement chaque survivant inconscient et le transporte vers une cellule de sortie ;
- un survivant mobile quitte la carte à pied ;
- aucun survivant ne disparaît instantanément sur place ;
- chaque survivant traité peut être récupéré indépendamment ;
- si un porteur est interrompu, un autre membre disponible peut reprendre le transport ;
- une mort reste une perte réelle ;
- réussite avec au moins une sortie vivante et aucun survivant vivant non résolu ;
- échec si aucune extraction n'est obtenue.

## 7. Persistance

Sauvegarder et recharger :

- caravane en route avec l'action d'arrivée stockée ;
- carte active avant traitement ;
- choc traité pendant que des Jaffa restent présents ;
- délai de récupération en cours ;
- un survivant déjà évacué et un autre encore présent ;
- opération résolue avant la reformation de la caravane.

Résultat attendu :

- même site, variante et menace ;
- aucune seconde génération de scène, corps, survivants, ennemis, étagère ou butin ;
- soins, délai, arrivée de l'équipe, affectation des porteurs et évacuations conservés ;
- aucune double résolution.

## 8. Contrôles globaux reconfirmés

- délai et expiration ;
- difficulté faible et avancée ;
- récurrence et anti-répétition ;
- sauvegarde/recharge des phases mondiales ;
- quatre opérations Tok'ra précédentes ;
- textes anglais et français ;
- absence d'outils debug en jeu normal ;
- `Player.log` propre.

## État de validation

- [x] contrôle de cohérence ;
- [x] rebuild forcé ;
- [x] arrivée automatique, pause et enrôlement ;
- [x] secours réel avec soins au sol et récupération visible ;
- [x] placement cohérent sur plusieurs générations ;
- [x] signal compromis contextualisé ;
- [x] arrivée trop tard contextualisée avec composants présents sur étagère ;
- [x] persistance pendant l'extraction ;
- [x] difficulté adaptative, expiration, récurrence et anti-répétition ;
- [x] régressions des quatre opérations précédentes ;
- [x] textes EN/FR ;
- [x] `Player.log` propre.

Validation locale finale : `0.3.29-dev-r7`. Le jalon est publié sous le tag final unique `v0.3.29-dev`.
