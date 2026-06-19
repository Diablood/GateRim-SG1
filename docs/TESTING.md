# Testing workflow

## 0.3.1-dev - Refonte de l'opération de renseignements Tok'ra

### Préconditions

- Utiliser une sauvegarde créée avec `0.3.0-dev` ou une nouvelle partie.
- Disposer d'un communicateur sécurisé Tok'ra alimenté.
- Disposer d'un colon capable de travail Intellectuel.
- Activer le mode développeur RimWorld ou l'option avancée GateRim SG-1 pour les contrôles techniques.
- Conserver deux médicaments et les autres préconditions des opérations sans les utiliser : cette série de tests doit rester isolée sur l'archétype de renseignements.

### Test 1 — Module réservé à l'opération

1. Ouvrir `Architecte`, notamment `Mobilier`, puis rechercher le module de renseignements Tok'ra.
2. Vérifier qu'il n'apparaît dans aucune catégorie et ne peut pas être construit.
3. Forcer l'offre de renseignements depuis le menu debug unique du communicateur.
4. Accepter normalement l'offre.
5. Vérifier que le module est généré selon l'ordre zone de livraison, communicateur, puis bord de carte accessible.
6. Sélectionner le module et vérifier qu'il ne propose aucune action directe d'analyse.

Résultat attendu : le module est uniquement un objectif généré par l'opération ; toute analyse passe par le communicateur.

### Test 2 — Informations affichées sur le communicateur

1. Avant l'offre, consulter normalement l'état du canal.
2. Vérifier qu'aucun catalogue d'opérations, délai caché, pondération ou historique technique n'est révélé.
3. Forcer puis accepter l'offre de renseignements.
4. Consulter de nouveau le rapport normal à chaque phase : offre, module livré, méthode choisie, opération résolue.
5. En mode normal, vérifier que le panneau d'inspection ne liste ni les demandes verrouillées, ni les délais internes, ni le catalogue des opérations.
6. Vérifier qu'il ne montre que l'opération organique réellement en cours et les éventuelles étapes durables des missions uniques.
7. Activer ensuite le mode debug et confirmer que le rapport technique complet reste accessible.
8. Ouvrir `Afficher l'état du framework` depuis le menu debug.
9. Vérifier que le rapport technique affiche la méthode, le travail restant, l'état de l'interférence, la patrouille programmée, le porteur du module et la variante de résultat.

Résultat attendu : séparation nette entre le rapport RP normal et le diagnostic complet réservé au debug.

### Test 3 — Analyse prudente

1. Forcer et accepter l'offre, puis sélectionner un colon capable d'Intellectuel.
2. Faire un clic droit sur le communicateur et choisir l'analyse du module.
3. Dans la fenêtre, choisir `Analyse prudente`.
4. Vérifier que le colon rejoint d'abord le module, le prend en charge puis le transporte physiquement jusqu'au communicateur.
5. Vérifier que l'analyse ne commence qu'une fois le module arrivé au communicateur.
6. Interrompre le colon avant le ramassage, pendant le transport puis pendant l'analyse, lui donner une autre tâche, puis reprendre depuis le communicateur.
7. Sauvegarder pendant le transport et pendant le travail, recharger puis reprendre.
8. Laisser l'analyse se terminer.
9. Vérifier la disparition du module, une seule réussite, l'amélioration qualitative de la confiance et `350 XP` en Intellectuel.
10. Vérifier qu'aucune patrouille Goa'uld n'est programmée par cette méthode.

### Test 4 — Décodage accéléré sans interférence

1. Recréer l'offre et choisir `Décodage accéléré`.
2. Vérifier que le travail est nettement plus court que l'analyse prudente.
3. Interrompre puis reprendre une fois pour confirmer la persistance de la progression.
4. Terminer une occurrence sans interférence.
5. Vérifier `500 XP` en Intellectuel, la réussite unique et l'absence de patrouille programmée.

### Test 5 — Interférence et patrouille Goa'uld

1. Forcer une nouvelle offre, l'accepter et choisir le décodage accéléré.
2. Utiliser `Renseignements : forcer la patrouille` dans le menu debug avant la résolution, ou répéter naturellement jusqu'à produire l'interférence.
3. Terminer l'analyse.
4. Vérifier que la lettre de réussite avertit en RP qu'une émission parasite a pu attirer une patrouille.
5. Vérifier que l'opération est immédiatement réussie, que le module disparaît et que la confiance positive n'est pas annulée.
6. Consulter le rapport debug et confirmer qu'une patrouille différée a été programmée.
7. Attendre environ deux à cinq heures de jeu.
8. Vérifier l'arrivée d'une petite force Goa'uld/Jaffa dimensionnée sous la menace normale de la colonie.
9. Sauvegarder après la réussite mais avant l'arrivée, recharger et vérifier que l'incident programmé arrive toujours.

Résultat attendu : la patrouille est une conséquence réelle du choix risqué, distincte du résultat positif de l'opération.

### Test 6 — Échecs et méthode verrouillée

1. Choisir une méthode, interrompre le travail puis tenter de sélectionner l'autre méthode.
2. Vérifier que la méthode reste verrouillée pour cette occurrence.
3. Détruire le module avant la fin et confirmer un seul échec.
4. Refaire l'opération et laisser expirer la fenêtre après acceptation.
5. Vérifier un seul échec, aucun XP, aucun module résiduel et aucune patrouille créée par une analyse inachevée.

### Test 7 — Variantes RP répétées

1. Résoudre plusieurs occurrences prudentes, accélérées sans interférence et accélérées avec interférence.
2. Noter les lettres de réussite.
3. Vérifier que chaque famille dispose de plusieurs formulations cohérentes avec le contexte.
4. Vérifier que deux résultats successifs évitent la même variante lorsque plusieurs variantes sont disponibles.
5. Vérifier qu'aucun texte joueur ne mentionne un jet, un pourcentage, un compteur interne ou une règle de framework.

### Test 8 — Debug regroupé et compatibilité `0.3.0-dev`

1. Vérifier qu'un seul gizmo `Debug des opérations Tok'ra` regroupe les actions sur le communicateur.
2. Vérifier les entrées de méthode prudente, méthode accélérée et interférence forcée.
3. Désactiver le mode développeur et l'option avancée, puis vérifier la disparition de tous ces contrôles.
4. Charger si possible une sauvegarde `0.3.0-dev` avec une offre ou un module de renseignements actif.
5. Vérifier que l'opération peut reprendre depuis le communicateur.
6. Pour une sauvegarde prise pendant l'ancienne tâche directe, vérifier que cette tâche s'arrête proprement avec un message invitant à utiliser le communicateur, sans erreur de Def ou de JobDriver.

### Contrôle final `0.3.1-dev`

- Rejouer une analyse prudente et un décodage accéléré sans debug.
- Vérifier qu'aucune table de recherche vanilla ne propose l'analyse.
- Vérifier qu'aucune action directe ne reste sur le module.
- Vérifier le retour du canal à son état RP générique après résolution.
- Vérifier `Player.log` : aucune Def manquante, erreur de Scribe, référence nulle, tâche invalide ou résolution double.


## 0.3.0-dev - Framework interne des opérations Tok'ra organiques

### Préconditions générales

- Utiliser une nouvelle partie créée avec `0.3.0-dev`.
- Ne pas charger de sauvegarde `0.2.x-dev` pour cette validation.
- Construire la DLL puis vérifier l'absence d'erreur rouge au démarrage.
- Disposer d'un communicateur Tok'ra alimenté et d'au moins un colon valide.
- Conserver `Player.log` pour le contrôle final.

### Test 1 — Visibilité normale et debug

1. Désactiver le mode développeur RimWorld.
2. Désactiver `Afficher les informations de debug avancées` dans les options GateRim SG-1.
3. Sélectionner le communicateur.
4. Vérifier qu'aucun menu ou gizmo technique des opérations organiques n'est visible.
5. Activer l'option avancée GateRim SG-1 sans activer le mode développeur.
6. Sélectionner de nouveau le communicateur.
7. Vérifier la présence d'un seul gizmo `Debug des opérations Tok'ra`.
8. Ouvrir ce menu et vérifier les actions d'état, acceptation, avancement,
   réussite, échec, expiration, conséquence secondaire et réinitialisation.
9. Désactiver l'option avancée, activer le mode développeur et confirmer que
   les actions compactes `Tok'ra ops: ...` sont disponibles dans les outils
   développeur.

Résultat attendu : le système de test est accessible par les deux voies prévues,
mais totalement absent du jeu normal.

### Test 2 — Offre et persistance communes

Pour chacun des quatre archétypes :

