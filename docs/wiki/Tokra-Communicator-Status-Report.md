# Rapport d'état du canal Tok'ra

> Statut : prototype actif
> Version d'introduction : 0.2.32-dev
> Passe RP : 0.2.32-dev-r1
> Ajustement RP : 0.2.32-dev-r2
> Lecture de progression Tok'ra : 0.2.33-dev-r1
> Séparation RP/debug des opérations : 0.3.1-dev

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

En jeu normal, le rapport reste volontairement compact et ne présente que les informations actuellement utiles à la colonie :

```text
- relation qualitative avec la cellule Tok'ra
- état du canal principal
- opération organique actuellement proposée, acceptée ou active
- issue durable pertinente d'une mission Tok'ra unique déjà achevée
```

Il ne présente plus la liste complète des demandes verrouillées ou disponibles, leurs temps de silence, les compteurs internes, les pondérations, les futures opérations ni l'historique technique.

Lorsque le mode développeur RimWorld ou l'option avancée GateRim SG-1 est actif, la version technique complète reste accessible. Elle peut afficher les demandes manuelles, les états internes du framework, la méthode de décodage, la progression restante, les conséquences différées et les diagnostics utiles aux tests.

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

Depuis `0.2.33-dev`, le rapport ajoute une lecture RP de la posture Tok'ra : la
cellule peut sembler distante, encore prudente, plus coopérative, proche d'un
changement d'attitude ou déjà prête à recevoir des demandes sensibles. Le joueur
ne voit pas de score brut dans cette fenêtre. Le rapport ne montre jamais le
catalogue des opérations organiques, leurs pondérations, leurs délais cachés ou
leur historique technique. Ces informations restent limitées au mode développeur
ou à l'option avancée GateRim SG-1.

Cette option clarifie l'état du communicateur avant de choisir une vraie demande
Tok'ra.


## Libellés verrouillés simplifiés

Depuis `0.2.33-dev-r1`, les options du clic droit gardent des raisons de verrouillage courtes. Le rapport d'état du canal reste l'endroit principal où le joueur consulte les détails de posture Tok'ra et de disponibilité des demandes.

Depuis `0.2.34-dev`, le rapport affiche aussi l'état du canal de débriefing
opérationnel. Ce canal reste informatif et formateur : il ne déclenche aucun
soutien matériel ni militaire.

Depuis `0.2.35-dev`, le rapport affiche aussi l'état de la mission discrète Tok'ra : elle peut être verrouillée, prête à être proposée ou déjà en préparation.
