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

## Références techniques durables

Les documents de sous-système décrivent les contrats encore utiles au code et
aux régressions. Les principales portes d'entrée sont :

- culture et identités : [`CULTURAL_FRAMEWORK.md`](CULTURAL_FRAMEWORK.md),
  [`CULTURAL_BACKSTORIES.md`](CULTURAL_BACKSTORIES.md) et
  [`TOKRA_DUAL_IDENTITY_DESIGN.md`](TOKRA_DUAL_IDENTITY_DESIGN.md) ;
- missions : [`MISSION_FRAMEWORK.md`](MISSION_FRAMEWORK.md) ;
- storyteller et orchestration :
  [`STORYTELLER_SG1.md`](STORYTELLER_SG1.md) ;
- direction Goa'uld : [`GOAULD_GAMEPLAY_DIRECTION.md`](GOAULD_GAMEPLAY_DIRECTION.md),
  [`GOAULD_OPEN_CONFLICT_BATTLEFIELD.md`](GOAULD_OPEN_CONFLICT_BATTLEFIELD.md),
  [`GOAULD_HOST.md`](GOAULD_HOST.md) et [`GOAULD_KARA_KESH.md`](GOAULD_KARA_KESH.md) ;
- Jaffa et Prim'ta : fichiers `JAFFA_*.md` et `PRIMTA_*.md` ;
- Tok'ra : fichiers `TOKRA_*.md`, classés par système ou opération ;
- équipement : fichiers `SG_*.md`, `MATOK_*.md`,
  [`ZATNIKTEL_INCAPACITATION.md`](ZATNIKTEL_INCAPACITATION.md) et
  [`NON_LETHAL_CAPTURE_TOOLS.md`](NON_LETHAL_CAPTURE_TOOLS.md) ;
- diagnostics : [`DEBUG_UI_AUDIT.md`](DEBUG_UI_AUDIT.md),
  [`LOGGING.md`](LOGGING.md) et [`LOCALIZATION.md`](LOCALIZATION.md).

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

## Première consolidation

Le jalon `0.3.63-dev` retire les anciens plans désormais absorbés par cette
structure, le backlog ou les idées à revoir. Il ne consolide pas encore
`TESTING.md` ni les familles techniques détaillées : ces travaux exigent une
seconde passe avec comparaison des contrats et des régressions avant toute
suppression. La première consolidation est validée et publiée sous le tag
`v0.3.63-dev`.
