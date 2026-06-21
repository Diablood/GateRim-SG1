# Tests du jalon actif

Jalon : `0.3.21-dev - Add cultural starter loadout rules`

Branche validée : `feature/cultural-starter-loadouts`

Base : `v0.3.20-dev`

Révision fonctionnelle validée : `0.3.21-dev-r4`

Révision documentaire corrective : `0.3.21-dev-r6`

Version de DLL validée : `0.3.21.0`

Statut : validation fonctionnelle terminée sur `0.3.21-dev-r4` ; jalon clôturé et publié sous `v0.3.21-dev`.

## Résultat final validé

- Le composant spécifique `ScenPart_SGTeamStartingGear.cs` reste supprimé.
- Le contrôle de cohérence valide `0.3.21-dev`, `0.3.21.0` et `83` backstories.
- Le parseur accepte aussi bien la formulation préparatoire `Version de DLL attendue` que la formulation finale `Version de DLL validée`, sans produire un second échec redondant lorsqu’une valeur est absente.
- Le test négatif avec une tabulation Markdown volontaire échoue correctement, puis le contrôle positif repasse au vert après suppression de la sonde.
- Le rebuild forcé produit la DLL `0.3.21.0`.
- Le chargement ne signale plus `Apparel_Tshirt`, de slot d'habillement nul, de Def manquant, de patch invalide ni de texture absente.
- Les starters SG-team acceptés ont au moins 20 ans biologiques et sont capables de violence.
- Les noms Tau'ri et les carrières SGC restent appliqués.
- Chaque starter porte un tee-shirt vanilla en tissu, un pantalon SG, les bottes, les gants et le gilet tactique, tous à qualité normale.
- Les pantalons olive, noirs et désert sont générés.
- La veste reste facultative et, lorsqu'elle est présente, reprend toujours la variante du pantalon du même pawn.
- Des starters avec et sans veste, ainsi qu'avec et sans casque, sont générés.
- Le tee-shirt, la veste et le gilet coexistent sans remplacement silencieux ni conflit de couche visible dans le périmètre testé.
- Les nouvelles pièces sont correctes dans les orientations et morphologies testées.
- Le scénario fournit un fusil d'assaut, un pistolet-mitrailleur, un pistolet automatique et un fusil à pompe, ainsi que les fournitures habituelles.
- Aucun lot de quatre casques n'est laissé au sol.
- Un scénario vanilla conserve ses règles de sélection et ses vêtements habituels.
- Sauvegarde et rechargement conservent les vêtements et identités générés.
- `Player.log` est propre pour le périmètre validé.
- Décision finale : conserver la révision fonctionnelle `r4` sans correctif supplémentaire.

## Contrôle de cohérence validé

```powershell
./tools/check-project-consistency.cmd `
    -ExpectedVersion 0.3.21-dev `
    -ExpectedBackstoryCount 83
```

Résumé validé :

```text
Project consistency check passed.
Version: 0.3.21-dev
Assembly: 0.3.21.0
Backstories: 83
```

Le code de sortie est `0` et aucun fichier Markdown ne contient de tabulation littérale.

## Loadout SG-team validé

Équipement obligatoire par starter :

```text
1 tee-shirt vanilla en tissu
1 pantalon SG olive, noir ou désert
1 paire de bottes tactiques SG
1 paire de gants tactiques SG
1 gilet tactique SG
```

Équipement facultatif :

```text
veste SG correspondant à la variante du pantalon
casque de terrain SG
```

La future casquette SG doit être ajoutée au slot de couvre-chef existant, sans nouvelle branche C# spécifique au scénario.

## Armes et fournitures validées

```text
1 fusil d'assaut
1 pistolet-mitrailleur
1 pistolet automatique
1 fusil à pompe
4 sacs de couchage
30 repas de survie
20 médicaments industriels
300 acier
150 bois
20 composants industriels
120 tissu
80 cuir ordinaire
```

Les armes humaines vanilla constituent le choix normal du scénario. Une future compatibilité avec des mods d'armes devra prendre la forme de patchs facultatifs, pas d'une gamme humaine dupliquée uniquement pour GateRim SG-1.

## Régressions durables

- Conserver le loadout limité à `PawnGenerationContext.PlayerStarter` et au marqueur de scénario prévu.
- Vérifier qu'un nouveau slot obligatoire ou facultatif n'entre pas en conflit avec les couches déjà portées.
- Vérifier tous les `variantKey` lorsqu'une variante est ajoutée à un groupe partagé.
- Rejouer un échantillon suffisant pour observer les options facultatives sans exiger un ratio exact sur un petit nombre de pawns.
- Conserver les anciens treillis combinés pour les sauvegardes, mais ne pas les réintroduire dans le starter.
- Rejouer un scénario vanilla à chaque évolution des restrictions ou du loadout culturel.
- Utiliser `/` dans les chemins PowerShell relatifs écrits en Markdown et conserver le test des tabulations littérales.
