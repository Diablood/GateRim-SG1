# Roadmap

Ce fichier est le backlog durable des travaux **décidés**. Il ne sert ni de
second changelog, ni de registre d'idées, ni de liste de règles de test.

- l'historique publié appartient à [`CHANGELOG.md`](CHANGELOG.md) et aux tags Git ;
- les pistes non décidées appartiennent à
  [`IDEAS_TO_REVISIT.md`](IDEAS_TO_REVISIT.md) ;
- les contrats permanents sont regroupés séparément des futurs jalons ;
- chaque futur chantier suffisamment distinct est planifié comme un jalon
  individuel ;
- les décisions encore ouvertes ne sont pas tranchées prématurément : elles sont
  reprises au lancement du jalon concerné avant toute implémentation.

## Dernier jalon visuel validé et publié

`0.3.98-dev - Finalize tretonin dose visual`

La dose physique de trétonine reçoit une ampoule médicale finale `128×128`,
transparente, fortement détourée et remplie d'un liquide cyan lumineux. Le PNG
reste cadré serré pour conserver un maximum de définition ; sa petite taille en
jeu continue d'être réglée par le `drawSize` du Def.

Le jalon ne modifie ni la fabrication par lots, ni l'empilement, ni la masse, ni
l'administration médicale, ni la substitution active pendant une journée. La
famille passe de temporaire à finale et reçoit une copie wiki protégée
byte-identique.

La clôture documentaire conserve `610` PNG et `77` familles locales, dont `44`
familles finales.

## Dernier jalon visuel validé et publié

`0.3.97-dev - Finalize Tok'ra hypodermic rifle visual`

Le fusil hypodermique expérimental Tok'ra reçoit une texture finale simplifiée,
contrastée et lisible en `128×128`. La silhouette horizontale respecte le rendu
des armes longues de RimWorld, tandis que la légère rotation aléatoire au sol
reste celle héritée des armes vanilla.

Les modules cyan rappellent les douze charges scellées sans surcharger le sprite
de micro-détails flous sous Camera+. Le projectile, la neutralisation non
létale, la livraison de mission, les sauvegardes et l'auto-neutralisation après
la dernière charge restent inchangés.

La clôture documentaire porte le registre à `610` PNG, `77` familles et `43`
familles finales. La fléchette hypodermique reste une famille projectile
temporaire distincte.

## Dernier jalon visuel validé et publié

`0.3.92-dev - Add final Goa'uld and Jaffa command icons`

La révision visuelle finale `r1` remplace les quatre familles de commandes qui
réutilisaient encore l'icône démon personnelle : chasse autonome, extraction
d'urgence, implantation forcée et implantation rituelle. Les chemins C# restent
stables et les quatre fichiers sont des PNG transparents `64×64`.

Les concepts retenus distinguent clairement la recherche d'une cible,
l'extraction chirurgicale, l'attaque d'implantation directe et le cadre
cérémoniel. Les quatre commandes sont validées dans l'interface réelle, y
compris l'usage partagé de `SG1_RitualImplantation` sur les surfaces Goa'uld,
Tok'ra et la cérémonie formelle du Prim'ta.

Le build `0.3.92.0`, les contrôles de durées, d'assets visuels, de cohérence du
projet et `git diff --check` passent. La baseline reste `605` PNG et `72`
familles, avec `28` familles finales et aucune référence manquante ou non
enregistrée.

Les copies wiki restent byte-identiques aux fichiers de jeu et apparaissent sur
les pages fonctionnelles dédiées. La révision documentaire `r2` enregistre la
clôture, l'intégration fast-forward dans `develop`, le tag annoté unique
`v0.3.92-dev` et la synchronisation du wiki séparé, sans modifier les icônes
validées.

## Dernier jalon visuel validé et publié

`0.3.91-dev - Add final intrinsic Jaffa forehead-mark overlays`

La révision visuelle finale `r1` remplace les douze fichiers directionnels des
trois familles intrinsèques existantes sans recréer de gène technique ni changer
les chemins sauvegardés. Les variantes noire ordinaire, argentée d'élite et
dorée de Premier Primat partagent le même petit symbole d'Apophis.

