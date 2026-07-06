# Domaines des Grands Maîtres Goa'uld

> Statut : Présence mondiale jouable
>
> Fondation technique : 0.1.61-dev
>
> Présence mondiale et premier raid naturel : 0.2.1-dev
>
> Noms mondiaux dédiés : 0.3.46-dev
>
> Noms culturels des Grands Maîtres : 0.3.48-dev
>
> Trois domaines proposés et garde-fous territoriaux : 0.3.83-dev
>
> Première prise territoriale bornée : 0.3.84-dev, révision finale r1 validée et publiée

## Présentation

Les territoires contrôlés par les Grands Maîtres Goa'uld sont représentés
sur la carte du monde par des factions hostiles visibles. L'entrée de création
du monde conserve le libellé générique :

```text
Domaines des Grands Maîtres Goa'uld
```

Chaque faction réellement générée reçoit toutefois un nom de domaine propre,
par exemple :

```text
Dominion du trône d'or
Empire de la couronne du serpent
Cour de l'œil éternel
```

Ces noms décrivent le pouvoir ou le culte du domaine sans imposer un Grand
Maître canon ni prétendre correspondre au dirigeant généré séparément.

## Présence mondiale

Une nouvelle planète propose dans la liste vanilla :

```text
3 factions Goa'uld hostiles par défaut
un nombre limité de colonies visibles par domaine
```

Le joueur peut réduire ce nombre lorsque la limite totale de factions vanilla
importe, ou ajouter davantage de domaines. Le mod ne recrée jamais une faction
retirée. La simulation territoriale nécessite au moins deux domaines non vaincus
possédant chacun une colonie permanente ; avec un seul domaine, seuls ces effets
territoriaux sont suspendus.

## Diplomatie et territoire

Depuis `0.3.83-dev`, les relations entre deux domaines sont réconciliées avec le
système vanilla : conflit ouvert signifie hostile, alliance signifie allié, et
neutralité, rivalité ou trêve utilisent la relation neutre. La définition de
faction autorise la bonne volonté uniquement entre deux instances Goa'uld ; elle
maintient une hostilité permanente envers le joueur et toutes les factions
extérieures. La synchronisation technique n'envoie ni message ni lettre.

Les garde-fous publiés en `0.3.83-dev` protègent la dernière colonie de chaque
domaine, bloquent les mondes trop pauvres, ralentissent l'expansion avec la taille
du domaine et empêchent de dépasser automatiquement `75 %` avec deux domaines ou
`50 %` à partir de trois domaines.

La version publiée `0.3.84-dev`, validée en révision finale `r1`, ajoute une première prise réelle mais bornée.
Sous **Commandement SG-1**, une paire en conflit ouvert peut transférer la seule
propriété d'une colonie existante après une réservation de `1–2` jours. La
colonie conserve son nom, son identifiant et sa tuile ; aucune colonie ou faction
n'est créée ou détruite. Une carte chargée, la présence du joueur ou une quête
active protège également la colonie.

## Doctrine stratégique

Depuis `0.3.64-dev`, chaque faction Goa'uld reçoit une
[doctrine stratégique persistante](Goauld-Domain-Doctrines) :

- conquête ;
- asservissement ;
- terre brûlée.

Cette préférence appartient au domaine, pas à son Grand Maître actuel. Elle
reste donc identique si le dirigeant est remplacé. Elle ne rend pas les raids
plus fréquents ou plus puissants : elle modifie seulement la préférence entre
assaut direct, enlèvement et destruction lorsque ces doctrines sont déjà
admissibles.

La présence mondiale utilise volontairement un poids de génération de colonies
réduit. Les Goa'uld sont visibles sans saturer la carte. Les nouvelles colonies
utilisent également des noms dédiés, avec des formes comme `Temple du trône
d'or`, `Premier sanctuaire de la Porte sacrée` ou `Deuxième pyramide du soleil
noir`, au lieu des noms pirates vanilla.

