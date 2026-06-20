# Tests du jalon actif

Jalon : `0.3.11-dev - Add player-controlled Tok'ra personality switching`

Branche : `feature/tokra-personality-switching`

Statut : validation fonctionnelle terminée sur `0.3.11-dev-r2`; jalon prêt à publier.

## Préconditions

- Extraire l'archive à la racine du dépôt depuis `v0.3.10-dev`.
- Effectuer un rebuild complet.
- Vérifier `GateRimSG1.dll` en version `0.3.11.0`.
- Tester principalement en français.
- Préparer un colon avec un nom, des backstories et plusieurs niveaux de compétences faciles à noter.

## 1. Chargement et migration

1. Lancer RimWorld avec Biotech et GateRim SG-1.
2. Charger une sauvegarde `0.3.10-dev` contenant un Tok'ra contrôlé par le joueur.
3. Vérifier l'absence d'erreur XML, C# ou de chargement.
4. Sélectionner le Tok'ra.

Résultat attendu :

- le nom et les backstories de l'hôte restent actifs ;
- la description de santé affiche toujours les deux identités ;
- une ligne indique la personnalité active ;
- un seul gizmo permet d'activer le symbiote ;
- aucune compétence ne change avant l'utilisation du gizmo.

## 2. Régression du premier clic corrigée dans `r2`

1. Utiliser un Tok'ra dont le symbiote possède une carrière adulte mais aucune enfance dédiée.
2. Cliquer une première fois sur le gizmo d'activation du symbiote.
3. Ouvrir immédiatement la fiche du pawn et `Player.log`.

Résultat attendu :

- aucune `NullReferenceException` dans `Pawn_StoryTracker.set_Childhood` ;
- l'enfance de l'hôte reste affichée, puisqu'aucune enfance Tok'ra n'est actuellement configurée ;
- la carrière adulte devient celle du symbiote ;
- les bonus de compétences tiennent compte de l'enfance conservée et de la carrière Tok'ra active ;
- le nom devient celui du symbiote.

## 3. Basculement vers le symbiote

1. Noter le nom, l'enfance, l'âge adulte et les niveaux bruts de toutes les compétences modifiées par les deux profils.
2. Cliquer sur le gizmo d'activation du symbiote.
3. Ouvrir immédiatement la fiche du pawn.

Résultat attendu :

- le nom principal devient le nom du symbiote ;
- la carrière adulte devient celle du symbiote ; l'enfance reste celle de l'hôte tant qu'aucune enfance Tok'ra dédiée n'est configurée ;
- le titre affiché suit les backstories du symbiote ;
- seules les différences de niveaux prévues par les `skillGains` changent ;
- les passions, aptitudes génétiques, traits, faction, relations, équipement, inventaire et santé restent identiques ;
- la description de santé conserve les deux noms et indique le symbiote comme personnalité active ;
- le gizmo propose maintenant de réactiver l'hôte.

## 4. Retour vers l'hôte et anti-cumul

1. Réactiver l'hôte.
2. Comparer le nom, les backstories et les compétences avec les valeurs initiales.
3. Effectuer au moins dix allers-retours rapides.

Résultat attendu :

- le nom et les backstories exacts de l'hôte sont restaurés ;
- les niveaux retrouvent exactement leurs valeurs attendues ;
- aucun bonus ne s'empile ;
- aucune compétence ne dérive progressivement ;
- aucun nouveau trait, passion ou travail interdit n'apparaît en dehors des effets normaux des backstories actives.

## 5. Progression commune des compétences

1. Activer le symbiote.
2. Faire gagner assez d'expérience à une compétence pour que sa progression soit visible, idéalement jusqu'au niveau suivant.
3. Revenir à l'hôte.
4. Comparer la compétence.
5. Gagner ensuite de l'expérience avec l'hôte et revenir au symbiote.

Résultat attendu :

- l'expérience gagnée sous le symbiote reste présente sous l'hôte ;
- l'expérience gagnée sous l'hôte reste présente sous le symbiote ;
- seul l'écart de niveau provenant des backstories change ;
- aucun niveau ou XP n'est dupliqué ou perdu.

