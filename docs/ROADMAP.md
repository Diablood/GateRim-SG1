# Roadmap

Ce fichier est le backlog durable du projet. Il conserve les travaux ouverts,
les règles qui doivent guider de futurs jalons et le dernier jalon en cours.
L'historique des versions publiées appartient à `docs/CHANGELOG.md` et aux tags
Git ; les pistes non décidées appartiennent à `docs/IDEAS_TO_REVISIT.md`.

## Jalon validé et publié - Consolidation documentaire (`0.3.63-dev`)

- [x] Partir du tag publié `v0.3.62-dev` sur
  `feature/documentation-consolidation`.
- [x] Créer `docs/README.md` comme index et contrat de rangement durable.
- [x] Transférer les questions encore utiles des anciennes feuilles de route
  sur les reines Goa'uld et les interactions Tok'ra vers
  `docs/IDEAS_TO_REVISIT.md`.
- [x] Supprimer les cinq documents techniques obsolètes ou redondants retenus
  par l'audit, sans créer de dossier d'archives dans l'arbre courant.
- [x] Retirer la feuille de route Tok'ra obsolète du brouillon wiki, de la page
  Tok'ra et de la navigation publique.
- [x] Réduire cette roadmap au jalon courant, aux travaux ouverts et aux règles
  durables ; l'historique publié reste dans le changelog et Git.
- [x] Conserver `docs/TESTING.md` et les spécifications techniques détaillées
  hors de cette première passe afin de ne pas mélanger nettoyage sûr et fusion
  fonctionnelle à risque.
- [x] Ajouter dans `AGENTS.md` la règle de mise à jour prioritaire d'un document
  de sous-système existant avant toute nouvelle fiche de micro-jalon.
- [x] Valider les suppressions, les liens locaux, la navigation wiki, les
  versions, le rebuild `0.3.63.0` et les contrôles de cohérence.
- [x] Faire approuver la structure consolidée et les six suppressions par le
  mainteneur sur la révision locale `r1`.
- [x] Publier la branche, le tag annoté `v0.3.63-dev` et le wiki séparé
  seulement après validation et autorisation explicite.

Ce jalon ne change aucun comportement de jeu, Def, traduction ou texture. Git
et les tags publiés restent l'archive des documents retirés.

La révision finale `r1` est publiée avec la branche dédiée, le tag annoté
`v0.3.63-dev` et le wiki séparé synchronisé, y compris la suppression explicite
de l'ancienne page de roadmap Tok'ra.

## Registre d'idées non planifiées

Les pistes exploratoires sans jalon décidé sont conservées dans
[`docs/IDEAS_TO_REVISIT.md`](IDEAS_TO_REVISIT.md). Leur présence évite une
perte de contexte mais ne doit jamais déclencher automatiquement du travail.

## Présentation et publication publique

- [ ] Publier et maintenir la description Workshop lors de la préparation de
  la première version publique, à partir du README, de `About/About.xml` et de
  l'accueil du wiki.

## Passe visuelle globale

Cette passe reste différée jusqu'à stabilisation des mécaniques, races,
factions et scénarios. Les textures actuelles servent souvent de placeholders
sur des chemins définitifs ; éviter les micro-jalons isolés de finition.

- [ ] Auditer les pawns, vêtements, objets, dispositifs, modules, marqueurs et
  bâtiments qui utilisent encore des visuels provisoires ou trompeurs.
- [ ] Remplacer le dispositif d'observation portable, actuellement trop proche
  d'un courrier et peu lisible sur la carte.
- [ ] Vérifier le module de renseignements Tok'ra et les objets d'opération afin
  que leur silhouette indique immédiatement leur fonction.
- [ ] Vérifier les marqueurs et sites temporaires sans ajouter de gizmo lorsque
  le vrai problème est leur lisibilité.
- [ ] Donner à l'officier Jaffa capturable une apparence distinctive sans
  dépendre uniquement de sa marque frontale.
- [ ] Harmoniser les objets Tok'ra, Goa'uld, Jaffa et SGC autour d'identités
  visuelles cohérentes.
- [ ] Valider un concept propre par famille avant de produire les textures
  finales.
- [ ] Ajouter les visuels définitifs au wiki et réutiliser les meilleurs pour
  la présentation Workshop.

## Équipement Goa'uld et attributs de rang

- [ ] Étudier chaque futur dispositif de main, technologie de contrôle, soin
  avancé ou attribut de rang comme un objet ou système distinct ; ne pas faire
  du kara kesh un appareil universel.
- [ ] Tester chaque nouvel équipement contre les armes vanilla et GateRim SG-1,
  le corps à corps, l'IEM lorsque pertinent, les caravanes et la sauvegarde.
