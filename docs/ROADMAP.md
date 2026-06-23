# GateRim SG-1 — Roadmap durable

## Dernier jalon clôturé — Mission d'introduction et objet-clé Tok'ra (`0.3.31-dev`)

- [x] Partir explicitement de `v0.3.30-dev` sur `feature/tokra-introduction-artifact-mission`.
- [x] Séparer cet arc unique des six opérations récurrentes dépendantes du communicateur.
- [x] Définir la mission comme unique uniquement après récupération réussie de l'objet-clé.
- [x] Conserver une nouvelle tentative possible après offre ignorée, refus, expiration ou échec.
- [x] Ajouter des délais cachés longs et variables : `4–12` jours au premier contact, `10–60` jours après une offre non engagée et `7–45` jours après une tentative acceptée échouée.
- [x] Ajouter un état persistant couvrant attente, offre, tentative active, nouveau délai et réussite définitive.
- [x] Ajouter une lettre à choix persistante avec acceptation et refus explicites.
- [x] Créer un site mondial hostile seulement après acceptation et conserver le voyage vanilla.
- [x] Capturer à l'offre un snapshot de menace et dimensionner une garde introductive volontairement modérée : facteur `0,35`, `180–650` points et `2–6` défenseurs.
- [x] Ajouter un module de chiffrement physique suivi individuellement et refuser les copies créées séparément comme objectif valide.
- [x] Conserver le combat, le butin et la reformation vanilla de la caravane.
- [x] Ajouter un avertissement persistant environ un jour avant l'expiration du site.
- [x] Gérer l'expiration réelle du site, la destruction réelle du module et la perte inattendue du site sans verrou permanent.
- [x] Préserver l'offre, le site, la carte, l'identité exacte du module, les délais, l'avertissement et le résultat après sauvegarde/rechargement.
- [x] Valider localement les fondations `r1`, le flux jouable `r2` et les échecs réels `r3`.
- [x] Revalider plusieurs chemins d'échec, les nouveaux délais cachés et la fermeture définitive après récupération réussie.
- [x] Effectuer la passe finale des textes joueur ; aucune réécriture fonctionnelle supplémentaire n'a été nécessaire.
- [x] Compléter la documentation durable et le wiki.
- [x] Clôturer le jalon, publier la branche et le tag final unique `v0.3.31-dev`, puis synchroniser le wiki séparé.

La révision locale finale `r3` valide l'offre naturelle, la lettre à choix, le site mondial, la garde adaptative modérée, le module exact, le combat, le butin, la reformation vanilla, l'avertissement final, l'expiration réelle, la destruction du module, les sauvegardes intermédiaires, les délais de retour et la réussite définitive. Le jalon est publié sous le tag final unique `v0.3.31-dev`.

Cette mission reste accessible avant le communicateur et indépendante du slot des opérations organiques. Une offre ignorée ou une tentative ratée ne ferme jamais l'arc : seule la récupération réelle du module Tok'ra active le verrou permanent.

Le profil de combat introductif reste volontairement borné à `2–6` défenseurs. Une comparaison prolongée sur davantage de niveaux de richesse et de storytellers reste un travail d'équilibrage, sans remettre en cause le flux fonctionnel validé.

## Dernier jalon clôturé — Livraison vers une base Tok'ra temporaire (`0.3.30-dev`)

- [x] Partir explicitement de `v0.3.29-dev` sur `feature/tokra-temporary-base-delivery`.
- [x] Sélectionner uniquement des contrats réellement fabricables selon recettes, recherches, poste de travail et compétences disponibles.
- [x] Créer un rendez-vous mondial temporaire et conserver le voyage libre par le flux de caravane vanilla.
- [x] Remettre explicitement la cargaison avec un gizmo de caravane sur la tuile du site.
- [x] Valider la quantité, la qualité et l'état réels des objets, puis consommer exactement la commande.
- [x] Ajouter une période de grâce après l'échéance, avec récompense de confiance réduite en cas de livraison tardive.
- [x] Ajouter une interception Goa'uld unique pendant le trajet, limitée à une cargaison complète allant vers le site exact.
- [x] Dimensionner les interceptions depuis le snapshot de menace capturé à l'offre et préserver ce budget après sauvegarde/rechargement.
- [x] Ajouter une embuscade mutuellement exclusive sur la dernière tuile d'approche plutôt que directement sur le rendez-vous.
- [x] Conserver l'inventaire réel, la récupération complète du butin et la reformation vanilla avant la dernière case.
- [x] Ne jamais remplacer la livraison par une victoire militaire ; exiger la quantité conforme survivante.
- [x] Corriger en `r9` le suivi différé de la carte d'embuscade, l'absence de sécurisation prématurée et l'erreur de journal associée.
- [x] Valider le déclenchement naturel d'une complication sans action développeur.
- [x] Revalider retard, expiration, persistance, récurrence, anti-répétition, textes RP et anciennes opérations.
- [x] Clôturer le jalon, publier la branche et le tag final unique `v0.3.30-dev`.
- [x] Mettre à jour les pages `docs/wiki/` décrivant le contrat, puis synchroniser le dépôt wiki séparé.

La révision locale finale `r9` valide la production accessible, la remise explicite depuis l'inventaire réel, la période de grâce, l'interception de trajet, l'embuscade sur l'approche finale, la perte possible de cargaison, la récupération de tout le butin vanilla, la reformation sans détour artificiel, la difficulté adaptative, la persistance et un `Player.log` propre. Une interception a également été obtenue naturellement sans forçage développeur. Le jalon est publié sous le tag final unique `v0.3.30-dev`.

La victoire militaire ne remplace jamais la livraison : la mission ne réussit que si la quantité conforme atteint réellement les Tok'ra. Les données de contrat, délais, récompenses et difficulté restent pilotées par Defs ; les flux de caravane et de carte temporaire restent dans des adaptateurs spécialisés.

## Jalon précédent clôturé — Appel de détresse Tok'ra (`0.3.29-dev`)

- [x] Partir explicitement de `v0.3.28-dev` sur `feature/tokra-distress-call-world-site`.
- [x] Ajouter un cinquième MissionDef organique Tok'ra et conserver le slot global unique.
- [x] Créer un site mondial temporaire ciblable par caravane avec situation cachée jusqu'à l'entrée.
- [x] Implémenter les variantes secours réel, signal compromis et arrivée trop tardive.
- [x] Permettre à un secours réel de se dégrader en arrivée tardive selon le temps de trajet.
- [x] Capturer la difficulté à l'offre et dimensionner les groupes Goa'uld/Jaffa depuis ce snapshot.
- [x] Conserver en Defs les poids, délais, textes, plages, récompenses et conséquences.
- [x] Ajouter les outils debug permettant de forcer chaque variante.
- [x] Remplacer l'entrée manuelle par l'action d'arrivée de caravane vanilla, avec chargement automatique, pause de carte hostile et colons enrôlés.
- [x] Générer une scène cohérente autour d'un ancrage unique : camp, caravane attaquée, position compromise ou camp submergé, avec débris et corps optionnels.
- [x] Placer survivants et défenseurs près de cette scène et faire entrer la caravane par le bord vanilla le plus proche.
- [x] Reconnaître les soins du choc au sol puis évacuer rapidement les survivants traités sans exiger lit, chauffage, guérison complète ou départ à pied.
- [x] Préparer les textes anglais/français et le protocole de test complet.
- [x] Valider le contrôle de cohérence et le rebuild forcé `0.3.29.0`.
- [x] Valider les trois variantes, la difficulté adaptative, l'expiration et la persistance.
- [x] Valider la récurrence, l'anti-répétition, les quatre anciennes opérations et `Player.log`.
- [x] Clôturer le jalon, publier le tag final unique `v0.3.29-dev` et synchroniser le wiki.

Le site mondial et les mécaniques narratives propres à la mission restent dans un adaptateur spécialisé. L'arrivée, la pause, l'entrée de carte, l'enrôlement et les soins utilisent les mécanismes vanilla lorsque ceux-ci couvrent le besoin. Le gestionnaire organique demeure l'unique propriétaire du slot, de la confiance, des résultats et des délais cachés. L'équilibrage des poids, effectifs, délais et récompenses reste évolutif après des tests prolongés.

