# Âge requis pour le Prim'ta

> Statut : Prototype  
> Version d'introduction : 0.1.33-dev

## Présentation

L'implantation du Prim'ta est désormais limitée par l'âge biologique du Jaffa.

## Seuil actuel

```text
10 ans biologiques
```

Ce seuil représente la première approximation jouable de l'âge de Prata, lié
au passage vers la puberté.

## Fonctionnement

| Situation | Résultat |
|---|---|
| Jaffa compatible de moins de `10` ans | Opération indisponible |
| Jaffa compatible âgé de `10` ans ou plus | Opération disponible |
| Jaffa déjà porteur d'un Prim'ta | Opération indisponible |

## Limites du prototype

La dépendance progressive à partir de la puberté n'est pas encore implémentée.

Une future étape pourra ajouter :

```text
Jaffa atteignant la puberté sans Prim'ta
    ↓
déficience immunitaire progressive
    ↓
urgence médicale
    ↓
implantation ou traitement à la trétonine
```


## Dépendance à partir de 12 ans

Depuis `0.1.34-dev`, un Jaffa compatible âgé de `12` ans ou plus sans Prim'ta
développe une [dépendance pubertaire](Primta-Puberty-Dependency) progressive.
