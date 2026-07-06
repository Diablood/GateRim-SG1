# Current milestone validation

Jalon : `0.3.78-dev - Add delayed allied Goa'uld raid reinforcements`

Branche : `feature/goauld-allied-reinforcements`

Révision locale : `r1`

Version de DLL attendue : `0.3.78.0`

Statut : validation finale `r1` réussie et jalon publié.

## Préparation

- utiliser une carte de colonie joueur ;
- activer le storyteller `Commandement SG-1` ;
- activer le mode développeur ;
- mettre le jeu en pause avant de préparer les relations.

## Test principal obligatoire

1. Ouvrir
   `Actions de débogage > GateRim SG-1 > Goa'uld inter-domain relations...`.
2. Lancer `Show relation report`.
3. Si moins de deux domaines Goa'uld actifs sont listés, lancer une seule fois
   `Create additional test domain`.
4. Lancer `Set first pair: Alliance`.
5. Lancer `Force allied natural raid (1200 points, short delay)`.
6. Vérifier que le premier groupe arrive à pied depuis un bord de carte.
7. Avant l'arrivée du second groupe, vérifier qu'aucune lettre, alerte ou
   minuterie n'annonce des renforts.
8. Laisser passer environ `600` ticks, soit approximativement dix secondes à
   vitesse normale.
9. Vérifier qu'un second groupe Jaffa entre à pied depuis un bord de carte avec
   une couleur de faction différente.
10. Vérifier qu'une lettre apparaît seulement à cet instant, avec le titre
    visible `Renforts Goa'uld alliés : <nom du domaine>` et un court texte RP.
11. Observer les deux groupes : ils doivent attaquer la colonie et ne jamais se
    prendre mutuellement pour cible.
12. Ouvrir `Show allied reinforcement report` dans le même sous-menu et vérifier
    qu'une paire primaire/alliée active est indiquée.
13. Examiner `Player.log` et signaler toute nouvelle erreur Harmony, C#, Scribe,
    raid, Lord, traduction ou génération de pawn.

## Résultat attendu

- le raid principal et la vague alliée partagent le budget final de `1320`
  points issu des `1200 × 1,10` points du test ;
- aucun budget gratuit, incident storyteller ou arrivée en pod n'est ajouté ;
- le délai reste secret jusqu'à l'arrivée ;
- la lettre RP nomme le domaine allié au moment exact de son entrée ;
- les deux couleurs sont visibles ;
- la coopération n'altère pas durablement les relations entre factions.

## Résultat validé

Le mainteneur confirme le test principal :

- aucune annonce de renfort avant la seconde arrivée ;
- vague différée visible avec une couleur de domaine distincte ;
- lettre RP affichée uniquement à l'arrivée ;
- aucune attaque entre les deux forces Goa'uld alliées ;
- aucun défaut bloquant signalé dans `Player.log`.

Les régressions facultatives ci-dessous n'ont pas été requises pour valider
localement `r1`.

## Régressions facultatives

### Sauvegarde avant l'arrivée

1. Refaire le test principal.
2. Sauvegarder après l'arrivée du premier groupe mais avant `600` ticks.
3. Recharger la sauvegarde.
4. Vérifier que la vague alliée arrive une seule fois avec sa lettre.

### Sauvegarde pendant la coopération

1. Sauvegarder après l'arrivée des deux groupes.
2. Recharger.
3. Vérifier les couleurs distinctes et l'absence de combat fratricide.

### Retraite

Laisser ou provoquer la retraite du groupe principal. Les survivants alliés
doivent recevoir à leur tour un ordre de sortie de carte.

### Exclusions

- sous `800` points finaux, aucune vague n'est planifiée ;
- sous Cassandra, Phoebe, Randy ou un autre storyteller compatible, une
  alliance enregistrée ne produit aucune vague ;
- un conflit ouvert prioritaire conserve le facteur `75 %` et bloque les
  renforts alliés ;
- les raids contrôlés, représailles, missions et sites restent inchangés.

## Contrôles automatiques

Depuis la racine du dépôt :

```powershell
git diff --check
.\build.cmd
.\tools\check-duration-formatting.cmd
.\tools\check-project-consistency.cmd
```

Résultats attendus :

- assembly `0.3.78.0` ;
- audit des durées avec `104` clés uniques ;
- cohérence des versions et traductions ;
- aucun lien Markdown local manquant ;
- aucune erreur de compilation.
