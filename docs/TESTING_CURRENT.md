# Tests du jalon actif

Jalon : `0.3.19-dev - Audit cultural backstory skill coverage`

Branche attendue : `feature/cultural-backstory-skill-coverage`

Base attendue : `v0.3.18-dev`

Révision locale validée : `0.3.19-dev-r1`

Version de DLL attendue : `0.3.19.0`

Statut : validation fonctionnelle terminée sur `0.3.19-dev-r1` ; jalon clôturé et publié sous `v0.3.19-dev`.

## Résultat final validé

- Rebuild forcé validé avec la DLL `0.3.19.0`.
- Chargement validé sans nouvelle erreur de `BackstoryDef`, `skillGains`, patch, catégorie ou traduction.
- Les onze nouvelles carrières apparaissent dans les cinq pools culturels modifiés.
- Les trois nouvelles carrières SGC apparaissent dans le scénario Équipe SG isolée sans carrière adulte vanilla.
- Les carrières Goa'uld ordinaires, Grands Maîtres et Tok'ra conservent les groupes de noms attendus.
- L'origine humaine hors-monde peut sélectionner `SG1_OffworldHuman_QuarryWorker`.
- L'origine Tau'ri peut sélectionner les trois nouvelles carrières SGC.
- Les poids des origines générées restent inchangés à `1` et `0.2`, avec une majorité hors-monde.
- Les pools Jaffa Goa'uld et Jaffa libres restent inchangés et sans fuite d'une nouvelle carrière.
- Les starters humains ordinaires conservent une majorité nette de carrières vanilla.
- Les noms, backstories et bonus persistent après sauvegarde et rechargement.
- Une sauvegarde `0.3.18-dev` conserve les histoires déjà attribuées sans reroll.
- Dix basculements Tok'ra successifs ne provoquent aucun cumul, perte ou duplication de compétences.
- Le diagnostic culturel reste en lecture seule et affiche le profil correspondant.
- `Player.log` est propre pour le périmètre testé.
- Décision finale : conserver la révision fonctionnelle `r1` sans correctif C# ou Def supplémentaire.

## 1. Couverture culturelle validée

Les sept ensembles audités disposent désormais d'au moins une voie vers chacune des douze compétences standards :

- Tau'ri / SGC ;
- Jaffa Goa'uld ;
- Jaffa libres ;
- humains hors-monde ;
- hôtes Goa'uld ordinaires ;
- Grands Maîtres Goa'uld ;
- carrières Tok'ra.

Les compétences faiblement représentées restent documentées dans `docs/CULTURAL_SKILL_COVERAGE.md`. Elles ne justifient pas automatiquement de nouvelles backstories.

## 2. Scénario Équipe SG isolée

Résultat validé :

- `géologue planétaire du SGC` apparaît ;
- `cuisinier d'expédition du SGC` apparaît ;
- `archéologue de terrain du SGC` apparaît ;
- aucune carrière adulte vanilla ne remplace le pool SGC exclusif ;
- les noms restent Tau'ri.

## 3. Profils Goa'uld et Tok'ra

Résultat validé :

- `médecin de palais Goa'uld` et `duelliste de palais Goa'uld` apparaissent avec le groupe de noms Goa'uld ;
- `Grand Maître Goa'uld architecte de domaine` et `Grand Maître Goa'uld expérimentateur biomédical` apparaissent sur les hôtes Grands Maîtres ;
- les trois nouvelles carrières Tok'ra apparaissent avec le groupe de noms Tok'ra ;
- le rapport culturel affiche le profil attendu sans modifier le pawn.

## 4. Hôtes historiques Tok'ra générés

Résultat validé :

- `SG1_GeneratedHost_OffworldHuman` peut utiliser la nouvelle carrière de carrier ;
- `SG1_GeneratedHost_TauriSGCVolunteer` peut utiliser les trois nouvelles carrières SGC ;
- les poids relatifs restent inchangés ;
- l'origine hors-monde reste majoritaire ;
- les noms de l'hôte et du symbiote restent distincts ;
- aucun reroll n'est déclenché par la nouvelle liste de carrières.

## 5. Régressions culturelles

Résultat validé :

- aucun ajout ne fuit dans les pools Jaffa ;
- les humains ordinaires conservent leur fonctionnement additif et leur majorité vanilla ;
- les groupes de noms restent cohérents avec la carrière sélectionnée ;
- les backstories déjà enregistrées restent inchangées après chargement d'une sauvegarde `0.3.18-dev`.

## 6. Sauvegarde et identité Tok'ra

Résultat validé :

- sauvegarde et rechargement stables avec l'hôte actif ;
- sauvegarde et rechargement stables avec le symbiote actif ;
- dix basculements sans dérive des niveaux ou de l'expérience ;
- progression commune conservée ;
- aucune duplication des bonus de backstories.

## 7. Contrôle final

```powershell
git status --short
git diff --check
```

Le journal final ne contient aucune nouvelle erreur ou répétition attribuable à GateRim SG-1.
