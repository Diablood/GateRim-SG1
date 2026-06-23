# Tests

## 0.3.31-dev - Mission d'introduction et objet-clé Tok'ra

Validation locale terminée sur la révision `r3`, puis jalon publié sous `v0.3.31-dev`. Les révisions intermédiaires ont ajouté la fondation persistante, le flux jouable, la véritable lettre à choix et les chemins d'échec réels.

Couverture validée :

- contrôle de cohérence positif pour `0.3.31-dev`, `0.3.31.0` et `83` backstories ;
- rebuild forcé et DLL `0.3.31.0` après restauration des champs MissionDef de la livraison et de l'import `RimWorld` requis par la reformation ;
- chargement de la mission d'introduction, de sa lettre, du WorldObject, du module et de ses traductions sans erreur bloquante ;
- arc indépendant des six opérations organiques et disponible avant le communicateur ;
- première opportunité cachée `4–12` jours et persistance de tous les états ;
- trois variantes d'offre RP et deux variantes de réussite ;
- vraie lettre à choix persistante avec acceptation et refus intégrés ;
- refus, expiration ou absence de réponse suivis d'un délai caché `10–60` jours ;
- site mondial créé seulement après acceptation, à distance configurable, avec trajet vanilla ;
- menace capturée lors de l'offre, facteur `0,35`, budget `180–650` et garde bornée à `2–6` défenseurs ;
- un seul module physique suivi par son identité exacte, sans validation par une copie créée séparément ;
- combat, inventaire réel, récupération d'équipement ennemi et reformation vanilla ;
- réussite seulement lorsque le module suivi atteint une caravane, un pawn ou une carte de colonie du joueur ;
- avertissement final unique environ un jour avant la fermeture du site ;
- expiration réelle du site, destruction réelle du module et perte inattendue du site ;
- nouveau délai caché `7–45` jours après chaque tentative acceptée échouée ;
- répétition des tentatives possible jusqu'à réussite, puis fermeture définitive de l'arc ;
- sauvegarde/rechargement dans les états attente, offre, actif, carte hostile, avertissement, délai de retour et réussite ;
- absence de lettre, site, module, échec, réussite ou délai dupliqué ;
- passe finale des textes joueur sans formulation technique opaque nécessitant une correction.

Points de régression durables :

- ne jamais fermer l'arc après un refus, une expiration ou un échec ;
- conserver un délai caché nouvellement tiré et persisté après chaque résolution non réussie ;
- suivre l'identité exacte du module, pas seulement son `ThingDef` ;
- ne jamais réussir sur la seule victoire militaire ou sur la présence d'une copie du module ;
- préserver le butin et la reformation vanilla au lieu de reconstruire artificiellement la caravane ;
- capturer la difficulté à l'offre et maintenir l'introduction sous le niveau des futurs affrontements Tok'ra ;
- conserver l'arc hors du slot et du catalogue des opérations récurrentes ;
- vérifier sauvegarde/rechargement avant le choix, pendant le site, après l'avertissement, après un échec et après la réussite ;
- empêcher toute nouvelle offre, tout second site et tout second module après réussite ;
- revalider ce flux lorsque l'étude du module, la recherche ou le verrouillage du communicateur seront ajoutés.

## 0.3.30-dev - Livraison vers une base Tok'ra temporaire

Validation locale terminée sur la révision `r9`, puis jalon publié sous `v0.3.30-dev`. Les révisions intermédiaires ont séparé le voyage de la remise, ajouté la période de grâce et les deux complications de combat, déplacé l'embuscade finale sur la tuile d'approche et corrigé le suivi différé de la carte temporaire.

Couverture validée :

- contrôle de cohérence positif pour `0.3.30-dev`, `0.3.30.0` et `83` backstories ;
- rebuild forcé et DLL `0.3.30.0` ;
- chargement de six MissionDefs organiques, du WorldObject de rendez-vous et des deux IncidentDefs d'interception sans erreur XML, traduction ou initialisation ;
- sélection limitée aux objets réellement fabricables selon recette, recherches, contenu requis, poste de travail et colon capable ;
- quantité, qualité minimale, état minimal, échéances, menace et complication persistants ;
- création d'un rendez-vous temporaire `6–16` tuiles plus loin et voyage libre par le pathfinding vanilla ;
- arrivée sans consommation automatique, gizmo explicite de remise, décompte réel du manque et retrait exact de la quantité conforme ;
- conservation du surplus, des autres objets de caravane et du butin récupéré ;
- livraison ponctuelle à `+2`, période de grâce de `120000` ticks à `+1`, puis expiration finale à `-1` ;
- interception Goa'uld unique pendant le trajet, seulement pour une cargaison complète allant vers le site exact ;
- embuscade mutuellement exclusive sur la dernière tuile d'approche, jamais directement sur le rendez-vous ;
- transfert réel des colons, animaux et inventaire sur la carte temporaire ;
- récupération des armes, armures et autres objets autorisés par la fenêtre vanilla de reformation ;
- reformation sur la tuile adjacente puis sélection directe du rendez-vous pour la dernière case ;
- victoire militaire sans confiance ni réussite tant que la cargaison conforme n'est pas remise ;
- perte, destruction ou dégradation des objets réellement prise en compte ;
- menace adaptative consommant le snapshot capturé à l'offre, avec couverture sur colonie faible et avancée ;
- suivi différé de la carte créée par long event, sans erreur d'objet mondial non identifié ni sécurisation prématurée ;
- sauvegarde/rechargement avant l'incident, sur la carte hostile, après combat, après reformation, sur la tuile du site et avant remise ;
- absence de carte, lettre, message, résultat ou confiance dupliqués ;
- déclenchement naturel observé sans forçage développeur ;
- rééligibilité après réussite, échec et offre ignorée, délais cachés variables, pénalité du dernier archétype et slot global unique ;
- variantes RP et anti-répétition locale conservées ;
- observation, renseignements, agent blessé, remise médicale et appel de détresse sans régression ;
- `Player.log` final propre.

Points de régression durables :

- ne proposer que des contrats réellement réalisables au moment de l'offre et ne jamais recalculer opportunément leur contenu après acceptation ;
- conserver le voyage et la reformation vanilla, mais laisser le gestionnaire GateRim propriétaire du contrat, des délais, de la confiance et de la récurrence ;
- ne jamais réussir sur la seule victoire militaire : exiger la remise physique de la quantité conforme survivante ;
- consommer exactement la commande sans toucher au surplus, aux objets non conformes ou au butin ;
- déclencher l'embuscade finale sur la tuile d'approche afin que la caravane puisse être reformée avec tout le butin puis rejoindre directement la cible ;
- observer les cartes de caravane créées par long event après leur apparition effective, sans supposer que l'objet mondial existe au retour de `TryExecute` ;
- figer la menace à l'offre et couvrir les colonies de puissances différentes ;
- préserver le tirage unique, les retries, les échéances, les messages et les résultats à travers la sauvegarde/rechargement ;
- revalider retard, interception, approche finale, récurrence, anti-répétition et les cinq autres opérations lors de toute modification du moteur partagé.

## 0.3.29-dev - Appel de détresse Tok'ra sur site mondial

Validation locale terminée sur la révision `r7`, puis jalon publié sous `v0.3.29-dev`. Les révisions intermédiaires ont corrigé l'entrée de caravane, la cohérence de la scène, l'extraction visible, le rebuild non incrémentiel et la présentation physique de la récompense.

Couverture validée :

- contrôle de cohérence positif pour `0.3.29-dev`, `0.3.29.0` et `83` backstories ;
- rebuild forcé non incrémentiel et DLL `0.3.29.0` ;
- chargement de cinq MissionDefs organiques et du profil de site mondial sans erreur XML ou traduction ;
- offre, acceptation, création du site, action d'arrivée persistante, chargement automatique de la carte, pause hostile vanilla et colons enrôlés ;
- situations cachées secours réel, signal compromis et arrivée trop tardive, avec dégradation temporelle du secours ;
- scène ancrée cohérente sur plusieurs générations : survivants, Jaffa, camp ou caravane attaquée, débris, corps optionnels et entrée raisonnablement proche ;
- menace Goa'uld/Jaffa dimensionnée depuis le snapshot capturé à l'offre, testée sur des colonies de puissance différente ;
- traitement réel du choc du symbiote directement au sol, sans lit, chauffage ni guérison complète imposés ;
- arrivée visible d'une équipe Tok'ra, portage vanilla des survivants incapables de marcher et sortie physique de la carte avant comptabilisation de l'évacuation ;
- échec lorsque tous les survivants meurent, réussite avec au moins une évacuation vivante et absence de double résultat ;
- récompense matérielle de l'arrivée tardive déjà présente sur une étagère vanilla lors de la génération, sans apparition finale au sol ni duplication après sauvegarde/rechargement ;
- expiration de l'offre et du site, nettoyage différé de la carte et conservation des bloqueurs RimWorld normaux ;
- sauvegarde/rechargement pendant le trajet, sur la carte, après traitement, pendant l'arrivée ou le portage de l'équipe et après résolution ;
- récurrence après réussite, échec et offre ignorée, slot global unique, délai caché et anti-répétition locale ;
- observation, renseignements, agent blessé et remise médicale validés sans régression ;
- textes anglais et français, limites des outils debug et `Player.log` final propres.

Points de régression durables :

- privilégier les actions vanilla de caravane, de génération/entrée de carte, de pause hostile, d'enrôlement, de portage et de sortie lorsqu'elles couvrent le besoin ;
- calculer un ancrage de scène unique avant de placer acteurs, structures, corps, débris et butin afin d'éviter les spawns indépendants incohérents ;
- ne pas générer les survivants alliés au bord de la carte et conserver une distance d'intervention raisonnable sur les grandes cartes ;
- exiger un soin réel du choc sans transformer la mission en construction obligatoire d'un hôpital temporaire ;
- ne comptabiliser une évacuation qu'après la sortie réelle du survivant, sans destruction ou disparition directe ;
- représenter les récompenses matérielles liées au site dans la scène dès sa génération, puis réserver les gains abstraits à la résolution ;
- conserver variante, menace, acteurs, équipe de récupération, affectations, butin et résultat à travers la sauvegarde/recharge ;
- forcer les builds de test après extraction d'un overlay afin que les dates archivées ne laissent pas une DLL précédente active ;
- revalider les cinq opérations organiques, leur récurrence et le slot global lors de toute extension aux caravanes, sites mondiaux ou interceptions.

## 0.3.28-dev - Audit de l'orchestration et de la récurrence Tok'ra

Validation locale terminée sur la révision `r1`, puis jalon publié sous `v0.3.28-dev`. Aucun correctif fonctionnel supplémentaire n'a été nécessaire.

Couverture validée :

- contrôle de cohérence positif pour `0.3.28-dev`, `0.3.28.0` et `83` backstories ;
- rebuild forcé et DLL `0.3.28.0` ;
- chargement des quatre MissionDefs et conservation de leurs données finales ;
- audit déterministe de `5000` tirages par palier de confiance terminé avec `PASS` ;
- atteignabilité de chaque archétype de poids positif et pénalité locale du dernier archétype ;
- filtrage des workers non proposables avant le tirage pondéré ;
- tirage naturel réel, refus de remplacer une offre active et slot global unique ;
- nouveau délai caché après réussite, échec et offre ignorée ;
- rééligibilité de chaque archétype sans cycle fixe ni exclusion permanente ;
- sauvegarde/rechargement pendant un délai caché, une offre et une opération active ;
- observation, renseignements, agent blessé et remise médicale sans régression ;
- communicateur limité à l'opération courante et diagnostics réservés au debug ;
- compatibilité avec un storyteller compatible différent ;
- `Player.log` final propre.

Points de régression durables :

- appeler `CanOffer(map)` et retirer les candidats indisponibles avant chaque tirage naturel ;
- conserver un seul slot global et ne jamais écraser une occurrence active ;
- distinguer l'absence de poids configuré de l'indisponibilité temporaire de tous les candidats ;
- programmer la prochaine opportunité après tous les résultats, y compris l'offre ignorée ;
- préserver l'historique, les compteurs, le dernier archétype et le prochain tick après sauvegarde/rechargement ;
- vérifier la récurrence et l'anti-répétition sur plusieurs occurrences, pas uniquement par lecture des Defs ;
- revalider les quatre opérations lors de toute extension aux sites mondiaux, caravanes ou interceptions ;
- maintenir les futurs pools de faction séparés au niveau RP tout en partageant le moteur technique ;
- ne pas confondre l'arc d'introduction Tok'ra unique avec les missions récurrentes qu'il débloquera ultérieurement.

## 0.3.27-dev - Migration de la remise médicale vers le framework de missions

Validation locale terminée sur la révision `r1`, puis jalon publié sous `v0.3.27-dev`. Aucun correctif fonctionnel supplémentaire n'a été nécessaire.

Couverture validée :

- contrôle de cohérence positif pour `0.3.27-dev`, `0.3.27.0` et `83` backstories ;
- rebuild forcé et DLL `0.3.27.0` ;
- chargement de quatre MissionDefs et du profil `handoff` sans erreur de configuration ;
- définition `SG1_TokraOrganic_MedicalSupplyHandoff` exposant cinq phases, les références du pawn, de la ressource, du job et de la compétence, les délais, les textes, les variantes et les conséquences ;
- flux complet validé : offre, arrivée différée, rencontre, dialogue, consommation exacte de deux médicaments industriels accessibles, réussite et départ ;
- consommation correcte à travers plusieurs piles sans détruire le surplus ;
- options désactivées pour ressource insuffisante ou inaccessible, incapacité sociale, réservation et absence de chemin ;
- récompense `Social +350` et confiance `+2` accordées une seule fois après remise ;
- mort, capture, disparition et expiration avant remise avec conséquence de confiance `-1` ;
- mort après remise mais avant sortie avec conséquence supplémentaire `-2`, sans annuler la réussite ni rendre les médicaments ;
- sauvegarde/rechargement pendant l'offre, avant l'arrivée, pendant l'approche, au point de rencontre et pendant le départ surveillé ;
- migration prudente d'une occurrence créée sous `v0.3.26-dev` sans perte du pawn, de l'état ou de l'échéance ;
- trois variantes d'offre et trois variantes de réussite avec anti-répétition immédiate ;
- rééligibilité, délais cachés contextuels et pénalité de poids `0.25` pour le dernier archétype ;
- observation, récupération de renseignements et agent blessé validés sans régression ;
- outils techniques limités au debug et `Player.log` final propre.

Points de régression durables :

- conserver le MissionDef comme source unique du PawnKind, de la ressource, de la quantité, du JobDef, du SkillDef, des délais, textes, récompenses et conséquences migrés ;
- désactiver explicitement l'archétype lorsqu'une configuration requise est absente ou invalide, sans fallback C# complet silencieux ;
- conserver le spawn, le Lord, le déplacement, les réservations, le dialogue et la consommation réelle des piles dans l'adaptateur tant qu'un second usage réel ne justifie pas leur mutualisation ;
- consommer exactement la quantité configurée parmi les ressources accessibles et ne jamais compter les piles interdites ou inatteignables ;
- distinguer les échecs avant remise de la conséquence post-remise sans annuler ni dupliquer la réussite ;
- préserver le pawn, les échéances et la conséquence post-remise en attente après sauvegarde/rechargement ;
- couvrir variantes, anti-répétition, récurrence, offre ignorée, réussite et échecs sur plusieurs occurrences ;
- revalider les trois autres opérations après toute modification du XML ou du moteur partagé ;
- ajouter de nouvelles abstractions uniquement lorsqu'un autre cas réel démontre un besoin commun.

## 0.3.26-dev - Migration des soins de l'agent blessé vers le framework de missions

Validation locale terminée sur la révision `r2`, puis jalon publié sous `v0.3.26-dev`. La révision `r1` a validé le flux complet de l'agent blessé. La révision `r2` a restauré le champ XML `workTicks=5000` de l'analyse accélérée des renseignements, omis lors de l'assemblage de `r1`, sans modifier le C# ni le gameplay de l'agent blessé.

Couverture validée :

- contrôle de cohérence positif pour `0.3.26-dev`, `0.3.26.0` et `83` backstories ;
- rebuild forcé et DLL `0.3.26.0` ;
- chargement des trois MissionDefs, y compris l'analyse accélérée des renseignements à `5000` ticks ;
- définition `SG1_TokraOrganic_WoundedAgentCare` exposant six phases, le profil `pawnCare`, les références de pawn et de Hediffs, les seuils médicaux, les durées, les textes, les variantes et les plages de récurrence ;
- flux complet validé : offre, arrivée, sauvetage, lit médical, soin réel du choc de symbiote, récupération post-choc, soins vanilla, `5000` ticks de stabilité, départ et réussite uniquement après sortie de carte ;
- confiance `+3` accordée une seule fois après sortie réussie ;
- difficulté adaptative validée sur colonie faible et avancée à partir du snapshot mis à l'échelle capturé lors de l'offre ;
- chance et sévérité de l'affection optionnelle persistantes, sans reroll après modification de richesse ou sauvegarde/rechargement ;
- mort, capture, disparition, expiration des soins et échec de départ après la grâce de `60000` ticks ;
- pénalité de confiance `-2` et nettoyage correct de l'objectif pour chaque échec ;
- sauvegarde/rechargement pendant l'offre, avant le premier soin, pendant la récupération, pendant la stabilité et pendant le départ ;
- migration prudente d'une occurrence créée avant la migration sans perte du pawn ni duplication de résultat ;
- trois variantes d'offre et trois variantes de réussite avec anti-répétition immédiate ;
- rééligibilité et délais cachés contextuels après résolution ;
- observation, récupération de renseignements et remise médicale validées sans régression ;
- outils techniques limités au debug et `Player.log` final propre.

Points de régression durables :

- conserver le MissionDef comme source unique du PawnKind, des Hediffs, seuils, durées, paramètres d'affection, textes, récurrence et conséquences migrés ;
- désactiver explicitement l'archétype lorsqu'une configuration requise est absente ou invalide, sans fallback C# complet silencieux ;
- préserver les soins vanilla, l'évaluation de santé, le Lord et le départ dans l'adaptateur tant qu'un second usage réel ne justifie pas leur mutualisation ;
- capturer la difficulté à l'offre et conserver les paramètres médicaux de l'occurrence après sauvegarde/rechargement ;
- exiger un soin réel du choc de symbiote et ne jamais valider la mission sur la seule stabilisation médicale ;
- accorder la réussite uniquement après la sortie vivante du pawn ;
- couvrir mort, capture, perte, expiration et incapacité durable à quitter la carte ;
- tester les menaces ou contraintes adaptatives sur une colonie faible et une colonie avancée ;
- vérifier variantes, anti-répétition, récurrence, plusieurs points de sauvegarde/recharge et anciennes sauvegardes ;
- revalider toutes les opérations déjà migrées après toute modification du XML partagé ;
- conserver un validateur strict : l'omission de `workTicks` en `r1` doit continuer à désactiver explicitement l'opération concernée plutôt qu'à masquer l'erreur.

## 0.3.25-dev - Migration de la récupération de renseignements vers le framework de missions

Validation locale terminée sur la révision `r2`, puis jalon publié sous `v0.3.25-dev`. La révision `r2` a porté les durées finales à `10000` ticks pour l'analyse prudente et `5000` ticks pour l'analyse accélérée.

Couverture validée :

