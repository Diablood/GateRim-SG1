# Implantation rituelle Goa'uld

> Statut : Prototype  
> Version d'introduction : 0.1.22-dev

## Présentation

Le symbiote Goa'uld libre dispose désormais d'une voie d'implantation contrôlée,
distincte de sa chasse autonome.

## Utilisation actuelle

1. Génère ou récupère un `symbiote Goa'uld`.
2. Désactive `Chasse autonome` pour préparer calmement le test.
3. Place un humanoïde adulte compatible dans un rayon de `12` cases.
4. Sélectionne le symbiote libre.
5. Clique sur :

```text
Implantation rituelle
```

Le jeu ouvre désormais un curseur de ciblage sur la carte. Clique sur l'humanoïde
compatible accessible de ton choix dans ce rayon. Le symbiote disparaît puis
transfère son identité persistante dans l'état :

```text
implantation Goa'uld récente
```

## Identité persistante

L'identifiant reste identique avant et après le rituel :

```text
symbiote libre
    ↓ implantation rituelle
implantation récente
    ↓
même identifiant persistant
```

## Limites du prototype

- aucune animation de cérémonie n'est encore présente ;
- aucune durée de rituel n'est encore appliquée ;
- aucune faction, salle, cuve ou structure n'est encore requise ;
- l'utilisation sur prisonnier n'est pas encore distinguée.

Ces éléments seront ajoutés après validation du transfert contrôlé.


## Sélection explicite de la cible

Depuis `0.1.23-dev`, la commande `Implantation rituelle` ouvre un curseur sur la
carte. Clique directement sur le pawn à implanter.

Le pawn doit être compatible, accessible, vivant, âgé d'au moins `13` ans et
situé dans le rayon de `12` cases.
