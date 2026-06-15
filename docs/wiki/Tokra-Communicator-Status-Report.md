# Rapport d'état du canal Tok'ra

> Statut : prototype actif  
> Version d'introduction : 0.2.32-dev  
> Passe RP : 0.2.32-dev-r1  
> Ajustement RP : 0.2.32-dev-r2

Le rapport d'état du canal Tok'ra est une consultation sans effet direct,
accessible via le communicateur sécurisé Tok'ra.

## Utilisation

```text
1. sélectionner un colon humain contrôlé par la colonie
2. clic droit sur le communicateur sécurisé Tok'ra
3. choisir "Consulter l'état du canal Tok'ra"
4. le colon rejoint le communicateur et sonde le canal
```

## Informations affichées

La fenêtre conserve les informations utiles au joueur, mais sous forme de
transmission Tok'ra plus RP. Depuis `0.2.32-dev-r2`, le rapport ne répète plus l'alimentation du relais : cette information est déjà comprise par l'usage même du communicateur.

```text
- relation et confiance opérationnelle avec la cellule
- état du canal principal
- diversion défensive : disponible, verrouillée ou en cooldown
- évaluation tactique : disponible, verrouillée ou en cooldown
- soutien médical : disponible, verrouillé ou en cooldown
- cache médicale : disponible, verrouillée ou en cooldown
- signatures hostiles proches
- colons signalés comme blessés ou malades
```

## Limites

La consultation sert uniquement à prendre connaissance du canal. La cellule reste
en écoute et n'agit pas tant qu'aucune demande précise n'est envoyée.

```text
aucune demande envoyée
aucune aide déclenchée
aucun objet livré
aucun soin direct
aucun effet de combat
aucun renfort
aucune révélation de carte
aucune quête
```

Cette option clarifie l'état du communicateur avant de choisir une vraie demande
Tok'ra.