La révision locale finale `r7` valide les trois situations cachées, l'arrivée de caravane vanilla, la scène ancrée, les soins de terrain, l'équipe Tok'ra visible, le portage et la sortie réels des blessés, la cache de composants déjà présente sur étagère, la difficulté adaptative, l'expiration, la persistance, la récurrence, les anciennes opérations et un `Player.log` propre. Le jalon est publié sous le tag final unique `v0.3.29-dev`.

## Sélection du jalon après `v0.3.30-dev` — terminée

- [x] Relire cette roadmap depuis le tag publié `v0.3.30-dev` avant d'ouvrir une nouvelle branche.
- [x] Choisir la fondation persistante de la mission d'introduction comme périmètre fonctionnel unique et testable.
- [x] Créer `feature/tokra-introduction-artifact-mission` depuis `v0.3.30-dev` et enregistrer `0.3.31-dev` dans `docs/PROJECT_STATE.md`.

L'arc d'introduction Tok'ra est sélectionné pour `0.3.31-dev`. Ce jalon couvre la mission et l'objet-clé ; l'étude, la recherche dédiée et le verrouillage du pool récurrent restent répartis dans des jalons ultérieurs.

Ce fichier est la référence centrale pour les ajouts, refontes et actions futures du projet.

Il doit être consulté avec `docs/PROJECT_STATE.md` au début de chaque nouvelle discussion, après une perte de contexte ou avant de choisir un nouveau jalon.

- `docs/PROJECT_STATE.md` décrit le jalon actuellement actif et ses tests.
- `docs/ROADMAP.md` conserve les travaux futurs qui ne doivent pas être oubliés.
- `docs/CHANGELOG.md` conserve l'historique des jalons terminés.
- `docs/MILESTONE_PUBLICATION.md` conserve la procédure de validation et de publication.

Lorsqu'une nouvelle idée durable est validée pendant une discussion, elle doit être ajoutée ici au plus tard dans le correctif documentaire du jalon en cours.

## Jalon précédent clôturé — Audit de l'orchestration Tok'ra (`0.3.28-dev`)

- [x] Partir explicitement de `v0.3.27-dev` sur `feature/tokra-operation-orchestration-audit`.
- [x] Reporter dans les fichiers autoritatifs la séquence verrouillée `0.3.28-dev` à `0.3.30-dev`.
- [x] Auditer le slot global, la sélection pondérée, les délais cachés, la persistance et l'anti-répétition.
- [x] Filtrer les MissionDefs temporairement indisponibles avant le tirage pondéré.
- [x] Conserver le même délai court de nouvelle vérification lorsque tous les candidats configurés sont temporairement indisponibles.
- [x] Ajouter un outil debug lançant le véritable tirage naturel sans forcer un archétype.
- [x] Ajouter un rapport d'audit déterministe sur `5000` tirages pour chaque palier de confiance.
- [x] Conserver inchangés les poids, délais, récompenses et conséquences des quatre missions actuelles.
- [x] Valider le contrôle de cohérence et le rebuild forcé `0.3.28.0`.
- [x] Valider réussite, échec, offre ignorée, récurrence, slot unique et sauvegarde/recharge.
- [x] Valider les quatre opérations existantes, les limites d'interface et `Player.log`.
- [x] Préparer le verrou documentaire final, publier le tag unique `v0.3.28-dev` et synchroniser le wiki.

Ce jalon ne cherche pas à figer l'équilibrage statistique définitif. Il sécurise l'orchestration avant les sites mondiaux et les caravanes, tout en conservant la possibilité de réajuster fréquences, poids, récompenses, variantes et mécaniques après des tests prolongés, y compris après `1.0.0`.

La révision locale `r1` valide le rapport d'audit déterministe, le tirage naturel réel, le slot unique, les délais cachés après réussite, échec et offre ignorée, la récurrence, l'anti-répétition locale, la persistance, les quatre opérations existantes, les limites d'interface, la compatibilité storyteller et un `Player.log` propre. Le jalon est publié sous le tag final unique `v0.3.28-dev`.

## Séquence Tok'ra verrouillée après `v0.3.27-dev`

### `0.3.28-dev - Audit Tok'ra operation orchestration and long-term recurrence`

Consolider le planificateur persistant des quatre opérations actuelles : slot actif unique, éligibilité avant tirage, récurrence après tous les résultats, délais cachés, anti-répétition, persistance et diagnostics de longue durée.

### `0.3.29-dev - Add Tok'ra distress call world-site mission`

Créer un site mondial temporaire avec délai d'échec et situation cachée jusqu'à l'arrivée :

- secours réel de survivants Tok'ra ;
- signal compromis ou piège Goa'uld/Jaffa ;
- arrivée trop tardive, sans survivants alliés, avec les derniers ennemis encore sur place ou en cours d'évacuation.

Prévoir des raisons RP crédibles, une difficulté adaptative, des résultats faisant évoluer la confiance, plusieurs variantes de textes et une récurrence sans répétition immédiate.

### `0.3.30-dev - Add Tok'ra temporary-base delivery mission` — clôturé

Le jalon livre une destination Tok'ra temporaire et une cargaison configurable limitée aux productions réellement accessibles. Il couvre la livraison normale, le retard, l'expiration, une interception de trajet et une embuscade sur l'approche finale avec perte possible de cargaison, butin vanilla et reformation complète.

Une victoire militaire ne suffit jamais si la cargaison est perdue. Les mécaniques de caravane, site mondial et combat restent dans des adaptateurs spécialisés ; les données de mission restent pilotées par Defs lorsque cela est raisonnable.


## Suite de l'arc d'introduction Tok'ra et accès au communicateur

La mission d'introduction et le module de chiffrement physique sont intégrés dans `0.3.31-dev`. Les étapes suivantes restent séparées afin de conserver des jalons testables :

- ajouter une étude ou analyse persistante du module récupéré ;
- ouvrir une recherche GateRim SG-1 dédiée, distincte des recherches vanilla, avec `Electricity` comme prérequis ;
- faire du module récupéré le prérequis durable de cette progression, selon une logique comparable aux objets d'étude du mécaniteur ;
- exiger ensuite la recherche terminée pour construire le communicateur Tok'ra ;
- rendre les opérations Tok'ra récurrentes inéligibles tant qu'aucun communicateur construit et alimenté n'est disponible ;
- préserver les opérations avec les storytellers vanilla ou moddés compatibles : seul leur accès doit dépendre du communicateur, pas d'un storyteller GateRim SG-1 ;
- revalider les sauvegardes où l'arc est terminé, en attente, échoué ou encore jamais proposé lorsque ces nouveaux prérequis seront ajoutés ;
- conserver la mission d'introduction unique et séparée des archétypes rejouables.

Le prochain jalon doit être choisi après publication de `v0.3.31-dev`. L'étude du module et la recherche dédiée constituent la prochaine étape logique ; la construction du communicateur et le verrouillage du pool récurrent peuvent rester un jalon distinct.

## Direction durable des pools de missions

- Enrichir progressivement le pool global afin de préserver la variété et le plaisir sur les longues parties.
- Privilégier plusieurs archétypes distincts et rejouables plutôt qu'un petit nombre de scénarios surchargés.
- Considérer les missions comme évolutives : fréquence, difficulté, récompenses, textes, variantes et mécaniques pourront être réajustés après des tests prolongés, y compris après `1.0.0`.
- Terminer et enrichir d'abord le pool Tok'ra.
- Prévoir ensuite des missions Goa'uld autonomes, sans lien obligatoire avec les Tok'ra.
- Étendre ensuite des pools distincts aux Jaffa libres, Asgard, Nox, Unas et futures factions.
- Garder un framework techniquement générique, mais séparer clairement identité RP, conditions d'apparition, récompenses et conséquences de chaque faction.
- N'ajouter au framework partagé que les capacités justifiées par plusieurs besoins réels ; conserver des adaptateurs C# pour les mécaniques propres aux sites mondiaux, caravanes, interceptions et combats.

## Dernier jalon clôturé — Migration de la remise médicale (`0.3.27-dev`)

