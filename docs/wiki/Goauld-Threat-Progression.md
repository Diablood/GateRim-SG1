# Progression des menaces Goa'uld

> Première version : `0.3.53-dev`
> Réduction des conflits ouverts : `0.3.68-dev`
> Bonus borné des alliances : `0.3.73-dev`
> Renforts alliés différés : `0.3.78-dev`
> Raids conjoints coordonnés : `0.3.79-dev`

Les raids, garnisons et renforts Goa'uld partent des points de menace calculés
par RimWorld. Ces points tiennent compte de la puissance de la colonie et des
réglages du storyteller actif.

Les conséquences principales sont les suivantes :

- un raid naturel ou annoncé ne reste plus bloqué à une petite force fixe en
  fin de partie ;
- les sites de combat conservent leurs multiplicateurs propres mais ne sont
  plus plafonnés aux effectifs de début de partie ;
- le relais Goa'uld mémorise sa difficulté avant le voyage et peut devenir un
  bunker, une station divisée ou une cour fortifiée selon la menace ;
- les colonies Goa'uld continuent d'utiliser la génération vanilla des
  établissements ;
- les troupes Goa'uld/Jaffa arrivent à pied depuis le bord de carte, sans pods
  vanilla même lorsque leur budget de menace devient élevé ;
- les incursions de symbiotes restent volontairement limitées à quatre, car
  chaque implantation peut créer une menace persistante bien plus importante
  qu'un combattant ordinaire.

Depuis `0.3.54-dev`, l'unique incident naturel utilise les points vanilla pour
rendre l'enlèvement et la destruction progressivement admissibles. Leur ajout
ne crée aucun tirage storyteller ni délai indépendant.

Sous **Commandement SG-1**, une conséquence bornée est appliquée seulement après
le choix de la doctrine :

- un domaine engagé dans au moins un conflit ouvert utilise `75 %` des points ;
- sinon, un domaine engagé dans au moins une alliance utilise `110 %` ;
- sinon, le raid naturel conserve `100 %`.

Plusieurs conflits ou alliances ne se cumulent pas. Le conflit ouvert est
prioritaire lorsqu'un même domaine possède les deux types de relation. Les
points initiaux restent utilisés pour l'éligibilité et les poids des doctrines.

À partir de `800` points finaux, un raid bénéficiant du facteur allié peut
répartir ce même total entre une force principale à `75 %` et une vague d'un
second domaine à `25 %`. Cette vague arrive plus tard à pied et n'est révélée
qu'au moment de son entrée. Elle n'ajoute aucun multiplicateur ni tirage
storyteller.

Depuis `0.3.79-dev`, la moitié des raids admissibles reste entièrement standard.
Pour un assaut direct coopératif, l'autre moitié se partage entre le renfort
différé `75/25` et un raid conjoint simultané `60/40`. Une lettre politique
d'alliance ne lance donc aucun raid et ne permet pas de prévoir sa forme.

Ces facteurs :

- ne modifient pas la fréquence des raids ;
- ne concernent pas les représailles après extraction ;
- ne concernent pas les missions, sites ou raids de test exacts ;
- disparaissent avec un autre storyteller ;
- sont recalculés depuis les relations persistantes ; seule une vague différée
  déjà planifiée sérialise temporairement son arrivée et ses participants.
