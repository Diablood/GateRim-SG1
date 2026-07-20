# Armures Jaffa modulaires

> Statut : prototype jouable
> Première version : `0.1.66-dev`
> Gizmo final et contrôle manuel du casque : `0.3.101-dev`

## Présentation

Les armures Jaffa sont modulaires. Elles protègent les zones vitales, mais
également les mains, doigts, pieds et orteils qui restent vulnérables dans
RimWorld.

## Pièces disponibles

```text
armure Jaffa légère
armure Jaffa lourde
gantelets blindés Jaffa
bottes renforcées Jaffa
casque Jaffa rétractable
armure d'officier Jaffa
casque d'officier Jaffa rétractable
```

## Protection localisée

| Pièce | Zones protégées |
|---|---|
| Armure légère | torse, cou, épaules |
| Armure lourde | torse, cou, épaules |
| Armure d'officier | torse, cou, épaules ; `+10 %` d'impact social |
| Gantelets | bras, mains, doigts |
| Bottes | jambes, pieds, orteils |
| Casque déployé | tête complète et visage |

Les gantelets et les bottes sont de véritables équipements défensifs, pas de
simples éléments visuels.

## Casque Jaffa

Depuis `0.3.101-dev`, le casque utilise une bascule manuelle à deux actions :

- `Déployer casque` lorsqu'il est rétracté ;
- `Rétracter casque` lorsqu'il est déployé.

L'enrôlement ne commande plus automatiquement la position. Consulte
[Casque Jaffa rétractable](Jaffa-Retractable-Helmet) pour le gizmo final, la
persistance et la migration des anciennes sauvegardes.

La position modifie sa couverture réelle :

| Position | Couverture |
|---|---|
| Rétracté | sommet de la tête |
| Déployé | tête complète et visage |

Les valeurs brutes d'armure restent identiques. La différence défensive vient
des zones corporelles effectivement couvertes.

## Fabrication

Les pièces standard et les deux variantes d'officier fabricables sont produites
au banc d'usinage après la recherche `Armures Jaffa`. Les coûts et prérequis de
Fabrication augmentent avec le niveau de protection.

## Équipement des Jaffa générés

Les guerriers et gardes Jaffa Goa'uld reçoivent automatiquement des ensembles
adaptés à leur rôle. Pour l'officier de l'opération Tok'ra, la révision finale
`0.3.74-dev-r2` vérifie l'équipement après génération et ajoute explicitement
l'armure et le casque rouges si nécessaire. Ces pièces restent à commonalité
nulle et ne sont donc pas distribuées au hasard aux autres Jaffa. Les armures
standard restent utilisées dans les raids, colonies et missions du mod. Le
loadout distinctif est validé et publié sous `v0.3.74-dev`.

Consulte [Équipements automatiques des Jaffa Goa'uld](Jaffa-Armor-Loadouts).

## Limites actuelles

Les textures portées des armures et casques restent provisoires. Le gizmo de
commande du casque est toutefois final, et les chemins dédiés de l'ensemble
rouge d'officier restent définitifs.
