# Double identité Tok'ra

> Statut : basculement carte / caravane validé ; identités distinctes des Tok'ra générés déjà fusionnés ajoutées dans `0.3.13-dev`

Un Tok'ra réunit deux personnes conscientes dans un même corps : l'hôte et le
symbiote. GateRim SG-1 conserve donc leurs deux identités au lieu d'effacer le
nom ou le parcours de l'une après l'implantation.

## Identités conservées

Lorsqu'un symbiote Tok'ra rejoint un hôte, le mod enregistre séparément :

- le nom exact de l'hôte ;
- l'enfance et la carrière de l'hôte ;
- le nom propre du symbiote ;
- le parcours culturel du symbiote ;
- la personnalité actuellement active ;
- l'hôte actuel et l'hôte précédent dans les données techniques de transfert.

Le symbiote conserve la même identité lors de la conversion en symbiose active,
d'une sauvegarde, d'une extraction et d'une nouvelle implantation.

## Tok'ra générés déjà fusionnés

Certains Tok'ra sont créés directement par le jeu comme des hôtes volontaires déjà
fusionnés, par exemple lors d'un incident, d'une visite, pour un chef de faction
ou avec l'action développeur `Spawn pawn`. Dans ce cas, aucune implantation
historique n'existait auparavant pour fournir une identité d'hôte.

GateRim SG-1 génère désormais deux identités séparées :

- un hôte humain hors-monde avec son propre nom, son enfance et sa carrière ;
- un symbiote Tok'ra avec son nom et son parcours d'agent.

L'hôte est actif par défaut. Les deux noms sont toujours distincts et restent
stables après une sauvegarde ou un rechargement. Le mod enregistre explicitement
que cette identité d'hôte a été générée pour un Tok'ra déjà fusionné ; il ne se
contente pas de comparer les noms.

L'origine humaine hors-monde est le profil disponible actuellement. Le système
est conçu pour accepter plus tard des origines Tau'ri, Jaffa, Unas ou autres,
avec leurs propres pondérations, noms et histoires culturelles.

Une implantation réelle reste différente : lorsqu'un symbiote rejoint un pawn
existant, le nom et les backstories de ce pawn deviennent l'identité de l'hôte,
sans génération artificielle. Après extraction d'un Tok'ra pré-fusionné puis
réimplantation dans un nouveau colon, la véritable identité du nouveau colon
remplace l'ancienne origine générée.

## Basculement de personnalité

Un Tok'ra appartenant réellement à la colonie et directement contrôlé par le
joueur dispose d'un gizmo unique. Il permet :

- de laisser le symbiote prendre le contrôle lorsque l'hôte est actif ;
- de rendre le contrôle à l'hôte lorsque le symbiote est actif.

Le basculement change :

- le nom principal affiché ;
- la carrière affichée ; l'enfance reste celle de l'hôte tant qu'aucune enfance Tok'ra dédiée n'est disponible ;
- le titre associé ;
- uniquement les écarts de compétences accordés par les parcours actifs.

Il ne change pas :

- le corps, les gènes ou le xénotype ;
- la faction ou les relations ;
- les traits et passions ;
- l'équipement ou l'inventaire ;
- la santé, les blessures ou les implants.

La description de santé continue d'afficher les deux noms, les deux parcours et
la personnalité active, afin que l'identité inactive ne disparaisse jamais.

## Progression commune

Les deux personnalités partagent la progression réellement acquise pendant la
partie. Le mod conserve une base commune d'expérience et applique seulement les
bonus de compétences fournis par les backstories actuellement actives.

L'expérience gagnée sous une personnalité reste disponible sous l'autre. Les
tests de `0.3.11-dev` confirment que des basculements répétés n'empilent pas les
bonus, ne dupliquent pas les niveaux et n'effacent pas la progression.

## Extraction et nouvel hôte

Avant une extraction ou un transfert, l'identité de l'hôte est automatiquement
restaurée. L'ancien hôte conserve ainsi son nom, ses backstories et sa
progression propre.

Lorsqu'il rejoint un nouvel hôte, le symbiote conserve son nom et son parcours,
mais les données propres à l'ancien hôte ne sont pas appliquées au nouveau.

## Limite aux Tok'ra du joueur

Les visiteurs, alliés, ennemis, membres de faction et pawns de quête non
contrôlés gardent le fonctionnement classique. Ils ne reçoivent aucun gizmo et
ne changent pas manuellement de personnalité.

Le gizmo disparaît également lorsqu'un colon n'est plus directement contrôlable,
par exemple pendant un état mental, puis réapparaît lorsque le contrôle revient.

## Voyage en caravane

Un colon Tok'ra reste contrôlé par le joueur lorsqu'il quitte la carte dans une
caravane. La caravane sélectionnée dispose donc d'un unique gizmo
**Identités Tok'ra** lorsqu'elle transporte au moins un Tok'ra éligible.

Ce gizmo ouvre une liste : chaque ligne indique le pawn concerné et l'identité
qui prendra le contrôle. Le basculement réutilise exactement les mêmes données,
backstories et règles de progression que le gizmo du pawn sur une carte.

Les invités, prisonniers, esclaves et pawns de quête non recrutés transportés
avec la caravane restent exclus. Le système ne transforme pas un simple contrôle
temporaire en appartenance réelle à la colonie.

## Intégration aux interfaces vanilla

Le nom et les backstories actifs sont les vraies données affichées par le pawn.
Les onglets Bio, Social et Santé, les caravanes et les nouveaux messages doivent
donc refléter la personnalité active, tandis que le résumé de santé conserve
toujours les deux identités.

Les anciennes lettres déjà reçues restent des traces historiques et ne sont pas
réécrites après un basculement ultérieur. La mort, les cadavres et la
résurrection sont audités avant toute correction spécifique afin d'éviter un
second système d'identité parallèle.

