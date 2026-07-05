# Current milestone validation

Jalon : `0.3.73-dev - Add bounded Goa'uld alliance raid-strength bonus`

Branche attendue : `feature/goauld-alliance-raid-strength`

Révision finale validée : `r1`

Version de DLL attendue : `0.3.73.0`

Statut : build et validation fonctionnelle terminés avec succès.

## Précontrôles

Depuis la racine du dépôt :

```powershell
git branch --show-current
git diff --check
.\build.cmd
.\tools\check-duration-formatting.cmd
.\tools\check-project-consistency.cmd
```

Résultats attendus :

- branche `feature/goauld-alliance-raid-strength` ;
- assembly `0.3.73.0` ;
- audit des durées toujours validé avec `104` clés uniques ;
- contrôle de cohérence sans erreur ;
- aucune erreur de compilation C#.

## Préparation en jeu

1. Charger une partie de test avec le storyteller **Commandement SG-1**.
2. Utiliser une carte principale de colonie.
3. Vérifier qu'au moins deux domaines Goa'uld actifs existent. Sinon utiliser :

```text
Actions de débogage
> GateRim SG-1
> Goa'uld inter-domain relations...
> Create additional test domain
```

4. Ouvrir le même menu et sélectionner `Reset relations`.

## Test obligatoire : alliance à 110 %

Dans :

```text
Actions de débogage
> GateRim SG-1
> Goa'uld inter-domain relations...
```

1. Sélectionner `Set all pairs: Alliance`.
2. Ouvrir `Show natural raid relation-pressure report`.
3. Confirmer pour tous les domaines actifs :
   - `active alliance: yes` ;
   - `active open conflict: no` ;
   - `ordinary natural raid factor: 110%`.
4. Ouvrir :

```text
Actions de débogage
> GateRim SG-1
> Goa'uld...
> Threat progression...
> Show current progression
```

5. Confirmer que le domaine diagnostiqué affiche un facteur naturel de `110 %`
   et des points effectifs égaux aux points vanilla multipliés par `1,10`.
6. Revenir au menu des relations et sélectionner
   `Force current natural raid (relation pressure applied)`.
7. Confirmer qu'un raid naturel Goa'uld/Jaffa démarre normalement, avec une
   doctrine choisie depuis les points vanilla d'origine.
8. Avec les informations de debug avancées actives, confirmer dans `Player.log`
   ou la sortie de log GateRim une ligne indiquant une augmentation des points,
   le facteur `1.10` et la raison `active alliance`.

## Régressions ciblées

### Conflit ouvert conservé

1. Sélectionner `Set first pair: Open conflict`.
2. Ouvrir `Show natural raid relation-pressure report`.
3. Confirmer `75%` pour les deux domaines de la paire.
4. Forcer le raid avec pression et confirmer une réduction `0.75`, sans
   modification de la doctrine choisie depuis les points initiaux.

### Non-cumul et priorité du conflit

1. Disposer d'au moins trois domaines actifs, en utilisant
   `Create additional test domain` si nécessaire.
2. Sélectionner `Reset relations`, puis `Set all pairs: Alliance`.
3. Confirmer dans le rapport que tous les domaines restent à `110%`, même ceux
   participant à plusieurs alliances.
4. Sélectionner ensuite `Set first pair: Open conflict`.
5. Confirmer que les deux domaines de cette paire passent à `75%` malgré leurs
   autres alliances, tandis qu'un domaine seulement allié reste à `110%`.

### Storyteller et appels forcés

1. Sélectionner `Set all pairs: Alliance`.
2. Basculer temporairement sur Cassandra Classique.
3. Confirmer dans le rapport que tous les facteurs deviennent `100%`.
4. Revenir à Commandement SG-1 et confirmer le retour immédiat à `110%`.
5. Dans :

```text
Actions de débogage
> GateRim SG-1
> Goa'uld...
> Threat progression...
```

6. Lancer `Force natural direct raid` et confirmer que le test déterministe
   conserve ses `300` points au lieu de recevoir le facteur d'alliance.

### Persistance

1. Sauvegarder avec au moins une alliance active.
2. Recharger la partie.
3. Ouvrir le rapport et confirmer que le facteur dérivé reste `110%` sans
   nouveau champ ou migration visible.

## Fin de test

Examiner `Player.log` et confirmer l'absence de nouvelle erreur C#, Harmony,
XML, Scribe, génération de raid ou Lord. Signaler séparément toute différence
entre le facteur affiché, le facteur journalisé et la force réellement générée.

## Résultat final

La révision finale `r1` est validée :

- rebuild forcé `0.3.73.0` réussi ;
- audit des durées réussi avec `104` clés uniques ;
- contrôle global de cohérence réussi ;
- alliance seule validée à `110 %` ;
- conflit ouvert conservé à `75 %` ;
- priorité du conflit et non-cumul validés avec plusieurs domaines ;
- autres storytellers validés à `100 %` ;
- incident naturel et appels forcés correctement distingués ;
- sauvegarde/rechargement validé ;
- `Player.log` accepté sans nouvelle erreur C#, Harmony, XML, Scribe,
  génération de raid ou Lord.

Aucune limite fonctionnelle nouvelle n'est connue. Le jalon est finalisé,
intégré par fast-forward, publié sous `v0.3.73-dev` et synchronisé vers le wiki
séparé.

