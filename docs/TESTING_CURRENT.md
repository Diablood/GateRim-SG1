# Validation locale - 0.3.64-dev

Jalon : `0.3.64-dev - Add persistent Goa'uld domain doctrine profiles`

Branche : `feature/goauld-domain-doctrine-profiles`

Base : `v0.3.63-dev`

Version de DLL validée : `0.3.64.0`

Révision locale : `r2`

Statut : révision finale `r2` reconstruite et validée ; branche, tag annoté
`v0.3.64-dev` et wiki séparé publiés.

Charger dans cet ordre :

```text
Core
Harmony
Biotech
GateRim SG-1
```


## Correctif r2

Le premier paquet corrigé contenait encore dans `docs/wiki/_Sidebar.md` le lien
supprimé par le jalon de consolidation :

```text
Tokra-Interaction-Roadmap
```

`r2` retire uniquement cette entrée de navigation. Le contrôle suivant a été
validé sans cible Markdown manquante :

```powershell
.\tools\check-project-consistency.cmd `
    -ExpectedVersion 0.3.64-dev `
    -ExpectedBackstoryCount 83
```

## Test obligatoire validé

1. Ouvrir exactement :

   ```text
   Actions de débogage > GateRim SG-1 > Goa'uld... > Domain doctrines...
   ```

2. Lancer `Show domain doctrine report`.
3. Appliquer successivement au même domaine :
   - `Set doctrine: Conquest` : `4/1/1` ;
   - `Set doctrine: Enslavement` : `2/3/1` ;
   - `Set doctrine: Scorched earth` : `2/1/3`.
4. Ouvrir la fiche normale de la faction et vérifier que le nom et la
   description de la doctrine sont entièrement français, sans poids numériques.
5. Sauvegarder, recharger et vérifier que le profil du même domaine est conservé.
6. Ouvrir exactement :

   ```text
   Actions de débogage > GateRim SG-1 > Goa'uld... > Threat progression...
   > Show current progression
   ```

   Vérifier le domaine, son profil, les pourcentages effectifs et les seuils
   historiques inchangés.
7. Lancer les trois commandes de raid naturel forcé et confirmer leurs
   comportements direct, enlèvement et destruction.
8. Contrôler `Player.log`.

## Tests optionnels

- plusieurs domaines : profils indépendants ;
- remplacement du dirigeant : aucune réattribution ;
- doctrine spécialisée inéligible : poids effectif nul ;
- aucune modification de fréquence, points de menace ou délai de récidive ;
- aucune régression des représailles, opérations Tok'ra, kara kesh ou bracelet
  de guérison.

## Normalisation Git

Après extraction, appliquer une seule fois dans ce dépôt :

```powershell
git config --local core.autocrlf false
git add --renormalize .
git restore --staged .
```

Le `.gitattributes` versionné reste ensuite la source de vérité des fins de
ligne.

## Résultat

Le mainteneur confirme le rebuild forcé `0.3.64.0`, les trois profils et leurs
poids, la traduction française, la sauvegarde/recharge, les raids de régression
et `Player.log`.

Le contrôle de cohérence est également validé après la correction `r2` du lien
wiki obsolète. La branche `feature/goauld-domain-doctrine-profiles`, le tag
annoté `v0.3.64-dev` et le wiki séparé sont publiés.
