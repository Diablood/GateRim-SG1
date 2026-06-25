# Chirurgies d'extraction Goa'uld

> Statut : jouable
> Extraction d'urgence : 0.1.20-dev
> Extraction d'un hôte actif : 0.3.41-dev

## Deux opérations distinctes

Le moment de l'intervention détermine la difficulté.

### Implantation récente

```text
extraction d'urgence Goa'uld
```

Cette opération doit être planifiée avant la fin du compte à rebours d'une journée.

| Élément | Valeur |
|---|---:|
| Compétence médicale minimale | `6` |
| Médicament | `1` unité |
| Temps de travail | `1800` |
| Facteur de réussite chirurgicale | `0,85` |
| Risque de mort en cas d'échec | `2 %` |

### Hôte Goa'uld actif

```text
extraire le symbiote Goa'uld actif
```

Cette opération n'apparaît que si l'hôte Goa'uld est contrôlé par le joueur ou détenu comme prisonnier de la colonie. Un ancien colon passé à l'ennemi doit donc être mis à terre et capturé avant toute tentative. Sa capture le retire du groupe d'assaut Goa'uld et empêche sa réassignation tant qu'il reste détenu.

| Élément | Valeur |
|---|---:|
| Compétence médicale minimale | `10` |
| Médicament | `3` unités |
| Temps de travail | `4200` |
| Facteur de réussite chirurgicale | `0,75` |
| Risque de mort en cas d'échec | `5 %` |
| Tok'ra | jamais éligibles |

## Réussite sur un hôte actif

Une réussite :

- retire l'état d'hôte Goa'uld actif ;
- restaure la faction du colon déplacé par la prise de contrôle ;
- conserve exactement le même pawn, son corps, ses relations, son équipement, son xenotype et ses blessures ;
- libère le même symbiote avec son identifiant et son allégeance d'origine ;
- anesthésie temporairement le symbiote extrait afin qu'il ne réimplante pas immédiatement le patient ou le chirurgien.

Le symbiote reste vivant et dangereux lorsqu'il se réveille.
L'opération ne le tue pas automatiquement et n'ajoute aucun confinement spécial : la colonie doit donc sécuriser ou éliminer le Goa'uld avant la fin de l'anesthésie.

## Échec

Les conséquences suivent le système normal des chirurgies RimWorld. Si le patient survit, le symbiote actif reste en place et aucune copie libre n'est créée.

## Planifier l'opération

1. Place le patient dans un lit permettant les soins.
2. Ouvre son onglet Santé.
3. Ouvre la liste des opérations.
4. Choisis l'opération correspondant à son état actuel.
5. Prépare le médecin et les médicaments nécessaires.

La commande instantanée [Extraction d'urgence](Emergency-Extraction) est uniquement un outil de développement depuis `0.3.41-dev`.