Seule la vue `South` contient des pixels visibles. Les neuf fichiers `North`,
`East` et `West` sont entièrement transparents, ce qui conserve une marque
strictement frontale sans débordement latéral ni texture flottante au-dessus de
la tête.

Le mainteneur a validé les douze PNG dans leurs dossiers définitifs, puis le
build `0.3.91.0`, les contrôles de durées, d'assets visuels, de cohérence du
projet et `git diff --check`. Le lot conserve les Defs intrinsèques,
l'affectation par rang, les outils développeur, la persistance, le retrait
manuel, les offsets et la couverture par casque.

Les trois références `South` sont publiées sur le wiki. La liste blanche
visuelle atteint `24` familles finales sur une baseline inchangée de `605` PNG
et `72` familles. La révision documentaire `r2` enregistre la clôture,
l'intégration dans `develop`, le tag annoté unique `v0.3.91-dev` et la
synchronisation du wiki séparé, sans modifier les textures validées.

## Dernier jalon de nettoyage validé et publié

`0.3.90-dev - Remove legacy Jaffa forehead-mark migration genes`

Branche finale : `feature/final-jaffa-forehead-mark-gene-icons`, créée depuis
`develop` aligné avec le tag publié `v0.3.89-dev`. Le nom historique de la
branche est conservé bien que le périmètre ait été corrigé après l'inspection UI
locale de `r1`.

La révision fonctionnelle finale `r2` supprime les trois anciens gènes techniques
de migration, leurs traductions, champs DefOf, icônes et logique de conversion.
Le mainteneur accepte explicitement de ne plus prendre en charge les sauvegardes
privées qui contiennent encore ces prototypes.

Le système intrinsèque reste intact : marques noire, argentée et dorée,
affectation par domaine Goa'uld, rendu pawn temporaire existant, persistance,
retrait manuel et outils développeur. Le build `0.3.90.0`, les contrôles de
durées, d'assets visuels, de cohérence du projet, `git diff --check` et les tests
ciblés passent. Les gènes obsolètes sont absents de l'inspection et les marques
intrinsèques restent fonctionnelles.

La révision documentaire `r3` enregistre la clôture, l'intégration fast-forward
dans `develop`, le tag annoté unique `v0.3.90-dev` et la synchronisation du wiki
séparé. Les suffixes locaux `r1`, `r2` et `r3` sont absents du commit et du tag.

## Dernier jalon correctif et gameplay validé et publié

`0.3.87-dev - Fix Jaffa and Tok'ra starter symbiotes`

Branche finale : `fix/jaffa-tokra-starter-symbiotes`, créée depuis `develop`
aligné avec le tag publié `v0.3.86-dev`.

La révision finale `r3` est validée et publiée. Le correctif agit uniquement
pendant `PawnGenerationContext.PlayerStarter`, avant l'affichage du pawn sur la
page vanilla de configuration. Chaque Jaffa adulte éligible reçoit exactement
un Prim'ta et chaque candidat `SG1_GoauldHost` reçoit exactement un véritable
symbiote adulte persistant.

La carrière finale détermine l'origine : Tok'ra avec identité humaine distincte
et changement de personnalité, ou Goa'uld sans commande Tok'ra. Le premier
pawn et chaque reroll contraint par xénotype sont couverts, sans doublon.
Aucune réconciliation au premier tick ou au chargement n'est ajoutée, afin de
respecter un retrait volontaire effectué avec un mod d'édition avant le début
de la partie.

## Dernier jalon visuel validé et publié

`0.3.89-dev - Add final gene icons and complete world visual references`

Branche finale :
`feature/final-gene-icons-and-world-visual-references`, créée depuis `develop`
aligné avec le tag publié `v0.3.88-dev`.

La révision visuelle `r1` ajoute six icônes finales de gènes de gameplay,
supprime le prototype obsolète `SG1_JaffaLongevity`, classe les quatre icônes de
factions mondiales déjà approuvées comme finales et ajoute au wiki les images
des six gènes, quatre factions et sept sites d'événements. Les trois icônes
techniques des marques frontales restent explicitement exclues pour un futur lot
Jaffa dédié.

