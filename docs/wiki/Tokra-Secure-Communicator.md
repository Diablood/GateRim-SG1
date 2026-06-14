# Communicateur sécurisé Tok'ra

> Statut : prototype actif
> Version d'introduction : 0.2.26-dev
> Première demande active : 0.2.27-dev

Le communicateur sécurisé Tok'ra est une passerelle de confiance vers les
soutiens avancés Tok'ra. Il reste volontairement clandestin : il ne transforme
pas les Tok'ra en faction alliée classique.

## Fonctions actuelles

```text
- bâtiment constructible et alimenté
- nécessite la recherche Microélectronique
- utilisable seulement au palier de confiance fiable
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


## Direction future

L’utilisation actuelle passe par les commandes du bâtiment. Une prochaine étape
devra envisager une interaction via un colon sélectionné, afin que le
communicateur se comporte davantage comme une console opérée par un pion.
