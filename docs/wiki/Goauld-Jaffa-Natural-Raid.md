# Raid naturel de Jaffa Goa'uld

> Statut : Jouable, évolution `0.3.68-dev` publiée
> Première version : `0.2.1-dev`
> Doctrines naturelles : `0.3.54-dev`
> Pression des conflits ouverts : `0.3.68-dev`

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

## Conflit entre domaines

Sous le storyteller **Commandement SG-1**, un domaine engagé dans au moins un
conflit ouvert ne consacre que `75 %` de ses points habituels à son raid naturel
contre la colonie.

Exemple :

```text
1 200 points calculés par RimWorld
→ doctrine choisie avec 1 200 points
→ force générée avec 900 points
```

La réduction :

- ne se cumule pas avec plusieurs conflits ouverts ;
- disparaît pendant une trêve, une rivalité, une alliance ou la neutralité ;
- disparaît avec Cassandra, Phoebe, Randy ou un storyteller compatible ;
- ne change ni la chance du raid, ni son délai minimal ;
- ne réduit pas les représailles déclenchées après l'extraction d'un Goa'uld ;
- ne s'applique pas aux attaques de mission ou aux outils de test déterministes.

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
