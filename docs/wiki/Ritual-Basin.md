# Bassin rituel Goa'uld

> Statut : Prototype  
> Version d'introduction : 0.1.25-dev

## Présentation

Le bassin rituel Goa'uld est la première structure cérémonielle du mod.

Il sert de point d'ancrage aux implantations rituelles contrôlées, sans exiger le
DLC `Ideology`.

## Construction

| Élément | Valeur |
|---|---:|
| Catégorie | Mobilier |
| Taille | `1 × 1` |
| Acier | `60` |
| Or | `5` |
| Travail | `1200` |
| Points de vie | `180` |

Le visuel actuel est temporaire.

## Exigence rituelle

Pour commencer et maintenir un rituel :

```text
symbiote libre
    ↓ moins de 6 cases
bassin rituel Goa'uld
    ↑ moins de 6 cases
cible sélectionnée
```

La distance maximale habituelle entre le symbiote et sa cible reste de `12`
cases.

## Annulation automatique

La cérémonie est interrompue si :

```text
le bassin est détruit
le bassin est retiré de la carte
le symbiote s'éloigne trop du bassin
la cible s'éloigne trop du bassin
une condition précédente du rituel n'est plus respectée
```

Le symbiote libre reste disponible après l'annulation.

## Évolution future

Une intégration optionnelle avec `Ideology` pourra réutiliser ce bassin comme
point central d'un rituel plus riche : rôles, participants, qualité de cérémonie,
salle dédiée et objets thématiques.
