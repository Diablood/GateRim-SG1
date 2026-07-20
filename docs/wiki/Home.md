# GateRim SG-1

Bienvenue dans le wiki joueur français de **GateRim SG-1**, un mod inspiré de
Stargate SG-1 pour RimWorld 1.6.

> Statut du wiki : documentation française active
> Version du mod documentée : `0.3.102-dev`

## Équipements Jaffa non directionnels finalisés

`0.3.102-dev` finalise les icônes au sol et en inventaire des armes, projectiles
et équipements Jaffa concernés. Les armures standard et officier, les casques
déployés, les gantelets, les bottes, la sous-armure, le pantalon et la ceinture
disposent désormais de visuels dédiés et transparents. Les rendus portés
directionnels restent prévus dans un lot ultérieur.
## Gizmo final et contrôle manuel du casque Jaffa

`0.3.101-dev` finalise le gizmo du [casque Jaffa rétractable](Jaffa-Retractable-Helmet)
avec une icône cobra transparente `64×64`. Le mode automatique disparaît : la
commande propose désormais directement `Déployer casque` ou `Rétracter casque`.
La position persiste, l'enrôlement ne la modifie plus et les anciennes
sauvegardes conservent leur position enregistrée avant migration.

## Garde-fous documentaires

`0.3.100-dev` restaure les entrées historiques manquantes et ajoute des
contrôles automatiques sur le changelog, les tests durables, le registre visuel,
la roadmap et l'état final de publication. Cette maintenance ne change aucun
contenu jouable, visuel ou identifiant de sauvegarde.
## Icône du champ de bataille Goa'uld finalisée

