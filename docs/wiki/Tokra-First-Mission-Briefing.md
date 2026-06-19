# Contact de briefing de mission Tok'ra

> Statut : étape active de la chaîne de mission
> Première version : `0.2.36-dev`

Le contact de briefing est la première étape initiée par la cellule après une
demande de mission discrète. Il évite que toute la progression Tok'ra passe par
des commandes répétées du communicateur.

## Déclenchement

```text
mission Tok'ra discrète préparée
état persistant en sauvegarde
contact automatique après un délai RP
aucune action supplémentaire du joueur nécessaire
```

## Effet

La cellule transmet une lettre de briefing et enregistre l'étape dans le rapport
du canal. Le briefing ne donne pas immédiatement une récompense ou une cible à
attaquer.

Il prépare toutefois la suite aujourd'hui jouable :

```text
cache de mission
→ analyse des renseignements codés
→ piste décodée
→ relais Goa'uld révélé
→ mission de sabotage
```

Le communicateur reste ainsi un point de contact, pas un bouton à répéter à
chaque délai disponible.
