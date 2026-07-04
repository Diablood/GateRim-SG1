
# Validation locale - 0.3.65-dev

Jalon : `0.3.65-dev - Add GateRim SG-1 storyteller foundation`

Branche : `feature/sg1-storyteller-foundation`

Base : `v0.3.64-dev`

Version de DLL validée : `0.3.65.0`

Révision locale : `r2`

Statut : révision finale `r2` validée et publiée.

Charger dans cet ordre :

```text
Core
Harmony
Biotech
GateRim SG-1
```

## Test validé

1. Aucun rebuild supplémentaire n’est requis : `r2` ne modifie que les deux descriptions XML.
2. Redémarrer RimWorld puis vérifier dans l’interface de sélection que `Commandement SG-1` est visible, que sa description française tient sans barre de défilement et que son portrait temporaire est chargé.
3. Démarrer ou charger une partie avec ce storyteller.
4. Ouvrir exactement :

   ```text
   Actions de débogage > GateRim SG-1 > Storyteller SG-1...
   > Show orchestration report
   ```

5. Vérifier :
   - `active defName: SG1_GateRimStoryteller` ;
   - `GateRim orchestration active: yes` ;
   - `baseline definition: Cassandra` ;
   - `baseline clone initialized: yes` ;
   - un composant SG-1 de plus que les composants du baseline ;
   - aucun incident de fondation ;
   - aucune relation inter-domaines active ;
   - aucun autre storyteller modifié.
6. Sauvegarder, recharger et vérifier la persistance.
7. Passer temporairement à Cassandra ou à un autre storyteller et confirmer
   `GateRim orchestration active: no`.
8. Revenir à `Commandement SG-1` et confirmer la reprise.
9. Forcer un incident ou raid GateRim existant et vérifier son comportement
   inchangé.
10. Contrôler `Player.log`.

## Tests optionnels

- migration d’une sauvegarde antérieure ;
- plusieurs changements de storyteller ;
- compteur d’activation incrémenté uniquement lors d’une réactivation ;
- nombre de composants stable après sauvegarde/rechargement ;
- aucun changement des fréquences, points de menace, délais ou incidents
  existants.

## Résultat

Le mainteneur confirme la validation finale de `r2` :

- `Commandement SG-1` est sélectionnable ;
- la baseline Cassandra, l’activation et la désactivation sont correctes ;
- la sauvegarde/recharge conserve l’état ;
- le rapport développeur reste cohérent ;
- les incidents GateRim existants ne régressent pas ;
- `Player.log` reste propre ;
- la description française raccourcie tient entièrement sans barre de
  défilement.

La révision finale du jalon est `r2`. La branche
`feature/sg1-storyteller-foundation`, le tag annoté `v0.3.65-dev` et le
wiki séparé sont publiés.
