# GateRim SG-1

Bienvenue dans le wiki joueur français de **GateRim SG-1**, un mod inspiré de
Stargate SG-1 pour RimWorld 1.6.

> Statut du wiki : documentation française active
> Version du mod documentée : `0.3.29-dev`

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
- [Domaines des Grands Maîtres Goa'uld](Goauld-System-Lord-Faction),
  [Jaffa libres](Free-Jaffa-Faction), colonies mondiales et incidents associés.
- Armes et équipements : [Ma'Tok](Matok-Staff), [Zat'nik'tel](ZatnikTel),
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
- [Opérations organiques récurrentes](Tokra-Organic-Operation-Opportunities) :
  observation discrète, analyse de renseignements, accueil médical prolongé
  d'un agent blessé, remise de médicaments à un agent de liaison et appel de
  détresse vers un site mondial temporaire. Les cinq opérations lisent leurs
  données de mission depuis XML, tandis que leurs interactions RimWorld
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

## Développement à venir

La série `0.3.x` consolide les systèmes réutilisables et la stabilité avant les
grands chapitres de contenu suivants. Les directions durables sont notamment :

- un framework réutilisable de missions et questlines, avec rejouabilité, variantes RP et difficulté adaptative ;
- de nouveaux archétypes d'opérations Tok'ra réellement distincts ;
- une progression d'introduction Tok'ra prévue à terme : première mission à enjeu, objet-clé, recherche dédiée puis construction du communicateur avant l'accès aux opérations récurrentes ;
- une refonte visuelle globale, préparée par des concept arts, après la stabilisation des mécaniques et avant la Porte des étoiles ;
- les futures cultures Asgard, Nox et Unas avec leurs noms et parcours propres ;
- un storyteller GateRim SG-1 qui orchestre le mod sans rendre ses événements
  dépendants de ce storyteller ;
- un préréglage de monde entièrement GateRim SG-1 ;
- une phase ultérieure consacrée à la Porte des étoiles fonctionnelle et aux
  expéditions hors monde.

L'audit de l'orchestration et l'appel à l'aide sur site mondial sont désormais
intégrés. La prochaine étape est la livraison vers une base Tok'ra temporaire.
Les autres éléments restent planifiés selon les tests, les dépendances
techniques et la stabilité des systèmes existants.

## Orchestration des opérations Tok'ra

La version `0.3.29-dev` étend le planificateur partagé à cinq opérations tout
en conservant une seule occurrence active, le filtrage avant tirage pondéré,
les délais cachés après chaque résultat et la persistance après sauvegarde.

L'appel de détresse crée un site mondial temporaire dont la situation réelle
n'est révélée qu'à l'arrivée de la caravane. RimWorld gère l'entrée, la pause et
l'enrôlement, tandis que le mod compose une scène cohérente et une extraction
rapide après les soins. Le prochain ajout prévu est une livraison vers une base
Tok'ra temporaire avec risques d'interception. Ces
missions resteront rejouables et pourront être rééquilibrées après des tests
prolongés.

## Liens utiles

- [Bien débuter](Getting-Started)
- [Installation et dépendances](Installation-and-Requirements)
- [État du contenu](Content-Status)
- [Histoires culturelles](Cultural-Backstories)
- [FAQ](FAQ)
- [Dépôt GitHub principal](https://github.com/Diablood/GateRim-SG1)

