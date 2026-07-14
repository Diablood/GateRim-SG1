# Références visuelles validées

> Version de référence : `0.3.92-dev`
> Statut : quatre commandes finales validées et publiées dans `0.3.92-dev`

Cette page rassemble les références visuelles explicitement acceptées. Une copie
placée sous `docs/wiki/images/` doit rester byte-identique au PNG utilisé par le
jeu. Un visuel fonctionnel n'est pas automatiquement définitif : son passage à
`final` exige toujours une validation explicite.

## Identité publique du mod

| Usage | Chemin | Statut |
|---|---|---|
| Icône publique GateRim SG-1 | `About/ModIcon.png` | Final ; conserver l'image personnelle démon rouge/noir et ne pas la réutiliser comme art de gameplay |

## Portraits de storyteller validés

| Portrait | Storyteller | Def | Chemins sous `Textures/` | Référence visuelle |
|---|---|---|---|---|
| ![Portrait final de Commandement SG-1](images/SG1_Command.png) | Commandement SG-1 | `SG1_GateRimStoryteller` | `Storytellers/SG1_Command`, `Storytellers/SG1_Command_Tiny` | Grand portrait transparent `560×600` et recadrage tiny dédié `122×130`, fournis par le mainteneur puis validés dans l'interface réelle |

## Icônes de xénotypes validées

| Icône | Xénotype | Def | Chemin sous `Textures/` | Référence visuelle |
|---|---|---|---|---|
| ![Icône finale du xénotype Jaffa](images/SG1_Jaffa.png) | Jaffa | `SG1_Jaffa` | `UI/Xenotypes/SG1_Jaffa` | Tête humaine blanche très épurée portant la marque d'Apophis |
| ![Icône finale de l'hôte Goa'uld](images/SG1_GoauldHost.png) | Hôte Goa'uld | `SG1_GoauldHost` | `UI/Xenotypes/SG1_GoauldHost` | Symbiote Goa'uld blanc simplifié qui représente l'état acquis |

## Icônes de gènes validées

| Icône | Gène | Chemin sous `Textures/` | Lecture visuelle |
|---|---|---|---|
| ![Lignée jaffa](images/SG1_JaffaLineage.png) | `SG1_JaffaLineage` | `UI/Genes/SG1_JaffaLineage` | Visage Jaffa adulte et descendant marqué, pour la transmission de la lignée |
| ![Physiologie jaffa](images/SG1_JaffaPhysiology.png) | `SG1_JaffaPhysiology` | `UI/Genes/SG1_JaffaPhysiology` | Caisse et flèche ascendante verte pour le bonus de capacité de transport |
| ![Prédisposition à la poche jaffa](images/SG1_JaffaPouchPotential.png) | `SG1_JaffaPouchPotential` | `UI/Genes/SG1_JaffaPouchPotential` | Corps vanilla simplifié avec deux incisions abdominales croisées à 45 degrés |
| ![Compatibilité avec un symbiote immature](images/SG1_JaffaSymbioteCompatibility.png) | `SG1_JaffaSymbioteCompatibility` | `UI/Genes/SG1_JaffaSymbioteCompatibility` | Larve Goa'uld brune, collerette et quatre dents, dans un cercle vert de compatibilité |
| ![Longévité de l'hôte Goa'uld](images/SG1_GoauldLongevity.png) | `SG1_GoauldLongevity` | `UI/Genes/SG1_GoauldLongevity` | Sablier entouré de deux flèches cycliques |
| ![Naquadah dans le sang](images/SG1_NaquadahBlood.png) | `SG1_NaquadahBlood` | `UI/Genes/SG1_NaquadahBlood` | Goutte de sang rouge avec reflet vert fluorescent rappelant une fiole de naquadah |

Le prototype obsolète `SG1_JaffaLongevity` n'est plus utilisé : son `GeneDef`,
sa traduction et son PNG dédié ont été supprimés en `0.3.89-dev`. La longévité
des Jaffa reste fournie par l'état de santé du Prim'ta. Les trois anciens gènes
techniques de marques frontales sont également supprimés en `0.3.90-dev`; les
marques réellement utilisées restent des données intrinsèques du pion.

## Marques frontales Jaffa intrinsèques validées

| Marque visible | Rang | Def intrinsèque | Famille sous `Textures/` | Rendu |
|---|---|---|---|---|
| ![Marque frontale Jaffa noire](images/GenericJaffaForeheadMark_south.png) | Jaffa ordinaire | `SG1_JaffaForeheadMark_GenericIntrinsic` | `Things/Pawn/Humanlike/JaffaForeheadMarks/GenericJaffaForeheadMark` | Petit symbole d'Apophis noir, visible uniquement en `South` |
| ![Marque frontale Jaffa argentée](images/GenericSilverJaffaForeheadMark_south.png) | Élite sélectionnée | `SG1_JaffaForeheadMark_GenericSilverIntrinsic` | `Things/Pawn/Humanlike/JaffaForeheadMarks/GenericSilverJaffaForeheadMark` | Même géométrie argentée, visible uniquement en `South` |
| ![Marque frontale Jaffa dorée](images/GenericGoldJaffaForeheadMark_south.png) | Premier Primat | `SG1_JaffaForeheadMark_GenericGoldIntrinsic` | `Things/Pawn/Humanlike/JaffaForeheadMarks/GenericGoldJaffaForeheadMark` | Même géométrie dorée, visible uniquement en `South` |

