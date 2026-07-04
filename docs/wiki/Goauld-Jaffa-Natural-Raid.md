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
toujours disponible et sert donc de solution de repli. La préférence ne change
ni la fréquence des raids, ni leurs points de menace.

L'assaut direct cherche à vaincre la colonie. La doctrine d'enlèvement tente
d'évacuer les colons tombés à terre avant de se replier. La doctrine de
destruction mène d'abord une attaque prolongée contre la colonie, puis les
survivants peuvent récupérer captifs ou objets de valeur avant leur départ.

Les armes et équipements portés par les Jaffa vaincus peuvent devenir une
source naturelle de matériel Goa'uld, notamment les bâtons Ma'Tok.

Consulte aussi [Domaine des Grands Maîtres Goa'uld](Goauld-System-Lord-Faction),
[Doctrines des domaines Goa'uld](Goauld-Domain-Doctrines)
et [Progression des menaces Goa'uld](Goauld-Threat-Progression).
