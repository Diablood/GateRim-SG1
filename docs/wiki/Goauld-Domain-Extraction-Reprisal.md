# Ultimatum et représailles après extraction Goa'uld

> Statut : Prototype jouable
> Première version : `0.3.55-dev`
> Choix ajouté : `0.3.56-dev`

Réussir l'extraction chirurgicale d'un [hôte Goa'uld actif](Active-Goauld-Host)
sur la colonie constitue désormais un affront envers le domaine auquel le
symbiote appartient.

Une transmission arrive immédiatement. Elle nomme le domaine, le Goa'uld
extrait et son ancien hôte, afin que la cause de l'exigence reste
compréhensible.

## Ultimatum

Le domaine laisse un jour à la colonie pour choisir :

- remettre le symbiote vivant qui vient d'être extrait ; il disparaît alors de
  la carte et l'attaque est annulée ;
- défier le domaine et assumer les représailles annoncées ;
- voir la demande plus tard ; la lettre reste disponible, mais le délai d'un
  jour continue de s'écouler.

Le silence produit la même conséquence que le refus. Si le symbiote exact
n'est plus disponible sur la carte de la colonie, sa remise devient impossible
et l'interface en indique la raison.

Le symbiote reste sous anesthésie tant que la décision est ouverte : le délai
ne force donc pas la colonie à subir son réveil ou à disposer immédiatement
d'une prison spéciale. Le tuer pendant ce délai vaut défi immédiat, ferme
l'ultimatum et annonce clairement le temps restant avant l'arrivée des Jaffa.

Si l'opération échoue mais que l'hôte survit, aucune réaction n'est créée. Si
l'échec tue immédiatement l'hôte Goa'uld, le domaine annonce directement des
représailles : aucun symbiote vivant ne reste disponible pour une remise.

## Conséquence

Après un refus ou l'expiration de l'ultimatum, le domaine envoie une force
Jaffa entre un et trois jours plus tard. Sa puissance utilise le niveau de
menace calculé par RimWorld au moment de l'extraction. La force arrive à pied
depuis le bord de la carte et peut employer les doctrines naturelles déjà
disponibles.

Le raid appartient au domaine nommé dans la transmission, y compris lorsque le
monde contient plusieurs factions Goa'uld.

## Garde-fous

- un seul ultimatum ou raid de représailles peut attendre par domaine ;
- une nouvelle extraction ne crée pas de pile pendant cette attente ;
- après la résolution, le même domaine respecte 15 jours de refroidissement ;
- l'extraction d'urgence pendant l'implantation récente ne déclenche pas cette
  réaction ;
- les Tok'ra et les symbiotes sans domaine Goa'uld identifiable sont exclus.

L'exigence porte sur le symbiote à l'origine de l'affront. Elle ne demande ni
argent, ni goodwill, ni monnaie spéciale et ne constitue pas une mission
Tok'ra.

## Représailles communes d'alliance

Depuis `0.3.81-dev`, le même suivi de réactions gère aussi un cas distinct : la
victoire visible du joueur contre un raid naturel standard issu d'un contexte
d'alliance. Ce n'est pas lié à l'extraction d'un symbiote.

Si la force standard initiale compte au moins cinq Jaffa et tombe à `25 %` ou
moins de combattants actifs, il existe `25 %` de chances qu'une représaille
commune soit annoncée. Elle attend `2` à `4` jours sous **Commandement SG-1**,
puis lance un assaut direct coordonné de la paire exacte : le domaine vaincu et
son allié.

La réaction utilise `80 %` des points de menace vanilla actuels et les partage
`60/40`. Elle n'ajoute ni goodwill, ni rupture d'alliance, ni conséquence
territoriale. Les renforts différés, raids conjoints, missions, raids contrôlés
et représailles d'extraction ne déclenchent pas cette boucle.
