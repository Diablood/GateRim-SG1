# Extraction d'urgence Goa'uld

> Statut : chirurgie jouable et outil développeur
> Version d'introduction : 0.1.19-dev
> Restriction de la commande instantanée : 0.3.41-dev

## Présentation

![Icône finale de l'extraction d'urgence](images/SG1_EmergencyExtraction.png)

Pendant la phase critique d'implantation récente, le joueur peut interrompre la prise de contrôle par une véritable opération médicale :

```text
extraction d'urgence Goa'uld
```

La réussite retire le symbiote avant qu'il ne devienne un [hôte Goa'uld actif](Active-Goauld-Host). Le même parasite réapparaît à proximité avec son identité persistante.

## Commande instantanée

L'ancienne commande `Extraction d'urgence` effectue le même transfert sans médecin, médicament ni risque d'échec. Depuis `0.3.41-dev`, elle apparaît uniquement lorsque le mode développeur RimWorld est actif. Les informations avancées GateRim SG-1 restent purement diagnostiques.

Elle n'est plus visible pendant une partie normale. La [chirurgie d'extraction](Extraction-Surgery) constitue le parcours joueur.

## Identité persistante

```text
symbiote libre
    ↓ implantation
implantation récente
    ↓ extraction réussie
même symbiote libre
```

L'identifiant, le nom, l'origine et l'allégeance du symbiote sont conservés.

## Après la phase critique

Lorsque la conversion active est déjà terminée, l'extraction d'urgence n'est plus disponible. Il faut neutraliser et maîtriser l'hôte, puis tenter l'opération plus dangereuse décrite dans [Chirurgie d'extraction](Extraction-Surgery).
