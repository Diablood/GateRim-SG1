# Visiteurs Tok'ra pacifiques

> Statut : Prototype naturel rare
> Version d'introduction : 0.1.42-dev
> Sélection naturelle activée : 0.1.43-dev

## Présentation

Une petite équipe Tok'ra pacifique peut désormais visiter naturellement la
colonie à partir du jour `15`.

```text
sélection rare par le storyteller
    ↓
visiteurs Tok'ra pacifiques
    ↓
1 à 3 hôtes Tok'ra volontaires
```

Un délai minimal de `30` jours empêche deux visites rapprochées.

La visite reste aussi déclenchable manuellement en mode développeur :

```text
Do incident
    ↓
visiteurs Tok'ra pacifiques
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

Depuis `0.2.7-dev`, chaque partie possède déjà une
[présence mondiale masquée Tok'ra](Tokra-World-Presence).

Les visites réutilisent cette même instance après sauvegarde et rechargement.
Une ancienne sauvegarde qui en est dépourvue reçoit automatiquement la présence
masquée sans créer de colonie.

## Limites actuelles

```text
aucune colonie Tok'ra
aucun marchand
aucun stock commercial
aucune diplomatie complète
aucune quête
```

Cette première présence naturelle reste volontairement rare avant l'ajout de
relations Tok'ra plus complètes.