- contrôle de cohérence positif pour `0.3.25-dev`, `0.3.25.0` et `83` backstories ;
- rebuild forcé et DLL `0.3.25.0` ;
- chargement des deux MissionDefs et de toutes les références `ThingDef`, `JobDef`, `SkillDef` et `IncidentDef` requises ;
- définition de récupération de renseignements exposant six phases, trois banques nommées, les délais contextuels, les conséquences et le profil `ThreatPointsScaled` ;
- méthode prudente validée à `10000` ticks avec `Intellectual +350`, sans patrouille ;
- méthode accélérée validée à `5000` ticks avec `Intellectual +500` au total ;
- interruption, reprise et sauvegarde/rechargement sans remise à zéro ni modification du total configuré ;
- trois banques de résultats indépendantes avec variantes pondérées et anti-répétition immédiate ;
- expiration de l'offre, expiration après acceptation et perte du module sans récompense ni patrouille résiduelle ;
- récurrence pilotée par les quatre plages de confiance configurées dans le MissionDef ;
- interférence forcée utilisant `clamp(base threat × 0.35, 180, 700)` ;
- consommation du snapshot mis à l'échelle capturé lors de l'offre, sans recalcul après changement de richesse ou de situation ;
- menace adaptative testée sur une colonie faible et une colonie avancée ;
- migration prudente des états créés avant la migration, sans module, résultat ou récompense dupliqués ;
- observation, agent blessé et remise médicale validés sans régression ;
- outils techniques limités au debug et `Player.log` propre.

Points de régression durables :

- conserver le MissionDef comme source unique des durées, récompenses, textes, délais, références de Defs et paramètres de conséquence migrés ;
- désactiver explicitement l'archétype lorsqu'une configuration requise est absente ou invalide, sans fallback C# complet silencieux ;
- figer le snapshot de menace à l'offre et vérifier que toute conséquence ultérieure consomme cette valeur ;
- tester toute menace adaptative sur au moins une colonie faible et une colonie avancée ;
- préserver le total et la progression des analyses déjà commencées lors d'un rééquilibrage XML ;
- maintenir des historiques d'anti-répétition indépendants pour les banques nommées ;
- couvrir réussite, échecs, récurrence, plusieurs points de sauvegarde/recharge et migration des anciennes sauvegardes ;
- vérifier toutes les opérations encore héritées après chaque migration progressive ;
- généraliser seulement les capacités démontrées par plusieurs cas réels.

## 0.3.24-dev - Complete observation mission Def migration

Validation locale terminée sur la révision `r1`, puis jalon publié sous `v0.3.24-dev`. Aucun correctif fonctionnel supplémentaire n'a été nécessaire.

Couverture validée :

- contrôle de cohérence positif pour `0.3.24-dev`, `0.3.24.0` et `83` backstories ;
- rebuild forcé et DLL `0.3.24.0` ;
- chargement de `SG1_TokraOrganic_GoauldObservation` avec toutes ses références `ThingDef`, `JobDef` et `SkillDef` valides ;
- rapport développeur conforme avec `26` textes d'exécution, la plage de récurrence `240000–480000`, les quatre durées et la récompense `Intellectual +250` ;
- flux complet validé : livraison, déploiement `500`, observation `10000`, récupération `500` et transmission `1000` ticks ;
- XP active appliquée depuis la compétence et le taux configurés, puis récompense finale générique accordée une seule fois ;
- aucune utilisation de l'ancienne définition C# complète de secours ;
- désactivation explicite et diagnostic clair prévus pour toute définition requise absente, incomplète, dupliquée ou invalide ;
- sauvegarde/recharge validée pendant l'offre, l'observation partielle et la transmission interrompue ;
- migration prudente d'une occurrence active créée sous `v0.3.23-dev` ;
- variantes de réussite pondérées et anti-répétition immédiate validées ;
- échec par expiration et par perte du dispositif ou du point d'observation, sans XP final ;
- prochaine opportunité planifiée depuis la plage XML du MissionDef ;
- opérations de renseignements, d'agent blessé et de remise médicale inchangées sur leurs flux C# hérités ;
- scénario SG, scénario vanilla, limites des outils debug et `Player.log` propres pour le périmètre testé.

Points de régression durables :

- toute donnée déclarative migrée doit avoir le MissionDef comme source unique, sans fallback silencieux ni duplication C# active ;
- valider les références de Defs au chargement avant de rendre un archétype éligible ;
- conserver en C# les mécaniques RimWorld spécialisées tant qu'un second usage réel ne justifie pas leur généralisation ;
- préserver le total, la progression et les objets persistants des occurrences commencées avant un changement de configuration ;
- tester séparément XP active, récompense finale et absence de récompense en cas d'échec ;
- couvrir les variantes, l'anti-répétition, les échecs, la récurrence et plusieurs points de sauvegarde/recharge ;
- vérifier les opérations encore héritées après chaque migration progressive ;
- maintenir les données techniques hors des interfaces joueur et réserver les rapports détaillés au debug.

## 0.3.23-dev - Fondation réutilisable du framework de missions

Validation locale terminée sur la révision `r2`, puis jalon publié sous `v0.3.23-dev`. La révision `r2` a remplacé la plage C# codée en dur de l'observation par la durée `workTicks` réellement lue depuis le Def et fixée à `10000` ticks, soit quatre heures en jeu.

Couverture validée :

- contrôle de cohérence positif pour `0.3.23-dev`, `0.3.23.0` et `83` backstories ;
- rebuild forcé et DLL `0.3.23.0` ;
- chargement de `GateRimMissionDef`, des phases, objectifs, textes, récompenses et traductions sans nouvelle erreur ;
- rapport développeur commun listant la définition pilote, ses sept phases, ses trois variantes, son facteur de répétition et sa capture de menace ;
- apparition des trois variantes d'offre RP et exclusion de la dernière variante utilisée lors du tirage suivant ;
- absence de poids, index, points de menace ou autres détails techniques dans les textes joueur ;
- flux complet de l'observation Tok'ra conservé : offre, acceptation, livraison, déploiement, observation, repli, retour, transmission et résolution ;
- nouvelle durée de `10000` ticks annoncée comme quatre heures et restant perceptible en vitesse maximale ;
- sauvegarde/recharge pendant l'offre, après acceptation et pendant l'observation sans reroll, redémarrage ni perte de progression ;
- conservation du total déjà enregistré pour une observation commencée avant le correctif de durée ;
- migration prudente d'une ancienne occurrence vers les données génériques sans recréer la mission ;
- capture de menace positive sur une carte active et sensiblement supérieure sur une colonie matériellement plus puissante ;
- régressions validées pour les opérations de renseignements, d'agent blessé et de remise médicale encore exécutées par leur code C# historique ;
- scénario SG, scénario vanilla, frontières des outils debug et `Player.log` propres pour le périmètre testé.

Points de régression durables :

- conserver les Defs comme source réelle des paramètres migrés ; un champ XML documenté ne doit pas rester doublé par une constante C# active ;
- ne pas modifier rétroactivement le total ou la progression d'une occurrence déjà commencée lors d'un rééquilibrage ;
- tester l'anti-répétition sur plusieurs occurrences et non seulement la présence des variantes dans le Def ;
- vérifier la rééligibilité après résolution, échec et offre ignorée pour tout archétype déclaré récurrent ;
- comparer les points de menace sur des colonies de puissances différentes avant de considérer la capture adaptative comme valide ;
- valider leur consommation réelle sur une mission comportant naturellement une menace avant d'étendre davantage l'abstraction ;
- conserver les opérations non migrées sur leur flux éprouvé et vérifier qu'aucune donnée générique d'une autre mission ne fuit vers elles ;
- ajouter une abstraction commune uniquement lorsqu'un second usage réel la justifie ;
- maintenir les diagnostics et actions de phase hors du jeu normal ;
- vérifier la persistance et la migration à chaque nouvelle phase ou donnée générique.

## 0.3.22-dev - Casquette de terrain SG

Validation locale terminée sur la révision `r1`, puis jalon publié sous `v0.3.22-dev`. Aucun correctif fonctionnel supplémentaire n'a été nécessaire.

Couverture validée :

- contrôle de cohérence positif pour `0.3.22-dev`, `0.3.22.0` et `83` backstories ;
- rebuild forcé et DLL `0.3.22.0` ;
- chargement sans nouvelle erreur de Def, traduction ou texture ;
- observation des trois résultats pondérés : casque, casquette et aucun couvre-chef ;
- rendu de la casquette validé au nord, au sud, à l'est et à l'ouest sur les morphologies testées ;
- casque et casquette mutuellement exclusifs ;
- tee-shirt, pantalon, bottes, gants, gilet et veste facultative assortie inchangés ;
- assortiment d'armes vanilla et fournitures du scénario inchangés ;
- sauvegarde/recharge avec casquette équipée ;
- scénario vanilla non affecté ;
- `Player.log` propre.

Points de régression durables :

- interpréter les poids comme des probabilités et non comme des pourcentages garantis sur un petit échantillon ;
- vérifier toute nouvelle pièce portée dans les quatre directions, sur plusieurs morphologies et après sauvegarde/recharge ;
- conserver les couvre-chefs concurrents sur un emplacement mutuellement exclusif ;
- ajouter les futures options de loadout principalement par XML sans réintroduire de logique spécifique au scénario ;
- maintenir ensemble les Defs, traductions, textures, documentation technique et pages wiki.

## 0.3.21-dev - Règles culturelles d'équipement des starters

Validation locale terminée sur la révision `r4`, puis jalon publié sous `v0.3.21-dev`. La révision `r3` a révélé une référence vanilla incorrecte `Apparel_Tshirt`, corrigée vers `Apparel_BasicShirt` dans `r4`.

Couverture validée :

- contrôle de cohérence positif pour `0.3.21-dev`, `0.3.21.0` et `83` backstories ;
- contrôle final compatible avec les libellés `Version de DLL attendue` et `Version de DLL validée`, sans second diagnostic redondant si le champ est absent ;
- détection d'une tabulation Markdown volontaire, nettoyage de la sonde puis nouveau passage positif ;
- rebuild forcé et DLL `0.3.21.0` ;
- restrictions SG-team d'âge biologique minimum et de capacité de violence ;
- noms Tau'ri et carrières SGC conservés ;
- tee-shirt vanilla, pantalon SG, bottes, gants et gilet toujours équipés à qualité normale ;
- pantalons olive, noirs et désert observés ;
- veste facultative conservant toujours la même variante que le pantalon du pawn ;
- cas avec et sans veste, puis avec et sans casque ;
- coexistence correcte des couches tee-shirt, veste et gilet ;
- vérification visuelle des pièces séparées dans les orientations et morphologies testées ;
- assortiment d'un fusil d'assaut, d'un pistolet-mitrailleur, d'un pistolet automatique et d'un fusil à pompe ;
- fournitures habituelles présentes et absence des quatre casques anciennement déposés au sol ;
- scénario vanilla non affecté ;
- sauvegarde/recharge stable et `Player.log` propre.

Points de régression durables :

- garder les règles de starter limitées au contexte `PlayerStarter` et au profil culturel réellement sélectionné ;
- conserver le marqueur de scénario comme identifiant sans réintroduire de logique spécifique dans son `ScenPart` ;
- vérifier les conflits de couches à chaque ajout d'un vêtement obligatoire ou facultatif ;
- maintenir des ensembles de `variantKey` identiques entre les slots d'un même `variantGroup` ;
- tester les options pondérées sur un échantillon suffisant sans transformer leur poids en pourcentage garanti ;
- conserver les anciens Defs de treillis combinés pour les sauvegardes sans les réutiliser dans le scénario ;
- ajouter la future casquette SG dans le slot de couvre-chef existant ;
- conserver les armes humaines vanilla comme base et réserver les armes de mods à des patchs de compatibilité facultatifs ;
- utiliser `/` dans les chemins relatifs PowerShell documentés et conserver l'audit des tabulations Markdown.

## 0.3.20-dev - Contrôle automatisé de cohérence du projet

Validation locale terminée sur la révision `r2`, puis jalon publié sous `v0.3.20-dev`. La révision `r1` a révélé une incompatibilité de syntaxe avec Windows PowerShell 5.1, corrigée et validée dans `r2`.

Couverture validée :

- contrôle positif de la version `0.3.20-dev`, de l'assembly `0.3.20.0` et des `83` backstories ;
- comparaison des versions About, projet, README, wiki, état du projet, tests courants et changelog ;
- comptage réel des `BackstoryDef`, unicité des `defName` et comparaison avec les résumés publics et techniques ;
- comparaison des 83 lignes du catalogue wiki avec les Defs chargés ;
- test négatif par version volontairement incorrecte, avec message `[FAIL]` et code de sortie non nul ;
- nouveau passage positif immédiatement après le test négatif ;
- absence de mutation de l'arbre de travail pendant toutes les exécutions ;
- rebuild forcé et DLL `0.3.20.0` ;
- chargement du menu principal, version About correcte et `Player.log` propre ;
- README et pages wiki alignés sur `0.3.20-dev` et `83` backstories.

Points de régression durables :

- exécuter `tools/check-project-consistency.cmd` après la passe documentaire finale et avant `git add -A` ;
- conserver `About/About.xml` comme version de développement autoritative ;
- mettre à jour ensemble l'outil et `docs/PROJECT_CONSISTENCY_CHECKS.md` lorsqu'un format contrôlé évolue ;
- tester périodiquement un échec volontaire sans éditer le dépôt ;
- vérifier que le contrôleur reste strictement en lecture seule ;
- ne pas substituer ce contrôle au rebuild, au chargement RimWorld, à la relecture RP ou à l'audit du wiki.


## 0.3.19-dev - Audit de couverture des compétences culturelles

Validation locale terminée sur la révision `r1`, puis jalon publié sous `v0.3.19-dev`. Aucun correctif C# ou Def supplémentaire n'a été nécessaire après la matrice complète.

Couverture validée :

- audit des douze compétences sur les véritables combinaisons enfance/adulte de sept profils culturels ;
- couverture Jaffa Goa'uld et Jaffa libres déjà complète, sans ajout artificiel ;
- onze carrières ciblées ajoutées aux seuls profils présentant une absence réelle ;
- trois carrières SGC validées dans le scénario exclusif et les listes additives prévues ;
- carrières ordinaires Goa'uld, Grands Maîtres et Tok'ra validées avec leurs groupes de noms ;
- deux origines d'hôtes historiques conservant leurs poids et identités persistantes ;
- humains ordinaires restant majoritairement vanilla ;
- sauvegarde/recharge et ancienne sauvegarde `0.3.18-dev` sans reroll ;
- dix basculements Tok'ra sans dérive des compétences ;
- catalogue wiki complet de `83` backstories ;
- `Player.log` propre.

Points de régression durables :

- mesurer la couverture à partir des pools réellement combinables, pas à partir de chaque fichier isolé ;
- distinguer une compétence absente d'une compétence seulement peu redondante ;
- ne pas ajouter de backstory uniquement pour équilibrer des volumes entre cultures ;
- vérifier les groupes de noms lors de tout ajout à un profil mixte ;
- conserver les poids et identités persistantes des hôtes générés lors de l'extension des listes ;
- appliquer le même audit aux futures cultures dès qu'elles disposent de pools réels ;
- mettre à jour simultanément la matrice technique et le catalogue wiki.


## 0.3.18-dev - Origine Tau'ri des hôtes Tok'ra générés

Validation locale terminée sur la révision `r1`, puis jalon publié sous `v0.3.18-dev`. Aucun correctif C# ou Def supplémentaire n'a été nécessaire après la matrice complète.

Couverture validée :

- rebuild forcé et DLL `0.3.18.0` ;
- chargement sans nouvelle erreur de Def, backstory, patch ou traduction ;
- génération des origines `SG1_GeneratedHost_OffworldHuman` et `SG1_GeneratedHost_TauriSGCVolunteer` ;
- origine humaine hors-monde restant clairement majoritaire avec des poids relatifs `1` et `0.2` ;
- noms Tau'ri, deux enfances modernes et huit carrières SGC cohérents pour la nouvelle origine ;
- pools de noms et de backstories humains hors-monde inchangés ;
- host/symbiote distincts, hôte actif par défaut et marqueur `GeneratedPreJoined` ;
- dix basculements successifs sans dérive de compétences ;
- sauvegarde/recharge avec chaque personnalité active ;
- ancienne identité `0.3.17-dev` conservée sans reroll ;
- extraction puis vraie réimplantation utilisant `ImplantedExistingHost` et effaçant l'origine générée ;
- absence des deux nouvelles enfances dans les pools de starters ordinaires et de l'équipe SG ;
- catalogue wiki complet de `72` backstories ;
- `Player.log` propre.

Points de régression durables :

- traiter les poids d'origine comme des poids relatifs, jamais comme un pourcentage garanti sur un petit échantillon ;
- conserver la sélection d'origine, du nom et des backstories stable à partir de l'identité persistante ;
- ne jamais rerouler une identité déjà sauvegardée lorsqu'une nouvelle origine est ajoutée ;
- conserver les implantations réelles prioritaires sur toute origine historique générée ;
- limiter les enfances dédiées à leur catégorie de génération prévue ;
- ajouter les futures origines principalement par XML tant que le schéma actuel reste suffisant ;
- mettre à jour le catalogue wiki dans le même jalon que toute modification de backstory.


## 0.3.17-dev - Harmonisation de la présentation du projet et du wiki

Validation locale terminée sur la révision `r2`, puis jalon publié sous `v0.3.17-dev`. Aucun correctif de gameplay n'a été nécessaire.

Couverture validée :

- rebuild forcé et DLL `0.3.17.0` ;
- chargement jusqu'au menu principal sans nouvelle erreur GateRim SG-1 ;
- version de mod `0.3.17-dev` et description About inchangée ;
- remplacement du README historique par une présentation durable ;
- accueil du wiki et état du contenu cohérents avec les systèmes validés jusqu'à `0.3.17-dev` ;
- distinction claire entre contenu jouable et grands développements futurs ;
- sidebar organisée en catégories thématiques lisibles ;
- conservation de toutes les anciennes cibles internes sans doublon ;
- ajout à la navigation des pages `Tokra-Dual-Identity` et `Tokra-Tactical-Threat-Assessment` ;
- vérification des liens internes et externes du périmètre ;
- absence de modification des fichiers C#, Defs, traductions, textures et de `About/ModIcon.png` ;
- `Player.log` propre pour le test de chargement.

Points de régression durables :

- conserver le README comme présentation stable plutôt que comme journal du dernier micro-jalon ;
- mettre à jour `Home.md` et `Content-Status.md` lorsqu'un changement public rend leur résumé obsolète ;
- classer toute nouvelle page wiki dans une catégorie thématique de `_Sidebar.md` ;
- vérifier à chaque création, renommage ou suppression de page que la sidebar ne contient ni cible absente, ni doublon, ni lien obsolète ;
- ne pas présenter comme jouable un système encore limité à la roadmap ;
- préserver la séparation entre documentation joueur, documents de développement et diagnostics techniques.


## 0.3.16-dev - Extension mesurée des backstories culturelles

Validation locale terminée sur la révision `r1`, puis jalon publié sous `v0.3.16-dev`. Aucun correctif C# ou Def supplémentaire n'a été nécessaire après la matrice complète.

Couverture validée :

