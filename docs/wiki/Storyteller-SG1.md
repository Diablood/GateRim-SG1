# Storyteller GateRim SG-1

> Première version : `0.3.65-dev`
> Dernière évolution : `0.3.83-dev`
> Statut : garde-fous territoriaux et cohérence diplomatique publiés après validation `r3`

**Commandement SG-1** est un storyteller optionnel qui conserve un rythme
classique tout en coordonnant les systèmes stratégiques propres au mod.

## Rythme ordinaire

Il reprend la définition Cassandra Classique actuellement résolue par RimWorld :

- montée progressive de la pression ;
- périodes de répit ;
- mêmes contrats d'incidents et même adaptation de difficulté ;
- aucun remplacement figé du XML de Core.

Les incidents GateRim déjà publiés restent utilisables avec les autres
storytellers compatibles.

## Relations entre domaines Goa'uld

Depuis `0.3.66-dev`, chaque paire de domaines Goa'uld conserve l'un des états
suivants : neutralité, rivalité, conflit ouvert, trêve ou alliance.

La relation appartient aux factions elles-mêmes. Elle survit au remplacement
d'un Grand Maître et à la sauvegarde/recharge. Les changements sont lents et
produisent des rapports RP nommant les deux domaines.

Une partie contenant un seul domaine Goa'uld ne possède aucune paire à faire
évoluer. Plusieurs instances peuvent être ajoutées lors de la création du monde.

La lettre produite par un changement de relation rend la situation lisible mais
ne déclenche aucun raid ni champ de bataille. Elle indique seulement que certains
événements deviennent possibles ; un raid standard peut toujours survenir.

## Influence des relations sur les doctrines

Depuis `0.3.80-dev`, une seule relation active peut légèrement déplacer les
probabilités des doctrines déjà admissibles d'un raid naturel :

- conflit ouvert : destruction `x1,25` ;
- sinon alliance : assaut direct `x1,25` ;
- sinon rivalité : enlèvement `x1,25` ;
- neutralité et trêve : aucun effet.

La priorité est `conflit ouvert > alliance > rivalité` et les effets ne se
cumulent jamais. Les seuils, le profil permanent du domaine, les points et la
fréquence restent autoritaires. Les autres storytellers désactivent cette
influence sans effacer les relations stockées.

## Pression des raids selon les relations

Sous **Commandement SG-1**, les relations modifient légèrement la puissance des
raids naturels de Jaffa sans toucher à leur fréquence :

- un domaine engagé dans au moins un conflit ouvert utilise `75 %` de ses
  points habituels ;
- sinon, un domaine engagé dans au moins une alliance utilise `110 %` ;
- sinon, il conserve `100 %`.

Les facteurs ne se cumulent jamais. Un conflit ouvert est prioritaire lorsqu'un
domaine possède aussi une alliance. L'éligibilité et le choix entre assaut
direct, enlèvement et destruction utilisent toujours les points vanilla
initiaux.

Les représailles, missions, sites hostiles et tests déterministes sont exclus.
Le worker commun distingue désormais l'appel naturel du storyteller d'un appel
qui était déjà forcé avant la génération du raid.

## Renforts alliés différés

Lorsqu'un raid allié atteint au moins `800` points finaux, un quart de son
budget existant peut être confié à un second domaine allié. La force principale
conserve les trois quarts restants : le facteur total reste `110 %` et aucun
budget gratuit n'est créé.

La seconde force arrive plus tard depuis le bord de carte. Le délai reste caché
et aucune alerte ne révèle les renforts à l'avance. Une courte lettre RP apparaît
seulement à leur arrivée, avec le nom du domaine allié. Les deux factions gardent
leurs couleurs et coopèrent uniquement pendant cette attaque.

## Raids conjoints coordonnés

À partir de `0.3.79-dev`, la moitié des raids d'alliance admissibles reste
standard. Pour un assaut direct coopératif, le jeu choisit ensuite entre le
renfort différé et un assaut conjoint simultané.

Le raid conjoint partage le budget `110 %` existant en `60/40`. Les forces de
deux domaines exacts arrivent depuis des bords opposés, conservent leurs couleurs
et partagent une seule lettre RP. Elles se retirent ensemble si un détachement
rompt le combat. Aucun tirage storyteller, point gratuit, pod, rupture
d'alliance ou effet territorial n'est ajouté.

## Représailles communes des domaines alliés

À partir de `0.3.81-dev`, la défaite décisive d'un raid naturel standard peut
provoquer une réaction commune de la paire alliée exacte. Le raid initial doit
compter au moins cinq Jaffa et tomber à `25 %` ou moins de combattants actifs.
La réaction reste rare (`25 %`), limitée à une seule attente globale et suivie
d'un cooldown de `30` jours pour la paire.

Après `2–4` jours, les deux domaines lancent un assaut direct simultané utilisant
`80 %` des points de menace vanilla, partagés `60/40`. La lettre de programmation
ne propose aucun déplacement de caméra, puisqu'aucune force n'existe encore. La
lettre d'arrivée nomme les deux domaines, annonce clairement deux détachements
sur des côtés opposés et permet d'identifier une cible dans chaque force.

## Rupture d'alliance après un échec majeur

