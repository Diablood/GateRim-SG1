# Trétonine

> Statut : Prototype  
> Version d'introduction : 0.1.36-dev

## Présentation

La trétonine constitue une première alternative médicale temporaire au Prim'ta
pour les Jaffa ayant atteint la puberté.

## Ressource physique

```text
dose de trétonine
```

| Élément | Valeur |
|---|---:|
| Limite de pile | `25` |
| Masse | `0,02` |
| Catégorie | Produits médicaux Goa'uld |
| Obtention actuelle | [Production au laboratoire de drogues](Tretonin-Production) ou mode développeur |

La dose n'est pas classée comme médicament vanilla générique.

## Administration

Depuis l'onglet Santé, planifie :

```text
administrer de la trétonine
```

Conditions :

```text
Jaffa compatible
12 ans biologiques ou plus
aucun Prim'ta implanté
aucune substitution déjà active
1 dose physique de trétonine
Médecine 2+
```

## Effet

```text
dose administrée
    ↓
substitution par trétonine pendant 1 jour
    ↓
déficience retirée immédiatement
```

Lorsque l'effet expire, la déficience peut réapparaître lors du prochain contrôle
horaire si le Jaffa n'a toujours pas reçu de Prim'ta.

## Limites actuelles

Cette première version ne gère pas encore :

```text
production naturelle
administration automatique
politiques de drogues
tolérance
effets secondaires
approvisionnement des Jaffa libres
```


## Production

Depuis `0.1.37-dev`, prépare les doses au laboratoire de drogues vanilla :

```text
1 larve de Prim'ta
    +
1 médicament
    ↓
5 doses de trétonine
```

Consulte [Production de trétonine](Tretonin-Production).
