# GateRim SG-1 — Roadmap durable

Ce fichier est la référence centrale pour les ajouts, refontes et actions futures du projet.

Il doit être consulté avec `docs/PROJECT_STATE.md` au début de chaque nouvelle discussion, après une perte de contexte ou avant de choisir un nouveau jalon.

- `docs/PROJECT_STATE.md` décrit le jalon actuellement actif et ses tests.
- `docs/ROADMAP.md` conserve les travaux futurs qui ne doivent pas être oubliés.
- `docs/CHANGELOG.md` conserve l'historique des jalons terminés.
- `docs/MILESTONE_PUBLICATION.md` conserve la procédure de validation et de publication.

Lorsqu'une nouvelle idée durable est validée pendant une discussion, elle doit être ajoutée ici au plus tard dans le correctif documentaire du jalon en cours.

## Dernier jalon clôturé

### Audit de couverture des compétences culturelles (`0.3.19-dev`)

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