- [x] Partir explicitement de `v0.3.26-dev` sur `feature/medical-handoff-mission-migration`.
- [x] Ajouter `SG1_TokraOrganic_MedicalSupplyHandoff` comme quatrième opération complète pilotée par MissionDef.
- [x] Ajouter un profil générique `handoff` limité à l'identité du visiteur, l'arrivée, la grâce de départ et la conséquence post-remise.
- [x] Réutiliser l'objectif générique `DeliverThing` pour la ressource, la quantité, le JobDef et la compétence.
- [x] Déplacer dans XML les poids, délais contextuels, textes, variantes, XP et conséquences de confiance.
- [x] Supprimer la définition C# complète de secours et valider explicitement les références requises.
- [x] Conserver en C# le spawn, le Lord, le déplacement, les réservations, le dialogue et la consommation réelle des piles RimWorld.
- [x] Valider le contrôle de cohérence et le rebuild forcé `0.3.27.0`.
- [x] Valider le flux normal, les ressources, les échecs, la pénalité post-remise et la persistance.
- [x] Valider les variantes RP, la récurrence, les trois autres opérations migrées et `Player.log`.
- [x] Préparer le verrou documentaire final, publier le tag unique `v0.3.27-dev` et synchroniser le wiki.

La révision locale `r1` valide le chargement des quatre MissionDefs, le flux complet de remise de deux médicaments, les restrictions d'interaction, les conséquences de confiance avant et après remise, les échecs, la persistance, les variantes RP, l'anti-répétition, la récurrence, les trois autres opérations migrées et un `Player.log` propre. Le jalon est publié sous le tag final unique `v0.3.27-dev`.

Les quatre opérations organiques Tok'ra actuelles sont désormais pilotées par MissionDef pour leurs données déclaratives. Le prochain jalon doit partir explicitement de `v0.3.27-dev` et être choisi après audit de la roadmap, sans ajouter d'abstraction théorique au framework.

## Dernier jalon clôturé — Migration des soins de l'agent blessé (`0.3.26-dev`)

- [x] Partir explicitement de `v0.3.25-dev` sur `feature/wounded-agent-mission-migration`.
- [x] Ajouter `SG1_TokraOrganic_WoundedAgentCare` comme troisième opération complète pilotée par MissionDef.
- [x] Ajouter un profil générique `pawnCare` limité aux données déclaratives réellement partagées.
- [x] Déplacer dans XML le PawnKind, les Hediffs de choc et de récupération et l'affection optionnelle.
- [x] Déplacer dans XML les seuils médicaux, la durée de stabilité et la grâce de départ.
- [x] Faire varier la chance et la sévérité de l'affection optionnelle à partir du snapshot de menace capturé à l'offre.
- [x] Déplacer dans XML les poids, délais contextuels, textes, variantes et conséquences de confiance.
- [x] Supprimer la définition C# complète de secours et valider explicitement toutes les références requises.
- [x] Conserver en C# la génération, les soins vanilla, le Lord, l'évaluation de santé et le départ.
- [x] Valider le contrôle de cohérence et le rebuild forcé `0.3.26.0`.
- [x] Valider le flux normal, la stabilité de `5000` ticks et la réussite uniquement après sortie de carte.
- [x] Valider la difficulté adaptative sur colonie faible et avancée.
- [x] Valider mort, capture, perte, expiration et échec de départ.
- [x] Valider sauvegarde/recharge, variantes RP, anti-répétition et récurrence.
- [x] Valider les trois autres opérations organiques et `Player.log`.
- [x] Corriger en `r2` la durée XML accélérée de `5000` ticks omise lors de l’assemblage de `r1`.
- [x] Revalider au chargement les trois MissionDefs et la récupération de renseignements accélérée après redémarrage complet.
- [x] Clôturer le jalon, publier le tag final unique `v0.3.26-dev` et synchroniser le wiki.

Le jalon ne généralise pas les soins RimWorld. Le bloc `pawnCare` contient uniquement des références et seuils configurables ; les interactions médicales concrètes restent dans un adaptateur spécialisé tant qu'un second cas réel ne justifie pas leur mutualisation.

La révision `r1` a validé le flux complet de l'agent blessé. Elle a également révélé l'omission accidentelle du champ XML `workTicks=5000` de l'analyse accélérée des renseignements ; le validateur a correctement désactivé cet archétype sans fallback caché. La révision `r2` restaure uniquement cette valeur et valide le chargement des trois MissionDefs, la régression des renseignements et un `Player.log` propre. Le jalon est publié sous le tag final unique `v0.3.26-dev`.

Prochaine migration logique : la remise médicale, dernière opération organique encore héritée. Son périmètre exact doit être confirmé au début du prochain jalon à partir de `v0.3.26-dev`.

## Dernier jalon clôturé — Migration de la récupération de renseignements (`0.3.25-dev`)

- [x] Partir explicitement de `v0.3.24-dev` sur `feature/intelligence-recovery-mission-migration`.
- [x] Ajouter un MissionDef complet pour la récupération de renseignements Tok'ra.
- [x] Déplacer dans XML le module, le job, la compétence, les durées prudente et accélérée et les récompenses d'XP.
- [x] Déplacer dans XML tous les textes du flux et trois banques nommées de résultats avec anti-répétition locale.
- [x] Ajouter des plages de récurrence configurables par palier de confiance et les faire consommer par le planificateur.
- [x] Déplacer dans XML la chance d'interférence, l'IncidentDef de patrouille, son délai et son délai de nouvelle tentative.
- [x] Dimensionner la patrouille à partir du snapshot de menace capturé à l'offre, avec facteur et bornes configurés.
- [x] Supprimer la définition C# complète de secours de cette opération.
- [x] Valider explicitement les Defs requis, le profil de difficulté et les quatre plages de récurrence contextuelles.
- [x] Étendre le rapport développeur aux banques nommées, délais contextuels et paramètres des conséquences.
- [x] Valider le contrôle de cohérence et le rebuild `0.3.25.0`.
- [x] Valider sur `r2` les durées rééquilibrées à `10000` ticks en méthode prudente et `5000` ticks en méthode accélérée.
- [x] Valider l'interférence forcée et l'utilisation du budget de menace capturé sur une colonie faible et une colonie avancée.
- [x] Valider les variantes RP, l'anti-répétition, les échecs, la récurrence et la sauvegarde/recharge, puis recontrôler la persistance des nouveaux totaux sur `r2`.
- [x] Valider les régressions des trois autres opérations organiques et `Player.log`.
- [x] Clôturer le jalon, publier le tag final unique `v0.3.25-dev` et synchroniser le wiki.

Le jalon doit prouver une nouvelle capacité avec un cas réel : la difficulté adaptative ne doit plus rester une simple donnée enregistrée. La patrouille de la méthode accélérée doit utiliser les points de menace capturés au moment de l'offre, sans recalcul opportuniste au moment de la conséquence.

La révision `r2` valide les durées finales de `10000` et `5000` ticks, leur persistance, les récompenses, les conséquences et l'ensemble du flux migré. La menace adaptative a été contrôlée sur une colonie faible et une colonie avancée : la patrouille consomme le snapshot mis à l'échelle capturé lors de l'offre, borné entre `180` et `700` points, sans recalcul lors du déclenchement. Le jalon est publié sous le tag final unique `v0.3.25-dev`.

## Dernier jalon clôturé — Migration Def complète de l'observation (`0.3.24-dev`)

- [x] Partir explicitement de `v0.3.23-dev` sur `feature/observation-mission-def-cleanup`.
- [x] Étendre le vocabulaire des objectifs avec cible secondaire, job, compétence, XP par tick et durée secondaire.
- [x] Déplacer dans le MissionDef les Defs du dispositif, du marqueur, du job de déploiement et du job de transmission.
- [x] Déplacer dans le MissionDef les durées de déploiement, observation, récupération et transmission.
- [x] Déplacer dans le MissionDef la compétence active, son gain d'expérience par tick et la récompense finale générique de compétence.
- [x] Déplacer dans le MissionDef toutes les actions et clés de textes spécifiques au flux d'observation.
- [x] Déplacer les variantes de lettre de réussite dans une banque pondérée avec anti-répétition.
- [x] Faire consommer au planificateur la plage de récurrence du MissionDef après une observation résolue.
- [x] Supprimer la définition C# complète de secours de l'observation.
- [x] Désactiver explicitement l'archétype et journaliser une erreur si sa définition requise est absente, incomplète ou référence un Def inconnu.
- [x] Étendre le rapport développeur pour exposer les valeurs réellement chargées.
- [x] Valider le contrôle de cohérence et le rebuild `0.3.24.0`.
- [x] Valider le flux complet avec les quatre durées `500 / 10000 / 500 / 1000`.
- [x] Valider compétence, XP, textes, variantes, échecs, récurrence et sauvegarde/recharge.
- [x] Valider les trois opérations organiques encore héritées et `Player.log`.
- [x] Clôturer le jalon, publier le tag final unique `v0.3.24-dev` et synchroniser le wiki.

