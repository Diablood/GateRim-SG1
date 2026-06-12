# Domaines des Grands Maîtres Goa'uld

> Statut : Première base mondiale jouable  
> Fondation technique : 0.1.61-dev  
> Présence mondiale et premier raid naturel : 0.2.1-dev

## Présentation

Les territoires contrôlés par les Grands Maîtres Goa'uld sont désormais
représentés sur la carte du monde par une faction hostile visible :

```text
Domaines des Grands Maîtres Goa'uld
```

Cette faction unique sert d'abstraction pratique RimWorld pour plusieurs
domaines Goa'uld rivaux.

## Présence mondiale

Une nouvelle planète génère :

```text
1 faction Goa'uld hostile
un nombre limité de colonies visibles
```

La présence mondiale utilise volontairement un poids de génération de colonies
réduit. Les Goa'uld sont visibles sans saturer la carte.

## Hostilité

La faction est ennemie permanente de l'expédition du SGC.

Les marchands, l'aide militaire et les sites de quête Goa'uld restent
désactivés.

## Serviteurs Jaffa

Les colonies et groupes de combat utilisent actuellement :

```text
guerrier Jaffa au service des Goa'uld
garde Jaffa au service des Goa'uld
```

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
