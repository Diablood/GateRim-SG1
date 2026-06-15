# Feuille de route des interactions Tok'ra

> Statut : feuille de route
> Version : 0.2.27-dev

Cette page résume la direction prévue pour les interactions avec les Tok'ra.
Depuis `0.2.27-dev`, la première aide défensive active existe via le communicateur fiable.

## Déjà en place

```text
faction Tok'ra masquée
confiance Tok'ra persistante
caches médicaux modestes
signaux de planque
pistes de planque
planques temporaires visitables
contact Tok'ra pacifique et non marchand
briefing médical unique par contact
XP Médecine selon la confiance
piste de suivi aux paliers coopératif et fiable
```

Les Tok'ra restent une faction clandestine : pas de colonie publique, pas de
commerce classique, pas de recrutement et pas de renfort militaire permanent.

## Sens des paliers

```text
méfiante     : contact très limité
neutre       : aide médicale modeste et prudente
coopérative  : coordination via planques et pistes de suivi
fiable       : base pour les soutiens avancés
```

## Directions prévues

### Soutien médical

Les Tok'ra sont d'abord une faction de soutien médical et de renseignement.
Les prochaines récompenses fiables devraient donc rester limitées et rares :
conseils avancés, petit cache médical ou accès conditionnel à une aide plus
spécialisée.

### Réseau de planques

Les signaux, pistes et planques forment déjà une boucle courte. Une future
mini-questline pourra s'appuyer dessus : signal, planque, contact, puis choix de
récompense ou de coopération.

### Communicateur Tok'ra

Une piste importante est un communicateur sécurisé, débloqué seulement à haute
confiance. Il pourrait permettre de demander ponctuellement :

```text
une piste de planque
un conseil médical
une diversion défensive pendant une attaque active
```

### Aide militaire

L'aide militaire Tok'ra doit rester rare et défensive. Les options envisagées
sont plutôt :

```text
avertissement tactique avant une menace
sabotage discret ou perturbation d'un raid (premier prototype en `0.2.27-dev`)
petit groupe temporaire de 1 à 3 agents en cas de crise
extraction ou aide d'urgence limitée
```

Ce ne doit pas devenir un bouton de renfort allié classique.

## Extensions futures par culture

Plus tard, les Tok'ra pourront réagir différemment aux grands profils du mod :

```text
membres d'équipe SG
Jaffa libres
Jaffa de domaine Goa'uld
hôtes Goa'uld actifs
hôtes Tok'ra actifs
Grands Maîtres Goa'uld
```

Ces différences devraient rester contextuelles : bonus, méfiance, dialogues ou
conditions plus strictes, plutôt que des blocages absolus partout.

## Suite logique

La base du communicateur sécurisé est posée en `0.2.26-dev`. Elle ouvre un canal
au palier fiable, mais ne donne pas encore de récompense directe. Les prochaines
évolutions pourront y raccorder progressivement le soutien médical, les pistes
de planque et une première aide défensive rare.

## Mise à jour 0.2.26-dev

Un premier communicateur sécurisé Tok'ra est disponible comme bâtiment alimenté.
Il sert de point d'accès visible pour les futurs soutiens, mais son canal ne
s'ouvre qu'au palier de confiance fiable.

Il ne donne pas encore de soin, d'objet, de piste, d'aide militaire, de commerce,
de recrutement ou de quête.


## Interaction ajoutée en 0.2.34-dev

Le communicateur fiable permet désormais de transmettre un débriefing
opérationnel Tok'ra. Cette interaction reste non matérielle : elle accorde un
petit retour formatif à l'opérateur et prépare une future boucle RP de confiance
ou de mission sans livrer d'objet ni appeler de renfort.