1. Forcer l'offre correspondante.
2. Ouvrir `Afficher l'état du framework` et noter l'archétype, l'état, la carte
   et le tick d'expiration.
3. Sauvegarder puis recharger.
4. Vérifier que la même offre reste active et qu'aucune seconde offre
   organique n'apparaît.
5. Utiliser `Accepter l'offre actuelle` ou l'interaction normale du
   communicateur.
6. Sauvegarder puis recharger avant la résolution.
7. Vérifier que l'instance active, ses références et ses délais restent
   cohérents.

Résultat attendu : une seule instance persistante porte l'opération active,
sans ancien champ de migration ni double création.

### Test 3 — Observation Goa'uld

1. Forcer puis accepter l'offre d'observation.
2. Utiliser `Avancer la phase actuelle`.
3. Vérifier que le rapport est immédiatement prêt à transmettre.
4. Sauvegarder et recharger dans cet état.
5. Transmettre normalement via le communicateur.
6. Vérifier une seule réussite, un seul gain de confiance qualitatif et un seul
   gain d'expérience.
7. Répéter avec `Résoudre en échec`.

### Test 4 — Module de renseignement

1. Forcer puis accepter l'offre de récupération.
2. Vérifier le placement selon l'ordre zone de livraison, communicateur,
   fallback accessible.
3. Sauvegarder et recharger avec le module présent.
4. Sécuriser normalement le module.
5. Vérifier une seule réussite et la disparition de l'objectif.
6. Refaire le test en détruisant le module ou avec `Faire expirer l'état
   actuel`.
7. Vérifier un seul échec et aucun objectif résiduel.

### Test 5 — Agent Tok'ra blessé

1. Forcer puis accepter l'offre.
2. Vérifier l'arrivée du patient, le choc de symbiote et le flux médical
   vanilla.
3. Sauvegarder et recharger pendant le transport vers un lit, après un soin et
   pendant le départ.
4. Vérifier que la réussite n'est appliquée qu'après la sortie réelle.
5. Refaire le test avec la mort du patient avant sa sortie.
6. Vérifier que la mort produit un seul échec.
7. Vérifier aussi l'action debug d'avancement, qui doit lever le blocage de
   soin et conduire à la phase suivante sans dupliquer la résolution.

### Test 6 — Remise de fournitures médicales

1. Forcer puis accepter l'offre.
2. Utiliser l'action d'avancement avant l'arrivée et vérifier que l'agent de
   liaison est généré et progresse vers le point de rencontre.
3. Sauvegarder et recharger avant puis après son arrivée.
4. Effectuer la remise normale de deux médicaments.
5. Vérifier la réussite immédiate et le départ du visiteur.
6. Sauvegarder et recharger pendant son départ.
7. Tuer le visiteur avant sa sortie.
8. Vérifier que la réussite n'est pas annulée et que la conséquence
   relationnelle secondaire n'est appliquée qu'une fois.
9. Refaire sans remise et faire expirer l'opération pour vérifier l'échec
   unique.

### Test 7 — Résolution et nettoyage partagés

1. Sur chaque archétype accepté, utiliser successivement les actions debug de
   réussite, échec ou expiration dans des sessions séparées.
2. Après chaque résolution, rouvrir l'état du framework.
3. Vérifier l'absence d'opération active et la programmation d'une future
   opportunité.
4. Réutiliser immédiatement l'action de résolution précédente.
5. Vérifier qu'aucun gain, perte, lettre ou compteur n'est appliqué une seconde
   fois.
6. Utiliser `Réinitialiser le framework` et confirmer le nettoyage de tout
   objectif ou visiteur encore rattaché à l'instance active.

### Test 8 — Contrôle de la rupture de sauvegarde

1. Vérifier qu'aucun des fichiers de compatibilité du conteneur médical n'est
   encore présent dans le dépôt.
2. Vérifier que le code ne contient plus
   `GameComponent_TokraOrganicOperationTracker`.
3. Vérifier que les nouvelles sauvegardes contiennent
   `tokraOrganicActiveOperation` et `tokraOrganicOperationFollowUp`.
4. Ne pas demander de prise en charge d'une sauvegarde `0.2.x-dev` : cette
   rupture est volontaire et documentée.

### Contrôle final

- Rejouer une occurrence normale de chaque archétype sans outil debug.
- Vérifier le retour immédiat à l'état RP générique du canal après résolution.
- Vérifier l'anti-répétition locale et le délai caché entre opportunités.
- Vérifier `Player.log` : aucune erreur de chargement de type, de Scribe, de
  référence nulle, de Def manquante ou de résolution double.


## Vérification du wiki joueur français

À exécuter après toute passe globale de traduction ou de réorganisation du
wiki :

1. Ouvrir `docs/wiki/Home.md`, `Content-Status.md`,
   `Tokra-Interaction-Roadmap.md`, `_Sidebar.md` et `_Footer.md`.
2. Vérifier que les versions, états et directions de développement correspondent
   à `docs/PROJECT_STATE.md`.
3. Vérifier que `Liens utiles` ne contient que des liens ou références et que
   les éléments de roadmap se trouvent dans une section de développement.
4. Rechercher les formulations anglaises restantes dans `docs/wiki/*.md`.
   Conserver uniquement les noms propres, identifiants techniques, commandes
   RimWorld et termes volontairement non traduits.
5. Cliquer chaque lien interne des pages modifiées et confirmer que la page
   cible existe.
6. Synchroniser `docs/wiki/*.md` vers le dépôt wiki séparé et vérifier le rendu
   de l'accueil, de la barre latérale, des tableaux et des listes.
7. Vérifier qu'aucune page française n'annonce une fonctionnalité prévue comme
   déjà jouable, ou inversement.


Durable tests follow the structure and ordering rules in `docs/TESTING_GUIDELINES.md`.

## Minimal isolated test

Use this active mod list first:

```text
Core
Biotech
GateRim SG-1
```

This isolates GateRim definitions from unrelated third-party gene categories and patches.

## Jaffa foundation checklist

1. Start RimWorld with the minimal isolated mod list.
2. Open the xenotype editor.
3. Confirm that the editor opens without exceptions.
4. Load the premade `Jaffa` xenotype.
5. Confirm that `Jaffa physiology` displays a texture.
6. Confirm that `Jaffa longevity` displays a `150%` lifespan factor.
7. Switch to French and verify the translated labels and descriptions.
8. Close the game and inspect `Player.log`.

## Interpreting the first external test log

The first external log contained two categories of issues:

### GateRim issues corrected in 0.1.5-dev
- Leading and trailing whitespace in the Jaffa xenotype description.
- French translation values formatted across multiple lines.
- Missing `UI/Icons/Genes/Gene_Robust` texture.

### Third-party compatibility issue to isolate separately
- `KeyNotFoundException` for a gene category named `Ability`.

The `Ability` category is not declared by the current GateRim definitions. Re-run the minimal isolated test before investigating loaded third-party mods.


## Free Goa'uld symbiote prototype

Use developer mode to spawn:

```text
SG1_GoauldSymbiote
```

Checklist:

1. Confirm the pawn appears with its temporary sprite.
2. Confirm movement and a weak bite attack.
3. Confirm that no natural biome spawn occurs.
4. Switch to French and verify the translated label and description.
5. Check `Player.log` for `SG1_GoauldSymbiote` errors.


## 0.1.9-dev XML regression check

After applying the free-symbiote XML correction:

1. Launch with `Core`, `Biotech`, and `GateRim SG-1`.
2. Confirm that `Player.log` no longer reports:
   ```text
   XML error: <wildness>1</wildness> doesn't correspond to any field in type RaceProperties.
   ```
3. Spawn `SG1_GoauldSymbiote` through developer mode.
4. Confirm that movement, the weak bite, and the temporary sprite still work.


## Recent Goa'uld implantation prototype

Add the following Hediff through developer mode to a humanoid pawn:

```text
SG1_GoauldRecentImplantation
```

Checklist:

1. Confirm the health tab displays `recent Goa'uld implantation`.
2. Confirm the remaining-time countdown appears.
3. Confirm the pawn receives additional pain.
4. Wait one in-game day and confirm the Hediff disappears.
5. Switch to French and verify the translated label, description and stage.
6. Check `Player.log` for `SG1_GoauldRecentImplantation` errors.


## Jaffa Prim'ta split

Use newly generated pawns after applying `0.1.13-dev`.

1. Generate a Jaffa pawn.
2. Confirm the germline gene list contains:
   ```text
   SG1_JaffaLineage
   SG1_JaffaPouchPotential
   SG1_JaffaSymbioteCompatibility
   SG1_JaffaPhysiology
   ```
