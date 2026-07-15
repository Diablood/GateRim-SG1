# Current testing — final basin art and relay-structure simplification

Jalon : `0.3.93-dev`
Révision à tester : `r1`
Version de DLL attendue : `0.3.93.0`

## Préparation

```powershell
.\build.cmd
.\tools\check-duration-formatting.cmd
git diff --check
```

Le contrôle visuel complet et le contrôle de cohérence documentaire sont à
relancer après la révision de finalisation du registre et du wiki.

## Chargement

- Vérifier `GateRimSG1 0.3.93.0` dans `Player.log`.
- Vérifier l'absence d'erreur XML, DefOf, C#, texture manquante ou fond magenta.
- Vérifier que les trois anciens Defs de structure de relais ne sont plus chargés.

## Bassin rituel Goa'uld

- Construire ou faire apparaître `SG1_GoauldRitualBasin`.
- Confirmer une empreinte `1×1`, sans commande de rotation.
- Confirmer la transparence, le centrage et la lisibilité du nouveau visuel.
- Tester une implantation rituelle Goa'uld.
- Tester la cérémonie formelle du Prim'ta.
- Sauvegarder et recharger avec le bassin construit.

## Bassin d'incubation du Prim'ta

- Construire `SG1_PrimtaIncubationBasin`.
- Confirmer une empreinte `1×1`, sans rotation.
- Confirmer que la cellule d'interaction reste au sud.
- Lancer une facture d'incubation et vérifier le travail, la consommation et le
  produit final.
- Vérifier que le pawn peut atteindre la cellule d'interaction.
- Sauvegarder/recharger pendant une facture.

## Bassin de conservation du Prim'ta

- Construire `SG1_PrimtaPreservationBasin`.
- Confirmer une empreinte `1×1`, sans rotation.
- Vérifier le filtre limité aux symbiotes immatures et larves matures.
- Vérifier que deux piles peuvent être stockées dans la cellule unique.
- Vérifier la conservation alimentée, l'arrêt hors tension et la reprise du
  vieillissement biologique.
- Sauvegarder/recharger avec deux piles stockées.

## Site de sabotage du relais

- Générer les trois profils de site : bunker, station divisée et cour fortifiée.
- Confirmer que les murs sont des `Wall` vanilla, les portes des `Door` vanilla
  et les défenses des `Barricade` vanilla.
- Vérifier les toits, le brouillard intérieur et tous les accès.
- Vérifier qu'aucune structure n'est manquante ou superposée.
- Saboter le nœud, gérer les défenseurs et renforts, récupérer le butin puis
  reformer la caravane.
- Sauvegarder/recharger avant sabotage et pendant le compte à rebours.

## Résultat attendu

Aucun changement de mission, de récompense ou de comportement biologique en
dehors de la réduction volontaire des deux empreintes Prim'ta à `1×1` et du
remplacement des trois structures génériques par leurs équivalents vanilla.