Le build forcé `0.3.89.0`, les contrôles de durées, d'assets visuels, de
cohérence du projet et `git diff --check` passent. Les six icônes sont validées à
leur taille réelle dans l'éditeur de xénotype, l'ancien gène de longévité Jaffa
est absent, le soutien de longévité du Prim'ta reste intact, les galeries wiki
sont complètes et `Player.log` ne contient aucune nouvelle erreur pertinente.
Le libellé français long de compatibilité avec le symbiote reste inchangé par
décision explicite du mainteneur.

La clôture documentaire `r2` ne modifie aucun PNG, Def, traduction, code C#,
assemblage, comportement ou état de sauvegarde. Le jalon est publié par commit
final sur la branche temporaire, intégration fast-forward dans `develop`, tag
annoté `v0.3.89-dev` et synchronisation du wiki séparé. Les suffixes locaux
`r1` et `r2` sont absents du commit et du tag.

## Jalon visuel publié précédent

`0.3.88-dev - Add final storyteller SG-1 portrait`

Branche finale : `feature/final-storyteller-portrait`, créée depuis `develop`
aligné avec le tag publié `v0.3.87-dev`.

La révision visuelle `r1` remplace uniquement les portraits large et tiny du
storyteller Commandement SG-1 par les deux PNG approuvés par le mainteneur,
conserve les chemins existants et affiche la grande image sur la page wiki
dédiée. Le rendu réel des deux tailles est validé sans cadrage défectueux,
opacité parasite ni régression de comportement.

La révision documentaire `r2` classe les deux familles comme finales, porte la
liste blanche visuelle à `11` familles et aligne les métadonnées publiques et
techniques sur `0.3.88-dev`. Les contrôles de durées, d'assets visuels, de
cohérence du projet et `git diff --check` passent. La clôture documentaire `r3`
ne modifie aucun PNG, Def, code de storyteller, assemblage ou état de sauvegarde.

Le jalon est publié par commit final sur la branche temporaire, intégration
fast-forward dans `develop`, tag annoté `v0.3.88-dev` et synchronisation du wiki
séparé. Les suffixes locaux `r1`, `r2` et `r3` sont absents du commit et du tag.

## Jalon suivant

Aucun numéro ultérieur n'est réservé. Le prochain travail sera sélectionné parmi
les familles visuelles et de présentation restantes de la Phase 1.

## Jalon visuel publié antérieur

`0.3.86-dev - Add final Goa'uld host and Jaffa xenotype icons`

Branche finale : `feature/final-xenotype-icons`, créée depuis `develop` aligné
avec le tag publié `v0.3.85-dev`.

La révision finale `r2` est validée et publiée. Ce premier lot artistique
définitif reste limité aux deux icônes de xénotypes les plus simples. L'icône
Jaffa fournie par le mainteneur reprend une tête humaine épurée avec la marque
d'Apophis. L'icône de l'hôte Goa'uld représente directement le symbiote
simplifié, organisme qui définit l'état acquis de l'hôte.

Le jalon conserve les Defs et comportements, remplace seulement le PNG existant
de l'hôte, ajoute le PNG Jaffa et redirige son `iconPath` depuis le Hussard
vanilla. Le build `0.3.86.0`, les contrôles, le rendu réel en petite taille,
`Player.log` et les références visuelles sur les pages wiki dédiées sont
validés. Les copies wiki restent byte-identiques aux textures de jeu.

## Dernier jalon documentaire validé et publié

`0.3.85-dev - Audit provisional visual assets`

Branche finale : `feature/provisional-visual-asset-audit`, créée depuis
`develop` aligné avec le tag publié `v0.3.84-dev`.

La révision finale `r3` est validée et publiée. Elle inventorie les `608` PNG du
dépôt sous `75` familles canoniques, classe chaque visuel et dépendance vanilla,
fixe un ordre `P0/P1/P2/P3/done` et ajoute un contrôle automatisé en lecture
seule. Seules les sept icônes de sites d'événements explicitement validées sont
finales dans `Textures/`; `About/ModIcon.png` reste l'autre référence finale hors
compteur. Tous les autres visuels demeurent temporaires. Aucun PNG, Def, rendu ou
comportement de jeu n'est modifié.

