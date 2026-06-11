# Domaine d'un Grand Maître Goa'uld

> Statut : Prototype
> Version d'introduction : 0.1.61-dev
> Extension Jaffa serviteurs : 0.1.62-dev
> Attribution initiale du Prim'ta : 0.1.63-dev
> Premier équipement Jaffa : 0.1.64-dev
> Loadout Ma'Tok automatique : 0.1.65-dev
> Armures Jaffa modulaires : 0.1.66-dev
> Casque Jaffa rétractable : 0.1.67-dev
> Loadouts d'armures automatiques : 0.1.68-dev
> Raid Jaffa contrôlé : 0.1.69-dev
> Stratégie de raid contrôlé explicite : 0.1.70-dev
> Raid d'enlèvement Jaffa contrôlé : 0.1.71-dev
> Raid de destruction Jaffa contrôlé : 0.1.72-dev
> Identité visuelle Jaffa générique : 0.1.73-dev

## Présentation

Le premier domaine d'un Grand Maître Goa'uld est une fondation technique
hostile. Il prépare l'arrivée progressive des groupes ennemis, raids, colonies
et événements Goa'uld.

```text
domaine d'un Grand Maître Goa'uld
```

## Fonctionnement actuel

La définition reste volontairement limitée :

- hostilité permanente ;
- faction masquée ;
- aucune génération automatique au démarrage ;
- aucune colonie mondiale ;
- aucun raid naturel ;
- aucun marchand ;
- aucun site de quête.

Depuis `0.1.62-dev`, deux premiers `PawnKindDef` de serviteurs Jaffa sont
disponibles pour les tests développeur :

```text
guerrier Jaffa au service des Goa'uld
garde Jaffa au service des Goa'uld
```

Les deux variantes forcent la lignée Jaffa héréditaire existante. Le domaine
possède également un profil technique `Combat` composé majoritairement de
guerriers et plus rarement de gardes.

Depuis `0.1.63-dev`, chacun de ces serviteurs reçoit automatiquement un
Prim'ta initial lorsqu'il est généré. Cette attribution n'est effectuée
qu'une fois par pawn : une larve retirée ultérieurement ne réapparaît pas.

## Pourquoi la faction reste masquée

Le profil de groupe prépare les futurs contenus hostiles, mais il n'est pas
encore relié à une génération naturelle. Les pawns peuvent être générés
manuellement pour vérifier leur xenotype.

Depuis `0.1.64-dev`, un premier [bâton Ma'Tok](Matok-Staff) jouable peut être
fabriqué et testé manuellement. Depuis `0.1.65-dev`, les guerriers et gardes
Jaffa générés le reçoivent automatiquement via le système vanilla de loadout.
Depuis `0.1.66-dev`, cinq [armures Jaffa modulaires](Jaffa-Armor) peuvent être
fabriquées et testées manuellement : deux torses, des gantelets protégeant
les doigts, des bottes protégeant les orteils et un casque déployé.
Depuis `0.1.67-dev`, le casque possède trois modes rétractables persistants.
Depuis `0.1.68-dev`, les guerriers et gardes générés reçoivent automatiquement
leur ensemble modulaire adapté. Les marques faciales et l'équilibrage doivent
encore être complétés avant l'activation
des raids et des colonies.

## Suite prévue

```text
identité visuelle
    ↓
tests contrôlés de groupes ennemis
    ↓
activation progressive des raids et de la présence mondiale
```

## Marque frontale Jaffa générique

Depuis `0.1.73-dev`, les serviteurs Jaffa générés reçoivent une marque
frontale noire intrinsèque rendue comme un tatouage. Cette première couche
visuelle reste indépendante des doctrines de raid et de l'armure modulaire.

Elle prépare des variantes propres aux futurs domaines de Grands Maîtres.

Consulte [Identité visuelle des Jaffa Goa'uld](Jaffa-System-Lord-Visual-Identity).
