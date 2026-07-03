# Validation finale - 0.3.59-dev

Jalon : `0.3.59-dev - Add kara kesh kinetic blast`

Branche : `feature/kara-kesh-kinetic-blast`

Base : `v0.3.58-dev`

Version de DLL validée : `0.3.59.0`

Révision locale : `r2`

Statut : révision finale `r2` reconstruite et validée en jeu, puis branche,
tag annoté `v0.3.59-dev` et wiki séparé publiés. `r1` avait échoué au build
avec `CS0266`; `r2` corrige uniquement le type de la collection de pawns
utilisée par l'IA.

Charger dans cet ordre :

```text
Core
Harmony
Biotech
GateRim SG-1
```

## Préparation commune

Utiliser une zone ouverte et conserver le jeu en pause. Sur un colon joueur,
ouvrir exactement :

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Biological naquadah traces...
```

Lancer `Equip kara kesh on target`, puis `Apply persistent trace` si le colon ne
porte pas déjà le marqueur biologique.

Ouvrir ensuite :

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh...
```

Lancer `Prepare kinetic blast test state`, puis `Inspect kinetic blast state`
sur le colon. Le rapport doit indiquer une trace présente, un bouclier actif,
`4.00` points d'énergie et aucun cooldown.

## Test obligatoire court

1. Lancer `Spawn hostile System Lord with kara kesh`, garder la partie en
   pause, puis appliquer `Prepare kinetic blast test state` au Grand Maître.
   Placer ou choisir le colon à moins de `10.9` cases du Grand Maître, avec une
   ligne de vue dégagée.
2. Sélectionner le colon, cliquer sur le gizmo `Onde cinétique` / `Kinetic
   blast`, puis cibler le Grand Maître hostile.
3. Vérifier un flash et un texte visibles, des dégâts contondants, un
   étourdissement bref et un recul maximal de deux cases si l'espace est libre.
   La cible ne doit jamais entrer dans un mur, une case occupée ou hors carte.
4. Relancer `Inspect kinetic blast state` sur le colon. L'énergie doit avoir
   diminué d'environ `1.25` et le cooldown doit être proche de `900` ticks. Une
   nouvelle activation immédiate doit être refusée.
5. Lancer `Reset kinetic blast cooldown`, placer un obstacle juste derrière la
   cible, puis tirer de nouveau. Dégâts et étourdissement doivent s'appliquer,
   mais le recul doit s'arrêter avant l'obstacle.
6. Laisser un pawn joueur hostile au Grand Maître dans la portée, puis
   dépauser. Le Grand Maître doit utiliser automatiquement l'onde dans un délai
   d'environ `60` ticks, consommer sa propre énergie et entrer en cooldown.
7. Sauvegarder pendant le cooldown, recharger et relancer `Inspect kinetic blast
   state`. Le cooldown et l'énergie restante doivent persister.
8. Contrôler `Player.log` sans nouvelle erreur XML, Def, Scribe ou C#.

Résultat attendu : le joueur et l'IA utilisent la même onde cinétique ; la
capacité partage réellement l'énergie du bouclier, respecte sa condition
biologique, son cooldown, la portée et la ligne de vue, et ne produit jamais de
recul invalide.

Résultat : réussi. Le joueur et le Grand Maître hostile ont utilisé la même
onde cinétique. La dépense de `1.25` énergie, le cooldown de `900` ticks, le
recul sûr en terrain libre et devant un obstacle, l'utilisation autonome de
l'IA et la persistance après sauvegarde/rechargement ont été validés. Aucun
nouvel échec XML, Def, Scribe ou C# n'a été observé dans `Player.log`.

## Tests optionnels

- cibler un pawn derrière un mur : la cible doit être refusée ;
- descendre l'énergie sous `1.25` : le gizmo doit être désactivé ;
- utiliser `Apply EMP test hit` : l'onde doit rester indisponible durant les
  `1800` ticks de réinitialisation ;
- confirmer qu'un porteur non tracé n'obtient toujours ni bouclier actif ni
  gizmo d'onde cinétique ;
- confirmer les anciens contres du bouclier : mêlée et chaleur traversantes,
  armes à distance ordinaires bloquées vers l'extérieur.

## Limites connues

L'onde est un impact direct sur un seul pawn. Elle ne crée pas de projectile,
d'explosion de zone, d'attaque neurale, de paralysie prolongée ou d'effet sur
les bâtiments. L'icône du gizmo réutilise l'icône d'attaque vanilla en attendant
la passe visuelle globale.