- rebuild forcé et DLL `0.3.16.0` ;
- chargement sans nouvelle erreur XML, BackstoryDef, `skillGains`, patch ou traduction ;
- douze titres, descriptions et bonus français conformes aux Defs et au wiki ;
- nouveaux pools d'enfances et de carrières Jaffa, avec groupes de noms `GoauldJaffa` et `FreeJaffa` cohérents ;
- nouvelles carrières d'hôtes Goa'uld avec groupes `Goauld` et `Tokra` cohérents ;
- huit carrières SGC exclusives dans le scénario Équipe SG isolée ;
- intégration additive des carrières SGC chez les humains ordinaires sans dépasser la majorité vanilla ;
- génération représentative de pawns, raids et identités culturelles sans régression ;
- sauvegarde/recharge, noms manuels et basculement Tok'ra sans reroll ni dérive de compétences ;
- catalogue wiki complet de `70` backstories ;
- `Player.log` propre.

Points de régression durables :

- étendre les catalogues existants par Defs et patches XML tant que le schéma actuel exprime correctement le besoin ;
- conserver des bonus modérés sans imposer traits, passions, incapacités ou multiplicateurs directs ;
- maintenir la cohérence entre les pools mixtes de backstories et leurs règles de noms culturels ;
- préserver la majorité vanilla du profil humain additif et l'exclusivité SGC du scénario dédié ;
- mettre à jour le tableau wiki correspondant dans le même jalon que toute modification de backstory ;
- ne pas ajouter de backstories Asgard, Nox, Unas ou d'une autre culture avant que cette culture existe réellement dans le mod.

## 0.3.15-dev - Diagnostic unifié de l'identité culturelle

Validation locale terminée sur la révision `r1`, puis jalon publié sous `v0.3.15-dev`. Aucun correctif fonctionnel supplémentaire n'a été nécessaire après la matrice complète.

Couverture validée :

- rebuild forcé et DLL `0.3.15.0` ;
- chargement sans nouvelle erreur rouge et message propre lorsqu'aucun pawn n'est sélectionné ;
- cohérence du rapport pour un humain ordinaire, un Jaffa libre, un Jaffa Goa'uld, un hôte Goa'uld et un Tok'ra pré-fusionné ;
- séparation correcte des contextes `NonPlayer` et `PlayerStarter` ;
- cohérence entre profils, groupe de noms, backstories, faction, physiologie Jaffa, Prim'ta, marque frontale et identité sociale ;
- conservation du même enregistrement persistant lors d'un basculement de personnalité Tok'ra ;
- accès identique depuis l'action développeur et les options avancées ;
- masquage complet lorsque le mode développeur et l'option avancée sont désactivés ;
- ouverture répétée, sauvegarde/recharge, conservation des noms manuels et absence de modification du pawn ;
- `Player.log` propre.

Points de régression durables :

- conserver un seul rapport central qui interroge les services autoritatifs existants ;
- ne jamais réserver un nom, remplacer une backstory, corriger une faction ou initialiser une identité pendant l'inspection ;
- afficher séparément `NonPlayer` et `PlayerStarter`, car leurs profils peuvent légitimement différer ;
- garder les accès cachés hors mode développeur ou option avancée explicite ;
- corriger toute incohérence future dans le sous-système propriétaire de la donnée, pas par une synchronisation propre au rapport.

> Current milestone tests and validation status: `docs/TESTING_CURRENT.md`. This file remains the complete historical regression archive.

## 0.3.14-dev - Mort, cadavre, tombe et résurrection de l'identité Tok'ra

Validation locale terminée sur la révision `r1`, puis jalon publié sous `v0.3.14-dev`. Aucun changement C# n'a été nécessaire : l'architecture persistante existante couvre correctement le cycle de vie testé.

Couverture validée :

- mort avec l'hôte actif puis sauvegarde/recharge du cadavre ;
- inhumation, sauvegarde/recharge de la tombe et récupération du cadavre ;
- résurrection avec conservation des deux identités, de la personnalité active et de la progression commune ;
- répétition complète avec le symbiote actif au moment de la mort ;
- absence de gizmo sur un pawn mort, un cadavre ou une tombe ;
- retour d'un gizmo unique après résurrection pour un colon directement contrôlé ;
- dix basculements post-résurrection sans perte, duplication ni cumul ;
- extraction et réimplantation après résurrection ;
- régressions sur Goa'uld, humains ordinaires, sauvegarde et `Player.log`.

Points de régression durables :

- conserver un seul pawn et un seul objet persistant de données de symbiote à travers le cadavre et la tombe ;
- ne pas créer de seconde identité propre au cadavre ou à la résurrection ;
- accepter que le libellé vanilla du cadavre ou de la tombe suive le nom actif au moment de la mort tant que les deux identités restent stockées ;
- ne pas restaurer automatiquement l'hôte à la mort sans besoin fonctionnel démontré ;
- n'ajouter un correctif qu'après reproduction d'un défaut précis avec l'hôte actif ou le symbiote actif.

## 0.3.13-dev - Identités distinctes pour les Tok'ra générés déjà fusionnés

Validation locale terminée après la révision `r2`, puis jalon publié et wiki synchronisé. La couverture durable reste conservée ci-dessous.

Couverture obligatoire :

- chargement des `GeneratedHostOriginDef` et du profil Tok'ra sans erreur XML ni référence manquante ;
- génération directe de `SG1_TokraVoluntaryHost` avec un nom d'hôte et un nom de symbiote distincts ;
- enfance et carrière humaines hors-monde pour l'hôte, carrière Tok'ra pour le symbiote ;
- conservation de l'hôte actif pendant la finalisation différée du nom culturel du symbiote ;
- basculements répétés sans dérive des compétences ni reroll des identités ;
- sauvegarde/recharge avec l'hôte actif puis le symbiote actif ;
- migration stable d'une sauvegarde `0.3.12-dev` contenant un Tok'ra pré-fusionné ;
- implantation Tok'ra réelle conservant l'identité existante du pawn ;
- extraction puis réimplantation remplaçant l'origine générée par le nouvel hôte réel ;
- visiteurs, escortes, chef de faction et autres générations hors carte lorsque disponibles ;
- absence de régression sur les caravanes, les Tok'ra IA, les Goa'uld et `Player.log`.

Points de régression durables :

- ne jamais déduire la source d'identité en comparant les noms affichés ;
- conserver un marqueur persistant explicite pour les implantations réelles et les générations pré-fusionnées ;
- générer les origines d'hôte par Defs XML pondérés et non par branches culturelles codées en dur ;
- conserver le nom du symbiote sans écraser le nom de l'hôte actif ;
- ne pas appliquer l'identité d'hôte historique générée au nouveau pawn après une vraie réimplantation ;
- maintenir le catalogue wiki à jour lors de toute modification des six carrières humaines hors-monde.

## 0.3.12-dev - Intégration de l'identité Tok'ra active

Validation locale terminée et jalon publié. Les tests détaillés et leurs résultats restent conservés dans l'historique du projet.

Couverture obligatoire :

- basculement sur carte, formation d'une caravane, basculement sur la carte du monde puis retour sur carte ;
- menu unique pour une caravane contenant un ou plusieurs Tok'ra du joueur ;
- exclusion des invités, prisonniers, esclaves, pawns en état mental et Tok'ra gérés par l'IA ;
- cohérence des onglets Bio, Social et Santé ainsi que de la fiche du pawn depuis une caravane ;
- conservation de la progression commune, de l'anti-cumul, de la sauvegarde et de l'extraction validés dans `0.3.11-dev` ;
- audit facultatif de la mort, du cadavre et de la résurrection lorsqu'un flux de test sûr est disponible ;
- `Player.log` propre.

Points de régression durables :

- ne jamais utiliser `Pawn.IsColonistPlayerControlled` seul pour décider qu'un colon permanent en caravane a perdu le contrôle joueur ;
- utiliser `Caravan.IsOwner(pawn)` afin de ne pas exposer la mécanique aux invités, prisonniers ou esclaves transportés ;
- conserver un seul service de basculement et un seul modèle de progression partagée ;
- ne pas réécrire rétroactivement les lettres ou messages historiques déjà créés ;
- ne corriger les interfaces vanilla qu'après reproduction d'une incohérence réelle.

## 0.3.10-dev - Persistance de l'identité Tok'ra implantée

Validation locale terminée sur `r1`. Les tests détaillés et leurs résultats sont conservés dans `docs/TESTING_CURRENT.md`.

Couverture validée :

- chargement des pools d'identité configurés dans le profil Tok'ra ;
- implantation volontaire sans changement du nom principal, des backstories actives ou des compétences de l'hôte ;
- affichage du nom de l'hôte, du nom du symbiote et des deux parcours sur un colon Tok'ra contrôlé par le joueur ;
- transfert intact lors de la conversion entre implantation récente et hôte actif ;
- migration d'une sauvegarde `0.3.9-dev` sans remplacement de l'identité existante ;
- sauvegarde, arrêt complet et rechargement sans reroll ;
- extraction et réimplantation du même symbiote avec conservation de son nom et de sa carrière ;
- remplacement des seules informations d'hôte lors d'un transfert vers un nouveau colon ;
- absence du nouveau résumé et de tout gizmo sur les Tok'ra gérés par l'IA ;
- absence de régression sur les implantations et extractions Goa'uld ;
- `Player.log` propre.

Points de régression durables :

- ne jamais modifier les backstories actives ou les compétences dans la phase de persistance seule ;
- conserver les données dans l'objet de symbiote déjà transféré entre les états ;
- ne pas exposer le futur basculement aux visiteurs, alliés, ennemis ou pawns de quête non recrutés ;
- vérifier extraction et réimplantation à chaque évolution du format d'identité.


## 0.3.9-dev - Profils culturels configurables pour les starters

Validation locale terminée sur `r3`.

Couverture validée :

- chargement des huit profils culturels et du ScenPart caché sans erreur XML ou dépendance Harmony ;
- scénario `Équipe SG isolée` limité aux six carrières adultes SGC configurées ;
- starters Jaffa limités aux enfances Jaffa et aux carrières Jaffa Goa'uld ou Jaffa libres ;
- starters hôtes Goa'uld limités aux enfances humaines hors-monde et aux carrières Goa'uld ou Tok'ra ;
- cohérence entre la carrière finale, le groupe de noms et les écarts de compétences appliqués ;
- profil humain ordinaire conservant une nette majorité de carrières vanilla, avec apparition occasionnelle des carrières Tau'ri / SGC selon leur poids relatif dans le pool vanilla compatible ;
- absence de pourcentage fixe pour le profil humain additif ;
- priorité conservée pour les profils exclusifs Jaffa, hôte Goa'uld et scénario SG-1 ;
- modifications manuelles réalisées après génération conservées au démarrage ;
- aucune application du filtrage starter aux PawnKinds debug, raids, colonies, visiteurs, incidents, quêtes ou autres pawns du monde ;
- sauvegarde et rechargement sans second renommage, reroll de backstory ou réapplication de compétence ;
- `Player.log` propre pour le périmètre testé.

Points de régression durables :

- conserver la frontière stricte entre `PlayerStarter` et génération du monde ;
- conserver la pondération relative au pool vanilla plutôt qu'un taux fixe pour les humains ordinaires ;
- vérifier les priorités lorsqu'un nouveau profil culturel est ajouté ;
- vérifier la compatibilité des sélections manuelles lorsqu'un éditeur de pawns est mis à jour ;
- vérifier l'absence de cumul des écarts de compétences après sauvegarde et rechargement.

## 0.3.8-dev - Refonte des backstories culturelles

Cette série vérifie les `52` histoires existantes, leurs descriptions enrichies, leurs bonus de compétences et l'absence de régression sur la génération culturelle.

### Préconditions

- Créer la branche `feature/cultural-backstory-rework` depuis `v0.3.7-dev`.
- Extraire l'archive à la racine du dépôt.
- Effectuer un rebuild complet de `GateRimSG1.dll`.
- Utiliser le français pour contrôler les traductions, puis effectuer au moins un chargement en anglais si possible.

### Test 1 — Chargement des Defs

1. Démarrer RimWorld avec GateRim SG-1 et Biotech.
2. Attendre l'arrivée au menu principal.
3. Vérifier l'absence de fenêtre d'erreur XML.
4. Ouvrir le journal développeur et rechercher `BackstoryDef`, `skillGains`, `SG1_CulturalBackstories` et `translation`.

Résultat attendu : les `52` histoires et leurs traductions se chargent sans erreur ni clé dupliquée.

### Test 2 — Équipe SG isolée

1. Créer une nouvelle partie avec le scénario `Équipe SG isolée`.
2. Sur l'écran de sélection, inspecter les quatre candidats Tau'ri.
3. Régénérer plusieurs fois les candidats afin d'obtenir plusieurs carrières SGC.
4. Lire les descriptions et vérifier qu'elles sont composées de deux idées cohérentes, sans formulation technique.
5. Comparer les compétences visibles au parcours affiché : forces spéciales, médecine, recherche, linguistique, ingénierie ou liaison.
6. Modifier manuellement un nom, lancer la partie et vérifier qu'il reste inchangé.

Résultat attendu : les carrières SGC restent optionnelles, lisibles et cohérentes avec les compétences, sans régression sur les noms du scénario.

### Test 3 — Génération développeur immédiate

1. Mettre le jeu en pause.
2. Utiliser `Debug actions menu` → `Spawn pawn` sur les PawnKinds `SG1_...` disponibles.
3. Tester au minimum les quatre Jaffa suivants :
   - `SG1_GoauldJaffaWarrior` ;
   - `SG1_GoauldJaffaGuard` ;
   - `SG1_GoauldSettlementJaffaWarrior` ;
   - `SG1_GoauldSettlementJaffaGuard`.
4. Tester aussi les PawnKinds disponibles pour les Jaffa libres, les hôtes Goa'uld, les Grands Maîtres et les hôtes Tok'ra.
5. Inspecter immédiatement l'enfance, l'âge adulte, la description et les compétences sans reprendre le temps.
6. Reprendre le temps et vérifier qu'aucune histoire, compétence ou identité n'est remplacée une seconde fois.

Résultat attendu : les filtres culturels continuent de sélectionner les mêmes catégories et les nouveaux bonus sont appliqués dès la génération.

### Test 4 — Échantillon de chaque famille

Obtenir et inspecter au moins un personnage de chacune des familles suivantes :

- enfance Jaffa ;
- âge adulte Jaffa Goa'uld ;
- âge adulte Jaffa libre ;
- enfance humaine hors-monde ;
- hôte Goa'uld ordinaire ;
- Grand Maître Goa'uld ;
- agent Tok'ra ;
- adulte Tau'ri / SGC.

Pour chaque personnage, vérifier :

1. la cohérence du texte français ;
2. l'absence de texte tronqué ou de clé XML visible ;
3. la présence de bonus de compétences modestes et thématiques ;
4. l'absence de nouvelle incapacité de travail, passion forcée ou trait imposé.

### Test 5 — Génération naturelle

1. Déclencher un raid Goa'uld et inspecter plusieurs Jaffa.
2. Visiter ou générer une colonie Goa'uld et inspecter les gardes, hôtes ordinaires et le Grand Maître lorsqu'ils sont disponibles.
3. Visiter ou générer une colonie Jaffa libre.
4. Déclencher des visiteurs ou contacts Tok'ra générés naturellement.
5. Vérifier que les backstories correspondent toujours à la faction et au rôle du pawn.

Résultat attendu : la refonte des Defs n'altère pas les filtres de génération existants.

### Test 6 — Colon existant et implantation Tok'ra

1. Choisir un colon possédant des histoires vanilla ou GateRim SG-1.
2. Effectuer une implantation Tok'ra volontaire.
3. Vérifier que son enfance et son âge adulte d'origine restent inchangés.
4. Vérifier que le nom visible reste inchangé.

Résultat attendu : l'implantation n'attribue pas rétroactivement une carrière Tok'ra.

### Test 7 — Sauvegarde et rechargement

1. Sauvegarder une partie contenant plusieurs cultures et parcours.
2. Quitter complètement RimWorld.
3. Recharger la sauvegarde.
4. Comparer les histoires, descriptions, compétences et noms.

Résultat attendu : aucun parcours n'est reroulé et aucune compétence n'est ajoutée une seconde fois.

### Test 8 — Catalogue wiki des backstories

1. Ouvrir `docs/wiki/Cultural-Backstories.md`.
2. Vérifier la présence des huit tableaux culturels : Tau'ri / SGC, enfances Jaffa, Jaffa Goa'uld, Jaffa libres, humains hors-monde, hôtes Goa'uld, Grands Maîtres Goa'uld et Tok'ra.
3. Compter les lignes de backstories et confirmer que les `52` entrées sont présentes une seule fois.
4. Comparer plusieurs lignes de chaque tableau aux Defs et aux traductions françaises : nom, description et bonus de compétences doivent correspondre exactement.
5. Vérifier que la page précise que toute future backstory doit mettre à jour le tableau de sa culture dans le même jalon.

Résultat attendu : le wiki constitue un catalogue joueur complet, cohérent avec les Defs et durable pour les futures cultures.

### Test 9 — Frontière du jalon

1. Sur l'écran de sélection des pawns de départ, utiliser plusieurs fois la randomisation avec les configurations actuellement disponibles.
2. Vérifier que `0.3.8-dev` n'introduit pas encore de nouveau filtrage culturel propre aux starters.
3. Générer ensuite des pawns par raid, colonie, visiteur ou outil debug et confirmer que leurs filtres habituels restent inchangés.
4. Vérifier dans `docs/PROJECT_STATE.md` et `docs/ROADMAP.md` que le futur filtrage est décrit comme un jalon séparé, piloté par les profils culturels du framework global.

Résultat attendu : la refonte actuelle reste un changement de données compatible ; aucun comportement futur n'est activé par anticipation.

### Contrôle final

- Vérifier la version d'assembly `0.3.8.0`.
- Vérifier la version affichée `0.3.8-dev`.
- Rejouer un raid Goa'uld pour confirmer l'absence de régression générale.
- Contrôler `Player.log` et vérifier l'absence d'erreurs XML, `BackstoryDef`, `skillGains`, traduction, génération, Scribe ou ancienne DLL.
- Confirmer qu'aucune nouvelle backstory n'a été ajoutée : le total reste `52`.

# Testing workflow

## 0.3.7-dev - Présentation courte du mod

Cette vérification confirme que la nouvelle description et les métadonnées s'affichent correctement sans modifier le gameplay.

### Préconditions

- Extraire le correctif sur une branche créée depuis le tag `v0.3.6-dev`.
- Reconstruire complètement `GateRimSG1.dll`.

### Test 1 — Métadonnées XML

1. Ouvrir `About/About.xml` dans Cursor.
2. Vérifier que le document XML ne signale aucune erreur.
3. Vérifier la version `0.3.7-dev`, RimWorld `1.6`, la dépendance Biotech et l'URL du dépôt.
4. Vérifier que `About/ModIcon.png` est toujours présent et inchangé.

Résultat attendu : les métadonnées sont valides et aucune duplication de `About.xml` n'existe à la racine.

### Test 2 — Affichage dans le gestionnaire de mods

1. Lancer RimWorld.
2. Ouvrir le gestionnaire de mods.
3. Sélectionner GateRim SG-1.
4. Lire l'intégralité de la description et vérifier les paragraphes, apostrophes et retours à la ligne.
5. Vérifier que l'ancien inventaire commençant par `Current development scope` n'apparaît plus.

Résultat attendu : la présentation est courte, lisible, immersive et ne déborde pas en une liste technique.

