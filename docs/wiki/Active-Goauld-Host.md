# Hôte Goa'uld actif

![Icône finale de l'hôte Goa'uld](images/SG1_GoauldHost.png)

Après la phase d'[implantation récente](Recent-Implantation), le même symbiote
devient un état adulte permanent dans son hôte. Son identifiant et son identité
persistent pendant la conversion. L'icône finale représente directement le
symbiote Goa'uld : c'est sa présence qui distingue l'hôte actif d'un humain
ordinaire.

## Cas des personnages de départ

Depuis `0.3.87-dev`, tout candidat de départ utilisant le xénotype `hôte
Goa'uld` reçoit avant validation le véritable état adulte décrit sur cette page.
La carrière finale détermine l'origine persistante :

- une carrière Tok'ra crée un symbiote d'origine Tok'ra avec une
  [double identité](Tokra-Dual-Identity) immédiatement disponible ;
- une carrière Goa'uld crée un symbiote d'origine Goa'uld sans commande de
  changement de personnalité Tok'ra.

Le xénotype ne peut donc plus apparaître comme un simple humain amélioré sans
symbiote. Une suppression volontaire effectuée avec un mod d'édition après
l'affichage reste toutefois respectée au lancement et au rechargement.

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

Depuis `0.3.56-dev`, cette réussite ouvre un ultimatum du domaine enregistré
dans l'identité du symbiote. La colonie dispose d'un jour pour remettre le
symbiote exact et éviter l'attaque. Refuser ou garder le silence programme une
force de représailles entre un et trois jours plus tard. Une nouvelle
extraction du même domaine ne peut pas empiler une seconde réaction pendant
l'attente ni durant les 15 jours suivants.

La lettre peut être refermée avec `Voir plus tard` sans arrêter son délai. Si
l'opération échoue et tue immédiatement l'hôte, le domaine passe directement
aux représailles puisqu'aucun symbiote vivant ne peut être remis.

Le symbiote extrait reste anesthésié pendant l'ultimatum. Sa mise à mort avant
la réponse est considérée comme un défi immédiat et l'avertissement indique le
délai avant le raid.

Lorsqu'un hôte de caste généré par un domaine est extrait en captivité, le
corps humain n'appartient plus aux Goa'uld. Il reste toutefois prisonnier et ne
rejoint pas automatiquement la colonie : le joueur peut le recruter ou le
libérer avec les choix vanilla. Un ancien colon possédé retrouve toujours sa
faction d'origine.

Consulte [Chirurgies d'extraction Goa'uld](Extraction-Surgery).

## Exceptions

La prise de contrôle hostile et l'extraction Goa'uld active ne s'appliquent pas aux Tok'ra. Les symbiotes Goa'uld contrôlés par le joueur ou sans allégeance hostile ne retirent pas automatiquement le contrôle de l'hôte, mais un hôte Goa'uld contrôlé peut tout de même choisir l'opération d'extraction active.
