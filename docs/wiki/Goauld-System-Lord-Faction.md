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
1 faction Goa'uld hostile par défaut
un nombre limité de colonies visibles
```

Le joueur peut ajouter manuellement plusieurs factions Goa'uld s'il souhaite
représenter séparément plusieurs domaines de Grands Maîtres.

La présence mondiale utilise volontairement un poids de génération de colonies
réduit. Les Goa'uld sont visibles sans saturer la carte.

## Résumé de xénotype provisoire

Dans l'écran de création du monde, la faction indique actuellement :

```text
xénotype : Jaffa (100 %)
```

Ce résumé reste une limite de l'interface vanilla : elle affiche les
xenotypes, pas les états parasitaires acquis. Depuis `0.2.3-dev`, des profils
d'hôtes Goa'uld minoritaires existent réellement dans les colonies et parmi
les dirigeants, avec un symbiote persistant.

## Grand Maître Goa'uld

Depuis `0.2.3-dev`, chaque domaine visible génère un véritable :

```text
Grand Maître Goa'uld
```

Le dirigeant reçoit un symbiote adulte actif avec une identité persistante. Le
représentant provisoire `commandant Jaffa de domaine` n'est plus utilisé.

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
