# Validation finale - 0.3.58-dev

Jalon : `0.3.58-dev - Add persistent biological naquadah traces`

Branche : `feature/persistent-naquadah-biological-traces`

Base : `v0.3.57-dev`

Version de DLL attendue : `0.3.58.0`

Révision locale : `r1`

Statut : rebuild forcé et validation ciblée `r1` réussis ; branche, tag
annoté `v0.3.58-dev` et wiki séparé publiés.

Charger dans cet ordre :

```text
Core
Harmony
Biotech
GateRim SG-1
```

## Test obligatoire court

Préparer un colon joueur humain sans le gène `naquadah dans le sang`. Ouvrir
exactement :

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Biological naquadah traces...
```

1. Utiliser `Equip kara kesh on target`, puis `Inspect pawn trace state` sur le colon. Le rapport doit afficher
   `persistentTrace=False` et `karaKeshEligible=False`. Le gizmo vanilla
   `Énergie du bouclier` doit être absent.
2. Ouvrir exactement :

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh shield...
```

   Utiliser `Apply ranged test hit` sur le colon : le tir doit le blesser et ne
   doit pas être absorbé.
3. Revenir dans `Biological naquadah traces...`, utiliser `Apply persistent
   trace`, puis `Inspect pawn trace state`. Le gène doit apparaître et le
   rapport doit afficher `persistentTrace=True` et `karaKeshEligible=True`.
4. Sélectionner le colon jusqu'à apparition du gizmo `Énergie du bouclier`, puis
   appliquer un nouveau `Apply ranged test hit`. Le bouclier doit absorber le
   tir sans blessure.
5. Sur un second humain sans trace, utiliser `Apply adult symbiote test state`,
   vérifier la trace, puis `Remove adult symbiote test state`. La trace doit
   subsister après retrait, sauvegarde et rechargement.
6. Sur un Jaffa compatible, utiliser `Apply Prim'ta test state`, vérifier la
   trace, puis `Remove Prim'ta test state`. La trace doit également subsister.
7. Contrôler `Player.log` sans nouvelle erreur XML, Def, gène, Scribe ou C#.

Résultat attendu : l'activation du kara kesh dépend uniquement du marqueur
biologique persistant ; un porteur non compatible peut transporter ou porter
l'objet sans activer le champ ; les anciens hôtes restent compatibles.

Résultat : réussi. Le porteur non tracé est resté sans champ ni gizmo et le
tir de test l'a blessé. L'ajout de la trace a activé le bouclier et absorbé le
tir suivant. Les traces d'hôte adulte et de Prim'ta ont persisté après retrait,
sauvegarde et rechargement. Aucun nouvel échec XML, Def, gène, Scribe ou C# n'a
été observé dans `Player.log`.

## Tests optionnels

- `Spawn hostile System Lord with kara kesh` : vérifier trace, gizmo, absorption,
  mêlée traversante et rupture IEM sans régression de `0.3.57-dev`.
- Vérifier un hôte Tok'ra généré ou rencontré.
- Sauvegarder un pawn tracé en caravane puis recharger.
- Retirer la trace d'un hôte actif, lancer `Reconcile all known pawns` et
  confirmer sa restauration.
- Porter le kara kesh sans trace et vérifier qu'une arme à distance reste
  utilisable, puisque le champ inactif ne bloque pas les tirs sortants.

## Limites connues

Le marqueur reste un xénogène visible et acquis pour préserver le Def et les
sauvegardes existants. L'extraction ou la duplication artificielle de ce gène
par les systèmes Biotech n'est pas traitée dans ce jalon.
