# Idées à revoir pour plus tard

Ce fichier conserve des pistes de conception qui méritent peut-être une étude ultérieure, mais qui ne constituent ni des jalons décidés, ni des fonctionnalités promises, ni un ordre de priorité.

Une entrée ne doit être transformée en jalon qu'après une nouvelle discussion, un cadrage explicite et une vérification de sa compatibilité avec les systèmes déjà publiés.

## Évolution du cycle biologique des reines Goa'uld

**Statut : extension non planifiée du cycle déjà jouable.**

Le cycle reine, symbiote immature et maturation assistée est disponible depuis
les jalons `0.1.57-dev` à `0.2.10-dev`. Une future reprise devra décider si les
reines restent hostless, exigent un hôte ou acceptent les deux formes, puis
définir leur infrastructure et leurs interactions avec domaines Goa'uld,
Tok'ra, Jaffa libres, commerce, raids et missions.

La reprise ne doit pas supprimer l'acquisition actuelle avant qu'une boucle de
remplacement complète, équilibrée et testable existe.

## Réactions Tok'ra selon les cultures et factions

**Statut : idée narrative non planifiée.**

Une future passe peut différencier les réactions Tok'ra envers Tau'ri, Jaffa
libres, Jaffa de domaine, hôtes Goa'uld, autres Tok'ra et Grands Maîtres. Ces
différences devraient modifier contexte, confiance ou dialogue sans créer de
verrous absolus ni rouvrir automatiquement le pool fermé des huit opérations.

## Sarcophage Goa'uld

**Statut : technologie lourde distincte non planifiée.**

Le sarcophage ne doit pas devenir une fonction supplémentaire du kara kesh ou
du bracelet de guérison. Une future étude devra traiter séparément bâtiment,
acquisition, alimentation, puissance de soin ou de résurrection, effets
psychologiques, dépendance, usage par l'IA et contre-jeu. Aucun soin actuel ne
doit anticiper cette technologie ni restaurer les morts ou parties manquantes.

## Passe de compatibilité avec les DLC optionnels

**Statut : idée non planifiée à réexaminer après stabilisation du contenu de base.**

### Piste à conserver

Prévoir une passe globale de compatibilité avec les DLC RimWorld autres que
Biotech lorsque les systèmes concernés sont suffisamment stables. Odyssey doit
notamment être audité pour les environnements sans atmosphère : les armures
Jaffa et les autres équipements fermés pertinents pourraient recevoir des
statistiques adaptées, par exemple une résistance ou une protection contre le
vide, si les mécanismes et l'équilibrage vanilla le justifient.

### Garde-fous

- Ne pas rendre un DLC optionnel obligatoire pour utiliser le contenu GateRim
  SG-1 de base.
- Appliquer les ajouts par patchs conditionnels ou code isolé uniquement quand
  le DLC correspondant est actif.
- Auditer l'ensemble des équipements, races, factions, incidents, recherches
  et cartes concernés plutôt que corriger seulement une armure au cas par cas.
- Comparer les valeurs aux équipements vanilla du DLC avant d'attribuer des
  résistances liées au vide, à l'atmosphère ou à d'autres environnements.
- Conserver des contreparties cohérentes de masse, protection, coût, recherche
  et disponibilité afin de ne pas transformer l'équipement Jaffa en solution
  universelle.

### Limite de cette entrée

Aucun patch Odyssey, changement d'armure, prérequis, dépendance ou jalon n'est
acté par cette note. Le périmètre devra être rediscuté à partir des DLC
réellement pris en charge au moment de la passe.

## Confinement d'un symbiote Goa'uld extrait vivant

**Statut : idée non planifiée à réexaminer.**

### Contexte actuel

Depuis `0.3.41-dev`, l'extraction chirurgicale d'un hôte Goa'uld actif libère le même symbiote vivant sous anesthésie vanilla temporaire. Le Goa'uld conserve son identité et son allégeance hostile. L'anesthésie donne au joueur le temps de sécuriser ou d'éliminer la menace, mais elle ne constitue pas un confinement durable et le symbiote redevient dangereux à son réveil.

