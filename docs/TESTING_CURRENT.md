# Validation terminée - 0.3.55-dev

Jalon : `0.3.55-dev - Add Goa'uld extraction reprisals`

Branche : `feature/goauld-domain-extraction-reprisal`

Base : `v0.3.54-dev` (`1b1afbe`)

Version de DLL attendue : `0.3.55.0`

Révision locale : `r1`

Statut : révision finale `r1` validée ; branche, tag annoté `v0.3.55-dev` et
wiki publiés.

Charger dans cet ordre :

```text
Core
Harmony
Biotech
GateRim SG-1
```

## Test principal r1

Sur une carte de colonie joueur, ouvrir exactement :

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Domain reactions...
```

1. Cliquer sur `Reset extraction reprisals`.
2. Cliquer sur `Schedule extraction reprisal` et vérifier la lettre immédiate
   `Représailles d'un domaine Goa'uld`. Elle doit nommer le domaine et expliquer
   que l'extraction a provoqué sa mobilisation.
3. Cliquer sur `Show extraction reprisal state` et vérifier une réaction en
   attente sur la carte courante avec un nombre de points supérieur à zéro.
4. Sauvegarder, recharger et rouvrir ce rapport : domaine, carte et points
   doivent être conservés.
5. Cliquer sur `Trigger pending reprisal now` et vérifier qu'un raid
   Goa'uld/Jaffa du domaine annoncé arrive à pied depuis un bord de carte.
6. Rouvrir le rapport et vérifier un refroidissement, sans réaction encore en
   attente. Contrôler `Player.log` sans nouvelle erreur C#, XML, Scribe, faction
   ou raid.

Résultat attendu : une extraction annoncée produit une seule conséquence
persistante attribuée au bon domaine, sans nouveau catalogue de missions.

Résultat : validé par le mainteneur. La lettre, l'état persistant après
sauvegarde/rechargement, le raid du domaine, l'arrivée à pied, le
refroidissement et `Player.log` sont acceptés.

## Régressions optionnelles

- Programmer deux fois avant le raid : la seconde commande doit être refusée.
- Reprogrammer après le raid : la commande doit rester refusée pendant le
  refroidissement de 15 jours.
- Sur un prisonnier avec un hôte Goa'uld actif, terminer l'opération Santé
  `extraire le symbiote Goa'uld actif` et vérifier le même avertissement sans
  utiliser `Schedule extraction reprisal`.
- Avec deux factions Goa'uld, utiliser un hôte du second domaine et vérifier
  que la lettre et les assaillants conservent cette faction exacte.

---

# Validation terminée - 0.3.54-dev

Jalon : `0.3.54-dev - Enable natural Goa'uld assault doctrines`

Branche : `feature/goauld-natural-assault-doctrines`

Base : `v0.3.53-dev` (`7af8b1c`)

Version de DLL attendue : `0.3.54.0`

Révision locale : `r2`

Statut : révision finale `r2` validée. `r1` a validé le rapport, le direct et
l'enlèvement ; `r2` a corrigé puis validé la commande de destruction sous
`10 000` de richesse bâtie. Branche, tag annoté `v0.3.54-dev` et wiki publiés.

Charger dans cet ordre :

```text
Core
Harmony
Biotech
GateRim SG-1
```

La même colonie de test peut être utilisée quelle que soit sa richesse bâtie.

## Test ciblé r2

Ouvrir exactement :

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Threat progression...
```

1. Cliquer sur `Show current progression` et vérifier que le rapport indique
   explicitement `destruction needs 10000` à côté de la richesse bâtie.
2. Cliquer sur `Force natural destruction raid (1800 points)` et vérifier que
   le raid démarre même si la richesse bâtie affichée est inférieure à `10 000`.
3. Vérifier la lettre `raid de destruction de Jaffa Goa'uld`, la phase
   destructrice puis le repli/récupération et l'arrivée à pied sans pod.
4. Contrôler
   `Player.log` sans nouvelle erreur C#, XML, arrivée de pawn ou Lord.

Résultat attendu : le tirage naturel conserve son seuil de richesse, mais la
commande développeur force réellement la doctrine demandée.

Résultat : validé par le mainteneur. Le raid de destruction forcé démarre,
conserve sa doctrine, arrive à pied sans pod et ne révèle aucune nouvelle
erreur dans `Player.log`.

## Régressions optionnelles

- Avec moins de deux colons libres, vérifier `0%` d'enlèvement dans le rapport
  sans désactiver sa commande forcée.
- Sous `10 000` de richesse bâtie, vérifier `0%` de destruction naturelle sans
  désactiver sa commande forcée.
- Sauvegarder/recharger puis vérifier que le choix forcé précédent ne persiste
  pas dans le raid suivant.

---

# Validation terminée - 0.3.53-dev

Jalon : `0.3.53-dev - Audit Goa'uld threat progression`

Branche : `feature/goauld-threat-progression-audit`

Base : `v0.3.52-dev` (`3830be6`)

Version de DLL attendue : `0.3.53.0`

Révision locale : `r2`

Statut : révision finale `r2` validée. `r1` a validé la progression puis révélé
les pods à `4000` points ; `r2` impose l'arrivée à pied sans régression. La
branche et le tag annoté `v0.3.53-dev` sont publiés.

