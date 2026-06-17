# Tok'ra

> Statut : Prototype
> Version d'introduction : 0.1.39-dev

## Présentation

Les Tok'ra constituent une première branche distincte des Goa'uld hostiles.

Cette première version permet de tester leur principe essentiel :

```text
symbiote Tok'ra libre
    ↓
hôte volontaire
    ↓
symbiose persistante
```

## Présence mondiale masquée

Depuis `0.2.7-dev`, les Tok'ra disposent d'une
[présence mondiale masquée](Tokra-World-Presence) persistante.

```text
1 faction Tok'ra persistante
0 colonie mondiale classique
0 raid naturel
```

La faction représente un réseau clandestin de cellules plutôt qu'une
civilisation territoriale visible. Les visiteurs, opportunités thérapeutiques
et livraisons médicales existantes réutilisent cette même présence sauvegardée.

Les bases cachées visitables, sites de quête, marchands et personnages nommés
restent prévus pour plus tard.

## Tester un symbiote Tok'ra

Fais apparaître en mode développeur :

```text
symbiote Tok'ra
```

Sélectionne-le.

Il doit proposer uniquement :

```text
Implantation Tok'ra volontaire
```

Il ne doit pas proposer :

```text
implantation forcée
implantation rituelle Goa'uld
chasse autonome
```

## Implantation volontaire

La cible doit être :

```text
humanoïde compatible
contrôlé par le joueur
âgé d'au moins 13 ans biologiques
dans un rayon de 12 cases
accessible
sans symbiote adulte existant
```

Après implantation, l'identité Tok'ra reste persistante.

## Extraction

L'extraction chirurgicale existante reste disponible pendant l'implantation
récente.

Après extraction, le même symbiote doit réapparaître comme :

```text
symbiote Tok'ra
```

avec :

```text
origine Tok'ra
chasse autonome désactivée
```

## Tenue de terrain Tok'ra

Depuis `0.2.19-dev`, les hôtes Tok'ra volontaires générés pour les planques peuvent porter une [tenue de terrain Tok'ra](Tokra-Field-Clothing-Set) dédiée.

Cette tenue unique donne une identité visuelle sobre aux agents Tok'ra et corrige le contact de planque qui pouvait apparaître nu après le jalon `0.2.18-dev`.


## Feuille de route des interactions

