# Validation ciblée — 0.3.43-dev

Jalon : `0.3.43-dev - Add Free Jaffa trade network`

Branche : `feature/free-jaffa-trade-network`

Tag de départ : `v0.3.42-dev`

Version de DLL validée : `0.3.43.0`

Révision locale finale validée : `r5`

Statut : validation terminée, publication sous `v0.3.43-dev`.

## 1. Contrôles statiques et chargement

- [x] `git diff --check` ne signale aucune erreur.
- [x] `./tools/check-project-consistency.cmd` termine avec un code `0`.
- [x] La DLL existante reste en version `0.3.43.0` ; aucun rebuild n'est requis pour cette révision XML et documentaire.
- [x] Aucun `Could not resolve cross-reference` ne concerne `SG1_Caravan_FreeJaffaClanSupplies`, ses objets ou ses catégories.
- [x] Aucun message de configuration ne signale qu'un objet du stock ne peut pas être vendu par le marchand.
- [x] `SG1_JaffaLightArmor`, `SG1_JaffaHeavyArmor`, `SG1_JaffaGauntlets`, `SG1_JaffaReinforcedBoots` et `SG1_JaffaDeployedHelmet` peuvent être générés dans le stock du convoi.
- [x] `SG1_JaffaRetractedHelmet` reste interne et n'apparaît jamais dans le commerce.
- [x] Aucun message `not a trader but is in a traders list` ne concerne `SG1_FreeJaffaTrader`.

## 2. Génération du convoi spécialisé

- [x] Une arrivée commerciale Jaffa libre utilise le type `convoi de ravitaillement des clans libres`.
- [x] Le groupe contient un `SG1_FreeJaffaTrader`, des gardes Jaffa libres et des animaux de bât.
- [x] Le marchand est reconnu comme interlocuteur et la fenêtre de commerce s'ouvre.
- [x] Générer au moins cinq stocks permet d'observer une variation réelle sans changer l'identité générale du convoi.
- [x] Le groupe quitte normalement la carte après la visite.

## 3. Stock vendu par le convoi

Vérifier sur plusieurs générations la présence cohérente de :

- [x] repas de survie et/ou pemmican ;
- [x] phytomédicaments et médicaments industriels ;
- [x] acier, plasteel, composants industriels, chemfuel et tissu en quantités modérées ;
- [x] composants avancés seulement de manière occasionnelle ;
- [x] quelques armes humaines industrielles à distance et de mêlée ;
- [x] quelques protections militaires ;
- [x] zéro à deux bâtons Ma'Tok ;
- [x] équipement Jaffa en pièces limitées et variables ;
- [x] Zat'nik'tel absent de la majorité des stocks et limité à un exemplaire lorsqu'il apparaît.

## 4. Catégories volontairement absentes

- [x] Aucun assortiment générique de meubles, œuvres d'art ou objets de loisir n'est ajouté.
- [x] Aucun stock d'animaux à vendre n'est ajouté ; les animaux présents servent uniquement de porteurs.
- [x] Aucun catalogue générique de drogues récréatives n'est ajouté.
- [x] Le convoi ne devient pas un marchand de biens exotiques, d'implants ou de technologies ultrarares.

## 5. Achat d'équipement au joueur et limite économique

Préparer plusieurs armes et protections humaines de valeurs différentes :

- [x] le convoi accepte les armes à distance industrielles ;
- [x] le convoi accepte les armes de mêlée ;
- [x] le convoi accepte les armures et casques militaires ;
- [x] le convoi accepte les équipements Jaffa et Goa'uld explicitement présents dans son profil ;
- [x] la réserve d'argent générée se situe approximativement entre `850` et `1300` ;
- [x] un lot important d'équipement ne peut pas être intégralement vendu une fois la réserve épuisée ;
- [x] une arme coûteuse peut absorber une part importante du budget sans créer d'argent illimité.

## 6. Identité du marchand

- [x] Le marchand possède le xénotype Jaffa et un Prim'ta initial.
- [x] Son nom et ses backstories appartiennent aux profils Jaffa libres.
- [x] Il porte un bâton Ma'Tok, une armure légère, des gantelets et des bottes renforcées.
- [x] Il ne reçoit pas automatiquement de marque frontale Goa'uld.
- [x] L'absence de casque permet de le distinguer des gardes.

## 7. Autres flux commerciaux et limites de faction

- [x] Une caravane du joueur peut toujours commercer avec une colonie Jaffa libre.
- [x] Le visiteur marchand vanilla peut toujours utiliser la faction Jaffa libre.
- [x] Une demande de marchand par console respecte encore les relations, délais et coûts vanilla.
- [x] Une faction Jaffa libre hostile n'est pas proposée comme partenaire pacifique.
- [x] L'aide militaire, les sites de quête, les raids naturels, les sièges et les attaques préparées restent désactivés.
- [x] Les visiteurs pacifiques non marchands de `0.2.6-dev` restent inchangés.

## 8. Persistance et régressions

- [x] Sauvegarder et recharger avec un convoi présent conserve le marchand, le stock, les gardes et les porteurs.
- [x] Le commerce reste possible après rechargement.
- [x] Les colonies, relations, visiteurs pacifiques, Jaffa Goa'uld, Tok'ra et opérations existantes ne présentent pas de régression évidente.
- [x] `Player.log` ne contient aucune nouvelle erreur GateRim SG-1.

## Résultat final

La révision `r5` remplace le grossiste générique par un convoi de
ravitaillement Jaffa libre identifiable et utile, capable d'acheter du matériel
militaire humain tout en restant limité par son argent. Elle ne devient pas
un marchand universel destiné à couvrir par avance les rôles économiques des
futures factions GateRim SG-1.
