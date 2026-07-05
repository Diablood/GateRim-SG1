# Progression des menaces Goa'uld

> Première version : `0.3.53-dev`
> Réduction des conflits ouverts : `0.3.68-dev`

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

Sous **Commandement SG-1**, la version `0.3.68-dev` ajoute une seule exception
bornée : un domaine engagé dans un conflit ouvert utilise ensuite `75 %` de ces
points pour la force de son raid naturel. La doctrine est toujours choisie avec
la valeur vanilla initiale.

Cette réduction :

- ne se cumule pas avec plusieurs conflits ;
- ne modifie pas la fréquence des raids ;
- ne concerne pas les représailles après extraction ;
- ne concerne pas les missions, sites ou raids de test ;
- disparaît avec un autre storyteller ou lorsque le conflit ouvert prend fin.