Le code spécialisé reste responsable des mécaniques RimWorld de terrain, mais il ne doit plus dupliquer les données d'équilibrage, les références de Defs ou les textes qui définissent cette mission. Une généralisation supplémentaire des Toils, réservations ou règles de placement n'est justifiée que lorsqu'une seconde mission présente le même besoin.

La validation locale de la révision `r1` confirme le chargement de toutes les références configurées, le flux complet avec les quatre durées XML, les XP actifs et finaux, les variantes et échecs, la persistance, la récurrence configurée, les trois opérations héritées et un `Player.log` propre. Le jalon est publié sous le tag final unique `v0.3.24-dev`.

## Dernier jalon clôturé — Fondation du framework de missions (`0.3.23-dev`)

- [x] Repartir du tag publié `v0.3.22-dev` sur la branche dédiée `feature/mission-framework-foundation`.
- [x] Abandonner le jalon visuel isolé du dispositif d'observation sans commit ni tag.
- [x] Définir une boîte à outils couvrant environ 70 à 90 % des missions et questlines courantes, avec extensions C# pour les cas atypiques.
- [x] Ajouter des Defs XML pour les phases, objectifs, textes, récompenses, conséquences, récurrence et difficulté.
- [x] Ajouter un état générique persistant compatible avec les champs spécialisés déjà sauvegardés.
- [x] Ajouter des variantes de textes RP pondérées avec anti-répétition locale.
- [x] Ajouter une capture de difficulté fondée sur les points de menace RimWorld.
- [x] Rendre le facteur d'anti-répétition configurable par mission.
- [x] Ajouter un rapport développeur commun pour les définitions chargées.
- [x] Migrer l'observation Goa'uld Tok'ra comme premier pilote XML sans modifier son flux joueur.
- [x] Piloter sa durée de travail réelle depuis le Def et la porter à `10000` ticks, soit quatre heures en jeu.
- [x] Conserver les trois autres opérations organiques sur leur implémentation C# pendant cette première validation.
- [x] Valider le build, les Defs, les trois variantes RP et l'anti-répétition immédiate.
- [x] Valider le flux complet de l'observation, la sauvegarde/recharge et la migration d'une ancienne occurrence.
- [x] Valider la capture de menace sur des colonies de puissance différente.
- [x] Valider les trois opérations héritées et `Player.log`.
- [x] Clôturer le jalon, publier le tag final unique `v0.3.23-dev` et synchroniser le wiki.

La validation locale de la révision `r2` confirme le chargement du framework, les trois variantes RP, l'anti-répétition immédiate, le flux complet de l'observation, la durée de quatre heures, la persistance, les captures de menace différenciées, les trois opérations héritées et un `Player.log` propre. Le jalon est publié sous le tag final unique `v0.3.23-dev`.

La prochaine évolution doit migrer un second cas réel afin d'éprouver une nouvelle capacité commune, de préférence la consommation de difficulté adaptative lorsqu'elle correspond naturellement à la mission, sans ajouter d'abstraction purement théorique.

### Règles durables pour les missions et questlines

- Chaque archétype récurrent doit pouvoir revenir après réussite, échec ou offre ignorée.
- Les délais doivent rester cachés et variables, avec anti-répétition locale.
- Les menaces, effectifs, équipements et contraintes doivent partir de la difficulté active et des points de menace/valeur de colonie de RimWorld plutôt que de valeurs fixes.
- Les lettres, dialogues, messages et descriptions visibles doivent proposer des variantes solides lorsque la répétition serait perceptible.
- Un texte unique reste acceptable s'il est suffisamment contextuel et naturel pour être répété.
- Les détails techniques restent réservés aux rapports développeur, logs et documents internes.
- Le moteur générique doit évoluer à partir de besoins communs observés dans plusieurs missions, sans sur-généralisation prématurée.

## Jalon précédent

### Casquette de terrain SG (`0.3.22-dev`)

- [x] Partir du tag publié `v0.3.21-dev` sur la branche dédiée `feature/sg-team-field-cap`.
- [x] Ajouter une casquette noire légère avec textures au sol et dans les quatre directions.
- [x] Ajouter le Def anglais, les traductions françaises et la fabrication après la recherche d'équipement de terrain du SGC.
- [x] Corriger la description obsolète du casque qui évoquait encore quatre exemplaires fournis au sol.
- [x] Ajouter la casquette comme option de poids égal au casque dans le slot de couvre-chef existant.
- [x] Conserver la chance globale `0.6`, soit `30 %` casque, `30 %` casquette et `40 %` aucun couvre-chef.
- [x] Conserver le moteur C# et le scénario inchangés : l'intégration reste entièrement pilotée par XML.
- [x] Valider le contrôle de cohérence, le rebuild `0.3.22.0` et le chargement sans erreur.
- [x] Valider les trois résultats de couvre-chef et le rendu de la casquette dans les quatre directions.
- [x] Valider la persistance après sauvegarde/rechargement et l'absence d'impact sur un scénario vanilla.
- [x] Valider `Player.log`, clôturer le jalon, publier le tag final unique `v0.3.22-dev` et synchroniser le wiki.

La révision locale `r1` est la version fonctionnelle validée. Le slot de couvre-chef culturel reste générique et piloté par XML : les poids représentent des probabilités, non des ratios garantis sur un petit échantillon. La casquette constitue l'option légère et visuelle, tandis que le casque reste l'option protectrice.

### Jalon précédent — Règles culturelles d'équipement des starters (`0.3.21-dev`)

- [x] Partir du tag publié `v0.3.20-dev` sur la branche dédiée `feature/cultural-starter-loadouts`.
- [x] Auditer le composant spécifique `ScenPart_SGTeamStartingGear` et le consommateur culturel commun.
- [x] Étendre `CulturalStarterRule` avec des restrictions de candidat et des listes d'équipement XML.
- [x] Remplacer le composant spécifique par un marqueur de scénario culturel générique.
- [x] Corriger les chemins de commandes Markdown susceptibles de transformer `\t` en tabulation et étendre le contrôle automatique aux tabulations littérales.
- [x] Étendre le framework avec des emplacements d'habillement pondérés, une chance de sélection, un matériau optionnel et des groupes de variantes partagés.
- [x] Rendre le tee-shirt vanilla obligatoire pour chaque membre de l'équipe et corriger la référence vers `Apparel_BasicShirt` après l'erreur de chargement de `r3`.
- [x] Scinder le treillis SG en pantalon obligatoire et veste facultative.
- [x] Ajouter trois variantes visuelles pondérées pour les pantalons et les vestes : olive, noire et désert.
- [x] Relier pantalon et veste afin qu'une veste éventuelle reprenne toujours la variante du pantalon du même pawn.
- [x] Déplacer le gilet tactique sur la couche `Shell` afin de permettre le cumul tee-shirt, veste et gilet.
- [x] Rendre le casque de terrain aléatoire et directement équipé, tout en préparant le même emplacement pour une future casquette SG ou l'absence de couvre-chef.
- [x] Rééquilibrer l'assortiment d'armes humaines vanilla vers un fusil d'assaut, un pistolet-mitrailleur, un pistolet automatique et un fusil à pompe.
- [x] Valider le contrôle de cohérence, le test négatif des tabulations, le rebuild `0.3.21.0` et le chargement XML.
- [x] Corriger le contrôle final pour accepter les formulations `Version de DLL attendue` et `Version de DLL validée` sans double diagnostic en cas de valeur absente.
- [x] Valider la sélection des starters, les restrictions d'âge et de violence, les couches vestimentaires et la diversité pondérée.
- [x] Valider les trois couleurs, la cohérence pantalon/veste, les cas avec et sans veste et les cas avec et sans casque.
- [x] Valider le nouvel assortiment d'armes, les fournitures et l'absence des quatre casques laissés au sol.
- [x] Valider qu'un scénario vanilla ne reçoit ni restrictions ni équipement SG-team.
- [x] Valider sauvegarde/recharge et un `Player.log` propre.
- [x] Publier la branche, le tag final unique `v0.3.21-dev` et synchroniser le wiki séparé.

