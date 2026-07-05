# Current milestone validation

Jalon: `0.3.71-dev - Standardize player-facing duration formatting`

Branch: `feature/standardize-duration-formatting`

Révision locale cumulative : `r5`

Statut : `r1` et `r2` validées fonctionnellement. `r4` a rendu l’audit
exécutable et a révélé sept clés temporelles encore non migrées ainsi que des
faux positifs C# dus à un motif trop large. `r5` corrige ces deux catégories ;
audit et retest ciblé à exécuter.

## Build and consistency

Depuis la racine du dépôt :

```powershell
git diff --check
.\build.cmd
.\tools\check-duration-formatting.cmd
.\tools\check-project-consistency.cmd
```

Version de DLL attendue : `0.3.71.0`

Le contrôle de durée doit se terminer par :

```text
Duration-formatting audit passed.
```

Jusqu’à la mise à jour finale du changelog, le seul échec de cohérence accepté
reste :

```text
Newest changelog version is '0.3.70-dev'; expected '0.3.71-dev'.
```

Tout autre échec est anormal.

## Couverture fonctionnelle déjà validée

La validation `r2` couvre :

- les principaux sites mondiaux et leurs comptes à rebours ;
- les huit familles d’opérations Tok’ra ;
- les lettres, statuts et inspections associés ;
- les cooldowns et dialogues du communicateur sécurisé ;
- les étapes différées de la mission Tok’ra unique ;
- la menace interceptée et l’ancien marqueur de planque ;
- le français et l’anglais ;
- les durées multi-jours et inférieures à un jour ;
- sauvegarde/recharge, échéances inchangées et `Player.log` propre.

## Test final r5

1. Exécuter les quatre commandes ci-dessus.
2. Vérifier que l’audit charge `104` clés de migration explicites.
3. Vérifier qu’aucune clé traduite avec unité fixe non migrée n’est signalée.
4. Vérifier que les appels vanilla `ToStringTicksToPeriod()` et les calculs
   mécaniques par jour ne sont plus signalés comme convertisseurs manuels.
5. Forcer ou charger le site mondial du relais Goa’uld décodé.
6. Vérifier en français puis en anglais son inspection, la description de
   reconnaissance et la description de préparation du sabotage. Aucune unité ne
   doit être doublée après la durée vanilla.
7. Vérifier au moins une offre d’observation issue du framework, une offre
   thérapeutique, le cooldown d’extraction d’une reine et un message de cooldown
   diplomatique.
8. Charger une sauvegarde utilisée pour `r2` et confirmer l’absence d’erreur
   rouge, XML, traduction ou Harmony.

Aucune répétition complète des autres scénarios fonctionnels n’est requise pour
`r5`.


## Correctif r4

- correction de la compatibilité Windows PowerShell 5.1 du script `tools/check-duration-formatting.ps1` ;
- aucun changement C#, XML, traduction, durée réelle, sauvegarde ou gameplay par rapport au contenu de `r3` ;
- relancer l’audit de durée, le contrôle de cohérence et le retest ciblé du relais.


## Correctif r5

- ajoute les deux variantes d’offre d’observation du framework ;
- migre l’inspection d’offre thérapeutique ;
- migre le cooldown d’extraction de la reine Goa’uld ;
- migre le message de cooldown diplomatique Tok’ra ;
- couvre le fallback historique heures/jours du site de bataille Goa’uld ;
- reconnaît les formes `RimWorld day(s)` et `jour(s) RimWorld` ;
- retire le faux positif générique sur tout `ToString("0.#")` ;
- ignore explicitement les actions développeur et le calcul mécanique de
  dépendance au Prim’ta ;
- ne modifie aucun tick, délai, sauvegarde ou équilibrage.
