# Hôte Goa'uld actif

Après la phase d'[implantation récente](Recent-Implantation), le même symbiote devient un état adulte permanent dans son hôte. Son identifiant et son identité persistent pendant la conversion.

## Effets biologiques

L'hôte bénéficie notamment d'une guérison et d'une immunité accélérées, d'une réduction de la douleur et des dégâts entrants, et d'une longévité fortement augmentée. Son xenotype germinal n'est pas remplacé.

## Prise de contrôle hostile

Depuis `0.3.40-dev`, un symbiote Goa'uld appartenant à une faction hostile peut prendre le contrôle d'un colon :

- le joueur conserve toute la phase critique pour tenter une extraction d'urgence ;
- après conversion, le pawn existant rejoint la faction Goa'uld et quitte le contrôle de la colonie ;
- son corps, ses relations, son équipement, son xenotype et l'identité du symbiote sont conservés ;
- son nom affiché devient celui du symbiote tant que le contrôle hostile reste actif, tandis que le nom complet de l'ancien colon est conservé pour sa restauration ;
- il attaque les colons et les biens comme une force de raid ;
- il peut finalement se replier si la carte est abandonnée ou l'assaut épuisé.

## Sauver l'ancien colon après conversion

Depuis `0.3.41-dev`, la conversion active n'est plus irréversible. Le joueur doit toutefois :

1. mettre l'hôte hostile à terre sans le tuer ;
2. le capturer comme prisonnier de la colonie ; son groupe d'assaut est alors libéré et ne peut plus le reprendre pendant sa détention ;
3. disposer d'un médecin de niveau `10` et de trois médicaments ;
4. planifier `extraire le symbiote Goa'uld actif` dans l'onglet Santé.

Une réussite restaure l'ancien colon et son nom d'origine sans recréer le pawn et extrait le même symbiote vivant. Le parasite est temporairement anesthésié, puis redevient dangereux à son réveil. L'extraction ne le tue pas et aucun confinement spécial n'est disponible : la salle doit rester sécurisée, puis la colonie doit éliminer le Goa'uld ou affronter son réveil. Un échec laisse le symbiote actif en place si le patient survit.

Consulte [Chirurgies d'extraction Goa'uld](Extraction-Surgery).

## Exceptions

La prise de contrôle hostile et l'extraction Goa'uld active ne s'appliquent pas aux Tok'ra. Les symbiotes Goa'uld contrôlés par le joueur ou sans allégeance hostile ne retirent pas automatiquement le contrôle de l'hôte, mais un hôte Goa'uld contrôlé peut tout de même choisir l'opération d'extraction active.
