# Goa'uld

> Statut : Prototype
> Première fondation : 0.1.7-dev

## Présentation

Les Goa'uld sont des symbiotes parasites capables de prendre le contrôle d'un
hôte humanoïde. Contrairement à un simple xenotype, le système final devra
traiter le Goa'uld comme un organisme distinct pouvant entrer dans un hôte,
le quitter et éventuellement être transféré.

## Prototypes actuels

### Hôte déjà implanté

Le xenotype suivant permet de tester les effets biologiques d'un hôte déjà possédé :

```text
hôte Goa'uld
```

### Symbiote libre

Depuis `0.1.8-dev`, un symbiote adulte sans hôte peut être généré en mode développeur :

```text
symbiote Goa'uld
```

### Implantation récente

Depuis `0.1.11-dev`, un état de santé temporaire peut être ajouté manuellement
à un humanoïde :

```text
implantation Goa'uld récente
```

Cet état représente la phase critique pendant laquelle le parasite s'attache
au système nerveux de sa victime. Il dure provisoirement une journée de jeu,
affiche un compte à rebours et augmente la douleur.

### Implantation forcée interactive

Depuis `0.1.17-dev`, un symbiote libre adjacent à un humanoïde adulte compatible
peut déclencher manuellement :

```text
Implantation forcée
```

Le symbiote disparaît et son identité persistante est transférée dans l'état
`implantation Goa'uld récente`.

