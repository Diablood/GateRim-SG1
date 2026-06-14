# Enterable Tok'ra hidden safehouse site

Version: `0.2.19-dev`

Validation: `0.2.18-dev` contact flow passed. `0.2.19-dev` adds the
Tok'ra clothing requirement for the generated contact.

## Purpose

The `0.2.17-dev` milestone turned one stored Tok'ra safehouse lead into a real,
temporary vanilla `Site`. The `0.2.18-dev` milestone adds one peaceful Tok'ra
contact to its generated map.

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
Contact gen step: GateRimSG1.Goauld.GenStep_TokraHiddenSafehouseContact
```

The site reuses RimWorld's vanilla `Site`, `SitePartWorker_ItemStash` and
`GenStep_ItemStash` systems. A small custom `GenStep` adds exactly one contact
after the cache without replacing the vanilla map generator.

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
1 peaceful SG1_TokraVoluntaryHost aged 20+
2 tretonin doses
4 industrial medicine
```

An unvisited site expires after `600000` ticks, or `10` RimWorld days.

The contact:

```text
uses the persistent SG1_Tokra faction
receives a three-day peaceful visit duty
has an adulthood backstory
has no trader role or inventory
has Recruitable set to false
starts non-hostile
wears the dedicated `SG1_TokraFieldGarb` outfit since `0.2.19-dev`
```

## Manual test checklist

### Test principal obligatoire pour 0.2.18-dev

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
5. À l'arrivée, ouvre `Actions de débogage`, puis choisis directement :
   `GateRim SG-1 > Verify Tok'ra safehouse contact test`.
   Résultat attendu : un message vert indique `Contacts : 1`, un âge d'au moins
   `20`, `histoire adulte : True`, `hostile : False`, `marchand : False` et
   `recrutable : False`.
6. Vérifie que le cache contient toujours exactement `2` doses de trétonine et
   `4` médicaments industriels.
7. Sauvegarde puis recharge pendant que la carte de la planque est ouverte,
   puis relance
   `GateRim SG-1 > Verify Tok'ra safehouse contact test`.
   Résultat attendu : le test reste vert et aucun message rouge n'apparaît.
8. Utilise `Reformer la caravane` et quitte la carte.
   Résultat attendu : la carte temporaire et le site disparaissent proprement.

### Tests complémentaires facultatifs

1. Après le test principal, avec `0` piste et aucun site actif, lance de nouveau
   `GateRim SG-1 > Create Tok'ra safehouse test site`.
   Résultat attendu : aucun nouveau site n'apparaît.
2. Sur une carte autre que celle de la planque, lance
   `GateRim SG-1 > Verify Tok'ra safehouse contact test`.
   Résultat attendu : un message rouge demande d'ouvrir la carte générée de la
   planque.
3. Prépare puis crée un autre site et vérifie à nouveau le contact.
   Résultat attendu : un seul contact est présent et l'action de vérification
   affiche de nouveau un message vert.
