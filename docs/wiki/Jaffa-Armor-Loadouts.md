# Équipements automatiques des Jaffa Goa'uld

> Statut : prototype jouable
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

## Officier de capture Tok'ra

```text
armure d'officier Jaffa rouge
gantelets blindés Jaffa
bottes renforcées Jaffa
casque d'officier Jaffa rétractable rouge
arme Jaffa
Prim'ta initial
```

Les deux pièces rouges restent à commonalité nulle afin de ne pas apparaître au
hasard. La révision finale `0.3.74-dev-r2` vérifie le loadout après la
génération de la cible
et équipe explicitement l'armure et le casque si RimWorld ne les a pas ajoutés.
Les escortes conservent les ensembles standard. Ce comportement est validé et
publié sous `v0.3.74-dev`.

## Casque rétractable

Le casque est généré en position déployée, puis son mode persistant est
appliqué normalement. En mode automatique, il se rétracte lorsque le porteur
n'est pas enrôlé et se déploie pendant l'enrôlement.

## Utilisation actuelle

Les équipements standard sont utilisés par les Jaffa Goa'uld générés dans les
raids, colonies et missions du mod. L'officier rouge reste pour l'instant réservé
à l'opération de capture. Une extension future pourra en intégrer zéro ou un
dans certains groupes d'au moins cinq Jaffa, sans ajouter un pawn gratuit au
budget de menace. Les textures restent provisoires et pourront être remplacées
lors d'une future passe graphique.
