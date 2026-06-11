# Raid d'enlèvement Jaffa Goa'uld contrôlé

> Statut : Prototype
> Version d'introduction : 0.1.71-dev

## Présentation

Ce second incident développeur teste une doctrine Goa'uld consacrée à la
capture de colons.

```text
raid d'enlèvement contrôlé de Jaffa Goa'uld
```

Sa chance storyteller reste fixée à :

```text
0
```

Aucun raid naturel Goa'uld n'est activé.

## Déclenchement

Active le mode développeur puis utilise :

```text
Do incident (Map)
└── raid d'enlèvement contrôlé de Jaffa Goa'uld
```

La variante `Do incident (points)` permet de tester un groupe plus important.

## Comportement

Les Jaffa commencent par un assaut normal. Dès qu'un colon à terre peut être
récupéré sans danger immédiat par un Jaffa proche, une fenêtre de capture de
`2400` ticks démarre :

- les Jaffa disponibles tentent d'emporter les colons à terre proches ;
- les autres continuent leur assaut ;
- de nouvelles victimes peuvent être prises en charge pendant la fenêtre.

Une fois ce délai écoulé, les troupes survivantes se replient avec ou sans
victime.

Si aucun colon ne devient récupérable, un délai maximal séparé de `12000`
ticks impose tout de même le repli afin d'éviter un combat sans fin.

## Différence avec le raid d'assaut contrôlé

| Incident | Vol | Enlèvement | Objectif |
|---|---|---|---|
| Raid d'assaut contrôlé | non | non | combat prolongé |
| Raid d'enlèvement contrôlé | non | oui | captures multiples puis repli |

## Limites

Les raids naturels, colonies et marchands Goa'uld restent désactivés. La
future doctrine de destruction avec pillage opportuniste après victoire sera
ajoutée séparément.

## Déclenchement pendant le combat

La fenêtre de capture s'ouvre dès qu'une victime à terre proche est récupérable,
même si le combat est encore actif. La logique vanilla choisit ensuite seulement
les Jaffa suffisamment disponibles pour emporter une victime. Les autres
continuent l'assaut.