- [ ] Intégrer sa puissance réelle dans `combatPower`, les budgets de menace,
  la valeur, l'acquisition et la disponibilité comme butin.
- [ ] Réserver les technologies les plus fortes aux rangs cohérents et éviter
  que chaque raid fournisse automatiquement un objet rare.

## Pression et rivalités des domaines Goa'uld

- [ ] Différencier éventuellement les domaines par préférences stratégiques ou
  poids de doctrines, sans coder un système unique par Grand Maître.
- [ ] Étendre les exigences et ultimatums uniquement à partir d'une cause
  visible et d'une conséquence compréhensible, sans copier les offres Tok'ra.
- [ ] Représenter les conflits entre domaines d'abord par des informations RP,
  puis éventuellement par expansion territoriale et destruction de colonies.
- [ ] Concevoir avant toute simulation des garde-fous discrets contre
  l'auto-élimination, l'expansion incontrôlée et le déséquilibre du monde.

## Reines Goa'uld

Les extensions encore spéculatives de l'origine des larves sont conservées
dans `docs/IDEAS_TO_REVISIT.md`. Aucun remplacement du cycle actuel n'est
planifié tant que les besoins de gameplay et d'infrastructure ne sont pas
cadrés.

## Opérations Tok'ra organiques

Le pool reste fermé aux huit archétypes publiés. Les futures parties longues
doivent surtout révéler les correctifs et ajustements nécessaires.

- [ ] Continuer à tester observation, renseignements, agent blessé, remise
  médicale, appel de détresse, livraison, diversion et capture d'officier.
- [ ] Corriger uniquement les défauts, déséquilibres ou instructions ambiguës
  observés en partie.
- [ ] Conserver un seul slot d'opération organique visible à la fois.
- [ ] Garder catalogue, pondérations, délais et historique hors de l'interface
  normale.
- [ ] Réserver les diagnostics complets au mode développeur ou à l'option
  avancée en lecture seule.
- [ ] Maintenir plusieurs variantes RP lorsque la répétition serait visible.

## Missions et questlines

- [ ] Garder chaque archétype récurrent rééligible après réussite, échec ou
  offre ignorée lorsque sa conception le prévoit.
- [ ] Conserver des délais cachés variables et un anti-répétition local.
- [ ] Dimensionner menaces et effectifs depuis la difficulté, les points de
  menace et la valeur de colonie plutôt qu'avec des nombres fixes.
- [ ] Garder les détails techniques dans les diagnostics et les logs.
- [ ] Étendre le framework commun uniquement lorsqu'au moins deux besoins réels
  justifient la même abstraction.
- [ ] Conserver des adaptateurs spécialisés pour les sites mondiaux, caravanes,
  interceptions et combats atypiques.

## Interface et outils de debug

- [ ] Regrouper les actions d'un même appareil dans une entrée cohérente.
- [ ] Organiser les actions par thème ou phase avec des libellés courts.
- [ ] Garder les détails techniques dans les rapports et les logs.
- [ ] Vérifier qu'aucun outil d'action n'est visible hors mode développeur.
- [ ] Effectuer avant stabilisation publique une passe sur le ton RP, les
  boutons et l'absence d'informations techniques inutiles.

## Équipement Tau'ri / SGC

- [ ] Ajouter de futures variantes de pantalons et vestes comme contenu XML
  pondéré, avec une apparence cohérente entre les pièces.
- [ ] Conserver les armes humaines vanilla dans le scénario et n'envisager que
  des substitutions facultatives pour des mods compatibles.

## Framework culturel, noms et backstories

- [ ] Réutiliser les profils culturels pour les futurs noms, backstories,
  starters, identités, incidents, missions et équipements lorsqu'ils expriment
  réellement le besoin.
- [ ] Ajouter les noms et backstories Asgard, Nox, Unas et autres cultures lors
  de leur création, sans gonfler artificiellement les catalogues.
- [ ] Maintenir les tableaux du wiki et les contrôles de couverture pour toute
  future backstory.

## Origines d'hôtes et identité Tok'ra

- [ ] Ajouter des origines pondérées Jaffa, Unas ou autres uniquement lorsque
  ces origines existent réellement en jeu.
- [ ] Garder le moteur d'origine générique et privilégier les ajouts XML.
- [ ] Vérifier compatibilités biologiques, noms et backstories avant activation.
- [ ] Ne jamais réécrire l'origine d'une implantation réelle : l'identité du
  pawn existant doit rester intacte.
- [ ] Réévaluer les pawns temporairement contrôlables seulement face à un cas
  concret qui exige le basculement d'identité Tok'ra.
- [ ] Tester les interfaces de préparation ou de gestion de pawns de mods tiers
  uniquement lorsqu'une incompatibilité concrète est signalée.

## Futures races et factions

### Asgard

