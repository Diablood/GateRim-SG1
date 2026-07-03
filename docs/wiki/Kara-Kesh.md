# Kara kesh des Grands Maîtres Goa'uld

> Statut : Implémenté
> Première version : 0.3.57-dev
> Activation biologique : 0.3.58-dev

## Fonctionnement

Les Grands Maîtres Goa'uld générés portent désormais un kara kesh, le gant
alimenté au naquadah et commandé par interface neurale. Les hôtes Goa'uld
ordinaires et les Jaffa n'en reçoivent pas. Cette première version implémente
uniquement son mode bouclier personnel.

Le champ absorbe des tirs soutenus et les éclats tant qu'il lui reste de l'énergie.
Il ne bloque ni les attaques de mêlée ni la chaleur, empêche son porteur de
tirer vers l'extérieur et s'effondre immédiatement sous une attaque IEM. Après
épuisement, un délai précède sa remise en service à pleine charge. La rupture
produit un son, un flash et des fissures clairement visibles.

Sa capacité permet d'absorber environ `400` dégâts à distance à pleine charge.
Le but est de donner au Grand Maître la présence presque invulnérable qu'il
revendique face aux armes ordinaires, tout en conservant trois réponses nettes :
l'IEM, la mêlée et la chaleur.

Le bouclier Goa'uld est meilleur que la ceinture-bouclier vanilla dans tous ses
paramètres principaux : capacité, coût énergétique des impacts, recharge et
délai de retour. Lorsque le porteur est sélectionné, le gizmo vanilla `Énergie
du bouclier` permet de suivre sa charge et son temps de récupération.

Chaque impact absorbé suspend sa recharge pendant environ cinq secondes. Des
tirs soutenus peuvent donc finir par épuiser la réserve, tandis que des tirs
isolés laissent au champ le temps de récupérer rapidement.

## Acquisition et recherche

Le bouclier n'est pas proposé par les marchands et n'apparaît pas comme objet
aléatoire. Vaincre ou capturer un véritable Grand Maître constitue la source
naturelle rare.

Un exemplaire récupéré peut être porté immédiatement, mais son champ ne
s'active que pour un pawn possédant des [traces biologiques persistantes de
naquadah](Biological-Naquadah-Traces). Pour en fabriquer au banc d'usinage, la
colonie doit terminer :

```text
Armures Jaffa -> Kara kesh
```

La recette exige `Fabrication 12`, six composants avancés, `100` unités de
plastacier et `60` unités d'or.

## Visuel provisoire

La texture actuelle est un placeholder conservé sur le chemin définitif de
l'objet. Elle sera remplacée pendant la future passe visuelle globale sans
changer l'identité du bouclier dans les sauvegardes.

## Fonctions différées

Le kara kesh possède également dans le lore une onde cinétique, une attaque
neurale, une fonction de paralysie et diverses commandes à distance. Ces
fonctions ne sont pas simulées par le prototype actuel et seront étudiées
séparément afin de ne pas transformer un seul objet en solution universelle.

Son activation est désormais réservée aux personnes portant des traces
biologiques persistantes de naquadah : hôtes Goa'uld ou Tok'ra, Jaffa porteurs
d'un Prim'ta et anciens hôtes. Un pawn non compatible peut transporter ou porter
le gant, mais le champ et son indicateur d'énergie restent inactifs.
