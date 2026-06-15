# Contact de briefing de mission Tok'ra

> Statut : prototype actif  
> Version d'introduction : 0.2.36-dev

Le contact de briefing est la première étape Tok'ra initiée par la cellule après
une demande de mission discrète. Il évite que toute la progression Tok'ra passe
par des commandes répétées du communicateur.

## Déclenchement

```text
mission Tok'ra discrète déjà préparée
état persistant en sauvegarde
contact automatique après un délai RP
aucune action supplémentaire du joueur nécessaire
```

## Effet

La cellule Tok'ra reprend contact par lettre et transmet un briefing prudent. Le
briefing marque une progression narrative, mais ne livre pas encore de cible
jouable.

```text
état persistant : briefing reçu
visible dans le rapport du canal Tok'ra
aucun site monde créé
aucun raid forcé
aucun objet ni récompense immédiate
```

Cette étape prépare une future vraie mission Tok'ra sans transformer le
communicateur en simple bouton à spammer à chaque cooldown.
