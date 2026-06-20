# GateRim SG-1 — Roadmap durable

Ce fichier est la référence centrale pour les ajouts, refontes et actions futures du projet.

Il doit être consulté avec `docs/PROJECT_STATE.md` au début de chaque nouvelle discussion, après une perte de contexte ou avant de choisir un nouveau jalon.

- `docs/PROJECT_STATE.md` décrit le jalon actuellement actif et ses tests.
- `docs/ROADMAP.md` conserve les travaux futurs qui ne doivent pas être oubliés.
- `docs/CHANGELOG.md` conserve l'historique des jalons terminés.
- `docs/MILESTONE_PUBLICATION.md` conserve la procédure de validation et de publication.

Lorsqu'une nouvelle idée durable est validée pendant une discussion, elle doit être ajoutée ici au plus tard dans le correctif documentaire du jalon en cours.

## Priorité immédiate

### Basculement de personnalité Tok'ra contrôlé par le joueur (`0.3.11-dev`)

- [x] Partir du système d'identité persistante publié dans `v0.3.10-dev`.
- [x] Ajouter un gizmo uniquement aux Tok'ra appartenant au joueur et directement contrôlables.
- [x] Basculer le nom actif entre le nom exact de l'hôte et le nom du symbiote.
- [x] Basculer l'enfance, l'âge adulte et le titre affichés entre les deux identités persistantes.
- [x] Ajouter un service culturel réutilisable qui applique uniquement les écarts de compétences dus aux backstories.
- [x] Conserver une progression d'XP commune sans cumuler ni perdre les bonus lors des basculements.
- [x] Persister la personnalité active et les données de progression partagée.
- [x] Restaurer automatiquement l'identité de l'hôte avant extraction, transfert ou retrait du Hediff.
- [x] Conserver le comportement classique sans gizmo pour les Tok'ra gérés par l'IA.
- [x] Valider en jeu les basculements répétés, l'XP commune, les sauvegardes, l'extraction et la frontière joueur/IA.
- [ ] Publier la branche `feature/tokra-personality-switching`, le tag `v0.3.11-dev` et synchroniser le wiki.

### Après validation

- [ ] Auditer les éventuelles incohérences restantes dans les lettres, quêtes, relations sociales et interfaces tierces lorsque la personnalité du symbiote est active.
- [ ] Décider si les invités ou pawns de quête temporairement contrôlables doivent rester exclus ou recevoir une règle dédiée.

## Présentation du mod et métadonnées

- [x] Réécrire la description de `About/About.xml` dans un style court, immersif et immédiatement compréhensible.
- [x] Retirer les inventaires de fonctionnalités, détails de jalons, prototypes internes et éléments de roadmap.
- [x] Structurer la présentation autour de l'ambiance, des factions, des possibilités de jeu et des dépendances indispensables.
- [ ] Harmoniser ultérieurement cette présentation avec la page Workshop et l'accueil du wiki lors d'un jalon qui modifiera réellement `docs/wiki/*.md`.

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
- [ ] Organiser une discussion dédiée avant d'étendre le nombre de backstories ; conserver une quantité raisonnable, lisible et maintenable plutôt qu'un catalogue massif.
- [ ] Vérifier la cohérence entre noms, backstories, factions, marques Jaffa et identités sociales.
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
- [ ] Vérifier plus tard la mort, la résurrection éventuelle, les relations, lettres, quêtes et interfaces tierces lorsque la personnalité du symbiote est active.
- [ ] Décider explicitement du cas des invités ou pawns de quête temporairement contrôlables ; les exclure par défaut tant qu'ils ne rejoignent pas la colonie.

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