La révision locale `r4` est la version fonctionnelle validée. Le système reste générique : une future culture peut configurer ses propres emplacements obligatoires, facultatifs, pondérés et liés par variante principalement en XML. Les anciens treillis combinés restent définis pour les sauvegardes existantes, mais ne sont plus utilisés par le scénario. Les armes Tau'ri restent volontairement des armes humaines vanilla ; des patchs de compatibilité pourront éventuellement substituer des armes provenant d'autres mods sans imposer une gamme d'armes SGC propre à GateRim SG-1.

### Jalon précédent — Contrôle automatisé de cohérence du projet (`0.3.20-dev`)

- [x] Partir du tag publié `v0.3.19-dev` sur la branche dédiée `feature/project-consistency-checks`.
- [x] Reproduire la dérive publique : README et accueil du wiki restés en `0.3.17-dev` et à `70` backstories après la publication de `0.3.19-dev` et de `83` entrées.
- [x] Ajouter un contrôle PowerShell en lecture seule et un lanceur Windows dans `tools/`.
- [x] Corriger la compatibilité du parseur Windows PowerShell 5.1 après l’erreur détectée sur la révision locale `r1`.
- [x] Vérifier automatiquement les versions About, projet, README, wiki, état du projet, tests courants et changelog.
- [x] Dériver la version d'assembly attendue depuis la version `x.y.z-dev`.
- [x] Compter les `BackstoryDef`, détecter les `defName` manquants ou dupliqués et comparer le total aux résumés et au catalogue wiki.
- [x] Ajouter des attentes optionnelles de version et de nombre afin de tester un échec sans modifier les fichiers.
- [x] Corriger le README, l'accueil du wiki et l'état détaillé du contenu jusqu'à `0.3.20-dev` et `83` backstories.
- [x] Intégrer le contrôle à `docs/MILESTONE_PUBLICATION.md` avant le commit final.
- [x] Valider le chemin heureux sur le dépôt complet avec `0.3.20-dev`, `0.3.20.0` et `83` backstories.
- [x] Valider un échec volontaire puis un nouveau passage vert sans mutation du dépôt.
- [x] Valider le rebuild forcé, le chargement du menu principal, la version About et un `Player.log` propre.
- [x] Publier la branche, le tag final unique `v0.3.20-dev` et synchroniser le wiki séparé.

Ce jalon ne modifie ni gameplay, ni Def, ni traduction, ni texture. La révision locale `r2` a validé le chemin positif, l'échec volontaire, le nouveau passage vert, l'absence de mutation du dépôt, le rebuild `0.3.20.0`, le chargement du menu principal et un `Player.log` propre. Le contrôle documentaire manuel dispose désormais d'un garde-fou exécutable avant chaque publication. Les futurs jalons qui modifient la version ou le catalogue des backstories doivent maintenir les motifs publics contrôlés par l'outil, ou mettre à jour l'outil et sa documentation dans le même jalon.

### Jalon précédent — Audit de couverture des compétences culturelles (`0.3.19-dev`)

- [x] Partir du tag publié `v0.3.18-dev` sur la branche dédiée `feature/cultural-backstory-skill-coverage`.
- [x] Auditer les douze compétences RimWorld à partir des véritables combinaisons enfance/adulte de chaque profil.
- [x] Distinguer une absence réelle d'une simple faible redondance afin d'éviter de gonfler artificiellement le catalogue.
- [x] Confirmer que les profils Jaffa Goa'uld et Jaffa libres couvrent déjà toutes les compétences sans ajout.
- [x] Ajouter onze carrières ciblées pour les Tau'ri / SGC, humains hors-monde, hôtes Goa'uld, Grands Maîtres et Tok'ra.
- [x] Conserver le moteur culturel C# inchangé et intégrer les nouvelles listes uniquement par Defs et patchs XML.
- [x] Étendre les origines d'hôtes Tok'ra générés sans modifier leurs poids ni rerouler les identités existantes.
- [x] Porter le catalogue technique et wiki de `72` à `83` backstories.
- [x] Valider le rebuild `0.3.19.0`, le chargement XML et les traductions françaises.
- [x] Valider les pools SGC, Goa'uld, Grands Maîtres, Tok'ra et hôtes historiques générés.
- [x] Valider les noms culturels, la sauvegarde/recharge, le basculement Tok'ra et un `Player.log` propre.
- [x] Publier la branche, le tag final unique `v0.3.19-dev` et synchroniser le wiki séparé.

La matrice complète a été validée sur la révision locale `r1`. Les sept profils audités couvrent désormais les douze compétences, sans ajout artificiel pour les Jaffa dont la couverture était déjà complète. Les noms culturels, les pools de départ, les deux origines d'hôtes générés, la sauvegarde/recharge et le basculement Tok'ra restent stables. Aucun correctif C# ou Def supplémentaire n'a été nécessaire.

Le critère durable reste la présence d'au moins une voie culturellement crédible par compétence, et non l'égalité numérique entre compétences ou cultures. Les futures cultures Asgard, Nox, Unas et autres devront recevoir le même audit lorsque leurs pools réels existeront.

Le catalogue wiki contient désormais les `83` backstories et doit être synchronisé avec le dépôt wiki séparé lors de la publication finale.


### Jalon précédent — Origine Tau'ri minoritaire des hôtes Tok'ra générés (`0.3.18-dev`)

- [x] Partir du tag publié `v0.3.17-dev` sur la branche dédiée `feature/tokra-generated-host-tauri-origin`.
- [x] Ajouter une origine pondérée `SG1_GeneratedHost_TauriSGCVolunteer` sans modifier le moteur C#.
- [x] Conserver l'origine humaine hors-monde comme origine dominante avec un poids `1` contre `0.2` pour l'origine Tau'ri.
- [x] Réutiliser le générateur de noms Tau'ri et les huit carrières adultes SGC existantes.
- [x] Ajouter deux enfances Tau'ri modernes dédiées aux identités historiques générées.
- [x] Empêcher ces nouvelles enfances d'entrer dans les pools ordinaires des starters humains ou du scénario Équipe SG isolée.
- [x] Exposer les deux origines au profil Tok'ra par le patch XML existant.
- [x] Mettre à jour le framework culturel, le catalogue technique et le tableau wiki des backstories.
- [x] Valider le rebuild `0.3.18.0` et le chargement sans erreur de Def, patch ou traduction.
- [x] Observer les deux origines sur des `SG1_TokraVoluntaryHost` générés, avec une majorité hors-monde.
- [x] Valider les noms, enfances, carrières, basculements et compétences partagées pour les deux origines.
- [x] Valider la sauvegarde/recharge, la stabilité d'une ancienne identité `0.3.17-dev` et l'absence de reroll.
- [x] Valider l'extraction puis la réimplantation réelle sans origine générée résiduelle.
- [x] Valider l'isolation des starters, les régressions essentielles et un `Player.log` propre.
- [x] Publier la branche, le tag final unique `v0.3.18-dev` et synchroniser le wiki séparé.

La matrice complète a été validée sur la révision locale `r1`. Les deux origines sont générées, l'origine humaine hors-monde reste clairement majoritaire, les identités persistantes ne sont pas reroulées et une vraie réimplantation remplace correctement l'origine historique générée par l'hôte réel. Aucun correctif C# ou Def supplémentaire n'a été nécessaire.

Ce jalon constitue la première extension réelle des origines d'hôte pondérées prévues en `0.3.13-dev`. Les origines Jaffa et Unas restent différées jusqu'à ce que leur compatibilité biologique et leurs pools d'identité puissent être traités sans approximation.

Le catalogue wiki contient désormais les `72` backstories et doit être synchronisé avec le dépôt wiki séparé lors de la publication finale.


### Jalon précédent — harmonisation de la présentation du projet et du wiki (`0.3.17-dev`)

