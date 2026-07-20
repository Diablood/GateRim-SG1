# Casque Jaffa rétractable

> Statut : contrôle manuel jouable
> Version d'introduction : `0.1.67-dev`
> Bascule manuelle et gizmo final : `0.3.101-dev`

## Commande

![Gizmo final du casque Jaffa](images/SG1_JaffaHelmetMode.png)

Le casque n'utilise plus de mode automatique lié à l'enrôlement. Le gizmo affiche
directement l'action disponible :

| Position actuelle | Action du gizmo |
|---|---|
| Rétracté | `Déployer casque` |
| Déployé | `Rétracter casque` |

Un clic applique immédiatement la nouvelle position. Enrôler ou désenrôler le
Jaffa ne change plus le casque.

## Protection

Les valeurs brutes d'armure restent identiques. Seule la couverture varie :

| Position | Couverture |
|---|---|
| Rétracté | sommet de la tête (`UpperHead`) |
| Déployé | tête complète et visage (`FullHead`) |

La position choisie est conservée après sauvegarde et rechargement.

## Variante d'officier

La paire rouge réservée à l'officier capturable reste séparée de la paire
ordinaire. Le même gizmo manuel est utilisé, mais chaque casque commute
uniquement avec sa propre variante déployée ou rétractée.

## Anciennes sauvegardes

Une ancienne sauvegarde encore réglée sur le mode automatique conserve la
position physique enregistrée au chargement. Cette position devient ensuite un
choix manuel persistant.