3. Confirm `SG1_JaffaPrimta` is absent at birth or initial generation.
4. Add `SG1_JaffaPrimta` through developer mode.
5. Confirm immunity, healing, pain, damage and lifespan modifiers.
6. Remove the Hediff and confirm the modifiers disappear.
7. Check `Player.log` for `SG1_JaffaPrimta` errors.


## C# logging scaffold smoke test

1. Build the mod assembly with `build.ps1` or `build.sh`.
2. Confirm that `1.6/Assemblies/GateRimSG1.dll` exists locally.
3. Launch RimWorld with `Core`, `Biotech`, and `GateRim SG-1`.
4. Close the game after the main menu appears.
5. Inspect `Player.log`.
6. Confirm the presence of:
   ```text
   <color=#D9B44A>[GateRim SG-1]</color> Version 0.1.42.0 loaded.
   ```


## Colored logging prefix regression check

1. Build with:
   ```powershell
   .\build.cmd "D:\SteamLibrary\steamapps\common\RimWorld\RimWorldWin64_Data\Managed"
   ```
2. Launch RimWorld with `Core`, `Biotech`, and `GateRim SG-1`.
3. Inspect `Player.log`.
4. Confirm the bootstrap line contains:
   ```text
   <color=#D9B44A>[GateRim SG-1]</color>
   ```


## Persistent Goa'uld symbiote identity

1. Build the assembly with `build.cmd`.
2. Launch RimWorld with `Core`, `Biotech`, and `GateRim SG-1`.
3. Select a humanoid pawn.
4. Add the health state:
   ```text
   adult Goa'uld symbiote
   ```
5. Open the health-state description and copy the displayed symbiote ID.
6. Save the game.
7. Reload the save.
8. Confirm that the displayed symbiote ID is unchanged.
9. Inspect `Player.log`.
10. Confirm that `Attached` and `Loaded` messages use the same symbiote ID.
11. Remove the Hediff and confirm a `Detached` message appears.

Optional regression check:

1. Add `recent Goa'uld implantation`.
2. Confirm that it also receives a persistent symbiote ID.
3. Confirm that the temporary state still disappears after one in-game day.


## Forced Goa'uld implantation prototype

1. Build with `build.cmd`.
2. Spawn a free `Goa'uld symbiote`.
3. Place one adult humanoid pawn in an adjacent cell.
4. Select the symbiote and record its free-symbiote ID.
5. Click `Forced implantation`.
6. Confirm that the free symbiote disappears.
7. Confirm that the target receives `recent Goa'uld implantation`.
8. Confirm that the ID displayed on the Hediff matches the former free-symbiote ID.
9. Save and reload.
10. Confirm that the transferred ID remains unchanged.
11. Inspect `Player.log` for the transfer lifecycle.

Negative checks:

- no adjacent compatible humanoid;
- child under 13;
- animal target;
- pawn already implanted;
- pawn already carrying an adult Goa'uld symbiote state.


## Active Goa'uld host conversion

1. Build with `build.cmd`.
2. Implant an adjacent adult humanoid through the manual forced-implantation command.
3. Record the persistent symbiote ID.
4. Save and reload during `recent Goa'uld implantation`.
5. Let the one-day countdown expire.
6. Confirm that the recent state disappears.
7. Confirm that `adult Goa'uld symbiote` appears.
8. Confirm that the ID is unchanged.
9. Confirm active-host modifiers and preservation of the original germline xenotype.
10. Save and reload after conversion.
11. Confirm that the ID remains unchanged.
12. Inspect `Player.log` for preparation, conversion and safe-removal logs.


## Emergency Goa'uld extraction prototype

1. Build with `build.cmd`.
2. Implant an adjacent adult humanoid through `Forced implantation`.
3. Record the persistent symbiote ID.
4. Select the implanted host before the critical countdown expires.
5. Click `Emergency extraction`.
6. Confirm that recent implantation disappears.
7. Confirm that a free Goa'uld symbiote pawn appears nearby.
8. Confirm that the free pawn displays the same ID.
9. Re-implant the extracted pawn and confirm the ID remains unchanged.
10. Save and reload after extraction.
11. Confirm that the free pawn ID remains unchanged.
12. Inspect `Player.log` for reverse-transfer lifecycle logs.


## Emergency Goa'uld extraction surgery

1. Build with `build.cmd`.
2. Implant an adult humanoid through `Forced implantation`.
3. Open the health tab and schedule:
   ```text
   emergency Goa'uld extraction
   ```
4. Provide a doctor with Medicine `6+`, a bed and medicine.
5. Let the bill complete.
6. On success, confirm that recent implantation disappears.
7. Confirm that a nearby free symbiote displays the same ID.
8. Re-implant the extracted pawn and confirm the ID remains unchanged.
9. Save and reload after extraction.
10. Confirm persistence.
11. Test a lower-quality medical setup.
12. Confirm that a failed surgery leaves recent implantation in place.
13. Inspect `Player.log`.


## Autonomous free-symbiote hunt

1. Build with `build.cmd`.
2. Spawn a free Goa'uld symbiote several cells away from an adult humanoid.
3. Confirm `Autonomous hunt` is enabled.
4. Wait for the pursuit job to start.
5. Confirm movement toward the target.
6. Confirm automatic implantation on contact.
7. Confirm the persistent ID transfer.
8. Extract the parasite through surgery.
9. Confirm the free pawn returns with a non-zero cooldown.
10. Confirm it does not immediately re-implant the patient.
11. Wait for cooldown expiry and confirm pursuit resumes.
12. Toggle autonomous hunt off and on.
13. Confirm the toggle interrupts and restores the autonomous behavior.
14. Inspect `Player.log`.


## Ritual Goa'uld implantation prototype

1. Build with `build.cmd`.
2. Spawn a free Goa'uld symbiote.
3. Disable autonomous hunt.
4. Place one compatible humanoid within 12 cells but not adjacent.
5. Select the symbiote and record its persistent ID.
6. Click `Ritual implantation`.
7. Confirm that the nearest valid humanoid receives recent implantation.
8. Confirm that the free pawn disappears.
9. Confirm identity persistence.
10. Save and reload.
11. Confirm the same ID remains visible.
12. Repeat without a valid target in range and confirm rejection.


## Explicit ritual map target selection

1. Build with `build.cmd`.
2. Spawn a free Goa'uld symbiote.
3. Place two compatible humanoids within `12` cells.
4. Disable autonomous hunt for a controlled test.
5. Select the symbiote and click `Ritual implantation`.
6. Confirm that a map-targeting cursor appears.
7. Click the farther valid humanoid.
8. Confirm that the clicked pawn, not the nearest pawn, receives recent implantation.
9. Confirm persistent identity transfer.
10. Repeat with an invalid pawn, an out-of-range pawn and an unreachable pawn.
11. Confirm rejection without consuming the free symbiote.
12. Save and reload after a valid ritual.
13. Confirm identity persistence.


## Timed ritual ceremony and cancellation

1. Build with `build.cmd`.
2. Spawn a free Goa'uld symbiote and disable autonomous hunt.
3. Start `Ritual implantation`.
4. Select one compatible reachable humanoid within `12` cells.
5. Confirm that implantation is not immediate.
6. Confirm the inspection panel displays the target and remaining ticks.
7. Save and reload during the ceremony.
8. Confirm the countdown resumes.
9. Let the countdown reach zero.
10. Confirm recent implantation and persistent identity transfer.
11. Start another ritual, click `Cancel ritual`, and confirm no transfer occurs.
12. Start another ritual and move the target out of range.
13. Confirm automatic cancellation without consuming the free symbiote.


## Goa'uld ritual basin requirement

1. Build with `build.cmd`.
2. Build or spawn `Goa'uld ritual basin`.
3. Spawn a free symbiote and disable autonomous hunt.
4. Keep the symbiote and one compatible target within `6` cells of the basin.
5. Start ritual implantation and select the target.
6. Confirm the inspection panel displays the ritual basin.
7. Save and reload during the ceremony.
8. Confirm the basin reference and countdown persist.
9. Complete the ritual and confirm identity transfer.
10. Start another ritual, destroy the basin, and confirm automatic cancellation.
11. Start another ritual and move the target beyond `6` cells from the basin.
12. Confirm cancellation without consuming the free symbiote.
13. Try starting without a nearby basin and confirm rejection.


## Jaffa Prim'ta implantation procedure