### Test 3 — Version d'assembly et chargement

1. Reconstruire le projet avec `-t:Rebuild`.
2. Vérifier que `1.6/Assemblies/GateRimSG1.dll` porte la version `0.3.7.0`.
3. Charger le menu principal puis une partie existante compatible.
4. Contrôler `Player.log`.

Résultat attendu : le mod charge normalement, sans erreur XML, assembly, dépendance ou régression de sauvegarde.

### Contrôle final

- Confirmer qu'aucun Def ou fichier C# de gameplay n'est modifié.
- Confirmer qu'aucun fichier `docs/wiki/*.md` n'est modifié.
- Confirmer la version de mod `0.3.7-dev` et la version d'assembly `0.3.7.0`.
- Confirmer que `Player.log` est propre.

## 0.3.6-dev - Consolidation des PawnKinds Jaffa Goa'uld

Cette série vérifie que la réduction de duplication XML ne change ni les raids, ni les colonies Goa'uld, ni les noms culturels validés dans `0.3.5-dev`.

### Préconditions

- Extraire le correctif sur une base propre issue du tag `v0.3.5-dev`.
- Reconstruire complètement `GateRimSG1.dll` afin d'obtenir la version `0.3.6.0`.
- Utiliser une nouvelle partie ou une sauvegarde compatible avec la base `0.3.x-dev`.
- Activer le mode développeur RimWorld pour les générations ciblées.

### Test 1 — Chargement des Defs héritées

1. Lancer RimWorld avec GateRim SG-1 actif.
2. Ouvrir le journal développeur dès le menu principal.
3. Rechercher les erreurs relatives à `ParentName`, `Abstract`, `PawnKindDef`, `SG1_GoauldJaffaWarriorBase` ou `SG1_GoauldJaffaGuardBase`.

Résultat attendu : les deux parents abstraits sont chargés sans devenir des PawnKinds générables, et les quatre Defs concrètes restent disponibles.

### Test 2 — Génération ciblée des quatre PawnKinds

1. Mettre le jeu en pause.
2. Ouvrir `Debug actions menu` > `Spawn pawn`, puis générer successivement :
   - `SG1_GoauldJaffaWarrior`;
   - `SG1_GoauldJaffaGuard`;
   - `SG1_GoauldSettlementJaffaWarrior`;
   - `SG1_GoauldSettlementJaffaGuard`.
3. Vérifier immédiatement leur nom culturel, leur xenotype Jaffa, leur Prim'ta et leur équipement.
4. Reprendre le temps et vérifier qu'aucun nom ou équipement n'est remplacé une seconde fois.

Résultat attendu : les profils standards et `Settlement` restent générables et identiques à leur comportement validé dans `0.3.5-dev`.

### Test 3 — Différences intentionnelles entre profils

1. Vérifier en jeu que les guerriers standards et de colonie portent l'armure légère, les gantelets, les bottes renforcées, le casque déployé et un Ma'Tok.
2. Vérifier en jeu que les gardes standards et de colonie portent l'armure lourde, les gantelets, les bottes renforcées, le casque déployé et disposent des profils d'armes Ma'Tok/Zat.
3. Ouvrir `1.6/Defs/PawnKindDefs/SG1_GoauldAlignedJaffa.xml` dans Cursor.
4. Vérifier que `SG1_GoauldJaffaGuardBase` conserve `combatPower` `145`.
5. Vérifier que `SG1_GoauldSettlementJaffaGuard` remplace cette valeur par `130` et conserve `maxPerGroup` `2`.
6. Vérifier que `SG1_GoauldSettlementJaffaWarrior` conserve `maxPerGroup` `7`.

Résultat attendu : seuls les champs communs sont hérités; les différences contextuelles restent explicites dans le fichier et intactes en jeu.

### Test 4 — Raid Goa'uld

1. Ouvrir `Debug actions menu` > `Do incident (map)` > `controlled Goa'uld Jaffa test raid`, ou laisser survenir le raid naturel déjà validé.
2. Vérifier que le groupe apparaît normalement et utilise les profils standards de guerrier et de garde.
3. Contrôler les armes, armures, Prim'ta, marques et noms culturels.
4. Vérifier l'absence de régression dans le comportement du raid.

Résultat attendu : les groupes `Combat` restent inchangés et ne dépendent pas des limites `Settlement`.

### Test 5 — Colonie Goa'uld

1. Générer ou visiter une colonie Goa'uld.
2. Vérifier que les profils `SG1_GoauldSettlementJaffaWarrior` et `SG1_GoauldSettlementJaffaGuard` sont utilisés dans le contexte `Settlement`.
3. Vérifier que la colonie reste composée majoritairement de Jaffa avec une présence Goa'uld minoritaire.
4. Vérifier qu'aucune limite de groupe n'est perdue après l'héritage XML.

Résultat attendu : la composition validée des colonies est conservée sans modifier les raids directs.

### Test 6 — Sauvegarde, chargement et journal

1. Sauvegarder avec les quatre PawnKinds présents sur une carte.
2. Quitter complètement RimWorld puis recharger la sauvegarde.
3. Vérifier que les personnages, noms, équipements, Prim'ta et marques restent inchangés.
4. Contrôler `Player.log`.

Résultat attendu : aucune erreur XML, Def, faction, PawnKind, nom culturel, sauvegarde ou ancienne DLL.

### Contrôle final

- Confirmer la version d'assembly `0.3.6.0` et la version de mod `0.3.6-dev`.
- Confirmer que les quatre `defName` concrets n'ont pas changé.
- Confirmer qu'aucun nouveau contenu joueur, incident ou équilibrage n'a été introduit.
- Confirmer que `Player.log` est propre.

### Validation locale

Validation complète réussie : chargement XML, génération immédiate des quatre PawnKinds, différences contextuelles, raid Goa'uld, colonie Goa'uld, sauvegarde/rechargement et `Player.log`. Aucun écart ni régression constaté.

## 0.3.5-dev - Générateurs de noms culturels

Cette série vérifie l'attribution unique de noms cohérents aux nouveaux personnages GateRim SG-1, sans renommer les personnages déjà présents ou nommés par le joueur.

### Préconditions

- Utiliser d'abord une nouvelle partie créée avec `0.3.5-dev`.
- Préparer aussi une sauvegarde créée avec `0.3.4-dev` contenant plusieurs personnages déjà nommés.
- Activer le mode développeur RimWorld ou l'option avancée GateRim SG-1 uniquement pour ouvrir le rapport d'exemples.

### Test 1 — Rapport debug regroupé

1. En mode développeur, exécuter `Cultural names: show samples`.
2. Vérifier qu'un seul rapport contient des exemples pour les Jaffa Goa'uld, Jaffa libres, Goa'uld, Tok'ra et Tau'ri / SGC.
3. Désactiver le mode développeur, activer l'option avancée GateRim SG-1 et ouvrir le même rapport depuis les réglages du mod.
4. Désactiver les deux modes et vérifier qu'aucun contrôle de noms n'est visible en jeu normal.

Résultat attendu : un point d'entrée regroupé, sans multiplication des gizmos ni modification de personnages.

### Test 2 — Jaffa Goa'uld et Jaffa libres

1. Générer plusieurs guerriers et gardes Jaffa Goa'uld avec les outils développeur ou un raid contrôlé.
2. Vérifier que leurs noms utilisent le style Jaffa aligné aux domaines Goa'uld.
3. Générer plusieurs Jaffa libres via leur incident de visiteurs ou les outils développeur.
4. Vérifier un style culturel apparenté mais distinct.
5. Générer au moins vingt exemples de chaque groupe et relever les doublons immédiats éventuels.

Résultat attendu : noms cohérents, variés et conservés après sauvegarde/chargement.

### Test 3 — Goa'uld, Tok'ra et double identité

1. Générer un hôte Goa'uld et un hôte Tok'ra volontaire.
2. Laisser leurs initialisateurs de symbiote s'exécuter.
3. Vérifier que le nom visible devient culturellement cohérent.
4. Activer les informations debug et vérifier que les données persistantes contiennent un `hostName` et un `symbioteName` distincts.
5. Sauvegarder et recharger.
6. Vérifier que le nom visible et les deux champs persistants restent identiques.

Résultat attendu : la distinction hôte/symbiote est préparée sans changement répété du nom affiché.

### Test 4 — Tau'ri / SGC et protection des noms joueur

1. Démarrer le scénario Équipe SG isolée avec des noms choisis ou régénérés dans l'écran de préparation.
2. Vérifier que les personnages de départ conservent exactement ces noms.
3. Générer ensuite un nouveau personnage appartenant à la faction d'expédition du SGC.
4. Vérifier qu'il reçoit un prénom et un nom de famille Tau'ri.
5. Renommer manuellement un personnage déjà traité, puis laisser passer plusieurs scans.

Résultat attendu : les personnages de départ et les renommages ultérieurs ne sont pas écrasés par le gestionnaire.

### Test 5 — Compatibilité d'une sauvegarde antérieure

1. Charger la sauvegarde `0.3.4-dev` contenant plusieurs personnages déjà nommés.
2. Laisser passer au moins dix secondes de jeu.
3. Vérifier qu'aucun personnage existant n'est renommé.
4. Générer ensuite un nouveau visiteur ou assaillant GateRim SG-1.
5. Vérifier que seul ce nouveau personnage reçoit un nom culturel.

Résultat attendu : l'activation initiale enregistre l'existant sans le modifier, puis traite normalement les nouvelles générations.

### Test 6 — Incidents, monde et persistance

1. Tester au moins un groupe de visiteurs Tok'ra, un groupe de Jaffa libres et un raid Jaffa Goa'uld.
2. Vérifier les noms sur la carte et dans les inspections de personnages.
3. Vérifier les dirigeants des factions Goa'uld, Jaffa libres et Tok'ra lorsque disponibles.
4. Sauvegarder, quitter complètement RimWorld, recharger et comparer les noms.
5. Recruter, capturer, implanter ou extraire un personnage déjà traité et vérifier qu'il n'est pas renommé.

Résultat attendu : chaque personnage garde une identité stable indépendamment de son changement de statut.

### Contrôle final

- Vérifier la version d'assembly `0.3.5.0`.
- Revoir `Player.log` et vérifier l'absence d'erreurs `Name`, `Scribe`, `GameComponent`, `WorldPawns`, `DefOf` ou ancienne DLL.
- Confirmer qu'aucun autre gameplay, incident ou équilibrage n'a été modifié.


### Test ciblé r2 — Génération développeur immédiate et équipe SG

1. Mettre le jeu en pause.
2. Utiliser l'outil vanilla `Spawn pawn` sur chacun des PawnKinds `SG1_...` disponibles.
3. Vérifier immédiatement, sans reprendre le temps, que les Jaffa, Goa'uld, Tok'ra et symbiotes concernés reçoivent un nom culturel.
4. Pour les hôtes Goa'uld et Tok'ra, vérifier aussi que l'identité du symbiote est initialisée avant l'attribution du nom visible.
5. Reprendre le temps et vérifier que le gestionnaire ne remplace pas une seconde fois ces noms.
6. Générer ensuite un raid Goa'uld et confirmer que la voie naturelle déjà validée reste inchangée.
7. Ne pas utiliser un raid vanilla comme test pour les Tok'ra, les Jaffa libres ou l'expédition SGC : leurs Defs interdisent actuellement les raids ou correspondent à la faction du joueur. Tester plutôt leurs visiteurs, dirigeants ou PawnKinds directs.
8. Créer une nouvelle partie avec le scénario de l'équipe SG isolée.
9. Vérifier que les quatre candidats reçoivent des noms Tau'ri cohérents avant le lancement de la partie.
10. Renommer manuellement un candidat, lancer la partie puis vérifier que ce choix n'est jamais écrasé.

Résultat attendu : `Spawn pawn`, les générations naturelles et le scénario SG utilisent le même système culturel, sans délai visible ni renommage ultérieur des choix du joueur.

## 0.3.4-dev - Visuel et déroulement du site d'observation Tok'ra

Cette série vérifie le nouveau visuel ainsi que la suppression de l'attente automatique après déploiement.

### Préconditions

- Utiliser une sauvegarde compatible avec `0.3.0-dev` ou une version ultérieure.
- Disposer d'un communicateur sécurisé Tok'ra alimenté et d'un colon capable d'Intellectuel.
- Activer les outils debug uniquement pour forcer l'offre et inspecter l'état interne.

### Test 1 — Visibilité et placement

1. Vérifier que le point d'observation n'apparaît dans aucune catégorie Architecte.
2. Forcer puis accepter l'opération.
3. Vérifier que le point temporaire utilise le nouveau visuel de lunette sur trépied.

Résultat attendu : le site est lisible comme équipement d'observation et reste exclusivement généré par l'opération.

### Test 2 — Tâche continue

1. Faire un clic droit sur le dispositif livré avec un colon valide.
2. Vérifier qu'il transporte le dispositif jusqu'au site.
3. Vérifier que l'installation courte ne joue plus l'effet de construction métallique ni un bruit de perceuse.
4. Vérifier que le colon reste auprès de la lunette, lui fait face et observe pendant environ une à deux heures de jeu.
5. À la fin, vérifier qu'il replie immédiatement le dispositif.
6. Vérifier qu'il le rapporte directement au communicateur et transmet les données sans deuxième ordre du joueur.

Résultat attendu : le flux complet se déroule en une seule tâche cohérente.

### Test 3 — Interruption et reprise

1. Interrompre volontairement le colon pendant l'observation.
2. Sélectionner un colon valide et faire un clic droit sur le site installé.
3. Utiliser `Poursuivre l'observation et transmettre les données`.
4. Vérifier que le travail restant reprend, puis que le repli, le retour et la transmission s'enchaînent.

Résultat attendu : la progression persiste et aucun timer de fond ne termine l'observation sans opérateur.

### Test 4 — Sauvegarde et chargement

1. Sauvegarder pendant l'observation puis recharger.
2. Sauvegarder après le repli pendant le trajet vers le communicateur puis recharger.
3. Vérifier qu'aucun dispositif ni site supplémentaire n'est créé.
4. Vérifier que la réussite n'est appliquée qu'une fois après transmission.

### Test 5 — Échecs

1. Détruire le site pendant l'observation et vérifier un échec unique.
2. Tester séparément l'expiration de la fenêtre sécurisée avant transmission.
3. Vérifier qu'une interruption normale ne provoque pas l'échec tant que la fenêtre reste ouverte et que le site existe.

### Compatibilité

- Charger une sauvegarde `0.3.3-dev` avec un site déjà déployé et un ancien timer actif.
- Vérifier que l'état devient un travail d'observation restant à accomplir par un colon.
- Charger un site déjà prêt et vérifier qu'il peut être replié puis transmis normalement.

### Contrôle final

- Vérifier les deux actions d'observation du menu debug regroupé.
- Revoir `Player.log` et vérifier l'absence d'erreurs XML, texture, JobDriver, réservation, Scribe, ancienne DLL ou double résolution.


## 0.3.3-dev - Ralentissement de la récupération de l'agent Tok'ra blessé

Cette série conserve le flux validé de l'événement et vérifie uniquement le nouveau rythme de récupération après les premiers soins.

### Préconditions

- Utiliser une nouvelle partie ou une sauvegarde créée avec `0.3.0-dev` ou une version ultérieure.
- Disposer d'un communicateur sécurisé Tok'ra alimenté, d'un lit médical de la colonie et d'un colon capable de Médecine.
- Activer le mode développeur RimWorld ou l'option avancée GateRim SG-1 uniquement pour forcer l'offre et inspecter l'état technique.

### Test 1 — Choc aigu avant traitement

1. Forcer l'offre d'agent blessé et l'accepter normalement au communicateur.
2. Vérifier que le même agent arrive inconscient avec `choc du symbiote`.
3. Laisser avancer brièvement le temps avant tout soin.
4. Vérifier que l'agent ne se relève pas et que ses blessures, leur gravité et le saignement associé ne diminuent plus automatiquement sous l'effet de la régénération thérapeutique Tok'ra tant que le choc est actif.

Résultat attendu : le comportement précédemment validé du choc aigu reste inchangé.

### Test 2 — Passage à la récupération affaiblie

1. Secourir l'agent dans un lit médical appartenant à la colonie.
2. Faire soigner le choc lui-même, y compris si toutes les autres affections ont déjà disparu.
3. Vérifier que `choc du symbiote` disparaît.
4. Vérifier que `récupération affaiblie du symbiote` apparaît immédiatement.
5. Lire le message de premiers soins et vérifier qu'il demande de poursuivre les soins et le repos au lieu d'annoncer une régénération normale immédiate.

Résultat attendu : les premiers soins débloquent l'agent, puis la régénération thérapeutique Tok'ra ne reprend qu'à 25 % de sa vitesse normale ; les soins conventionnels restent visiblement utiles.

### Test 3 — Rythme de guérison et utilité des soins

1. Noter les blessures restantes juste après le premier traitement.
2. Maintenir l'agent au repos médical avec les soins vanilla disponibles.
3. Observer sa récupération sur plusieurs heures de jeu.
4. Vérifier qu'elle est nettement plus lente qu'en `0.3.2-dev`, sans devenir bloquée.
5. Confirmer que le contrôle des saignements, les soins ordinaires et le lit médical restent utiles.
6. Vérifier que les médicaments ne sont pas imposés artificiellement lorsque les réglages de soin permettent de traiter sans eux.

Résultat attendu : la guérison n'est plus presque immédiate après la levée du choc, mais l'agent peut encore devenir apte au voyage avant la fermeture de la fenêtre de cinq jours avec une prise en charge correcte.

### Test 4 — Sauvegarde et chargement

1. Sauvegarder avant le premier soin et recharger.
2. Soigner le choc, sauvegarder pendant la récupération affaiblie puis recharger.
3. Vérifier que le choc ne revient pas et que la récupération affaiblie reste présente.
4. Continuer jusqu'au message de départ, sauvegarder pendant le trajet puis recharger.

Résultat attendu : le même patient, ses blessures, sa phase de récupération et son ordre de départ persistent sans doublon.

### Test 5 — Résolution et nettoyage

1. Laisser l'agent quitter la carte vivant et vérifier que la réussite survient uniquement après la sortie effective.
2. Sur une autre occurrence, tuer l'agent avant sa sortie, y compris pendant le trajet de départ.
3. Vérifier que la mort provoque l'échec et reste prioritaire sur toute réussite en attente.
4. Tester séparément une capture avec les outils développeur.
5. Vérifier qu'après chaque résolution les affections temporaires propres à l'opération ne restent pas sur un pawn conservé.

Résultat attendu : aucune modification des règles de réussite ou d'échec précédemment validées, et aucun Hediff temporaire orphelin.

### Test 6 — Outil debug commun

1. Forcer et accepter une nouvelle occurrence.
2. Utiliser l'action commune d'avancement de phase pour l'agent blessé.
3. Vérifier qu'elle retire le choc aigu et applique la récupération affaiblie, comme le flux médical normal.
4. Vérifier que les outils restent regroupés dans le gizmo debug unique et invisibles lorsque les deux modes debug sont désactivés.

### Contrôle final

- Rejouer rapidement les opérations d'observation, de renseignements et de remise médicale afin de confirmer l'absence de régression.
- Revoir `Player.log` et vérifier l'absence d'erreurs XML, Hediff, Scribe, référence de Def, double résolution ou ancienne DLL.


## 0.3.2-dev - Refonte de l'opération d'observation Tok'ra

