# Validation finale - 0.3.60-dev

Jalon : `0.3.60-dev - Add kara kesh neural attack`

Branche : `feature/kara-kesh-neural-attack`

Base : `v0.3.59-dev`

Version de DLL validée : `0.3.60.0`

Révision locale : `r1`

Statut : révision finale `r1` reconstruite et validée en jeu, puis branche,
tag annoté `v0.3.60-dev` et wiki séparé publiés. Aucun correctif fonctionnel
`r2` n'a été requis.

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

Lancer `Equip kara kesh on target`, puis `Apply persistent trace` si nécessaire.

Ouvrir ensuite :

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh...
```

Lancer `Prepare neural attack test state`, puis `Inspect neural attack state`
sur le colon. Le rapport doit indiquer une trace présente, un bouclier actif,
`4.00` points d'énergie et `cooldownTicks=0`.

## Test obligatoire court

1. Lancer `Spawn hostile System Lord with kara kesh` et maintenir les deux
   pawns à moins de `8,9` cases avec une ligne de vue dégagée.
2. Sélectionner le colon, cliquer sur `Attaque neurale` / `Neural attack`, puis
   cibler le Grand Maître hostile.
3. Vérifier un flash et le texte flottant de douleur neurale. Dans l'onglet
   Santé, la cible doit recevoir `douleur neurale du kara kesh` pendant environ
   `600` ticks.
4. Vérifier une douleur supplémentaire de `45 %` et une Conscience multipliée
   par `80 %`, sans blessure directe, sans recul et sans capacité de Déplacement
   forcée à zéro.
5. Relancer `Inspect neural attack state` sur le colon. L'énergie doit être
   proche de `2.25` et le cooldown proche de `1200` ticks. Une réutilisation
   immédiate doit être refusée.
6. Lancer `Clear neural agony` sur la cible, puis `Prepare neural attack test
   state` sur le Grand Maître. Dépauser avec le colon hostile dans la portée.
   Le Grand Maître doit utiliser automatiquement l'attaque neurale dans un
   délai d'environ `60` ticks.
7. Sauvegarder pendant l'effet et le cooldown, recharger puis confirmer que la
   durée temporaire, l'énergie et le cooldown persistent.
8. Contrôler `Player.log` sans nouvelle erreur XML, Def, Scribe, ciblage ou C#.

Résultat attendu : le joueur et l'IA utilisent la même attaque neurale ; elle
partage réellement l'énergie du bouclier, affecte uniquement une cible
humanoïde biologique hostile et consciente, applique une douleur temporaire
sans implémenter la paralysie prolongée, puis expire proprement.

Résultat : validé sur `r1`. Le gizmo joueur, l'effet temporaire, la
consommation de `1.75` énergie, le cooldown de `1200` ticks, l'utilisation par
l'IA hostile, la sauvegarde/recharge et `Player.log` sont acceptés. Aucun `r2`
fonctionnel n'est requis.

## Tests optionnels

- cible alliée, animale, mécanoïde, à terre, hors portée ou derrière un mur :
  ciblage refusé ;
- énergie inférieure à `1.75` : gizmo désactivé ;
- bouclier brisé par `Apply EMP test hit` : attaque indisponible pendant les
  `1800` ticks de réinitialisation ;
- porteur sans traces persistantes : aucun bouclier ni gizmo offensif ;
- l'onde cinétique existante fonctionne encore avec son coût de `1.25`, son
  cooldown de `900` ticks et son recul sûr ;
- l'effet neural expire naturellement et les capacités reviennent à leur état
  antérieur.

## Limites connues

Cette révision n'ajoute ni paralysie prolongée, ni torture sur cible à terre,
ni contrôle mental, ni télécommande, ni attaque de zone. L'icône reste
provisoirement l'icône d'attaque générique.