Depuis `0.2.25-dev`, une [feuille de route des interactions Tok'ra](Tokra-Interaction-Roadmap) cadre les prochaines évolutions de la faction.

Elle couvre les pistes suivantes : soutien médical, réseau de planques, communicateur sécurisé, aide défensive rare, courte questline et réactions futures selon les races ou cultures.


## Communicateur sécurisé Tok'ra

Depuis `0.2.26-dev`, les colonies ayant atteint le palier fiable peuvent construire un [communicateur sécurisé Tok'ra](Tokra-Secure-Communicator).

Depuis `0.2.27-dev`, ce communicateur permet de demander une [diversion défensive Tok'ra](Tokra-Defensive-Diversion-Request) pendant une attaque active. L'aide reste limitée et clandestine : elle perturbe quelques ennemis, mais ne fait pas apparaître de renforts permanents.

Depuis `0.2.31-dev`, il peut aussi fournir une [évaluation tactique Tok'ra](Tokra-Tactical-Threat-Assessment) purement informative pendant une menace hostile active.

Depuis `0.2.45-dev-r5`, cette chaîne aboutit à une opération locale jouable : la caravane entre sur une carte temporaire, affronte une garnison Jaffa adaptée aux points de menace de la colonie et sabote un nœud de contrôle avant l'arrivée éventuelle de renforts. Depuis `0.2.46-dev-r1`, le relais occupe l'un de trois petits postes Goa'uld préconçus, dont les pièces couvertes restent masquées jusqu'à leur ouverture, et une réserve limitée est déjà stockée dans le poste.

## Évolutions prévues

```text
faction générée
hôtes Tok'ra volontaires via événements
colonies et diplomatie
approvisionnement en trétonine
reine Goa'uld ou Tok'ra
origine biologique des larves
```


## Prototype d'hôte volontaire

Depuis `0.1.40-dev`, fais apparaître un
[prototype d'hôte Tok'ra volontaire](Tokra-Host-Prototype) en mode développeur.

Ce colon humain contrôlé par le joueur reçoit automatiquement une identité
Tok'ra persistante après son apparition.


## Fondation des groupes

Depuis `0.1.41-dev`, une
[fondation technique des groupes Tok'ra](Tokra-Pawn-Groups) existe.

La faction masquée contient désormais des profils internes `Combat` et
`Peaceful`. Depuis `0.2.7-dev`, une instance persistante unique est générée
sans créer de colonie mondiale.


## Visiteurs pacifiques

Depuis `0.1.42-dev`, une
[visite Tok'ra pacifique](Tokra-Peaceful-Visitors) peut être déclenchée
manuellement en mode développeur.

Depuis `0.1.43-dev`, le storyteller peut également sélectionner rarement cette
visite à partir du jour `15`. Un délai minimal de `30` jours évite les visites
trop rapprochées.


## Hébergement thérapeutique

Les hôtes Tok'ra actifs soignent désormais les affections biologiques curables
selon les règles de santé de RimWorld. L'asthme est pris en charge même lorsqu'il
affecte les deux poumons, et les blessures non permanentes se régénèrent
progressivement.

Les cicatrices permanentes, les membres manquants, les implants, les prothèses,
les addictions et les dépendances restent inchangés. Une éventuelle
régénération avancée devra être étudiée séparément.


## Implantation thérapeutique volontaire

Depuis `0.1.45-dev`, un symbiote Tok'ra libre dispose d'une
[action d'implantation thérapeutique](Tokra-Therapeutic-Implantation). Elle
cible un humanoïde malade compatible contrôlé par le joueur et demande une
confirmation explicite avant d'utiliser le flux d'implantation existant.


## Opportunité thérapeutique naturelle

Depuis `0.1.47-dev`, une
[opportunité thérapeutique Tok'ra](Tokra-Therapeutic-Opportunity) peut
apparaître rarement lorsqu'un colon compatible souffre d'une affection
biologique curable non traumatique. Depuis `0.1.48-dev`, le symbiote Tok'ra
libre arrive avec une petite escorte de 1 à 2 hôtes volontaires. L'implantation
reste un choix manuel soumis à confirmation. Depuis `0.1.49-dev`, l'offre est
limitée à deux jours : le joueur peut l'accepter, la refuser explicitement ou
la laisser expirer. Dans les trois cas, l'escorte repart proprement.


## Confiance Tok'ra

Depuis `0.1.50-dev`, les issues des offres thérapeutiques alimentent une
[jauge persistante de confiance Tok'ra](Tokra-Trust). L'acceptation augmente la
confiance, le refus explicite l'abaisse légèrement et l'expiration sans réponse
la réduit davantage.

Depuis `0.1.51-dev`, les [paliers de confiance Tok'ra](Tokra-Trust-Thresholds)
modulent la durée des nouvelles offres thérapeutiques et la taille de leur
escorte pacifique.

La faction reste masquée : cette jauge constitue une fondation légère avant
l'introduction de quêtes et de relations diplomatiques plus complètes.


## Soutien médical en trétonine

Depuis `0.1.52-dev`, les équipes Tok'ra suffisamment confiantes apportent un
[cadeau léger de trétonine](Tokra-Medical-Support-Gifts) lors d'une opportunité
thérapeutique escortée. Le palier coopérative fournit `1` dose et le palier
fiable `2` doses. Les paliers méfiante et neutre n'apportent aucune ressource.


## Livraisons médicales indépendantes

Depuis `0.1.53-dev`, les relations Tok'ra coopératives ou fiables peuvent
déclencher rarement une [livraison médicale indépendante](Tokra-Medical-Support-Deliveries).
Cette équipe apporte de la trétonine sans exiger de colon malade et sans proposer
de symbiose : `2` doses au palier coopérative, puis `4` doses au palier fiable.


## Pondérations storyteller par confiance

Depuis `0.1.54-dev`, les [pondérations storyteller Tok'ra](Tokra-Storyteller-Trust-Weights)
modulent la fréquence naturelle des opportunités thérapeutiques et des
livraisons médicales indépendantes. Les relations méfiantes réduisent les
offres, tandis que les relations fiables rendent les deux incidents légèrement
plus probables.


## Refroidissement diplomatique méfiant

Depuis `0.1.55-dev`, un [refroidissement diplomatique Tok'ra](Tokra-Wary-Diplomatic-Cooldown)
suspend temporairement les nouvelles opportunités thérapeutiques lorsque la
confiance reste sous `0` après une réponse négative : `3` jours après un refus
explicite et `5` jours après une expiration sans réponse. Les visites pacifiques
ordinaires restent possibles.


## Soutien médical avancé au palier fiable

Depuis `0.1.56-dev`, une [livraison médicale Tok'ra fiable](Tokra-Trusted-Advanced-Medicine-Support)
ajoute `1` médicament ultratechnologique vanilla aux `4` doses de trétonine
déjà fournies. Cette première récompense positive reste rare et ne transforme
pas encore les Tok'ra en marchands.


## Cellules clandestines

Depuis `0.2.12-dev`, les Tok'ra peuvent laisser un
[cache d'une cellule clandestine](Tokra-Hidden-Cell-Cache).

Il s'agit d'une présence discrète, non territoriale et limitée à quelques
fournitures médicales. Elle ne crée ni colonie, ni marchand, ni recrutement.

## Signaux de planque

Depuis `0.2.13-dev`, une cellule Tok'ra clandestine peut envoyer un
[signal de planque](Tokra-Safehouse-Signal).

Ce signal ne crée pas encore de site visitable. Il confirme seulement un canal
clandestin et accorde un très léger gain de confiance.

## Pistes de planque

Depuis `0.2.14-dev`, les signaux de planque Tok'ra conservent aussi des
[pistes de planque](Tokra-Safehouse-Leads).

Ces pistes ne créent pas encore de site mondial. Elles préparent le futur
prototype de planque Tok'ra visitable.

Depuis `0.2.15-dev`, une piste de planque peut mener à un
[cache médical découvert grâce à une piste Tok'ra](Tokra-Safehouse-Lead-Cache).

Cette étape consomme une piste, mais ne crée pas encore de vraie planque
visitable.

Depuis `0.2.16-dev`, une piste de planque peut aussi créer un
[marqueur temporaire de planque Tok'ra](Tokra-Hidden-Safehouse-World-Marker) sur
la carte du monde.

Le marqueur ne crée pas encore de site visitable.

Depuis `0.2.17-dev`, une piste peut enfin révéler une
[planque Tok'ra visitable](Tokra-Hidden-Safehouse-Site). Une caravane peut
explorer sa petite carte non hostile et récupérer un cache médical limité.

Depuis `0.2.18-dev`, un unique hôte Tok'ra volontaire âgé d'au moins `20` ans
demeure dans la planque. Ce contact est pacifique, non marchand et non
recrutable.

## Mission jouable de sabotage du relais

La piste décodée mène à une seule opération jouable. Une caravane présente sur le relais lance la mission et entre sur une carte temporaire avec une garnison Goa'uld/Jaffa dimensionnée à partir des points de menace vanilla. Depuis `0.2.46-dev-r2`, cette garnison protège d'abord le poste au lieu de charger immédiatement les intrus ; elle passe à l'assaut si elle est attaquée, si une installation Goa'uld est endommagée ou dès que le sabotage commence. Le nœud de contrôle doit être saboté par un colon capable de travail intellectuel ; sa progression persiste si le travail est interrompu.

Depuis `0.2.46-dev-r1`, la carte utilise aléatoirement l'un de trois plans de poste relais : bunker de commandement avec annexe, station divisée en deux bâtiments ou enceinte fortifiée avec cour intérieure. Les bâtiments utilisent des murs, portes, sols, toits et barricades vanilla, sans dépendance à un DLC supplémentaire. Les pièces fermées sont couvertes et restent masquées jusqu'à l'ouverture normale d'une porte.

Le sabotage déclenche un avertissement de renforts de 12 heures. Le compte à rebours ne bloque pas le départ : une fois l'objectif accompli et les hostiles actifs neutralisés ou en fuite, la caravane peut repartir avant l'arrivée de la vague. Si le joueur reste, un à trois Jaffa arrivent depuis le bord de carte.

La réserve est présente dès la génération sur une étagère du local de stockage : une arme énergétique Goa'uld, généralement un Zat'nik'tel et plus rarement un Ma'Tok, ainsi qu'un ou deux composants industriels. Son contenu est accessible normalement dès la découverte du local et peut même être utilisé pendant l'opération. La règle d'évacuation empêche toutefois de l'emporter hors de la carte avant le sabotage et la neutralisation des hostiles actifs, sans nouvelle interaction monde ni analyse supplémentaire.

Les anciens états de reconnaissance et de préparation restent conservés pour la compatibilité des sauvegardes et le rapport du canal Tok'ra, mais le flux visible reste une seule action monde suivie de la mission locale.

Le relais reste physiquement destructible. S'il est détruit avant la fin du sabotage, l'opération discrète échoue, les Jaffa encore présents sont alertés et le compte à rebours local de renforts est annulé. La caravane peut évacuer dès qu'aucun hostile actif ne reste, mais la destruction laisse une signature exploitable : une riposte Goa'uld/Jaffa est programmée contre une colonie après un délai inconnu du joueur.

Depuis `0.2.46-dev-r4`, la commande vanilla de reformation de caravane est explicitement rattachée au site : elle reste masquée tant que l'opération n'est ni réussie ni abandonnée, puis devient disponible dès qu'aucun hostile actif ne subsiste. Une vague seulement annoncée ne bloque pas le départ. Les murs, portes et barricades Goa'uld restent vulnérables aux armes. Leur revendication suit les règles vanilla et, une fois un élément revendiqué, sa déconstruction reste disponible normalement même si un nouvel ennemi apparaît ensuite sur la carte. Depuis `0.2.46-dev-r5`, la carte temporaire et son marqueur monde sont supprimés automatiquement après le départ de la caravane, et la génération applique de nouveau une toiture complète après la pose de toutes les structures. Depuis `0.2.47-dev`, ce départ enregistre automatiquement l'issue de l'opération. La cellule Tok'ra transmet ensuite un débriefing entre 6 et 18 heures plus tard : un sabotage discret réussi renforce sa confiance, tandis qu'une destruction directe du relais la réduit. Aucune nouvelle action sur la carte du monde n'est demandée au joueur.

## 0.2.45-dev-r5 validation note

Relay sabotage work is now stored on the device and persists if the pawn is interrupted. The hidden duration scales from roughly 12 in-game hours at Intellectual 0 to 6 hours at Intellectual 20. Initial defenders and delayed reinforcements are spawned directly from Goa'uld Jaffa pawn kinds, avoiding the faction combat-group generator that cannot resolve the low reinforcement budget.