Cette section remplace les anciens sous-tests d'observation abstraite présents plus bas dans le document. Les autres archétypes conservent leurs procédures actuelles.

### Préconditions

- Utiliser une sauvegarde créée avec `0.3.0-dev`, `0.3.1-dev` ou une nouvelle partie.
- Disposer d'un communicateur sécurisé Tok'ra alimenté.
- Disposer d'un colon capable de travail Intellectuel.
- Activer le mode développeur RimWorld ou l'option avancée GateRim SG-1 uniquement pour les contrôles techniques.

### Test 1 — Objets réservés à l'opération

1. Ouvrir toutes les catégories d'Architecte et rechercher le dispositif ainsi que le point d'observation Tok'ra.
2. Vérifier qu'aucun des deux ne peut être construit.
3. Forcer l'offre d'observation depuis le menu debug unique du communicateur.
4. Accepter normalement l'offre.
5. Vérifier qu'un seul dispositif apparaît selon l'ordre zone de livraison, communicateur, puis fallback valide.
6. Vérifier qu'un seul point temporaire apparaît en extérieur, près de la périphérie et sur une cellule accessible.

Résultat attendu : les deux objets sont exclusivement générés par l'opération et aucun doublon n'apparaît.

### Test 2 — Transport et déploiement

1. Sélectionner un colon incapable d'Intellectuel et cliquer droit sur le dispositif.
2. Vérifier la raison courte de blocage.
3. Sélectionner un colon capable d'Intellectuel et choisir l'action de déploiement.
4. Vérifier qu'il rejoint le dispositif, le prend, se rend physiquement au point indiqué puis effectue un travail d'installation.
5. Vérifier que le dispositif porté est consommé par l'installation et que le point devient la station d'observation active, sans objet lâché au sol.
6. Interrompre successivement avant le ramassage, pendant le transport et pendant le travail d'installation.
7. Reprendre l'action depuis le dispositif à chaque fois.
8. Sauvegarder pendant le transport, recharger et terminer l'installation.

Résultat attendu : la progression ne se résout pas instantanément, le dispositif est physiquement transporté puis installé comme station de terrain ; aucun objet libre n'est jeté au sol à la fin du travail.

### Test 3 — Enregistrement sur le terrain

1. Après le déploiement, consulter le communicateur en mode normal.
2. Vérifier qu'il indique seulement que l'observation est en cours, sans révéler les ticks ou états internes.
3. Laisser la durée d'enregistrement se terminer ou utiliser `Observation : terminer l'enregistrement` dans le menu debug.
4. Vérifier qu'un message annonce les données prêtes.
5. Vérifier qu'aucune réussite, aucun XP et aucune amélioration de confiance ne sont encore appliqués.
6. Sauvegarder avant puis après la fin de l'enregistrement et recharger les deux états.

Résultat attendu : le dispositif reste présent, l'état `données prêtes` persiste et la réussite attend toujours la transmission finale.

### Test 4 — Récupération et transmission

1. Lorsque les données sont prêtes, sélectionner un colon capable d'Intellectuel et faire un clic droit directement sur la station d'observation.
2. Choisir `Replier le dispositif et transmettre les données`.
3. Vérifier que le colon travaille sur place pour replier le capteur, récupère l'objet portable puis part immédiatement vers le communicateur sans détour préalable par la base.
4. Vérifier que la transmission ne commence qu'une fois le dispositif revenu au communicateur.
5. Interrompre avant le repli, pendant le trajet retour puis pendant la transmission.
6. Après un repli déjà effectué mais interrompu, reprendre depuis le communicateur avec le dispositif portable.
7. Sauvegarder pendant le trajet retour et pendant la transmission, recharger puis terminer.
8. Vérifier que le succès est appliqué une seule fois après la fin du travail.

Résultat attendu : le flux normal part du site d'observation, le repli, le retour et la transmission forment une tâche continue ; le dispositif disparaît après la transmission, le colon reçoit l'XP prévue, la confiance s'améliore qualitativement et le canal revient à son état RP générique.

### Test 5 — Variantes RP et répétition

1. Réaliser au moins trois occurrences réussies en réinitialisant proprement le framework entre elles si nécessaire.
2. Lire chaque résultat sans consulter le rapport debug.
3. Vérifier que plusieurs formulations cohérentes peuvent apparaître et que la même variante n'est pas répétée immédiatement lorsque d'autres variantes sont disponibles.

### Test 6 — Destruction, expiration et double résolution

1. Accepter une nouvelle observation et détruire le dispositif avant le déploiement.
2. Refaire le test en le détruisant pendant l'enregistrement.
3. Vérifier dans chaque cas un seul échec et le nettoyage du point temporaire.
4. Refaire l'opération et laisser dépasser la fenêtre après acceptation.
5. Vérifier un seul échec d'expiration.
6. Après chaque résolution, utiliser les actions de réussite et d'échec forcées et confirmer qu'aucune conséquence supplémentaire n'est appliquée.

### Test 7 — Affichage normal et debug

1. Désactiver simultanément le mode développeur et l'option avancée GateRim SG-1.
2. Consulter le communicateur à chaque phase : offre, attente de déploiement, enregistrement, données prêtes et transmission interrompue.
3. Vérifier qu'il ne montre que l'état utile au joueur et les éventuelles missions uniques durables.
4. Réactiver le debug et ouvrir `Afficher l'état du framework`.
5. Vérifier la présence des références d'objet, cellule cible, ticks, progression et variante de résultat.
6. Vérifier qu'un seul gizmo debug du communicateur regroupe les actions d'observation.

### Test 8 — Compatibilité `0.3.1-dev`

1. Charger une sauvegarde `0.3.1-dev` avec une offre d'observation encore proposée et l'accepter.
2. Vérifier qu'elle démarre directement le nouveau flux physique.
3. Charger séparément une sauvegarde `0.3.1-dev` avec une observation déjà acceptée sous l'ancien minuteur abstrait.
4. Laisser passer le contrôle du framework.
5. Vérifier qu'un dispositif et un point sont créés une seule fois et que l'opération peut continuer normalement.

### Contrôle final

- Rejouer rapidement les opérations de renseignements, agent blessé et remise médicale afin de confirmer l'absence de régression.
- Revoir `Player.log` et vérifier notamment l'absence de `Invalid count: -1`, d'erreurs XML, Scribe, réservation, transport, placement, destruction ou double résolution.


## 0.3.1-dev - Refonte de l'opération de renseignements Tok'ra

### Préconditions

- Utiliser une sauvegarde créée avec `0.3.0-dev` ou une nouvelle partie.
- Disposer d'un communicateur sécurisé Tok'ra alimenté.
- Disposer d'un colon capable de travail Intellectuel.
- Activer le mode développeur RimWorld ou l'option avancée GateRim SG-1 pour les contrôles techniques.
- Conserver deux médicaments et les autres préconditions des opérations sans les utiliser : cette série de tests doit rester isolée sur l'archétype de renseignements.

### Test 1 — Module réservé à l'opération

1. Ouvrir `Architecte`, notamment `Mobilier`, puis rechercher le module de renseignements Tok'ra.
2. Vérifier qu'il n'apparaît dans aucune catégorie et ne peut pas être construit.
3. Forcer l'offre de renseignements depuis le menu debug unique du communicateur.
4. Accepter normalement l'offre.
5. Vérifier que le module est généré selon l'ordre zone de livraison, communicateur, puis bord de carte accessible.
6. Sélectionner le module et vérifier qu'il ne propose aucune action directe d'analyse.

Résultat attendu : le module est uniquement un objectif généré par l'opération ; toute analyse passe par le communicateur.

### Test 2 — Informations affichées sur le communicateur

1. Avant l'offre, consulter normalement l'état du canal.
2. Vérifier qu'aucun catalogue d'opérations, délai caché, pondération ou historique technique n'est révélé.
3. Forcer puis accepter l'offre de renseignements.
4. Consulter de nouveau le rapport normal à chaque phase : offre, module livré, méthode choisie, opération résolue.
5. En mode normal, vérifier que le panneau d'inspection ne liste ni les demandes verrouillées, ni les délais internes, ni le catalogue des opérations.
6. Vérifier qu'il ne montre que l'opération organique réellement en cours et les éventuelles étapes durables des missions uniques.
7. Activer ensuite le mode debug et confirmer que le rapport technique complet reste accessible.
8. Ouvrir `Afficher l'état du framework` depuis le menu debug.
9. Vérifier que le rapport technique affiche la méthode, le travail restant, l'état de l'interférence, la patrouille programmée, le porteur du module et la variante de résultat.

Résultat attendu : séparation nette entre le rapport RP normal et le diagnostic complet réservé au debug.

### Test 3 — Analyse prudente

1. Forcer et accepter l'offre, puis sélectionner un colon capable d'Intellectuel.
2. Faire un clic droit sur le communicateur et choisir l'analyse du module.
3. Dans la fenêtre, choisir `Analyse prudente`.
4. Vérifier que le colon rejoint d'abord le module, le prend en charge puis le transporte physiquement jusqu'au communicateur.
5. Vérifier que l'analyse ne commence qu'une fois le module arrivé au communicateur.
6. Interrompre le colon avant le ramassage, pendant le transport puis pendant l'analyse, lui donner une autre tâche, puis reprendre depuis le communicateur.
7. Sauvegarder pendant le transport et pendant le travail, recharger puis reprendre.
8. Laisser l'analyse se terminer.
9. Vérifier la disparition du module, une seule réussite, l'amélioration qualitative de la confiance et `350 XP` en Intellectuel.
10. Vérifier qu'aucune patrouille Goa'uld n'est programmée par cette méthode.

### Test 4 — Décodage accéléré sans interférence

1. Recréer l'offre et choisir `Décodage accéléré`.
2. Vérifier que le travail est nettement plus court que l'analyse prudente.
3. Interrompre puis reprendre une fois pour confirmer la persistance de la progression.
4. Terminer une occurrence sans interférence.
5. Vérifier `500 XP` en Intellectuel, la réussite unique et l'absence de patrouille programmée.

### Test 5 — Interférence et patrouille Goa'uld

1. Forcer une nouvelle offre, l'accepter et choisir le décodage accéléré.
2. Utiliser `Renseignements : forcer la patrouille` dans le menu debug avant la résolution, ou répéter naturellement jusqu'à produire l'interférence.
3. Terminer l'analyse.
4. Vérifier que la lettre de réussite avertit en RP qu'une émission parasite a pu attirer une patrouille.
5. Vérifier que l'opération est immédiatement réussie, que le module disparaît et que la confiance positive n'est pas annulée.
6. Consulter le rapport debug et confirmer qu'une patrouille différée a été programmée.
7. Attendre environ deux à cinq heures de jeu.
8. Vérifier l'arrivée d'une petite force Goa'uld/Jaffa dimensionnée sous la menace normale de la colonie.
9. Sauvegarder après la réussite mais avant l'arrivée, recharger et vérifier que l'incident programmé arrive toujours.

Résultat attendu : la patrouille est une conséquence réelle du choix risqué, distincte du résultat positif de l'opération.

### Test 6 — Échecs et méthode verrouillée

1. Choisir une méthode, interrompre le travail puis tenter de sélectionner l'autre méthode.
2. Vérifier que la méthode reste verrouillée pour cette occurrence.
3. Détruire le module avant la fin et confirmer un seul échec.
4. Refaire l'opération et laisser expirer la fenêtre après acceptation.
5. Vérifier un seul échec, aucun XP, aucun module résiduel et aucune patrouille créée par une analyse inachevée.

### Test 7 — Variantes RP répétées

1. Résoudre plusieurs occurrences prudentes, accélérées sans interférence et accélérées avec interférence.
2. Noter les lettres de réussite.
3. Vérifier que chaque famille dispose de plusieurs formulations cohérentes avec le contexte.
4. Vérifier que deux résultats successifs évitent la même variante lorsque plusieurs variantes sont disponibles.
5. Vérifier qu'aucun texte joueur ne mentionne un jet, un pourcentage, un compteur interne ou une règle de framework.

### Test 8 — Debug regroupé et compatibilité `0.3.0-dev`

1. Vérifier qu'un seul gizmo `Debug des opérations Tok'ra` regroupe les actions sur le communicateur.
2. Vérifier les entrées de méthode prudente, méthode accélérée et interférence forcée.
3. Désactiver le mode développeur et l'option avancée, puis vérifier la disparition de tous ces contrôles.
4. Charger si possible une sauvegarde `0.3.0-dev` avec une offre ou un module de renseignements actif.
5. Vérifier que l'opération peut reprendre depuis le communicateur.
6. Pour une sauvegarde prise pendant l'ancienne tâche directe, vérifier que cette tâche s'arrête proprement avec un message invitant à utiliser le communicateur, sans erreur de Def ou de JobDriver.

### Contrôle final `0.3.1-dev`

- Rejouer une analyse prudente et un décodage accéléré sans debug.
- Vérifier qu'aucune table de recherche vanilla ne propose l'analyse.
- Vérifier qu'aucune action directe ne reste sur le module.
- Vérifier le retour du canal à son état RP générique après résolution.
- Vérifier `Player.log` : aucune Def manquante, erreur de Scribe, référence nulle, tâche invalide ou résolution double.


## 0.3.0-dev - Framework interne des opérations Tok'ra organiques

### Préconditions générales

- Utiliser une nouvelle partie créée avec `0.3.0-dev`.
- Ne pas charger de sauvegarde `0.2.x-dev` pour cette validation.
- Construire la DLL puis vérifier l'absence d'erreur rouge au démarrage.
- Disposer d'un communicateur Tok'ra alimenté et d'au moins un colon valide.
- Conserver `Player.log` pour le contrôle final.

### Test 1 — Visibilité normale et debug

1. Désactiver le mode développeur RimWorld.
2. Désactiver `Afficher les informations de debug avancées` dans les options GateRim SG-1.
3. Sélectionner le communicateur.
4. Vérifier qu'aucun menu ou gizmo technique des opérations organiques n'est visible.
5. Activer l'option avancée GateRim SG-1 sans activer le mode développeur.
6. Sélectionner de nouveau le communicateur.
7. Vérifier la présence d'un seul gizmo `Debug des opérations Tok'ra`.
8. Ouvrir ce menu et vérifier les actions d'état, acceptation, avancement,
   réussite, échec, expiration, conséquence secondaire et réinitialisation.
9. Désactiver l'option avancée, activer le mode développeur et confirmer que
   les actions compactes `Tok'ra ops: ...` sont disponibles dans les outils
   développeur.

Résultat attendu : le système de test est accessible par les deux voies prévues,
mais totalement absent du jeu normal.

### Test 2 — Offre et persistance communes

Pour chacun des quatre archétypes :

1. Forcer l'offre correspondante.
2. Ouvrir `Afficher l'état du framework` et noter l'archétype, l'état, la carte
   et le tick d'expiration.
3. Sauvegarder puis recharger.
4. Vérifier que la même offre reste active et qu'aucune seconde offre
   organique n'apparaît.
5. Utiliser `Accepter l'offre actuelle` ou l'interaction normale du
   communicateur.
6. Sauvegarder puis recharger avant la résolution.
7. Vérifier que l'instance active, ses références et ses délais restent
   cohérents.

Résultat attendu : une seule instance persistante porte l'opération active,
sans ancien champ de migration ni double création.

### Test 3 — Observation Goa'uld

1. Forcer puis accepter l'offre d'observation.
2. Utiliser `Avancer la phase actuelle`.
3. Vérifier que le rapport est immédiatement prêt à transmettre.
4. Sauvegarder et recharger dans cet état.
5. Transmettre normalement via le communicateur.
6. Vérifier une seule réussite, un seul gain de confiance qualitatif et un seul
   gain d'expérience.
7. Répéter avec `Résoudre en échec`.

### Test 4 — Module de renseignement

1. Forcer puis accepter l'offre de récupération.
2. Vérifier le placement selon l'ordre zone de livraison, communicateur,
   fallback accessible.
3. Sauvegarder et recharger avec le module présent.
4. Sécuriser normalement le module.
5. Vérifier une seule réussite et la disparition de l'objectif.
6. Refaire le test en détruisant le module ou avec `Faire expirer l'état
   actuel`.
7. Vérifier un seul échec et aucun objectif résiduel.

### Test 5 — Agent Tok'ra blessé

1. Forcer puis accepter l'offre.
2. Vérifier l'arrivée du patient, le choc de symbiote et le flux médical
   vanilla.
3. Sauvegarder et recharger pendant le transport vers un lit, après un soin et
   pendant le départ.
4. Vérifier que la réussite n'est appliquée qu'après la sortie réelle.
5. Refaire le test avec la mort du patient avant sa sortie.
6. Vérifier que la mort produit un seul échec.
7. Vérifier aussi l'action debug d'avancement, qui doit lever le blocage de
   soin et conduire à la phase suivante sans dupliquer la résolution.

### Test 6 — Remise de fournitures médicales

1. Forcer puis accepter l'offre.
2. Utiliser l'action d'avancement avant l'arrivée et vérifier que l'agent de
   liaison est généré et progresse vers le point de rencontre.
3. Sauvegarder et recharger avant puis après son arrivée.
4. Effectuer la remise normale de deux médicaments.
5. Vérifier la réussite immédiate et le départ du visiteur.
6. Sauvegarder et recharger pendant son départ.
7. Tuer le visiteur avant sa sortie.
8. Vérifier que la réussite n'est pas annulée et que la conséquence
   relationnelle secondaire n'est appliquée qu'une fois.
9. Refaire sans remise et faire expirer l'opération pour vérifier l'échec
   unique.

### Test 7 — Résolution et nettoyage partagés

1. Sur chaque archétype accepté, utiliser successivement les actions debug de
   réussite, échec ou expiration dans des sessions séparées.
2. Après chaque résolution, rouvrir l'état du framework.
3. Vérifier l'absence d'opération active et la programmation d'une future
   opportunité.
4. Réutiliser immédiatement l'action de résolution précédente.
5. Vérifier qu'aucun gain, perte, lettre ou compteur n'est appliqué une seconde
   fois.
6. Utiliser `Réinitialiser le framework` et confirmer le nettoyage de tout
   objectif ou visiteur encore rattaché à l'instance active.

### Test 8 — Contrôle de la rupture de sauvegarde

1. Vérifier qu'aucun des fichiers de compatibilité du conteneur médical n'est
   encore présent dans le dépôt.
2. Vérifier que le code ne contient plus
   `GameComponent_TokraOrganicOperationTracker`.
3. Vérifier que les nouvelles sauvegardes contiennent
   `tokraOrganicActiveOperation` et `tokraOrganicOperationFollowUp`.
4. Ne pas demander de prise en charge d'une sauvegarde `0.2.x-dev` : cette
   rupture est volontaire et documentée.

### Contrôle final

- Rejouer une occurrence normale de chaque archétype sans outil debug.
- Vérifier le retour immédiat à l'état RP générique du canal après résolution.
- Vérifier l'anti-répétition locale et le délai caché entre opportunités.
- Vérifier `Player.log` : aucune erreur de chargement de type, de Scribe, de
  référence nulle, de Def manquante ou de résolution double.


## Vérification du wiki joueur français

À exécuter après toute passe globale de traduction ou de réorganisation du
wiki :

1. Ouvrir `docs/wiki/Home.md`, `Content-Status.md`,
   `Tokra-Interaction-Roadmap.md`, `_Sidebar.md` et `_Footer.md`.