Le build `0.3.85.0`, les contrôles de durées, d'assets et de cohérence, la liste
blanche finale, les priorités P0/P1 et le chargement du menu principal sans
nouvelle erreur pertinente sont validés. Chaque future validation visuelle,
ainsi que tout ajout, suppression, renommage ou déplacement d'image, doit mettre
à jour le registre technique et la page wiki de références dans la même
révision. Le premier lot a ensuite été publié sous `0.3.86-dev`.

## Jalon gameplay précédent

`0.3.84-dev - Add first bounded Goa'uld territorial takeover`

La révision finale `r1` est validée et publiée. Une paire exacte en conflit
ouvert peut réserver puis transférer la propriété d'une seule colonie Goa'uld
existante. Le même objet mondial, nom, identifiant et tuile sont conservés ;
tous les garde-fous sont revérifiés avant la mutation, l'ancien domaine
conserve au moins une colonie et le gagnant reste sous le plafond dynamique.
Le transfert, sa lettre ciblée et ses cooldowns persistent après
sauvegarde/rechargement sans modifier relations, raids, doctrines, missions,
récompenses ou fréquence storyteller.

## Jalon gameplay antérieur

`0.3.83-dev - Add Goa'uld territorial safeguards and diplomatic coherence`

La révision finale `r3` est validée et publiée. Trois domaines Goa'uld sont
proposés par défaut tout en laissant la liste vanilla réductible. Les cinq états
GateRim sont synchronisés vers les relations vanilla `Neutral`, `Hostile` ou
`Ally` uniquement entre domaines, sans toucher au joueur ni aux factions
extérieures. Deux domaines territoriaux actifs suffisent au cadre stratégique ;
le dernier territoire est protégé, les mondes trop pauvres et l'hégémonie
automatique sont bloqués, l'expansion ralentit avec la taille et une seule
réservation sèche persistante peut exister. Aucun territoire n'est créé,
transféré ou détruit.

## Jalon gameplay plus ancien

`0.3.82-dev - Add Goa'uld alliance rupture after major failure`

La révision finale `r1` est validée et publiée. Une représaille commune naturelle
d'au moins six Jaffa combinés est observée jusqu'à une défaite à `20%` ou moins.
Cet échec majeur programme sans second tirage aléatoire la rupture de la paire
exacte après `1–2` jours. La relation passe de `Alliance` à `Rivalry` avec une
lettre RP sans cible ; une seule rupture peut être en attente, son délai est
suspendu hors `Commandement SG-1` et elle est annulée si la paire n'est plus
alliée ou si un domaine devient inactif. Aucun goodwill, raid, territoire,
colonie, budget ou profil doctrinal n'est modifié.

## Dernier jalon documentaire clôturé

`0.3.77-dev - Establish the core-faction completion gate`

La révision finale `r1` est validée et publiée. Ce jalon
documentaire rend obligatoire la clôture du socle Tok'ra, Goa'uld/Jaffa et
Tau'ri/SGC avant l'ouverture des peuples et chapitres qui hériteront de ses
systèmes. Il ne modifie aucun gameplay.

## Jalon documentaire précédent

`0.3.76-dev - Reconcile future roadmap and visual debt`

La révision finale `r2` est validée et publiée. Elle retire les travaux déjà
publiés du backlog actif, sépare les contrats permanents des futurs jalons,
planifie individuellement les chantiers décidés et inscrit une passe artistique
définitive couvrant tous les placeholders et textures temporaires.

Elle inscrit aussi les anneaux de transport en deux étapes distinctes et impose
que les questions encore ouvertes soient reposées au lancement du jalon concerné
avant toute implémentation. Aucun gameplay, Def, traduction, texture, sauvegarde
ou équilibrage n'est modifié.

## Jalon gameplay précédent

`0.3.75-dev - Add Jaffa officers to eligible Goa'uld forces`, révision finale
`r2`, est validé et publié. Les raids naturels, défenses de colonies et missions
adaptées peuvent remplacer au plus un garde par un officier de même budget dans
un groupe d'au moins cinq Jaffa.

## Travaux déjà réalisés retirés du backlog actif

Les éléments suivants ne doivent plus être proposés comme nouveaux jalons :

