# Enterable Tok'ra hidden safehouse site

Version: `0.2.17-dev`

Validation: manual RimWorld tests passed on June 14, 2026 using the dedicated
`GateRim SG-1` preparation and site-creation debug actions.

## Purpose

This milestone turns one stored Tok'ra safehouse lead into a real, temporary
vanilla `Site` that a caravan can visit.

It remains intentionally small:

```text
no hostile defenders
no trader
no recruitment
no military aid
no raid
no permanent settlement
```

## Definitions

```text
IncidentDef: SG1_TokraHiddenSafehouseSiteIncident
WorldObjectDef: SG1_TokraHiddenSafehouseSite
SitePartDef: SG1_TokraHiddenSafehouseSitePart
Worker: GateRimSG1.Goauld.IncidentWorker_TokraHiddenSafehouseSite
```

The site reuses RimWorld's vanilla `Site`, `SitePartWorker_ItemStash` and
`GenStep_ItemStash` systems. No custom map generator is introduced.

## Requirements

The incident requires:

```text
at least 1 stored Tok'ra safehouse lead
Tok'ra trust tier neutral, cooperative or trusted
no active Tok'ra safehouse marker or site
one valid world tile 5 to 12 tiles from the colony
the persistent hidden SG1_Tokra faction
```

## Result

On success:

```text
-1 Tok'ra safehouse lead
1 enterable non-hostile site
1 generated map sized 120 x 120
2 tretonin doses
4 industrial medicine
```

An unvisited site expires after `600000` ticks, or `10` RimWorld days.

## Manual test checklist

### Test principal obligatoire

1. Après le rebuild, ferme complètement RimWorld puis relance-le. Active le
   mode développeur dans `Options > Général > Mode développeur` et charge une
   colonie.
2. Sur la carte de la colonie, ouvre `Actions de débogage`, puis choisis :
   `GateRim SG-1 > Prepare Tok'ra safehouse site test`.
   Résultat attendu : un message vert confirme une confiance neutre, `1` piste
   et l'absence d'ancien marqueur ou site inactif.
3. Ouvre `Actions de débogage`, puis choisis directement :
   `GateRim SG-1 > Create Tok'ra safehouse test site`.
   Résultat attendu : une lettre positive apparaît, la piste passe à `0/3` et
   un seul site apparaît sur la carte du monde.
4. Forme une caravane avec au moins un colon, sélectionne la planque comme
   destination et envoie la caravane.
   Résultat attendu : l'action proposée est une visite, pas une attaque.
5. À l'arrivée, vérifie que la petite carte ne contient aucun ennemi et que le
   cache contient exactement `2` doses de trétonine et `4` médicaments
   industriels.
6. Sauvegarde puis recharge pendant que la carte de la planque est ouverte.
   Résultat attendu : la carte, la caravane et le cache restent présents sans
   erreur rouge.
7. Utilise `Reformer la caravane` et quitte la carte.
   Résultat attendu : la carte temporaire et le site disparaissent proprement.

### Tests complémentaires facultatifs

1. Après le test principal, avec `0` piste et aucun site actif, lance de nouveau
   `GateRim SG-1 > Create Tok'ra safehouse test site`.
   Résultat attendu : aucun nouveau site n'apparaît.
2. Prépare puis crée un site, et relance une seconde fois
   `GateRim SG-1 > Create Tok'ra safehouse test site` avant de le visiter.
   Résultat attendu : aucun deuxième site n'apparaît.
3. Prépare puis crée un site, mais ne le visite pas pendant `10` jours
   RimWorld.
   Résultat attendu : le site disparaît automatiquement.
