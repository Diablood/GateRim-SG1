# Communicateur Tok'ra opéré par un colon

> Statut : prototype actif
> Version d'introduction : 0.2.28-dev

Le communicateur sécurisé Tok'ra n'est plus seulement un bouton de bâtiment.
Il doit être utilisé par un colon.

## Utilisation

```text
1. Sélectionner un colon contrôlé par la colonie.
2. Clic droit sur le communicateur sécurisé Tok'ra.
3. Choisir le canal Tok'ra ou la demande de diversion.
4. Le colon rejoint le bâtiment et l'utilise brièvement.
5. La fenêtre ou l'effet Tok'ra se déclenche ensuite.
```

## Conditions conservées

```text
palier fiable requis
alimentation requise
menace active requise pour la diversion
cooldown de diversion conservé
pas de commerce, recrutement, quête ou récompense matérielle
```

## Diagnostics et commandes développeur

```text
les boutons directs du bâtiment sont masqués en jeu normal
l’option avancée GateRim SG-1 conserve uniquement les informations de diagnostic
les commandes qui forcent ou contournent le gameplay exigent le mode développeur RimWorld
l’utilisation joueur passe par un colon sélectionné + clic droit
```

## Découverte progressive depuis `0.3.42-dev`

Avant que la relation Tok'ra atteigne le palier fiable, le menu obtenu par clic droit ne liste plus les futures demandes fiables sous forme d'options grisées. La consultation de l'état du canal reste visible, ainsi que l'interaction liée à une opération organique déjà proposée ou active.

Une fois le palier fiable atteint, les demandes apparaissent normalement. Les restrictions immédiates liées au colon, à l'accès, à la réservation, à l'alimentation, au délai, à l'absence de menace ou de patient restent visibles sous forme de motifs gris.
