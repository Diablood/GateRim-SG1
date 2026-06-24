# GateRim SG-1

Bienvenue dans le wiki joueur français de **GateRim SG-1**, un mod inspiré de
Stargate SG-1 pour RimWorld 1.6.

> Statut du wiki : documentation française active
> Version du mod documentée : `0.3.39-dev`

## Le mod en bref

Une équipe SG est isolée sur un RimWorld inconnu. La Terre est hors de portée,
la Porte des étoiles reste silencieuse et des puissances extraterrestres ont
déjà marqué ce monde.

GateRim SG-1 construit progressivement une expérience jouable autour du SGC,
des Jaffa, des Goa'uld, des Tok'ra et de leurs technologies. La Porte des
étoiles fonctionnelle et la progression complète hors monde ne sont pas encore
disponibles : la version actuelle se concentre sur les factions, la biologie
des symbiotes, l'équipement, les événements et les opérations de terrain.

## Contenu actuellement jouable

### Jaffa et Goa'uld

- [Jaffa](Jaffa), [Prim'ta](Primta), trétonine, implantation médicale et
  [cérémonie formelle](Primta-Formal-Ceremony).
- [Bassin d'incubation](Primta-Incubation),
  [bassin de conservation](Primta-Preservation-Basin) et
  [congélation profonde](Primta-Deep-Freezing) des symbiotes immatures.
- [Goa'uld](Goauld), implantation forcée ou rituelle, extraction d'urgence,
  hôtes actifs et [reine Goa'uld](Goauld-Queen).
- [Incursions de symbiotes Goa'uld libres](Goauld-Free-Symbiote-Incursion) :
  menace biologique rare et adaptative utilisant leur chasse autonome.
- [Domaines des Grands Maîtres Goa'uld](Goauld-System-Lord-Faction),
  [Jaffa libres](Free-Jaffa-Faction), colonies mondiales et incidents associés.
- Armes et équipements : [Ma'Tok](Matok-Staff), [Zat'nik'tel](ZatnikTel),
  [outils de capture non létaux](Non-Lethal-Capture-Tools),
  [armures Jaffa](Jaffa-Armor), tenue de terrain et couvre-chefs variés du SGC.

### Tok'ra

- Faction mondiale masquée, visiteurs pacifiques et implantation thérapeutique.
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

