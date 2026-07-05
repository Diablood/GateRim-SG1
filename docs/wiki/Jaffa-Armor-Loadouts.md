# Équipements automatiques des Jaffa Goa'uld

> Statut : officiers de forces éligibles validés dans `0.3.75-dev`
> Première version : `0.1.68-dev`

## Présentation

Les serviteurs Jaffa générés pour les domaines Goa'uld reçoivent
automatiquement un ensemble d'armure et d'armes cohérent avec leur rôle.

## Guerrier Jaffa Goa'uld

```text
armure Jaffa légère
gantelets blindés Jaffa
bottes renforcées Jaffa
casque Jaffa rétractable
bâton Ma'Tok
Prim'ta initial
```

## Garde Jaffa Goa'uld

```text
armure Jaffa lourde
gantelets blindés Jaffa
bottes renforcées Jaffa
casque Jaffa rétractable
bâton Ma'Tok ou Zat'nik'tel
Prim'ta initial
```

Le Zat'nik'tel reste plus rare et concerne surtout certains gardes.

## Officier Jaffa Goa'uld

```text
armure d'officier Jaffa rouge
gantelets blindés Jaffa
bottes renforcées Jaffa
casque d'officier Jaffa rétractable rouge
arme Jaffa
Prim'ta initial
```

Les deux pièces rouges restent à commonalité nulle afin de ne pas apparaître au
hasard. Depuis `0.3.74-dev`, le mod vérifie le loadout après la génération et
équipe explicitement l'armure et le casque si RimWorld ne les a pas ajoutés.
Depuis `0.3.75-dev`, le même rang peut remplacer un garde dans certains groupes
Goa'uld d'au moins cinq Jaffa. Un seul officier est permis par groupe, sans pawn
supplémentaire ni hausse du budget de menace.

## Casque rétractable

Le casque est généré en position déployée, puis son mode persistant est
appliqué normalement. En mode automatique, il se rétracte lorsque le porteur
n'est pas enrôlé et se déploie pendant l'enrôlement.

## Utilisation actuelle

Les équipements standard restent utilisés par les guerriers et gardes Goa'uld.
L'officier rouge apparaît comme cible unique de l'opération de capture et peut
également remplacer un garde dans :

- les raids naturels Goa'uld ;
- les groupes de défense des colonies Goa'uld ;
- les défenses de l'introduction et des appels de détresse ;
- les défenseurs et renforts de relais ;
- les interceptions de livraison et les assauts de diversion.

Le groupe doit contenir au moins cinq Jaffa. L'officier de terrain remplace un
garde à `145` points et l'officier de garnison un garde à `130` points. La cible
de capture conserve son profil historique à `165` points. L'effectif et le budget
des groupes générés restent donc inchangés. Les textures
restent provisoires et pourront être remplacées lors d'une future passe
graphique. La révision finale `r2` valide le seuil de cinq Jaffa, le
remplacement unique sans hausse du budget et la conservation du loadout après
sauvegarde/rechargement.