- [x] Partir du tag publié `v0.3.16-dev` sur la branche dédiée `feature/project-presentation-refresh`.
- [x] Remplacer le README historique centré sur `0.2.18-dev` par une présentation durable du projet.
- [x] Mettre à jour l'accueil du wiki jusqu'à `0.3.17-dev` et retirer les directions déjà terminées.
- [x] Actualiser `Content-Status.md` avec les systèmes culturels et Tok'ra validés depuis `0.3.5-dev`.
- [x] Remplacer la sidebar devenue linéaire par des catégories thématiques stables.
- [x] Réintégrer dans la navigation les pages existantes sur la double identité Tok'ra et l'évaluation tactique.
- [x] Ajouter à la procédure de publication un contrôle durable de la navigation lors de toute création ou renommage de page wiki.
- [x] Retirer de la section future les fonctions déjà implémentées ou les formulations devenues trompeuses.
- [x] Conserver une séparation explicite entre contenu jouable, développement en cours et grands chapitres futurs.
- [x] Valider le rebuild `0.3.17.0`, le chargement du menu principal et la version About.
- [x] Relire les liens et les formulations des trois pages de présentation.
- [x] Vérifier le rendu de la sidebar, ses catégories et l'ensemble de ses liens internes.
- [x] Publier la branche, le tag final unique `v0.3.17-dev` et synchroniser le wiki séparé.

La validation locale de `r2` confirme que la présentation publique est cohérente, que la sidebar catégorisée reste lisible et qu'aucune cible interne de l'ancienne navigation n'a été perdue ou dupliquée. Les pages sur la double identité Tok'ra et l'évaluation tactique sont désormais accessibles depuis la sidebar. Le rebuild, le chargement du menu principal, les métadonnées et `Player.log` sont validés sans modification de gameplay.

Ce jalon ne modifie aucun comportement de jeu. Il transforme les pages publiques et leur navigation en documents durables afin qu'elles ne redeviennent pas obsolètes à chaque micro-jalon. La description Workshop proprement dite restera à publier lors de la préparation de la première version publique ; `About/About.xml`, le README et l'accueil du wiki en constituent désormais la base éditoriale commune.

Le jalon suivant a démarré explicitement depuis `v0.3.17-dev`, conformément à cette procédure.

### Jalon précédent — extension mesurée des backstories culturelles (`0.3.16-dev`)

- [x] Partir du tag publié `v0.3.15-dev` sur la branche dédiée `feature/cultural-backstory-expansion`.
- [x] Limiter l'extension à douze backstories afin de préserver la lisibilité et la cohérence culturelle.
- [x] Ajouter deux carrières SGC, deux enfances Jaffa, deux carrières Jaffa Goa'uld, deux carrières Jaffa libres, deux carrières d'hôte Goa'uld et deux carrières Tok'ra.
- [x] Conserver des bonus de compétences modérés sans traits, passions, incapacités ni multiplicateurs directs.
- [x] Intégrer les nouvelles entrées aux profils de départ et aux règles de noms existants uniquement par XML.
- [x] Mettre à jour le catalogue wiki complet dans le même jalon.
- [x] Valider le rebuild `0.3.16.0` et le chargement XML sans erreur.
- [x] Valider les douze textes français, les bonus, les catégories et les profils culturels.
- [x] Valider la randomisation des starters Jaffa, hôtes Goa'uld, humains ordinaires et du scénario Équipe SG isolée.
- [x] Valider la génération normale du monde, la sauvegarde/recharge et un `Player.log` propre.
- [x] Publier la branche, le tag final unique `v0.3.16-dev` et synchroniser le wiki séparé.

Ce jalon ne crée aucune race, faction ou nouvelle branche C#. Il exploite le framework culturel existant et conserve les futures backstories Asgard, Nox et Unas pour leurs propres jalons de contenu.

La matrice complète a été validée sur la révision locale `r1` : les douze entrées sont chargées et traduites, les profils mixtes conservent les groupes de noms attendus, les humains ordinaires gardent une majorité de carrières vanilla, la génération normale et la sauvegarde restent stables, et `Player.log` est propre. Aucun correctif fonctionnel supplémentaire n'a été nécessaire.

Le catalogue wiki contient désormais les `70` backstories et a été synchronisé avec le dépôt wiki séparé lors de la publication finale.

### Jalon précédent — diagnostic unifié de l'identité culturelle (`0.3.15-dev`)

- [x] Partir du tag publié `v0.3.14-dev` sur la branche dédiée `feature/cultural-identity-diagnostics`.
- [x] Auditer les services existants avant d'ajouter un nouvel outil.
- [x] Ajouter un rapport central en lecture seule pour le pawn sélectionné.
- [x] Afficher les profils correspondants, le profil choisi et le groupe de noms dans les contextes `NonPlayer` et `PlayerStarter`.
- [x] Regrouper l'état Jaffa, les marques, l'identité sociale et les données persistantes du symbiote dans le même rapport.
- [x] Exposer un seul accès développeur et un seul bouton dans la section avancée existante.
- [x] Garder tous les diagnostics masqués lorsque le mode développeur et l'option avancée sont désactivés.
- [x] Garantir par conception et par test que l'outil ne renomme pas, ne reroll pas et ne modifie aucune identité.
- [x] Valider le rebuild `0.3.15.0` et le chargement sans erreur.
- [x] Valider les cas humain, Jaffa libre, Jaffa Goa'uld, hôte Goa'uld et Tok'ra pré-fusionné.
- [x] Valider l'accès par les options, la sauvegarde/recharge et un `Player.log` propre.
- [x] Publier la branche et le tag final unique `v0.3.15-dev` après validation locale.

Le rapport central a reproduit fidèlement les valeurs des services existants pour toute la matrice ciblée, y compris le basculement d'identité Tok'ra, sans mutation du pawn. Aucun correctif fonctionnel supplémentaire n'a été nécessaire après la révision locale `r1`.

Ce jalon clôt le point durable de cohérence entre noms, backstories, factions, marques Jaffa et identités sociales. Les futurs systèmes culturels pourront ajouter une section concise au rapport uniquement lorsqu'ils disposent d'un service autoritatif réel à auditer.


### Jalon précédent — mort, cadavre, tombe et résurrection des Tok'ra (`0.3.14-dev`)

- [x] Partir du tag publié `v0.3.13-dev` sur la branche dédiée `feature/tokra-death-resurrection-audit`.
- [x] Auditer statiquement la persistance existante avant d'ajouter du code.
- [x] Conserver un seul pawn, un seul objet `GoauldSymbioteData` et une seule progression de compétences partagée.
- [x] Ne créer aucun système parallèle propre au cadavre, à la tombe ou à la résurrection sans défaut reproductible.
- [x] Tester une mort avec l'hôte actif, puis cadavre, sauvegarde/recharge, tombe et résurrection.
- [x] Tester la même séquence avec le symbiote actif.
- [x] Vérifier que le cadavre et la tombe restent cohérents avec le nom actif au moment de la mort.
- [x] Vérifier qu'aucun gizmo de personnalité n'est exposé sur un pawn mort ou son cadavre.
- [x] Vérifier après résurrection les deux noms, les backstories, la personnalité active, les compétences et l'absence de cumul.
- [x] Tester un nouveau basculement, une sauvegarde/recharge et une extraction après résurrection.
- [x] Corriger uniquement les défauts reproduits, puis compléter les régressions Goa'uld et `Player.log`.
- [x] Publier la branche et le tag final unique `v0.3.14-dev`.

Le cycle complet a été validé sur la révision locale `r1` sans correctif C# : les données persistantes existantes couvrent correctement le cadavre, la tombe, la résurrection, le basculement post-résurrection et l'extraction ultérieure. Aucun fichier wiki n'a été modifié.

### Jalon précédent — identités distinctes des Tok'ra pré-fusionnés (`0.3.13-dev`)

- [x] Distinguer une implantation réelle d'une génération déjà fusionnée avec un marqueur persistant.
- [x] Générer une identité d'hôte humain hors-monde stable et distincte via des Defs XML pondérés.
- [x] Préserver séparément l'identité Tok'ra et maintenir l'hôte actif par défaut.
- [x] Valider `Spawn pawn`, variété, basculement, sauvegarde/recharge et migration d'une sauvegarde `0.3.12-dev`.
- [x] Valider les générations disponibles hors carte et l'absence de régression sur les implantations réelles, caravanes, extractions et Goa'uld.
- [x] Corriger le chargement XML des `skillGains` dans la révision locale `r2`.
- [x] Publier `feature/tokra-generated-host-identities`, le tag `v0.3.13-dev` et synchroniser le wiki.

### Extensions futures des origines d'hôte