- refonte fonctionnelle et visuelle du poste d'observation Tok'ra sous forme de
  lunette de terrain : `0.3.4-dev` ;
- regroupement hiérarchique des outils de debug : `0.3.35-dev` ;
- audit du pool fermé des huit opérations Tok'ra, de leur récurrence, de leur
  anti-répétition et de leurs variantes RP : `0.3.38-dev` ;
- icônes mondiales des factions : `0.3.50-dev` ;
- icônes mondiales des sites et missions : `0.3.51-dev` ;
- audit global de la progression de menace Goa'uld : `0.3.53-dev` ;
- première consolidation documentaire : `0.3.63-dev` ;
- réduction bornée des raids en conflit ouvert : `0.3.68-dev` ;
- bonus borné des raids en alliance : `0.3.73-dev` ;
- apparence distinctive de l'officier capturable : `0.3.74-dev` ;
- officiers dans les forces Goa'uld éligibles : `0.3.75-dev` ;
- renforts différés d'un domaine Goa'uld allié : `0.3.78-dev` ;
- raids conjoints coordonnés entre domaines alliés : `0.3.79-dev`;
- influence des relations sur les doctrines de raid : `0.3.80-dev`;
- représailles communes entre domaines alliés : `0.3.81-dev` ;
- rupture d'alliance après l'échec majeur d'une représaille commune :
  `0.3.82-dev` ;
- garde-fous territoriaux et cohérence diplomatique inter-domaines :
  `0.3.83-dev` ;
- première prise territoriale Goa'uld bornée : `0.3.84-dev` ;
- correction des Prim'ta et symbiotes des starters Jaffa, Tok'ra et Goa'uld :
  `0.3.87-dev`.

Le poste d'observation, les équipements d'officier et les autres visuels
provisoires restent toutefois inclus dans la future passe artistique définitive.
Une fonctionnalité peut donc être terminée tout en conservant une dette d'art.

# Priorité de développement obligatoire

## Phase 1 - Clôturer le socle Tok'ra / Goa'uld-Jaffa / Tau'ri-SGC

Aucun nouveau peuple, menace majeure ou préréglage mondial héritier ne doit être
commencé tant que le socle actuel n'est pas explicitement clos.

Cette phase bloquante comprend :

- la stabilisation ciblée de la partie Tok'ra existante, sans neuvième opération ;
- la finalisation de la présentation Tok'ra, Goa'uld, Jaffa et Tau'ri/SGC ;
- les audits transversaux encore décidés ;
- les variantes d'équipement Tau'ri/SGC encore décidées ;
- les conséquences stratégiques, territoriales, équipements et présentations
  Goa'uld, Jaffa de domaine et Jaffa libres encore décidés ;
- les anneaux de transport joueur puis leurs usages bornés de mission ou hostiles ;
- les fondations Stargate, la première expédition hors monde et la Porte
  fonctionnelle.

L'ordre interne de ces jalons reste sélectionnable un par un. Cette souplesse ne
permet pas de prendre un jalon de la phase suivante.

Le pool Tok'ra reste fermé à huit opérations. « Clore la partie Tok'ra » signifie
corriger les défauts réellement observés, terminer les surfaces partagées et la
présentation, puis déclarer le socle stable ; cela ne signifie pas créer de
nouvelles opérations pour retarder artificiellement la clôture.

## Critère de sortie de la phase 1

La phase peut être déclarée close seulement lorsque chaque jalon bloquant est :

1. validé et publié ;
2. explicitement retiré de la roadmap décidée ;
3. explicitement déplacé dans `IDEAS_TO_REVISIT.md` comme piste non bloquante.

Une question non répondue, un élément simplement omis ou une case laissée ouverte
ne constitue pas une clôture.

## Phase 2 - Extensions héritières bloquées

Avant la clôture explicite de la phase 1, ne pas commencer :

- la fondation Asgard ;
- la fondation Nox ;
- la fondation Unas ;
- la fondation des Réplicateurs ;
- les audits optionnels Ideology et Royalty ;
- le préréglage de monde entièrement GateRim SG-1.

Les origines pondérées des hôtes Tok'ra constituent une extension dépendante des
futures cultures. Elles seront reprises après l'existence de plusieurs cultures
pertinentes et ne maintiennent pas le socle Tok'ra actuel artificiellement ouvert.