2. Vérifier que les versions, états et directions de développement correspondent
   à `docs/PROJECT_STATE.md`.
3. Vérifier que `Liens utiles` ne contient que des liens ou références et que
   les éléments de roadmap se trouvent dans une section de développement.
4. Rechercher les formulations anglaises restantes dans `docs/wiki/*.md`.
   Conserver uniquement les noms propres, identifiants techniques, commandes
   RimWorld et termes volontairement non traduits.
5. Cliquer chaque lien interne des pages modifiées et confirmer que la page
   cible existe.
6. Synchroniser `docs/wiki/*.md` vers le dépôt wiki séparé et vérifier le rendu
   de l'accueil, de la barre latérale, des tableaux et des listes.
7. Vérifier qu'aucune page française n'annonce une fonctionnalité prévue comme
   déjà jouable, ou inversement.


Durable tests follow the structure and ordering rules in `docs/TESTING_GUIDELINES.md`.

## Minimal isolated test

Use this active mod list first:

```text
Core
Biotech
GateRim SG-1
```

This isolates GateRim definitions from unrelated third-party gene categories and patches.

## Jaffa foundation checklist

1. Start RimWorld with the minimal isolated mod list.
2. Open the xenotype editor.
3. Confirm that the editor opens without exceptions.
4. Load the premade `Jaffa` xenotype.
5. Confirm that `Jaffa physiology` displays a texture.
6. Confirm that `Jaffa longevity` displays a `150%` lifespan factor.
7. Switch to French and verify the translated labels and descriptions.
8. Close the game and inspect `Player.log`.

## Interpreting the first external test log

The first external log contained two categories of issues:

### GateRim issues corrected in 0.1.5-dev
- Leading and trailing whitespace in the Jaffa xenotype description.
- French translation values formatted across multiple lines.
- Missing `UI/Icons/Genes/Gene_Robust` texture.

### Third-party compatibility issue to isolate separately
- `KeyNotFoundException` for a gene category named `Ability`.

The `Ability` category is not declared by the current GateRim definitions. Re-run the minimal isolated test before investigating loaded third-party mods.


## Free Goa'uld symbiote prototype

Use developer mode to spawn:

```text
SG1_GoauldSymbiote
```

Checklist:

1. Confirm the pawn appears with its temporary sprite.
2. Confirm movement and a weak bite attack.
3. Confirm that no natural biome spawn occurs.
4. Switch to French and verify the translated label and description.
5. Check `Player.log` for `SG1_GoauldSymbiote` errors.


## 0.1.9-dev XML regression check

After applying the free-symbiote XML correction:

1. Launch with `Core`, `Biotech`, and `GateRim SG-1`.
2. Confirm that `Player.log` no longer reports:
   ```text
   XML error: <wildness>1</wildness> doesn't correspond to any field in type RaceProperties.
   ```
3. Spawn `SG1_GoauldSymbiote` through developer mode.
4. Confirm that movement, the weak bite, and the temporary sprite still work.


## Recent Goa'uld implantation prototype

Add the following Hediff through developer mode to a humanoid pawn:

```text
SG1_GoauldRecentImplantation
```

Checklist:

1. Confirm the health tab displays `recent Goa'uld implantation`.
2. Confirm the remaining-time countdown appears.
3. Confirm the pawn receives additional pain.
4. Wait one in-game day and confirm the Hediff disappears.
5. Switch to French and verify the translated label, description and stage.
6. Check `Player.log` for `SG1_GoauldRecentImplantation` errors.


## Jaffa Prim'ta split

Use newly generated pawns after applying `0.1.13-dev`.

1. Generate a Jaffa pawn.
2. Confirm the germline gene list contains:
   ```text
   SG1_JaffaLineage
   SG1_JaffaPouchPotential
   SG1_JaffaSymbioteCompatibility
   SG1_JaffaPhysiology
   ```
3. Confirm `SG1_JaffaPrimta` is absent at birth or initial generation.
4. Add `SG1_JaffaPrimta` through developer mode.
5. Confirm immunity, healing, pain, damage and lifespan modifiers.
6. Remove the Hediff and confirm the modifiers disappear.
7. Check `Player.log` for `SG1_JaffaPrimta` errors.


## C# logging scaffold smoke test

1. Build the mod assembly with `build.ps1` or `build.sh`.
2. Confirm that `1.6/Assemblies/GateRimSG1.dll` exists locally.
3. Launch RimWorld with `Core`, `Biotech`, and `GateRim SG-1`.
4. Close the game after the main menu appears.
5. Inspect `Player.log`.
6. Confirm the presence of:
   ```text
   <color=#D9B44A>[GateRim SG-1]</color> Version 0.1.42.0 loaded.
   ```


## Colored logging prefix regression check

1. Build with:
   ```powershell
   .\build.cmd "D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
   ```
2. Launch RimWorld with `Core`, `Biotech`, and `GateRim SG-1`.
3. Inspect `Player.log`.
4. Confirm the bootstrap line contains:
   ```text
   <color=#D9B44A>[GateRim SG-1]</color>
   ```


## Persistent Goa'uld symbiote identity

1. Build the assembly with `build.cmd`.
2. Launch RimWorld with `Core`, `Biotech`, and `GateRim SG-1`.
3. Select a humanoid pawn.
4. Add the health state:
   ```text
   adult Goa'uld symbiote
   ```
5. Open the health-state description and copy the displayed symbiote ID.
6. Save the game.
7. Reload the save.
8. Confirm that the displayed symbiote ID is unchanged.
9. Inspect `Player.log`.
10. Confirm that `Attached` and `Loaded` messages use the same symbiote ID.
11. Remove the Hediff and confirm a `Detached` message appears.

Optional regression check:

1. Add `recent Goa'uld implantation`.
2. Confirm that it also receives a persistent symbiote ID.
3. Confirm that the temporary state still disappears after one in-game day.


## Forced Goa'uld implantation prototype

1. Build with `build.cmd`.
2. Spawn a free `Goa'uld symbiote`.
3. Place one adult humanoid pawn in an adjacent cell.
4. Select the symbiote and record its free-symbiote ID.
5. Click `Forced implantation`.
6. Confirm that the free symbiote disappears.
7. Confirm that the target receives `recent Goa'uld implantation`.
8. Confirm that the ID displayed on the Hediff matches the former free-symbiote ID.
9. Save and reload.
10. Confirm that the transferred ID remains unchanged.
11. Inspect `Player.log` for the transfer lifecycle.

Negative checks:

- no adjacent compatible humanoid;
- child under 13;
- animal target;
- pawn already implanted;
- pawn already carrying an adult Goa'uld symbiote state.


## Active Goa'uld host conversion

1. Build with `build.cmd`.
2. Implant an adjacent adult humanoid through the manual forced-implantation command.
3. Record the persistent symbiote ID.
4. Save and reload during `recent Goa'uld implantation`.
5. Let the one-day countdown expire.
6. Confirm that the recent state disappears.
7. Confirm that `adult Goa'uld symbiote` appears.
8. Confirm that the ID is unchanged.
9. Confirm active-host modifiers and preservation of the original germline xenotype.
10. Save and reload after conversion.
11. Confirm that the ID remains unchanged.
12. Inspect `Player.log` for preparation, conversion and safe-removal logs.


## Emergency Goa'uld extraction prototype

1. Build with `build.cmd`.
2. Implant an adjacent adult humanoid through `Forced implantation`.
3. Record the persistent symbiote ID.
4. Select the implanted host before the critical countdown expires.
5. Click `Emergency extraction`.
6. Confirm that recent implantation disappears.
7. Confirm that a free Goa'uld symbiote pawn appears nearby.
8. Confirm that the free pawn displays the same ID.
9. Re-implant the extracted pawn and confirm the ID remains unchanged.
10. Save and reload after extraction.
11. Confirm that the free pawn ID remains unchanged.
12. Inspect `Player.log` for reverse-transfer lifecycle logs.


## Emergency Goa'uld extraction surgery

1. Build with `build.cmd`.
2. Implant an adult humanoid through `Forced implantation`.
3. Open the health tab and schedule:
   ```text
   emergency Goa'uld extraction
   ```
4. Provide a doctor with Medicine `6+`, a bed and medicine.
5. Let the bill complete.
6. On success, confirm that recent implantation disappears.
7. Confirm that a nearby free symbiote displays the same ID.
8. Re-implant the extracted pawn and confirm the ID remains unchanged.
9. Save and reload after extraction.
10. Confirm persistence.
11. Test a lower-quality medical setup.
12. Confirm that a failed surgery leaves recent implantation in place.
13. Inspect `Player.log`.


## Autonomous free-symbiote hunt

1. Build with `build.cmd`.
2. Spawn a free Goa'uld symbiote several cells away from an adult humanoid.
3. Confirm `Autonomous hunt` is enabled.
4. Wait for the pursuit job to start.
5. Confirm movement toward the target.
6. Confirm automatic implantation on contact.
7. Confirm the persistent ID transfer.
8. Extract the parasite through surgery.
9. Confirm the free pawn returns with a non-zero cooldown.
10. Confirm it does not immediately re-implant the patient.
11. Wait for cooldown expiry and confirm pursuit resumes.
12. Toggle autonomous hunt off and on.
13. Confirm the toggle interrupts and restores the autonomous behavior.
14. Inspect `Player.log`.


## Ritual Goa'uld implantation prototype

1. Build with `build.cmd`.
2. Spawn a free Goa'uld symbiote.
3. Disable autonomous hunt.
4. Place one compatible humanoid within 12 cells but not adjacent.
5. Select the symbiote and record its persistent ID.
6. Click `Ritual implantation`.
7. Confirm that the nearest valid humanoid receives recent implantation.
8. Confirm that the free pawn disappears.
9. Confirm identity persistence.
10. Save and reload.
11. Confirm the same ID remains visible.
12. Repeat without a valid target in range and confirm rejection.


## Explicit ritual map target selection

1. Build with `build.cmd`.
2. Spawn a free Goa'uld symbiote.
3. Place two compatible humanoids within `12` cells.
4. Disable autonomous hunt for a controlled test.
5. Select the symbiote and click `Ritual implantation`.
6. Confirm that a map-targeting cursor appears.
7. Click the farther valid humanoid.
8. Confirm that the clicked pawn, not the nearest pawn, receives recent implantation.
9. Confirm persistent identity transfer.
10. Repeat with an invalid pawn, an out-of-range pawn and an unreachable pawn.
11. Confirm rejection without consuming the free symbiote.
12. Save and reload after a valid ritual.
13. Confirm identity persistence.


## Timed ritual ceremony and cancellation

1. Build with `build.cmd`.
2. Spawn a free Goa'uld symbiote and disable autonomous hunt.
3. Start `Ritual implantation`.
4. Select one compatible reachable humanoid within `12` cells.
5. Confirm that implantation is not immediate.
6. Confirm the inspection panel displays the target and remaining ticks.
7. Save and reload during the ceremony.
8. Confirm the countdown resumes.
9. Let the countdown reach zero.
10. Confirm recent implantation and persistent identity transfer.
11. Start another ritual, click `Cancel ritual`, and confirm no transfer occurs.
12. Start another ritual and move the target out of range.
13. Confirm automatic cancellation without consuming the free symbiote.


## Goa'uld ritual basin requirement

1. Build with `build.cmd`.
2. Build or spawn `Goa'uld ritual basin`.
3. Spawn a free symbiote and disable autonomous hunt.
4. Keep the symbiote and one compatible target within `6` cells of the basin.
5. Start ritual implantation and select the target.
6. Confirm the inspection panel displays the ritual basin.
7. Save and reload during the ceremony.
8. Confirm the basin reference and countdown persist.
9. Complete the ritual and confirm identity transfer.
10. Start another ritual, destroy the basin, and confirm automatic cancellation.
11. Start another ritual and move the target beyond `6` cells from the basin.
12. Confirm cancellation without consuming the free symbiote.
13. Try starting without a nearby basin and confirm rejection.


## Jaffa Prim'ta implantation procedure

1. Build with `build.cmd`.
2. Spawn a newly generated Jaffa.
3. Open the health-tab operation menu.
4. Confirm `implant Jaffa Prim'ta` is available.
5. Schedule the procedure.
6. Provide one medicine and a doctor with Medicine `4+`.
7. Let the operation complete.
8. Confirm `Prim'ta symbiote` appears.
9. Confirm the expected biological modifiers.
10. Save and reload.
11. Confirm persistence and the `Loaded Jaffa Prim'ta symbiote` log.
12. Confirm the implantation operation is hidden while Prim'ta is present.
13. Select a baseliner and confirm the operation is unavailable.
14. Remove the Hediff in developer mode and confirm the removal log.


## Physical Prim'ta larva resource

1. Build with `build.cmd`.
2. Spawn one `Prim'ta larva` through developer tools.
3. Confirm the physical item can be hauled and stored.
4. Spawn a compatible Jaffa.
5. Schedule `implant Jaffa Prim'ta`.
6. Confirm the operation requires one medicine and one larva.
7. Let the surgery complete.
8. Confirm the larva is consumed.
9. Confirm `Prim'ta symbiote` appears.
10. Save and reload.
11. Confirm persistence.
12. Try the same workflow without an available larva.
13. Confirm the bill waits for the missing ingredient.


## Prim'ta larva acquisition prototype

1. Build with `build.cmd`.
2. Construct or spawn `Prim'ta incubation basin`.
3. Open its Bills tab.
4. Add `incubate Prim'ta larva`.
5. Let a colonist complete the Intellectual work.
6. Confirm one physical `Prim'ta larva` appears.
7. Confirm hauling, storage and stacking still work.
8. Use the produced larva in `implant Jaffa Prim'ta`.
9. Confirm the surgery consumes it and adds `Prim'ta symbiote`.
10. Save and reload after production and after implantation.


## Prim'ta incubation work-giver fix

1. Restart RimWorld so XML Defs are reloaded.
2. Construct or spawn `Prim'ta incubation basin`.
3. Add `incubate Prim'ta larva`.
4. Confirm a pawn with Handling enabled and Animals `4+` can prioritize the basin manually.
5. Confirm the same pawn starts the bill automatically when Handling work is enabled.
6. Confirm a pawn below Animals `4` is rejected with a minimum-skill message.
7. Let the work complete and confirm one physical larva appears.


## Prim'ta incubation nutrient requirements

1. Build with `build.cmd`.
2. Construct or spawn `Prim'ta incubation basin`.
3. Add `incubate Prim'ta larva`.
4. Confirm a pawn with Handling enabled and Animals `4+` is eligible.
5. Leave the map without raw meat and confirm the bill waits.
6. Add fewer than `10` raw-meat units and confirm the bill still waits.
7. Add at least `10` raw-meat units.
8. Confirm the pawn hauls the meat and completes the bill.
9. Confirm `10` units of raw meat are consumed.
10. Confirm one physical `Prim'ta larva` appears.
11. Complete the existing Jaffa implantation workflow.


## Prim'ta larva preservation prototype

1. Build with `build.cmd`.
2. Produce or spawn one `Prim'ta larva`.
3. Select the item and confirm rotting/spoilage information appears.
4. Store one larva at room temperature.
5. Confirm rot progresses.
6. Store one larva in a cold room or freezer.
7. Confirm it is preserved better than the room-temperature larva.
8. Let a warm larva fully rot.
9. Confirm it is destroyed.
10. Implant a fresh larva into a compatible Jaffa and confirm the medical loop still works.


## Prim'ta larva biological storage category

1. Build with `build.cmd`.
2. Start RimWorld and inspect the log for XML errors.
3. Produce or spawn one `Prim'ta larva`.
4. Open a stockpile storage filter.
5. Confirm the larva appears under:
   ```text
   raw resources
       ↓
   Goa'uld biological products
   ```
6. Confirm it no longer appears under `manufactured`.
7. Confirm it is not presented as raw food or an animal food product.
8. Confirm hauling, stacking, rotting and Jaffa implantation still work.


## Prim'ta larva temperature tuning

1. Build with `build.cmd`.
2. Spawn or incubate several `Prim'ta larva` items.
3. Select one larva and confirm the thermal inspection lines appear.
4. Store larvae below `0 °C`, around `5 °C`, around `20 °C`, above `25 °C`
   and above `40 °C`.
5. Confirm the displayed effective rates are respectively approximately:
   ```text
   ×0
   ×0.5
   ×1
   ×2
   ×3
   ```
6. Confirm hot larvae deteriorate faster than room-temperature larvae.
7. Confirm frozen larvae stop deteriorating for this prototype.
8. Confirm storage category, hauling, stacking, incubation and implantation
   regressions remain valid.


## Jaffa Prim'ta implantation age eligibility

1. Build with `build.cmd`.
2. Spawn a compatible Jaffa below `10` biological years.
3. Confirm `implant Jaffa Prim'ta` is absent from the operations list.
4. Spawn a compatible Jaffa aged exactly `10` biological years.
5. Confirm the operation appears.
6. Complete the normal surgery with one medicine and one larva.
7. Confirm `Prim'ta symbiote` is attached.
8. Confirm the duplicate-operation guard still works.
9. Save and reload.
10. Confirm persistence.


## Jaffa puberty dependency prototype

1. Build with `build.cmd`.
2. Spawn a compatible Jaffa aged `11` without Prim'ta.
3. Wait at least one in-game hour and confirm no dependency appears.
4. Spawn a compatible Jaffa aged `12` without Prim'ta.
5. Wait up to one in-game hour.
6. Confirm `Prim'ta deficiency` appears.
7. Accelerate time and confirm progressive severity stages.
8. Confirm immunity and healing modifiers worsen.
9. Save and reload during progression.
10. Confirm severity persists.
11. Implant a physical larva with the existing medical procedure.
12. Confirm the dependency disappears immediately.


## Jaffa Prim'ta cultural thoughts

1. Build with `build.cmd`.
2. Spawn a compatible Jaffa aged `9` without Prim'ta.
3. Confirm `awaiting Prim'ta` is absent.
4. Spawn a compatible Jaffa aged `10` without Prim'ta.
5. Confirm `awaiting Prim'ta` appears with mood `-1`.
6. Implant a physical larva with the existing surgery.
7. Confirm `awaiting Prim'ta` disappears.
8. Confirm `received Prim'ta` appears with mood `+3`.
9. Remove the Prim'ta in developer mode.
10. Confirm `awaiting Prim'ta` returns.
11. Save and reload.
12. Reimplant a larva.
13. Confirm the `received Prim'ta` memory is not granted again.
14. Confirm the puberty dependency remains separate and still works from age `12`.


## Jaffa tretonin substitution prototype

1. Build with `build.cmd`.
2. Spawn a compatible Jaffa aged `12+` without Prim'ta.
3. Wait for `Prim'ta deficiency`.
4. Spawn one `tretonin dose`.
5. Schedule `administer tretonin` from the health tab.
6. Confirm the dose is consumed.
7. Confirm the dependency disappears immediately.
8. Confirm `tretonin substitution` appears with remaining time.
9. Save and reload.
10. Confirm remaining duration persistence.
11. Let one day expire.
12. Confirm substitution disappears.
13. Wait for the next hourly dependency scan.
14. Confirm dependency returns.
15. Confirm Jaffa with implanted Prim'ta cannot receive tretonin.
16. Confirm waiting-thought behavior remains separate.


## Tretonin acquisition prototype