- [x] Ajouter dans `0.3.18-dev` une première origine Tau'ri minoritaire entièrement pilotée par XML.
- [ ] Ajouter des profils pondérés Jaffa, Unas ou autres uniquement lorsque ces origines sont réellement disponibles et cohérentes en jeu.
- [ ] Garder le moteur C# générique : les nouvelles origines doivent être ajoutées principalement par XML.
- [ ] Vérifier les compatibilités biologiques, les générateurs de noms et les pools de backstories avant d'activer une nouvelle origine.
- [ ] Ne jamais appliquer une origine générée aux implantations réelles, qui doivent conserver l'identité existante du pawn.

### Intégration de l'identité Tok'ra active (`0.3.12-dev`)

- [x] Centraliser la règle de contrôle direct sur carte et en caravane.
- [x] Conserver l'exclusion des invités, prisonniers, esclaves, alliés, visiteurs et pawns de quête non recrutés.
- [x] Réutiliser un seul service de basculement et une seule progression commune.
- [x] Valider les onglets Bio, Social et Santé, les messages, caravanes et relations lorsque le symbiote est actif.
- [x] Reporter explicitement la mort, le cadavre, la tombe et la résurrection vers le jalon dédié `0.3.14-dev`.
- [x] Publier la branche `feature/tokra-active-identity-integration`, le tag `v0.3.12-dev` et synchroniser le wiki.

### Après validation

- [ ] Tester les interfaces de mods de préparation ou de gestion de pawns lorsqu'une incompatibilité concrète est signalée.
- [x] Reprendre l'extension générale du catalogue de backstories dans un jalon dédié, avec une première vague raisonnable et culturellement cohérente (`0.3.16-dev`).

## Présentation du mod et métadonnées

- [x] Réécrire la description de `About/About.xml` dans un style court, immersif et immédiatement compréhensible.
- [x] Retirer les inventaires de fonctionnalités, détails de jalons, prototypes internes et éléments de roadmap.
- [x] Structurer la présentation autour de l'ambiance, des factions, des possibilités de jeu et des dépendances indispensables.
- [ ] Publier et maintenir la description Workshop lors de la préparation de la première version publique, en réutilisant la base harmonisée par `0.3.17-dev`.

## Passe visuelle globale des objets

Cette passe est volontairement différée jusqu'à la stabilisation des mécaniques, races, factions et scénarios. Elle devra précéder le développement de la Porte des étoiles et de ses équipements, avec des concept arts validés par famille d'assets avant la production des textures finales.

Cette passe doit être réalisée lorsque les mécaniques concernées sont suffisamment stables pour arrêter des designs définitifs.

- [ ] Auditer les objets temporaires, dispositifs, modules, marqueurs et bâtiments qui utilisent encore des textures provisoires, génériques ou trompeuses.
- [ ] Remplacer le visuel du dispositif d'observation portable, actuellement proche d'un courrier et peu lisible sur la carte.
- [ ] Vérifier le module de renseignements Tok'ra et les autres objets d'opération afin que leur silhouette indique immédiatement leur fonction.
- [ ] Vérifier les marqueurs et sites temporaires afin d'éviter d'ajouter des gizmos de repérage lorsque le vrai problème est la lisibilité visuelle.
- [ ] Harmoniser les objets Tok'ra, Goa'uld, Jaffa et SGC selon une identité visuelle cohérente.
- [ ] Créer un visuel conceptuel propre pour chaque objet important une fois son design définitif validé.
- [ ] Ajouter les visuels définitifs au wiki et réutiliser les meilleurs pour la présentation Workshop.

## Opérations Tok'ra organiques

Le framework `0.3.0-dev` constitue la base persistante commune. Les opérations existantes doivent rester récurrentes, anti-répétitives et compatibles avec les sauvegardes créées à partir de cette base.

- [ ] Continuer à tester les quatre archétypes existants sur les parties longues : observation, renseignements, agent blessé et remise médicale.
- [ ] Ajouter ultérieurement de nouveaux archétypes réellement distincts, sans dupliquer les mêmes actions sous un autre texte.
- [ ] Conserver une seule opération organique visible à la fois sur le communicateur.
- [ ] Ne jamais révéler en jeu normal le catalogue des opérations, les pondérations, les délais cachés ou l'historique technique.
- [ ] Réserver les diagnostics complets au mode développeur RimWorld ou à l'option avancée GateRim SG-1.
- [ ] Conserver les textes RP récurrents sous forme de variantes afin d'éviter les répétitions évidentes.

## Interface et outils de debug

- [ ] Continuer à regrouper les actions de debug d'un même appareil dans un gizmo unique ouvrant un menu.
- [ ] Regrouper les actions par thème ou par phase avec des libellés courts et homogènes.
- [ ] Garder les détails techniques dans les rapports debug, les messages développeur ou les logs.
- [ ] Vérifier qu'aucun outil debug n'est visible lorsque le mode développeur et l'option avancée du mod sont désactivés.
- [ ] Effectuer avant stabilisation publique une passe globale sur les textes visibles : ton RP, formulations naturelles, boutons courts et absence d'informations techniques inutiles.

## Équipement Tau'ri / SGC

- [ ] Valider et publier dans `0.3.22-dev` la casquette SG dédiée ajoutée comme option pondérée au slot de couvre-chef existant : casque, casquette ou aucun.
- [ ] Ajouter de futures variantes de pantalons et de vestes comme contenu XML pondéré, en conservant la cohérence de variante entre les deux pièces.
- [ ] Conserver les armes humaines vanilla comme base du scénario ; envisager uniquement des patchs de compatibilité facultatifs pour substituer des armes équivalentes provenant d'autres mods.

## Framework culturel interne `0.3.x`

La série `0.3.x` doit construire un framework interne global et réutilisable, pas une collection de correctifs isolés.

- [x] Centraliser une première identification culturelle dans un service commun avec profils configurables en XML.
- [x] Permettre à un profil de déclarer ses critères d'identification, générateurs de noms, backstories autorisées, priorités et restrictions de scénario.
- [x] Raisonner en profils culturels plutôt qu'en simples races biologiques afin de couvrir Jaffa soumis ou libres, hôtes Goa'uld ou Tok'ra, Tau'ri / SGC et futurs cas hybrides.
- [ ] Réutiliser ces profils, lorsque pertinent, pour les noms, backstories, pawns de départ, identités persistantes, scénarios, génération de pawns, incidents, quêtes, équipements culturels et outils debug.
- [x] Garder des interfaces C# stables et étendre le moteur seulement lorsqu'un nouveau besoin réel n'est pas exprimable par les Defs existantes.
- [x] Éviter la sur-généralisation : une abstraction doit répondre à plusieurs usages réels avant d'être intégrée au noyau commun.
- [x] Ajouter des diagnostics techniques pour les profils absents, ambigus, contradictoires ou mal configurés.

## Noms, cultures et backstories

- [x] Premier générateur de noms culturels publié dans `0.3.5-dev` pour les Jaffa Goa'uld, Jaffa libres, Goa'uld, Tok'ra et Tau'ri / SGC.
- [ ] Ajouter les générateurs de noms et les backstories propres aux Asgard, Nox, Unas et autres cultures lors de leur création ou dans un jalon immédiatement suivant.
- [x] Valider dans `0.3.10-dev` la persistance et l'affichage séparés des identités de l'hôte et du symbiote, selon `docs/TOKRA_DUAL_IDENTITY_DESIGN.md`.
- [x] Valider en jeu la refonte `0.3.8-dev` des `52` backstories existantes et de leurs descriptions anglaises et françaises.
- [x] Valider en jeu les bonus de compétences modérés ajoutés aux backstories dans `0.3.8-dev`.
- [x] Organiser une discussion dédiée avant d'étendre le nombre de backstories ; première extension mesurée validée dans `0.3.16-dev`.
- [x] Clôturer dans `0.3.19-dev` l’audit de couverture des compétences par culture ou race : vérifier que chaque catalogue couvre suffisamment les compétences RimWorld pertinentes, identifier les compétences absentes ou sous-représentées et compléter uniquement les lacunes réelles avec des backstories culturellement cohérentes.
  - Auditer au minimum les Tau'ri / SGC, Jaffa Goa'uld, Jaffa libres, hôtes Goa'uld et Tok'ra.
  - Appliquer le même contrôle aux futures cultures Asgard, Nox, Unas et à toute nouvelle race ou faction disposant de backstories.
  - Éviter de gonfler artificiellement le catalogue : une compétence peut être couverte par plusieurs parcours complémentaires sans exiger une backstory dédiée à chaque combinaison.
