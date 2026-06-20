# Double identité Tok'ra

> Statut : basculement de personnalité validé dans `0.3.11-dev`

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
