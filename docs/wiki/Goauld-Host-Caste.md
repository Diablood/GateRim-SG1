# Caste des hôtes Goa'uld

> Statut : Première base jouable  
> Première version : 0.2.3-dev

## Présentation

Les domaines Goa'uld génèrent désormais de véritables hôtes persistants.

Deux profils existent :

```text
Goa'uld
Grand Maître Goa'uld
```

Le profil ordinaire est volontairement nommé simplement **Goa'uld**. Il ne
représente pas un rang de surveillant distinct : il s'agit d'un symbiote adulte
contrôlant un hôte humanoïde.

## Architecture biologique

Un hôte généré naturellement reste un corps humain. Son état Goa'uld est acquis
pendant sa vie :

```text
corps humanoïde
+
symbiote Goa'uld adulte actif
+
identité persistante du symbiote
```

Le mod n'impose donc pas artificiellement le xenotype prototype `hôte Goa'uld`
aux nouveaux personnages. Cette décision préservera la cohérence lors des
futures extractions et des futurs transferts entre hôtes.

## Dirigeants

Chaque faction des :

```text
Domaines des Grands Maîtres Goa'uld
```

génère désormais un véritable :

```text
Grand Maître Goa'uld
```

Le dirigeant provisoire `commandant Jaffa de domaine` n'est plus utilisé.

Depuis `0.3.48-dev`, un Grand Maître nouvellement généré porte immédiatement
un nom formel Goa'uld visible dans l'écran de création du monde. Ce nom est
l'identité du symbiote. Le système persistant conserve en parallèle un nom
humain distinct pour l'hôte, afin qu'une extraction prise en charge puisse
restaurer son identité sans perdre celle du Goa'uld.

## Colonies

Les colonies Goa'uld restent majoritairement défendues par des Jaffa.

Chaque groupe généré pour une colonie utilise désormais des plafonds lisibles :

```text
jusqu'à 7 guerriers Jaffa par groupe
jusqu'à 2 gardes Jaffa par groupe
jusqu'à 1 Goa'uld par groupe
```

Une carte de ville peut résoudre plusieurs groupes. Il reste donc normal de
rencontrer deux Goa'uld dans une colonie plus importante, tant qu'ils restent
minoritaires face aux Jaffa.

## Tenue provisoire

En attendant la création de vêtements Goa'uld dédiés, les hôtes générés
reçoivent une tenue vanilla temporaire :

```text
pantalon
chemise à col
cache-poussière
```

Ils ne doivent donc plus apparaître nus dans les villes.

## Santé initiale

Lors de l'initialisation du symbiote adulte, certaines affections biologiques
chroniques déjà présentes sur le corps généré sont retirées, notamment le
lumbago, la fragilité, les cataractes ou la perte auditive.

Les cicatrices, parties du corps manquantes et blessures de combat ordinaires
ne sont pas effacées.

## Raids directs

Les raids directs naturels et contrôlés restent composés de Jaffa uniquement.
Les Goa'uld ne sont pas encore envoyés comme combattants ordinaires dans les
assauts.

## Limite de l'écran de création du monde

Le résumé vanilla conserve provisoirement :

```text
xénotype : Jaffa (100 %)
```

Cette interface affiche les xenotypes, pas les états parasitaires acquis par
un symbiote persistant. Les hôtes Goa'uld générés sont néanmoins réels dans les
colonies et chez les dirigeants.

## Étapes suivantes

Restent prévus :

- backstories hors-monde dédiées ;
- vêtements Goa'uld propres ;
- extraction des hôtes actifs ;
- enrichissement de l'identité sociale ;
- icônes thématiques des factions.
