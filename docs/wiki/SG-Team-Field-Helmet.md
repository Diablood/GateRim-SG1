# Couvre-chefs de terrain SG

> Casque introduit : `0.2.0-dev-r2`
> Loadout variable : `0.3.21-dev`
> Casquette ajoutée : `0.3.22-dev`

## Présentation

Les équipes SG peuvent maintenant commencer avec l'un de deux couvre-chefs, ou sans couvre-chef.

### Casque de terrain SG

```text
casque de terrain SG
```

Le casque ouvert protège utilement le haut de la tête sans devenir une armure lourde. Il reste l'option adaptée aux missions les plus dangereuses.

Fabrication après la recherche **Équipement de terrain du SGC** :

```text
25 acier
15 tissu
Artisanat 4
```

### Casquette de terrain SG

```text
casquette de terrain SG
```

La casquette noire légère porte un insigne discret du SGC. Elle offre presque aucune protection, mais donne aux équipes SG une silhouette immédiatement reconnaissable et moins militarisée que le casque.

Fabrication après la recherche **Équipement de terrain du SGC** :

```text
20 tissu
Artisanat 2
```

## Intégration au scénario

Dans le scénario [Équipe SG isolée](Stranded-SG-Team-Scenario), chaque pawn effectue un tirage indépendant :

```text
30 % casque
30 % casquette
40 % aucun couvre-chef
```

Ces valeurs proviennent d'une chance globale de `0,6` pour le slot, puis d'un choix de poids égal entre le casque et la casquette.

Le casque et la casquette occupent le même emplacement et ne peuvent pas être portés simultanément.

## Compatibilité

Les deux couvre-chefs complètent :

- la [tenue de terrain SG](SG-Team-Uniform) ;
- les [variantes du treillis SG](SG-Team-Uniform-Variants) ;
- les [bottes tactiques SG](SG-Tactical-Boots) ;
- les [gants tactiques SG](SG-Tactical-Gloves) ;
- le [gilet tactique SG](SG-Tactical-Vest).

Leur sélection reste limitée au scénario culturel prévu. Les scénarios vanilla ne reçoivent aucun couvre-chef SG automatiquement.
