# Storyteller GateRim SG-1

> Première version : `0.3.65-dev`
> Dernière évolution : `0.3.69-dev`
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

## Pression réduite en conflit ouvert

La version `0.3.68-dev` ajoute la première conséquence mécanique de ces
relations.

Sous **Commandement SG-1**, un domaine engagé dans au moins un conflit ouvert
utilise `75 %` de ses points de menace habituels pour ses raids naturels de
Jaffa contre la colonie.

Cette réduction reste volontairement limitée :

- elle ne se cumule pas lorsque le domaine affronte plusieurs rivaux ;
- elle ne rend pas les raids plus ou moins fréquents ;
- elle ne change pas la préférence de doctrine du domaine ;
- les conditions d'assaut direct, d'enlèvement ou de destruction utilisent
  toujours les points initiaux calculés par RimWorld ;
- les représailles après extraction ne sont pas réduites ;
- les missions, sites hostiles et raids de test restent inchangés.

Le conflit détourne donc une partie des moyens du domaine sans neutraliser
complètement sa menace envers le joueur.

## Batailles locales en conflit ouvert

Depuis `0.3.69-dev`, un conflit ouvert peut aussi produire une
[bataille entre deux domaines](Goauld-Open-Conflict-Battlefields) près d'une
colonie joueur.

Les deux détachements jaffa appartiennent aux domaines exacts concernés. Ils
entrent depuis le bord de la carte, rejoignent des positions de ralliement,
puis un message annonce leur assaut mutuel. La colonie peut rester à l'écart ou
intervenir contre un camp ou contre les deux. Un camp provoqué riposte localement,
mais abandonne la poursuite après `1800` ticks sans nouvelle attaque, au-delà de
`35` cellules, ou après `6000` ticks de riposte pendant son retrait.

## Choix facultatif

La progression automatique des relations, la réduction de pression et les
nouvelles opportunités de bataille fonctionnent uniquement avec
**Commandement SG-1**.

Avec Cassandra, Phoebe, Randy ou un storyteller compatible :

- les relations déjà enregistrées sont conservées ;
- aucun nouvel état n'est tiré ;
- aucune lettre relationnelle n'est produite ;
- les échéances sont repoussées pendant toute la suspension ;
- revenir à Commandement SG-1 reprend la simulation sans déclencher un retard
  accumulé ;
- les raids naturels utilisent de nouveau `100 %` des points calculés ;
- la cadence du storyteller choisi n'est jamais modifiée par GateRim.

## Limites actuelles

Les relations ne provoquent pas encore :

- de site de bataille sur la carte mondiale ;
- de renforts ou raids conjoints en alliance ;
- d'augmentation de fréquence ou de puissance liée aux alliances ;
- de modification territoriale ou de destruction de colonies mondiales ;
- de changement diplomatique avec la colonie.

Ces conséquences restent réservées à des jalons séparés.

## Validation

Le facteur `75 %` reste validé depuis `0.3.68-dev`. La bataille locale de
`0.3.69-dev-r6` doit maintenant valider le combat mutuel, la riposte bornée,
la rupture morale à `30%`, le retrait, la sauvegarde/recharge et l'absence de
régression.
