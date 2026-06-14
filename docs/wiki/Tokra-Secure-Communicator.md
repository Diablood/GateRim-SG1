# Communicateur sécurisé Tok'ra

> Statut : prototype actif
> Version d'introduction : 0.2.26-dev
> Première demande active : 0.2.27-dev
> Interaction opérée par un colon : 0.2.28-dev

Le communicateur sécurisé Tok'ra est une passerelle de confiance vers les
soutiens avancés Tok'ra. Il reste volontairement clandestin : il ne transforme
pas les Tok'ra en faction alliée classique.

## Fonctions actuelles

```text
- bâtiment constructible et alimenté
- nécessite la recherche Microélectronique
- utilisable seulement au palier de confiance fiable
- utilisé par un colon sélectionné via clic droit
- ouvre une fenêtre vanilla de contact sécurisé
- peut demander une diversion défensive pendant une attaque active
```

## Diversion défensive

Depuis `0.2.27-dev`, une colonie fiable peut demander une
[diversion défensive Tok'ra](Tokra-Defensive-Diversion-Request).

```text
menace hostile active requise
jusqu'à 3 ennemis brièvement perturbés
long délai avant nouvelle demande
pas d'escouade Tok'ra physique
```

## Limites actuelles

```text
pas de soin direct
pas d'objet donné
pas de demande de piste de planque via communicateur
pas de renfort permanent
pas de commerce
pas de recrutement
pas de quête
```

Le communicateur sert donc de premier accès fiable aux futures demandes Tok'ra
plus avancées.


## Utilisation par un colon

Depuis `0.2.28-dev`, les demandes passent par un colon : sélectionnez un colon,
puis faites un clic droit sur le communicateur. Le colon rejoint le bâtiment,
l'utilise brièvement, puis la fenêtre ou la demande Tok'ra se déclenche.

Les commandes du bâtiment servent surtout d'information : elles rappellent de
sélectionner un colon pour utiliser réellement l'appareil.