1. Build with `build.cmd`.
2. Spawn a newly generated Jaffa.
3. Open the health-tab operation menu.
4. Confirm `implant Jaffa Prim'ta` is available.
5. Schedule the procedure.
6. Provide one medicine and a doctor with Medicine `4+`.
7. Let the operation complete.
8. Confirm `Prim'ta symbiote` appears.
9. Confirm the expected biological modifiers.
10. Save and reload.
11. Confirm persistence and the `Loaded Jaffa Prim'ta symbiote` log.
12. Confirm the implantation operation is hidden while Prim'ta is present.
13. Select a baseliner and confirm the operation is unavailable.
14. Remove the Hediff in developer mode and confirm the removal log.


## Physical Prim'ta larva resource

1. Build with `build.cmd`.
2. Spawn one `Prim'ta larva` through developer tools.
3. Confirm the physical item can be hauled and stored.
4. Spawn a compatible Jaffa.
5. Schedule `implant Jaffa Prim'ta`.
6. Confirm the operation requires one medicine and one larva.
7. Let the surgery complete.
8. Confirm the larva is consumed.
9. Confirm `Prim'ta symbiote` appears.
10. Save and reload.
11. Confirm persistence.
12. Try the same workflow without an available larva.
13. Confirm the bill waits for the missing ingredient.


## Prim'ta larva acquisition prototype

1. Build with `build.cmd`.
2. Construct or spawn `Prim'ta incubation basin`.
3. Open its Bills tab.
4. Add `incubate Prim'ta larva`.
5. Let a colonist complete the Intellectual work.
6. Confirm one physical `Prim'ta larva` appears.
7. Confirm hauling, storage and stacking still work.
8. Use the produced larva in `implant Jaffa Prim'ta`.
9. Confirm the surgery consumes it and adds `Prim'ta symbiote`.
10. Save and reload after production and after implantation.


## Prim'ta incubation work-giver fix

1. Restart RimWorld so XML Defs are reloaded.
2. Construct or spawn `Prim'ta incubation basin`.
3. Add `incubate Prim'ta larva`.
4. Confirm a pawn with Handling enabled and Animals `4+` can prioritize the basin manually.
5. Confirm the same pawn starts the bill automatically when Handling work is enabled.
6. Confirm a pawn below Animals `4` is rejected with a minimum-skill message.
7. Let the work complete and confirm one physical larva appears.


## Prim'ta incubation nutrient requirements

1. Build with `build.cmd`.
2. Construct or spawn `Prim'ta incubation basin`.
3. Add `incubate Prim'ta larva`.
4. Confirm a pawn with Handling enabled and Animals `4+` is eligible.
5. Leave the map without raw meat and confirm the bill waits.
6. Add fewer than `10` raw-meat units and confirm the bill still waits.
7. Add at least `10` raw-meat units.
8. Confirm the pawn hauls the meat and completes the bill.
9. Confirm `10` units of raw meat are consumed.
10. Confirm one physical `Prim'ta larva` appears.
11. Complete the existing Jaffa implantation workflow.


## Prim'ta larva preservation prototype

1. Build with `build.cmd`.
2. Produce or spawn one `Prim'ta larva`.
3. Select the item and confirm rotting/spoilage information appears.
4. Store one larva at room temperature.
5. Confirm rot progresses.
6. Store one larva in a cold room or freezer.
7. Confirm it is preserved better than the room-temperature larva.
8. Let a warm larva fully rot.
9. Confirm it is destroyed.
10. Implant a fresh larva into a compatible Jaffa and confirm the medical loop still works.


## Prim'ta larva biological storage category

1. Build with `build.cmd`.
2. Start RimWorld and inspect the log for XML errors.
3. Produce or spawn one `Prim'ta larva`.
4. Open a stockpile storage filter.
5. Confirm the larva appears under:
   ```text
   raw resources
       ↓
   Goa'uld biological products
   ```
6. Confirm it no longer appears under `manufactured`.
7. Confirm it is not presented as raw food or an animal food product.
8. Confirm hauling, stacking, rotting and Jaffa implantation still work.


## Prim'ta larva temperature tuning

1. Build with `build.cmd`.
2. Spawn or incubate several `Prim'ta larva` items.
3. Select one larva and confirm the thermal inspection lines appear.
4. Store larvae below `0 °C`, around `5 °C`, around `20 °C`, above `25 °C`
   and above `40 °C`.
5. Confirm the displayed effective rates are respectively approximately:
   ```text
   ×0
   ×0.5
   ×1
   ×2
   ×3
   ```
6. Confirm hot larvae deteriorate faster than room-temperature larvae.
7. Confirm frozen larvae stop deteriorating for this prototype.
8. Confirm storage category, hauling, stacking, incubation and implantation
   regressions remain valid.


## Jaffa Prim'ta implantation age eligibility

1. Build with `build.cmd`.
2. Spawn a compatible Jaffa below `10` biological years.
3. Confirm `implant Jaffa Prim'ta` is absent from the operations list.
4. Spawn a compatible Jaffa aged exactly `10` biological years.
5. Confirm the operation appears.
6. Complete the normal surgery with one medicine and one larva.
7. Confirm `Prim'ta symbiote` is attached.
8. Confirm the duplicate-operation guard still works.
9. Save and reload.
10. Confirm persistence.


## Jaffa puberty dependency prototype

1. Build with `build.cmd`.
2. Spawn a compatible Jaffa aged `11` without Prim'ta.
3. Wait at least one in-game hour and confirm no dependency appears.
4. Spawn a compatible Jaffa aged `12` without Prim'ta.
5. Wait up to one in-game hour.
6. Confirm `Prim'ta deficiency` appears.
7. Accelerate time and confirm progressive severity stages.
8. Confirm immunity and healing modifiers worsen.
9. Save and reload during progression.
10. Confirm severity persists.
11. Implant a physical larva with the existing medical procedure.
12. Confirm the dependency disappears immediately.


## Jaffa Prim'ta cultural thoughts

1. Build with `build.cmd`.
2. Spawn a compatible Jaffa aged `9` without Prim'ta.
3. Confirm `awaiting Prim'ta` is absent.
4. Spawn a compatible Jaffa aged `10` without Prim'ta.
5. Confirm `awaiting Prim'ta` appears with mood `-1`.
6. Implant a physical larva with the existing surgery.
7. Confirm `awaiting Prim'ta` disappears.
8. Confirm `received Prim'ta` appears with mood `+3`.
9. Remove the Prim'ta in developer mode.
10. Confirm `awaiting Prim'ta` returns.
11. Save and reload.
12. Reimplant a larva.
13. Confirm the `received Prim'ta` memory is not granted again.
14. Confirm the puberty dependency remains separate and still works from age `12`.


## Jaffa tretonin substitution prototype

1. Build with `build.cmd`.
2. Spawn a compatible Jaffa aged `12+` without Prim'ta.
3. Wait for `Prim'ta deficiency`.
4. Spawn one `tretonin dose`.
5. Schedule `administer tretonin` from the health tab.
6. Confirm the dose is consumed.
7. Confirm the dependency disappears immediately.
8. Confirm `tretonin substitution` appears with remaining time.
9. Save and reload.
10. Confirm remaining duration persistence.
11. Let one day expire.
12. Confirm substitution disappears.
13. Wait for the next hourly dependency scan.
14. Confirm dependency returns.
15. Confirm Jaffa with implanted Prim'ta cannot receive tretonin.
16. Confirm waiting-thought behavior remains separate.


## Tretonin acquisition prototype

1. Build with `build.cmd`.
2. Build or spawn the vanilla `DrugLab`.
3. Confirm `prepare tretonin doses` appears in its Bills tab.
4. Confirm the minimum Intellectual skill is `6`.
5. Test with no larva and confirm the bill waits.
6. Test with no medicine and confirm the bill waits.
7. Supply one `Prim'ta larva` and one medicine unit.
8. Complete the bill.
9. Confirm both inputs are consumed.
10. Confirm exactly five `tretonin dose` items appear.
11. Confirm storage and stacking.
12. Administer one dose to an eligible Jaffa.
13. Confirm the existing one-day substitution workflow remains valid.


## Formal Jaffa Prim'ta ceremony prototype

1. Build with `build.cmd`.
2. Construct or spawn one `Goa'uld ritual basin`.
3. Place one physical `Prim'ta larva` within `6` cells.
4. Place one compatible Jaffa aged `10+` within `6` cells.
5. Select the basin and start `Formal Prim'ta ceremony`.
6. Target the Jaffa.
7. Confirm the inspection panel shows target, reserved larva and progress.
8. Save and reload during the rite.
9. Confirm progress persists.
10. Let the rite complete.
11. Confirm one larva is consumed and `Prim'ta symbiote` appears.
12. Confirm dependency relief and cultural-memory behavior.
13. Test manual cancellation.
14. Test automatic cancellation after moving the larva away.
15. Confirm cancelled ceremonies do not consume the larva.
16. Confirm the medical implantation operation still works independently.


