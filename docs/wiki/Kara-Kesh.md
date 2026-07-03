# Kara kesh des Grands Maîtres Goa'uld

> Statut : Implémenté
> Première version : 0.3.57-dev
> Activation biologique : 0.3.58-dev
> Onde cinétique : 0.3.59-dev
> Attaque neurale : 0.3.60-dev

## Fonctionnement

Les Grands Maîtres Goa'uld générés portent un kara kesh, gant alimenté au
naquadah et commandé par interface neurale. Les hôtes Goa'uld ordinaires et les
Jaffa n'en reçoivent pas.

Le même appareil réunit désormais trois fonctions :

- un bouclier personnel ;
- une onde cinétique focalisée ;
- une attaque neurale temporaire contre les humanoïdes biologiques.

Toutes exigent des traces biologiques persistantes de naquadah et partagent la
même réserve d'énergie.

## Bouclier personnel

Le champ absorbe des tirs soutenus et les éclats tant qu'il lui reste de
l'énergie. Il ne bloque ni les attaques de mêlée ni la chaleur, empêche son
porteur de tirer vers l'extérieur et s'effondre immédiatement sous une attaque
IEM. Après épuisement, un délai précède sa remise en service à pleine charge.

Sa capacité permet d'absorber environ `400` dégâts à distance à pleine charge.
Chaque impact absorbé suspend sa recharge pendant environ cinq secondes. Des
tirs soutenus peuvent donc épuiser la réserve, tandis que des tirs isolés
laissent au champ le temps de récupérer rapidement.

Lorsque le porteur est sélectionné, le gizmo vanilla `Énergie du bouclier`
permet de suivre sa charge et son temps de récupération.

## Onde cinétique

Un porteur compatible dispose d'un gizmo `Onde cinétique`. Il peut cibler un
pawn hostile à moins de `10,9` cases avec une ligne de vue dégagée.

Chaque utilisation consomme `1,25` point d'énergie, suspend brièvement la
recharge et impose un délai de `900` ticks. Elle inflige `12` dégâts
contondants avec `25 %` de pénétration d'armure, étourdit brièvement la cible et
la repousse jusqu'à deux cases si le terrain est libre.

Le recul s'arrête avant un mur, une case occupée ou le bord de la carte. Les
Grands Maîtres hostiles peuvent employer automatiquement la même capacité.

## Attaque neurale

Un porteur compatible reçoit aussi le gizmo `Attaque neurale`. Il cible un pawn
humanoïde biologique, hostile et conscient, à moins de `8,9` cases avec une
ligne de vue dégagée.

L'attaque consomme `1,75` point d'énergie et impose un délai de `1200` ticks.
Pendant environ `600` ticks, la cible subit `douleur neurale du kara kesh` :

- `45 %` de douleur supplémentaire ;
- Conscience multipliée par `80 %` ;
- aucune blessure directe ;
- aucun recul ;
- aucune paralysie automatique.

Un personnage déjà blessé ou affaibli peut tomber à terre par les règles
normales de douleur ou de Conscience, mais la capacité ne fixe jamais
directement son Déplacement à zéro. Les animaux, mécanoïdes, cibles à terre et
alliés ne sont pas des cibles valides.

Les Grands Maîtres hostiles privilégient cette attaque contre un ennemi
humanoïde proche, puis conservent l'onde cinétique comme solution de repli.

## Acquisition et recherche

Le kara kesh n'est proposé par aucun marchand et n'apparaît pas comme objet
aléatoire. Vaincre ou capturer un véritable Grand Maître constitue sa source
naturelle rare.

Un exemplaire récupéré peut être porté immédiatement, mais il ne s'active que
pour un pawn possédant des
[traces biologiques persistantes de naquadah](Biological-Naquadah-Traces).
Pour en fabriquer un au banc d'usinage, la colonie doit terminer :

```text
Armures Jaffa -> Kara kesh
```

La recette exige `Fabrication 12`, six composants avancés, `100` unités de
plastacier et `60` unités d'or.

## Visuel provisoire

La texture et les icônes de commandes restent provisoires. Elles seront
remplacées pendant la future passe visuelle globale sans changer l'identité de
l'objet dans les sauvegardes.

## Fonctions différées

La paralysie prolongée, la torture de prisonniers ou de cibles à terre, le
contrôle mental et les commandes à distance restent différés. Chaque fonction
doit conserver un coût, un contre-jeu et une couverture de tests propres.