## 6. Sauvegarde avec chaque personnalité active

### Hôte actif

1. Sauvegarder avec l'hôte actif.
2. Quitter complètement RimWorld.
3. Recharger.

Résultat attendu : l'hôte, ses backstories et ses niveaux attendus restent actifs.

### Symbiote actif

1. Activer le symbiote.
2. Sauvegarder sous un autre nom.
3. Quitter complètement RimWorld.
4. Recharger.

Résultat attendu :

- le symbiote reste actif ;
- son nom et ses backstories restent affichés ;
- les niveaux restent cohérents ;
- le gizmo permet toujours de revenir à l'hôte sans cumul.

## 7. Extraction et réimplantation

1. Activer le symbiote.
2. Lancer une extraction autorisée.
3. Vérifier le pawn immédiatement après le retrait.
4. Réimplanter le même symbiote dans un autre colon.

Résultat attendu :

- l'ancien hôte retrouve automatiquement son nom, ses backstories et ses compétences d'hôte avant la fin de l'extraction ;
- le symbiote conserve son nom et son parcours ;
- le nouvel hôte conserve son propre nom et ses propres backstories ;
- le gizmo du nouvel hôte bascule entre sa propre identité et celle du même symbiote ;
- aucun état de compétence de l'ancien hôte n'est appliqué au nouveau.

## 8. Frontière joueur / IA

1. Générer ou rencontrer un Tok'ra visiteur, allié ou membre de la faction Tok'ra non contrôlé.
2. L'inspecter et sélectionner son Hediff.
3. Tester également un pawn de quête temporaire si disponible.

Résultat attendu :

- aucun gizmo de personnalité n'est visible ;
- le comportement classique est conservé ;
- aucune bascule automatique de nom, backstories ou compétences ne se produit.

## 9. État mental et contrôle direct

1. Sur un Tok'ra colon, déclencher temporairement un état mental via le mode développeur.
2. Vérifier les gizmos pendant l'état mental.
3. Mettre fin à l'état mental.

Résultat attendu : le gizmo disparaît lorsque le pawn n'est plus directement contrôlable et réapparaît ensuite sans changer la personnalité active.

## 10. Régression Goa'uld

1. Effectuer une implantation Goa'uld forcée.
2. Laisser la conversion en hôte actif se produire.
3. Tester une extraction d'urgence.

Résultat attendu :

- aucun gizmo Tok'ra n'apparaît ;
- le nom, les backstories, les compétences et le flux Goa'uld restent identiques à `0.3.10-dev`.

## 11. Contrôle final

Vérifier `Player.log`, notamment pour :

- `BackstorySkillOffsetUtility` ;
- `BackstorySkillProgressState` ;
- `Scribe_Collections` ;
- `NameTriple` ou `NameSingle` ;
- `CompGetGizmos` ;
- `TokraActivePersonality` ;
- erreurs de traduction ou anciennes DLL.

## Résultats

- [x] Premier clic sans exception, avec conservation de l'enfance de l'hôte.
- [x] Chargement et migration depuis `0.3.10-dev`.
- [x] Basculement correct vers le symbiote.
- [x] Retour exact vers l'hôte et dix cycles sans cumul.
- [x] XP et niveaux acquis partagés entre les deux personnalités.
- [x] Sauvegarde/rechargement avec l'hôte actif.
- [x] Sauvegarde/rechargement avec le symbiote actif.
- [x] Extraction depuis l'état symbiote et restauration automatique de l'hôte.
- [x] Réimplantation dans un nouvel hôte sans transfert de ses compétences.
- [x] Aucun gizmo pour les Tok'ra gérés par l'IA.
- [x] Gizmo correctement masqué pendant un état mental.
- [x] Aucune régression Goa'uld.
- [x] `Player.log` propre.


## Validation finale

Tous les tests ciblés ont été validés après le correctif `r2`. Le premier clic ne provoque plus d'exception, les basculements restent réversibles sans cumul, la progression est commune, les deux états de sauvegarde sont stables, l'extraction et la réimplantation conservent les identités attendues, la frontière joueur / IA est respectée et `Player.log` est propre.