## Formal Jaffa Prim'ta ceremony ticker regression

1. Restart RimWorld after applying the XML fix.
2. Construct or reuse one `Goa'uld ritual basin`.
3. Place one eligible Jaffa and one larva within `6` cells.
4. Start `Formal Prim'ta ceremony`.
5. Select the basin.
6. Confirm the remaining duration decreases from `600 / 600`.
7. Let the ceremony complete and confirm one larva is consumed.
8. Confirm the Prim'ta Hediff is attached.


## Tok'ra foundation prototype

1. Build with `build.cmd`.
2. Spawn `Tok'ra symbiote` through developer tools.
3. Confirm the inspection panel displays origin `Tok'ra` and autonomous hunt `disabled`.
4. Confirm only `Voluntary Tok'ra implantation` is available.
5. Confirm forced implantation, Goa'uld ritual implantation and autonomous hunt are absent.
6. Place a player-controlled compatible adult within `12` cells.
7. Start voluntary implantation and target the colonist.
8. Confirm recent implantation uses the same persistent ID.
9. Save and reload.
10. Wait one day and confirm active Tok'ra symbiosis.
11. Repeat and extract during recent implantation.
12. Confirm the free pawn returns as `Tok'ra symbiote`.
13. Confirm origin remains `Tok'ra` and hunt remains disabled.
14. Spawn a normal `Goa'uld symbiote`.
15. Confirm previous Goa'uld forced, ritual and autonomous workflows remain available.


## Tok'ra FactionDef loading regression

1. Apply the `0.1.39-dev-r1` XML patch.
2. Restart RimWorld completely.
3. Open `Player.log`.
4. Confirm the following errors no longer appear:
   ```text
   hairTags doesn't correspond to any field in type FactionDef
   startingGoodwill doesn't correspond to any field in type FactionDef
   naturalColonyGoodwill doesn't correspond to any field in type FactionDef
   raidLootValueFromPointsCurve must be defined
   ```
5. Confirm:
   ```text
   [GateRim SG-1] Version 0.1.39.0 loaded.
   ```
6. Continue the Tok'ra voluntary-implantation regression tests.


## Tok'ra voluntary-host pawn prototype

1. Build with `build.cmd`.
2. Spawn `Tok'ra voluntary host` through developer tools.
3. Confirm the pawn is player-controlled.
4. Wait up to `60` ticks.
5. Confirm `adult Goa'uld-family symbiote` appears in the Health tab.
6. Confirm the persistent origin is `Tok'ra`.
7. Save and reload.
8. Confirm the same symbiote identity persists.
9. Spawn a second prototype host.
10. Confirm the second pawn receives a distinct identity.
11. Confirm free Tok'ra voluntary implantation still works.
12. Confirm normal Goa'uld workflows remain available.


## Tok'ra voluntary-host resistance-range regression

1. Apply the `0.1.40-dev-r1` XML patch.
2. Restart RimWorld completely.
3. Open `Player.log`.
4. Confirm the following error no longer appears:
   ```text
   Config error in SG1_TokraVoluntaryHost: initial resistance range is undefined for humanlike pawn kind.
   ```
5. Confirm:
   ```text
   [GateRim SG-1] Version 0.1.40.0 loaded.
   ```
6. Repeat the developer-spawn and save/reload regression tests.


## Tok'ra pawn-group foundation

1. Restart RimWorld completely.
2. Open `Player.log`.
3. Confirm no XML or Def-validation errors reference:
   ```text
   SG1_TokraSmallTeam
   SG1_TokraVisitorPrototype
   ```
4. Confirm:
   ```text
   [GateRim SG-1] Version 0.1.41.0 loaded.
   ```
5. Confirm the Tok'ra faction remains hidden and non-generated.
6. Spawn `Tok'ra voluntary host` and confirm one-time Tok'ra initialization.
7. Spawn `Tok'ra symbiote` and confirm voluntary implantation only.
8. Spawn `Goa'uld symbiote` and confirm previous hostile workflows.


## Tok'ra pawn-group nested-profile regression

1. Apply the `0.1.41-dev-r1` patch.
2. Restart RimWorld completely.
3. Open `Player.log`.
4. Confirm this error no longer appears:
   ```text
   Type PawnGroupMakerDef is not a Def type or could not be found
   ```
5. Confirm no new `SG1_Tokra`, `pawnGroupMakers` or
   `maxPawnCostPerTotalPointsCurve` error appears.
6. Confirm:
   ```text
   [GateRim SG-1] Version 0.1.41.0 loaded.
   ```
7. Repeat Tok'ra-host, free-Tok'ra and Goa'uld regression tests.


## Tok'ra peaceful visitor prototype

1. Build with `build.cmd`.
2. Restart RimWorld completely.
3. Open developer tools.
4. Run:
   ```text
   Do incident
       ↓
   Tok'ra peaceful visitors (test)
   ```
5. Confirm a neutral letter appears.
6. Confirm `1` to `3` Tok'ra hosts enter from the map edge.
7. Wait up to `60` ticks.
8. Confirm each visitor receives active Tok'ra symbiosis.
9. Confirm visitors are not player-controlled.
10. Confirm automatic departure after the visit.
11. Save and reload after the first visit.
12. Trigger the incident again.
13. Confirm the same hidden Tok'ra faction instance is reused.
14. Confirm no random storyteller visits, traders or settlements are enabled.
15. Repeat free-Tok'ra, Tok'ra-host and Goa'uld regression tests.


## Tok'ra peaceful-visitor faction-generator build regression

1. Apply the `0.1.42-dev-r1` patch.
2. Rebuild with `build.cmd`.
3. Confirm the compiler no longer reports:
   ```text
   CS1503: cannot convert from 'RimWorld.FactionDef' to 'RimWorld.FactionGeneratorParms'
   ```
4. Restart RimWorld completely.
5. Trigger:
   ```text
   Do incident
       ↓
   Tok'ra peaceful visitors (test)
   ```
6. Confirm the hidden Tok'ra faction is created and the peaceful visit starts.

## 0.2.24-dev - Tok'ra safehouse follow-up lead

Suggested validation:

1. Build with a forced C# rebuild.
2. Prepare a safehouse test and keep Tok'ra trust neutral.
3. Enter the safehouse and exchange with the contact: the dialogue should give Medicine XP, but no follow-up lead.
4. Raise Tok'ra trust to cooperative with the debug step action, prepare/create a fresh safehouse, then exchange: one follow-up safehouse lead should be stored if capacity remains.
5. Raise Tok'ra trust to trusted and repeat with a fresh contact: one follow-up lead should again be stored if capacity remains.
6. Fill the lead registry to the cap and repeat: the briefing should report that stored lead capacity is already full.
7. Confirm the contact remains non-trading, non-recruitable, non-hostile and once per generated contact.

## Tok'ra organic operation opportunities

The procedures below are arranged to minimize reloads. Keep developer mode enabled and use the exact developer-action labels shown in backticks. Player-facing trust changes are checked through qualitative RP messages and the channel report; exact trust deltas are internal balance values and are not expected to appear in the normal interface.

### Shared preparation — perform once

**Purpose:** create a reusable starting state for all current-version tests.

1. Load a player home map with:
   - one powered Tok'ra secure communicator;
   - one Tok'ra delivery drop zone;
   - one colon capable of Intellectual work;
   - one colon capable of Medical work;
   - at least one available medical bed and ordinary medical supplies;
   - at least two industrial medicines in one reachable stockpile stack;
   - developer mode enabled.
2. Select the Intellectual-capable colon and record the current Intellectual XP.
3. Open the Tok'ra channel report and note the current qualitative relationship state. Exact trust values are intentionally hidden from the normal player interface.
4. Run `Tok'ra ops: reset framework`.
5. Save the game as `GR_TokraOrganic_Base`.

**Expected result:** no organic Tok'ra operation, intelligence module, visiting medical liaison, legacy handoff container or wounded patient is active. This save is the common checkpoint for later reload and failure tests.

### Continuous session A — success paths and placement

These tests may be executed consecutively without reloading `GR_TokraOrganic_Base`.

#### A1 — Complete an observation operation

**Purpose:** verify the complete observation success path and its rewards.

1. Run `Tok'ra ops: force observation offer`.
2. Select the Intellectual-capable colon.
3. Right-click the powered communicator and choose `Accept Tok'ra observation request`.
4. Confirm that the communicator reports the observation as in progress.
5. Run `Tok'ra ops: advance current phase`.
6. Right-click the communicator again. Confirm that `Transmit Tok'ra observation report` is now present and enabled, then choose it.
7. Compare the transmitting colonist's Intellectual XP with the value recorded before step 1, then read the success message and the Tok'ra channel report.

