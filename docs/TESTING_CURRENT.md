# Current milestone validation

Jalon : `0.3.72-dev - Repair 0.3.71 publication documentation`

Branche : `fix/0.3.71-publication-documentation`

Révision corrective candidate : `r1`

Statut : validation ciblée requise avant publication.

## Contexte de régression

Le commit et le tag publiés `v0.3.71-dev` pointent sur
`a52c5ab13cc6943926b7f5d83743922793e3262a`. Le code fonctionnel validé en
`0.3.71-dev-r5` est présent, mais le paquet documentaire final n'a pas été
inclus dans ce commit.

Ce jalon correctif ne réécrit pas le tag. Il publie les documents omis dans un
nouveau commit versionné et tagué séparément.

## Build and consistency

Depuis la racine du dépôt :

```powershell
git diff --check
.\build.cmd
.\tools\check-duration-formatting.cmd
.\tools\check-project-consistency.cmd
```

Version de DLL attendue : `0.3.72.0`

Résultats attendus :

- DLL `0.3.72.0` construite avec succès ;
- audit terminé par `Duration-formatting audit passed.` ;
- `104` clés de migration explicites et uniques ;
- aucune traduction avec unité fixe non migrée ;
- aucun convertisseur manuel joueur non approuvé ;
- versions `0.3.72-dev` / `0.3.72.0` cohérentes dans les métadonnées et
  documents actifs ;
- aucun fichier de gameplay modifié par le correctif.

## Validation ciblée en jeu

- Charger le menu principal et vérifier l'absence de nouvelle erreur rouge.
- Confirmer que les métadonnées du mod affichent `0.3.72-dev`.
- Charger une sauvegarde utilisée pour `0.3.71-dev-r5`.
- Vérifier une durée déjà validée, par exemple l'inspection d'un site ou un
  cooldown du communicateur, sans unité dupliquée.
- Sauvegarder et recharger sans déplacement de l'échéance observée.
- Accepter `Player.log` uniquement s'il ne contient aucune nouvelle erreur
  Harmony, XML, traduction ou C#.

## Contrôle du périmètre

```powershell
git diff --name-status v0.3.71-dev
git diff --check v0.3.71-dev
```

Le diff doit rester limité aux documents restaurés, aux deux sources wiki, aux
métadonnées de version et à la DLL reconstruite selon le workflow du dépôt.
Aucun fichier sous `Source/GateRimSG1/**/*.cs`, `Defs/`, `Patches/` ou
`Languages/` ne doit changer.

## Publication après validation

La publication sera effectuée seulement après retour explicite du testeur :

- commit final sans suffixe `-r1` ;
- fast-forward dans `develop` ;
- tag annoté unique `v0.3.72-dev` ;
- synchronisation du wiki séparé ;
- aucune modification de `v0.3.71-dev`.
