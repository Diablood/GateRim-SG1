# Âge requis pour le Prim'ta

> Statut : prototype jouable
> Première version : `0.1.33-dev`

## Présentation

L'implantation du Prim'ta est limitée par l'âge biologique du Jaffa.

## Seuil actuel

```text
10 ans biologiques
```

Ce seuil représente une approximation jouable de l'âge de Prata, lié au passage
vers la puberté.

## Fonctionnement

| Situation | Résultat |
|---|---|
| Jaffa compatible de moins de `10` ans | opération indisponible |
| Jaffa compatible âgé de `10` ans ou plus | opération disponible |
| Jaffa déjà porteur d'un Prim'ta | opération indisponible |

À partir de `12` ans, un Jaffa compatible sans Prim'ta développe une
[dépendance pubertaire](Primta-Puberty-Dependency) progressive. Une implantation
ou la trétonine permet alors de traiter ou suspendre ses effets.
