# Roadmap

Ce fichier contient uniquement les travaux futurs **décidés**.

- l'historique publié appartient à [`CHANGELOG.md`](CHANGELOG.md) et aux tags Git ;
- les pistes encore ouvertes appartiennent à
  [`IDEAS_TO_REVISIT.md`](IDEAS_TO_REVISIT.md) ;
- les contrats permanents appartiennent aux documents techniques concernés ;
- aucun numéro de version n'est réservé avant la création de la branche du jalon ;
- un jalon terminé est retiré de ce fichier pendant sa finalisation documentaire.

## Prochain jalon décidé

### Finaliser la forme mobile de la reine Goa'uld

Créer un lot séparé pour la reine après la validation de l'archétype adulte
partagé par les Goa'uld et les Tok'ra.

Contrats déjà décidés :

- ne pas réutiliser le simple agrandissement de l'ancien symbiote adulte ;
- utiliser une silhouette reproductrice distincte fondée sur les références de
  reine disponibles ;
- conserver le ThingDef, le PawnKindDef, la production de symbiotes immatures,
  les incidents, la capture et les sauvegardes ;
- traiter le véritable rendu directionnel du pawn animal ;
- valider toutes les orientations, le déplacement, la sélection, l'inspection,
  la capture et la sauvegarde/recharge ;
- ne pas modifier la famille adulte finalisée.

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
