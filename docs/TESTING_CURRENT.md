# Validation finale — 0.3.33-dev-r2

Jalon : `0.3.33-dev - Gate Tok'ra operations behind the communicator`

Version de DLL validée : `0.3.33.0`

Branche : `feature/tokra-communicator-operation-gating`

Tag final : `v0.3.33-dev`

## Résultat r1 — construction et disponibilité

- `SG1_TokraSecureCommunications` verrouille les nouvelles constructions du communicateur.
- Les communicateurs construits avant ce changement restent utilisables.
- Le service commun reconnaît correctement une carte de colonie joueur, la propriété, les composants requis et l'alimentation active.
- La coupure et le retour du courant sont reflétés par `Tok'ra communicator: show availability`.
- Sauvegarde et rechargement ne modifient pas l'état physique du canal.

## Résultat r2 — blocage d'une nouvelle offre

Validation effectuée avec :

```text
Debug actions menu
-> GateRim SG-1
-> Tok'ra ops: make natural offer due
```

Puis :

```text
Debug actions menu
-> GateRim SG-1
-> Tok'ra ops: show framework state
```

Résultats validés sans communicateur alimenté :

- `Communicator gate blocked: True` ;
- `Communicator available: False` ;
- aucune lettre d'offre ;
- aucun archétype actif ;
- aucun échec, refus ou changement de confiance ;
- aucune relance horaire créant une offre ;
- sauvegarde/rechargement conservant le verrou et le slot vide.

## Résultat r2 — reprise organique du canal

Après construction, rallumage ou réalimentation d'un communicateur valide :

- `Communicator gate blocked: False` ;
- `Communicator available: True` ;
- `Next opportunity tick` est recalculé dans le futur ;
- aucune lettre n'apparaît immédiatement au retour du courant ;
- sauvegarde/rechargement conserve la nouvelle échéance sans nouveau tirage.

L'action suivante a ensuite produit une seule offre normale par le véritable tirage pondéré :

```text
Debug actions menu
-> GateRim SG-1
-> Tok'ra ops: roll next natural offer
```

## Résultat r2 — opération existante préservée

- Une offre déjà visible reste dans le slot actif lorsque le communicateur perd son alimentation ou est détruit.
- Sauvegarde/rechargement ne supprime ni ne remplace cette offre.
- Le retour du canal permet de reprendre le flux normal.
- La coupure ne modifie pas l'archétype, ne crée pas une seconde opération, n'ajoute pas de conséquence de confiance et ne duplique pas la lettre.
- Les délais propres à l'opération continuent à suivre leurs règles existantes.
- Les actions `Tok'ra ops: force ... offer` ne contournent pas le verrou en l'absence de communicateur valide.

## Contrôles finaux

- contrôle de cohérence du projet réussi pour `0.3.33-dev`, `0.3.33.0` et `83` backstories ;
- rebuild forcé validé ;
- aucune nouvelle erreur GateRim SG-1 signalée dans `Player.log` ;
- mission d'introduction conservée hors du verrou récurrent ;
- demandes manuelles du communicateur conservées hors du planificateur organique ;
- passe finale des textes joueur terminée sans correction supplémentaire nécessaire ;
- diagnostic technique réservé au mode développeur ou aux informations avancées ;
- page wiki des opérations organiques mise à jour avec l'accès au canal et la reprise différée.

## Limites conservées

- Une panne ne met pas en pause les échéances d'une opération déjà proposée ou acceptée.
- Le jalon ne modifie pas les poids, récompenses, conséquences ou difficultés propres aux six archétypes existants.
- L'équilibrage statistique sur de très longues parties reste évolutif et ne remet pas en cause la validation fonctionnelle du verrou.
