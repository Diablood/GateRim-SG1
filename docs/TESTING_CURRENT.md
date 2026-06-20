# Tests du jalon actif

Jalon : `0.3.12-dev - Audit Tok'ra active identity integration`

Branche : `feature/tokra-active-identity-integration`

Statut : validation fonctionnelle terminée, prêt à publier.

## Résultats validés

- [x] Build `0.3.12.0` réussi.
- [x] Basculement carte → caravane → carte validé.
- [x] Menu unique et sélection de plusieurs Tok'ra validés.
- [x] Frontière colon / invité / prisonnier / esclave / IA validée.
- [x] Bio, Social, Santé et fiche caravane cohérents avec la personnalité active.
- [x] Relations et résumé permanent des deux identités inchangés.
- [x] Anti-cumul, progression commune, sauvegarde et extraction sans régression.
- [x] `Player.log` propre.
- [ ] Mort, cadavre, tombe et résurrection : audit différé, aucun correctif requis dans ce jalon.

## Parcours principal validé

1. Basculer un Tok'ra contrôlé sur la carte de colonie.
2. Former une caravane contenant ce pawn.
3. Sélectionner la caravane et utiliser le gizmo unique `Identités Tok'ra`.
4. Choisir le Tok'ra et l'identité cible dans le menu.
5. Contrôler le nom, les backstories, les compétences, Bio, Social et Santé.
6. Revenir sur une carte et réutiliser le gizmo individuel.
7. Sauvegarder, recharger et vérifier l'absence de dérive.

Résultat validé :

- le même service de basculement fonctionne sur carte et en caravane ;
- aucun bonus ne s'empile et aucune progression n'est perdue ;
- le menu de caravane n'expose que les Tok'ra réellement contrôlés ;
- les deux identités restent visibles dans le résumé de santé ;
- aucune erreur XML, C# ou de traduction n'apparaît.

## Test exploratoire — Tok'ra généré déjà fusionné

Un test supplémentaire avec :

```text
Spawn pawn > SG1_TokraVoluntaryHost
```

a montré que le pawn rejoint correctement les colons et que la carrière active change, mais que le nom d'hôte enregistré peut être identique au nom du symbiote.

Cause conceptuelle : ce PawnKind est généré directement comme Tok'ra déjà fusionné. Il n'existe donc aucun hôte historique préimplantation à capturer.

Décision validée :

- ne pas corriger ce cas par une comparaison de noms ou un reroll tardif ;
- ne pas modifier le flux des implantations réelles ;
- traiter le cas dans un jalon dédié ;
- ajouter un marqueur persistant de source d'identité ;
- générer une identité d'hôte distincte à partir de profils culturels pondérés et configurables ;
- utiliser par défaut un humain hors-monde dans l'état actuel du mod, puis permettre plus tard des origines Tau'ri, Jaffa, Unas ou autres.

Ce résultat ne bloque pas la publication de `0.3.12-dev`, dont le périmètre est l'intégration de l'identité active dans les interfaces et les caravanes.

## Audit encore différé

Lorsque le flux de test est utile et sûr, vérifier ultérieurement :

- mort avec l'hôte actif puis avec le symbiote actif ;
- identité affichée sur le cadavre et la tombe ;
- résurrection et restauration de l'état persistant ;
- interfaces de mods tiers de gestion ou de préparation de pawns.
