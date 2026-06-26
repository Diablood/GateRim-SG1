# Jaffa libres

> Statut : Première base mondiale jouable
> Première version : 0.2.2-dev

## Présentation

Les **Jaffa libres** sont des communautés indépendantes libérées de la
domination Goa'uld.

```text
Jaffa libres
```

Une nouvelle planète génère une faction visible avec un nombre limité de
colonies.

Dans l'écran de création du monde, son résumé indique désormais :

```text
xénotype : Jaffa (100 %)
```

Une faction est proposée par défaut. Le joueur peut en ajouter davantage
manuellement s'il souhaite représenter plusieurs communautés Jaffa libres.

## Noms des factions et des colonies

Depuis `0.3.45-dev`, l'entrée de création du monde reste présentée comme
`Jaffa libres`, mais chaque faction nouvellement générée reçoit un nom collectif
propre. La grammaire combine alliances, conseils, communautés et thèmes de
résistance pour produire `216` possibilités.

Exemples possibles :

```text
Alliance des clans libres
Conseil de la résistance Jaffa
Fraternité des hôtes libérés
Pacte contre les Maîtres
```

Les nouvelles colonies n'utilisent plus les noms génériques des factions
outlander. Une seconde grammaire combine types de colonies, thèmes de
libération et, plus rarement, des ordinaux RP. Elle offre `600` résultats.

La casse française reste naturelle :

```text
Refuge des affranchis
Citadelle des clans libres
Premier abri des Maîtres déchus
Deuxième cité de la chaîne brisée
```

Les factions et colonies déjà présentes dans une sauvegarde conservent leurs
noms existants. Le changement s'applique uniquement aux nouvelles générations.

Depuis `0.3.47-dev`, les nouveaux chefs de faction reçoivent directement pendant leur génération un nom Jaffa libre formel composé d'un nom personnel et d'un nom de clan. Le nom personnel reste utilisé comme nom court, tandis que le nom complet apparaît dans les interfaces diplomatiques. Cette structure évite les noms humains vanilla et permet à plusieurs chefs d'être générés sans conflit d'unicité. Les chefs déjà sérialisés dans une ancienne sauvegarde conservent leur nom existant ; les noms des factions et des colonies ne sont pas modifiés par cette attribution.

Depuis `0.3.50-dev`, l'entrée de faction utilise une silhouette Jaffa libre
dédiée avec lance et chaînes brisées. Le PNG reste blanc/alpha : RimWorld
applique ensuite la couleur de faction, ce qui conserve les variations de
teinte lorsqu'un joueur ajoute plusieurs communautés Jaffa libres.

## Relations initiales

Les Jaffa libres commencent avec une relation neutre envers :

```text
expédition du SGC
```

Ils restent ennemis des :

```text
Domaines des Grands Maîtres Goa'uld
```

## Serviteurs et défenseurs

Les colonies utilisent actuellement :

```text
guerrier Jaffa libre
garde Jaffa libre
```

Ces profils reçoivent :

- la lignée Jaffa ;
- un Prim'ta initial ;
- un bâton Ma'Tok ;
- une armure modulaire ;
- un casque Jaffa rétractable.

## Différence visuelle importante

Les Jaffa libres ne reçoivent pas automatiquement la marque frontale d'un
domaine Goa'uld.

Cette absence de marque imposée permet de les distinguer des serviteurs des
Grands Maîtres tout en conservant la possibilité d'appliquer manuellement une
marque dans un scénario particulier.

## Visiteurs pacifiques

Depuis `0.2.6-dev`, de rares groupes de
[visiteurs Jaffa libres pacifiques](Free-Jaffa-Peaceful-Visitors) peuvent
apparaître naturellement près de la colonie du joueur.

Ces premiers voyageurs sont armés, mais ne sont pas hostiles ni marchands.

## Commerce

Depuis `0.3.43-dev`, les communautés Jaffa libres participent aussi aux flux
commerciaux ordinaires de RimWorld : colonies visitables, visiteurs
marchands, demandes par communicateur et convois spécialisés dans le
ravitaillement des clans libres lorsque les relations le permettent.

Le [réseau commercial Jaffa libre](Free-Jaffa-Trade) utilise un marchand Jaffa
dédié, escorté par des guerriers et gardes Jaffa libres. Son stock se concentre sur les provisions, les ressources stratégiques et le matériel militaire, avec une réserve d'argent limitée.

## Aide militaire

Depuis `0.3.44-dev`, une faction Jaffa libre alliée peut répondre à une
[demande d'aide militaire](Free-Jaffa-Military-Aid) transmise par une console
de communication alimentée.

Le coût de bonne volonté, le délai, l'arrivée et le départ des renforts suivent
les règles ordinaires de RimWorld. La force reste composée de guerriers et de
gardes Jaffa libres existants.

## Limites actuelles

Restent prévus pour plus tard :

- profils civils ou diplomatiques spécialisés ;
- quêtes ;
- réactions diplomatiques plus détaillées.

Les textures définitives et les concept arts semi-réalistes destinés au wiki
seront produits lors d'une future passe graphique dédiée.