1. Build with `build.cmd`.
2. Build or spawn the vanilla `DrugLab`.
3. Confirm `prepare tretonin doses` appears in its Bills tab.
4. Confirm the minimum Intellectual skill is `6`.
5. Test with no larva and confirm the bill waits.
6. Test with no medicine and confirm the bill waits.
7. Supply one `Prim'ta larva` and one medicine unit.
8. Complete the bill.
9. Confirm both inputs are consumed.
10. Confirm exactly five `tretonin dose` items appear.
11. Confirm storage and stacking.
12. Administer one dose to an eligible Jaffa.
13. Confirm the existing one-day substitution workflow remains valid.


## Formal Jaffa Prim'ta ceremony prototype

1. Build with `build.cmd`.
2. Construct or spawn one `Goa'uld ritual basin`.
3. Place one physical `Prim'ta larva` within `6` cells.
4. Place one compatible Jaffa aged `10+` within `6` cells.
5. Select the basin and start `Formal Prim'ta ceremony`.
6. Target the Jaffa.
7. Confirm the inspection panel shows target, reserved larva and progress.
8. Save and reload during the rite.
9. Confirm progress persists.
10. Let the rite complete.
11. Confirm one larva is consumed and `Prim'ta symbiote` appears.
12. Confirm dependency relief and cultural-memory behavior.
13. Test manual cancellation.
14. Test automatic cancellation after moving the larva away.
15. Confirm cancelled ceremonies do not consume the larva.
16. Confirm the medical implantation operation still works independently.


## Formal Jaffa Prim'ta ceremony ticker regression

1. Restart RimWorld after applying the XML fix.
2. Construct or reuse one `Goa'uld ritual basin`.
3. Place one eligible Jaffa and one larva within `6` cells.
4. Start `Formal Prim'ta ceremony`.
5. Select the basin.
6. Confirm the remaining duration decreases from `600 / 600`.
7. Let the ceremony complete and confirm one larva is consumed.
8. Confirm the Prim'ta Hediff is attached.


## Tok'ra foundation prototype

1. Build with `build.cmd`.
2. Spawn `Tok'ra symbiote` through developer tools.
3. Confirm the inspection panel displays origin `Tok'ra` and autonomous hunt `disabled`.
4. Confirm only `Voluntary Tok'ra implantation` is available.
5. Confirm forced implantation, Goa'uld ritual implantation and autonomous hunt are absent.
6. Place a player-controlled compatible adult within `12` cells.
7. Start voluntary implantation and target the colonist.
8. Confirm recent implantation uses the same persistent ID.
9. Save and reload.
10. Wait one day and confirm active Tok'ra symbiosis.
11. Repeat and extract during recent implantation.
12. Confirm the free pawn returns as `Tok'ra symbiote`.
13. Confirm origin remains `Tok'ra` and hunt remains disabled.
14. Spawn a normal `Goa'uld symbiote`.
15. Confirm previous Goa'uld forced, ritual and autonomous workflows remain available.


## Tok'ra FactionDef loading regression

1. Apply the `0.1.39-dev-r1` XML patch.
2. Restart RimWorld completely.
3. Open `Player.log`.
4. Confirm the following errors no longer appear:
   ```text
   hairTags doesn't correspond to any field in type FactionDef
   startingGoodwill doesn't correspond to any field in type FactionDef
   naturalColonyGoodwill doesn't correspond to any field in type FactionDef
   raidLootValueFromPointsCurve must be defined
   ```
5. Confirm:
   ```text
   [GateRim SG-1] Version 0.1.39.0 loaded.
   ```
6. Continue the Tok'ra voluntary-implantation regression tests.


## Tok'ra voluntary-host pawn prototype

1. Build with `build.cmd`.
2. Spawn `Tok'ra voluntary host` through developer tools.
3. Confirm the pawn is player-controlled.
4. Wait up to `60` ticks.
5. Confirm `adult Goa'uld-family symbiote` appears in the Health tab.
6. Confirm the persistent origin is `Tok'ra`.
7. Save and reload.
8. Confirm the same symbiote identity persists.
9. Spawn a second prototype host.
10. Confirm the second pawn receives a distinct identity.
11. Confirm free Tok'ra voluntary implantation still works.
12. Confirm normal Goa'uld workflows remain available.


## Tok'ra voluntary-host resistance-range regression

1. Apply the `0.1.40-dev-r1` XML patch.
2. Restart RimWorld completely.
3. Open `Player.log`.
4. Confirm the following error no longer appears:
   ```text
   Config error in SG1_TokraVoluntaryHost: initial resistance range is undefined for humanlike pawn kind.
   ```
5. Confirm:
   ```text
   [GateRim SG-1] Version 0.1.40.0 loaded.
   ```
6. Repeat the developer-spawn and save/reload regression tests.


## Tok'ra pawn-group foundation

1. Restart RimWorld completely.
2. Open `Player.log`.
3. Confirm no XML or Def-validation errors reference:
   ```text
   SG1_TokraSmallTeam
   SG1_TokraVisitorPrototype
   ```
4. Confirm:
   ```text
   [GateRim SG-1] Version 0.1.41.0 loaded.
   ```
5. Confirm the Tok'ra faction remains hidden and non-generated.
6. Spawn `Tok'ra voluntary host` and confirm one-time Tok'ra initialization.
7. Spawn `Tok'ra symbiote` and confirm voluntary implantation only.
8. Spawn `Goa'uld symbiote` and confirm previous hostile workflows.


## Tok'ra pawn-group nested-profile regression

1. Apply the `0.1.41-dev-r1` patch.
2. Restart RimWorld completely.
3. Open `Player.log`.
4. Confirm this error no longer appears:
   ```text
   Type PawnGroupMakerDef is not a Def type or could not be found
   ```
5. Confirm no new `SG1_Tokra`, `pawnGroupMakers` or
   `maxPawnCostPerTotalPointsCurve` error appears.
6. Confirm:
   ```text
   [GateRim SG-1] Version 0.1.41.0 loaded.
   ```
7. Repeat Tok'ra-host, free-Tok'ra and Goa'uld regression tests.


## Tok'ra peaceful visitor prototype

1. Build with `build.cmd`.
2. Restart RimWorld completely.
3. Open developer tools.
4. Run:
   ```text
   Do incident
       ↓
   Tok'ra peaceful visitors (test)
   ```
5. Confirm a neutral letter appears.
6. Confirm `1` to `3` Tok'ra hosts enter from the map edge.
7. Wait up to `60` ticks.
8. Confirm each visitor receives active Tok'ra symbiosis.
9. Confirm visitors are not player-controlled.
10. Confirm automatic departure after the visit.
11. Save and reload after the first visit.
12. Trigger the incident again.
13. Confirm the same hidden Tok'ra faction instance is reused.
14. Confirm no random storyteller visits, traders or settlements are enabled.
15. Repeat free-Tok'ra, Tok'ra-host and Goa'uld regression tests.


## Tok'ra peaceful-visitor faction-generator build regression

1. Apply the `0.1.42-dev-r1` patch.
2. Rebuild with `build.cmd`.
3. Confirm the compiler no longer reports:
   ```text
   CS1503: cannot convert from 'RimWorld.FactionDef' to 'RimWorld.FactionGeneratorParms'
   ```
4. Restart RimWorld completely.
5. Trigger:
   ```text
   Do incident
       ↓
   Tok'ra peaceful visitors (test)
   ```
6. Confirm the hidden Tok'ra faction is created and the peaceful visit starts.

## 0.2.24-dev - Tok'ra safehouse follow-up lead

Suggested validation:

1. Build with a forced C# rebuild.
2. Prepare a safehouse test and keep Tok'ra trust neutral.
3. Enter the safehouse and exchange with the contact: the dialogue should give Medicine XP, but no follow-up lead.
4. Raise Tok'ra trust to cooperative with the debug step action, prepare/create a fresh safehouse, then exchange: one follow-up safehouse lead should be stored if capacity remains.
5. Raise Tok'ra trust to trusted and repeat with a fresh contact: one follow-up lead should again be stored if capacity remains.
6. Fill the lead registry to the cap and repeat: the briefing should report that stored lead capacity is already full.
7. Confirm the contact remains non-trading, non-recruitable, non-hostile and once per generated contact.

## Tok'ra organic operation opportunities

The procedures below are arranged to minimize reloads. Keep developer mode enabled and use the exact developer-action labels shown in backticks. Player-facing trust changes are checked through qualitative RP messages and the channel report; exact trust deltas are internal balance values and are not expected to appear in the normal interface.

### Shared preparation — perform once

**Purpose:** create a reusable starting state for all current-version tests.

1. Load a player home map with:
   - one powered Tok'ra secure communicator;
   - one Tok'ra delivery drop zone;
   - one colon capable of Intellectual work;
   - one colon capable of Medical work;
   - at least one available medical bed and ordinary medical supplies;
   - at least two industrial medicines in one reachable stockpile stack;
   - developer mode enabled.
2. Select the Intellectual-capable colon and record the current Intellectual XP.
3. Open the Tok'ra channel report and note the current qualitative relationship state. Exact trust values are intentionally hidden from the normal player interface.
4. Run `Tok'ra ops: reset framework`.
5. Save the game as `GR_TokraOrganic_Base`.

**Expected result:** no organic Tok'ra operation, intelligence module, visiting medical liaison, legacy handoff container or wounded patient is active. This save is the common checkpoint for later reload and failure tests.

### Continuous session A — success paths and placement

These tests may be executed consecutively without reloading `GR_TokraOrganic_Base`.

#### A1 — Complete an observation operation

**Purpose:** verify the complete observation success path and its rewards.

1. Run `Tok'ra ops: force observation offer`.
2. Select the Intellectual-capable colon.
3. Right-click the powered communicator and choose `Accept Tok'ra observation request`.
4. Confirm that the communicator reports the observation as in progress.
5. Run `Tok'ra ops: advance current phase`.
6. Right-click the communicator again. Confirm that `Transmit Tok'ra observation report` is now present and enabled, then choose it.
7. Compare the transmitting colonist's Intellectual XP with the value recorded before step 1, then read the success message and the Tok'ra channel report.

**Expected result:**

- the player-facing message and channel report indicate that Tok'ra confidence has improved; the exact internal trust change is not required in the normal interface;
- immediately after the advance action, the communicator exposes `Transmit Tok'ra observation report`;
- the transmitting colonist gains exactly `250` Intellectual XP;
- the success letter appears once;
- no organic operation remains active;
- no physical objective exists for this archetype.

**End state:** continue directly to A2.

#### A2 — Verify that a resolved observation cannot resolve twice

**Purpose:** verify the shared resolution guard immediately after A1.

1. Record the current Intellectual XP and note the current qualitative Tok'ra relationship state in the channel report.
2. Run `Tok'ra ops: advance current phase`.
3. Run `Tok'ra ops: fail current operation`.
4. Right-click the communicator and verify that `Transmit Tok'ra observation report` is absent.

**Expected result:** both developer actions report that no suitable active operation exists. The qualitative trust state and XP remain unchanged, and no second success or failure letter appears.

**End state:** continue directly to A3.

#### A3 — Analyze an intelligence module cautiously at the delivery zone

**Purpose:** verify preferred placement and the complete cautious-analysis path.

1. Record the selected colon's Intellectual XP and note the current qualitative Tok'ra relationship state in the channel report.
2. Run `Tok'ra ops: force intelligence offer`.
3. Right-click the powered communicator and choose `Accept Tok'ra intelligence recovery`.
4. Confirm that exactly one sealed intelligence module appears on or immediately beside the Tok'ra delivery drop zone.
5. Confirm that the module has no direct completion action.
6. Select an Intellectual-capable colon, right-click the powered communicator and choose `Analyze Tok'ra intelligence module`.
7. Choose `Cautious analysis`.
8. Confirm that the colon walks to the module, carries it to the communicator, and only then begins the analysis.
9. Let the work complete.

**Expected result:**

- the player-facing message and channel report indicate that Tok'ra confidence has improved; the exact internal trust change is not required in the normal interface;
- the analyzing colon gains exactly `350` Intellectual XP;
- the module disappears after completion;
- one cautious success variant appears;
- no signal patrol is queued;
- no item, resource or material reward remains;
- no organic operation remains active.

**End state:** continue directly to A4.

#### A4 — Verify communicator placement and accelerated decoding

**Purpose:** verify the second placement route and the accelerated method without reloading the game.

1. Remove the Tok'ra delivery drop zone.
2. Keep the secure communicator powered.
3. Run `Tok'ra ops: force intelligence offer`.
4. Accept it through the communicator.
5. Confirm that exactly one intelligence module appears beside the powered communicator rather than at the map edge.
6. Start analysis from the communicator and choose `Accelerated decoding`.
7. Confirm that the colon retrieves and carries the module to the communicator before decoding begins.
8. Complete the work. When necessary, use the dedicated debug action to test the interference branch separately.

**Expected result:** the accelerated work is shorter, the operator gains exactly `500` Intellectual XP, the module is removed, and no stale or duplicate module remains. The border fallback is not used while a powered communicator exists. A detected-interference occurrence queues a delayed patrol without cancelling the success.

**End state:** recreate the delivery zone if desired, then continue to A5.

#### A5 — Shelter, stabilize and release a wounded Tok'ra agent

**Purpose:** verify that the patient cannot recover alone, requires real colony treatment, then resumes normal Tok'ra recovery and leaves once fit to travel.

1. Recreate the Tok'ra delivery zone if it was removed; its presence is irrelevant to this pawn-arrival operation.
2. Run `Tok'ra ops: force wounded agent offer`.
3. Read the offer and confirm that it asks for shelter and treatment, does not mention colony medicine stocks, and says that ignoring it has no consequence.
4. Select a colon, right-click the powered communicator and choose `Accept the wounded Tok'ra agent` in English or `Accueillir l'agent Tok'ra blessé` in French.
5. Confirm that exactly one injured Tok'ra agent appears at a reachable map edge, already downed, with the health condition `symbiote shock` or `choc du symbiote`.
6. Pause briefly without rescuing the patient. Confirm that the agent cannot stand or walk toward the colony. Slow vanilla or residual healing may still occur, but the shock must keep the patient downed and unable to complete the event without colony care.
7. Consult the channel report and confirm that it names only this current operation and indicates that the agent is awaiting rescue and emergency treatment.
8. Rescue the agent into a player-owned bed marked for medical use. While a colon is carrying the patient, confirm that the patient does not vanish and that no operation-failure letter appears.
9. Once the patient is placed in the bed and before a doctor tends the agent, confirm that the shock remains active.
10. If ordinary injuries or illnesses are still present, let them heal or remove them through developer tools until `symbiote shock` / `choc du symbiote` is the patient's only remaining medical condition.
11. Select a doctor, right-click the patient in the player medical bed and confirm that a normal tending action is still available for the shock itself. Complete that tending action.
12. Wait for the shared operation check, then confirm that a message reports the emergency treatment, the shock hediff disappears and normal Tok'ra regeneration can resume.
13. Continue ordinary medical care, feeding and rest. Do not use `Tok'ra ops: advance current phase`; that command is not intended to heal the patient.
14. Observe the health tab while recovery progresses. Complete healing is not required.
15. When the agent becomes conscious, mobile and medically stable, confirm that a message announces preparation for departure.
16. Let the agent walk off the map.

**Expected result:**

- the patient arrives downed and cannot travel or become fit to leave before player intervention;
- the temporary carried state used by vanilla rescue does not count as the patient disappearing from the map;
- rescue to a medical bed alone does not remove the shock;
- the shock itself remains directly tendable even when every ordinary injury or illness has already healed;
- the shock is removed only after that condition has been tended in a player medical bed;
- after that treatment, normal Tok'ra recovery resumes alongside vanilla medical care;
- the operation does not create a medicine container or consume an arbitrary fixed stack;
- the agent may leave with minor remaining injuries once fit to travel;
- success is not reported merely when the agent becomes stable; it is reported once after the living agent actually leaves the map;
- the player-facing result indicates improved Tok'ra confidence without revealing a raw value;
- the communicator immediately returns to its generic RP state;
- no stale observation or intelligence-module text remains.

**End state:** continue directly to A6.

#### A6 — Complete a medical-supply handoff with a visiting liaison

**Purpose:** verify that the new social-logistical archetype does not inspect stocks before acceptance, then consumes exactly two industrial medicines only when the liaison dialogue confirms the donation.

1. Load `GR_TokraOrganic_Base`.
2. Record the Social XP of one player colon capable of Social.
3. Temporarily forbid or move all industrial medicine so none is accessible to that colon.
4. Run `Tok'ra ops: force medical handoff offer`.
5. Read the offer and confirm that it asks for two industrial medicines, does not claim to know the colony's reserves, and says that ignoring it has no consequence.
6. Select any valid colon, right-click the powered communicator and choose `Accept Tok'ra medical resupply request` or `Accepter la demande de ravitaillement médical Tok'ra`.
7. Confirm that acceptance succeeds despite the unavailable medicine and that no container is created.
8. Advance normal game time. Confirm that one Tok'ra liaison enters from the map edge roughly one to two in-game hours later.
9. With a Tok'ra delivery zone present, confirm that the liaison walks toward it. Repeat from a fresh checkpoint without the zone and confirm fallback near the powered communicator. If neither target exists, confirm a reachable point near the colony centre is used.
10. Select a colon incapable of Social and right-click the liaison. Confirm that the interaction is disabled with a short reason.
11. Select the Social-capable colon, right-click the liaison and choose the short talk action.
12. Confirm that the paused dialogue contains exactly two choices: give two medicines or cancel.
13. Choose the donation while no medicine is accessible. Confirm that an error message appears, the medicine count remains unchanged and the operation remains active.
14. Reopen the dialogue, choose cancel and confirm that only the window closes.
15. Make exactly two industrial medicines accessible, including a test where the units are split between two stacks.
16. Reopen the dialogue and confirm the donation.
17. Compare the medicine count and Social XP, then inspect the result letter, liaison behavior and channel report.

**Expected result:**

- the offer and acceptance never inspect medicine stocks;
- the liaison arrives only after the delayed entry and uses the expected meeting-point priority;
- Social, not Medicine or Intellectual, controls the player interaction;
- cancel closes only the dialogue;
- insufficient stocks show a rejection without resolving the operation;
- exactly two accessible industrial medicine units are consumed on confirmation, including across multiple stacks;
- the negotiating colon gains exactly `350` Social XP;
- one qualitative Tok'ra trust improvement and one success letter are applied immediately;
- the communicator immediately returns to its generic RP state;
- the liaison begins leaving the map, but their physical exit is not required for success;
- no temporary handoff container appears.

#### A7 — Verify manual communicator actions remain independent

**Purpose:** detect regressions outside the organic-operation framework.

1. Open the communicator's right-click menu with a selected valid colon.
2. Inspect the existing manual Tok'ra requests and channel report.
3. Trigger one manual request whose ordinary conditions are currently satisfied, or inspect its disabled reason when conditions are not satisfied.

**Expected result:** existing Trusted-tier requirements, threat or patient conditions and request cooldowns remain unchanged. Organic-operation successes have not consumed manual-request cooldowns.

**End state:** continuous success-path testing is complete. Use `GR_TokraOrganic_Base` for the reload and failure sessions below.

### Checkpoint session B — current-version save and reload

Start each test from `GR_TokraOrganic_Base` unless a test explicitly creates another checkpoint.

#### B1 — Reload an offered observation

**Purpose:** verify persistence before an offer is accepted.

