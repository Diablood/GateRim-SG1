# Validation terminée — 0.3.36-dev

Jalon : `0.3.36-dev - Add non-lethal capture tools`

Branche : `feature/non-lethal-capture-tools`

Tag de départ : `v0.3.35-dev`

Révision locale validée : `0.3.36-dev-r9`

Version de DLL validée : `0.3.36.0`

## Résultat final

La révision finale `r9` est fonctionnellement validée :

- les bolas sont opérationnelles et utilisent une entrave physique RP distincte ;
- le fusil hypodermique expérimental Tok'ra est opérationnel ;
- une réussite laisse la Conscience intacte et place temporairement le Mouvement à zéro ;
- aucune mort aléatoire liée à une réduction artificielle de Conscience n'a été observée ;
- la cible est mise à terre par le système de santé ordinaire ;
- l'ordre vanilla `Capturer` est disponible et mène correctement la cible vers un lit de prisonnier ;
- la cible se relève normalement après expiration si aucune autre affection ne la maintient à terre ;
- les charges du fusil sont visibles dans l'inspection au sol, dans l'infobulle équipée et dans l'inventaire ;
- une charge est consommée par tir, y compris après un manque ou une résistance ;
- les charges et la durée de l'effet persistent après sauvegarde/rechargement ;
- le fusil disparaît immédiatement après la dernière charge ;
- les bolas sont consommées après leur lancer ;
- les effets ne s'empilent pas en plusieurs Hediffs concurrents ;
- aucune nouvelle erreur bloquante liée aux outils de capture n'a été signalée.

## Suppressions finales

Les cinq fichiers du prototype abandonné restent supprimés :

```text
1.6/Defs/JobDefs/SG1_NonLethalCaptureJobs.xml
1.6/Patches/SG1_NonLethalCaptureFloatMenu.xml
Source/GateRimSG1/Weapons/Comp_NonLethalRestraintFloatMenu.cs
Source/GateRimSG1/Weapons/HediffComp_NonLethalStun.cs
Source/GateRimSG1/Weapons/JobDriver_RestrainNeutralizedPawn.cs
```

La révision `r9` n'ajoute aucune autre suppression. Les quatre textures provisoires sont conservées.

## Publication

- Commit final : `0.3.36-dev - add non-lethal capture tools`.
- Branche publiée : `feature/non-lethal-capture-tools`.
- Tag annoté final unique : `v0.3.36-dev`.
- Wiki séparé synchronisé, car plusieurs pages `docs/wiki/*.md` documentent le jalon.

## Limites connues

- Aucune mission de capture n'est encore incluse.
- La neutralisation reste résistible et les tirs peuvent manquer.
- Le fusil n'est ni fabricable, ni achetable, ni rechargeable en jeu normal.
- Les dégâts physiques très faibles restent réels sur une cible déjà gravement blessée.
- Les quatre textures restent provisoires jusqu'à la future passe visuelle globale.

## Étape suivante

Le jalon suivant prévu est `0.3.37-dev - Add Tok'ra Jaffa officer capture operation`, à créer depuis `v0.3.36-dev` sur une nouvelle branche dédiée après relecture des procédures du dépôt.
