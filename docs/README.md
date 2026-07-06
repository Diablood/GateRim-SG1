# Documentation map

Ce répertoire contient la documentation interne du projet et les brouillons du
wiki joueur. Il ne doit pas devenir une archive de chaque micro-jalon : Git, les
tags et `CHANGELOG.md` conservent déjà l'historique publié.

## Points d'entrée autoritatifs

| Besoin | Document |
|---|---|
| Reprendre le travail courant | [`PROJECT_STATE.md`](PROJECT_STATE.md) |
| Choisir un travail décidé ou différé | [`ROADMAP.md`](ROADMAP.md) |
| Conserver une piste non planifiée | [`IDEAS_TO_REVISIT.md`](IDEAS_TO_REVISIT.md) |
| Consulter l'historique publié | [`CHANGELOG.md`](CHANGELOG.md) et tags Git |
| Choisir, créer et fusionner une branche | [`BRANCHING_WORKFLOW.md`](BRANCHING_WORKFLOW.md) |
| Exécuter la validation du jalon courant | [`TESTING_CURRENT.md`](TESTING_CURRENT.md) |
| Maintenir les régressions durables | [`TESTING.md`](TESTING.md) |
| Publier un jalon | [`MILESTONE_PUBLICATION.md`](MILESTONE_PUBLICATION.md) |
| Construire la DLL | [`BUILD.md`](BUILD.md) |
| Synchroniser le wiki | [`WIKI_WORKFLOW.md`](WIKI_WORKFLOW.md) |
| Contrôler métadonnées et liens locaux | [`PROJECT_CONSISTENCY_CHECKS.md`](PROJECT_CONSISTENCY_CHECKS.md) |

`PROJECT_STATE.md` est un passage de relais, pas un historique. `ROADMAP.md` est
un backlog actif, pas un second changelog. `TESTING_CURRENT.md` conserve la
dernière procédure validée jusqu'au jalon suivant.

## Règles de planification

`ROADMAP.md` distingue obligatoirement :

1. le jalon courant et le dernier jalon clôturé ;
2. les contrats permanents qui guident les futurs travaux ;
3. les futurs jalons décidés, chacun décrit comme une unité distincte.

Un travail déjà publié ne reste pas sous forme de case ouverte. Sa trace appartient
au changelog et aux tags. Une dette d'art peut toutefois rester ouverte lorsque
la mécanique est terminée mais que son placeholder ou sa texture temporaire doit
encore être remplacé.

Lorsqu'un futur jalon nécessite des choix encore ouverts, la roadmap indique les
décisions reportées sans inventer de réponse. Ces points sont reposés au
mainteneur lorsque le jalon devient actif, puis les réponses sont enregistrées
avant l'implémentation.

`IDEAS_TO_REVISIT.md` contient uniquement les pistes non décidées. Une idée
promue est retirée de ce registre et reçoit son propre jalon dans la roadmap.

## Références techniques durables

Les documents de sous-système décrivent les contrats encore utiles au code et
aux régressions. Les principales portes d'entrée sont :

- culture et identités : [`CULTURAL_FRAMEWORK.md`](CULTURAL_FRAMEWORK.md),
  [`CULTURAL_BACKSTORIES.md`](CULTURAL_BACKSTORIES.md) et
  [`TOKRA_DUAL_IDENTITY_DESIGN.md`](TOKRA_DUAL_IDENTITY_DESIGN.md) ;
- missions : [`MISSION_FRAMEWORK.md`](MISSION_FRAMEWORK.md) ;
- storyteller et orchestration : [`STORYTELLER_SG1.md`](STORYTELLER_SG1.md) ;
- direction Goa'uld : [`GOAULD_GAMEPLAY_DIRECTION.md`](GOAULD_GAMEPLAY_DIRECTION.md),
  [`GOAULD_OPEN_CONFLICT_BATTLEFIELD.md`](GOAULD_OPEN_CONFLICT_BATTLEFIELD.md),
  [`GOAULD_SHARED_ALLIANCE_REPRISALS.md`](GOAULD_SHARED_ALLIANCE_REPRISALS.md),
  [`GOAULD_TERRITORIAL_SAFEGUARDS.md`](GOAULD_TERRITORIAL_SAFEGUARDS.md),
  [`GOAULD_HOST.md`](GOAULD_HOST.md) et
  [`GOAULD_KARA_KESH.md`](GOAULD_KARA_KESH.md) ;
- Jaffa et Prim'ta : fichiers `JAFFA_*.md` et `PRIMTA_*.md` ;
- Tok'ra : fichiers `TOKRA_*.md`, classés par système ou opération ;
- équipement : fichiers `SG_*.md`, `MATOK_*.md`,
  [`ZATNIKTEL_INCAPACITATION.md`](ZATNIKTEL_INCAPACITATION.md) et
  [`NON_LETHAL_CAPTURE_TOOLS.md`](NON_LETHAL_CAPTURE_TOOLS.md) ;
- diagnostics et présentation : [`DEBUG_UI_AUDIT.md`](DEBUG_UI_AUDIT.md),
  [`LOGGING.md`](LOGGING.md), [`LOCALIZATION.md`](LOCALIZATION.md) et
  [`DURATION_FORMATTING.md`](DURATION_FORMATTING.md).

Une fiche technique publiée peut rester sans lien direct depuis le code si elle
porte encore un contrat d'architecture ou de test. Son nom doit toutefois
désigner un sous-système stable, pas seulement une révision locale.

## Wiki joueur

Les brouillons sous [`wiki/`](wiki/) sont la source de vérité du dépôt wiki
séparé. Ils sont destinés au joueur et ne remplacent pas les documents
techniques. `wiki/Content-Status.md` est l'unique état public détaillé du
contenu ; aucun second `Content-Status.md` ne doit être créé à la racine de
`docs/`.

Toute création, suppression ou renommage d'une page doit être répercutée dans la
navigation, les liens entrants et le dépôt wiki séparé lors de la publication.
Une réorganisation interne n'impose pas de nouvelle page joueur, mais les
métadonnées de version et les résumés publics des directions futures doivent
rester cohérents avec la version publiée.

## Ajouter ou retirer un document

Avant de créer un fichier :

1. chercher le document du sous-système concerné ;
2. le mettre à jour lorsque le nouveau contenu prolonge le même contrat ;
3. créer une nouvelle fiche seulement pour un système réellement distinct ;
4. inscrire le travail futur décidé dans la roadmap et les idées spéculatives
   dans `IDEAS_TO_REVISIT.md`.

Avant de supprimer un fichier :

1. transférer toute décision encore active ;
2. corriger les liens et les contrôles automatisés ;
3. supprimer également le brouillon wiki devenu faux, le cas échéant ;
4. laisser Git et les tags conserver l'ancien contenu au lieu de créer un
   dossier `archive/`.

## Consolidations

Le jalon `0.3.63-dev` a publié la première consolidation documentaire et retiré
plusieurs anciens plans absorbés par la structure actuelle.

Le jalon `0.3.76-dev` réconcilie ensuite le backlog avec les fonctionnalités
réellement publiées et formalise les futurs jalons individuels. Il ne remplace
pas la seconde consolidation documentaire et des tests, qui reste un futur
jalon distinct fondé sur la comparaison des contrats et des régressions.