## Règle d'héritage de la phase 2

Chaque nouveau peuple, faction ou type de menace doit réutiliser, selon son
besoin, les fondations publiées : profils culturels, noms, backstories, identité,
factions, icônes, missions pilotées par Defs, menace vanilla, persistance,
compatibilité de sauvegarde, diagnostics, documentation et tests de régression.

Créer un framework parallèle ou remplacer une fondation existante exige une
décision d'architecture explicite avant le jalon concerné.

# Contrats permanents

Ces règles restent applicables à tous les futurs jalons, mais ne constituent pas
des fonctionnalités à reprogrammer.

## Opérations et missions

- conserver le pool actuel de huit opérations Tok'ra fermé ;
- corriger uniquement les défauts observés lors des parties longues ;
- conserver le slot visible unique, les délais cachés et l'anti-répétition ;
- maintenir plusieurs variantes RP solides lorsque la répétition est visible ;
- conserver les missions récurrentes rééligibles lorsqu'elles le prévoient ;
- dimensionner les menaces depuis les points vanilla et la valeur réelle de la
  colonie ;
- n'étendre le framework commun qu'après plusieurs besoins concrets ;
- conserver des adaptateurs spécialisés pour les missions atypiques.

## Interface et debug

- garder les détails techniques dans les rapports et les logs ;
- réserver les diagnostics complets au mode développeur ;
- ne rendre aucune action de debug visible hors mode développeur ;
- conserver une organisation par sous-système et par phase pratique de test.

## Relations stratégiques Goa'uld

- conserver les transitions automatiques uniquement sous `Commandement SG-1` ;
- annoncer chaque changement réel par une lettre RP nommant les domaines ;
- traiter cette annonce comme une ouverture de possibilités, jamais comme le
  déclenchement immédiat d'un raid ou d'un champ de bataille ;
- conserver des raids standards possibles sous alliance ou conflit afin que le
  joueur ne puisse pas déduire la prochaine forme d'attaque ;
- ne modifier ni fréquence storyteller ni délai de raid sans jalon explicite.

## Culture, identité et équipement

- privilégier les données XML lorsque le besoin est réellement déclaratif ;
- ne jamais réécrire l'origine culturelle d'une implantation réelle ;
- refléter la puissance dans `combatPower`, la menace, la valeur et
  l'acquisition ;
- réserver les technologies fortes aux rangs cohérents ;
- conserver les armes humaines vanilla comme base, avec substitutions
  facultatives seulement ;
- préserver les Defs et chemins de texture stabilisés lorsque seule l'illustration
  doit être remplacée.

## Publication et compatibilité

- maintenir d'abord le wiki français ;
- mettre à jour un document existant avant d'en créer un nouveau ;
- conserver les incidents GateRim compatibles avec les storytellers ordinaires ;
- préserver `About/ModIcon.png` ;
- livrer uniquement les fichiers nouveaux ou modifiés dans les ZIP de révision ;
- fournir chaque chemin comme fichier complet prêt à remplacer, sans `.patch`,
  diff applicable ni commande `git apply`, sauf demande exceptionnelle explicite ;
- signaler explicitement toute suppression avant extraction.

# Futurs jalons décidés

Les titres ci-dessous sont des unités de travail distinctes. Aucun numéro de
version ni nom de branche n'est réservé avant que le jalon devienne le prochain
travail actif.

## Visuels et présentation

### Remplacer tous les placeholders et textures temporaires par de l'art définitif

Effectuer une passe artistique globale de qualité couvrant notamment la lunette
d'observation Tok'ra, les objets d'opération, les modules Tok'ra, les équipements
Goa'uld/Jaffa/SGC et l'ensemble rouge des officiers. Conserver les Defs et chemins
stables lorsque cela suffit ; remplacer alors uniquement les PNG.

Le premier lot publié `0.3.86-dev` couvre les icônes finales des xénotypes
Jaffa et hôte Goa'uld. Le lot publié `0.3.88-dev` finalise les deux portraits du
storyteller Commandement SG-1. Le lot actif `0.3.89-dev` couvre six gènes de
gameplay et la documentation visuelle des factions et sites déjà approuvés. Les
autres placeholders et textures temporaires restent des lots ultérieurs
distincts.

