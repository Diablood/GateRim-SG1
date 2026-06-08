# Larve de Prim'ta

> Statut : Prototype  
> Version d'introduction : 0.1.27-dev

## Présentation

La larve de Prim'ta est un symbiote Goa'uld immature transportable. Elle sert
d'ingrédient physique à l'opération d'implantation destinée aux Jaffa.

## Objet

```text
larve de Prim'ta
```

## Propriétés actuelles

| Élément | Valeur |
|---|---:|
| Type | Ressource transportable |
| Limite de pile | `10` |
| Masse | `0,1` |
| Valeur marchande | `35` |
| Obtention jouable | [Bassin d'incubation](Primta-Incubation) |
| Obtention pour les tests | Mode développeur toujours disponible |

Le visuel actuel est temporaire.

## Utilisation

L'opération :

```text
implanter un Prim'ta jaffa
```

demande désormais :

```text
1 larve de Prim'ta
1 médicament
```

La larve est transportée jusqu'au patient puis consommée par l'opération.

## Évolutions prévues

- nutriments biologiques et équilibrage de l'incubation ;
- culture ou élevage de larves ;
- conservation et contraintes de stockage ;
- maturation en symbiote adulte ;
- cérémonie liée à l'âge du Jaffa ;
- dépendance et trétonine.


## Bassin d'incubation

Depuis `0.1.28-dev`, construis un
[bassin d'incubation du Prim'ta](Primta-Incubation), puis ajoute la tâche :

```text
incuber une larve de Prim'ta
```

Le premier prototype exige uniquement du travail.


## Nutriments d'incubation

Depuis `0.1.29-dev`, le [bassin d'incubation](Primta-Incubation) consomme :

```text
10 unités de viande crue
```

pour produire une larve.


## Conservation

Depuis `0.1.30-dev`, les larves de Prim'ta sont périssables.

```text
stockage chaud
    ↓
détérioration progressive
    ↓
larve détruite si elle pourrit complètement
```

Un stockage froid est recommandé avant l'implantation. Cette première version
utilise le système vanilla de pourrissement avec une durée de `6` jours.
