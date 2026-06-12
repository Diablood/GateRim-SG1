# Raid de destruction Jaffa Goa'uld contrôlé

> Statut : Prototype
> Version d'introduction : 0.1.72-dev

## Présentation

Ce troisième incident développeur teste une doctrine Goa'uld de destruction.

```text
raid de destruction contrôlé de Jaffa Goa'uld
```

Sa chance storyteller reste fixée à :

```text
0
```

Aucun raid naturel Goa'uld n'est activé.

## Phase militaire

Les Jaffa commencent par un assaut prolongé. Ils ne tentent pas de voler un
objet ni d'enlever une victime pendant cette phase.

La phase militaire se termine lorsque :

```text
la colonie a subi suffisamment de dégâts
ou
12000 ticks se sont écoulés
```

## Récupération opportuniste

Pendant les `2400` ticks suivants :

- les Jaffa disponibles donnent la priorité aux colons à terre proches ;
- ils peuvent sinon emporter des objets de valeur proches ;
- les autres continuent le combat pour couvrir la récupération.

Une fois le délai écoulé, les survivants quittent la carte avec ou sans
captif ou butin.

## Différence avec les autres prototypes

| Incident | Objectif |
|---|---|
| Assaut contrôlé | combat prolongé sans extraction opportuniste |
| Enlèvement contrôlé | captures déclenchées dès la première victime admissible |
| Destruction contrôlée | dégâts militaires d'abord, récupération opportuniste ensuite |

## Limites

Les colonies mondiales et les rares assauts directs naturels sont actifs depuis
`0.2.1-dev`. Cette doctrine de destruction reste réservée aux outils
développeur. Les seuils de dégâts et les délais sont encore des valeurs de
prototype.
