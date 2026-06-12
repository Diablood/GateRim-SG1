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
carte. Clique directement sur la cible à implanter.

La cible doit être compatible, accessible, vivant, âgé d'au moins `13` ans et
situé dans le rayon de `12` cases.


## Cérémonie temporisée

Depuis `0.1.24-dev`, l'implantation rituelle n'est plus instantanée.

Après avoir choisi la cible, une cérémonie de `600` ticks commence. Le panneau
d'inspection du symbiote affiche la cible et le temps restant.

La cible doit rester :

```text
vivante
compatible
accessible
dans le rayon de 12 cases
```

Le rituel peut être annulé manuellement avec `Annuler le rituel`. Il est aussi
interrompu automatiquement si les conditions ne sont plus remplies.

La progression est conservée lors d'une sauvegarde et reprend après rechargement.

## Intégrations DLC futures

Ce rituel de base fonctionne sans `Ideology`.

Une intégration optionnelle pourra ultérieurement exploiter les rituels du DLC
pour ajouter une idéologie Goa'uld, des rôles, des participants, des lieux et
des objets cérémoniels tout en réutilisant le même transfert persistant.


## Bassin rituel requis

Depuis `0.1.25-dev`, une [bassin rituel Goa'uld](Ritual-Basin) doit se trouver à
proximité du symbiote et de la cible.

Les deux doivent rester à moins de `6` cases du même bassin pendant toute la
cérémonie. La destruction du bassin ou l'éloignement d'un participant annule le
rituel.
