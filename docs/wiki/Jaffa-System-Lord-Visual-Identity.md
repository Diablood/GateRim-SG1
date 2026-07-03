# Identité visuelle des Jaffa Goa'uld

> Statut : Prototype intrinsèque persistant  
> Version d'introduction : 0.1.73-dev  
> Stockage intrinsèque dédié : 0.1.74-dev

## Présentation

Les Jaffa rattachés à un domaine Goa'uld reçoivent automatiquement une
marque frontale noire générique.
Depuis `0.1.74-dev`, cette marque n'est plus représentée par un gène cosmétique
technique : elle est conservée comme une donnée intrinsèque propre au personnage.

## Nature de la marque

Une fois attribuée, la marque est indépendante du xenotype, de la faction
actuelle et de l'équipement. Elle fonctionne comme un insigne culturel ou une
scarification :

```text
aucune pièce d'inventaire
aucune recette
aucune catégorie de stockage
aucune protection
aucune statistique d'armure
aucun butin retirable
aucun gène visible
```

Elle reste distincte du casque rétractable et de l'ensemble modulaire Jaffa.

## Hiérarchie visuelle prévue

```text
Jaffa ordinaires      -> marque noire
élites sélectionnées  -> marque argentée
Premier Primat        -> marque dorée embossée
```

Un garde lourd n'est pas automatiquement un Premier Primat.

## Attribution manuelle

Les outils développeur permettent d'appliquer ou retirer une marque sur
n'importe quel personnage. Un humain ou un Goa'uld infiltré peut donc recevoir une
fausse marque sans acquérir la génétique Jaffa.

## Persistance et migration

La marque survit aux sauvegardes. Les anciens gènes techniques des prototypes
précédents sont convertis automatiquement en données intrinsèques lorsqu'un
personnage concerné est rencontré.

## Limites

Les visuels actuels restent temporaires. Les variantes propres aux futurs
Grands Maîtres, leur placement définitif et l'attribution automatique des rangs
supérieurs seront traités ultérieurement.

Depuis `0.2.2-dev`, les [Jaffa libres](Free-Jaffa-Faction) restent sans marque
imposée. Les colonies visibles et les rares raids naturels Goa'uld sont actifs
depuis `0.2.1-dev`. Depuis `0.3.54-dev`, ces raids peuvent employer trois
doctrines ; les marchands restent désactivés.
