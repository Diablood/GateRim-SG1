# Cérémonie formelle du Prim'ta

> Statut : Prototype
> Version d'introduction : 0.1.38-dev

## Présentation

![Icône partagée de la cérémonie d'implantation](images/SG1_RitualImplantation.png)

Le bassin rituel Goa'uld permet désormais d'organiser une cérémonie formelle du
Prim'ta pour un Jaffa éligible.

Cette mécanique reste disponible sans le DLC Ideology.

## Bassin cérémoniel

![Bassin rituel Goa'uld](images/SG1_GoauldRitualBasin.png)

Les variantes `2 × 2` et `3 × 3` du bassin sont utilisables pour la cérémonie.
Elles partagent le même rendu visuel de taille `2 × 2` et la même icône.

## Conditions

```text
bassin rituel Goa'uld contrôlé par le joueur
    +
Jaffa éligible dans un rayon de 6 cases
    +
larve physique de Prim'ta dans un rayon de 6 cases
```

## Déroulement

1. Sélectionne le bassin rituel Goa'uld.
2. Clique sur `Cérémonie formelle du Prim'ta`.
3. Sélectionne un Jaffa éligible proche.
4. Maintiens le Jaffa et la larve près du bassin pendant `600` ticks.
5. Laisse le rite s'achever.

## Résultat

```text
1 larve consommée
    ↓
Prim'ta implanté
    ↓
déficience pubertaire retirée
    ↓
souvenir culturel accordé lors de la première implantation
```

## Annulation

La cérémonie peut être annulée manuellement.

Elle est aussi interrompue automatiquement si :

```text
la cible devient indisponible ou hors de portée
la larve est déplacée ou supprimée
le bassin est détruit ou n'est plus contrôlé par le joueur
```

Une cérémonie annulée ne consomme pas la larve.

## Évolutions prévues

Une intégration optionnelle avec Ideology pourra enrichir cette solution
provisoire avec des rôles, des exigences de lieu et une mise en scène rituelle
plus complète.
