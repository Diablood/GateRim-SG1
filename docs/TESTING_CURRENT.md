# Tests du jalon actif

Jalon : `0.3.22-dev - Add an SG-team field cap`

Branche : `feature/sg-team-field-cap`

Base : `v0.3.21-dev`

Révision fonctionnelle validée : `0.3.22-dev-r1`

Version de DLL validée : `0.3.22.0`

Statut : validation fonctionnelle terminée ; jalon clôturé et publié.

## Résultat final

Le protocole ciblé est validé sans correctif fonctionnel après `r1`.

### Dépôt et build

- contrôle de cohérence réussi pour `0.3.22-dev`, `0.3.22.0` et `83` backstories ;
- rebuild forcé réussi ;
- chargement jusqu'au menu principal sans nouvelle erreur de Def, traduction ou texture.

### Couvre-chefs pondérés

Les trois résultats ont été observés parmi les starters du scénario Équipe SG isolée :

```text
casque de terrain SG
casquette de terrain SG
aucun couvre-chef
```

Les probabilités configurées restent :

```text
30 % casque
30 % casquette
40 % aucun couvre-chef
```

Aucun ratio exact n'est exigé sur un petit échantillon.

### Contrôle visuel

- silhouette de casquette à visière lisible ;
- insigne frontal discret ;
- rendu nord, sud, est et ouest validé sur les morphologies testées ;
- aucune texture manquante, bord parasite ou carré opaque ;
- casque et casquette jamais portés simultanément.

### Régressions du scénario

Chaque starter conserve :

```text
tee-shirt vanilla en tissu
pantalon SG olive, noir ou désert
bottes tactiques SG
gants tactiques SG
gilet tactique SG
```

La veste reste facultative et assortie au pantalon. Les armes et fournitures restent inchangées :

```text
1 fusil d'assaut
1 pistolet-mitrailleur
1 pistolet automatique
1 fusil à pompe
4 sacs de couchage
30 repas de survie
20 médicaments industriels
300 acier
150 bois
20 composants industriels
120 tissu
80 cuir ordinaire
```

### Persistance et isolation

- sauvegarde/rechargement avec un porteur de casquette validé ;
- équipement et noms culturels conservés ;
- scénario vanilla non affecté ;
- `Player.log` propre pour le périmètre testé.

## Limites et règles durables

- les poids représentent des probabilités, pas des résultats garantis sur une petite série ;
- toute nouvelle pièce portée doit être vérifiée dans les quatre directions et sur plusieurs morphologies ;
- les options de couvre-chef doivent rester mutuellement exclusives lorsqu'elles partagent la même couche ;
- le slot doit rester piloté par XML sans réintroduire de logique spécifique au scénario ;
- toute page wiki modifiée doit rester cohérente avec la sidebar et être synchronisée lors de la publication.
