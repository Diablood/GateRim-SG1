# Validation terminée - 0.3.50-dev

Jalon : `0.3.50-dev - Add GateRim faction world icons`

Branche : `feature/faction-world-icon-overhaul`

Tag de départ : `v0.3.49-dev`

Version de DLL attendue : `0.3.50.0`

Révision locale : `r2`

Statut : test principal r2 validé. La r1 validait la couleur mais pas la lisibilité ; la r2 est acceptée après remplacement des trois nouvelles icônes par des silhouettes plus simples à contour sombre épais.

## 1. Contrôles statiques et chargement

- [x] `git diff --check` ne signale aucune erreur.
- [x] `./tools/check-project-consistency.cmd` termine avec un code `0`.
- [x] Le rebuild forcé `0.3.50.0` termine sans erreur de compilation.
- [x] Le mod charge avec l'ordre minimal suivant :

```text
Core
Harmony
Biotech
GateRim SG-1
```

- [x] RimWorld atteint la création du monde sans erreur XML, `FactionDef`, `factionIconPath`, texture manquante ou Harmony.

## 2. Test principal r2

Objectif : vérifier uniquement la lisibilité des icônes de faction et la variation vanilla de couleur sur les copies.

1. Depuis le menu principal, ouvrir `New colony`.
2. Sélectionner le scénario `Équipe SG isolée` / `Stranded SG team`.
3. Avancer jusqu'à l'écran `Create world`.
4. Dans la section `Factions`, vérifier que `Jaffa libres` / `Free Jaffa` utilise la nouvelle icône Jaffa libre simplifiée et lisible, avec contour sombre épais, et non la maison vanilla.
5. Vérifier que `Domaines des Grands Maîtres Goa'uld` / `Goa'uld System Lord domains` utilise la nouvelle icône pyramide/serpent simplifiée et lisible, avec contour sombre épais, et non l'avant-poste pirate vanilla.
6. Vérifier que `Tok'ra` conserve son icône Tok'ra circulaire dédiée.
7. Cliquer sur `Add...` et ajouter au moins deux entrées supplémentaires `Jaffa libres` / `Free Jaffa`.
8. Cliquer sur `Add...` et ajouter au moins deux entrées supplémentaires `Domaines des Grands Maîtres Goa'uld` / `Goa'uld System Lord domains`.
9. Vérifier que les copies d'une même faction gardent la même silhouette mais affichent des variations vanilla visibles de teinte, plus claires ou plus foncées.
10. Générer le monde et démarrer le scénario sur une tuile valide.
11. Une fois sur la carte, ouvrir l'onglet inférieur `Factions` et vérifier que `expédition du SGC` / `SGC expedition` utilise la nouvelle icône SGC simplifiée et lisible lorsqu'elle est affichée par RimWorld.
12. Contrôler `Player.log`.

Résultat attendu : les quatre identités de faction GateRim SG-1 sont lisibles à la taille finale de l'UI, les copies gardent la variation de couleur vanilla, l'icône Tok'ra validée reste inchangée, et `Player.log` ne contient aucune nouvelle erreur GateRim SG-1 liée aux textures, XML ou à Harmony.

Résultat r2 : validé par le testeur. Une légère variation de couleur subsiste, mais elle vient du rendu vanilla de RimWorld et non d'une couleur intégrée par GateRim SG-1 ; ce comportement est accepté.

Publication : commit et push de branche autorisés par le mainteneur. Le tag final `v0.3.50-dev` reste séparé de cette demande.

## 3. Régression optionnelle - avertissement Tok'ra

Dans l'écran `Create world` > `Factions` :

- [ ] Supprimer l'entrée `Tok'ra`.
- [ ] Vérifier que l'avertissement jaune `Warning:` apparaît immédiatement.
- [ ] Cliquer sur `Add...`, sélectionner `Tok'ra`, puis vérifier que l'avertissement disparaît.

Cette régression confirme que la passe visuelle n'a pas cassé le jalon `0.3.49-dev`.
