# Extraction d'urgence Goa'uld

> Statut : Prototype  
> Version d'introduction : 0.1.19-dev

## Présentation

Pendant la phase critique d'implantation récente, le joueur peut désormais
interrompre manuellement la prise de contrôle.

## Utilisation actuelle

1. Applique une implantation forcée à un humanoïde adulte.
2. Sélectionne la victime avant la fin du compte à rebours.
3. Clique sur :

```text
Extraction d'urgence
```

Le symbiote quitte sa victime et réapparaît sous la forme d'un pawn libre à
proximité.

## Identité persistante

L'identifiant du parasite reste identique :

```text
symbiote libre
    ↓ implantation forcée
implantation récente
    ↓ extraction d'urgence
symbiote libre
```

Le pawn extrait peut être implanté à nouveau.

## Limites du prototype

- l'action est immédiate ;
- aucune compétence médicale n'est encore requise ;
- aucun médicament n'est consommé ;
- il n'existe pas encore de risque d'échec ;
- l'extraction d'un hôte Goa'uld déjà actif n'est pas encore disponible.

Une future opération médicale remplacera ou complétera cette commande de test.


## Chirurgie disponible

Depuis `0.1.20-dev`, la [chirurgie d'extraction](Extraction-Surgery) constitue le
parcours joueur principal. La commande immédiate reste temporairement disponible
pour faciliter les tests de développement.
