# Raid naturel de Jaffa Goa'uld

> Statut : jouable, officiers éligibles `0.3.75-dev` validés
> Première version : `0.2.1-dev`
> Doctrines naturelles : `0.3.54-dev`
> Pression des conflits ouverts : `0.3.68-dev`
> Puissance bornée des alliances : `0.3.73-dev`
> Officiers dans les groupes éligibles : `0.3.75-dev`
> Renforts alliés différés : `0.3.78-dev`

Les domaines des Grands Maîtres Goa'uld peuvent lancer de rares raids contre
une colonie joueur à partir du jour 12. Un délai minimal commun de 18 jours
sépare ces raids : les doctrines supplémentaires et les relations entre
domaines n'augmentent donc pas leur fréquence globale.

La force arrive toujours à pied depuis un bord de carte, sans pods vanilla.
Sa taille part des points de menace calculés par RimWorld selon la colonie, la
difficulté et le storyteller actif.

## Doctrines possibles

| Doctrine | Condition |
| --- | --- |
| Assaut direct | toujours |
| Enlèvement | au moins `800` points et 2 colons libres |
| Destruction | au moins `1800` points et `10 000` de richesse bâtie |

Depuis `0.3.64-dev`, chaque domaine possède une préférence persistante :

| Profil du domaine | Direct | Enlèvement | Destruction |
| --- | ---: | ---: | ---: |
| Conquête | `4` | `1` | `1` |
| Asservissement | `2` | `3` | `1` |
| Terre brûlée | `2` | `1` | `3` |

Ces valeurs sont des poids relatifs, pas des chances fixes. Lorsqu'une doctrine
spécialisée n'est pas admissible, son poids devient nul. L'assaut direct reste
toujours disponible et sert donc de solution de repli.

Les conditions et la préférence de doctrine utilisent toujours les points
initiaux calculés par RimWorld.

## Effet des relations entre domaines

Sous le storyteller **Commandement SG-1**, la relation du domaine attaquant
peut modifier uniquement les points transmis à la force après le choix de la
doctrine :

| Situation du domaine | Facteur final |
| --- | ---: |
| au moins un conflit ouvert | `75 %` |
| aucune guerre ouverte et au moins une alliance | `110 %` |
| neutralité, rivalité ou trêve seulement | `100 %` |

Exemples :

```text
1 200 points calculés par RimWorld + conflit ouvert
→ doctrine choisie avec 1 200 points
→ force générée avec 900 points

1 200 points calculés par RimWorld + alliance seule
→ doctrine choisie avec 1 200 points
→ force générée avec 1 320 points
```

Les garde-fous sont stricts :

- plusieurs conflits ne réduisent jamais sous `75 %` ;
- plusieurs alliances n'augmentent jamais au-delà de `110 %` ;
- un conflit ouvert est prioritaire lorsqu'un domaine possède aussi une
  alliance ;
- aucun facteur n'est appliqué avec Cassandra, Phoebe, Randy ou un storyteller
  compatible ;
- la chance du raid, son premier jour et son délai minimal ne changent pas ;
- les représailles, missions, sites et outils de test déterministes gardent
  leurs points d'origine.

Le worker commun marque techniquement les raids comme forcés pendant leur
génération. `0.3.73-dev` distingue donc l'appel naturel du storyteller d'un
appel qui était déjà forcé avant d'entrer dans ce worker. Le raid naturel reçoit
bien son facteur, tandis que les représailles et tests exacts restent exclus.

## Renforts d'un domaine allié

Depuis `0.3.78-dev`, un raid sous **Commandement SG-1** bénéficiant du facteur
allié et disposant d'au moins `800` points finaux peut partager son budget avec
un second domaine :

- `75 %` pour la force principale ;
- `25 %` pour une vague alliée différée.

Le total reste donc celui du facteur `110 %` déjà annoncé. Aucun groupe gratuit
n'est ajouté. Une seule alliance est retenue, même si le domaine possède
plusieurs partenaires.

Les renforts marchent depuis un bord de carte après un court délai caché. Aucune
lettre, alerte ou minuterie ne prévient le joueur. Une lettre RP nomme le domaine
allié seulement lorsque ses Jaffa entrent réellement sur la carte. Les deux
couleurs de faction restent visibles et les forces coopèrent pendant cette
attaque au lieu de se battre entre elles. Les pods restent exclus.

## Officier dans une force éligible

À partir de `0.3.75-dev`, un raid naturel contenant au moins cinq Jaffa peut
remplacer un garde par un officier en armure rouge. Le groupe ne reçoit aucun
pawn supplémentaire : le garde et l'officier de terrain utilisent tous deux
`145` points de combat. La cible propre à l'opération de capture conserve sa
valeur historique de `165` et n'est pas utilisée par ce chemin. Un groupe trop
petit ou sans garde approprié reste inchangé, et un
même groupe ne reçoit jamais plus d'un officier. La révision finale `r2` valide
le seuil, le remplacement sans surcoût, la sauvegarde/rechargement et l'absence
de régression sur les raids exclus.

L'assaut direct cherche à vaincre la colonie. La doctrine d'enlèvement tente
d'évacuer les colons tombés à terre avant de se replier. La doctrine de
destruction mène d'abord une attaque prolongée contre la colonie, puis les
survivants peuvent récupérer captifs ou objets de valeur avant leur départ.

Les armes et équipements portés par les Jaffa vaincus peuvent devenir une
source naturelle de matériel Goa'uld, notamment les bâtons Ma'Tok.

Consulte aussi [Domaine des Grands Maîtres Goa'uld](Goauld-System-Lord-Faction),
[Doctrines des domaines Goa'uld](Goauld-Domain-Doctrines),
[Storyteller GateRim SG-1](Storyteller-SG1)
et [Progression des menaces Goa'uld](Goauld-Threat-Progression).
