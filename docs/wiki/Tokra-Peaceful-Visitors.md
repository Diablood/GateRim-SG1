# Visiteurs Tok'ra pacifiques

> Statut : Prototype de test  
> Version d'introduction : 0.1.42-dev

## Présentation

Une première équipe Tok'ra pacifique peut désormais visiter la colonie.

Cette visite doit être déclenchée manuellement en mode développeur :

```text
Do incident
    ↓
visiteurs Tok'ra pacifiques (test)
```

## Composition

```text
1 à 3 hôtes Tok'ra volontaires
```

Les visiteurs utilisent le profil interne :

```text
Peaceful
```

de la faction Tok'ra masquée.

## Comportement

```text
entrée par le bord de la carte
    ↓
visite pacifique de la colonie
    ↓
départ automatique
```

Les visiteurs ne sont pas contrôlés par le joueur.

Chaque hôte reçoit automatiquement son symbiote Tok'ra adulte actif peu après
son apparition.

## Faction masquée persistante

La première visite crée une instance masquée de la faction Tok'ra.

Les visites suivantes réutilisent cette même instance après sauvegarde et
rechargement.

## Limites actuelles

```text
aucune sélection aléatoire par le storyteller
aucune colonie Tok'ra
aucun marchand
aucun stock commercial
aucune diplomatie complète
aucune quête
```

L'incident reste un outil de test contrôlé avant l'activation progressive de la
présence Tok'ra dans le monde.