Charger dans cet ordre :

```text
Core
Harmony
Biotech
GateRim SG-1
```

## Test principal r2

Utiliser une sauvegarde permettant de recharger le même état avant chaque
raid. Ouvrir exactement :

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Threat progression...
```

1. Cliquer sur `Force advanced direct raid (4000 points)`.
2. Vérifier que toute la force arrive à pied depuis un bord de carte, sans aucun pod de transport.
3. Recharger entre chaque essai, puis tester `Force current abduction raid` et `Force current destruction raid`.
4. Vérifier la même arrivée à pied tout en conservant les comportements distincts de capture et de destruction.
5. Ouvrir `Actions de débogage` > `GateRim SG-1` > `Tok'ra...` > `Safehouse and intelligence chain...` > `Threat intelligence...` > `Create threat`.
6. Attendre le court délai et vérifier que la force annoncée arrive elle aussi à pied depuis un bord de carte.
7. Contrôler `Player.log` et l'absence de nouvelle erreur rouge C#, XML, arrivée de pawns ou Lord.

Résultat attendu : tous les raids Goa'uld/Jaffa du worker commun utilisent
`EdgeWalkIn`, même à `4000` points, sans régression de taille ou de doctrine.

Résultat : validé par le mainteneur, y compris le raid avancé, les doctrines
d'enlèvement/destruction, la menace interceptée et `Player.log`.

## Régression optionnelle du relais

1. Ouvrir `Actions de débogage` > `GateRim SG-1` > `Tok'ra...` > `Safehouse and intelligence chain...` > `Decoded lead...` > `Reveal site`.
2. Envoyer normalement une caravane sur le site révélé et lancer l'opération avec l'action normale du site.
3. Comparer la garnison et le plan au rapport `Show current progression` : `command bunker` sous `600` points de défense, `split relay station` de `600` à `1399`, `walled relay courtyard` à partir de `1400`.
4. Vérifier le sabotage, l'arrivée des renforts, l'évacuation et la sauvegarde/recharge.

## Régression optionnelle des colonies Goa'uld

1. Attaquer une colonie Goa'uld depuis une sauvegarde jeune ou peu riche et noter approximativement la garnison et les défenses.
2. Recommencer contre une autre colonie Goa'uld depuis une sauvegarde avancée et riche, avec les mêmes réglages de storyteller.
3. Vérifier que la génération vanilla `Settlement` produit une base clairement plus forte, et non la même petite garnison fixe.

---

# Validation terminée - 0.3.52-dev

Jalon : `0.3.52-dev - Add the Goa'uld faction caste summary`

Branche : `feature/goauld-caste-world-summary`

Base : `v0.3.51-dev` (`3ec8ac0`)

Version de DLL attendue : `0.3.52.0`

Révision locale : `r1`

Statut : révision finale `r1` validée et publiée sous le tag annoté `v0.3.52-dev`.

Contrôles locaux :

- [x] `git diff --check` ;
- [x] parsing XML anglais et français ;
- [x] `./tools/check-project-consistency.cmd` ;
- [x] rebuild forcé `0.3.52.0`, `0` avertissement et `0` erreur.

## Test principal r1

Résultat : validé, y compris l'infobulle Goa'uld, l'absence de section Goa'uld
sur `Jaffa libres`, la génération du monde et `Player.log`.

Charger dans cet ordre :

```text
Core
Harmony
Biotech
GateRim SG-1
```

1. Ouvrir `Nouvelle colonie` > `Équipe SG isolée` > `Créer le monde` > `Factions`.
2. Placer le pointeur sur `Domaines des Grands Maîtres Goa'uld`.
3. Vérifier que l'infobulle conserve la description et la section vanilla des xénotypes, puis affiche `Castes Goa'uld` avec `Serviteurs Jaffa : population dominante`, `Hôtes Goa'uld : minorité des colonies`, `Hôte Grand Maître : dirigeant de faction` et l'explication de la possession acquise.
4. Placer le pointeur sur `Jaffa libres` et vérifier que cette infobulle ne contient pas `Castes Goa'uld`.
5. Générer le monde, atteindre l'écran de choix de la tuile de départ et contrôler `Player.log`.

Résultat attendu : seul le résumé Goa'uld reçoit la section de castes ; les
pourcentages de xénotypes restent inchangés ; la génération du monde réussit et
aucune nouvelle erreur Harmony, C# ou de traduction n'apparaît.

## Régression optionnelle

- Dans `Créer le monde` > `Factions`, utiliser `Ajouter...` pour créer une seconde entrée `Domaines des Grands Maîtres Goa'uld` et vérifier que les deux lignes exposent le même résumé sans duplication interne.

---

# Validation précédente - 0.3.51-dev

Jalon : `0.3.51-dev - Add GateRim mission-site world icons`

Branche : `feature/operation-site-icon-overhaul`

Base de départ : branche validée `feature/faction-world-icon-overhaul`, commit `adb1eed`

Version de DLL attendue : `0.3.51.0`

Révision locale : `r3`

Statut : révision finale `r3` validée et publiée par branche et sous le tag
annoté `v0.3.51-dev`. Les pictogrammes colorés sont lisibles à zoom étendu,
les textures vanilla fonctionnent au zoom rapproché et le cycle de vie des
sites correspond aux règles documentées.

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
