# Validation finale — 0.3.40-dev

Jalon : `0.3.40-dev - Add hostile Goa'uld host takeover`

Branche : `feature/goauld-hostile-host-takeover`

Tag de départ : `v0.3.39-dev`

Version de DLL validée : `0.3.40.0`

Révision locale finale : `r4`

Tag final : `v0.3.40-dev`

## Résultat

La prise de contrôle hostile Goa'uld est fonctionnellement validée et publiée.

## Contrôles et build

- [x] `git branch --show-current` retourne `feature/goauld-hostile-host-takeover`.
- [x] `check-project-consistency.cmd` passe pour `0.3.40-dev` et `83` backstories.
- [x] Le build Windows produit `GateRimSG1.dll` version `0.3.40.0`.
- [x] Le journal de chargement utilise la DLL `0.3.40.0`.

## Parcours principal validé

- [x] Un symbiote Goa'uld hostile issu de l'incursion poursuit un hôte compatible sans alterner avec une fuite animale ou une sortie de carte.
- [x] L'implantation conserve l'identifiant du symbiote, son allégeance hostile et la faction déplacée de l'hôte.
- [x] L'état `Pending` et ses données persistent après sauvegarde/rechargement.
- [x] La conversion active retire le pawn de la barre des colons et le transfère dans la faction Goa'uld enregistrée.
- [x] Une seule lettre de menace est envoyée.
- [x] L'hôte converti attaque réellement les colons et les biens au lieu de chercher immédiatement à quitter la carte.
- [x] L'assaut et l'identité du symbiote persistent après sauvegarde/rechargement.
- [x] Une sauvegarde de développement `r3` migre automatiquement de l'ancien assaut sans retraite vers le Lord de raid actuel.
- [x] Après départ de tous les colons, l'hôte poursuit les cibles encore valables puis finit par se replier selon la logique vanilla du raid.
- [x] Il ne reste plus indéfiniment inactif ou endormi sur une carte abandonnée.

## Régressions validées

- [x] L'extraction d'urgence pendant la phase critique laisse l'hôte dans la faction joueur.
- [x] Le symbiote extrait conserve son identité et son allégeance Goa'uld hostile.
- [x] Une implantation Tok'ra ne déclenche aucune prise de contrôle hostile.
- [x] Un symbiote Goa'uld contrôlé par le joueur ou sans allégeance hostile ne retire pas le contrôle du pawn.
- [x] La récupération développeur retire le Lord de prise de contrôle avant de restaurer la faction déplacée.
- [x] Aucune nouvelle erreur GateRim SG-1 liée au jalon n'est signalée dans `Player.log`.

## Historique des révisions locales

- `r1` : persistance de l'allégeance et conversion de faction.
- `r2` : comportement d'assaut des symbiotes libres avant implantation.
- `r3` : assaut hostile réel de l'hôte converti, sans retraite.
- `r4` : remplacement par un assaut proche d'un raid avec repli vanilla et migration des sauvegardes `r3`.

## Publication

- [x] Documentation finale verrouillée.
- [x] Branche `feature/goauld-hostile-host-takeover` publiée.
- [x] Tag final unique `v0.3.40-dev` publié sans suffixe `-r4`.
- [x] Wiki séparé synchronisé et publié.

Le prochain jalon doit être choisi dans `docs/ROADMAP.md` et démarrer explicitement depuis `v0.3.40-dev`.
