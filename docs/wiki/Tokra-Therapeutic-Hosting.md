# Hébergement thérapeutique Tok'ra

**Statut : Implémenté — prototype `0.1.44-dev`, extension `0.1.46-dev`**

Un symbiote Tok'ra adulte actif améliore fortement la capacité de guérison de
son hôte. La mécanique suit désormais davantage les règles de santé de
RimWorld qu'une courte liste fermée de maladies.

## Affections traitées

Le symbiote soigne les affections biologiques visibles et néfastes que RimWorld
considère comme curables par un objet médical. Cela couvre notamment les pathologies déjà
testées comme le carcinome, ainsi que l'asthme. Lorsque l'asthme affecte les deux
poumons, les deux occurrences sont retirées.

Les blessures récentes non permanentes sont régénérées progressivement. Elles
ne disparaissent pas instantanément afin que les hôtes Tok'ra restent
vulnérables pendant un combat.

## Limites conservées

Le prototype ne retire pas :

- les cicatrices permanentes ;
- les membres manquants ou amputés ;
- les implants et prothèses ;
- les addictions, sevrages et dépendances ;
- les états liés à une grossesse ;
- les états internes propres à GateRim SG-1.

Une régénération avancée des cicatrices ou des membres perdus pourra être
étudiée séparément si le lore et l'équilibrage le justifient.

## Implantation thérapeutique volontaire

Un symbiote Tok'ra libre peut proposer une implantation thérapeutique à un pawn
compatible souffrant d'une affection curable. Une confirmation explicite reste
requise avant le transfert permanent du symbiote.

## Exclusion des hôtes Goa'uld

Les hôtes Goa'uld ne bénéficient pas automatiquement de cette mécanique
Tok'ra, même s'ils utilisent le même type général de symbiote adulte actif.
