# Identité visuelle des Jaffa Goa'uld

> Statut : Marques intrinsèques finales  
> Version d'introduction : 0.1.73-dev  
> Stockage intrinsèque dédié : 0.1.74-dev  
> Visuels définitifs : 0.3.91-dev

## Présentation

Les Jaffa rattachés à un domaine Goa'uld reçoivent automatiquement une
marque frontale noire générique. Depuis `0.1.74-dev`, cette marque n'est plus
représentée par un gène cosmétique technique : elle est conservée comme une
donnée intrinsèque propre au personnage.

Depuis `0.3.91-dev`, les trois rangs utilisent le même symbole compact
d'Apophis. La marque reste limitée au front et n'est visible que lorsque le
personnage est orienté vers le sud.

## Marques finales

### Jaffa ordinaire — marque noire

<img src="images/GenericJaffaForeheadMark_south.png" width="384" alt="Marque frontale Jaffa noire">

La marque noire est attribuée automatiquement aux Jaffa ordinaires rattachés
à un domaine Goa'uld.

### Élite sélectionnée — marque argentée

<img src="images/GenericSilverJaffaForeheadMark_south.png" width="384" alt="Marque frontale Jaffa argentée">

La variante argentée distingue les élites sélectionnées lorsqu'elle leur est
attribuée.

### Premier Primat — marque dorée

<img src="images/GenericGoldJaffaForeheadMark_south.png" width="384" alt="Marque frontale Jaffa dorée">

La variante dorée est réservée au rang de Premier Primat.

Les trois variantes partagent exactement la même géométrie. Seule la matière
visuelle change entre le noir, l'argent et l'or.

## Nature de la marque

Une fois attribuée, la marque est indépendante du xénotype, de la faction
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

## Rendu selon l'orientation

Chaque famille conserve quatre textures `128×128` :

```text
South -> symbole visible
North -> texture entièrement transparente
East  -> texture entièrement transparente
West  -> texture entièrement transparente
```

Cette restriction évite qu'une marque frontale apparaisse artificiellement
sur le côté ou au-dessus de la tête.

## Attribution manuelle

Les outils développeur permettent d'appliquer ou retirer une marque sur
n'importe quel personnage. Un humain ou un Goa'uld infiltré peut donc recevoir
une fausse marque sans acquérir la génétique Jaffa.

## Persistance

La marque survit aux sauvegardes et reste retirable. Les anciens gènes
techniques de migration ont été supprimés en `0.3.90-dev`; les marques
actuelles reposent uniquement sur les données intrinsèques persistantes du
personnage.

## Limites actuelles

Les variantes noire, argentée et dorée sont visuellement définitives. Les
futures marques propres à des domaines Goa'uld nommés et l'attribution
automatique des rangs supérieurs restent des travaux distincts.

Depuis `0.2.2-dev`, les [Jaffa libres](Free-Jaffa-Faction) restent sans marque
imposée. Les colonies visibles et les rares raids naturels Goa'uld sont actifs
depuis `0.2.1-dev`. Depuis `0.3.54-dev`, ces raids peuvent employer trois
doctrines ; les marchands restent désactivés.
