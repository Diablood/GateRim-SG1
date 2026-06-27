# Validation terminée - 0.3.51-dev

Jalon : `0.3.51-dev - Add GateRim mission-site world icons`

Branche : `feature/operation-site-icon-overhaul`

Base de départ : branche validée `feature/faction-world-icon-overhaul`, commit `adb1eed`

Version de DLL attendue : `0.3.51.0`

Révision locale : `r3`

Statut : révision finale `r3` validée et publiée par branche. Les pictogrammes colorés sont lisibles à zoom étendu, les textures vanilla fonctionnent au zoom rapproché et le cycle de vie des sites correspond aux règles documentées. Le tag final reste non créé faute de demande explicite.

Contrôles locaux `r3` :

- [x] les sept textures rapprochées utilisent `World/WorldObjects/Sites/GenericSite` ;
- [x] les sept textures étendues résolvent les six PNG dédiés ;
- [x] le sous-menu sépare `Independent arcs...` et `Organic sites (one active)...` ;
- [x] `git diff --check` et le contrôle de cohérence passent ;
- [x] le rebuild forcé `0.3.51.0` termine avec `0` avertissement et `0` erreur.

## Résultat r2

- [x] Les six icônes colorées sont présentes et rendent bien.
- [x] Le défaut de zoom rapproché de `r2` a été constaté : les pictogrammes personnalisés étaient réutilisés et tournés aléatoirement.
- [x] Les différences de coexistence ont été observées : elles correspondent aux deux étapes de planque, au slot unique des opérations organiques et aux arcs indépendants.

## Test principal r3

Résultat : validé par le mainteneur, y compris le rendu aux deux niveaux de zoom, les remplacements attendus, la coexistence des arcs indépendants et `Player.log`.

Charger dans cet ordre :

```text
Core
Harmony
Biotech
GateRim SG-1
```

Utiliser une partie avec les Tok'ra activés et un communicateur sécurisé Tok'ra construit et alimenté.

### 1. Arcs indépendants

Ouvrir exactement :

```text
Actions de débogage > GateRim SG-1 > Tok'ra... > Mission-site icon tests... > Independent arcs...
```

1. Cliquer sur `Create SG1_TokraHiddenSafehouseMarker`.
2. À zoom étendu, vérifier l'arche de planque sable et turquoise.
3. Zoomer : vérifier la texture rapprochée vanilla, sans pictogramme coloré tourné.
4. Cliquer sur `Create SG1_TokraHiddenSafehouseSite`.
5. Vérifier que le marqueur est remplacé par le site révélé, avec la même arche à zoom étendu et `GenericSite` à zoom rapproché.
6. Cliquer sur `Create SG1_TokraIntroductionArtifactWorldSite`, puis sur `Create SG1_TokraDecodedMissionWorldSite`.
7. Vérifier leurs pictogrammes colorés à zoom étendu, `GenericSite` à zoom rapproché et leur présence simultanée avec la planque.

### 2. Slot organique unique

Revenir à :

```text
Actions de débogage > GateRim SG-1 > Tok'ra... > Mission-site icon tests... > Organic sites (one active)...
```

1. Cliquer sur `Create SG1_TokraDistressCallWorldSite` et contrôler sa balise colorée puis son rendu `GenericSite` rapproché.
2. Cliquer sur `Create SG1_TokraTemporaryBaseDeliverySite` : vérifier que le signal de détresse disparaît et que la caisse logistique le remplace.
3. Cliquer sur `Create SG1_TokraJaffaOfficerCaptureSite` : vérifier que la caisse disparaît et que la position d'officier Jaffa la remplace.
4. Vérifier que la planque, le site d'introduction et le relais décodé sont toujours présents.
5. Contrôler `Player.log`.

Résultat attendu : les pictogrammes personnalisés restent réservés au zoom étendu ; le zoom rapproché utilise les textures vanilla ; les étapes de planque se remplacent ; un seul site organique reste actif ; les arcs indépendants coexistent ; aucune nouvelle erreur GateRim SG-1 de texture, XML ou C# n'apparaît.

## Régressions optionnelles

- Entrer dans un site de combat et dans `SG1_TokraHiddenSafehouseSite` pour vérifier l'arrivée de caravane.
- Vérifier que les icônes de faction validées en `0.3.50-dev` restent inchangées.
