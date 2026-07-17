# Roadmap

Ce fichier contient uniquement les travaux futurs **décidés**.

- l'historique publié appartient à [`CHANGELOG.md`](CHANGELOG.md) et aux tags Git ;
- les pistes encore ouvertes appartiennent à
  [`IDEAS_TO_REVISIT.md`](IDEAS_TO_REVISIT.md) ;
- les contrats permanents appartiennent aux documents techniques concernés ;
- aucun numéro de version n'est réservé avant la création de la branche du jalon ;
- un jalon terminé est retiré de ce fichier pendant sa finalisation documentaire.

## Prochain jalon décidé

### Finaliser l'icône du mode de casque Jaffa

Remplacer le visuel provisoire de
`Textures/UI/Commands/SG1_JaffaHelmetMode.png` par une icône finale dédiée.

Contrats déjà décidés :

- conserver une seule image transparente `64×64` ;
- ne pas créer de variantes `North`, `South`, `East` ou `West`, car il s'agit
  d'une commande d'interface et non d'un rendu directionnel de pawn ;
- conserver le chemin C# et tous les modes automatique, toujours déployé et
  toujours rétracté ;
- valider la lisibilité à la taille réelle du gizmo, ses états actif, inactif et
  survolé, ainsi que la sauvegarde/recharge du mode choisi ;
- ajouter une copie wiki protégée seulement après validation du visuel réel.

## Jalons différés décidés

### Formes mobiles des symbiotes Goa'uld

Créer un lot distinct pour les pawns mobiles Goa'uld, Tok'ra et reine, qui
partagent encore une image. Ce chantier devra traiter le véritable contrat
directionnel des pawns animaux au lieu de réutiliser les règles des objets
inertes.

### Apparence distinctive de l'officier Jaffa capturable

Donner à la cible de l'opération de capture une identité visuelle clairement
distincte d'un guerrier Jaffa ordinaire, sans modifier les règles de mission ni
le transfert du prisonnier.

### Familles visuelles temporaires restantes

Traiter les familles `P1` et `P2` du
[`VISUAL_ASSET_REGISTER.md`](VISUAL_ASSET_REGISTER.md) par lots cohérents et
séparés : armures, tenues, armes, projectiles et autres commandes. Chaque lot
conserve les chemins et identifiants existants sauf nécessité technique
démontrée.
