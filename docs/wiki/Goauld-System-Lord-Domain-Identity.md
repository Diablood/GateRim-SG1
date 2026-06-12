# Fondation d'identité des domaines Goa'uld

> Statut : Fondation technique  
> Version d'introduction : 0.1.74-dev

## Présentation

Le domaine Goa'uld prototype possède un profil d'identité piloté par les Defs.

```text
SG1_GoauldSystemLordDomainPrototype
```

Ce profil est associé à la faction mondiale Goa'uld. Depuis `0.2.1-dev`,
cette faction possède des colonies visibles et un premier raid naturel rare
d'assaut direct.

## Emplacements de rang intrinsèques

| Rang | Variante temporaire |
|---|---|
| Jaffa ordinaire | marque noire intrinsèque générique |
| élite sélectionnée | marque argentée intrinsèque générique |
| Premier Primat | marque dorée embossée intrinsèque générique |

Ces emplacements ne référencent plus des gènes. Les variantes argentée et dorée
sont disponibles pour les tests développeur mais ne sont pas encore attribuées
automatiquement.

## Pourquoi séparer le rang de l'armure

Un garde en armure lourde n'est pas automatiquement un Premier Primat. Le
futur système devra sélectionner explicitement le rôle et la marque
correspondante.

## Extension future

Les futurs Grands Maîtres pourront définir leurs propres profils :

```text
domaine d'Apophis
domaine de Ba'al
domaine de Sokar
autres domaines
```

Chaque domaine pourra remplacer les trois variantes sans modifier les
doctrines de raid ou le rendu intrinsèque déjà validé.

## Limites

Les colonies visibles et les rares assauts directs naturels sont actifs.
Les marchands, les raids naturels d'enlèvement et les raids naturels de
destruction restent désactivés. Les visuels argenté et doré sont temporaires.