## Résumé des xénotypes et des castes

Dans l'écran de création du monde, la faction indique actuellement :

```text
xénotype : Jaffa (100 %)
```

L'interface vanilla affiche les xénotypes, pas les états parasitaires acquis.
Depuis `0.3.52-dev`, l'infobulle Goa'uld complète donc ce pourcentage sans le
modifier : elle précise que les serviteurs Jaffa forment la population
dominante, que les hôtes Goa'uld sont une minorité des colonies et que le
dirigeant est lui-même un hôte Grand Maître. La possession reste un état
persistant acquis, jamais un xénotype germinal ajouté pour les besoins de l'UI.

## Grand Maître Goa'uld

Depuis `0.2.3-dev`, chaque domaine visible génère un véritable :

```text
Grand Maître Goa'uld
```

Le dirigeant reçoit un symbiote adulte actif avec une identité persistante. Le
représentant provisoire `commandant Jaffa de domaine` n'est plus utilisé.

Depuis `0.3.48-dev`, les nouveaux dirigeants reçoivent avant le choix de la
tuile un nom formel Goa'uld, par exemple `Amonaris Kheper`. Le premier élément
reste le nom court du symbiote ; le second représente une maison ou un nom de
trône, et non un patronyme humain.

Au démarrage de la partie, cette identité visible devient le nom persistant du
symbiote. Un autre nom humain hors-monde est conservé séparément pour le corps
hôte et peut être restauré si une extraction prise en charge retire le Goa'uld.
Le nom du chef reste indépendant du nom généré pour son domaine.

Consulte [Caste des hôtes Goa'uld](Goauld-Host-Caste).

## Hostilité

La faction est ennemie permanente de l'expédition du SGC.

Les marchands, l'aide militaire et les sites de quête Goa'uld restent
désactivés.

## Serviteurs Jaffa

Les raids directs utilisent toujours :

```text
guerrier Jaffa au service des Goa'uld
garde Jaffa au service des Goa'uld
```

Les groupes générés dans les villes utilisent des plafonds lisibles :

```text
jusqu'à 7 guerriers Jaffa par groupe
jusqu'à 2 gardes Jaffa par groupe
jusqu'à 1 Goa'uld par groupe
```

Une même carte de ville peut résoudre plusieurs groupes. Deux Goa'uld dans une
colonie restent donc possibles sans remettre en cause leur statut minoritaire.

Les serviteurs reçoivent automatiquement :

- leur lignée Jaffa ;
- un Prim'ta initial ;
- un bâton Ma'Tok ;
- leur armure modulaire ;
- leur casque rétractable ;
- leur marque frontale intrinsèque.

## Raid naturel

Depuis `0.2.1-dev`, un incident rare peut lancer un raid :

[Raids naturels de Jaffa Goa'uld](Goauld-Jaffa-Natural-Raid).

Depuis `0.3.54-dev`, cet incident unique choisit entre assaut direct,
enlèvement et destruction selon les points de menace, le nombre de colons et
la richesse bâtie. Le délai commun reste inchangé et l'assaut direct demeure
la doctrine dominante.

## Identité de domaine

Le profil piloté par les Defs introduit en `0.1.74-dev` reste associé à la
faction. Il prépare les marques intrinsèques noire, argentée et dorée selon le
rang, sans encore multiplier les factions mondiales.

Consulte [Fondation d'identité des domaines Goa'uld](Goauld-System-Lord-Domain-Identity).

## Identité visuelle mondiale

Depuis `0.3.50-dev`, l'entrée de faction utilise une silhouette Goa'uld dédiée
avec pyramide, géométrie impériale et serpent. Le PNG reste blanc/alpha :
RimWorld applique ensuite la couleur de faction, ce qui conserve les variations
de teinte lorsqu'un joueur ajoute plusieurs domaines Goa'uld.
