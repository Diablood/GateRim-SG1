# Hôte Goa'uld actif

Après la phase d'[implantation récente](Recent-Implantation), le même symbiote devient un état Goa'uld adulte permanent dans son hôte. Son identifiant et son identité persistent pendant la conversion.

## Effets biologiques

L'hôte bénéficie notamment d'une guérison et d'une immunité accélérées, d'une réduction de la douleur et des dégâts entrants, et d'une longévité fortement augmentée. Son xenotype germinal n'est pas remplacé.

## Prise de contrôle hostile

Depuis `0.3.40-dev`, l'origine et l'allégeance du symbiote ont une conséquence directe :

- un symbiote Goa'uld appartenant à une faction hostile arme une prise de contrôle lorsqu'il implante un colon joueur;
- le joueur conserve toute la durée de la phase critique pour tenter une extraction;
- si la conversion s'achève, le pawn existant rejoint la faction Goa'uld du symbiote et quitte le contrôle de la colonie;
- son corps, ses relations, son équipement, son xenotype et l'identité du symbiote restent ceux d'avant la conversion.

Une lettre de menace signale cette perte. L'hôte rejoint aussi un assaut Goa'uld persistant : il attaque la colonie au lieu de chercher immédiatement à quitter la carte comme un pawn hostile isolé. Il conserve toutefois le repli vanilla d'un raid et peut donc finalement quitter une carte abandonnée ou un assaut épuisé. Ce flux complet, y compris la migration des sauvegardes de développement antérieures, est validé depuis `0.3.40-dev`.

## Exceptions

La prise de contrôle hostile ne s'applique pas aux Tok'ra, aux symbiotes Goa'uld contrôlés par le joueur ni aux symbiotes sans allégeance hostile. Ces implantations conservent la faction actuelle de l'hôte.

Une extraction réussie pendant la phase critique laisse l'hôte dans sa faction et fait réapparaître le même symbiote libre avec son allégeance d'origine. Une restauration développeur d'un hôte déjà contrôlé retire d'abord son assaut Goa'uld avant de rendre la faction déplacée.