- [x] Vérifier la cohérence entre noms, backstories, factions, marques Jaffa et identités sociales avec le diagnostic unifié de `0.3.15-dev`.
- [x] Migrer les générateurs de noms existants vers la consommation des profils culturels communs sans renommer les pawns déjà traités.
- [x] Valider les pools d'identité hôte / symbiote dans les profils sans imposer leur affichage aux pawns gérés par l'IA.
- [x] Maintenir dans le wiki les tableaux de backstories par culture avec nom, description et bonus de compétences ; toute future backstory doit y être ajoutée dans le même jalon.

## Identité Tok'ra contrôlée par le joueur

La conception détaillée est conservée dans `docs/TOKRA_DUAL_IDENTITY_DESIGN.md`. Elle doit être reprise dans un jalon dédié après audit des mécanismes vanilla et prototype.

- [x] Valider dans `0.3.10-dev` la conservation durable du nom et des backstories propres du symbiote après implantation.
- [x] Valider dans `0.3.10-dev` la conservation de l'identité originale de l'hôte sans renommer ni réécrire rétroactivement son histoire.
- [x] Implémenter dans `0.3.11-dev` le gizmo de basculement réservé aux Tok'ra appartenant au joueur et directement contrôlables.
- [x] Laisser dans `0.3.11-dev` les Tok'ra gérés par le jeu dans leur fonctionnement classique, sans gizmo ni changement manuel de personnalité.
- [x] Implémenter dans `0.3.11-dev` le basculement du nom, des backstories affichées et des seuls écarts de compétences dus aux backstories actives.
- [x] Implémenter dans `0.3.11-dev` une progression commune des niveaux et de l'expérience, à valider contre toute perte, duplication ou cumul lors des tests ciblés.
- [x] Afficher dès `0.3.10-dev` les deux identités dans l'inspection des Tok'ra contrôlés par le joueur, puis conserver cet affichage quelle que soit la personnalité active dans le futur jalon de basculement.
- [x] Vérifier sauvegarde, rechargement, extraction et réimplantation.
- [x] Auditer dans `0.3.12-dev` les relations, lettres, quêtes et interfaces principales ; traiter la mort, le cadavre, la tombe et la résurrection dans le jalon dédié `0.3.14-dev`.
- [x] Exclure par défaut les invités et pawns de quête temporairement contrôlables tant qu'ils ne rejoignent pas réellement la colonie.
- [ ] Réévaluer cette frontière uniquement si un futur type de pawn temporaire possède un véritable contrôle joueur et un besoin de gameplay démontré.
- [x] Étendre dans `0.3.12-dev` le basculement aux Tok'ra propriétaires d'une caravane directement contrôlée par le joueur.

## Futures races et factions

### Asgard

- [ ] Ajouter une race technologiquement avancée orientée soutien commercial, aide militaire et attribution de quêtes.
- [ ] Concevoir une présence sans colonie ou base mondiale permanente visible, proche d'une organisation itinérante de soutien.
- [ ] Prévoir une tendance naturellement alliée aux Tau'ri sans rendre cette relation absolument fixe dans tous les scénarios.
- [ ] Créer des incidents de commerce, d'assistance et de mission compatibles avec tous les storytellers.

### Nox

- [ ] Ajouter une race pacifique à l'apparence primitive mais technologiquement avancée.
- [ ] Orienter sa présence vers le commerce, la diplomatie et les rencontres non militaires.
- [ ] Conserver une tendance neutre et éviter les raids ou renforts armés ordinaires.
- [ ] Représenter leur technologie discrète sans les transformer en simple faction tribale vanilla.

### Unas

- [ ] Ajouter une race reptilienne généralement hostile, avec cultures ou variantes tribales possibles.
- [ ] Permettre aux Unas de servir d'hôtes Goa'uld compatibles avec le système persistant de symbiote.
- [ ] Prévoir des PawnKinds sauvages, tribaux ou dominés par les Goa'uld selon les futurs besoins.
- [ ] Éviter de rendre toute présence Unas obligatoirement identique ou uniformément organisée.

## Monde entièrement GateRim SG-1

- [ ] Permettre de créer une partie avec uniquement les races, cultures et factions GateRim SG-1.
- [ ] Ajouter un préréglage ou scénario optionnel de génération du monde retirant les factions vanilla sélectionnables.
- [ ] Conserver uniquement les factions système techniquement indispensables lorsque leur suppression complète n'est pas sûre.
- [ ] Couvrir à terme les Tau'ri / SGC, Goa'uld, Jaffa soumis, Jaffa libres, Tok'ra, Asgard, Nox, Unas et futures civilisations Stargate.
- [ ] Vérifier raids, caravanes, commerce, quêtes, incidents, relations, génération de pawns et conditions de victoire sans factions vanilla.
- [ ] Garder tous les contenus du mod fonctionnels dans une partie vanilla ou moddé classique : ce préréglage ne doit jamais devenir obligatoire.

## Storyteller et orchestration des événements

- [ ] Concevoir un storyteller GateRim SG-1 avec une identité, un rythme et des pondérations propres.
- [ ] Orchestrer de manière cohérente les incidents, opérations, quêtes, factions et menaces du mod.
- [ ] Intégrer ce storyteller au futur préréglage de monde entièrement GateRim SG-1.
- [ ] Garder tous les incidents et événements GateRim SG-1 accessibles avec les storytellers vanilla ou moddés compatibles.
- [ ] Ne jamais faire du storyteller dédié une dépendance obligatoire pour recevoir le contenu du mod.
- [ ] Utiliser son orchestration pour espacer les opérations Tok'ra et éviter les successions artificielles d'événements GateRim SG-1.

## Progression Stargate et contenu majeur

- [ ] Continuer la progression de recherche déjà amorcée autour des fondations Stargate et des expéditions hors monde.
- [ ] Définir le jalon d'introduction de la Porte des étoiles fonctionnelle seulement lorsque la tranche de jeu sans Porte est suffisamment solide.
- [ ] Préserver les événements, factions, armes, biologie et opérations actuelles comme contenu jouable indépendant de la Porte.
- [ ] Prévoir des missions sur cartes temporaires lorsque la mécanique justifie réellement de quitter la carte de colonie.

## Documentation et wiki

- [ ] Maintenir en priorité une version française solide, complète et cohérente du wiki.
- [ ] Créer une version anglaise du wiki seulement lorsque la documentation française et les mécaniques principales seront suffisamment stabilisées.
- [ ] Pour une future version bilingue, conserver les pages françaises existantes et ajouter des pages `*-EN.md` avec navigation et liens de langue explicites.
- [ ] Mettre à jour `docs/wiki/Home.md`, `Content-Status.md` et les pages de roadmap à chaque changement majeur de chapitre ou de version.
- [ ] Conserver les commandes, noms propres et identifiants techniques en anglais uniquement lorsqu'une traduction serait inadaptée.

## Maintenance du projet

- [ ] Continuer les jalons sur des branches `feature/...` dédiées depuis le dernier tag publié.
- [ ] Mettre à jour `docs/PROJECT_STATE.md` à chaque jalon avec la branche, le périmètre, les tests et l'étape suivante.
- [ ] Ajouter les tests durables dans `docs/TESTING.md`, sans multiplier les fichiers de plan de test temporaires.
- [ ] Signaler explicitement chaque fichier à supprimer avant l'extraction d'un correctif ZIP.
- [ ] Générer les ZIP directement à la racine du dépôt, où ils sont couverts par `.gitignore`.
- [ ] Préserver systématiquement `About/ModIcon.png`.
- [ ] Utiliser uniquement `About/About.xml` et `docs/CHANGELOG.md`, sans doublons à la racine.

## Règle de clôture

Lorsqu'un élément est terminé :

1. le résultat validé doit être décrit dans `docs/CHANGELOG.md` et `docs/PROJECT_STATE.md` ;
2. l'élément doit être retiré de cette roadmap ou marqué comme terminé seulement s'il reste utile pour comprendre les étapes suivantes ;
3. les nouveaux travaux découverts pendant les tests doivent être ajoutés ici avant de clôturer le jalon.
