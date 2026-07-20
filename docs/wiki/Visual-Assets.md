# Références visuelles validées

> Version de référence : `0.3.103-dev`
> Statut : armes, projectiles, couteau et équipements Jaffa non directionnels validés jusqu'à `0.3.103-dev`

Cette page rassemble les références visuelles explicitement acceptées. Une copie
placée sous `docs/wiki/images/` doit rester byte-identique au PNG utilisé par le
jeu. Un visuel fonctionnel n'est pas automatiquement définitif : son passage à
`final` exige toujours une validation explicite.

## Armes et projectiles non directionnels validés

| Arme | Projectile | Référence |
|---|---|---|
| <img src="images/SG1_JaffaKnife.png" width="160" alt="Couteau Jaffa"> | — | Lame Jaffa fourchue, contour sombre renforcé, PNG transparent `128×128` et `drawSize = 0.65` |
| <img src="images/SG1_Bolas.png" width="128" alt="Bolas"> | <img src="images/SG1_BolasProjectile.png" width="128" alt="Projectile des bolas"> | Bolas réutilisables, corde et poids lisibles à l'échelle RimWorld |
| <img src="images/SG1_MatokStaff.png" width="160" alt="Bâton Ma'Tok"> | <img src="images/SG1_MatokBlast.png" width="128" alt="Projectile Ma'Tok"> | Longue arme Jaffa et bolt énergétique compact, vitesse `80` |
| <img src="images/SG1_ZatnikTel.png" width="160" alt="Zat'nik'tel"> | <img src="images/SG1_ZatnikTelBlast.png" width="128" alt="Décharge du Zat'nik'tel"> | Arme Goa'uld compacte et décharge distincte |
| Fusil hypodermique Tok'ra déjà référencé ci-dessous | <img src="images/SG1_TokraHypodermicDart.png" width="128" alt="Projectile hypodermique Tok'ra"> | Impulsion extraterrestre de neutralisation, pas une seringue conventionnelle |

## Équipements Jaffa non directionnels validés

![Concept art de la tenue Jaffa](images/Jaffa-Armor-Concept.png)

Le concept art ci-dessus sert de direction artistique générale. Il n'est pas une
texture de gameplay et ne représente pas le rendu directionnel final des pawns.

| Pièce standard | Visuel |
|---|---|
| Armure légère | <img src="images/JaffaLightArmor.png" width="128" alt="Armure légère Jaffa"> |
| Armure lourde | <img src="images/JaffaHeavyArmor.png" width="128" alt="Armure lourde Jaffa"> |
| Casque déployé | <img src="images/JaffaDeployedHelmet.png" width="128" alt="Casque Jaffa déployé"> |
| Gantelets | <img src="images/JaffaGauntlets.png" width="128" alt="Gantelets Jaffa"> |
| Bottes renforcées | <img src="images/JaffaReinforcedBoots.png" width="128" alt="Bottes renforcées Jaffa"> |
| Sous-armure textile | <img src="images/JaffaUnderArmor.png" width="128" alt="Sous-armure textile Jaffa"> |
| Pantalon | <img src="images/JaffaPants.png" width="128" alt="Pantalon Jaffa"> |
| Ceinture d'armure | <img src="images/JaffaArmorBelt.png" width="128" alt="Ceinture d'armure Jaffa"> |

| Pièce d'officier | Visuel |
|---|---|
| Armure d'officier | <img src="images/JaffaOfficerArmor.png" width="128" alt="Armure d'officier Jaffa"> |
| Casque d'officier déployé | <img src="images/JaffaOfficerDeployedHelmet.png" width="128" alt="Casque d'officier Jaffa déployé"> |

Ces images sont les surfaces au sol et en inventaire validées. Une famille
comportant encore des textures portées directionnelles provisoires reste
techniquement classée temporaire dans le registre global jusqu'à la finalisation
de toutes ses orientations.

## Consommable médical Goa'uld validé

