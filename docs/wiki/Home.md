# GateRim SG-1

Bienvenue dans le wiki joueur français de **GateRim SG-1**, un mod inspiré de
Stargate SG-1 pour RimWorld 1.6.

> Statut du wiki : documentation française active
> Version du mod documentée : `0.3.73-dev`


## Relations Goa'uld et pression des raids

La version `0.3.73-dev` étend les conséquences des relations entre domaines :
sous Commandement SG-1, un conflit ouvert conserve son facteur `75 %`, tandis
qu'une alliance sans conflit ouvert applique un bonus plafonné à `110 %` aux
points du raid naturel. Plusieurs relations ne se cumulent pas et les autres
storytellers restent à `100 %`.

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
  naturels ; sinon, une alliance peut porter ce budget à `110 %`, sans cumul.
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

La série `0.3.x` consolide les systèmes réutilisables et la stabilité avant les
grands chapitres de contenu suivants. Les directions durables sont notamment :

- un framework réutilisable de missions et questlines, avec rejouabilité, variantes RP et difficulté adaptative ;
- de nouveaux archétypes d'opérations Tok'ra réellement distincts ;
- l'enrichissement progressif du pool d'opérations Tok'ra après l'intégration de la recherche, du communicateur et de leur verrou d'accès ;
- une refonte visuelle globale, préparée par des concept arts, après la stabilisation des mécaniques et avant la Porte des étoiles ;
- les futures cultures Asgard, Nox et Unas avec leurs noms et parcours propres ;
- un storyteller GateRim SG-1 qui orchestre le mod sans rendre ses événements
  dépendants de ce storyteller ;
- un préréglage de monde entièrement GateRim SG-1 ;
- une phase ultérieure consacrée à la Porte des étoiles fonctionnelle et aux
  expéditions hors monde.

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
