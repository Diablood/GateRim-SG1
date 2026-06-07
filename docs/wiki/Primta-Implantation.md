# Implantation du Prim'ta jaffa

> Statut : Prototype  
> Version d'introduction : 0.1.26-dev

## Présentation

Un Jaffa compatible peut désormais recevoir un Prim'ta grâce à une opération
médicale planifiable.

## Planifier l'opération

1. Sélectionne un Jaffa.
2. Ouvre l'onglet de santé.
3. Ouvre la liste des opérations.
4. Ajoute :

```text
implanter un Prim'ta jaffa
```

## Conditions actuelles

| Élément | Valeur |
|---|---:|
| Patient | Jaffa compatible |
| Compétence médicale minimale | `4` |
| Médicament | `1` unité |
| [Larve de Prim'ta](Primta-Larva) | `1` unité |
| Temps de travail | `900` |
| Prim'ta déjà présent | Opération indisponible |

## Compatibilité biologique

Le pawn doit porter les gènes hérités suivants :

```text
lignée jaffa
prédisposition à la poche jaffa
compatibilité avec le symbiote immature
```

## Résultat

Une réussite ajoute :

```text
symbiote du Prim'ta
```

Les bonus d'immunité, de guérison, de résistance, de longévité et de réduction
de douleur deviennent actifs.

## Larve physique requise

Depuis `0.1.27-dev`, l'opération consomme une
[larve de Prim'ta](Primta-Larva) physique en plus du médicament.

## Limites du prototype

- l'âge approprié n'est pas encore contrôlé ;
- la cérémonie traditionnelle n'est pas encore représentée ;
- l'absence prolongée de Prim'ta n'entraîne pas encore de dépendance ;
- la trétonine n'est pas encore disponible.
