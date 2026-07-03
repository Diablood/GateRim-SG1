# Validation finale - 0.3.61-dev

Jalon : `0.3.61-dev - Add kara kesh paralysis hold`

Branche : `feature/kara-kesh-paralysis-hold`

Base : `v0.3.60-dev`

Version de DLL validée : `0.3.61.0`

Révision locale : `r1`

Statut : révision finale `r1` reconstruite et validée en jeu, puis branche,
tag annoté `v0.3.61-dev` et wiki séparé publiés. Aucun correctif fonctionnel
`r2` n'a été requis.

Charger dans cet ordre :

```text
Core
Harmony
Biotech
GateRim SG-1
```

## Préparation commune

Sur un colon joueur, ouvrir exactement :

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Biological naquadah traces...
```

Lancer `Equip kara kesh on target`, puis `Apply persistent trace` si nécessaire.

Ouvrir ensuite :

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh...
```

Lancer `Prepare paralysis hold test state`, puis `Inspect paralysis hold state`.
Le rapport doit indiquer une trace présente, un bouclier actif, `4.00` points
d'énergie, `cooldownTicks=0` et aucune cible active.

## Test obligatoire court

1. Lancer `Spawn hostile System Lord with kara kesh` et maintenir les deux pawns
   à moins de `6,9` cases avec une ligne de vue dégagée.
2. Sélectionner le colon, cliquer sur `Maintien paralysant` / `Paralysis hold`,
   puis cibler le Grand Maître hostile.
3. Vérifier le Hediff `maintien paralysant du kara kesh`, Déplacement à `0`,
   Manipulation à `10 %`, sans nouvelle blessure ni douleur ajoutée.
4. Relancer `Inspect paralysis hold state` : énergie proche de `1.50`, cooldown
   proche de `1800` ticks et cible active nommée. L'onde cinétique et l'attaque
   neurale doivent être indisponibles pendant le maintien.
5. Briser la portée ou la ligne de vue. L'effet doit disparaître sous environ
   `15` ticks et les capacités revenir, sans annuler le cooldown.
6. Lancer `Prepare paralysis hold test state` sur le Grand Maître, garder le
   colon hostile et à portée, puis dépauser. L'IA doit utiliser le maintien en
   environ `60` ticks et ne pas enchaîner un autre mode tant qu'il le maintient.
7. Sauvegarder pendant le maintien IA, recharger, vérifier la persistance du
   lien, de la durée, de l'énergie et du cooldown, puis utiliser
   `Release paralysis hold` sur le porteur.
8. Contrôler `Player.log` sans nouvelle erreur XML, Def, Scribe, Hediff,
   ciblage ou C#.

## Tests optionnels

- cible alliée, animale, mécanoïde, à terre, hors portée, derrière un mur ou
  déjà maintenue : refus ;
- énergie inférieure à `2.5` : commande désactivée ;
- IEM, retrait du kara kesh, perte de la trace ou porteur à terre : interruption
  sous environ `15` ticks ;
- `Clear paralysis hold` retire l'effet d'une cible de test ;
- les modes bouclier, onde cinétique et attaque neurale conservent leur
  comportement hors maintien actif.

## Résultat final

Le mainteneur a confirmé tous les tests obligatoires : ciblage joueur,
immobilisation et manipulation réduite, énergie et cooldown partagés,
interruptions par portée ou ligne de vue, utilisation IA prioritaire,
persistance après sauvegarde/rechargement, relâchement manuel et absence de
nouvelle erreur dans `Player.log`. Aucun correctif fonctionnel `r2` n'est requis.

## Limites du jalon

Aucune autre fonction spéculative du kara kesh n'est intégrée à ce jalon. Ces
concepts restent non planifiés dans `docs/IDEAS_TO_REVISIT.md`.
