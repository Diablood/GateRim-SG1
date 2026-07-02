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

Une nouvelle planète génère :

```text
1 faction Goa'uld hostile par défaut
un nombre limité de colonies visibles
```

Le joueur peut ajouter manuellement plusieurs factions Goa'uld s'il souhaite
représenter séparément plusieurs domaines de Grands Maîtres.

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

## Premier raid naturel

Depuis `0.2.1-dev`, un incident rare peut lancer un assaut direct :

[Raids naturels de Jaffa Goa'uld](Goauld-Jaffa-Natural-Raid).

Cette première activation conserve :

```text
ImmediateAttack
aucun vol opportuniste
aucun enlèvement opportuniste
```

## Doctrines encore contrôlées

Les raids d'enlèvement et de destruction restent disponibles uniquement pour
les tests développeur. Ils seront activés naturellement plus tard après une
passe d'équilibrage séparée.

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
