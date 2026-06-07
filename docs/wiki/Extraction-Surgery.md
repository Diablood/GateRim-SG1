# Chirurgie d'extraction Goa'uld

> Statut : Prototype  
> Version d'introduction : 0.1.20-dev

## Présentation

Une victime récemment implantée peut désormais recevoir une véritable opération
médicale afin de retirer le parasite avant la prise de contrôle définitive.

## Planifier l'opération

1. Sélectionne la victime.
2. Ouvre l'onglet de santé.
3. Ouvre la liste des opérations.
4. Ajoute :

```text
extraction d'urgence Goa'uld
```

## Conditions actuelles

| Élément | Valeur |
|---|---:|
| Compétence médicale minimale | `6` |
| Médicament | `1` unité |
| Temps de travail | `1800` |
| Facteur de réussite chirurgicale | `0,85` |
| Risque de mort en cas d'échec | `2 %` |
| Phase concernée | Implantation récente uniquement |

## Réussite

Lorsqu'elle réussit :

```text
implantation récente supprimée
    ↓
même symbiote libre généré à proximité
    ↓
même identifiant persistant
```

## Échec

En cas d'échec, l'état d'implantation récente reste actif. Le compte à rebours
continue donc vers la conversion en [hôte Goa'uld actif](Active-Goauld-Host).

## Commande instantanée

La commande immédiate [Extraction d'urgence](Emergency-Extraction) reste
temporairement disponible pour faciliter les tests de développement. La
chirurgie constitue désormais le parcours joueur prévu.