À partir de `0.3.82-dev`, seules les représailles communes naturelles comptant au
moins six Jaffa combinés peuvent produire cette conséquence. Les chemins créés
ou déclenchés par les commandes développeur sont exclus de l'observation
automatique.

Si la force combinée tombe à `20 %` ou moins de combattants actifs, l'échec est
consommé une seule fois et programme une rupture déterministe après `1–2` jours.
Une seule rupture peut être en attente dans le monde. Son délai est suspendu
lorsqu'un autre storyteller est actif.

À l'échéance, la paire exacte passe de **Alliance** à **Rivalité**. La conséquence
est annulée si les domaines ne sont plus alliés ou si l'un d'eux est vaincu avant
la résolution. Une lettre diplomatique sans cible nomme les deux domaines et
explique la rupture ; elle ne déclenche aucun raid supplémentaire.

Cette conséquence ne modifie ni la bonne volonté envers le joueur, ni les
colonies, ni les territoires, ni les budgets de menace ou les doctrines
permanentes.

## Cohérence avec les relations vanilla

À partir de `0.3.83-dev`, l'état diplomatique GateRim de chaque paire Goa'uld
devient autoritaire pour le type de relation vanilla entre ces deux factions :

| État GateRim | Relation vanilla |
|---|---|
| Neutralité | Neutre |
| Rivalité | Neutre |
| Conflit ouvert | Hostile |
| Trêve | Neutre |
| Alliance | Alliée |

La synchronisation intervient à la création de la paire, lors d'une transition,
au chargement et pendant la réconciliation périodique. Elle ne concerne jamais
la relation avec le joueur, les Tok'ra, les Jaffa libres ou une faction vanilla,
et ne produit aucune lettre de changement de bonne volonté.

La définition Goa'uld conserve une hostilité permanente envers l'expédition du
joueur et toutes les factions extérieures, mais exempte les autres instances du
même domaine technique. La version publiée après validation `r3` confirme que deux domaines peuvent
atteindre les relations vanilla neutre, hostile et alliée via la bonne volonté,
sans message ni lettre et sans modifier leurs relations extérieures.

## Garde-fous territoriaux

Le même jalon prépare les futures conséquences territoriales sans encore en
appliquer. La liste vanilla propose trois domaines Goa'uld par défaut, mais le
joueur peut réduire ce nombre. La simulation territoriale nécessite seulement
deux domaines actifs, chacun non vaincu et propriétaire d'au moins une colonie
permanente.

Le cadre protège systématiquement la dernière colonie d'un domaine, exige au
moins `nombre de domaines actifs + 2` colonies Goa'uld permanentes pour un
transfert hostile, ralentit fortement l'expansion des grands domaines et refuse
une acquisition qui dépasserait `50 %` des colonies Goa'uld permanentes.

Une seule réservation territoriale sèche peut être en attente dans le monde.
Elle mémorise la paire exacte, la colonie, la relation exigée, l'échéance et les
comptages initiaux. Les délais globaux, par domaine et par paire sont suspendus
hors **Commandement SG-1**. Une relation, une propriété, un domaine ou un seuil
de sécurité devenu invalide annule la réservation.

`0.3.83-dev` ne programme aucune occurrence naturelle et ne change aucun
propriétaire. L'outil développeur se termine par `CompletedDryRun` : aucune
colonie n'est créée, transférée ou détruite.

## Batailles en conflit ouvert

Depuis `0.3.69-dev`, une paire en conflit ouvert peut produire une
[bataille entre deux domaines](Goauld-Open-Conflict-Battlefields) près d'une
colonie.

La version `0.3.70-dev` ajoute une seconde forme : un site temporaire sur la
carte mondiale. Une caravane peut s'y rendre ou l'ignorer. À l'arrivée, la carte
est créée et réutilise exactement les règles de la bataille locale : entrée par
les bords, rassemblement, assaut mutuel annoncé, riposte joueur bornée, rupture
morale et retrait.

Les deux formes partagent un seul emplacement et un seul délai. Elles alternent
lorsque les deux sont possibles et ne peuvent jamais apparaître simultanément.

Ignorer le site mondial ne provoque aucun échec ou changement diplomatique.

## Choix facultatif

La progression automatique des relations, la réduction de pression et les
nouvelles opportunités de bataille fonctionnent uniquement avec
**Commandement SG-1**.

Avec Cassandra, Phoebe, Randy ou un storyteller compatible :

- les relations et occurrences déjà enregistrées sont conservées ;
- aucun nouvel état ou champ de bataille n'est tiré ;
- les échéances futures, y compris une rupture d'alliance ou une réservation
  territoriale sèche en attente, sont repoussées pendant la suspension ;
- revenir à Commandement SG-1 ne déclenche pas de retard accumulé ;
- les raids naturels utilisent de nouveau `100 %` des points ;
- aucune vague alliée différée n'est planifiée ;
- la cadence du storyteller choisi n'est jamais modifiée.

## Limites actuelles

Les relations ne provoquent pas encore :

- de transfert territorial réel ;
- de création ou destruction de colonies mondiales ;
- d'élimination de faction ;
- de changement diplomatique avec le joueur.

Le cadre de sécurité et sa simulation sèche existent depuis `0.3.83-dev`, mais
la première conséquence territoriale réelle reste réservée à un jalon séparé.
