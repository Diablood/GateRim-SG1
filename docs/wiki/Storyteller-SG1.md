# Storyteller GateRim SG-1

> Première version : `0.3.65-dev`
> Dernière évolution : `0.3.70-dev`
> Statut : site mondial validé en révision finale `r4`

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

## Pression réduite en conflit ouvert

Depuis `0.3.68-dev`, un domaine engagé dans au moins un conflit ouvert utilise
`75 %` de ses points habituels pour ses raids naturels de Jaffa contre la
colonie.

La réduction :

- ne se cumule pas contre plusieurs rivaux ;
- ne modifie ni fréquence ni doctrine ;
- conserve les points initiaux pour l'éligibilité des doctrines ;
- exclut représailles, missions, sites hostiles et tests forcés.

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
- les échéances futures sont repoussées pendant la suspension ;
- revenir à Commandement SG-1 ne déclenche pas de retard accumulé ;
- les raids naturels utilisent de nouveau `100 %` des points ;
- la cadence du storyteller choisi n'est jamais modifiée.

## Limites actuelles

Les relations ne provoquent pas encore :

- de renforts ou raids conjoints en alliance ;
- d'augmentation liée aux alliances ;
- de modification territoriale ;
- de destruction de colonies mondiales ;
- de changement diplomatique avec le joueur.

Ces conséquences restent réservées à des jalons séparés.
