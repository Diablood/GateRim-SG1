# Storyteller GateRim SG-1

> Première version : `0.3.65-dev`
> Dernière évolution : `0.3.66-dev`
> Statut : publié

**Commandement SG-1** est un storyteller optionnel qui conserve un rythme
classique tout en coordonnant les systèmes stratégiques propres au mod.

## Rythme ordinaire

Il reprend la définition Cassandra Classique actuellement résolue par RimWorld :

- montée progressive de la pression ;
- périodes de répit ;
- mêmes contrats d'incidents et même adaptation de difficulté ;
- aucun remplacement figé du XML de Core.

Les incidents GateRim déjà publiés restent également utilisables avec les
autres storytellers compatibles.

## Relations entre domaines Goa'uld

Depuis `0.3.66-dev`, chaque paire de domaines Goa'uld peut conserver l'un des
états suivants :

- neutralité ;
- rivalité ;
- conflit ouvert ;
- trêve ;
- alliance.

La relation appartient aux factions elles-mêmes. Elle survit donc au
remplacement d'un Grand Maître et à la sauvegarde/recharge.

Les changements sont lents et produisent des rapports RP nommant les deux
domaines. Plusieurs variantes de texte limitent les répétitions immédiates.

Une partie ne contenant qu'un seul domaine Goa'uld ne possède aucune paire à
faire évoluer. Ajouter plusieurs instances de la faction lors de la création du
monde permet d'utiliser cette simulation naturellement.

## Choix facultatif

La progression automatique des relations fonctionne uniquement avec
**Commandement SG-1**.

Avec Cassandra, Phoebe, Randy ou un storyteller compatible :

- les relations déjà enregistrées sont conservées ;
- aucun nouvel état n'est tiré ;
- aucune lettre relationnelle n'est produite ;
- les échéances sont repoussées pendant toute la suspension ;
- revenir à Commandement SG-1 reprend la simulation sans déclencher un retard
  accumulé ;
- la cadence du storyteller choisi n'est jamais modifiée par GateRim.

## Limites actuelles

Les états sont pour l'instant politiques et narratifs. Ils ne modifient pas
encore :

- la fréquence ou la puissance des raids ;
- les doctrines propres à chaque domaine ;
- les représailles après extraction ;
- le territoire ou les colonies mondiales ;
- les relations diplomatiques avec la colonie.

Les batailles entre domaines, renforts d'alliance, réductions de pression et
autres conséquences militaires restent prévues pour des jalons séparés après
validation de cette fondation persistante.


## Validation

La révision finale `r1` a validé les cinq états, les rapports français, la
persistance après sauvegarde, la suspension sous Cassandra, la reprise sous
Commandement SG-1, l'anti-répétition et l'absence de régression des systèmes
Goa'uld existants.