1. Load `GR_TokraOrganic_Base`.
2. Run `Tok'ra ops: force observation offer`.
3. Save as `GR_TokraOrganic_ObservationOffered`.
4. Reload `GR_TokraOrganic_ObservationOffered`.
5. Select the Intellectual-capable colon and right-click the powered communicator.

**Expected result:** `Accept Tok'ra observation request` is still available, the remaining offer time is coherent, and no duplicate offer or letter appears.

#### B2 — Reload an accepted observation and resolve it once

**Purpose:** verify accepted-state migration, readiness and single resolution.

1. From B1, accept the observation request.
2. Save as `GR_TokraOrganic_ObservationAccepted` before advancing it.
3. Reload that save.
4. Run `Tok'ra ops: advance current phase`.
5. Right-click the communicator and confirm that `Transmit Tok'ra observation report` is present and enabled.
6. Save as `GR_TokraOrganic_ObservationReady`.
7. Reload that save.
8. Right-click the communicator again and confirm that the same transmission action remains available.
9. Record Intellectual XP, note the qualitative Tok'ra relationship state, then transmit the report through the communicator.
10. Save the completed game as `GR_TokraOrganic_ObservationResolved` and reload it.

**Expected result:** the accepted and explicit ready states survive reloads; the transmission action is available both before and after reloading the ready checkpoint; completion reports a single qualitative trust improvement and grants exactly `250` Intellectual XP once; reloading the resolved save does not repeat the letter, trust gain or XP gain.

#### B3 — Reload an accepted intelligence analysis and resolve it once

**Purpose:** verify restoration of the physical objective, selected method, remaining work and deadline.

1. Load `GR_TokraOrganic_Base`.
2. Run `Tok'ra ops: force intelligence offer` and accept it through the communicator.
3. Confirm that one module exists, then save as `GR_TokraOrganic_ModuleAccepted`.
4. Reload that save and confirm that the same module remains active.
5. Start cautious analysis from the communicator and save once while the colon is carrying the module toward the communicator.
6. Reload, confirm that the same module remains associated with the operation, then interrupt the analysis after partial progress and save as `GR_TokraOrganic_ModuleAnalysis`.
7. Reload, resume from the communicator and complete the analysis.
8. Save as `GR_TokraOrganic_ModuleResolved`, then reload the resolved save.

**Expected result:** the framework restores the same module, method and remaining work after reload; completion reports one qualitative trust improvement and grants exactly `350` Intellectual XP once; the module is removed; the resolved save does not repeat the outcome.

#### B4 — Reload wounded-agent shock, care and departure states

**Purpose:** verify persistence of the patient reference, initial-treatment flag, health progress and departure state.

1. Load `GR_TokraOrganic_Base`.
2. Run `Tok'ra ops: force wounded agent offer`, accept through the communicator and save as `GR_TokraOrganic_PatientShock` before rescuing the downed patient.
3. Reload that save and confirm that the same named patient remains downed with symbiote shock, no duplicate pawn appears and regeneration is still suppressed.
4. Rescue the patient into a player medical bed. If necessary, let or force every ordinary injury and illness to heal so that only symbiote shock remains.
5. Confirm that a doctor can still tend the shock itself, complete that tending action and wait until the shock is removed. Then save as `GR_TokraOrganic_PatientCare` while the patient is still recovering.
6. Reload that save and confirm that the same named patient remains active, the shock does not return, no duplicate pawn appears and the communicator reports only that patient's care.
7. Continue treatment until the departure message appears, then save immediately as `GR_TokraOrganic_PatientDeparting` before the patient reaches the edge.
8. Reload the departing save and allow the patient to leave.
9. Save as `GR_TokraOrganic_PatientResolved` and reload once more.

**Expected result:** shock persists before first treatment, remains removed after the treatment checkpoint, the same patient and health state survive reloads, the departure order survives the second reload, success occurs once after map exit, and reloading the resolved save does not repeat trust feedback or letters.

#### B5 — Reload the medical-supply liaison flow and resolve it once

**Purpose:** verify restoration of delayed arrival, meeting state, dialogue cancellation, deadline, donation and post-success departure without duplicate effects.

1. Load `GR_TokraOrganic_Base`, run `Tok'ra ops: force medical handoff offer` and accept through the communicator.
2. Save immediately as `GR_TokraOrganic_MedicalSupplyBeforeArrival`, reload it and confirm that the liaison still arrives once after the remaining delay.
3. While the liaison is walking to the meeting point, save as `GR_TokraOrganic_MedicalSupplyApproaching` and reload it.
4. Confirm that the same liaison continues toward the same meeting point and that no duplicate pawn appears.
5. Once the liaison is ready, open the dialogue, choose cancel, save as `GR_TokraOrganic_MedicalSupplyWaiting` and reload it.
6. Confirm that the same liaison remains available, the deadline is coherent and the dialogue can be reopened.
7. Record the negotiator's Social XP and the exact industrial-medicine count, then donate two units.
8. Save immediately while the successful liaison is leaving as `GR_TokraOrganic_MedicalSupplyDeparting`, reload it and allow the pawn to exit.
9. Save as `GR_TokraOrganic_MedicalSupplyResolved` and reload once more.

**Expected result:** each checkpoint restores one liaison, one meeting point and one deadline; cancelling the dialogue never changes stocks or trust; exactly two medicine units and exactly `350` Social XP are applied once; success remains resolved while the liaison leaves; no duplicate letter, trust result, medicine consumption, XP gain or liaison appears after reload.

### Failure session C — destructive and expiry paths

Use copies of `GR_TokraOrganic_Base` so each failure starts from a known state.

#### C1 — Fail an accepted observation through the shared debug action

**Purpose:** verify one failure consequence and no duplicate application.

1. Load `GR_TokraOrganic_Base`.
2. Note the current qualitative Tok'ra relationship state in the channel report.
3. Run `Tok'ra ops: force observation offer`, select an Intellectual-capable colon, right-click the powered communicator and choose `Accept Tok'ra observation request`.
4. Run `Tok'ra ops: fail current operation`.
5. Save as `GR_TokraOrganic_ObservationFailed` and reload it.
6. Run `Tok'ra ops: fail current operation` again.

**Expected result:** one player-facing message reports a deterioration of Tok'ra confidence, the failure letter appears once, the operation is cleared, and the second failure attempt does not change trust.

#### C2 — Destroy an accepted intelligence module

**Purpose:** verify physical-objective loss and cleanup.

1. Load `GR_TokraOrganic_Base`.
2. Note the current qualitative Tok'ra relationship state in the channel report.
3. Run `Tok'ra ops: force intelligence offer`, select an Intellectual-capable colon, right-click the powered communicator and choose `Accept Tok'ra intelligence recovery`.
4. Destroy the spawned intelligence module through developer tools or damage.
5. Let the game advance until the tracker processes the missing objective.
6. Save and reload after the failure has been reported.

**Expected result:** one player-facing message reports a deterioration of Tok'ra confidence, one failure letter appears, the operation is cleared, and no stale module remains after reload.

#### C3 — Let an accepted operation expire

**Purpose:** verify deadline failure independently from manual destruction.

1. Load `GR_TokraOrganic_Base`.
2. Note the current qualitative Tok'ra relationship state in the channel report.
3. Force and accept one archetype using its exact developer action and communicator action listed in A1 or A3.
4. Do not complete the objective; advance game time beyond the displayed secure window.
5. After the failure appears, continue the game for several additional hours and then save/reload.

**Expected result:** one player-facing message reports a deterioration of Tok'ra confidence. No repeated failure, letter or additional trust loss occurs after more time or after reload.

#### C4 — Ignore an unsolicited offer

**Purpose:** verify that declining by inaction remains consequence-free.

1. Load `GR_TokraOrganic_Base`.
2. Note the current qualitative Tok'ra relationship state in the channel report.
3. Run any one of `Tok'ra ops: force observation offer`, `Tok'ra ops: force intelligence offer`, `Tok'ra ops: force wounded agent offer` or `Tok'ra ops: force medical handoff offer`.
4. Do not accept it and advance game time until the offer closes.

**Expected result:** the offer disappears, trust remains unchanged, no failure letter is issued and a future hidden opportunity can still be scheduled.

#### C5 — Let the wounded agent die

**Purpose:** verify death failure while preserving the corpse and preventing duplicate consequences.

1. Load `GR_TokraOrganic_Base`.
2. Force and accept `Tok'ra ops: force wounded agent offer`.
3. After the patient arrives, allow the injuries or illness to cause death, or use a developer health action to kill the patient without deleting the pawn.
4. Advance the game until the tracker processes the death, then save and reload.

**Expected result:** one RP failure reports the death and deterioration of Tok'ra confidence; the operation clears once; the corpse is not silently removed; reload does not apply a second failure.

#### C6 — Capture the wounded agent

**Purpose:** verify that taking the patient prisoner is treated as compromising the refuge.

1. Load `GR_TokraOrganic_Base`.
2. Force and accept the wounded-agent offer.
3. Arrest or otherwise turn the patient into a colony prisoner before departure.
4. Advance the game until the tracker processes the new status.

**Expected result:** one RP failure explains that the refuge was compromised, the operation clears, the captured pawn remains a prisoner, and no repeated penalty appears.

#### C7 — Keep the patient unfit until the care window closes

**Purpose:** verify the specific medical timeout rather than death or disappearance.

1. Load `GR_TokraOrganic_Base`.
2. Force and accept the wounded-agent offer.
3. Keep the patient alive but medically unfit to travel; for example, stabilize immediate bleeding while leaving a serious condition unresolved.
4. Advance beyond the remaining secure window shown by the communicator report.
5. Continue several more in-game hours, then save and reload.

**Expected result:** one timeout failure explains that a covert Tok'ra team recovered the living agent, the patient is removed by operation cleanup, and no second failure occurs later or after reload.

#### C8 — Lose the liaison before the handoff

**Purpose:** verify the accepted-operation failure paths tied to the visiting pawn.

1. Load `GR_TokraOrganic_Base`.
2. Note the current qualitative Tok'ra relationship state in the channel report.
3. Force and accept the medical-resupply offer, then wait for the liaison to arrive.
4. In separate copies of the checkpoint, test one of these conditions before donating medicine:
   - kill the liaison;
   - arrest the liaison;
   - remove or despawn the liaison through developer tools.
5. Let the tracker process the state, then save and reload.

**Expected result:** each scenario produces one appropriate RP failure, one qualitative deterioration after the accepted commitment, immediate return to the generic channel state and no repeated failure after reload. No medicine is consumed.

#### C9 — Let the liaison leave without receiving medicine

**Purpose:** verify that missing supplies before acceptance is allowed, but an accepted commitment fails when the liaison's waiting window expires.

1. Load `GR_TokraOrganic_Base`.
2. Forbid or remove all industrial medicine.
3. Force and accept the medical-resupply offer; confirm that acceptance still succeeds.
4. Wait for the liaison to arrive and reach the meeting point.
5. Do not complete the donation. Advance beyond the six-hour window shown by the channel report.
6. Confirm that the liaison begins leaving, then save and reload.

**Expected result:** the operation expires once with its specific accepted-failure text and qualitative trust deterioration; the liaison leaves; no medicine is consumed; the communicator returns to its generic state; no repeated letter or penalty appears after more time or reload.

#### C10 — Kill the liaison after a successful donation

**Purpose:** verify that a post-handoff death has a separate diplomatic consequence without invalidating completed success.

1. Load `GR_TokraOrganic_Base`.
2. Force and accept the medical-resupply offer, wait for the liaison and donate two medicines successfully.
3. Confirm the success letter, generic communicator state and departure order.
4. Before the liaison reaches the map edge, kill them through developer tools or an in-game threat.
5. Continue several tracker checks, then save and reload.

**Expected result:** the operation remains completed and never changes to failure; consumed medicine and Social XP are not restored; one separate negative RP letter and qualitative relationship penalty are applied for the liaison's death; neither the success nor the death consequence repeats after reload.

### Legacy-save migration session D

These tests require preserved saves created with the stated published version. They cannot be replaced by a current-version checkpoint.

#### D1 — Load `0.2.48-dev` observation states

1. Load a `0.2.48-dev` save with an observation offer active.
2. Confirm it can still be accepted or ignored normally.
3. Load a separate `0.2.48-dev` save with observation already accepted.
4. Confirm its remaining preparation or transmission window is coherent and that it can complete once.

**Expected result:** no red loading error occurs, no duplicate offer is created and the operation retains one qualitative trust improvement on success, `250` Intellectual XP, and one qualitative trust deterioration after an accepted failure.

#### D2 — Load `0.2.49-dev` intelligence-recovery states

1. Load a `0.2.49-dev` save with the offer active and accept it.
2. Confirm one module is placed through the normal preferred route.
3. Load a separate `0.2.49-dev` save with an accepted module already on the map.
4. Confirm the tracker recovers that module and its deadline.
5. Complete or fail the operation once.

**Expected result:** no red loading error occurs, no duplicate module is created, and the result applies only once.

#### D3 — Load a published `0.2.50-dev` save

1. Load a save created before the wounded-agent archetype existed.
2. Confirm that no patient or stale patient state is created during migration.
3. Force each of the four current archetypes in turn, resolving or resetting one before forcing the next.

**Expected result:** previous observation and intelligence states remain compatible, both newer archetypes become available normally, and only the currently active operation is displayed.

#### D4 — Load a published `0.2.51-dev` save and an optional `0.2.52-dev-r1/r2` development save

1. Load a clean `0.2.51-dev` save with no active organic operation.
2. Confirm that no liaison or stale handoff state is created during migration.
3. Force and accept the new medical-resupply offer, save before arrival, reload, then complete or reset it once.
4. Load a separate `0.2.51-dev` save with a wounded-agent operation in progress and confirm that its patient state still behaves normally.
5. When an unpublished `0.2.52-dev-r1/r2` save with the temporary container exists, load it and inspect the former handoff location.

**Expected result:** the framework initializes at save version `5` without red loading errors; the liaison archetype becomes available normally; existing wounded-agent treatment, departure and death-priority behavior remain intact; an old development container disappears automatically and the accepted handoff restarts as a delayed liaison visit without duplicate trust, XP or resource effects.

### Developer actions, presentation and final log review

1. Confirm the four force actions create the explicitly named archetype:
   - `Tok'ra ops: force observation offer`;
   - `Tok'ra ops: force intelligence offer`;
   - `Tok'ra ops: force wounded agent offer`;
   - `Tok'ra ops: force medical handoff offer`.
2. Confirm `Tok'ra ops: advance current phase` prepares an accepted observation report and does not invalidate an already placed intelligence module, wounded patient or visiting medical liaison.
3. Confirm `Tok'ra ops: reset framework` clears the active state, intelligence objective, living patient or active liaison without changing trust. A dead patient's or liaison's corpse should remain.
4. Review English and French player-facing letters, messages, dialogue and context actions for RP tone and understandable wording.
5. Confirm communicator, module and liaison interaction labels remain short.
6. Confirm the medical dialogue displays only the donation and cancel buttons, with no technical state or hidden timing details.
7. Confirm developer-action labels remain technical, explicit and readable without meaningful truncation. In particular, verify these compact legacy labels:
   - `Jaffa mark: black`, `Jaffa mark: silver`, `Jaffa mark: gold`, `Clear Jaffa mark`;
   - `Tok'ra safehouse: prepare`, `Tok'ra safehouse: create`, `Tok'ra safehouse: verify`;
   - `Tok'ra trust: +5`, `Tok'ra trust: -5`;
   - `Tok'ra cache: deliver`, `Tok'ra cache: reset`;
   - `Tok'ra lead: decode`, `Tok'ra site: reveal`, `Tok'ra site: recon`;
   - `Tok'ra relay: prepare`, `Tok'ra relay: complete`;
   - `Tok'ra threat: create`, `Tok'ra threat: clear`.
8. Close or pause the game and inspect `Player.log`.

**Expected result:** no red errors related to operation loading, Scribe references, pawn or lord persistence, meeting-point pathing, dialogue jobs, stock counting, medicine consumption, legacy-container cleanup, departure monitoring or duplicate resolution appear.


# 0.3.11-dev - Player-controlled Tok'ra personality switching

The concise active checklist is maintained in `docs/TESTING_CURRENT.md`. The milestone must validate:

- migration from a `0.3.10-dev` player-controlled Tok'ra with the host active by default;
- one gizmo only for directly controlled player Tok'ra;
- reversible host/symbiote name and backstory switching, preserving the host childhood whenever the symbiote has no dedicated childhood;
- exact restoration of `NameSingle` and `NameTriple` host names;
- backstory-derived skill differences applied once without cumulative stacking;
- shared XP and level progression earned under either personality;
- save/load with both host and symbiote active;
- automatic host restoration before extraction or Hediff removal;
- clean reimplantation into a different host without transferring the previous host's skill state;
- no gizmo or manual switching for AI-managed Tok'ra;
- gizmo hiding while the pawn is not directly controllable;
- unchanged Goa'uld implantation and extraction behavior;
- clean `Player.log`;
- first-click regression: never pass a missing Tok'ra childhood to `Pawn_StoryTracker.Childhood`; preserve the current host childhood instead.

Validation completed after `0.3.11-dev-r2`:

- first-click regression fixed and confirmed;
- ten repeated switches completed without stacking or skill drift;
- shared XP progression validated under both identities;
- save/load validated with host active and symbiote active;
- extraction from the symbiote-active state and reimplantation into a new host validated;
- no gizmo exposed to AI-managed Tok'ra or Goa'uld;
- Goa'uld flows unchanged;
- `Player.log` clean.


# 0.3.12-dev - Tok'ra active identity integration audit

The concise validated checklist is maintained in `docs/TESTING_CURRENT.md`. The milestone validated:

- one grouped Tok'ra identity gizmo on player caravans;
- switching from map to caravan and back without skill stacking, XP loss or identity reroll;
- multiple eligible Tok'ra listed separately in one caravan menu;
- exclusion of guests, prisoners, slaves, AI-managed Tok'ra and unrecruited quest pawns;
- coherent Bio, Social, Health and caravan presentation for the active identity;
- unchanged relations and persistent display of both identities in the health summary;
- save/load, extraction and the shared-skill model without regression;
- a clean `Player.log`.

Death, corpse, grave and resurrection remain a later optional compatibility audit.

An additional developer spawn test with `SG1_TokraVoluntaryHost` exposed a separate generation gap: a Tok'ra created already fused has no historical host identity to capture, so the stored host and symbiote names may be identical. This is deferred to a dedicated milestone using an explicit identity-source marker and configurable weighted host-origin profiles. Real implantation flows must remain unchanged.


# 0.3.16-dev - Cultural backstory variety expansion

The concise active checklist is maintained in `docs/TESTING_CURRENT.md`. The milestone must validate:

- twelve new native `BackstoryDef` entries and their French DefInjected text;
- moderate skill bonuses and correct childhood/adulthood slots;
- two additional SGC careers in ordinary-human additive randomization and the stranded SG-team scenario;
- two additional shared Jaffa childhoods;
- correct Goa'uld Jaffa and Free Jaffa name-group selection for four new Jaffa adult careers;
- correct Goa'uld and Tok'ra name-group selection for four new host adult careers;
- unchanged vanilla-majority behavior for ordinary human starters;
- unchanged normal world-pawn generation boundaries;
- save/load and Tok'ra active-identity skill-offset compatibility;
- complete wiki catalogue synchronization;
- clean `Player.log`.
