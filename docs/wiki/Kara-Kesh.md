# Kara kesh des Grands Maîtres Goa'uld

> Statut : Implémenté
> Première version : 0.3.57-dev
> Activation biologique : 0.3.58-dev
> Onde cinétique : 0.3.59-dev
> Attaque neurale : 0.3.60-dev
> Maintien paralysant : 0.3.61-dev

## Fonctionnement

Les Grands Maîtres Goa'uld générés portent un kara kesh, gant alimenté au
naquadah et commandé par interface neurale. Les hôtes Goa'uld ordinaires et les
Jaffa n'en reçoivent pas.

Le même appareil réunit désormais quatre fonctions :

- un bouclier personnel ;
- une onde cinétique focalisée ;
- une attaque neurale temporaire contre les humanoïdes biologiques ;
- un maintien paralysant mono-cible, dépendant de la portée et de la ligne de vue.

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

## Maintien paralysant

La version publiée `0.3.61-dev` ajoute un gizmo
`Maintien paralysant`. Il
cible un pawn humanoïde biologique, hostile et conscient, à moins de `6,9`
cases avec une ligne de vue dégagée.

L'activation consomme `2,5` points de la réserve commune et lance un cooldown de
`1800` ticks. Pendant au maximum `600` ticks, la cible reçoit
`maintien paralysant du kara kesh` :

- Déplacement limité à `0` ;
- Manipulation réduite à `10 %` ;
- aucune blessure directe ;
- aucune douleur ajoutée.

Le lien est réellement maintenu. Il s'interrompt rapidement si le porteur tombe
à terre, meurt, retire le kara kesh, perd ses traces de naquadah, subit une
réinitialisation du bouclier, quitte la carte, dépasse la portée, perd la ligne
de vue ou cesse d'être hostile à la cible. Le joueur peut aussi utiliser
`Relâcher le maintien paralysant` ; l'énergie n'est pas remboursée et le
cooldown continue.

Pendant le maintien, le bouclier ne recharge pas et les gizmos d'onde cinétique
et d'attaque neurale sont indisponibles. Les Grands Maîtres hostiles privilégient
ce contrôle avant leurs deux autres modes. Le rebuild, les interruptions, l'IA,
la sauvegarde/recharge, le relâchement et `Player.log` ont été validés sur `r1`.

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

## Idées non planifiées

Les autres fonctions spéculatives ne sont ni implémentées ni promises. Elles
restent de simples pistes à réexaminer séparément dans
`docs/IDEAS_TO_REVISIT.md`.