**Expected result:**

- the player-facing message and channel report indicate that Tok'ra confidence has improved; the exact internal trust change is not required in the normal interface;
- immediately after the advance action, the communicator exposes `Transmit Tok'ra observation report`;
- the transmitting colonist gains exactly `250` Intellectual XP;
- the success letter appears once;
- no organic operation remains active;
- no physical objective exists for this archetype.

**End state:** continue directly to A2.

#### A2 — Verify that a resolved observation cannot resolve twice

**Purpose:** verify the shared resolution guard immediately after A1.

1. Record the current Intellectual XP and note the current qualitative Tok'ra relationship state in the channel report.
2. Run `Tok'ra ops: advance current phase`.
3. Run `Tok'ra ops: fail current operation`.
4. Right-click the communicator and verify that `Transmit Tok'ra observation report` is absent.

**Expected result:** both developer actions report that no suitable active operation exists. The qualitative trust state and XP remain unchanged, and no second success or failure letter appears.

**End state:** continue directly to A3.

#### A3 — Analyze an intelligence module cautiously at the delivery zone

**Purpose:** verify preferred placement and the complete cautious-analysis path.

1. Record the selected colon's Intellectual XP and note the current qualitative Tok'ra relationship state in the channel report.
2. Run `Tok'ra ops: force intelligence offer`.
3. Right-click the powered communicator and choose `Accept Tok'ra intelligence recovery`.
4. Confirm that exactly one sealed intelligence module appears on or immediately beside the Tok'ra delivery drop zone.
5. Confirm that the module has no direct completion action.
6. Select an Intellectual-capable colon, right-click the powered communicator and choose `Analyze Tok'ra intelligence module`.
7. Choose `Cautious analysis`.
8. Confirm that the colon walks to the module, carries it to the communicator, and only then begins the analysis.
9. Let the work complete.

**Expected result:**

- the player-facing message and channel report indicate that Tok'ra confidence has improved; the exact internal trust change is not required in the normal interface;
- the analyzing colon gains exactly `350` Intellectual XP;
- the module disappears after completion;
- one cautious success variant appears;
- no signal patrol is queued;
- no item, resource or material reward remains;
- no organic operation remains active.

**End state:** continue directly to A4.

#### A4 — Verify communicator placement and accelerated decoding

**Purpose:** verify the second placement route and the accelerated method without reloading the game.

1. Remove the Tok'ra delivery drop zone.
2. Keep the secure communicator powered.
3. Run `Tok'ra ops: force intelligence offer`.
4. Accept it through the communicator.
5. Confirm that exactly one intelligence module appears beside the powered communicator rather than at the map edge.
6. Start analysis from the communicator and choose `Accelerated decoding`.
7. Confirm that the colon retrieves and carries the module to the communicator before decoding begins.
8. Complete the work. When necessary, use the dedicated debug action to test the interference branch separately.

**Expected result:** the accelerated work is shorter, the operator gains exactly `500` Intellectual XP, the module is removed, and no stale or duplicate module remains. The border fallback is not used while a powered communicator exists. A detected-interference occurrence queues a delayed patrol without cancelling the success.

**End state:** recreate the delivery zone if desired, then continue to A5.

#### A5 — Shelter, stabilize and release a wounded Tok'ra agent

**Purpose:** verify that the patient cannot recover alone, requires real colony treatment, then resumes normal Tok'ra recovery and leaves once fit to travel.

1. Recreate the Tok'ra delivery zone if it was removed; its presence is irrelevant to this pawn-arrival operation.
2. Run `Tok'ra ops: force wounded agent offer`.
3. Read the offer and confirm that it asks for shelter and treatment, does not mention colony medicine stocks, and says that ignoring it has no consequence.
4. Select a colon, right-click the powered communicator and choose `Accept the wounded Tok'ra agent` in English or `Accueillir l'agent Tok'ra blessé` in French.
5. Confirm that exactly one injured Tok'ra agent appears at a reachable map edge, already downed, with the health condition `symbiote shock` or `choc du symbiote`.
6. Pause briefly without rescuing the patient. Confirm that the agent cannot stand or walk toward the colony. Slow vanilla or residual healing may still occur, but the shock must keep the patient downed and unable to complete the event without colony care.
7. Consult the channel report and confirm that it names only this current operation and indicates that the agent is awaiting rescue and emergency treatment.
8. Rescue the agent into a player-owned bed marked for medical use. While a colon is carrying the patient, confirm that the patient does not vanish and that no operation-failure letter appears.
9. Once the patient is placed in the bed and before a doctor tends the agent, confirm that the shock remains active.
10. If ordinary injuries or illnesses are still present, let them heal or remove them through developer tools until `symbiote shock` / `choc du symbiote` is the patient's only remaining medical condition.
11. Select a doctor, right-click the patient in the player medical bed and confirm that a normal tending action is still available for the shock itself. Complete that tending action.
12. Wait for the shared operation check, then confirm that a message reports the emergency treatment, the shock hediff disappears and normal Tok'ra regeneration can resume.
13. Continue ordinary medical care, feeding and rest. Do not use `Tok'ra ops: advance current phase`; that command is not intended to heal the patient.
14. Observe the health tab while recovery progresses. Complete healing is not required.
15. When the agent becomes conscious, mobile and medically stable, confirm that a message announces preparation for departure.
16. Let the agent walk off the map.

**Expected result:**

- the patient arrives downed and cannot travel or become fit to leave before player intervention;
- the temporary carried state used by vanilla rescue does not count as the patient disappearing from the map;
- rescue to a medical bed alone does not remove the shock;
- the shock itself remains directly tendable even when every ordinary injury or illness has already healed;
- the shock is removed only after that condition has been tended in a player medical bed;
- after that treatment, normal Tok'ra recovery resumes alongside vanilla medical care;
- the operation does not create a medicine container or consume an arbitrary fixed stack;
- the agent may leave with minor remaining injuries once fit to travel;
- success is not reported merely when the agent becomes stable; it is reported once after the living agent actually leaves the map;
- the player-facing result indicates improved Tok'ra confidence without revealing a raw value;
- the communicator immediately returns to its generic RP state;
- no stale observation or intelligence-module text remains.

**End state:** continue directly to A6.

#### A6 — Complete a medical-supply handoff with a visiting liaison

**Purpose:** verify that the new social-logistical archetype does not inspect stocks before acceptance, then consumes exactly two industrial medicines only when the liaison dialogue confirms the donation.

1. Load `GR_TokraOrganic_Base`.
2. Record the Social XP of one player colon capable of Social.
3. Temporarily forbid or move all industrial medicine so none is accessible to that colon.
4. Run `Tok'ra ops: force medical handoff offer`.
5. Read the offer and confirm that it asks for two industrial medicines, does not claim to know the colony's reserves, and says that ignoring it has no consequence.
6. Select any valid colon, right-click the powered communicator and choose `Accept Tok'ra medical resupply request` or `Accepter la demande de ravitaillement médical Tok'ra`.
7. Confirm that acceptance succeeds despite the unavailable medicine and that no container is created.
8. Advance normal game time. Confirm that one Tok'ra liaison enters from the map edge roughly one to two in-game hours later.
9. With a Tok'ra delivery zone present, confirm that the liaison walks toward it. Repeat from a fresh checkpoint without the zone and confirm fallback near the powered communicator. If neither target exists, confirm a reachable point near the colony centre is used.
10. Select a colon incapable of Social and right-click the liaison. Confirm that the interaction is disabled with a short reason.
11. Select the Social-capable colon, right-click the liaison and choose the short talk action.
12. Confirm that the paused dialogue contains exactly two choices: give two medicines or cancel.
13. Choose the donation while no medicine is accessible. Confirm that an error message appears, the medicine count remains unchanged and the operation remains active.
14. Reopen the dialogue, choose cancel and confirm that only the window closes.
15. Make exactly two industrial medicines accessible, including a test where the units are split between two stacks.
16. Reopen the dialogue and confirm the donation.
17. Compare the medicine count and Social XP, then inspect the result letter, liaison behavior and channel report.

**Expected result:**

- the offer and acceptance never inspect medicine stocks;
- the liaison arrives only after the delayed entry and uses the expected meeting-point priority;
- Social, not Medicine or Intellectual, controls the player interaction;
- cancel closes only the dialogue;
- insufficient stocks show a rejection without resolving the operation;
- exactly two accessible industrial medicine units are consumed on confirmation, including across multiple stacks;
- the negotiating colon gains exactly `350` Social XP;
- one qualitative Tok'ra trust improvement and one success letter are applied immediately;
- the communicator immediately returns to its generic RP state;
- the liaison begins leaving the map, but their physical exit is not required for success;
- no temporary handoff container appears.