Chaque famille conserve quatre PNG `128×128`. Les fichiers `North`, `East` et
`West` sont entièrement transparents afin que la marque reste un tatouage
strictement frontal, sans texture flottante sur les autres orientations.

## Icônes de commandes Goa'uld et Jaffa validées

| Icône | Commande ou famille | Chemin sous `Textures/` | Lecture visuelle |
|---|---|---|---|
| ![Chasse autonome](images/SG1_AutonomousHunt.png) | Chasse autonome du symbiote | `UI/Commands/SG1_AutonomousHunt` | Symbiote brun en mouvement vers un réticule rouge |
| ![Extraction d'urgence](images/SG1_EmergencyExtraction.png) | Extraction instantanée développeur | `UI/Commands/SG1_EmergencyExtraction` | Symbiote séparé d'un hôte allongé sous une lampe chirurgicale |
| ![Implantation forcée](images/SG1_ForcedImplantation.png) | Implantation forcée adjacente | `UI/Commands/SG1_ForcedImplantation` | Symbiote frappant vers un hôte par une courte flèche rouge |
| ![Implantation rituelle](images/SG1_RitualImplantation.png) | Famille d'implantation et de cérémonie partagée | `UI/Commands/SG1_RitualImplantation` | Symbiote et hôte devant un sceau rituel doré |

Les quatre PNG sont transparents et conservent leur format gameplay `64×64`.
La famille rituelle reste partagée par les surfaces Goa'uld, Tok'ra et la
cérémonie formelle du Prim'ta, sans changement de logique.

## Icônes de factions mondiales validées

| Icône | Faction | Def principal | Chemin sous `Textures/` |
|---|---|---|---|
| ![Icône des Jaffa libres](images/SG1_FreeJaffa.png) | Jaffa libres | `SG1_FreeJaffa` | `World/WorldObjects/Expanding/SG1_FreeJaffa` |
| ![Icône des domaines Goa'uld](images/SG1_GoauldSystemLords.png) | Domaines des Grands Maîtres Goa'uld | `SG1_GoauldSystemLordPrototype` | `World/WorldObjects/Expanding/SG1_GoauldSystemLords` |
| ![Icône de l'expédition SGC](images/SG1_SGCExpedition.png) | Expédition SGC | `SG1_PlayerSGCExpedition` | `World/WorldObjects/Expanding/SG1_SGCExpedition` |
| ![Icône de la cellule Tok'ra](images/SG1_Tokra.png) | Tok'ra | `SG1_Tokra` | `World/WorldObjects/Expanding/SG1_Tokra` |

## Icônes de sites d'événements validées

| Icône | Événement ou site | Def ou usage principal | Chemin sous `Textures/` |
|---|---|---|---|
| ![Objectif Goa'uld chiffré](images/SG1_GoauldEncryptedObjective.png) | Objectif Goa'uld chiffré | `SG1_TokraIntroductionArtifactWorldSite` | `World/WorldObjects/Expanding/Sites/SG1_GoauldEncryptedObjective` |
| ![Champ de bataille Goa'uld](images/SG1_GoauldOpenConflictBattlefield.png) | Champ de bataille Goa'uld | `SG1_GoauldOpenConflictBattlefieldSite` | `World/WorldObjects/Expanding/Sites/SG1_GoauldOpenConflictBattlefield` |
| ![Relais Goa'uld à saboter](images/SG1_GoauldRelaySabotage.png) | Relais Goa'uld à saboter | `SG1_TokraDecodedMissionWorldSite` | `World/WorldObjects/Expanding/Sites/SG1_GoauldRelaySabotage` |
| ![Position de l'officier Jaffa](images/SG1_JaffaOfficerFieldPosition.png) | Position de l'officier Jaffa | `SG1_TokraJaffaOfficerCaptureSite` | `World/WorldObjects/Expanding/Sites/SG1_JaffaOfficerFieldPosition` |
| ![Contact clandestin Tok'ra](images/SG1_TokraClandestineContact.png) | Contact clandestin Tok'ra | `SG1_TokraHiddenSafehouseMarker`, `SG1_TokraHiddenSafehouseSitePart` | `World/WorldObjects/Expanding/Sites/SG1_TokraClandestineContact` |
| ![Signal de détresse Tok'ra](images/SG1_TokraDistressSignal.png) | Signal de détresse Tok'ra | `SG1_TokraDistressCallWorldSite` | `World/WorldObjects/Expanding/Sites/SG1_TokraDistressSignal` |
| ![Rendez-vous logistique Tok'ra](images/SG1_TokraLogisticsRendezvous.png) | Rendez-vous logistique Tok'ra | `SG1_TokraTemporaryBaseDeliverySite` | `World/WorldObjects/Expanding/Sites/SG1_TokraLogisticsRendezvous` |

## Visuels encore temporaires

Les autres visuels locaux restent temporaires, notamment les armes,
équipements, vêtements, bâtiments, objets de mission, projectiles et pawns.
Les trois familles de marques frontales intrinsèques et les quatre commandes
présentées ci-dessus sont désormais finales.

Le détail technique, les priorités et les nombres de fichiers restent maintenus
dans `docs/VISUAL_ASSET_REGISTER.md` du dépôt principal.
