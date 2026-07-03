# Raid naturel de Jaffa Goa'uld

> Statut : Jouable avec équilibrage initial
> Première version : `0.2.1-dev`
> Doctrines naturelles : `0.3.54-dev`

Les domaines des Grands Maîtres Goa'uld peuvent lancer de rares raids contre
une colonie joueur à partir du jour 12. Un délai minimal commun de 18 jours
sépare ces raids : les doctrines supplémentaires n'augmentent donc pas leur
fréquence globale.

La force arrive toujours à pied depuis un bord de carte, sans pods vanilla.
Sa taille utilise les points de menace calculés par RimWorld selon la colonie,
la difficulté et le storyteller actif.

## Doctrines possibles

| Doctrine | Condition | Part lorsque toutes sont disponibles |
| --- | --- | ---: |
| Assaut direct | toujours | `50%` |
| Enlèvement | au moins `800` points et 2 colons libres | `25%` |
| Destruction | au moins `1800` points et `10 000` de richesse bâtie | `25%` |

Quand une doctrine spécialisée n'est pas disponible, sa part revient au tirage
entre les doctrines restantes. Les premiers raids sont donc toujours directs.
Avec seulement l'enlèvement disponible, le tirage est environ `67%` direct et
`33%` enlèvement.

L'assaut direct cherche à vaincre la colonie. La doctrine d'enlèvement tente
d'évacuer les colons tombés à terre avant de se replier. La doctrine de
destruction mène d'abord une attaque prolongée contre la colonie, puis les
survivants peuvent récupérer captifs ou objets de valeur avant leur départ.

Les armes et équipements portés par les Jaffa vaincus peuvent devenir une
source naturelle de matériel Goa'uld, notamment les bâtons Ma'Tok.

Consulte aussi [Domaine des Grands Maîtres Goa'uld](Goauld-System-Lord-Faction)
et [Progression des menaces Goa'uld](Goauld-Threat-Progression).