#### A7 — Verify manual communicator actions remain independent

**Purpose:** detect regressions outside the organic-operation framework.

1. Open the communicator's right-click menu with a selected valid colon.
2. Inspect the existing manual Tok'ra requests and channel report.
3. Trigger one manual request whose ordinary conditions are currently satisfied, or inspect its disabled reason when conditions are not satisfied.

**Expected result:** existing Trusted-tier requirements, threat or patient conditions and request cooldowns remain unchanged. Organic-operation successes have not consumed manual-request cooldowns.

**End state:** continuous success-path testing is complete. Use `GR_TokraOrganic_Base` for the reload and failure sessions below.

### Checkpoint session B — current-version save and reload

Start each test from `GR_TokraOrganic_Base` unless a test explicitly creates another checkpoint.

#### B1 — Reload an offered observation

**Purpose:** verify persistence before an offer is accepted.

1. Load `GR_TokraOrganic_Base`.
2. Run `Tok'ra ops: force observation offer`.
3. Save as `GR_TokraOrganic_ObservationOffered`.
4. Reload `GR_TokraOrganic_ObservationOffered`.
5. Select the Intellectual-capable colon and right-click the powered communicator.

**Expected result:** `Accept Tok'ra observation request` is still available, the remaining offer time is coherent, and no duplicate offer or letter appears.

#### B2 — Reload an accepted observation and resolve it once

**Purpose:** verify accepted-state migration, readiness and single resolution.

1. From B1, accept the observation request.
2. Save as `GR_TokraOrganic_ObservationAccepted` before advancing it.
3. Reload that save.
4. Run `Tok'ra ops: advance current phase`.
5. Right-click the communicator and confirm that `Transmit Tok'ra observation report` is present and enabled.
6. Save as `GR_TokraOrganic_ObservationReady`.
7. Reload that save.
8. Right-click the communicator again and confirm that the same transmission action remains available.
9. Record Intellectual XP, note the qualitative Tok'ra relationship state, then transmit the report through the communicator.
10. Save the completed game as `GR_TokraOrganic_ObservationResolved` and reload it.

**Expected result:** the accepted and explicit ready states survive reloads; the transmission action is available both before and after reloading the ready checkpoint; completion reports a single qualitative trust improvement and grants exactly `250` Intellectual XP once; reloading the resolved save does not repeat the letter, trust gain or XP gain.

#### B3 — Reload an accepted intelligence analysis and resolve it once

**Purpose:** verify restoration of the physical objective, selected method, remaining work and deadline.

1. Load `GR_TokraOrganic_Base`.
2. Run `Tok'ra ops: force intelligence offer` and accept it through the communicator.
3. Confirm that one module exists, then save as `GR_TokraOrganic_ModuleAccepted`.
4. Reload that save and confirm that the same module remains active.
5. Start cautious analysis from the communicator and save once while the colon is carrying the module toward the communicator.
6. Reload, confirm that the same module remains associated with the operation, then interrupt the analysis after partial progress and save as `GR_TokraOrganic_ModuleAnalysis`.
7. Reload, resume from the communicator and complete the analysis.
8. Save as `GR_TokraOrganic_ModuleResolved`, then reload the resolved save.

**Expected result:** the framework restores the same module, method and remaining work after reload; completion reports one qualitative trust improvement and grants exactly `350` Intellectual XP once; the module is removed; the resolved save does not repeat the outcome.

#### B4 — Reload wounded-agent shock, care and departure states

**Purpose:** verify persistence of the patient reference, initial-treatment flag, health progress and departure state.

1. Load `GR_TokraOrganic_Base`.
2. Run `Tok'ra ops: force wounded agent offer`, accept through the communicator and save as `GR_TokraOrganic_PatientShock` before rescuing the downed patient.
3. Reload that save and confirm that the same named patient remains downed with symbiote shock, no duplicate pawn appears and regeneration is still suppressed.
4. Rescue the patient into a player medical bed. If necessary, let or force every ordinary injury and illness to heal so that only symbiote shock remains.
5. Confirm that a doctor can still tend the shock itself, complete that tending action and wait until the shock is removed. Then save as `GR_TokraOrganic_PatientCare` while the patient is still recovering.
6. Reload that save and confirm that the same named patient remains active, the shock does not return, no duplicate pawn appears and the communicator reports only that patient's care.
7. Continue treatment until the departure message appears, then save immediately as `GR_TokraOrganic_PatientDeparting` before the patient reaches the edge.
8. Reload the departing save and allow the patient to leave.
9. Save as `GR_TokraOrganic_PatientResolved` and reload once more.

**Expected result:** shock persists before first treatment, remains removed after the treatment checkpoint, the same patient and health state survive reloads, the departure order survives the second reload, success occurs once after map exit, and reloading the resolved save does not repeat trust feedback or letters.

#### B5 — Reload the medical-supply liaison flow and resolve it once

**Purpose:** verify restoration of delayed arrival, meeting state, dialogue cancellation, deadline, donation and post-success departure without duplicate effects.

1. Load `GR_TokraOrganic_Base`, run `Tok'ra ops: force medical handoff offer` and accept through the communicator.
2. Save immediately as `GR_TokraOrganic_MedicalSupplyBeforeArrival`, reload it and confirm that the liaison still arrives once after the remaining delay.
3. While the liaison is walking to the meeting point, save as `GR_TokraOrganic_MedicalSupplyApproaching` and reload it.
4. Confirm that the same liaison continues toward the same meeting point and that no duplicate pawn appears.
5. Once the liaison is ready, open the dialogue, choose cancel, save as `GR_TokraOrganic_MedicalSupplyWaiting` and reload it.
6. Confirm that the same liaison remains available, the deadline is coherent and the dialogue can be reopened.
7. Record the negotiator's Social XP and the exact industrial-medicine count, then donate two units.
8. Save immediately while the successful liaison is leaving as `GR_TokraOrganic_MedicalSupplyDeparting`, reload it and allow the pawn to exit.
9. Save as `GR_TokraOrganic_MedicalSupplyResolved` and reload once more.

**Expected result:** each checkpoint restores one liaison, one meeting point and one deadline; cancelling the dialogue never changes stocks or trust; exactly two medicine units and exactly `350` Social XP are applied once; success remains resolved while the liaison leaves; no duplicate letter, trust result, medicine consumption, XP gain or liaison appears after reload.

### Failure session C — destructive and expiry paths

Use copies of `GR_TokraOrganic_Base` so each failure starts from a known state.

#### C1 — Fail an accepted observation through the shared debug action

**Purpose:** verify one failure consequence and no duplicate application.

1. Load `GR_TokraOrganic_Base`.
2. Note the current qualitative Tok'ra relationship state in the channel report.
3. Run `Tok'ra ops: force observation offer`, select an Intellectual-capable colon, right-click the powered communicator and choose `Accept Tok'ra observation request`.
4. Run `Tok'ra ops: fail current operation`.
5. Save as `GR_TokraOrganic_ObservationFailed` and reload it.
6. Run `Tok'ra ops: fail current operation` again.

**Expected result:** one player-facing message reports a deterioration of Tok'ra confidence, the failure letter appears once, the operation is cleared, and the second failure attempt does not change trust.

#### C2 — Destroy an accepted intelligence module

**Purpose:** verify physical-objective loss and cleanup.

1. Load `GR_TokraOrganic_Base`.
2. Note the current qualitative Tok'ra relationship state in the channel report.
3. Run `Tok'ra ops: force intelligence offer`, select an Intellectual-capable colon, right-click the powered communicator and choose `Accept Tok'ra intelligence recovery`.
4. Destroy the spawned intelligence module through developer tools or damage.
5. Let the game advance until the tracker processes the missing objective.
6. Save and reload after the failure has been reported.

**Expected result:** one player-facing message reports a deterioration of Tok'ra confidence, one failure letter appears, the operation is cleared, and no stale module remains after reload.

#### C3 — Let an accepted operation expire

**Purpose:** verify deadline failure independently from manual destruction.

1. Load `GR_TokraOrganic_Base`.
2. Note the current qualitative Tok'ra relationship state in the channel report.
3. Force and accept one archetype using its exact developer action and communicator action listed in A1 or A3.
4. Do not complete the objective; advance game time beyond the displayed secure window.
5. After the failure appears, continue the game for several additional hours and then save/reload.

**Expected result:** one player-facing message reports a deterioration of Tok'ra confidence. No repeated failure, letter or additional trust loss occurs after more time or after reload.

#### C4 — Ignore an unsolicited offer