Consulte [Implantation forcée Goa'uld](Forced-Implantation) pour le mode d'emploi.

### Hôte actif après conversion

Depuis `0.1.18-dev`, la phase critique se transforme automatiquement après une
journée de jeu en :

```text
symbiote Goa'uld adulte
```

Le même identifiant persistant est conservé. L'état actif apporte des bonus
importants sans remplacer le xenotype germinal d'origine.

Consulte [Hôte Goa'uld actif](Active-Goauld-Host).


Depuis `0.3.40-dev`, un symbiote Goa'uld appartenant à une faction hostile conserve cette allégeance pendant l'implantation. Si la phase critique arrive à son terme, l'hôte joueur rejoint la faction du symbiote, quitte le contrôle de la colonie et participe à un assaut réel contre les colons et leurs biens. Comme une force de raid, il peut finalement se replier lorsque la carte est abandonnée ou que l'assaut est épuisé. Une extraction avant conversion préserve la colonie. Les Tok'ra et les symbiotes contrôlés par le joueur restent exclus de cette prise de contrôle.

### Extraction et chirurgie

Pendant l'implantation récente, `extraction d'urgence Goa'uld` permet de retirer le parasite avant la prise de contrôle. L'ancienne commande instantanée existe encore uniquement lorsque le mode développeur RimWorld est actif ; les diagnostics avancés restent informatifs.

Depuis `0.3.41-dev`, un hôte déjà actif peut aussi être sauvé après avoir été neutralisé et capturé. L'opération `extraire le symbiote Goa'uld actif` exige davantage de médecine, de travail et de médicaments. Une réussite restaure le même pawn et libère le même symbiote temporairement anesthésié ; un échec laisse le parasite actif si le patient survit.

Consulte [Extraction d'urgence Goa'uld](Emergency-Extraction) et [Chirurgies d'extraction Goa'uld](Extraction-Surgery).

### Chasse autonome

Depuis `0.1.21-dev`, le symbiote libre recherche un humanoïde compatible proche,
le poursuit et déclenche automatiquement son implantation au contact.

Après une extraction, un bref délai de sécurité évite une réimplantation
immédiate.

Consulte [Chasse autonome des symbiotes libres](Autonomous-Hunt).

### Incursion de symbiotes libres

Depuis `0.3.39-dev`, une menace rare peut faire entrer naturellement de un à
quatre symbiotes Goa'uld libres par une bordure accessible de la colonie. Le
nombre dépend des points de menace calculés par le storyteller.

Chaque créature conserve la chasse autonome et l'implantation persistante déjà
utilisées par les symbiotes libres générés manuellement. Trois avertissements
RP alternent sans répétition immédiate, y compris après sauvegarde et
rechargement.

Consulte [Incursion de symbiotes Goa'uld libres](Goauld-Free-Symbiote-Incursion).

### Implantation rituelle

Depuis `0.1.22-dev`, le symbiote libre peut déclencher une voie contrôlée :

```text
Implantation rituelle
```

La commande ouvre un curseur de ciblage : le joueur choisit directement un
humanoïde compatible accessible dans un rayon limité. La même identité
persistante est conservée.

Consulte [Implantation rituelle Goa'uld](Ritual-Implantation).

### Durée du rituel

Depuis `0.1.24-dev`, la cible rituelle n'est plus implantée immédiatement. Une
cérémonie temporisée doit s'achever tandis que la cible reste valide, accessible
et dans le rayon autorisé.

Le rituel peut être annulé et sa progression survit aux sauvegardes.

### Bassin rituel

Depuis `0.1.25-dev`, la cérémonie contrôlée exige un
[bassin rituel Goa'uld](Ritual-Basin). Le symbiote et la cible doivent rester
proches de cette structure jusqu'à la fin.

### Reine Goa'uld

Depuis `0.1.57-dev`, une reine Goa'uld distincte peut être générée en mode
développeur :

```text
reine Goa'uld
```

Depuis `0.2.10-dev`, une rare reine échappée peut également rejoindre
naturellement la colonie sous le contrôle du joueur. Elle fournit des
symbiotes immatures avec un délai persistant d'un jour, qui doivent ensuite
mûrir au bassin d'incubation du Prim'ta.

Consulte [Reine Goa'uld](Goauld-Queen).

### Domaines des Grands Maîtres Goa'uld

Depuis `0.1.61-dev`, une première fondation hostile de faction existe.

Depuis `0.2.1-dev`, elle devient une présence mondiale jouable :

```text
Domaines des Grands Maîtres Goa'uld
```

Une nouvelle planète génère une faction hostile visible avec un nombre limité
de colonies. Les noms des dirigeants Goa'uld utilisent encore une génération vanilla. Leur future correction devra préserver séparément l'identité de l'hôte et celle du symbiote.

Depuis `0.3.46-dev`, chaque nouvelle occurrence reçoit un nom de
domaine propre et ses colonies utilisent une grammaire Goa'uld dédiée plutôt
que des noms pirates vanilla. Les marchands Goa'uld restent désactivés.

Depuis `0.1.62-dev`, deux premiers serviteurs Jaffa Goa'uld sont utilisés par
les groupes de combat et les défenses de colonies :

```text
guerrier Jaffa au service des Goa'uld
garde Jaffa au service des Goa'uld
```

Les deux types de serviteurs utilisent la lignée héréditaire Jaffa existante. Les profils
`Combat` et `Settlement` servent respectivement aux assauts et aux premières
défenses de colonies.

Depuis `0.2.1-dev`, un
[raid naturel de Jaffa Goa'uld](Goauld-Jaffa-Natural-Raid) rare attaque la
colonie. Depuis `0.3.54-dev`, l'unique incident peut choisir un assaut direct,
une tentative d'enlèvement ou une frappe destructrice selon les points de
menace et le contexte de la colonie.

Depuis `0.3.56-dev`, réussir l'extraction chirurgicale d'un Goa'uld actif peut
provoquer un [ultimatum du domaine](Goauld-Domain-Extraction-Reprisal). Remettre
le symbiote exact évite l'attaque ; refuser ou laisser expirer la demande
programme le raid différé de la faction exacte.

Depuis `0.1.63-dev`, chaque nouveau guerrier ou garde Jaffa généré reçoit
automatiquement un Prim'ta initial. L'attribution n'est effectuée qu'une
seule fois : retirer ensuite la larve ne crée pas de remplacement artificiel.

Depuis `0.2.3-dev`, les domaines possèdent aussi une
[caste d'hôtes Goa'uld](Goauld-Host-Caste) minoritaire. Chaque faction génère
un véritable **Grand Maître Goa'uld** comme dirigeant, tandis que certains
groupes de colonie peuvent inclure des profils ordinaires nommés simplement
**Goa'uld**. Ces hôtes reçoivent un symbiote adulte actif avec identité
persistante.

Consulte [Domaines des Grands Maîtres Goa'uld](Goauld-System-Lord-Faction).

### Bâton Ma'Tok

Depuis `0.1.64-dev`, le premier équipement Jaffa jouable est disponible :

```text
bâton Ma'Tok
```

Cette arme associe une décharge énergétique lente mais puissante à une
hampe utilisable au corps à corps. Elle peut être fabriquée au banc
d'usinage après la recherche Armement Jaffa et utilise encore des visuels
temporaires dédiés.

Depuis `0.1.65-dev`, les guerriers Jaffa Goa'uld générés reçoivent
automatiquement cette arme grâce au système vanilla de loadout. Depuis
`0.2.9-dev`, les gardes peuvent recevoir un Ma'Tok ou un Zat'nik'tel.

Depuis `0.1.76-dev`, son impact plasma conserve les dégâts thermiques
principaux et ajoute une détérioration structurelle réduite contre les
mécanoïdes vanilla et les bâtiments.

Consulte [Bâton Ma'Tok](Matok-Staff).

### Zat'nik'tel

Depuis `0.1.77-dev`, une première arme de poing Goa'uld peut être fabriquée et
testée :

```text
Zat'nik'tel
```

Ce premier prototype représente uniquement la neutralisation temporaire du
premier tir. Il ajoute une faible perturbation IEM contre les mécanoïdes
vanilla et les bâtiments compatibles, sans blessure physique ni explosion de
zone.

Depuis `0.2.9-dev`, les gardes Jaffa Goa'uld peuvent apparaître avec un Ma'Tok
ou un Zat'nik'tel. Les raids naturels et colonies Goa'uld deviennent donc une
source rare d'armes récupérables.

Le deuxième tir létal et le troisième tir désintégrant restent prévus pour des
itérations séparées.

Consulte [Zat'nik'tel](ZatnikTel).

### Armures Jaffa modulaires

Depuis `0.1.66-dev`, cinq premières pièces d'équipement Jaffa peuvent être
fabriquées et testées manuellement :

```text
armure Jaffa légère
armure Jaffa lourde
gantelets blindés Jaffa
bottes renforcées Jaffa
casque Jaffa déployé
```

Les gantelets protègent réellement les bras, mains et doigts. Les bottes
protègent les jambes, pieds et orteils. Depuis `0.1.67-dev`, le casque peut
être rétracté ou déployé, automatiquement lors de l'enrôlement ou selon le
mode persistant choisi par le joueur.

Depuis `0.1.68-dev`, les guerriers et gardes générés reçoivent automatiquement
leur ensemble modulaire : armure légère pour le guerrier, armure lourde pour
le garde, avec gantelets, bottes et casque rétractable pour les deux profils.

Consulte [Armures Jaffa](Jaffa-Armor).

### Raid Jaffa Goa'uld contrôlé

Depuis `0.1.69-dev`, un incident réservé aux outils développeur permet de
faire arriver un véritable groupe hostile de Jaffa Goa'uld. Sa chance
storyteller est fixée à `0` : il ne se déclenche jamais naturellement.

Ce parcours sert à tester la faction non joueuse réelle, le profil de groupe
`Combat`, les équipements complets et l'absence de bouton de casque sur les
ennemis.

Depuis `0.1.70-dev`, il utilise explicitement la stratégie vanilla
`ImmediateAttack`, afin d'éviter la solution de repli du générateur de raids tout en
conservant le même comportement d'assaut direct.

Depuis `0.1.71-dev`, un second incident contrôlé teste une doctrine
d'enlèvement : certains Jaffa évacuent les colons à terre tandis que les
autres continuent le combat, puis le groupe se replie après une fenêtre
limitée.

Consulte [Raid d'enlèvement Jaffa Goa'uld contrôlé](Goauld-Jaffa-Controlled-Abduction-Raid).

Depuis `0.1.72-dev`, un troisième incident contrôlé teste une doctrine de
destruction : les Jaffa conduisent d'abord un assaut militaire prolongé,
puis tentent de récupérer des captifs ou des objets de valeur avant leur
extraction.

Consulte [Raid de destruction Jaffa Goa'uld contrôlé](Goauld-Jaffa-Controlled-Destruction-Raid).

Consulte [Raid Jaffa Goa'uld contrôlé](Goauld-Jaffa-Controlled-Raid).

## Progression de la menace

Depuis `0.3.53-dev`, les raids, avertissements interceptés et garnisons de sites
Goa'uld utilisent les points de menace vanilla. La richesse, la puissance de la
colonie et la difficulté du storyteller restent donc les sources de référence,
y compris avec un storyteller compatible autre que celui prévu à terme pour
GateRim SG-1.

Le relais Goa'uld adapte également sa garnison, ses renforts et son niveau de
fortification. Consulte [Progression des menaces Goa'uld](Goauld-Threat-Progression).

## Ce qui n'est pas encore implémenté

- reproduction autonome de la reine ;
- disponibilité naturelle des symbiotes immatures ;
- transfert direct entre deux hôtes sans phase de symbiote libre ;
- sarcophage ;
- approfondissement de la pression et des réactions propres aux domaines
  Goa'uld, selon la ligne directrice encore en cours de validation.

## Différence avec les sanguophages

Un sanguophage transmet un xenogerm. Un Goa'uld devra se déplacer réellement
entre plusieurs corps. Le futur système évitera donc toute duplication
automatique du symbiote.

## Identité visuelle Jaffa générique

Depuis `0.1.73-dev`, les Jaffa reçoivent automatiquement une marque frontale
noire générique rendue comme un tatouage intrinsèque. Depuis `0.1.74-dev`, elle
est stockée comme une donnée persistante propre au personnage plutôt que comme un
gène cosmétique technique.

Cet insigne est uniquement visuel : il ne fournit aucune armure, ne remplace
aucune pièce d'équipement et ne peut pas devenir du butin. Les outils
développeur permettent aussi de l'appliquer ou de le retirer manuellement sur
n'importe quel personnage, y compris un infiltrateur non-Jaffa.

Consulte [Identité visuelle des Jaffa Goa'uld](Jaffa-System-Lord-Visual-Identity).

## Fondation des domaines de Grands Maîtres

Depuis `0.1.74-dev`, l'identité visuelle des serviteurs est préparée par un
profil de domaine piloté par les Defs.

Le domaine prototype associe trois emplacements de rang :

```text
Jaffa ordinaire      -> marque noire
élite sélectionnée   -> marque argentée temporaire
Premier Primat       -> marque dorée embossée temporaire
```

Les variantes argentée et dorée existent comme données intrinsèques de
fondation mais ne sont pas encore attribuées automatiquement.

Consulte [Fondation d'identité des domaines Goa'uld](Goauld-System-Lord-Domain-Identity).