`0.3.99-dev` remplace l'ancienne icône floue du
[site mondial de bataille Goa'uld](Content-Status) par une version plate
`128×128`, cohérente avec les autres événements. Deux armes Goa'uld opposées et
un impact orange central signalent immédiatement le conflit ouvert, sans
ombrage pseudo-3D ni détails excessifs. Le fonctionnement du site ne change pas.

## Dose de trétonine finalisée

`0.3.98-dev` donne à la [dose physique de trétonine](Tretonin) une ampoule
médicale transparente `128×128`, remplie d'un liquide cyan et renforcée par un
contour sombre lisible. Le PNG reste cadré serré tandis que la petite taille en
jeu est conservée par le `drawSize` existant. La production, l'empilement,
l'administration et la substitution temporaire ne changent pas.

## Dispositifs de main Goa'uld finalisés

`0.3.94-dev` remplace les deux réutilisations temporaires du visuel du
Zat'nik'tel pour le kara kesh et le bracelet de guérison Goa'uld. Les deux
appareils disposent désormais d'illustrations transparentes `128×128` dédiées,
validées au sol, dans l'inventaire, dans l'inspection et lorsqu'elles sont
équipées.

Le [kara kesh](Kara-Kesh) utilise un gant articulé bronze et or avec une gemme
rouge centrale. Le [bracelet de guérison](Goauld-Healing-Bracelet) utilise un
anneau or et argent autour d'un noyau orange lumineux. Les chemins, Defs,
sauvegardes, recherches, recettes et comportements restent inchangés. Les deux
images figurent dans les [références visuelles validées](Visual-Assets).

## Bassins Goa'uld et Prim'ta finalisés

`0.3.93-dev` remplace les visuels temporaires du bassin rituel Goa'uld, du
bassin d'incubation du Prim'ta et du bassin de conservation du Prim'ta.

Le bassin rituel est proposé dans un même menu en deux empreintes : `2×2` pour
les pièces centrées sur un nombre pair de cases et `3×3` pour les pièces
centrées sur un nombre impair. Les deux variantes utilisent la même image de
taille visuelle `2×2` et la même icône d'interface ; seule l'empreinte de
placement change.

Le bassin d'incubation utilise un rendu biologique vert sur une case et conserve
son orientation fonctionnelle pour la cellule d'interaction. Le bassin de
conservation utilise un rendu bleu distinct sur une case, reste un stockage
spécialisé alimenté et se trouve dans la catégorie Mobilier.

Les trois images validées sont visibles dans les
[références visuelles validées](Visual-Assets).

## Marques frontales Jaffa intrinsèques finales

`0.3.91-dev` remplace les textures temporaires réellement rendues sur les pawns
par un petit symbole d'Apophis validé en jeu. Les Jaffa ordinaires utilisent la
version noire, certaines élites la version argentée et les Premiers Primats la
version dorée.

La marque n'est visible qu'en orientation `South`. Les trois autres textures de
chaque famille sont entièrement transparentes, ce qui évite tout débordement
au-dessus de la tête ou tout symbole flottant sur les côtés. Les données
intrinsèques, l'affectation, la persistance, les outils développeur et le retrait
manuel restent inchangés. Les trois références sont visibles sur la page
[Références visuelles validées](Visual-Assets).

## Nettoyage des anciens gènes de marques frontales Jaffa

`0.3.90-dev` supprime les trois anciens gènes techniques qui servaient seulement
à migrer les premiers prototypes de marques frontales. Leurs traductions,
icônes et logique de conversion sont également retirées : ils ne doivent plus
apparaître dans l'interface des gènes.

Les marques réellement utilisées restent des données intrinsèques du pion,
indépendantes des gènes et des vêtements. Les variantes noire, argentée et dorée,
leur affectation aux Jaffa des domaines Goa'uld, leur rendu et leur persistance
restent inchangés. Seules les anciennes sauvegardes privées contenant encore les
gènes prototypes ne sont plus prises en charge.

## Icônes finales de gènes et références mondiales

`0.3.89-dev` prépare six icônes dédiées pour les véritables gènes de gameplay :
lignée jaffa, physiologie jaffa, prédisposition à la poche, compatibilité avec
un symbiote immature, longévité de l'hôte Goa'uld et naquadah dans le sang.

Le prototype `SG1_JaffaLongevity`, qui n'était plus attribué aux Jaffa, est
supprimé avec sa traduction et son ancien PNG. La longévité Jaffa reste assurée
par le Prim'ta. Les quatre icônes de factions mondiales déjà approuvées sont
également classées comme finales, et la page
[Références visuelles validées](Visual-Assets) affiche désormais les six gènes,
quatre factions et sept sites d'événements.

## Portrait final du storyteller Commandement SG-1

`0.3.88-dev` remplace les deux portraits du storyteller par les PNG transparents
approuvés par le mainteneur. Le portrait principal `560×600` présente l'officier
SG-1 dans l'écran de sélection ; un recadrage dédié `122×130` reste net sur les
petites surfaces de l'interface.

Les deux tailles ont été validées en jeu face à Cassandra, Phoebe et Randy :
aucun étirement, cadrage coupé, fond blanc opaque ou manque de lisibilité n'a été
observé. Le Def, les textes, la cadence Cassandra et l'orchestration stratégique
de **Commandement SG-1** restent inchangés.

## Symbiotes des Jaffa et hôtes Goa'uld/Tok'ra de départ

`0.3.87-dev` corrige la génération vanilla lorsque le joueur choisit un
xénotype GateRim sur la page des personnages de départ. Un Jaffa adulte
éligible affiche désormais son Prim'ta dans le panneau Santé avant validation.
Tout candidat `hôte Goa'uld` reçoit également un véritable symbiote adulte :
une carrière Tok'ra crée l'origine Tok'ra et deux identités distinctes, tandis
qu'une carrière Goa'uld crée l'origine Goa'uld sans commande de changement de
personnalité Tok'ra.

Chaque clic de régénération produit un nouveau pawn complet. Le mod n'effectue
ensuite aucune réimplantation automatique : retirer volontairement le symbiote
avec un mod d'édition avant de lancer la partie reste un choix valide.

## Icônes finales des xénotypes Jaffa et hôte Goa'uld

`0.3.86-dev` ouvre la passe artistique définitive par un lot volontairement
limité à deux icônes simples. Le Jaffa utilise désormais une tête humaine blanche
très épurée portant la marque d'Apophis, fournie directement par le mainteneur.
L'hôte Goa'uld est représenté par le symbiote blanc simplifié qui définit cet
état acquis, plutôt que par une marque frontale pouvant être confondue avec celle
d'un Jaffa.

Les deux visuels ont été approuvés avant leur inclusion dans `r1`, puis leur
rendu réel en petite taille et les contrôles techniques ont été validés. La
révision `r2` les ajoute visuellement aux pages dédiées et protège les copies du
wiki contre toute divergence avec les PNG du jeu.

## Audit des assets visuels provisoires

La version publiée `0.3.85-dev`, révision finale `r3`, a créé le registre
technique exhaustif. Après les validations successives, la baseline compte
`606` PNG sous `73` familles de textures, dont `33` familles locales finales :
deux portraits de storyteller, deux xénotypes, six gènes de gameplay, trois
marques frontales intrinsèques, quatre commandes, trois bâtiments, deux
dispositifs de main, quatre factions mondiales et sept sites d'événements.
L'icône publique du mod reste finale hors compteur.

Chaque nouveau visuel validé est ajouté immédiatement à la page
[Références visuelles validées](Visual-Assets), sans attendre une passe globale
du wiki.

## Première prise territoriale Goa'uld bornée

Après les garde-fous publiés en `0.3.83-dev`, la version publiée
`0.3.84-dev` permet à une paire exacte en conflit ouvert de réserver une
colonie Goa'uld existante. Les tentatives naturelles restent rares, tous les
`45–90` jours sous **Commandement SG-1**, puis la résolution attend `1–2` jours.

Seule la faction propriétaire change. La colonie conserve son nom, son
identifiant et sa tuile ; aucune colonie ou faction n'est créée ou détruite. La
dernière colonie d'un domaine, les mondes sous le seuil `domaines + 2`, les
acquisitions dépassant `75 %` avec deux domaines ou `50 %` à partir de trois,
les cartes chargées, la présence du joueur et les cibles de quête actives sont
protégées.

Les états GateRim restent réconciliés avec les relations vanilla entre les mêmes
domaines : conflit ouvert devient hostile, alliance devient alliée, et
neutralité, rivalité ou trêve restent neutres. La prise ne modifie ni cette
relation, ni la bonne volonté envers le joueur ou les factions extérieures.

## Rupture d'alliance après un échec majeur

`0.3.82-dev` ajoute une conséquence diplomatique bornée à l'échec d'une
représaille commune naturelle. Si au moins six Jaffa combinés tombent à `20 %`
ou moins de combattants actifs, la paire exacte rompt son alliance après `1–2`
jours et passe à la rivalité. Une seule rupture peut être en attente, elle est
annulée si la paire n'est plus alliée et elle ne produit ni raid supplémentaire,
ni changement de territoire ou de bonne volonté envers le joueur.

## Représailles communes Goa'uld

`0.3.81-dev` permet à une paire de domaines alliés de préparer une réaction
commune après la défaite décisive d'un raid naturel standard. La réponse reste
rare, attend `2–4` jours et utilise un budget réduit partagé entre deux
détachements arrivant depuis des côtés opposés. La lettre d'annonce ne pointe
vers aucun lieu inexistant ; la lettre d'arrivée nomme les deux domaines et
permet d'identifier les deux forces. La révision finale `r2` est validée.

## Doctrines de raid influencées par les relations

`0.3.80-dev` permet aux relations Goa'uld d'influencer légèrement les
doctrines déjà admissibles des raids naturels sous Commandement SG-1 : alliance
favorise l'assaut direct, rivalité l'enlèvement et conflit ouvert la destruction,
avec un multiplicateur `x1,25` non cumulable et sans modifier les seuils, les
points ou la fréquence.

## Renforts Goa'uld alliés

`0.3.78-dev` permet à un raid naturel bénéficiant d'une alliance sous
**Commandement SG-1** de partager son budget avec une vague d'un second domaine.
Les renforts arrivent à pied après un court délai caché : aucune alerte ne les
annonce et leur lettre RP apparaît seulement au moment de leur entrée. Les deux
couleurs de faction restent visibles sans ajouter de points de menace gratuits.

## Priorité du développement

`0.3.77-dev` fixe une barrière de phase : le socle Tok'ra, Goa'uld/Jaffa et
Tau'ri/SGC doit être explicitement clos avant le lancement des Asgard, Nox,
Unas, Réplicateurs ou du préréglage de monde entièrement GateRim SG-1. Les
futurs peuples réutiliseront les systèmes culturels, de factions, de missions,
de menace, de sauvegarde et de tests déjà construits au lieu de repartir sur
des fondations parallèles. Aucun gameplay n'est modifié par ce jalon
documentaire.

## Réconciliation de la feuille de route

`0.3.76-dev` ne modifie pas le gameplay. Cette révision retire des travaux déjà
publiés de la liste des développements futurs, sépare les prochains chantiers et
réserve une passe artistique définitive pour remplacer tous les placeholders et
textures temporaires. Les anneaux de transport sont désormais planifiés comme
une future alternative thématique et limitée aux pods vanilla.


## Officiers Jaffa Goa'uld

La révision `0.3.74-dev` donne à la cible de
[l'opération de capture](Tokra-Jaffa-Officer-Capture) une armure lourde et un
casque rétractable rouges. Depuis `0.3.75-dev`, un groupe Goa'uld d'au moins cinq
Jaffa peut aussi remplacer un garde par un seul officier dans les raids naturels,
les défenses de colonies et plusieurs missions. Aucun pawn ni budget de menace
supplémentaire n'est ajouté. La révision finale `r2` est validée et publiée.

## Durées affichées

Le formatage localisé validé dans `0.3.71-dev` reste inchangé. Le correctif
`0.3.72-dev` restaure uniquement les sources documentaires omises lors de cette
publication et aligne à nouveau les métadonnées du dépôt. Les sites, offres,
statuts Tok'ra, refroidissements du communicateur et comptes à rebours utilisent
toujours le format temporel de RimWorld sans modifier leurs échéances. Voir
[Formatage des durées](Duration-Formatting).

## Le mod en bref

Une équipe SG est isolée sur un RimWorld inconnu. La Terre est hors de portée,
la Porte des étoiles reste silencieuse et des puissances extraterrestres ont
déjà marqué ce monde.

GateRim SG-1 construit progressivement une expérience jouable autour du SGC,
des Jaffa, des Goa'uld, des Tok'ra et de leurs technologies. La Porte des
étoiles fonctionnelle et la progression complète hors monde ne sont pas encore
disponibles : la version actuelle se concentre sur les factions, la biologie
des symbiotes, l'équipement, les événements et les opérations de terrain.

## Storyteller optionnel

- [Commandement SG-1](Storyteller-SG1) conserve un rythme classique et fait
  évoluer les relations persistantes entre paires de domaines Goa'uld. Un
  domaine en conflit ouvert consacre `75 %` de ses points habituels à ses raids
  naturels ; sinon, une alliance peut porter ce budget à `110 %`, sans cumul,
  puis produire un raid standard, une vague alliée différée ou un assaut
  conjoint sur les raids admissibles. La lettre de relation n'impose aucun de
  ces résultats.
  Ces conflits peuvent aussi produire une
  [bataille entre domaines](Goauld-Open-Conflict-Battlefields), soit près d'une
  colonie, soit sous la forme d'un site mondial facultatif accessible par
  caravane. Les deux formes partagent un seul emplacement et les autres
  storytellers suspendent les futures opportunités sans modifier leur cadence.

## Contenu actuellement jouable

### Jaffa et Goa'uld

- [Jaffa](Jaffa), [Prim'ta](Primta), trétonine, implantation médicale et
  [cérémonie formelle](Primta-Formal-Ceremony).
- [Bassin d'incubation](Primta-Incubation),
  [bassin de conservation](Primta-Preservation-Basin) et
  [congélation profonde](Primta-Deep-Freezing) des symbiotes immatures.
- [Goa'uld](Goauld), implantation forcée ou rituelle, extraction d'urgence,
  chirurgie d'un [hôte actif](Active-Goauld-Host) capturé et [reine Goa'uld](Goauld-Queen).
- [Incursions de symbiotes Goa'uld libres](Goauld-Free-Symbiote-Incursion) :
  menace biologique rare et adaptative utilisant leur chasse autonome ; un
  symbiote hostile non extrait pendant la phase critique peut ensuite livrer
  son hôte à la faction Goa'uld. Depuis `0.3.41-dev`, l'ancien colon peut encore
  être sauvé après neutralisation, capture et chirurgie risquée.
- [Domaines des Grands Maîtres Goa'uld](Goauld-System-Lord-Faction) et
  [Jaffa libres](Free-Jaffa-Faction), avec des noms combinatoires dédiés pour
  leurs factions et colonies mondiales. Les nouveaux chefs Jaffa libres
  reçoivent directement lors de leur génération un nom personnel et un nom de
  clan culturellement cohérents. Les nouveaux Grands Maîtres Goa'uld reçoivent
  également un nom formel de symbiote avant le choix de la tuile, tandis qu'un
  nom humain distinct reste conservé pour leur hôte. Depuis `0.3.50-dev`, leurs
  lignes de faction utilisent aussi des silhouettes dédiées, teintées par
  RimWorld plutôt que par des couleurs intégrées aux PNG.
  Leurs visiteurs et leur
  [convoi de ravitaillement spécialisé](Free-Jaffa-Trade) fournissent les
  ressources stratégiques et le matériel militaire, ainsi qu'une
  [aide militaire alliée](Free-Jaffa-Military-Aid) demandée par console.
- Armes et équipements : [Ma'Tok](Matok-Staff), [Zat'nik'tel](ZatnikTel),
  [outils de capture non létaux](Non-Lethal-Capture-Tools),
  [armures Jaffa](Jaffa-Armor), tenue de terrain et couvre-chefs variés du SGC.
- [Kara kesh des Grands Maîtres](Kara-Kesh) : bouclier personnel, onde
  cinétique, attaque neurale temporaire et maintien paralysant mono-cible
  alimentés par une réserve commune.

### Tok'ra

- Faction mondiale optionnelle sans colonies : sélectionnée par défaut avec une icône dédiée, elle peut être retirée à la création du monde pour désactiver l'ensemble des contacts, incidents et opérations Tok'ra. Un avertissement jaune apparaît immédiatement lorsque cette entrée est retirée.
- [Double identité hôte / symbiote](Tokra-Dual-Identity), identité d'hôte
  historique pour les Tok'ra générés déjà fusionnés et basculement de la
  personnalité active sur carte ou en caravane pour les Tok'ra contrôlés par le joueur.
- [Confiance Tok'ra](Tokra-Trust), réseau clandestin, planques, caches médicaux,
  contact de terrain et [communicateur sécurisé](Tokra-Secure-Communicator).
- [Zone de livraison Tok'ra](Tokra-Delivery-Drop-Zone) pour les caches et
  livraisons clandestines.
- [Mission d'introduction Tok'ra](Tokra-Introduction-Artifact-Mission) :
  transmission chiffrée accessible avant le communicateur, site Goa'uld à
  sécuriser et module physique à rapporter ; la mission peut revenir après un
  refus ou un échec, mais disparaît définitivement après la première réussite.
  Le véritable module récupéré peut ensuite être analysé au banc de recherche
  afin d'ouvrir la recherche `Communications sécurisées Tok'ra`, qui exige aussi
  la recherche vanilla `Électricité`.
- [Opérations organiques récurrentes](Tokra-Organic-Operation-Opportunities) :
  les nouvelles offres exigent un communicateur joueur alimenté, sans effacer
  les opérations déjà engagées en cas de panne ; observation discrète, analyse de renseignements, accueil médical prolongé
  d'un agent blessé, remise de médicaments à un agent de liaison, appel de
  détresse, contrat de production livré à un rendez-vous temporaire, assaut
  de diversion Goa'uld/Jaffa sur la colonie et [capture d'un officier Jaffa](Tokra-Jaffa-Officer-Capture). Les huit
  opérations lisent leurs données de mission depuis XML, tandis que leurs
  interactions RimWorld
  spécialisées restent en C#. La difficulté capturée lors de l'offre dimensionne
  les patrouilles, les défenseurs du site et certains paramètres médicaux.
- Missions sur cartes temporaires : appel de détresse à la situation incertaine,
  ainsi qu'infiltration et sabotage d'un relais Goa'uld avec renforts différés,
  évacuation et débriefing Tok'ra.

### Colonie et progression

- [Scénario Équipe SG isolée](Stranded-SG-Team-Scenario).
- [Histoires culturelles](Cultural-Backstories) : 83 enfances et carrières
  culturelles avec descriptions et bonus de compétences modérés.
- Profils configurables pour les pawns de départ, noms culturels persistants et
  [réactions sociales contextuelles](Contextual-Social-Baseline).
- [Recherches de fabrication Stargate](Stargate-Crafting-Research).
- [État détaillé du contenu](Content-Status) pour distinguer le contenu jouable,
  les prototypes et les développements encore prévus.


Les domaines Goa'uld disposent aussi d'une
[doctrine stratégique persistante](Goauld-Domain-Doctrines) : conquête,
asservissement ou terre brûlée. Cette préférence reste attachée à la faction,
même lorsque son Grand Maître change, et module uniquement les trois doctrines
de raid déjà existantes. Les Grands Maîtres peuvent également porter un
[bracelet de guérison Goa'uld](Goauld-Healing-Bracelet) distinct du kara kesh.

## Outils de neutralisation non létale

La version `0.3.36-dev` ajoute des bolas fabricables et un fusil
hypodermique expérimental Tok'ra actuellement réglé à douze charges. La version publiée `0.3.37-dev` emploie ces outils dans une opération
récurrente de capture vivante. Un tir réussi applique soit une entrave physique des jambes
avec les bolas, soit une inhibition neuromusculaire avec le fusil : la
Conscience reste intacte, mais la cible tombe au sol et devient capturable par
le flux vanilla. La précision, l'armure, la taille et la résistance propre à la
cible empêchent toute garantie. L'opération fournit un seul fusil scellé à sa zone de livraison. Une fois la
cible à terre et l'escorte neutralisée, la reformation vanilla permet de la
sélectionner comme prisonnier sans lit local. Le transfert reste ligoté pendant
le voyage. Une fois le prisonnier détenu dans une colonie, le communicateur permet
d'appeler une équipe Tok'ra qui entre sur la carte, emporte physiquement la cible
et valide la mission seulement après son départ complet ; le fusil reste non
fabricable et n'est pas distribué hors de cette mission.

## Développement à venir

Les huit opérations Tok'ra et le storyteller Commandement SG-1 sont déjà
publiés. Les directions futures sont désormais séparées en jalons distincts :

- inventaire puis remplacement définitif de tous les placeholders et textures
  temporaires ;
- extensions Goa'uld d'alliance, garde-fous stratégiques et conséquences
  territoriales bornées ;
- anneaux de transport d'abord entre plateformes du joueur, puis usages de
  mission ou hostiles ;
- fondations Asgard, Nox, Unas et Réplicateurs ;
- audits optionnels séparés pour Ideology et Royalty ;
- préréglage de monde entièrement GateRim SG-1 ;
- fondations Stargate, première expédition hors monde puis Porte fonctionnelle.

L'audit de l'orchestration, l'appel à l'aide sur site mondial et le contrat de
production livré à un rendez-vous temporaire sont intégrés. La livraison
utilise l'inventaire réel de la caravane, accepte un retard limité et peut être
perturbée par une interception Goa'uld ou une embuscade sur l'approche finale.
La première mission d'introduction Tok'ra est désormais jouable : une
transmission chiffrée peut révéler un site défendu par des Jaffa Goa'uld, puis
la récupération réelle du module clôt définitivement cette rencontre. Un refus,
une expiration ou un échec laisse la possibilité d'un nouveau signal après un
délai caché. Le module récupéré peut désormais être analysé en trois sessions :
il est non vendable avant décodage, démantelé à la dernière session et remplacé
automatiquement après un délai caché s'il disparaît prématurément. Cette analyse
ouvre la recherche **Communications sécurisées Tok'ra** après **Électricité**.
Depuis `0.3.33-dev`, cette recherche est le prérequis direct de construction
du communicateur et les nouvelles opérations récurrentes exigent un appareil
joueur alimenté. Une opération déjà engagée reste toutefois préservée pendant
une panne, et le retour du canal déclenche un nouveau délai caché plutôt qu'une
offre immédiate.

## Orchestration des opérations Tok'ra

La version `0.3.38-dev` consolide le planificateur partagé de huit opérations tout
en conservant une seule occurrence active, le filtrage avant tirage pondéré,
les délais cachés après chaque résultat et la persistance après sauvegarde.
Le nouvel assaut de diversion relaie un faux signal sans placer d'objet sur la
carte, puis attire une force Goa'uld/Jaffa dimensionnée par les points de menace
capturés lors de l'offre. Les assaillants cherchent à percer les fortifications ;
la colonie doit les neutraliser ou les chasser sans perdre de captif ni de butin.

L'appel de détresse crée un site mondial temporaire dont la situation réelle
n'est révélée qu'à l'arrivée de la caravane. RimWorld gère l'entrée, la pause et
l'enrôlement, tandis que le mod compose une scène cohérente et une extraction
rapide après les soins. Le contrat de production crée un rendez-vous Tok'ra
temporaire et ne retient que des objets réellement fabricables par la colonie.
La remise vérifie la cargaison réelle, une période de grâce autorise une
livraison tardive moins rémunératrice et des forces Goa'uld peuvent intercepter
la caravane pendant le trajet ou sur la dernière tuile d'approche. Après un
combat, RimWorld conserve sa reformation complète afin que le joueur puisse
emporter la cargaison survivante et le butin avant de parcourir la dernière
case. Ces missions restent rejouables et pourront être rééquilibrées après des
tests prolongés.

## Liens utiles

- [Bien débuter](Getting-Started)
- [Installation et dépendances](Installation-and-Requirements)
- [État du contenu](Content-Status)
- [Histoires culturelles](Cultural-Backstories)
- [FAQ](FAQ)
- [Dépôt GitHub principal](https://github.com/Diablood/GateRim-SG1)
