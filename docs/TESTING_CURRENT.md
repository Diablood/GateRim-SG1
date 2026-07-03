# Validation finale - 0.3.57-dev

Jalon : `0.3.57-dev - Add System Lord kara kesh shield`

Branche : `feature/goauld-system-lord-personal-shield`

Base : `v0.3.56-dev` (`93b29a7`)

Version de DLL validée : `0.3.57.0`

Révision locale : `r5`

Statut : rebuild forcé et validation ciblée `r5` réussis ; branche, tag annoté
`v0.3.57-dev` et wiki séparé publiés.

Charger dans cet ordre :

```text
Core
Harmony
Biotech
GateRim SG-1
```

## Test ciblé r5

1. Sur une carte de colonie, ouvrir `Recherche > GateRim SG-1`. Vérifier que
   `Kara kesh` se trouve directement à droite de `Armures
   Jaffa` et exige cette recherche.
2. Ouvrir exactement :

```text
Actions de débogage > GateRim SG-1 > Goa'uld... > Kara kesh shield...
```

3. Cliquer sur `Spawn hostile System Lord with kara kesh`, mettre le jeu en
   pause, sélectionner le Grand Maître et vérifier qu'il porte `kara kesh` et
   que le gizmo vanilla `Énergie du bouclier` est visible. Ne pas utiliser
   `Spawn kara kesh` pendant ce contrôle : cette commande
   distincte dépose volontairement un exemplaire seul au sol.
4. Cliquer dix fois sur `Apply ranged test hit`, puis chaque fois sur le Grand
   Maître : les dix impacts doivent être absorbés sans blessure et le champ doit
   rester actif. Le gizmo doit montrer une baisse progressive sans recharge
   entre les impacts rapprochés. Après le dernier impact, vérifier que la
   recharge ne reprend qu'après environ `300` ticks, soit cinq secondes à
   vitesse normale.
5. Cliquer sur `Apply melee test hit`, puis sur le même pawn : la blessure
   contondante doit passer malgré le champ encore actif.
6. Cliquer sur `Apply EMP test hit`, puis sur le même pawn : le champ doit se
   rompre immédiatement avec son, flash et fissures distincts. Laisser passer
   `1800` ticks et vérifier son retour à pleine charge. Contrôler `Player.log`
   sans nouvelle erreur XML, Def, texture ou C#.

Résultat attendu : la progression de recherche est explicite, le Grand Maître
porte toujours le bouclier et ses trois interactions de combat sont lisibles et
contrables.

Résultat : validé par le mainteneur sur la révision finale `r5`. La dépendance
de recherche, le port naturel, le gizmo d'énergie, l'absorption des dix impacts
rapprochés sans recharge active, le passage de la mêlée, la rupture IEM, le
retour du champ et l'absence de nouvelle erreur dans `Player.log` sont confirmés.

## Résultat r4 et ajustement r5

Le kara kesh a résisté aux armes à feu testées sans pouvoir être épuisé, ce qui
est accepté comme cohérent avec le lore puisque la mêlée traverse toujours le
champ. Le gizmo vanilla est bien apparu sur un soldat joueur équipé. Sa recharge
active était toutefois très rapide. `r5` la suspend pendant `300` ticks après
chaque impact absorbé ; les tirs soutenus repoussent continuellement ce délai.

## Résultat r3 et rééquilibrage r4

La génération, le port du bouclier et les trois interactions ont fonctionné.
L'équilibrage a été rejeté : un impact debug ou les deux premières balles d'un
pistolet-mitrailleur suffisaient à blesser le Grand Maître. `r4` porte la
capacité à `4.0`, la consommation à `0.01` par dégât, la recharge à `0.2`, le
retour complet à `1800` ticks et le `combatPower` à `400`. L'IEM reste la
désactivation immédiate clairement signalée par les effets vanilla. Tous les
paramètres sont meilleurs que ceux de la ceinture-bouclier vanilla.

## Résultat r2

Le journal confirme que l'ancien Grand Maître `Yareteris` a reçu son bouclier
par l'initialiseur. La création du nouveau sujet de test a ensuite échoué dans
`PawnRelationWorker_Parent.ResolveMyName` : la génération vanilla tentait
aléatoirement de créer un parent et de convertir son nom formel vers un type
incompatible. La révision `r3` désactive les relations uniquement pour cette
commande debug déterministe.

## Résultat r1

Le Def et l'objet étaient présents, mais deux Grands Maîtres générés ne portaient
aucun bouclier. L'exemplaire observé au sol provenait séparément de `Spawn
kara kesh`, dont c'est le comportement normal. La révision `r2` confie
l'attribution unique à l'initialiseur C# des hôtes générés et mémorise les pawns
déjà traités afin de ne pas recréer un bouclier retiré comme butin.

## Tests optionnels

- Attendre la réinitialisation après IEM et vérifier la reprise de la recharge.
- Utiliser `Spawn kara kesh`, terminer les recherches requises en mode
  développeur et vérifier au banc d'usinage la recette, les ingrédients et le
  niveau `Fabrication 12`, six composants avancés, `100` plastacier et `60` or.
- Sauvegarder/recharger un bouclier porté avec une charge partielle.
- Vérifier que seul un Grand Maître en porte naturellement et qu'il peut devenir
  un butin, contrairement aux Jaffa et hôtes Goa'uld ordinaires.

Limite connue : le visuel est un placeholder placé sur le chemin d'asset final
en attendant la passe artistique globale.
