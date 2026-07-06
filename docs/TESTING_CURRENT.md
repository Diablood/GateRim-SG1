# Current milestone validation

Jalon : `0.3.80-dev - Make Goa'uld relations influence raid doctrines`

Branche : `feature/goauld-relations-raid-doctrine-interactions`

Révision finale validée : `r1`

Version de DLL validée : `0.3.80.0`

## Préparation

- utiliser une carte de colonie jouable ;
- activer le mode développeur ;
- sélectionner le storyteller `Commandement SG-1` ;
- disposer d'au moins deux domaines Goa'uld actifs ;
- ouvrir :
  `Actions de débogage > GateRim SG-1 > Threat progression... > Domain doctrines...` ;
- utiliser `Show domain doctrine report` après chaque changement de relation.

Le rapport doit afficher pour chaque domaine :

- le profil permanent et ses poids de base ;
- les poids admissibles avant relation ;
- le Def relationnel sélectionné et sa priorité ;
- les multiplicateurs appliqués ;
- les poids et pourcentages finaux.

## Test obligatoire 1 - neutralité et trêve

1. Dans `Goa'uld inter-domain relations...`, placer la première paire en
   `Neutral`.
2. Ouvrir le rapport des doctrines.
3. Refaire le contrôle avec `Truce`.

Attendu :

- `selected relation modifier: none` ;
- les poids admissibles avant relation et les poids finaux sont identiques ;
- aucun poids nul n'est rendu admissible.

## Test obligatoire 2 - rivalité

1. Placer la première paire en `Rivalry`.
2. Ouvrir le rapport des doctrines.

Attendu pour chaque domaine impliqué sans relation prioritaire supérieure :

- Def sélectionné : `SG1_GoauldRelationDoctrine_Rivalry` ;
- priorité `100` ;
- multiplicateurs `direct x1 / abduction x1.25 / destruction x1` ;
- l'enlèvement augmente seulement s'il est déjà admissible ;
- sous `800` points ou avec moins de deux colons libres, son poids reste `0`.

## Test obligatoire 3 - alliance

1. Placer la première paire en `Alliance`.
2. Ouvrir le rapport des doctrines.

Attendu :

- Def sélectionné : `SG1_GoauldRelationDoctrine_Alliance` ;
- priorité `200` ;
- multiplicateurs `direct x1.25 / abduction x1 / destruction x1` ;
- les spécialités permanentes ne sont pas renversées artificiellement ;
- le facteur de points allié reste `110%` et n'est pas inclus dans les poids.

## Test obligatoire 4 - conflit ouvert et priorité

Avec au moins trois domaines :

1. placer le domaine testé en alliance avec un domaine ;
2. le placer en conflit ouvert avec un autre domaine ;
3. ouvrir le rapport des doctrines.

Attendu :

- un seul Def est sélectionné ;
- `SG1_GoauldRelationDoctrine_OpenConflict`, priorité `300` ;
- multiplicateurs `direct x1 / abduction x1 / destruction x1.25` ;
- l'alliance n'ajoute aucun second multiplicateur ;
- le facteur final de menace reste `75%`, puisque le conflit ouvert conserve sa
  priorité sur l'alliance.

Si seulement deux domaines existent, vérifier séparément le conflit ouvert puis
l'alliance ; le contrôle de non-cumul à trois domaines reste recommandé.

## Test obligatoire 5 - storytellers ordinaires

1. Conserver une relation active produisant un multiplicateur sous
   `Commandement SG-1`.
2. Passer à un storyteller vanilla.
3. Ouvrir le rapport des doctrines.

Attendu :

- `SG-1 Command active: false` ;
- aucun Def relationnel sélectionné ;
- poids admissibles et finaux identiques ;
- les relations restent stockées mais leur influence SG-1 est suspendue.

## Test obligatoire 6 - commandes forcées historiques

Exécuter séparément :

- `Force natural direct raid (300 points)` ;
- `Force natural abduction raid (800 points)` ;
- `Force natural destruction raid (1800 points)`.

Attendu :

- chaque commande produit exactement la doctrine demandée ;
- aucune relation ne remplace la doctrine forcée ;
- les chemins d'officier, de raid standard, de renfort différé et de raid
  conjoint restent inchangés.

## Régression fonctionnelle courte

Avec une alliance admissible :

- `Force standard alliance raid (1200 points)` reste mono-domaine ;
- `Force allied natural raid (1200 points, short delay)` reste partagé `75/25` ;
- `Force joint raid (1200 points, simultaneous)` reste partagé `60/40` et direct ;
- aucune commande forcée ne dépend d'un tirage relationnel aléatoire.

## Contrôles automatiques

Depuis la racine du dépôt :

```powershell
git diff --check
./build.cmd
./tools/check-duration-formatting.cmd
./tools/check-project-consistency.cmd
```

Résultats attendus :

- assembly `0.3.80.0` ;
- audit des durées avec `104` clés uniques ;
- versions, XML, traductions et documentation cohérents ;
- aucune erreur de compilation.

## Résultat final

Révision `r1` validée :

- les six tests obligatoires sont conformes ;
- le rapport confirme le facteur `x1.25` et la priorité
  `conflit ouvert > alliance > rivalité` ;
- aucune doctrine inéligible n'est débloquée ;
- les storytellers ordinaires n'appliquent aucun modificateur ;
- les chemins forcés restent déterministes ;
- les raids alliés standard, différés et conjoints restent conformes ;
- `Player.log` ne contient aucune nouvelle erreur pertinente.