Décisions encore reportées aux lots concernés : direction artistique détaillée,
résolution, variantes et externalisation éventuelle de la création.

### Harmoniser les identités visuelles des cultures

Rendre immédiatement lisibles les identités Tok'ra, Goa'uld, Jaffa et SGC sans
confondre technologies, rangs ou fonctions.

Décisions reportées au lancement : palette, formes, motifs, matériaux et
frontières exactes avec la passe de textures définitives.

### Actualiser les visuels publics

Après validation de chaque asset définitif, l'ajouter immédiatement à la page de
référence du wiki avec son Def et son chemin stable. Les captures et pages
concernées peuvent ensuite être renouvelées par petits lots ; ne pas attendre une
passe globale unique qui risquerait d'oublier ou d'écraser des références déjà
validées.

Décisions reportées au lancement : liste des captures, mise en scène et format de
la galerie.

## Maintenance et audits transversaux

### Seconde consolidation documentaire et des tests

Comparer les contrats techniques, consolider `docs/TESTING.md`, supprimer les
répétitions réelles et conserver toutes les régressions encore utiles. Ne pas
confondre ce travail avec la réconciliation de roadmap de `0.3.76-dev`.

Décisions reportées au lancement : documents fusionnables et couverture minimale
à préserver.

### Auditer stockage, nourriture, recettes et commerce

Réexaminer les catégories, filtres, recettes, prérequis, matières, vendabilité,
marchands et objets de mission afin d'éliminer les incohérences économiques.

Décisions reportées au lancement : périmètre exact des familles d'objets et
règles d'équilibrage comparatives.

### Effectuer la passe finale des outils de debug et du ton RP

Auditer les outils ajoutés après `0.3.35-dev`, leur ordre, leurs libellés et leur
visibilité. Harmoniser les formulations joueur sans refaire l'architecture déjà
publiée.

Décisions reportées au lancement : sous-menus concernés et niveau de réécriture
RP.

## Équipement Tau'ri / SGC

### Ajouter des variantes pondérées de pantalons et vestes SGC

Étendre la variété visuelle par XML pondéré sans créer artificiellement une
nouvelle gamme d'armes Tau'ri.

Décisions reportées au lancement : nombre de variantes, différences de stats,
méthode de sélection et textures temporaires ou définitives.

## Équipement Goa'uld lié au rang

### Sélectionner et implémenter le prochain dispositif de rang

Choisir un seul objet ou système distinct après audit de lore et de gameplay.
Chaque dispositif futur doit disposer de coûts, limites, contre-jeu, usage IA,
valeur et disponibilité cohérents.

Décisions reportées au lancement : dispositif retenu, porteurs, acquisition,
énergie, IEM, recharge, recherche et butin.

## Anneaux de transport

### Ajouter une fondation d'anneaux de transport entre plateformes du joueur

Créer une première alternative thématique aux pods vanilla : deux plateformes
construites, connues et alimentées transportent un groupe limité entre cartes
contrôlées par le joueur. Les pods vanilla restent disponibles dans les parties
normales.

Décisions reportées au lancement : besoin d'une plateforme aux deux extrémités,
portée, coût énergétique ou naquadah, capacité, délai, objets admissibles,
pawns à terre, prisonniers, animaux, cartes non chargées et solution de retour.

Garde-fous déjà décidés : ne pas rendre les caravanes ni la future Porte
obsolètes ; ne pas fournir de transport gratuit, illimité ou global ; ne pas
remplacer les pods vanilla sans option ou contexte GateRim explicite.

### Étendre les anneaux aux missions et usages hostiles

Après la fondation joueur, étudier séparément les balises temporaires, sites de
mission, extractions, arrivées Goa'uld et contre-jeu défensif.

Décisions reportées au lancement : ciblage sans plateforme permanente,
interception, panne, arrivée dispersée, usage ennemi et récupération du groupe.

## Progression Stargate

### Continuer les fondations technologiques Stargate

Prolonger la recherche et les infrastructures sans introduire encore une Porte
fonctionnelle complète.

Décisions reportées au lancement : prochaine technologie, prérequis, objet
physique et lien avec le contenu autonome existant.

