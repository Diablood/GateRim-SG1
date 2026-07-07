# Références visuelles validées

> Version de référence : `0.3.88-dev`
> Statut : portraits validés en `r1` ; finalisation documentaire `r2`

Cette page évite de reporter toutes les références visuelles à une future passe
globale du wiki. Lorsqu'un visuel devient définitif, sa référence doit être
ajoutée ici dans la même révision que sa validation, avec son Def ou son usage et
son chemin stable.

Un visuel fonctionnel, publié ou utilisé depuis longtemps n'est pas
automatiquement définitif. En l'absence d'une validation explicite, il reste
classé comme temporaire dans le registre technique.

## Identité publique du mod

| Usage | Chemin | Statut |
|---|---|---|
| Icône publique GateRim SG-1 | `About/ModIcon.png` | Final ; conserver l'image personnelle démon rouge/noir et ne pas la réutiliser comme art de gameplay |

## Portraits de storyteller validés

| Portrait | Storyteller | Def | Chemins sous `Textures/` | Référence visuelle |
|---|---|---|---|---|
| ![Portrait final de Commandement SG-1](images/SG1_Command.png) | Commandement SG-1 | `SG1_GateRimStoryteller` | `Storytellers/SG1_Command`, `Storytellers/SG1_Command_Tiny` | Grand portrait transparent `560×600` et recadrage tiny dédié `122×130`, fournis par le mainteneur puis validés dans l'interface réelle |

Les deux chemins restent stables. Le remplacement n'altère ni le Def, ni les
textes, ni la cadence Cassandra, ni l'orchestration stratégique du storyteller.

## Icônes de xénotypes validées

| Icône | Xénotype | Def | Chemin sous `Textures/` | Référence visuelle |
|---|---|---|---|---|
| ![Icône finale du xénotype Jaffa](images/SG1_Jaffa.png) | Jaffa | `SG1_Jaffa` | `UI/Xenotypes/SG1_Jaffa` | Tête humaine blanche très épurée portant la marque d'Apophis ; PNG `64×64` fourni et approuvé par le mainteneur |
| ![Icône finale de l'hôte Goa'uld](images/SG1_GoauldHost.png) | Hôte Goa'uld | `SG1_GoauldHost` | `UI/Xenotypes/SG1_GoauldHost` | Symbiote Goa'uld blanc simplifié, en forme de S, avec œil et mâchoire lisibles ; visuel approuvé avant intégration |

Le choix du symbiote pour l'hôte Goa'uld est volontaire : la présence de cet
organisme définit l'état acquis, tandis qu'un ancien hôte privé de son symbiote
redevient un humain ordinaire. Cette lecture évite aussi de confondre l'hôte avec
un Jaffa marqué.

## Icônes de sites d'événements validées

| Événement ou site | Def ou usage principal | Chemin sous `Textures/` |
|---|---|---|
| Objectif Goa'uld chiffré | `SG1_TokraIntroductionArtifactWorldSite` | `World/WorldObjects/Expanding/Sites/SG1_GoauldEncryptedObjective` |
| Champ de bataille Goa'uld | `SG1_GoauldOpenConflictBattlefieldSite` | `World/WorldObjects/Expanding/Sites/SG1_GoauldOpenConflictBattlefield` |
| Relais Goa'uld à saboter | `SG1_TokraDecodedMissionWorldSite` | `World/WorldObjects/Expanding/Sites/SG1_GoauldRelaySabotage` |
| Position de l'officier Jaffa | `SG1_TokraJaffaOfficerCaptureSite` | `World/WorldObjects/Expanding/Sites/SG1_JaffaOfficerFieldPosition` |
| Contact clandestin Tok'ra | `SG1_TokraHiddenSafehouseMarker`, `SG1_TokraHiddenSafehouseSitePart` | `World/WorldObjects/Expanding/Sites/SG1_TokraClandestineContact` |
| Signal de détresse Tok'ra | `SG1_TokraDistressCallWorldSite` | `World/WorldObjects/Expanding/Sites/SG1_TokraDistressSignal` |
| Rendez-vous logistique Tok'ra | `SG1_TokraTemporaryBaseDeliverySite` | `World/WorldObjects/Expanding/Sites/SG1_TokraLogisticsRendezvous` |

## Visuels encore temporaires

Tous les autres visuels locaux restent temporaires à ce stade, notamment :

- icônes de factions mondiales ;
- armes, équipements et vêtements ;
- bâtiments et objets de mission ;
- icônes de gènes et autres surfaces biologiques ;
- projectiles, pawns et commandes d'interface.

Le détail technique, les priorités et les nombres de fichiers restent maintenus
dans `docs/VISUAL_ASSET_REGISTER.md` du dépôt principal.