| Visuel | Objet | Def | Chemin sous `Textures/` | Référence validée |
|---|---|---|---|---|
| ![Dose finale de trétonine](images/SG1_TretoninDose.png) | Dose individuelle de trétonine | `SG1_TretoninDose` | `Things/Item/SG1_TretoninDose` | Ampoule transparente `128×128`, liquide cyan, contour sombre renforcé et petite échelle contrôlée par `drawSize` |

La texture reste cadrée serrée afin de préserver sa définition. La production,
l'empilement, l'administration médicale et la substitution temporaire restent
inchangés.

## Identité publique du mod

| Usage | Chemin | Statut |
|---|---|---|
| Icône publique GateRim SG-1 | `About/ModIcon.png` | Final ; conserver l'image personnelle démon rouge/noir et ne pas la réutiliser comme art de gameplay |

## Dispositifs de main Goa'uld validés

| Visuel | Équipement | Def | Chemin sous `Textures/` | Référence validée |
|---|---|---|---|---|
| ![Kara kesh final](images/KaraKesh.png) | Kara kesh des Grands Maîtres | `SG1_KaraKesh` | `Things/Pawn/Humanlike/Apparel/KaraKesh/KaraKesh` | Gant articulé bronze et or, sangles de cuir et gemme de contrôle rouge ; PNG transparent `128×128` validé au sol, dans l'inventaire et équipé |
| ![Bracelet de guérison Goa'uld final](images/GoauldHealingBracelet.png) | Bracelet de guérison Goa'uld | `SG1_GoauldHealingBracelet` | `Things/Pawn/Humanlike/Apparel/GoauldHealingBracelet/GoauldHealingBracelet` | Bracelet circulaire or et argent autour d'un noyau orange lumineux ; PNG transparent `128×128` validé au sol, dans l'inventaire et équipé |

Les deux Defs restent des apparel et conservent donc leurs chemins historiques
sous `Things/Pawn/Humanlike/Apparel`. Ce jalon remplace uniquement les images
d'objet ; il n'ajoute aucun rendu directionnel porté ni changement de gameplay.

## Bâtiments Goa'uld et Prim'ta validés

| Visuel | Bâtiment | Def | Chemin sous `Textures/` | Référence validée |
|---|---|---|---|---|
| ![Bassin rituel Goa'uld](images/SG1_GoauldRitualBasin.png) | Bassin rituel Goa'uld `2×2` ou `3×3` | `SG1_GoauldRitualBasin`, `SG1_GoauldRitualBasinLarge` | `Things/Building/SG1_GoauldRitualBasin` | Plateforme cérémonielle Goa'uld dorée et sombre, bassin central vert et quatre pylônes bleus ; même rendu visuel `2×2` et même icône pour les deux empreintes |
| ![Bassin d'incubation du Prim'ta](images/SG1_PrimtaIncubationBasin.png) | Bassin d'incubation du Prim'ta | `SG1_PrimtaIncubationBasin` | `Things/Building/SG1_PrimtaIncubationBasin` | Dispositif biologique vert sur une case ; image fixe tandis que l'orientation conserve la cellule d'interaction |
| ![Bassin de conservation du Prim'ta](images/SG1_PrimtaPreservationBasin.png) | Bassin de conservation du Prim'ta | `SG1_PrimtaPreservationBasin` | `Things/Building/SG1_PrimtaPreservationBasin` | Stockage biologique bleu sur une case, sans coloration de matériau ni ombre héritée |

Le `defName` historique du bassin rituel reste attribué à la variante `2×2` afin
de préserver les sauvegardes existantes. La variante `3×3` change uniquement
l'empreinte de placement : elle ne grossit pas l'image centrale.

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
| ![Mode du casque Jaffa](images/SG1_JaffaHelmetMode.png) | Bascule manuelle du casque Jaffa | `UI/Commands/SG1_JaffaHelmetMode` | Casque cobra brun et or entouré de deux flèches de déploiement et rétraction |

Les cinq PNG sont transparents et conservent leur format gameplay `64×64`.
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
| ![Champ de bataille Goa'uld](images/SG1_GoauldOpenConflictBattlefield.png) | Champ de bataille Goa'uld | `SG1_GoauldOpenConflictBattlefieldSite` | `World/WorldObjects/Expanding/Sites/SG1_GoauldOpenConflictBattlefield` | Icône plate `128×128`, deux armes opposées, impact central orange, détails limités et sans pseudo-3D |
| ![Relais Goa'uld à saboter](images/SG1_GoauldRelaySabotage.png) | Relais Goa'uld à saboter | `SG1_TokraDecodedMissionWorldSite` | `World/WorldObjects/Expanding/Sites/SG1_GoauldRelaySabotage` |
| ![Position de l'officier Jaffa](images/SG1_JaffaOfficerFieldPosition.png) | Position de l'officier Jaffa | `SG1_TokraJaffaOfficerCaptureSite` | `World/WorldObjects/Expanding/Sites/SG1_JaffaOfficerFieldPosition` |
| ![Contact clandestin Tok'ra](images/SG1_TokraClandestineContact.png) | Contact clandestin Tok'ra | `SG1_TokraHiddenSafehouseMarker`, `SG1_TokraHiddenSafehouseSitePart` | `World/WorldObjects/Expanding/Sites/SG1_TokraClandestineContact` |
| ![Signal de détresse Tok'ra](images/SG1_TokraDistressSignal.png) | Signal de détresse Tok'ra | `SG1_TokraDistressCallWorldSite` | `World/WorldObjects/Expanding/Sites/SG1_TokraDistressSignal` |
| ![Rendez-vous logistique Tok'ra](images/SG1_TokraLogisticsRendezvous.png) | Rendez-vous logistique Tok'ra | `SG1_TokraTemporaryBaseDeliverySite` | `World/WorldObjects/Expanding/Sites/SG1_TokraLogisticsRendezvous` |

## Arme Tok'ra de capture validée

| Visuel | Objet | Def | Chemin sous `Textures/` | Référence validée |
|---|---|---|---|---|
| ![Fusil hypodermique expérimental Tok'ra](images/SG1_TokraHypodermicRifle.png) | Fusil de neutralisation non létale à douze charges scellées | `SG1_TokraHypodermicRifle` | `Things/Item/Equipment/WeaponRanged/SG1_TokraHypodermicRifle` | Sprite horizontal simplifié, contour fort, modules cyan et canon conventionnel |

La légère variation de pose au sol reste celle des armes vanilla. La fléchette
`SG1_TokraHypodermicDart` demeure une famille projectile temporaire distincte.

## Stades larvaires du Prim'ta validés

| Visuel | Stade | Def | Chemin sous `Textures/` | Référence validée |
|---|---|---|---|---|
| ![Symbiote immature de Prim'ta](images/SG1_ImmaturePrimtaSymbiote.png) | Stade pré-larvaire extrait d'une reine | `SG1_ImmaturePrimtaSymbiote` | `Things/Item/SG1_ImmaturePrimtaSymbiote` | Petit organisme ivoire rosé, compact, recourbé et peu différencié |
| ![Larve de Prim'ta](images/SG1_PrimtaLarva.png) | Larve incubée et implantable | `SG1_PrimtaLarva` | `Things/Item/SG1_PrimtaLarva` | Organisme plus long, segmenté et développé, encore pâle et non cuirassé |

Ces deux familles concernent uniquement les objets biologiques inertes. Les
symbiotes Goa'uld et Tok'ra mobiles ainsi que la reine Goa'uld restent différés
vers un futur lot de pawns animaliers multidirectionnels.

## Visuels encore temporaires

Les icônes non directionnelles présentées sur cette page sont validées. Restent
notamment temporaires les rendus portés directionnels des armures et vêtements,
les pawns mobiles de symbiotes, ainsi que les autres familles `P1` et `P2`
explicitement conservées dans le registre technique.

Le détail technique, les priorités et les nombres de fichiers restent maintenus
dans `docs/VISUAL_ASSET_REGISTER.md` du dépôt principal.