# GateRim SG-1 — Roadmap durable

Ce fichier est la référence centrale pour les ajouts, refontes et actions futures du projet.

Il doit être consulté avec `docs/PROJECT_STATE.md` au début de chaque nouvelle discussion, après une perte de contexte ou avant de choisir un nouveau jalon.

- `docs/PROJECT_STATE.md` décrit le jalon actuellement actif et ses tests.
- `docs/ROADMAP.md` conserve les travaux futurs qui ne doivent pas être oubliés.
- `docs/CHANGELOG.md` conserve l'historique des jalons terminés.
- `docs/MILESTONE_PUBLICATION.md` conserve la procédure de validation et de publication.

Lorsqu'une nouvelle idée durable est validée pendant une discussion, elle doit être ajoutée ici au plus tard dans le correctif documentaire du jalon en cours.

## Priorité immédiate

### Stabiliser et publier `0.3.5-dev`

- [ ] Valider les générateurs de noms pour Jaffa Goa'uld, Jaffa libres, Goa'uld, Tok'ra et Tau'ri / SGC.
- [ ] Vérifier que les personnages déjà présents, les colons de départ et les noms choisis par le joueur ne sont jamais écrasés.
- [ ] Vérifier les visiteurs, raids, dirigeants de faction, sauvegardes et chargements.
- [ ] Vérifier la distinction persistante entre nom d'hôte et nom de symbiote.
- [ ] Publier le jalon depuis `feature/cultural-pawn-name-generators` avec le tag final `v0.3.5-dev`.
- [ ] Synchroniser et publier le wiki séparé selon la procédure corrigée de `docs/MILESTONE_PUBLICATION.md`.

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

## Noms, cultures et backstories

- [ ] Stabiliser le premier générateur de noms culturels de `0.3.5-dev` pour les Jaffa Goa'uld, Jaffa libres, Goa'uld, Tok'ra et Tau'ri / SGC.
- [ ] Étendre plus tard les générateurs aux Asgard, Nox, Unas et autres cultures ajoutées au mod.
- [ ] Déterminer dans quelles interfaces normales afficher séparément le nom de l'hôte et celui du symbiote.
- [ ] Revoir toutes les backstories existantes pour enrichir leur texte descriptif.
- [ ] Ajouter des modificateurs de statistiques cohérents aux backstories qui n'en possèdent pas encore.
- [ ] Créer davantage de backstories afin d'améliorer la variété culturelle et le renouvellement des pawns.
- [ ] Vérifier la cohérence entre noms, backstories, factions, marques Jaffa et identités sociales.

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
