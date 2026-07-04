# Roadmap

Ce fichier est le backlog durable du projet. Il conserve les travaux ouverts,
les règles qui doivent guider de futurs jalons et le dernier jalon clôturé.
L'historique publié appartient à `docs/CHANGELOG.md` et aux tags Git ; les pistes
non décidées appartiennent à `docs/IDEAS_TO_REVISIT.md`.

## Dernier jalon clôturé - Nouveau fonctionnement des branches (`0.3.67-dev`)

- [x] Créer `develop` exactement depuis le commit ciblé par `v0.3.66-dev`.
- [x] Publier `develop` comme branche d'intégration distante.
- [x] Créer `feature/develop-branch-workflow` depuis `develop`.
- [x] Réserver `main` à la future ligne stable `1.0.0` et aux correctifs stables.
- [x] Définir `develop` comme état intégré du dernier jalon de développement
  validé.
- [x] Exiger que les branches `feature/*` et `fix/*` partent d'un `develop` à
  jour.
- [x] Intégrer les jalons validés avec `git merge --ff-only`.
- [x] Créer les tags `v...-dev` après intégration, sur le même commit que
  `develop`.
- [x] Rendre facultative la publication des branches temporaires et autoriser
  leur suppression après vérification du tag.
- [x] Documenter la gestion de divergence, le passage futur à `1.0.0`, les
  hotfixes stables et la comparaison correcte des tags annotés.
- [x] Valider le rebuild `0.3.67.0`, le contrôle de cohérence et le démarrage
  minimal.
- [x] Publier le jalon par fast-forward dans `develop`, publier le tag annoté
  `v0.3.67-dev`, laisser `main` inchangée et synchroniser les métadonnées wiki.

La révision finale `r4` est validée et publiée. Le nouveau flux devient la règle
permanente du projet : `develop` est la base intégrée, les branches de jalon sont
temporaires et chaque tag de développement pointe sur un commit intégré dans
`develop`.

## Prochain jalon décidé - Réduction de pression en conflit ouvert (`0.3.68-dev`)

- [ ] Partir de `develop` après le tag publié `v0.3.67-dev`.
- [ ] Créer `feature/goauld-open-conflict-pressure-reduction`.
- [ ] Réduire de façon limitée et plafonnée la pression des raids naturels d'un
  domaine engagé dans au moins un conflit ouvert.
- [ ] Ne pas cumuler la réduction lorsqu'un domaine affronte plusieurs rivaux.
- [ ] Garder les représailles, raids contrôlés et missions hors de ce
  modificateur.
- [ ] Conserver les poids de doctrine fondés sur les points initiaux.
- [ ] Limiter l'effet au storyteller `Commandement SG-1`.
- [ ] Ajouter diagnostics, tests ciblés, régressions et documentation.

## Registre d'idées non planifiées

Les pistes exploratoires sans jalon décidé sont conservées dans
[`docs/IDEAS_TO_REVISIT.md`](IDEAS_TO_REVISIT.md).

## Présentation et publication publique

- [ ] Maintenir la description Workshop à partir du README, de
  `About/About.xml` et de l'accueil du wiki.

## Passe visuelle globale

- [ ] Auditer les visuels provisoires ou trompeurs.
- [ ] Remplacer le dispositif d'observation portable.
- [ ] Vérifier le module de renseignements Tok'ra et les objets d'opération.
- [ ] Diversifier les icônes de sites et missions mondiales.
- [ ] Donner à l'officier Jaffa capturable une apparence distinctive.
- [ ] Harmoniser les identités visuelles Tok'ra, Goa'uld, Jaffa et SGC.
- [ ] Ajouter les visuels définitifs au wiki et à la présentation Workshop.

## Équipement Goa'uld et attributs de rang

- [ ] Étudier chaque dispositif comme un objet ou système distinct.
- [ ] Tester armes, mêlée, IEM, caravanes et sauvegarde.
- [ ] Refléter la puissance dans `combatPower`, menace, valeur et acquisition.
- [ ] Réserver les technologies fortes aux rangs cohérents.

## Pression et rivalités des domaines Goa'uld

- [x] Ajouter des relations persistantes par paire de domaines : neutralité,
  rivalité, conflit ouvert, trêve et alliance.
- [x] Faire évoluer automatiquement ces relations uniquement lorsque le
  storyteller GateRim SG-1 est actif.
- [x] Conserver les relations aux factions plutôt qu'aux dirigeants.
- [ ] En conflit ouvert, réduire de façon limitée et plafonnée la pression des
  deux domaines contre le joueur.
- [ ] Permettre un événement rare sur la carte : deux troupes de domaines
  rivaux s'affrontent près de la colonie, avec une lettre, une durée maximale
  de quelques jours et une intervention facultative du joueur contre un camp
  ou les deux.
- [ ] En alliance, permettre une légère augmentation plafonnée de la fréquence
  ou de la puissance des attaques des domaines concernés.
- [ ] Étendre ultérieurement les alliances par des renforts d'un second domaine,
  des raids conjoints, des interactions de doctrines, des représailles communes
  ou une rupture après échec.
