# Roadmap

Ce fichier est le backlog durable du projet. Il conserve les travaux ouverts,
les règles qui doivent guider de futurs jalons et le dernier jalon clôturé.
L'historique publié appartient à `docs/CHANGELOG.md` et aux tags Git ; les pistes
non décidées appartiennent à `docs/IDEAS_TO_REVISIT.md`.

## Dernier jalon clôturé - Site mondial de bataille (`0.3.70-dev`)

La révision finale `r4` est validée et publiée. Les conflits ouverts peuvent
alterner entre une bataille locale et un site mondial facultatif, avec slot,
cooldown, paire et règles de combat partagés.

## Jalon courant - Formatage uniforme des durées (`0.3.71-dev`)

- [x] Partir de `develop` exactement alignée sur `v0.3.70-dev`.
- [x] Sélectionner `feature/standardize-duration-formatting`.
- [x] Créer un utilitaire commun fondé sur le formateur temporel vanilla.
- [x] Conserver les ticks bruts dans les diagnostics développeur.
- [x] Migrer une première tranche de sites mondiaux et de comptes à rebours.
- [x] Auditer les offres, statuts d'opérations et communicateurs restants.
- [x] Auditer l'ancien marqueur de planque et les inspections de menace interceptée.
- [x] Ajouter un contrôle statique global des unités fixes dans les
  traductions et des convertisseurs C# proches de l’interface joueur.
- [ ] Exécuter ce contrôle sur le dépôt complet ; la première passe du paquet a
  déjà corrigé cinq suffixes restants du relais décodé.
- [x] Vérifier en jeu toutes les surfaces anglaises et françaises de `r2`.
- [x] Confirmer sur `r2` qu'aucune durée réelle ni valeur d'équilibrage ne change.
- [x] Réussir le rebuild forcé `0.3.71.0`.
- [x] Valider sauvegarde/recharge, expirations et `Player.log` sur `r2`.
- [ ] Finaliser changelog, tests durables, état projet et wiki par ZIP cumulatif.
- [ ] Intégrer par fast-forward dans `develop` et publier `v0.3.71-dev`.

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
- [x] En conflit ouvert, réduire de façon limitée et plafonnée à `75%` les
  points des raids naturels ordinaires des deux domaines contre le joueur,
  sans cumul, sans changement de fréquence et uniquement sous Commandement SG-1.
- [x] Publier `0.3.69-dev` : bataille locale entre deux domaines en conflit
  ouvert, avec ralliement, intervention facultative et retrait borné.
- [x] Publier `0.3.70-dev` : site mondial temporaire représentant un autre
  champ de bataille entre domaines, visitable ou ignorable par caravane, avec
  slot et cooldown partagés.
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
