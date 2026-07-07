# Jaffa

> Statut : Prototype
> Version d'introduction : 0.1.1-dev
> Séparation de la lignée et du Prim'ta : 0.1.13-dev

## Présentation

![Icône finale du xénotype Jaffa](images/SG1_Jaffa.png)

Les Jaffa sont une lignée humaine modifiée pour subir le Prim'ta et porter un
symbiote Goa'uld immature. L'icône finale reprend la silhouette humaine épurée
des xénotypes vanilla et ajoute la marque d'Apophis, signe immédiatement
reconnaissable de l'identité Jaffa.

## Fondation héréditaire

Un enfant peut naître Jaffa sans porter automatiquement une larve.

Le xenotype contient actuellement :

| Gène germinal | Rôle |
|---|---|
| `lignée jaffa` | Marqueur de lignée |
| `prédisposition à la poche jaffa` | Compatibilité biologique avec le futur Prim'ta |
| `compatibilité avec un symbiote immature` | Compatibilité avec le soutien biologique de la larve |
| `physiologie jaffa` | Capacité de transport augmentée de `+15` |

Les Jaffa ne sont pas tous visiblement massifs. Le xenotype n'impose donc pas la silhouette vanilla `Body_Hulk`.

## Prim'ta

Le Prim'ta est maintenant représenté séparément par un état de santé persistant :

```text
symbiote du Prim'ta
```

Lorsqu'il est présent, il accorde provisoirement :

| Effet | Valeur |
|---|---:|
| Immunité | `×1,5` |
| Guérison des blessures | `×1,5` |
| Dégâts reçus | `×0,9` |
| Espérance de vie | `×1,5` |
| Douleur | `×0,85` |

Depuis `0.1.26-dev`, cet état peut être obtenu par une opération médicale. Depuis `0.1.27-dev`, l'opération consomme une [larve de Prim'ta](Primta-Larva) physique.

## Jaffa dans les personnages de départ

Depuis `0.3.87-dev`, sélectionner le xénotype Jaffa dans la page vanilla des
pawns de départ génère un adulte déjà porteur de son Prim'ta. L'état est visible
dans le panneau Santé avant de confirmer le groupe, y compris après chaque
régénération aléatoire conservant le xénotype.

La larve reste un état biologique distinct du xénotype. Un Jaffa forcé sous
l'âge minimal reste sans Prim'ta. Le mod ne réimplante rien après l'affichage :
un joueur qui retire volontairement l'état avec un mod d'édition peut commencer
ainsi et subir la dépendance normale.

## Évolutions prévues

- cérémonie du Prim'ta ;
- contrôle de l'âge ;
- implantation automatique ;
- dépendance au symbiote ;
- trétonine ;
- conséquences médicales après retrait ;
- sensibilité aux Goa'uld proches ;
- variantes de marques propres aux domaines Goa'uld nommés et attribution automatique des rangs supérieurs.


## Implantation médicale du Prim'ta

Depuis `0.1.26-dev`, un Jaffa compatible peut recevoir l'opération :

```text
implanter un Prim'ta jaffa
```

La réussite ajoute les effets acquis du [Prim'ta](Primta). Consulte
[Implantation du Prim'ta jaffa](Primta-Implantation) pour les conditions
actuelles.


## Larve de Prim'ta

Depuis `0.1.27-dev`, l'implantation médicale exige une
[larve de Prim'ta](Primta-Larva) transportable.

Depuis `0.1.58-dev`, le bassin fait mûrir cette larve à partir de :

```text
1 symbiote immature de Prim'ta issu d'une reine Goa'uld
    +
10 unités de viande crue
```

Depuis `0.2.10-dev`, une reine obtenue par l'incident naturel rare peut fournir
ce symbiote lorsqu'elle est contrôlée par le joueur. Consulte
[Maturation assistée des Prim'ta](Goauld-Queen-Assisted-Maturation).


## Conservation biologique dédiée

Depuis `0.1.59-dev`, le [bassin de conservation du Prim'ta](Primta-Preservation-Basin)
offre un stockage spécialisé alimenté pour les symbiotes immatures et les
larves matures. Il suspend toute aggravation sans réparer une ressource déjà
détériorée. Le réfrigérateur et le congélateur restent utiles lorsque ce bassin
n'est pas disponible.


## Congélation profonde

Depuis `0.1.60-dev`, les ressources biologiques du Prim'ta accumulent des
dommages après une exposition prolongée sous `-15 °C` hors bassin actif.
Consulte [Congélation profonde des Prim'ta](Primta-Deep-Freezing).


## Marques frontales intrinsèques

Depuis `0.1.74-dev`, les marques frontales Jaffa sont stockées comme des données
persistantes propres au personnage. Elles ne font pas partie du xenotype, ne sont pas
des gènes et ne sont pas des vêtements.

La variante noire générique est attribuée automatiquement uniquement aux Jaffa
rattachés à un domaine Goa'uld. Les [Jaffa libres](Free-Jaffa-Faction) restent
sans marque imposée.

Les outils développeur permettent aussi d'appliquer les variantes argentée et
dorée, de retirer une marque, ou d'apposer une marque sur un personnage
non-Jaffa pour un scénario d'infiltration.

Consulte [Identité visuelle des Jaffa Goa'uld](Jaffa-System-Lord-Visual-Identity).


## Faction mondiale des Jaffa libres

Depuis `0.2.2-dev`, une nouvelle planète génère une faction neutre :

```text
Jaffa libres
```

Elle possède un nombre limité de colonies visibles. Ses guerriers et gardes
utilisent la lignée Jaffa, un Prim'ta initial, des Ma'Tok et les armures
modulaires existantes sans recevoir automatiquement une marque frontale
Goa'uld.

Consulte [Jaffa libres](Free-Jaffa-Faction).
