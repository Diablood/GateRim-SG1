# Présence mondiale masquée Tok'ra

> Statut : Première base jouable  
> Première version : 0.2.7-dev

## Présentation

Les Tok'ra disposent désormais d'une présence mondiale persistante, mais
volontairement masquée.

```text
1 faction Tok'ra persistante
0 colonie mondiale classique
0 raid naturel
```

Cette structure représente un réseau clandestin de cellules plutôt qu'une
civilisation territoriale visible sur la carte.

## Nouvelle partie

Chaque nouveau monde reçoit une instance unique de faction :

```text
Tok'ra
```

Elle n'apparaît pas dans la liste configurable des factions et ne place aucune
colonie.

## Anciennes sauvegardes

Une sauvegarde créée avant `0.2.7-dev` reçoit automatiquement la même présence
masquée si elle n'existe pas encore.

Si une ancienne partie avait déjà déclenché une visite ou une livraison
médicale Tok'ra, l'instance existante est simplement réutilisée.

## Événements raccordés

La présence persistante sert désormais de point commun à :

- [visiteurs Tok'ra pacifiques](Tokra-Peaceful-Visitors) ;
- [opportunités thérapeutiques](Tokra-Therapeutic-Opportunity) ;
- [livraisons médicales](Tokra-Medical-Support-Deliveries).

## Limites actuelles

La faction Tok'ra ne possède pas encore :

- de colonies visibles ;
- de bases cachées visitables ;
- de sites de quête ;
- de marchands propres ;
- d'aide militaire ;
- de raids.

Des cellules cachées ou sites scénarisés pourront être ajoutés plus tard sans
transformer les Tok'ra en puissance territoriale classique.
