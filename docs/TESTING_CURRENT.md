# Current milestone validation

Jalon : `0.3.79-dev - Add coordinated allied Goa'uld joint raids`

Branche : `feature/goauld-joint-raids`

Révision finale validée : `r1`

Version de DLL validée : `0.3.79.0`

Statut : validation finale réussie et acceptée par le mainteneur.

## Résultat fonctionnel validé

Le test obligatoire confirme :

- la lettre de relation nomme les deux domaines sans lancer d'attaque ;
- le raid d'alliance standard ne génère qu'un domaine ;
- le raid conjoint fait arriver simultanément deux forces de couleurs
  différentes depuis des bords opposés ;
- l'unique lettre `Assaut coordonné des Goa'uld` nomme les deux domaines ;
- les deux détachements attaquent la colonie sans se cibler mutuellement ;
- le rapport indique `mode=JointRaid` et les effectifs suivis ;
- l'ordre de retrait primaire provoque le départ des deux détachements ;
- aucun nouvel incident de log pertinent n'est signalé.

Le budget final `1320` est partagé `792/528`, soit `60/40`, sans point gratuit
ni fréquence supplémentaire. Aucune rupture d'alliance, conséquence territoriale
ou récompense spéciale n'est créée.

## Observation sur les officiers Jaffa

Aucun officier n'a été observé pendant le test conjoint forcé à `1200` points.
Ce résultat est attendu et ne constitue pas une régression :

- le chemin développeur forcé du raid conjoint n'active pas le fallback dédié
  qui garantit un officier dans son test historique ;
- après le partage, le détachement principal ne reçoit que `792` points ;
- à ce budget, le garde Jaffa à `145` points n'est normalement pas admissible
  par la courbe de coût individuel ;
- sans garde généré, la couche d'officier n'a aucun pawn équivalent à remplacer.

Le chemin naturel publié en `0.3.75-dev`, son seuil de cinq Jaffa et son
remplacement garde/officier à budget identique restent présents et inchangés.

## Régressions facultatives durables

- le renfort différé conserve son arrivée et sa lettre tardive ;
- sauvegarde/recharge pendant `JointRaid` conserve factions et coopération ;
- enlèvement et destruction restent exclus du mode conjoint ;
- les autres storytellers ne produisent aucune forme de raid alliée ;
- plusieurs alliances ne fournissent jamais plus d'un partenaire.

## Contrôles automatiques

Depuis la racine du dépôt :

```powershell
git diff --check
./build.cmd
./tools/check-duration-formatting.cmd
./tools/check-project-consistency.cmd
```

Résultats attendus :

- assembly `0.3.79.0` ;
- audit des durées avec `104` clés uniques ;
- versions et traductions cohérentes ;
- aucun lien Markdown local manquant ;
- aucune erreur de compilation.
