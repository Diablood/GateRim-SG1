# Current milestone validation

Jalon : `0.3.75-dev - Add Jaffa officers to eligible Goa'uld forces`

Branche : `feature/jaffa-officers-in-goauld-forces`

Révision finale validée : `r2`

Version de DLL validée : `0.3.75.0`

Statut : validation finale réussie. `r1` avait échoué au build avec deux erreurs
`CS0115`. `r2` retire les overrides invalides et déplace l'injection vers le
postfix Harmony de génération de groupe, limité au contexte exact du raid
naturel ou de la diversion.

## Contrôles préalables

Depuis la racine du dépôt :

```powershell
git diff --check
.\build.cmd
.\tools\check-duration-formatting.cmd
.\tools\check-project-consistency.cmd
```

Résultats attendus :

- aucune erreur `CS0115` dans `IncidentWorker_GoauldJaffaNaturalRaid` ou
  `IncidentWorker_GoauldJaffaLuredAssault` ;
- assembly `0.3.75.0` ;
- audit des durées avec `104` clés uniques ;
- contrôle global de cohérence réussi ;
- aucune erreur de compilation.

## Test obligatoire court - raid naturel éligible

1. Charger une colonie avec le mode développeur RimWorld actif.
2. Ouvrir :

```text
Actions de débogage
> GateRim SG-1
> Goa'uld...
> Threat progression...
> Force eligible natural raid with officer (900 points)
```

3. Vérifier que la force contient au moins cinq Jaffa.
4. Vérifier qu'elle contient exactement un
   `SG1_GoauldJaffaFieldOfficer` et aucun second officier.
5. Vérifier que l'officier remplace un pawn du groupe au lieu d'être ajouté en
   supplément.
6. Vérifier son armure rouge, son casque rouge rétractable, sa marque frontale
   argentée, son arme Jaffa, ses gantelets, ses bottes et son Prim'ta.
7. Vérifier que les autres Jaffa conservent leurs équipements marron/doré.

## Test obligatoire - seuil inférieur

1. Dans le même menu, utiliser :

```text
Force natural direct raid (300 points)
```

2. Vérifier qu'un groupe contenant moins de cinq Jaffa ne reçoit aucun officier.
3. Vérifier que ce test forcé historique conserve son comportement et ses points
   exacts.

## Test obligatoire - colonie Goa'uld

1. Depuis la carte mondiale, attaquer une colonie d'un domaine Goa'uld avec une
   caravane de test.
2. Pour chaque groupe de défense visible, compter les Jaffa.
3. Un groupe de moins de cinq Jaffa ne doit contenir aucun officier.
4. Un groupe d'au moins cinq Jaffa peut contenir au plus un
   `SG1_GoauldSettlementJaffaOfficer` rouge ; il remplace un garde de garnison
   et ne change pas l'effectif du groupe.
5. Vérifier que les hôtes Goa'uld minoritaires et les autres défenseurs ne sont
   pas remplacés.

## Test obligatoire - mission adaptée

Valider au moins l'un des chemins suivants avec une force d'au moins cinq Jaffa :

- mission d'introduction Tok'ra ;
- appel de détresse Tok'ra ;
- relais Goa'uld décodé, défense initiale ou renforts ;
- interception de livraison temporaire ;
- assaut de diversion.

Vérifier exactement un `SG1_GoauldJaffaFieldOfficer` rouge, un effectif
inchangé et la conservation du comportement propre à la mission. La mission de
capture doit continuer à générer uniquement son `SG1_GoauldJaffaOfficer` cible
à `165` points ; son escorte ne doit pas recevoir un second officier.

## Persistance et régressions

1. Sauvegarder avec un officier généré présent sur une carte.
2. Recharger.
3. Vérifier le PawnKind, le Prim'ta, la marque argentée, l'armure rouge et le
   mode du casque.
4. Vérifier les raids contrôlés, les représailles d'extraction et les champs de
   bataille inter-domaines : aucun officier ne doit y être injecté par ce jalon.
5. Examiner `Player.log` et confirmer l'absence de nouvelle erreur C#, Harmony,
   XML, DefOf, génération de groupe, apparel, rendu, Scribe, raid ou mission.

## Résultat final validé

- build et audits ;
- raid éligible : nombre total de Jaffa et nombre d'officiers ;
- groupe sous le seuil : nombre total et absence d'officier ;
- défense de colonie ;
- mission testée ;
- sauvegarde/rechargement ;
- `Player.log` propre.