**Purpose:** verify that declining by inaction remains consequence-free.

1. Load `GR_TokraOrganic_Base`.
2. Note the current qualitative Tok'ra relationship state in the channel report.
3. Run any one of `Tok'ra ops: force observation offer`, `Tok'ra ops: force intelligence offer`, `Tok'ra ops: force wounded agent offer` or `Tok'ra ops: force medical handoff offer`.
4. Do not accept it and advance game time until the offer closes.

**Expected result:** the offer disappears, trust remains unchanged, no failure letter is issued and a future hidden opportunity can still be scheduled.

#### C5 — Let the wounded agent die

**Purpose:** verify death failure while preserving the corpse and preventing duplicate consequences.

1. Load `GR_TokraOrganic_Base`.
2. Force and accept `Tok'ra ops: force wounded agent offer`.
3. After the patient arrives, allow the injuries or illness to cause death, or use a developer health action to kill the patient without deleting the pawn.
4. Advance the game until the tracker processes the death, then save and reload.

**Expected result:** one RP failure reports the death and deterioration of Tok'ra confidence; the operation clears once; the corpse is not silently removed; reload does not apply a second failure.

#### C6 — Capture the wounded agent

**Purpose:** verify that taking the patient prisoner is treated as compromising the refuge.

1. Load `GR_TokraOrganic_Base`.
2. Force and accept the wounded-agent offer.
3. Arrest or otherwise turn the patient into a colony prisoner before departure.
4. Advance the game until the tracker processes the new status.

**Expected result:** one RP failure explains that the refuge was compromised, the operation clears, the captured pawn remains a prisoner, and no repeated penalty appears.

#### C7 — Keep the patient unfit until the care window closes

**Purpose:** verify the specific medical timeout rather than death or disappearance.

1. Load `GR_TokraOrganic_Base`.
2. Force and accept the wounded-agent offer.
3. Keep the patient alive but medically unfit to travel; for example, stabilize immediate bleeding while leaving a serious condition unresolved.
4. Advance beyond the remaining secure window shown by the communicator report.
5. Continue several more in-game hours, then save and reload.

**Expected result:** one timeout failure explains that a covert Tok'ra team recovered the living agent, the patient is removed by operation cleanup, and no second failure occurs later or after reload.

#### C8 — Lose the liaison before the handoff

**Purpose:** verify the accepted-operation failure paths tied to the visiting pawn.

1. Load `GR_TokraOrganic_Base`.
2. Note the current qualitative Tok'ra relationship state in the channel report.
3. Force and accept the medical-resupply offer, then wait for the liaison to arrive.
4. In separate copies of the checkpoint, test one of these conditions before donating medicine:
   - kill the liaison;
   - arrest the liaison;
   - remove or despawn the liaison through developer tools.
5. Let the tracker process the state, then save and reload.

**Expected result:** each scenario produces one appropriate RP failure, one qualitative deterioration after the accepted commitment, immediate return to the generic channel state and no repeated failure after reload. No medicine is consumed.

#### C9 — Let the liaison leave without receiving medicine

**Purpose:** verify that missing supplies before acceptance is allowed, but an accepted commitment fails when the liaison's waiting window expires.

1. Load `GR_TokraOrganic_Base`.
2. Forbid or remove all industrial medicine.
3. Force and accept the medical-resupply offer; confirm that acceptance still succeeds.
4. Wait for the liaison to arrive and reach the meeting point.
5. Do not complete the donation. Advance beyond the six-hour window shown by the channel report.
6. Confirm that the liaison begins leaving, then save and reload.

**Expected result:** the operation expires once with its specific accepted-failure text and qualitative trust deterioration; the liaison leaves; no medicine is consumed; the communicator returns to its generic state; no repeated letter or penalty appears after more time or reload.

#### C10 — Kill the liaison after a successful donation

**Purpose:** verify that a post-handoff death has a separate diplomatic consequence without invalidating completed success.

1. Load `GR_TokraOrganic_Base`.
2. Force and accept the medical-resupply offer, wait for the liaison and donate two medicines successfully.
3. Confirm the success letter, generic communicator state and departure order.
4. Before the liaison reaches the map edge, kill them through developer tools or an in-game threat.
5. Continue several tracker checks, then save and reload.

**Expected result:** the operation remains completed and never changes to failure; consumed medicine and Social XP are not restored; one separate negative RP letter and qualitative relationship penalty are applied for the liaison's death; neither the success nor the death consequence repeats after reload.

### Legacy-save migration session D

These tests require preserved saves created with the stated published version. They cannot be replaced by a current-version checkpoint.

#### D1 — Load `0.2.48-dev` observation states

1. Load a `0.2.48-dev` save with an observation offer active.
2. Confirm it can still be accepted or ignored normally.
3. Load a separate `0.2.48-dev` save with observation already accepted.
4. Confirm its remaining preparation or transmission window is coherent and that it can complete once.

**Expected result:** no red loading error occurs, no duplicate offer is created and the operation retains one qualitative trust improvement on success, `250` Intellectual XP, and one qualitative trust deterioration after an accepted failure.

#### D2 — Load `0.2.49-dev` intelligence-recovery states

1. Load a `0.2.49-dev` save with the offer active and accept it.
2. Confirm one module is placed through the normal preferred route.
3. Load a separate `0.2.49-dev` save with an accepted module already on the map.
4. Confirm the tracker recovers that module and its deadline.
5. Complete or fail the operation once.

**Expected result:** no red loading error occurs, no duplicate module is created, and the result applies only once.

#### D3 — Load a published `0.2.50-dev` save

1. Load a save created before the wounded-agent archetype existed.
2. Confirm that no patient or stale patient state is created during migration.
3. Force each of the four current archetypes in turn, resolving or resetting one before forcing the next.

**Expected result:** previous observation and intelligence states remain compatible, both newer archetypes become available normally, and only the currently active operation is displayed.

#### D4 — Load a published `0.2.51-dev` save and an optional `0.2.52-dev-r1/r2` development save

1. Load a clean `0.2.51-dev` save with no active organic operation.
2. Confirm that no liaison or stale handoff state is created during migration.
3. Force and accept the new medical-resupply offer, save before arrival, reload, then complete or reset it once.
4. Load a separate `0.2.51-dev` save with a wounded-agent operation in progress and confirm that its patient state still behaves normally.
5. When an unpublished `0.2.52-dev-r1/r2` save with the temporary container exists, load it and inspect the former handoff location.

**Expected result:** the framework initializes at save version `5` without red loading errors; the liaison archetype becomes available normally; existing wounded-agent treatment, departure and death-priority behavior remain intact; an old development container disappears automatically and the accepted handoff restarts as a delayed liaison visit without duplicate trust, XP or resource effects.

### Developer actions, presentation and final log review

1. Confirm the four force actions create the explicitly named archetype:
   - `Tok'ra ops: force observation offer`;
   - `Tok'ra ops: force intelligence offer`;
   - `Tok'ra ops: force wounded agent offer`;
   - `Tok'ra ops: force medical handoff offer`.
2. Confirm `Tok'ra ops: advance current phase` prepares an accepted observation report and does not invalidate an already placed intelligence module, wounded patient or visiting medical liaison.
3. Confirm `Tok'ra ops: reset framework` clears the active state, intelligence objective, living patient or active liaison without changing trust. A dead patient's or liaison's corpse should remain.
4. Review English and French player-facing letters, messages, dialogue and context actions for RP tone and understandable wording.
5. Confirm communicator, module and liaison interaction labels remain short.
6. Confirm the medical dialogue displays only the donation and cancel buttons, with no technical state or hidden timing details.
7. Confirm developer-action labels remain technical, explicit and readable without meaningful truncation. In particular, verify these compact legacy labels:
   - `Jaffa mark: black`, `Jaffa mark: silver`, `Jaffa mark: gold`, `Clear Jaffa mark`;
   - `Tok'ra safehouse: prepare`, `Tok'ra safehouse: create`, `Tok'ra safehouse: verify`;
   - `Tok'ra trust: +5`, `Tok'ra trust: -5`;
   - `Tok'ra cache: deliver`, `Tok'ra cache: reset`;
   - `Tok'ra lead: decode`, `Tok'ra site: reveal`, `Tok'ra site: recon`;
   - `Tok'ra relay: prepare`, `Tok'ra relay: complete`;
   - `Tok'ra threat: create`, `Tok'ra threat: clear`.
8. Close or pause the game and inspect `Player.log`.

**Expected result:** no red errors related to operation loading, Scribe references, pawn or lord persistence, meeting-point pathing, dialogue jobs, stock counting, medicine consumption, legacy-container cleanup, departure monitoring or duplicate resolution appear.
