# Validation locale finale - 0.3.66-dev

Jalon : `0.3.66-dev - Add persistent Goa'uld inter-domain relations`

Branche : `feature/goauld-inter-domain-relations`

Base : `v0.3.65-dev`

Version de DLL validée : `0.3.66.0`

Révision locale finale : `r1`

Statut : build, tests fonctionnels, régressions et publication validés.

Chargement utilisé :

```text
Core
Harmony
Biotech
GateRim SG-1
```

## Résultat final

La validation de la révision `r1` confirme :

- rebuild forcé `0.3.66.0` réussi ;
- chargement de la partie sans erreur rouge ;
- création canonique d'une relation pour une paire de domaines distincts ;
- état initial `neutralité` ;
- commandes directes validées pour `rivalité`, `conflit ouvert`, `trêve`,
  `alliance` et retour à `neutralité` ;
- lettres françaises nommant correctement les deux domaines ;
- variantes RP sans répétition immédiate de la même clé lorsque plusieurs
  variantes sont disponibles ;
- sauvegarde/rechargement conservant paire, état, compteur et échéances ;
- rapport d'orchestration détectant correctement le tracker et l'activation ;
- suspension réelle sous Cassandra sans lettre, transition ni retard accumulé ;
- reprise normale après retour à `Commandement SG-1` ;
- anti-répétition de la dernière paire avec trois domaines ;
- transitions limitées au graphe documenté ;
- doctrines de domaine, raids directs, enlèvements, destructions, ultimatums et
  représailles inchangés ;
- absence d'effet nouveau sur menace, goodwill, territoire ou colonies ;
- `Player.log` propre pour les erreurs C#, XML, traduction, Scribe, faction,
  lettre et storyteller.

## Outils validés

Chemin exact :

```text
Actions de débogage
> GateRim SG-1
> Goa'uld inter-domain relations...
```

Actions couvertes :

- `Show relation report`;
- `Create additional test domain`;
- `Reconcile relation pairs`;
- `Force next transition`;
- setters directs des cinq états ;
- `Reset relations`.

Le rapport storyteller reste disponible sous :

```text
Actions de débogage
> GateRim SG-1
> Storyteller SG-1...
> Show orchestration report
```

## Limites conservées

Cette validation ne rend actif aucun effet militaire ou territorial lié aux
relations. Les réductions de pression en conflit, batailles inter-domaines,
bonus d'alliance, renforts, raids conjoints, expansion et destruction de
colonies restent réservés à des jalons ultérieurs.

## Publication

- branche publiée : `feature/goauld-inter-domain-relations` ;
- tag annoté final unique : `v0.3.66-dev` ;
- dépôt principal publié ;
- brouillons wiki synchronisés et wiki séparé publié.
