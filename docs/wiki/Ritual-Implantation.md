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

Le symbiote sélectionne automatiquement l'humanoïde compatible accessible le
plus proche dans ce rayon, disparaît puis transfère son identité persistante dans
l'état :

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

- la cible valide la plus proche est choisie automatiquement ;
- le joueur ne clique pas encore sur une cible précise ;
- aucune animation de cérémonie n'est encore présente ;
- aucune durée de rituel n'est encore appliquée ;
- aucune faction, salle, cuve ou structure n'est encore requise ;
- l'utilisation sur prisonnier n'est pas encore distinguée.

Ces éléments seront ajoutés après validation du transfert contrôlé.
