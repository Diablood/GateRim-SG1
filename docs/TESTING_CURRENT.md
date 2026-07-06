# Current milestone validation

Jalon : `0.3.81-dev - Add shared Goa'uld alliance reprisals`

Branche : `feature/goauld-alliance-shared-reprisals`

Révision finale validée : `r2`

Version de DLL attendue : `0.3.81.0`

## Préparation

- utiliser une carte de colonie jouable ;
- activer le mode développeur ;
- sélectionner le storyteller `Commandement SG-1` ;
- disposer d'au moins deux domaines Goa'uld actifs ;
- dans
  `Actions de débogage > GateRim SG-1 > Goa'uld inter-domain relations...`,
  placer une paire en `Alliance` ;
- ouvrir ensuite
  `Actions de débogage > GateRim SG-1 > Goa'uld... > Domain reactions...`.

## Validation finale r2 — lettre de programmation

1. Exécuter `Reset shared alliance reprisals`.
2. Exécuter `Create shared alliance reprisal`.
3. Ouvrir la lettre `Représailles communes Goa'uld`.

Attendu :

- la lettre nomme le domaine lésé et son allié ;
- elle annonce une réaction future et son délai ;
- elle ne propose pas `Se rendre sur les lieux` ;
- cliquer la lettre ne centre pas artificiellement la caméra sur le centre de la
  colonie ;
- `Show domain reaction state` affiche une seule représaille commune en attente.

## Validation finale r2 — lettre d'arrivée

1. Exécuter `Trigger pending shared reprisal now`.
2. Ouvrir la nouvelle lettre `Représailles communes Goa'uld`.
3. Utiliser sa navigation de cible si RimWorld l'affiche.

Attendu :

- le texte nomme les deux domaines ;
- il annonce explicitement **deux détachements Jaffa** ;
- il précise qu'ils arrivent depuis des côtés opposés ;
- deux forces distinctes et simultanées sont visibles, chacune aux couleurs de
  son domaine ;
- les cibles de la lettre contiennent un pawn de la force principale et un pawn
  de la force alliée ; la navigation ou le surlignage permet donc d'identifier
  les deux détachements au lieu d'un seul ;
- aucune deuxième lettre de raid générique ne double l'annonce commune.

## Régressions déjà acceptées à ne pas rejouer sauf anomalie

Le premier passage a été signalé conforme pour :

- création et déclenchement de la représaille ;
- paire exacte ;
- budget total `80%` des points vanilla ;
- partage `60/40` ;
- arrivée simultanée depuis des bords opposés ;
- coopération temporaire ;
- absence d'effet territorial ou diplomatique.

## Contrôles automatiques

Depuis la racine du dépôt :

```powershell
git diff --check
.\build.cmd
.\tools\check-duration-formatting.cmd
.\tools\check-project-consistency.cmd
```

Résultats attendus :

- assembly `0.3.81.0` ;
- audit des durées avec `104` clés uniques ;
- versions, XML, traductions et documentation cohérents ;
- aucune erreur de compilation.

## Journal

Après les deux lettres, vérifier `Player.log`. Aucun nouvel avertissement ou
exception pertinent ne doit apparaître lors de la programmation, de la création
des deux groupes ou de l'ouverture/navigation de la lettre d'arrivée.


## Résultat accepté

La procédure finale `r2` est validée : la lettre de programmation ne propose
plus de déplacement vers un lieu inexistant, la lettre d'arrivée annonce et
cible les deux détachements, les autres tests fonctionnels restent conformes et
aucune nouvelle erreur pertinente n'est signalée dans `Player.log`.

Le nom exact de l'action développeur est
`Trigger pending shared reprisal now`.