Le jalon actuel ne tue pas automatiquement le symbiote et n'ajoute aucune solution spéciale pour le garder prisonnier.

### Piste à étudier

Évaluer un éventuel dispositif de confinement dédié pour un symbiote adulte vivant. Un tel système pourrait, selon le cadrage retenu, servir de support à une remise aux Tok'ra, une mission, un interrogatoire, une étude ou une autre conséquence narrative.

### Questions à résoudre avant tout jalon

- Le confinement apporte-t-il un choix de jeu suffisamment intéressant par rapport à l'élimination immédiate ?
- Faut-il un bâtiment spécialisé, un conteneur transportable ou les deux ?
- Quelles contraintes empêchent le stockage sans risque : alimentation, entretien, température, surveillance, durée maximale ou tentative d'évasion ?
- Que se passe-t-il si le dispositif est détruit, désalimenté, déplacé ou abandonné ?
- Une remise aux Tok'ra doit-elle dépendre du communicateur, de la confiance ou d'une opération active ?
- Quelles récompenses ou conséquences restent équilibrées sans transformer chaque extraction en source automatique de ressources ou de confiance ?
- Comment préserver l'identité, l'allégeance, les données d'hôte et la sauvegarde/recharge du symbiote confiné ?
- Faut-il des réactions sociales, morales ou liées à Ideology, et seulement si cette intégration devient réellement pertinente ?
- Le bassin de conservation existant est-il inadapté à un Goa'uld adulte hostile ? Par défaut, ne pas le réutiliser sans une justification mécanique et RP solide.

### Limite de cette entrée

Aucun Def, bâtiment, composant, interaction Tok'ra, recherche ou mission n'est prévu par cette note. Elle conserve uniquement le problème de conception afin qu'il puisse être réévalué plus tard sans imposer un futur jalon.

## Fonctions avancées possibles du kara kesh

**Statut : idées exploratoires non planifiées à réexaminer séparément.**

### Contexte actuel

Les jalons `0.3.57-dev`, `0.3.59-dev` et `0.3.60-dev` ont établi le kara kesh, son bouclier personnel, son onde cinétique et son attaque neurale. Les pistes ci-dessous ne font partie d'aucun jalon décidé et ne doivent pas être interprétées comme une extension promise du même objet.

### Pistes à conserver

- Torture d'un pawn à terre.
- Mise à mort ou dégâts continus.
- Contrôle mental.
- Ordres à distance.
- Manipulation d'objets ou de portes.
- Action sur plusieurs cibles simultanément.

### Questions à résoudre avant tout jalon

- Chaque fonction apporte-t-elle un gameplay distinct, lisible et équilibrable dans RimWorld ?
- Faut-il conserver ces fonctions sur le kara kesh ou les répartir entre plusieurs technologies Goa'uld ?
- Quelles restrictions de portée, d'énergie, de cooldown, de ligne de vue et d'éligibilité biologique seraient nécessaires ?
- Comment éviter qu'un même porteur cumule trop de fonctions offensives, défensives et utilitaires ?
- Quelle utilisation par l'IA reste compréhensible et équitable pour le joueur ?
- Quelles fonctions exigeraient des règles particulières pour les pawns à terre, prisonniers, alliés, neutres ou non consentants ?
- Les interactions avec les quêtes, les factions, la diplomatie et Ideology apportent-elles une valeur suffisante pour justifier leur complexité ?
- Une action multi-cible doit-elle être une amélioration rare, une capacité de rang supérieur ou une technologie distincte ?

### Limite de cette entrée

Aucun Def, code, recherche, recette, gizmo, comportement IA ou jalon n'est prévu par cette note. Chaque piste doit faire l'objet d'une nouvelle discussion et d'un cadrage indépendant avant toute intégration à la roadmap active.
