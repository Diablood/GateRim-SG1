# Validation locale finale - 0.3.68-dev

Jalon : `0.3.68-dev - Add open-conflict Goa'uld pressure reduction`

Branche validée : `feature/goauld-open-conflict-pressure-reduction`

Base d'intégration : `develop` au tag publié `v0.3.67-dev`

Version de DLL validée : `0.3.68.0`

Révision locale finale : `r3`

Statut : build, cohérence, comportement fonctionnel, sauvegarde/recharge,
régressions et journal validés.

## Résultat final

La validation de la révision `r3` confirme :

- assembly `GateRimSG1.dll` reconstruite en version `0.3.68.0` ;
- versions publique, assembly, README, projet, tests actifs et brouillons wiki
  alignées sur `0.3.68-dev` ;
- deux domaines d'une paire en conflit ouvert affichent et utilisent un facteur
  de pression naturelle de `75%` sous Commandement SG-1 ;
- un domaine engagé dans plusieurs conflits ouverts reste à `75%` et ne reçoit
  aucun second multiplicateur ;
- le passage à Cassandra conserve la relation mais ramène immédiatement le
  facteur à `100%` ;
- le retour à Commandement SG-1 réactive le facteur sans transition artificielle ;
- une trêve ou un retour à la neutralité ramène immédiatement les deux domaines
  à `100%` ;
- les points vanilla restent visibles et servent au choix de doctrine ;
- les points effectifs correspondent à `max(1, points vanilla × 0,75)` ;
- la commande `Force current natural raid (pressure applied)` exerce le vrai
  chemin réduit et inscrit les points initiaux, effectifs et le facteur dans
  `Player.log` ;
- les régressions forcées à `300`, `800` et `1800` points conservent exactement
  leur budget demandé ;
- les raids contrôlés, missions et attaques interceptées restent inchangés ;
- une représaille d'extraction conserve les points stockés et ne produit aucune
  trace de réduction ;
- sauvegarde et recharge conservent l'état relationnel et re-dérivent le facteur
  sans nouveau champ sérialisé ;
- les relations, doctrines, ultimatums et représailles existants restent
  fonctionnels ;
- aucune nouvelle erreur C#, XML, Scribe, faction, storyteller ou raid
  attribuable au jalon n'apparaît dans `Player.log`.

## Périmètre fonctionnel publié

Le jalon ajoute :

- `GoauldOpenConflictPressureUtility`, utilité stateless qui calcule le facteur
  depuis le storyteller actif et les relations existantes ;
- `GoauldOpenConflictPressureDebugActions`, rapport par domaine et commande de
  test dédiée ;
- l'application du facteur après le choix de doctrine dans
  `IncidentWorker_GoauldJaffaNaturalRaid` ;
- l'affichage des points vanilla et effectifs dans le diagnostic de progression ;
- l'entrée correspondante dans le menu développeur des relations inter-domaines.

Le jalon ne modifie pas :

- `baseChance`, `earliestDay` ou `minRefireDays` du raid naturel ;
- les poids ou seuils de doctrine ;
- les représailles après extraction ;
- les raids contrôlés ou de mission ;
- les relations de bonne volonté ;
- les territoires, colonies, batailles inter-domaines, alliances, renforts ou
  raids conjoints ;
- le schéma de sauvegarde.

## Livraisons locales

- `r1` et `r2` sont abandonnées : leurs patchs documentaires dépendaient de
  contextes incompatibles avec l'arbre réel.
- `r3` remplace ces livraisons par un paquet cumulatif de fichiers complets,
  sans `.patch` ni script d'application.
- Aucun correctif fonctionnel supplémentaire n'a été requis après les tests de
  `r3`.

## Publication validée

- commit final effectué sur
  `feature/goauld-open-conflict-pressure-reduction` ;
- branche intégrée dans `develop` avec `git merge --ff-only` ;
- `develop` publiée sur `origin` ;
- tag annoté final unique `v0.3.68-dev` publié ;
- tag pelé, `develop` local et `origin/develop` vérifiés sur le même commit ;
- wiki séparé synchronisé et publié ;
- `main` laissée inchangée.

## Prochaine base

Le prochain jalon doit partir de `develop` après `v0.3.68-dev`. Aucun sujet ni
nom de branche suivant n'est encore sélectionné.
