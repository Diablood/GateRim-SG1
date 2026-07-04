# Validation locale - 0.3.62-dev

Jalon : `0.3.62-dev - Add Goa'uld healing device prototype`

Branche : `feature/goauld-healing-device`

Base : `v0.3.61-dev`

Version de DLL attendue : `0.3.62.0`

Révision locale : `r1`

Statut : révision finale `r1` reconstruite et validée en jeu, puis branche,
tag annoté `v0.3.62-dev` et wiki séparé publiés.

Charger dans cet ordre :

```text
Core
Harmony
Biotech
GateRim SG-1
```

## Test obligatoire court

1. Placer deux colons joueurs côte à côte, puis ouvrir exactement :

   ```text
   Actions de débogage > GateRim SG-1 > Goa'uld... > Healing bracelet...
   ```

2. Lancer `Prepare healing-bracelet wearer` sur le soigneur, puis
   `Prepare bleeding patient` sur le second colon.
3. Sélectionner le soigneur, utiliser le gizmo visible
   `Utiliser le bracelet de guérison` / `Use healing bracelet`, puis cibler le
   patient adjacent.
4. Dans l'onglet Santé du patient, vérifier que les trois coupures sont
   traitées, que la guérison totale ne dépasse pas `20` et que la perte de sang
   passe d'au moins `30 %` à environ `15 %`.
5. Sur le soigneur, vérifier `épuisement du dispositif de guérison` /
   `healing-device exhaustion`. Lancer `Inspect healing-bracelet state` : le
   cooldown doit être proche de `30000` ticks et une seconde utilisation doit
   être impossible.
6. Sauvegarder, recharger et confirmer la persistance de la fatigue et du
   cooldown.
7. Ouvrir exactement :

   ```text
   Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh...
   ```

   Lancer `Spawn hostile System Lord with rank equipment`, vérifier qu'il porte
   le kara kesh et le bracelet, puis appliquer `Prepare bleeding patient` sur
   lui depuis le menu `Healing bracelet...`.
8. Dépauser : sous environ `60` ticks, le Grand Maître doit se soigner une fois,
   recevoir la fatigue et entrer en cooldown sans boucle de soins.
9. Dans l'onglet de recherche `GateRim SG-1`, vérifier que
   `Dispositifs de guérison Goa'uld` exige `Biotechnologies Goa'uld` et
   `Kara kesh`.
10. Contrôler `Player.log` sans nouvelle erreur XML, Def, apparel, Hediff,
    ciblage, Scribe ou C#.

## Tests optionnels

- sans trace persistante de naquadah : bracelet portable mais gizmo désactivé ;
- animal, mécanoïde, mort, pawn sain, hors contact ou derrière un mur : refus ;
- plus de quatre blessures : tous les saignements stabilisés, mais seulement
  quatre blessures réellement guéries dans le budget de `20` ;
- cicatrice permanente, maladie, infection, cancer, addiction et partie
  manquante : aucun changement ;
- aucune régression des quatre fonctions publiées du kara kesh.

## Résultat

Le mainteneur confirme le test obligatoire de la révision locale `r1` : soin,
stabilisation des saignements, fatigue, cooldown, sauvegarde/rechargement,
équipement et auto-soin du Grand Maître hostile, recherche et `Player.log`
sont validés. Aucun correctif fonctionnel `r2` n'est requis.

La branche `feature/goauld-healing-device`, le tag annoté `v0.3.62-dev` et le
wiki séparé synchronisé ont ensuite été publiés avec l'autorisation du
mainteneur.
