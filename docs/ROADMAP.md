# Roadmap

Ce fichier est le backlog durable du projet. Il conserve les travaux ouverts,
les règles qui doivent guider de futurs jalons et le dernier jalon en cours.
L'historique publié appartient à `docs/CHANGELOG.md` et aux tags Git ; les pistes
non décidées appartiennent à `docs/IDEAS_TO_REVISIT.md`.

## Jalon validé et publié - Fondation du storyteller GateRim SG-1 (`0.3.65-dev`)

- [x] Partir du tag publié `v0.3.64-dev` sur
  `feature/sg1-storyteller-foundation`.
- [x] Ajouter un storyteller `Commandement SG-1` optionnel.
- [x] Raccourcir sa description anglaise et française selon le style comportemental des storytellers vanilla, sans barre de défilement.
- [x] Utiliser le rythme Cassandra actuellement résolu comme baseline, sans
  figer une copie du XML Core.
- [x] Ajouter un composant d'orchestration GateRim sans incident actif.
- [x] Ajouter une détection commune fiable du storyteller actif.
- [x] Sérialiser activations, désactivations et rechargements.
- [x] Ajouter le rapport développeur exact.
- [x] Conserver tous les autres storytellers et incidents existants inchangés.
- [x] Documenter les relations futures entre domaines et leurs conséquences
  possibles sans les activer.
- [x] Exécuter le rebuild forcé `0.3.65.0` et les contrôles de cohérence.
- [x] Valider sélection, portrait, activation, sauvegarde/recharge,
  désactivation sous un autre storyteller, régressions et `Player.log`.
- [x] Confirmer que la description française `r2` tient sans barre de défilement.
- [x] Publier branche, tag annoté `v0.3.65-dev` et wiki après validation et
  autorisation explicite.

La révision finale `r2` est publiée avec la branche dédiée, le tag annoté
`v0.3.65-dev` et le wiki séparé synchronisé. La fondation fonctionnelle de
`r1`, le retest visuel sans barre de défilement, la sauvegarde/recharge, les
régressions et `Player.log` sont validés.

Le storyteller SG-1 constitue désormais la frontière d'orchestration des futurs
systèmes stratégiques propres au mod. Les autres storytellers continuent de
gérer leur rythme comme ils le souhaitent.


## Prochain jalon décidé - Relations persistantes entre domaines Goa'uld (`0.3.66-dev`)

- [ ] Partir du tag publié `v0.3.65-dev` sur
  `feature/goauld-inter-domain-relations`.
- [ ] Ajouter des états persistants par paire de domaines : neutralité,
  rivalité, conflit ouvert, trêve et alliance.
- [ ] Faire évoluer automatiquement ces relations uniquement sous le
  storyteller GateRim SG-1.
- [ ] Ajouter rapports RP, persistance, anti-répétition et diagnostics.
- [ ] Ne pas encore créer de bataille de carte, bonus d’alliance, réduction de
  pression, expansion ou destruction de colonie.

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

- [ ] Ajouter des relations persistantes par paire de domaines : neutralité,
  rivalité, conflit ouvert, trêve et alliance.
- [ ] Faire évoluer automatiquement ces relations uniquement lorsque le
  storyteller GateRim SG-1 est actif.
- [ ] Conserver les relations aux factions plutôt qu'aux dirigeants.
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
- [ ] Représenter les conflits par des rapports RP avant toute expansion ou
  destruction réelle de colonies.
- [ ] Concevoir des garde-fous contre auto-élimination, empilement de bonus,
  expansion incontrôlée et déséquilibre mondial.

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

- [ ] Utiliser le storyteller GateRim SG-1 comme orchestrateur exclusif des
  futures relations stratégiques automatiques entre domaines.
- [ ] Garder les incidents GateRim existants accessibles aux autres
  storytellers sans modifier leur cadence.
- [ ] Plafonner les réductions de pression en guerre et les bonus d'alliance.
- [ ] Espacer les événements stratégiques et appliquer un anti-répétition.
- [ ] Suspendre les transitions automatiques lorsqu'un autre storyteller est
  actif, tout en conservant l'état sérialisé pour une reprise ultérieure.

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

- [ ] Continuer sur des branches `feature/...` depuis le dernier tag.
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
