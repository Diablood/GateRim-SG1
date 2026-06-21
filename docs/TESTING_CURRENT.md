# Tests du jalon courant

Jalon : `0.3.28-dev - Audit Tok'ra operation orchestration and long-term recurrence`

Branche publiée : `feature/tokra-operation-orchestration-audit`

Révision locale validée : `0.3.28-dev-r1`

Version de DLL validée : `0.3.28.0`

Tag final : `v0.3.28-dev`

Statut : validation locale terminée ; dépôt principal et wiki publiés.

## Résultat final

- contrôle de cohérence réussi pour `0.3.28-dev`, `0.3.28.0` et `83` backstories ;
- rebuild forcé réussi et DLL `0.3.28.0` chargée ;
- quatre MissionDefs chargés avec leurs durées et données finales ;
- rapport `Tok'ra ops: audit long-term orchestration` terminé avec `Audit result: PASS` ;
- simulations déterministes de `5000` tirages validées pour les quatre paliers de confiance ;
- chaque archétype de poids positif reste atteignable ;
- filtrage `CanOffer(map)` appliqué avant le tirage pondéré ;
- véritable tirage naturel validé sans remplacement d'une opération active ;
- slot global unique conservé ;
- réussite, échec et offre ignorée planifient chacun un nouveau délai caché ;
- archétypes rééligibles et dernier archétype pénalisé localement sans exclusion permanente ;
- séquence non cyclique et variantes RP sans répétition immédiate visible ;
- sauvegarde/rechargement validés pendant un délai caché, une offre et une opération active ;
- observation, renseignements, agent blessé et remise médicale validés sans régression ;
- communicateur limité à l'état joueur courant et outils techniques réservés au mode développeur ;
- fonctionnement validé avec un storyteller compatible différent ;
- `Player.log` final propre.

## Points de régression durables

- filtrer tous les candidats temporairement indisponibles avant tout tirage naturel ;
- conserver un unique slot actif global pour les opérations organiques Tok'ra ;
- programmer un délai caché après réussite, échec et offre ignorée ;
- ne pas consommer un délai complet lorsque des candidats configurés existent mais sont momentanément indisponibles ;
- conserver la pénalité locale du dernier archétype sans créer d'exclusion définitive ni de cycle prévisible ;
- préserver après sauvegarde/rechargement l'état actif, les compteurs, les historiques de textes et la prochaine opportunité ;
- maintenir les poids, délais, récompenses, conséquences et textes dans les MissionDefs ;
- garder les diagnostics complets hors de l'interface normale du joueur ;
- revalider l'orchestration sur toutes les opérations existantes lors de l'ajout d'un site mondial ou d'une mission de caravane ;
- conserver le futur arc d'introduction Tok'ra comme verrou distinct : mission unique avec combat, objet-clé, recherche dédiée, communicateur construit, puis accès au pool récurrent.

## Prochaine étape

Démarrer `0.3.29-dev - Add Tok'ra distress call world-site mission` depuis le tag `v0.3.28-dev` sur une nouvelle branche dédiée.