### Ajouter une première expédition hors monde bornée

Créer un premier flux d'expédition limité une fois les fondations suffisamment
solides, sans prétendre couvrir immédiatement toute la galaxie.

Décisions reportées au lancement : transport utilisé, génération de carte,
retour, durée, risques, objectif et relation avec les anneaux.

### Introduire une Porte des étoiles fonctionnelle

N'activer la Porte qu'après validation du jeu sans Porte, des expéditions et des
boucles de retour. Tout le contenu actuel doit rester autonome et compatible.

Décisions reportées au lancement : adresse, alimentation, sélection de monde,
cartes distantes, fermeture, incidents, sauvegarde et limites d'usage.

## Phase 2 - Cultures, peuples et monde GateRim

Cette section est bloquée jusqu'à la clôture explicite du socle Tok'ra /
Goa'uld-Jaffa / Tau'ri-SGC.


### Ajouter des origines pondérées aux hôtes Tok'ra après les nouvelles cultures

Ce jalon est une extension dépendante de la phase 2, pas un reliquat bloquant du
socle Tok'ra actuel. Réutiliser le moteur culturel seulement lorsque plusieurs
cultures pertinentes existent, sans réécrire l'origine d'une implantation réelle.

Décisions reportées au lancement : cultures disponibles, poids, génération et
compatibilité avec les hôtes existants.

### Ajouter la fondation Asgard

Créer d'abord une présence cohérente pouvant soutenir commerce, assistance et
missions sans imposer une colonie territoriale classique.

Décisions reportées au lancement : représentation biologique, PawnKinds,
faction, apparition, commerce et limites technologiques.

### Ajouter la fondation Nox

Créer une présence pacifique, diplomatique et commerciale distincte des
factions militaires.

Décisions reportées au lancement : représentation, faction, relations,
non-violence, commerce et réactions aux conflits.

### Ajouter la fondation Unas

Créer des variantes sauvages ou tribales avec identité culturelle et
compatibilité biologique potentielle comme hôtes.

Décisions reportées au lancement : race ou xenotype, factions, langage,
équipement, implantation et génération mondiale.

### Ajouter la fondation des Réplicateurs

Créer une première menace Réplicateur autonome seulement après avoir défini sa
forme biologique ou mécanique, sa reproduction, son adaptation et son
contre-jeu sans copier un simple raid d'insectes ou de mécanoïdes.

Décisions reportées au lancement : type de pawn, cycle de réplication, matériaux
consommés, progression, armes efficaces, cartes, incidents et usage mondial.

### Auditer une intégration optionnelle Ideology

Étudier séparément les préceptes, réactions morales, rituels et attentes liés aux
symbiotes, aux hôtes, aux Jaffa et aux technologies extraterrestres, sans rendre
Ideology obligatoire.

Décisions reportées au lancement : surfaces réellement utiles, patchs
conditionnels, compatibilité des sauvegardes et limites de génération.

### Auditer une intégration optionnelle Royalty

Étudier séparément les interactions entre titres, honneurs, psycasts, hiérarchie
Goa'uld et Grands Maîtres, sans rendre Royalty obligatoire ni assimiler les rangs
Goa'uld à l'Empire vanilla.

Décisions reportées au lancement : systèmes compatibles, exclusions, récompenses
et patchs conditionnels.

### Ajouter un préréglage de monde entièrement GateRim SG-1

Proposer un mode facultatif sans factions vanilla sélectionnables, seulement
lorsque les rôles économiques, militaires, commerciaux et diplomatiques sont
suffisamment couverts.

Décisions reportées au lancement : factions minimales, économie, raids,
commerce, victoire, scénarios et compatibilité avec les parties normales.

# Règle de clôture

Lorsqu'un élément est terminé :

1. décrire le résultat dans le changelog et l'état courant ;
2. retirer le jalon terminé du backlog futur ;
3. conserver seulement les règles permanentes réellement utiles ;
4. déplacer les pistes abandonnées ou non décidées dans
   `IDEAS_TO_REVISIT.md` ;
5. utiliser Git et les tags comme archive au lieu de laisser une case ouverte
   pour un travail déjà publié.