- [x] Représenter d'abord les relations par des rapports RP avant toute expansion
  ou destruction réelle de colonies.
- [ ] Concevoir les garde-fous des futurs effets contre auto-élimination,
  empilement de bonus, expansion incontrôlée et déséquilibre mondial.

## Reines Goa'uld

Les extensions spéculatives de l'origine des larves restent dans
`docs/IDEAS_TO_REVISIT.md`.

## Opérations Tok'ra organiques

- [ ] Continuer à tester les huit archétypes publiés.
- [ ] Corriger uniquement les défauts observés en partie.
- [ ] Conserver un seul slot visible, les délais cachés et l'anti-répétition.
- [ ] Réserver les diagnostics complets aux outils avancés.
- [ ] Maintenir des variantes RP solides.

## Missions et questlines

- [ ] Garder les missions récurrentes rééligibles lorsqu'elles le prévoient.
- [ ] Dimensionner les menaces depuis la difficulté et la valeur de colonie.
- [ ] Étendre le framework commun uniquement sur plusieurs besoins réels.
- [ ] Conserver des adaptateurs spécialisés pour les cas atypiques.

## Interface et outils de debug

- [ ] Regrouper les actions par appareil et par phase.
- [ ] Garder les détails techniques dans les rapports et les logs.
- [ ] Vérifier qu'aucune action n'est visible hors mode développeur.
- [ ] Effectuer une passe finale sur les libellés et le ton RP.

## Équipement Tau'ri / SGC

- [ ] Ajouter les futures variantes de pantalons et vestes par XML pondéré.
- [ ] Conserver les armes humaines vanilla, avec substitutions facultatives.

## Framework culturel, noms et backstories

- [ ] Réutiliser les profils culturels lorsqu'ils expriment un besoin réel.
- [ ] Ajouter Asgard, Nox, Unas et autres cultures lors de leur création.
- [ ] Maintenir les tableaux wiki et les contrôles de couverture.

## Origines d'hôtes et identité Tok'ra

- [ ] Ajouter des origines pondérées seulement lorsque les cultures existent.
- [ ] Garder le moteur générique et privilégier les ajouts XML.
- [ ] Ne jamais réécrire l'origine d'une implantation réelle.
- [ ] Tester les mods tiers uniquement sur incompatibilité concrète.

## Futures races et factions

- [ ] Asgard : commerce, assistance et missions sans colonie obligatoire.
- [ ] Nox : présence pacifique, diplomatique et commerciale.
- [ ] Unas : variantes sauvages ou tribales et compatibilité comme hôtes.

## Monde entièrement GateRim SG-1

- [ ] Ajouter un préréglage optionnel sans factions vanilla sélectionnables.
- [ ] Couvrir les rôles économiques, raids, commerce, relations et victoire.
- [ ] Garder ce mode facultatif et le contenu compatible avec les parties normales.

## Storyteller et orchestration

- [x] Utiliser le storyteller GateRim SG-1 comme orchestrateur exclusif des
  relations stratégiques automatiques entre domaines.
- [x] Garder les incidents GateRim existants accessibles aux autres storytellers
  sans modifier leur cadence.
- [ ] Plafonner les futures réductions de pression en guerre et les futurs bonus
  d'alliance.
- [x] Espacer les événements stratégiques et appliquer un anti-répétition.
- [x] Suspendre les transitions automatiques lorsqu'un autre storyteller est
  actif, tout en conservant et en décalant l'état sérialisé pour une reprise
  ultérieure.

## Progression Stargate

- [ ] Continuer les fondations et expéditions.
- [ ] Introduire la Porte seulement lorsque le jeu sans Porte est solide.
- [ ] Préserver le contenu actuel comme autonome.

## Documentation et wiki

- [ ] Maintenir d'abord le wiki français.
- [ ] Mettre à jour l'accueil, l'état du contenu et les pages de sous-système.
- [ ] Conserver l'anglais pour les identifiants techniques lorsque nécessaire.
- [ ] Mettre à jour un document existant avant d'en créer un nouveau.

## Audits transversaux

- [ ] Réexaminer stockage, nourriture, recettes et commerce.
- [ ] Conserver les compatibilités DLC exploratoires dans les idées à revoir.

## Maintenance du projet

- [ ] Créer chaque branche `feature/*` ou `fix/*` depuis un `develop` à jour.
- [ ] Maintenir état, roadmap, tests et changelog selon `docs/README.md`.
- [ ] Signaler les suppressions avant extraction d'un ZIP.
- [ ] Garder les ZIP ignorés à la racine.
- [ ] Préserver `About/ModIcon.png`.

## Règle de clôture

Lorsqu'un élément est terminé :

1. décrire le résultat dans le changelog et l'état courant ;
2. retirer sa checklist de cette roadmap, sauf règle durable ;
3. inscrire les travaux décidés dans le backlog ;
4. transférer les pistes exploratoires dans `IDEAS_TO_REVISIT.md` ;
5. utiliser Git et les tags comme archive.
