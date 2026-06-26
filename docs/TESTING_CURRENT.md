# Validation terminée - 0.3.49-dev

Jalon : `0.3.49-dev - Add optional Tok'ra world-faction selection`

Branche : `feature/tokra-world-faction-selection-audit`

Tag de depart : `v0.3.48-dev`

Version de DLL attendue : `0.3.49.0`

Revision locale : `r5`

Statut : test principal r5 valide, jalon publié sous `v0.3.49-dev`.

Retour `r4` : l'icone Tok'ra est bien presente, mais aucun avertissement jaune n'apparait au retrait de la faction. Diagnostic : l'appel Harmony etait insere avant une cible de branche vanilla et pouvait etre saute par le flux normal.

## 1. Controles statiques et chargement

- [x] `git diff --check` ne signale aucune erreur.
- [x] `./tools/check-project-consistency.cmd` termine avec un code `0`.
- [x] Le rebuild force `0.3.49.0` termine sans erreur de compilation.
- [x] Le mod charge avec l'ordre minimal suivant :

```text
Core
Harmony
Biotech
GateRim SG-1
```

- [x] RimWorld atteint la creation du monde sans erreur XML, `0Harmony`, `FactionDef`, `factionIconPath` ou `SG1_Tokra`.

## 2. Test principal r5

Objectif : verifier uniquement l'icone, l'avertissement jaune et le retour a l'etat actif.

1. Depuis le menu principal, ouvrir `New colony`.
2. Avancer jusqu'a l'ecran `Create world`.
3. Dans la section `Factions`, reperer la ligne visible `Tok'ra`.
4. Verifier que la ligne `Tok'ra` est selectionnee une fois par defaut et affiche une icone distincte des maisons vanilla.
5. Cliquer sur le bouton de suppression de la ligne `Tok'ra`.
6. Verifier qu'une ligne jaune `Warning:` apparait immediatement et indique que les contacts, la mission d'introduction, les incidents et les operations Tok'ra recurrentes seront desactives pour cette partie.
7. Cliquer sur `Add...`, selectionner `Tok'ra`, puis verifier que l'avertissement jaune disparait.
8. Controler `Player.log`.

Resultat attendu : l'icone Tok'ra est visible, l'avertissement apparait des que la faction est retiree, disparait des qu'elle est rajoutee, et `Player.log` ne contient aucune nouvelle erreur GateRim SG-1.

Resultat r5 : valide par testeur.

## 3. Regression optionnelle - partie avec Tok'ra

Generer un monde en conservant l'entree Tok'ra, demarrer la partie puis ouvrir :

```text
GateRim SG-1 > Tok'ra... > World selection... > Show audit
```

- [ ] `Displayed in world faction selection` vaut `yes`.
- [ ] `Default selected count` vaut `1`.
- [ ] `Maximum configurable count` vaut `1`.
- [ ] `Mandatory count outside selection` vaut `0`.
- [ ] `Saved faction instances` vaut `1`.
- [ ] `Tok'ra content enabled` vaut `yes`.
- [ ] Aucune colonie Tok'ra n'existe.

## 4. Regression optionnelle - partie sans Tok'ra

Creer un second monde apres avoir retire l'entree Tok'ra.

- [ ] La generation du monde termine normalement.
- [ ] L'audit indique `Saved faction instances: 0`.
- [ ] L'audit indique `Tok'ra content enabled: no`.
- [ ] Aucune colonie Tok'ra n'existe.
- [ ] Un communicateur securise ne revele aucune action Tok'ra au clic droit.
- [ ] Sauvegarder, recharger, attendre au moins `1200` ticks et confirmer que le nombre d'instances reste `0`.
- [ ] `Player.log` ne signale aucune reconciliation automatique ni recreation de faction Tok'ra.

Publication finale : dépôt principal tagué `v0.3.49-dev` et wiki synchronisé.
