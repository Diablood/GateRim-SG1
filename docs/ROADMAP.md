# Roadmap

Ce fichier contient uniquement les travaux futurs **décidés**.

- l'historique publié appartient à [`CHANGELOG.md`](CHANGELOG.md) et aux tags Git ;
- les pistes encore ouvertes appartiennent à
  [`IDEAS_TO_REVISIT.md`](IDEAS_TO_REVISIT.md) ;
- les contrats permanents appartiennent aux documents techniques concernés ;
- aucun numéro de version n'est réservé avant la création de la branche du jalon ;
- un jalon terminé est retiré de ce fichier pendant sa finalisation documentaire.

## Prochain jalon décidé

### Finaliser les formes mobiles des symbiotes Goa'uld

Créer un lot distinct pour les pawns mobiles Goa'uld, Tok'ra et reine, qui
partagent encore une image.

Contrats déjà décidés :

- traiter le véritable rendu directionnel des pawns animaux ;
- distinguer visuellement le Goa'uld libre, le Tok'ra et la reine ;
- conserver les Defs, chemins fonctionnels, identités, implantations et
  sauvegardes ;
- valider toutes les orientations, l'animation de déplacement, la sélection,
  l'inspection, la capture et la sauvegarde/recharge ;
- ne pas réutiliser les règles des objets biologiques inertes finalisés en
  `0.3.96-dev`.

## Jalons différés décidés

### Finaliser les rendus portés directionnels Jaffa

Créer un ou plusieurs lots séparés pour les familles dont seule l'icône au sol
et en inventaire est validée :

- armures légère, lourde et officier ;
- sous-armure textile ;
- casques standard et officier, déployés et rétractés ;
- adapter chaque morphologie et orientation réellement utilisée ;
- ne pas rouvrir les icônes non directionnelles validées en `0.3.102-dev` ;
- ne pas créer de variantes portées inutiles pour les pièces que le renderer
  vanilla ne montre pas.

### Intégrer les nouvelles pièces aux tenues Jaffa du monde et des missions

Après validation fonctionnelle des nouveaux objets :

- ajouter sous-armure, pantalon et ceinture aux équipements cohérents ;
- définir leur répartition selon les rôles et niveaux d'armure ;
- mettre à jour les Jaffa générés par les factions du monde ;
- mettre à jour les Jaffa des missions et opérations ;
- attribuer l'armure et le casque d'officier aux cibles et rôles concernés ;
- préserver les équipements distinctifs des officiers et cibles spéciales ;
- valider génération, raids, sites, missions, équipement, mort, capture et
  sauvegarde/rechargement.

### Refactoriser les états du casque Jaffa rétractable

Revoir l'architecture du casque déployé et rétracté :

- conserver le casque déployé comme objet public avec son icône au sol ;
- éviter qu'un état rétracté interne dépende d'une texture portée distincte
  lorsqu'elle n'est pas nécessaire ;
- préserver les identifiants de sauvegarde et la migration des anciennes parties ;
- préserver l'isolation entre les paires standard et officier ;
- revalider le gizmo manuel, la couverture corporelle et la persistance.

### Familles visuelles temporaires restantes

Traiter les familles `P1` et `P2` du
[`VISUAL_ASSET_REGISTER.md`](VISUAL_ASSET_REGISTER.md) par lots cohérents et
séparés. Chaque lot conserve les chemins et identifiants existants sauf nécessité
technique démontrée.