- [ ] Ajouter une race avancée orientée commerce, assistance et missions.
- [ ] Concevoir une présence itinérante sans colonie mondiale obligatoire.
- [ ] Prévoir une tendance alliée aux Tau'ri sans relation absolument fixe.
- [ ] Rester compatible avec les storytellers vanilla et moddées.

### Nox

- [ ] Ajouter une race pacifique d'apparence primitive mais avancée.
- [ ] Orienter sa présence vers commerce, diplomatie et rencontres non armées.
- [ ] Éviter raids et renforts militaires ordinaires.
- [ ] Représenter sa technologie discrète sans en faire une faction tribale.
- [ ] Lui attribuer un rôle commercial complémentaire aux Jaffa libres.

### Unas

- [ ] Ajouter une race reptilienne avec variantes sauvages ou tribales.
- [ ] Permettre aux Unas de servir d'hôtes Goa'uld compatibles.
- [ ] Prévoir des PawnKinds sauvages, tribaux ou dominés selon les besoins.
- [ ] Éviter une culture Unas uniformément hostile ou organisée.

## Monde entièrement GateRim SG-1

- [ ] Ajouter un préréglage optionnel retirant les factions vanilla
  sélectionnables tout en conservant les factions système indispensables.
- [ ] Couvrir à terme les cultures et factions Stargate disponibles.
- [ ] Vérifier raids, commerce, missions, relations, génération et victoire.
- [ ] Répartir les rôles économiques sans doublons ni impasses.
- [ ] Garder tout le contenu compatible avec une partie classique ; ce mode ne
  doit jamais devenir obligatoire.

## Storyteller et orchestration

- [ ] Concevoir un storyteller GateRim SG-1 avec rythme et pondérations propres.
- [ ] Orchestrer incidents, opérations, missions, factions et menaces.
- [ ] L'intégrer au préréglage de monde entièrement GateRim SG-1.
- [ ] Garder les événements accessibles avec les autres storytellers.
- [ ] Ne jamais en faire une dépendance du contenu du mod.
- [ ] Espacer les événements GateRim pour éviter les successions artificielles.

## Progression Stargate

- [ ] Continuer la recherche autour des fondations Stargate et des expéditions.
- [ ] Introduire la Porte fonctionnelle seulement lorsque le jeu sans Porte est
  suffisamment solide.
- [ ] Préserver factions, armes, biologie et opérations comme contenu autonome.
- [ ] Utiliser des cartes temporaires lorsque quitter la colonie est justifié.

## Documentation et wiki

- [ ] Maintenir d'abord une version française complète et cohérente du wiki.
- [ ] N'ajouter les pages anglaises qu'après stabilisation, avec suffixe
  `*-EN.md` et liens de langue explicites.
- [ ] Mettre à jour `docs/wiki/Home.md`, `Content-Status.md` et les pages de
  sous-système lors de chaque changement public pertinent.
- [ ] Générer un nouveau rapport natif de traduction française avant de corriger
  les anciennes alertes ; ne pas réutiliser le décompte historique de cinq
  erreurs sans vérification.
- [ ] Conserver l'anglais pour les commandes et identifiants techniques lorsque
  leur traduction serait trompeuse.

## Audits transversaux

- [ ] Réexaminer les catégories de stockage lors de la stabilisation finale des
  objets Goa'uld, Jaffa et médicaux.
- [ ] Vérifier les interactions involontaires avec nourriture, recettes,
  stockage et commerce.
- [ ] Conserver les compatibilités DLC exploratoires dans
  `docs/IDEAS_TO_REVISIT.md` jusqu'à cadrage explicite.

## Maintenance du projet

- [ ] Continuer sur des branches `feature/...` créées depuis le dernier tag.
- [ ] Maintenir `PROJECT_STATE.md`, `TESTING_CURRENT.md`, `TESTING.md` et le
  changelog selon leur rôle défini dans `docs/README.md`.
- [ ] Mettre à jour un document de sous-système existant avant d'en créer un
  nouveau pour un micro-jalon.
- [ ] Signaler les suppressions avant toute extraction de ZIP.
- [ ] Générer les ZIP à la racine, où ils restent ignorés par Git.
- [ ] Préserver `About/ModIcon.png`.
- [ ] Ne conserver qu'`About/About.xml` et `docs/CHANGELOG.md` comme fichiers
  de métadonnées et historique officiels.

## Règle de clôture

Lorsqu'un élément est terminé :

1. décrire le résultat dans le changelog et l'état courant ;
2. retirer sa checklist de cette roadmap, sauf règle durable nécessaire ;
3. inscrire les travaux décidés dans le backlog ;
4. transférer les pistes exploratoires dans `IDEAS_TO_REVISIT.md` ;
5. utiliser Git et les tags comme archive plutôt qu'un dossier d'anciens plans.
